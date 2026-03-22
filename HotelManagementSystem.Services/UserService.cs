using System.Data;
using System.Text.RegularExpressions;
using hotel_management_system.Helpers;
using HotelManagementSystem.Models;
using HotelManagementSystem.Data;
using HotelManagementSystem.Data.SqlQueries;

namespace hotel_management_system.Services
{
    public class UserService
    {
        public UserService()
        {
            try
            {
                DbHelper.ExecuteNonQuery(UserQueries.CreateTable);
            }
            catch (Exception ex)
            {
                LoggerHelper.LogException(ex, "UserService.Constructor");
            }
        }

        // ---------------------- EXISTENCE CHECK ----------------------
        public bool UserExists(string username, string email, string passport, string phone)
        {
            try
            {
                object result = DbHelper.ExecuteScalar(UserQueries.Exists, cmd =>
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Passport", passport);
                    cmd.Parameters.AddWithValue("@Phone", phone);
                });

                return Convert.ToInt32(result) > 0;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogException(ex, "UserService.UserExists");
                return false;
            }
        }

        // ---------------------- ADD USER ----------------------
        public bool AddUser(User user)
        {
            try
            {
                int rows = DbHelper.ExecuteNonQuery(UserQueries.Insert, cmd =>
                {
                    cmd.Parameters.AddWithValue("@FullName", user.FullName);
                    cmd.Parameters.AddWithValue("@Username", user.Username);
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                    cmd.Parameters.AddWithValue("@PassportNumber", user.PassportNumber);
                    cmd.Parameters.AddWithValue("@Phone", user.Phone);
                    cmd.Parameters.AddWithValue("@IsAdmin", user.IsAdmin ? 1 : 0);
                });
                return rows > 0;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogException(ex, "UserService.AddUser");
                return false;
            }
        }

        // ---------------------- VALIDATE LOGIN ----------------------
        public User ValidateLogin(string input, string password)
        {
            try
            {
                string query;
                if (Regex.IsMatch(input, @"^\d{10}$"))
                    query = UserQueries.LoginByPhone;
                else if (input.Contains("@"))
                    query = UserQueries.LoginByEmail;
                else
                    query = UserQueries.LoginByUsername;

                User user = null;

                DbHelper.ExecuteQuery(query,
                    cmd => cmd.Parameters.AddWithValue("@input", input),
                    reader =>
                    {
                        if (reader.Read())
                        {
                            string storedHash = reader["PasswordHash"].ToString();
                            if (PasswordHelper.VerifyPassword(password, storedHash))
                            {
                                user = new User
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    FullName = reader["FullName"].ToString(),
                                    Username = reader["Username"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    PassportNumber = reader["PassportNumber"].ToString(),
                                    Phone = reader["Phone"].ToString(),
                                    PasswordHash = storedHash,
                                    IsAdmin = Convert.ToInt32(reader["IsAdmin"]) == 1
                                };
                            }
                        }
                    });

                return user;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogException(ex, "UserService.ValidateLogin");
                return null;
            }
        }

        // ---------------------- RESET CODE ----------------------
        public string GenerateResetCode(string email)
        {
            try
            {
                object exists = DbHelper.ExecuteScalar(UserQueries.ExistsByEmail, cmd =>
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                });

                if (Convert.ToInt32(exists) == 0)
                    return null;

                string resetCode = Guid.NewGuid().ToString("N").Substring(0, 6).ToUpper();

                DbHelper.ExecuteNonQuery(UserQueries.UpdateResetCode, cmd =>
                {
                    cmd.Parameters.AddWithValue("@Code", resetCode);
                    cmd.Parameters.AddWithValue("@Email", email);
                });

                return resetCode;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogException(ex, "UserService.GenerateResetCode");
                return null;
            }
        }

        public bool VerifyResetCode(string email, string resetCode)
        {
            try
            {
                object count = DbHelper.ExecuteScalar(UserQueries.VerifyResetCode, cmd =>
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Code", resetCode);
                });

                return Convert.ToInt32(count) > 0;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogException(ex, "UserService.VerifyResetCode");
                return false;
            }
        }

        public bool ResetPassword(string email, string newPassword)
        {
            try
            {
                int rows = DbHelper.ExecuteNonQuery(UserQueries.ResetPassword, cmd =>
                {
                    cmd.Parameters.AddWithValue("@Hash", PasswordHelper.HashPassword(newPassword));
                    cmd.Parameters.AddWithValue("@Email", email);
                });
                return rows > 0;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogException(ex, "UserService.ResetPassword");
                return false;
            }
        }

        // ---------------------- MANAGE USERS ----------------------
        public List<User> GetAllUsers(string term = null)
        {
            var list = new List<User>();
            string sql = UserQueries.GetAll;

            if (!string.IsNullOrWhiteSpace(term))
                sql += UserQueries.SearchFilter;

            try
            {
                var dt = new DataTable();

                DbHelper.ExecuteQuery(sql,
                    cmd =>
                    {
                        if (!string.IsNullOrWhiteSpace(term))
                            cmd.Parameters.AddWithValue("@q", $"%{term.Trim()}%");
                    },
                    reader => dt.Load(reader));

                foreach (DataRow row in dt.Rows)
                {
                    list.Add(new User
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        FullName = row["FullName"].ToString(),
                        Username = row["Username"].ToString(),
                        Email = row["Email"].ToString(),
                        PasswordHash = row["PasswordHash"].ToString(),
                        PassportNumber = row["PassportNumber"].ToString(),
                        Phone = row["Phone"].ToString(),
                        IsAdmin = Convert.ToInt32(row["IsAdmin"]) == 1
                    });
                }
            }
            catch (Exception ex)
            {
                LoggerHelper.LogException(ex, "UserService.GetAllUsers");
            }

            return list;
        }

        public bool UpdateUser(User user)
        {
            try
            {
                int rows = DbHelper.ExecuteNonQuery(UserQueries.Update, cmd =>
                {
                    cmd.Parameters.AddWithValue("@FullName", user.FullName);
                    cmd.Parameters.AddWithValue("@Username", user.Username);
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@PassportNumber", user.PassportNumber);
                    cmd.Parameters.AddWithValue("@Phone", user.Phone);
                    cmd.Parameters.AddWithValue("@IsAdmin", user.IsAdmin ? 1 : 0);
                    cmd.Parameters.AddWithValue("@Id", user.Id);
                });
                return rows > 0;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogException(ex, "UserService.UpdateUser");
                return false;
            }
        }

        public bool DeleteUser(int id)
        {
            try
            {
                return DbHelper.ExecuteNonQuery(UserQueries.Delete, cmd =>
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                }) > 0;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogException(ex, "UserService.DeleteUser");
                return false;
            }
        }
    }
}
