using hotel_management_system.Helpers;
using HotelManagementSystem.Data;
using HotelManagementSystem.Data.Queries;
using HotelManagementSystem.Models;
using HotelManagementSystem.Persistence;
using System.Data.SQLite;

namespace hotel_management_system.Services
{
    public class StaffService
    {
        public StaffService()
        {
            try
            {
                string dbPath = DatabaseInitializer.DbPath;
                if (string.IsNullOrEmpty(dbPath))
                {
                    DatabaseInitializer.Initialize();
                }

                using var connection = new SQLiteConnection($"Data Source={DatabaseInitializer.DbPath};Version=3;");
                connection.Open();

                using var cmd = new SQLiteCommand(StaffQueries.CreateTable, connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                LoggerHelper.LogException(ex, "EnsureTableExists (StaffService)");
            }
        }

        // ---------------------- GET ALL STAFF ----------------------
        public List<Staff> GetAllStaff(string search = null)
        {
            var list = new List<Staff>();

            try
            {
                string query = string.IsNullOrWhiteSpace(search)
                    ? StaffQueries.GetAll
                    : StaffQueries.GetAllFiltered;

                DbHelper.ExecuteQuery(query, cmd =>
                {
                    if (!string.IsNullOrWhiteSpace(search))
                        cmd.Parameters.AddWithValue("@q", $"%{search.Trim()}%");
                },
                reader =>
                {
                    while (reader.Read())
                    {
                        list.Add(new Staff
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            FullName = reader["FullName"].ToString(),
                            Position = reader["Position"].ToString(),
                            Phone = reader["Phone"]?.ToString(),
                            Email = reader["Email"]?.ToString(),
                            HireDate = reader["HireDate"].ToString(),
                            Salary = Convert.ToDecimal(reader["Salary"]),
                            Status = reader["Status"].ToString()
                        });
                    }
                });
            }
            catch (Exception ex)
            {
                LoggerHelper.LogException(ex, "GetAllStaff");
            }

            return list;
        }

        // ---------------------- ADD STAFF ----------------------
        public bool AddStaff(Staff staff)
        {
            try
            {
                return DbHelper.ExecuteNonQuery(StaffQueries.Insert, cmd =>
                {
                    cmd.Parameters.AddWithValue("@FullName", staff.FullName);
                    cmd.Parameters.AddWithValue("@Position", staff.Position);
                    cmd.Parameters.AddWithValue("@Phone", staff.Phone ?? "");
                    cmd.Parameters.AddWithValue("@Email", staff.Email ?? "");
                    cmd.Parameters.AddWithValue("@HireDate", staff.HireDate);
                    cmd.Parameters.AddWithValue("@Salary", staff.Salary);
                    cmd.Parameters.AddWithValue("@Status", staff.Status ?? "Active");
                }) > 0;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogException(ex, "AddStaff");
                return false;
            }
        }

        // ---------------------- UPDATE STAFF ----------------------
        public bool UpdateStaff(Staff staff)
        {
            try
            {
                return DbHelper.ExecuteNonQuery(StaffQueries.Update, cmd =>
                {
                    cmd.Parameters.AddWithValue("@FullName", staff.FullName);
                    cmd.Parameters.AddWithValue("@Position", staff.Position);
                    cmd.Parameters.AddWithValue("@Phone", staff.Phone ?? "");
                    cmd.Parameters.AddWithValue("@Email", staff.Email ?? "");
                    cmd.Parameters.AddWithValue("@HireDate", staff.HireDate);
                    cmd.Parameters.AddWithValue("@Salary", staff.Salary);
                    cmd.Parameters.AddWithValue("@Status", staff.Status);
                    cmd.Parameters.AddWithValue("@Id", staff.Id);
                }) > 0;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogException(ex, "UpdateStaff");
                return false;
            }
        }

        // ---------------------- DELETE STAFF ----------------------
        public bool DeleteStaff(int id)
        {
            try
            {
                return DbHelper.ExecuteNonQuery(StaffQueries.Delete, cmd =>
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                }) > 0;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogException(ex, "DeleteStaff");
                return false;
            }
        }

        // ---------------------- GET STAFF BY ID ----------------------
        public Staff GetStaffById(int id)
        {
            Staff staff = null;

            try
            {
                DbHelper.ExecuteQuery(StaffQueries.GetById, cmd =>
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                },
                reader =>
                {
                    if (reader.Read())
                    {
                        staff = new Staff
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            FullName = reader["FullName"].ToString(),
                            Position = reader["Position"].ToString(),
                            Phone = reader["Phone"]?.ToString(),
                            Email = reader["Email"]?.ToString(),
                            HireDate = reader["HireDate"].ToString(),
                            Salary = Convert.ToDecimal(reader["Salary"]),
                            Status = reader["Status"].ToString()
                        };
                    }
                });
            }
            catch (Exception ex)
            {
                LoggerHelper.LogException(ex, "GetStaffById");
            }

            return staff;
        }
    }
}
