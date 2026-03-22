using System.Data;
using hotel_management_system.Helpers;
using HotelManagementSystem.Models;
using HotelManagementSystem.Data;
using HotelManagementSystem.Data.SqlQueries;

namespace hotel_management_system.Services
{
    public class PaymentService
    {
        public PaymentService()
        {
            try
            {
                // створюємо таблицю лише якщо її ще немає
                DbHelper.ExecuteNonQuery(PaymentQueries.CreateTable);
            }
            catch (Exception ex)
            {
                LoggerHelper.LogException(ex, "PaymentService.Constructor");
            }
        }

        // ---------------------- GET ALL ----------------------
        public List<Payment> GetAllPayments()
        {
            var list = new List<Payment>();

            try
            {
                var table = new DataTable();

                DbHelper.ExecuteQuery(PaymentQueries.GetAll,
                    null,
                    reader =>
                    {
                        table.Load(reader);
                    });

                foreach (DataRow row in table.Rows)
                {
                    list.Add(new Payment
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        BookingId = Convert.ToInt32(row["BookingId"]),
                        Amount = Convert.ToDecimal(row["Amount"]),
                        PaymentDate = row["PaymentDate"].ToString(),
                        PaymentMethod = row["PaymentMethod"].ToString(),
                        Status = row["Status"].ToString(),
                        Notes = row["Notes"]?.ToString() ?? string.Empty
                    });
                }
            }
            catch (Exception ex)
            {
                LoggerHelper.LogException(ex, "PaymentService.GetAllPayments");
            }

            return list;
        }

        // ---------------------- ADD ----------------------
        public bool AddPayment(Payment payment)
        {
            try
            {
                int rows = DbHelper.ExecuteNonQuery(PaymentQueries.Insert, cmd =>
                {
                    cmd.Parameters.AddWithValue("@BookingId", payment.BookingId);
                    cmd.Parameters.AddWithValue("@Amount", payment.Amount);
                    cmd.Parameters.AddWithValue("@PaymentDate", payment.PaymentDate);
                    cmd.Parameters.AddWithValue("@PaymentMethod", payment.PaymentMethod);
                    cmd.Parameters.AddWithValue("@Status", payment.Status);
                    cmd.Parameters.AddWithValue("@Notes", payment.Notes ?? string.Empty);
                });

                return rows > 0;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogException(ex, "PaymentService.AddPayment");
                return false;
            }
        }

        // ---------------------- UPDATE ----------------------
        public bool UpdatePayment(Payment payment)
        {
            try
            {
                int rows = DbHelper.ExecuteNonQuery(PaymentQueries.Update, cmd =>
                {
                    cmd.Parameters.AddWithValue("@BookingId", payment.BookingId);
                    cmd.Parameters.AddWithValue("@Amount", payment.Amount);
                    cmd.Parameters.AddWithValue("@PaymentDate", payment.PaymentDate);
                    cmd.Parameters.AddWithValue("@PaymentMethod", payment.PaymentMethod);
                    cmd.Parameters.AddWithValue("@Status", payment.Status);
                    cmd.Parameters.AddWithValue("@Notes", payment.Notes ?? string.Empty);
                    cmd.Parameters.AddWithValue("@Id", payment.Id);
                });

                return rows > 0;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogException(ex, "PaymentService.UpdatePayment");
                return false;
            }
        }

        // ---------------------- DELETE ----------------------
        public bool DeletePayment(int id)
        {
            try
            {
                return DbHelper.ExecuteNonQuery(PaymentQueries.Delete, cmd =>
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                }) > 0;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogException(ex, "PaymentService.DeletePayment");
                return false;
            }
        }
    }
}
