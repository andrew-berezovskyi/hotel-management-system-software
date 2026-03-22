using System.Data;
using hotel_management_system.Helpers;
using HotelManagementSystem.Models;
using HotelManagementSystem.Data;
using HotelManagementSystem.Data.SqlQueries;

namespace hotel_management_system.Services
{
    public class RoomService
    {
        public RoomService()
        {
            try
            {
                // створюємо таблицю лише при потребі
                DbHelper.ExecuteNonQuery(RoomQueries.CreateTable);
            }
            catch (Exception ex)
            {
                LoggerHelper.LogException(ex, "RoomService.Constructor");
            }
        }

        // ------------------- GET ALL -------------------
        public List<Room> GetAllRooms()
        {
            var list = new List<Room>();

            try
            {
                var table = new DataTable();

                DbHelper.ExecuteQuery(RoomQueries.GetAll, null, reader =>
                {
                    table.Load(reader);
                });

                foreach (DataRow row in table.Rows)
                {
                    list.Add(new Room
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        Number = row["Number"].ToString(),
                        Type = row["Type"].ToString(),
                        Price = Convert.ToDecimal(row["Price"]),
                        Status = row["Status"].ToString(),
                        ImagePath = row["ImagePath"]?.ToString() ?? string.Empty
                    });
                }
            }
            catch (Exception ex)
            {
                LoggerHelper.LogException(ex, "RoomService.GetAllRooms");
            }

            return list;
        }

        // ------------------- ADD -------------------
        public bool AddRoom(Room room)
        {
            try
            {
                int rows = DbHelper.ExecuteNonQuery(RoomQueries.Insert, cmd =>
                {
                    cmd.Parameters.AddWithValue("@Number", room.Number);
                    cmd.Parameters.AddWithValue("@Type", room.Type);
                    cmd.Parameters.AddWithValue("@Price", room.Price);
                    cmd.Parameters.AddWithValue("@Status", room.Status);
                    cmd.Parameters.AddWithValue("@ImagePath", room.ImagePath ?? string.Empty);
                });

                return rows > 0;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogException(ex, "RoomService.AddRoom");
                return false;
            }
        }

        // ------------------- UPDATE -------------------
        public bool UpdateRoom(Room room)
        {
            try
            {
                int rows = DbHelper.ExecuteNonQuery(RoomQueries.Update, cmd =>
                {
                    cmd.Parameters.AddWithValue("@Number", room.Number);
                    cmd.Parameters.AddWithValue("@Type", room.Type);
                    cmd.Parameters.AddWithValue("@Price", room.Price);
                    cmd.Parameters.AddWithValue("@Status", room.Status);
                    cmd.Parameters.AddWithValue("@ImagePath", room.ImagePath ?? string.Empty);
                    cmd.Parameters.AddWithValue("@Id", room.Id);
                });

                return rows > 0;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogException(ex, "RoomService.UpdateRoom");
                return false;
            }
        }

        // ------------------- DELETE -------------------
        public bool DeleteRoom(int id)
        {
            try
            {
                return DbHelper.ExecuteNonQuery(RoomQueries.Delete, cmd =>
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                }) > 0;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogException(ex, "RoomService.DeleteRoom");
                return false;
            }
        }

        // ------------------- GET BY NUMBER -------------------
        public Room GetRoomByNumber(string number)
        {
            Room room = null;

            try
            {
                DbHelper.ExecuteQuery(RoomQueries.GetByNumber,
                    cmd => cmd.Parameters.AddWithValue("@Number", number),
                    reader =>
                    {
                        if (reader.Read())
                        {
                            room = new Room
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Number = reader["Number"].ToString(),
                                Type = reader["Type"].ToString(),
                                Price = Convert.ToDecimal(reader["Price"]),
                                Status = reader["Status"].ToString(),
                                ImagePath = reader["ImagePath"]?.ToString() ?? string.Empty
                            };
                        }
                    });
            }
            catch (Exception ex)
            {
                LoggerHelper.LogException(ex, "RoomService.GetRoomByNumber");
            }

            return room;
        }

        // ------------------- AVAILABILITY (NEW) -------------------
        /// <summary>
        /// true, якщо немає активних бронювань, що перетинаються з інтервалом [checkIn, checkOut).
        /// </summary>
        public bool IsRoomAvailable(int roomId, DateTime checkIn, DateTime checkOut)
        {
            try
            {
                const string sql = @"
                    SELECT COUNT(*)
                    FROM Bookings
                    WHERE RoomId = @rid AND Status = 'Active'
                      AND NOT (date(CheckOutDate) <= date(@cin) OR date(CheckInDate) >= date(@cout));";

                long count = 0;

                DbHelper.ExecuteQuery(sql,
                    cmd =>
                    {
                        cmd.Parameters.AddWithValue("@rid", roomId);
                        cmd.Parameters.AddWithValue("@cin", checkIn.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@cout", checkOut.ToString("yyyy-MM-dd"));
                    },
                    reader =>
                    {
                        if (reader.Read())
                            count = Convert.ToInt64(reader[0]);
                    });

                return count == 0;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogException(ex, "RoomService.IsRoomAvailable");
                return false; // консервативно
            }
        }
    }
}
