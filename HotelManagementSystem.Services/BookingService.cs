using System.Data;
using hotel_management_system.Helpers;
using HotelManagementSystem.Models;
using HotelManagementSystem.Data;
using HotelManagementSystem.Data.SqlQueries;

namespace hotel_management_system.Services
{
    public class BookingService
    {
        public BookingService()
        {
            try
            {
                DbHelper.ExecuteNonQuery(BookingQueries.CreateTable);
            }
            catch (Exception ex)
            {
                LoggerHelper.LogException(ex, "BookingService.Constructor");
            }
        }

        // ---------------------- AUTO UPDATE ----------------------
        public void AutoUpdateBookingStatuses()
        {
            try
            {
                DbHelper.ExecuteNonQuery(BookingQueries.AutoUpdateBookings);
                DbHelper.ExecuteNonQuery(BookingQueries.AutoUpdateRooms);
            }
            catch (Exception ex)
            {
                LoggerHelper.LogException(ex, "BookingService.AutoUpdateBookingStatuses");
            }
        }

        // ---------------------- MAP ROW -> BOOKING ----------------------
        private Booking MapBooking(DataRow row)
        {
            return new Booking
            {
                Id = Convert.ToInt32(row["Id"]),
                UserId = Convert.ToInt32(row["UserId"]),
                RoomId = Convert.ToInt32(row["RoomId"]),
                UserName = row["UserName"].ToString(),
                RoomNumber = row["RoomNumber"].ToString(),
                CheckInDate = row["CheckInDate"].ToString(),
                CheckOutDate = row["CheckOutDate"].ToString(),
                TotalPrice = Convert.ToDecimal(row["TotalPrice"]),
                Status = row["Status"].ToString(),
                PaymentMethod = row["PaymentMethod"].ToString(),
                Notes = row["Notes"].ToString(),
                CreatedAt = row["CreatedAt"].ToString()
            };
        }

        // ---------------------- GET ALL BOOKINGS (ADMIN) ----------------------
        public List<Booking> GetAllBookings(string term = null)
        {
            AutoUpdateBookingStatuses();
            var list = new List<Booking>();
            string sql = BookingQueries.GetAll;

            if (!string.IsNullOrWhiteSpace(term))
                sql += " WHERE u.FullName LIKE @q OR r.Number LIKE @q OR b.Status LIKE @q";

            try
            {
                var table = new DataTable();

                DbHelper.ExecuteQuery(sql,
                    cmd =>
                    {
                        if (!string.IsNullOrWhiteSpace(term))
                            cmd.Parameters.AddWithValue("@q", $"%{term.Trim()}%");
                    },
                    reader => { table.Load(reader); });

                foreach (DataRow row in table.Rows)
                {
                    list.Add(MapBooking(row));
                }
            }
            catch (Exception ex)
            {
                LoggerHelper.LogException(ex, "BookingService.GetAllBookings");
            }

            return list;
        }

        // ---------------------- GET USER BOOKINGS (ACCOUNT) ----------------------
        public List<Booking> GetUserBookings(int userId)
        {
            AutoUpdateBookingStatuses();
            var list = new List<Booking>();

            try
            {
                var table = new DataTable();

                DbHelper.ExecuteQuery(BookingQueries.GetByUser,
                    cmd => cmd.Parameters.AddWithValue("@UserId", userId),
                    reader => { table.Load(reader); });

                foreach (DataRow row in table.Rows)
                {
                    list.Add(MapBooking(row));
                }
            }
            catch (Exception ex)
            {
                LoggerHelper.LogException(ex, "BookingService.GetUserBookings");
            }

            return list;
        }

        // ---------------------- ADD BOOKING ----------------------
        public bool AddBooking(Booking booking)
        {
            try
            {
                object result = DbHelper.ExecuteScalar(BookingQueries.Insert, cmd =>
                {
                    cmd.Parameters.AddWithValue("@UserId", booking.UserId);
                    cmd.Parameters.AddWithValue("@RoomId", booking.RoomId);
                    cmd.Parameters.AddWithValue("@CheckInDate", booking.CheckInDate);
                    cmd.Parameters.AddWithValue("@CheckOutDate", booking.CheckOutDate);
                    cmd.Parameters.AddWithValue("@TotalPrice", booking.TotalPrice);
                    cmd.Parameters.AddWithValue("@Status", booking.Status ?? "Active");
                    cmd.Parameters.AddWithValue("@PaymentMethod", booking.PaymentMethod ?? "Unpaid");
                    cmd.Parameters.AddWithValue("@Notes", booking.Notes ?? "");
                });

                int newBookingId = Convert.ToInt32(result);

                // авто-створення платежу
                if (booking.PaymentMethod != null && booking.PaymentMethod != "Unpaid")
                {
                    try
                    {
                        var paymentService = new PaymentService();
                        var payment = new Payment
                        {
                            BookingId = newBookingId,
                            Amount = booking.TotalPrice,
                            PaymentDate = DateTime.Now.ToString("yyyy-MM-dd"),
                            PaymentMethod = booking.PaymentMethod,
                            Status = "Completed",
                            Notes = "Auto-generated payment from booking"
                        };
                        paymentService.AddPayment(payment);
                    }
                    catch (Exception ex)
                    {
                        LoggerHelper.LogException(ex, "BookingService.AddBooking -> Auto Payment");
                    }
                }

                UpdateRoomStatus(booking.RoomId, "Booked");
                return newBookingId > 0;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogException(ex, "BookingService.AddBooking");
                return false;
            }
        }

        // ---------------------- UPDATE BOOKING ----------------------
        public bool UpdateBooking(Booking booking)
        {
            try
            {
                int rows = DbHelper.ExecuteNonQuery(BookingQueries.Update, cmd =>
                {
                    cmd.Parameters.AddWithValue("@UserId", booking.UserId);
                    cmd.Parameters.AddWithValue("@RoomId", booking.RoomId);
                    cmd.Parameters.AddWithValue("@CheckInDate", booking.CheckInDate);
                    cmd.Parameters.AddWithValue("@CheckOutDate", booking.CheckOutDate);
                    cmd.Parameters.AddWithValue("@TotalPrice", booking.TotalPrice);
                    cmd.Parameters.AddWithValue("@Status", booking.Status);
                    cmd.Parameters.AddWithValue("@PaymentMethod", booking.PaymentMethod);
                    cmd.Parameters.AddWithValue("@Notes", booking.Notes);
                    cmd.Parameters.AddWithValue("@Id", booking.Id);
                });
                return rows > 0;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogException(ex, "BookingService.UpdateBooking");
                return false;
            }
        }

        // ---------------------- CANCEL BY ADMIN ----------------------
        public bool CancelBookingByAdmin(Booking booking)
        {
            try
            {
                int rows = DbHelper.ExecuteNonQuery(BookingQueries.CancelByAdmin, cmd =>
                {
                    cmd.Parameters.AddWithValue("@Id", booking.Id);
                });

                if (rows > 0)
                {
                    UpdateRoomStatus(booking.RoomId, "Available");
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogException(ex, "BookingService.CancelBookingByAdmin");
                return false;
            }
        }

        // ---------------------- CANCEL BY USER ----------------------
        public bool CancelBookingByUser(Booking booking)
        {
            try
            {
                int rows = DbHelper.ExecuteNonQuery(BookingQueries.CancelByUser, cmd =>
                {
                    cmd.Parameters.AddWithValue("@Id", booking.Id);
                    cmd.Parameters.AddWithValue("@UserId", booking.UserId);
                });

                if (rows > 0)
                {
                    UpdateRoomStatus(booking.RoomId, "Available");
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogException(ex, "BookingService.CancelBookingByUser");
                return false;
            }
        }

        // ---------------------- OLD DELETE (якщо коли-небудь знадобиться) ----------------------
        public bool DeleteBooking(int bookingId)
        {
            try
            {
                return DbHelper.ExecuteNonQuery(BookingQueries.Delete, cmd =>
                {
                    cmd.Parameters.AddWithValue("@Id", bookingId);
                }) > 0;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogException(ex, "BookingService.DeleteBooking");
                return false;
            }
        }

        // ---------------------- UPDATE ROOM STATUS ----------------------
        private void UpdateRoomStatus(int roomId, string status)
        {
            try
            {
                DbHelper.ExecuteNonQuery(BookingQueries.UpdateRoomStatus, cmd =>
                {
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.AddWithValue("@Id", roomId);
                });
            }
            catch (Exception ex)
            {
                LoggerHelper.LogException(ex, "BookingService.UpdateRoomStatus");
            }
        }
        public bool IsRoomAvailable(int roomId, DateTime checkIn, DateTime checkOut)
        {
            try
            {
                // Спочатку оновлюємо стани старих бронювань
                AutoUpdateBookingStatuses();

                const string sql = @"
            SELECT COUNT(*)
            FROM Bookings
            WHERE RoomId = @RoomId
              AND Status = 'Active'
              -- якщо інтервали ПЕРЕТИНАЮТЬСЯ
              AND NOT (
                    date(CheckOutDate) <= date(@CheckIn)
                OR  date(CheckInDate) >= date(@CheckOut)
              );";

                object result = DbHelper.ExecuteScalar(sql, cmd =>
                {
                    cmd.Parameters.AddWithValue("@RoomId", roomId);
                    cmd.Parameters.AddWithValue("@CheckIn", checkIn.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@CheckOut", checkOut.ToString("yyyy-MM-dd"));
                });

                int count = Convert.ToInt32(result);
                // якщо активних перетинів 0 – кімната вільна
                return count == 0;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogException(ex, "BookingService.IsRoomAvailable");
                // у випадку помилки краще вважати, що кімната ЗАЙНЯТА, щоб не було подвійних бронювань
                return false;
            }
        }

    }
}
