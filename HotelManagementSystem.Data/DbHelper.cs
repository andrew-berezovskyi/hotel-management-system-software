// DbHelper — універсальний клас для безпечного доступу до SQLite,
// який інкапсулює відкриття підключення, виконання SQL-запитів,
// обробку параметрів та логування помилок.
/*
 Архітектура доступу до бази даних у проєкті:

 Form
   ↓
 Service (бізнес-логіка, перевірки, правила)
   ↓
 DbHelper (відкриває підключення, формує SQL-запит)
   ↓
 SQLite (виконує запит до бази даних)
   ↓
 DbHelper (отримує результат виконання запиту)
   ↓
 Service (перетворює дані у моделі)
   ↓
 Model (User, Room, Booking, Payment тощо)
   ↓
 Form (відображення даних у UI)

 DbHelper є єдиною точкою взаємодії з SQLite.
 Форми напряму не працюють з базою даних,
 що забезпечує розділення відповідальностей
 та чисту архітектуру застосунку.
*/

using System.Data.SQLite;
using hotel_management_system.Helpers;
using HotelManagementSystem.Persistence;

namespace HotelManagementSystem.Data
{
    public static class DbHelper
    {
        private static string ConnectionString => $"Data Source={DatabaseInitializer.DbPath};Version=3;";

        // ---------------- EXECUTE NON QUERY ----------------
        public static int ExecuteNonQuery(string sql, Action<SQLiteCommand> paramAction = null)
        {
            try
            {
                using var connection = new SQLiteConnection(ConnectionString);
                connection.Open();

                using var cmd = new SQLiteCommand(sql, connection);
                paramAction?.Invoke(cmd);
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                LoggerHelper.LogException(ex, "ExecuteNonQuery");
                return -1;
            }
        }

        // ---------------- EXECUTE SCALAR ----------------
        public static object ExecuteScalar(string sql, Action<SQLiteCommand> paramAction = null)
        {
            try
            {
                using var connection = new SQLiteConnection(ConnectionString);
                connection.Open();

                using var cmd = new SQLiteCommand(sql, connection);
                paramAction?.Invoke(cmd);
                return cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                LoggerHelper.LogException(ex, "ExecuteScalar");
                return null;
            }
        }

        // ---------------- EXECUTE QUERY ----------------
        public static void ExecuteQuery(string sql, Action<SQLiteCommand> paramAction, Action<SQLiteDataReader> readerAction)
        {
            try
            {
                using var connection = new SQLiteConnection(ConnectionString);
                connection.Open();

                using var cmd = new SQLiteCommand(sql, connection);
                paramAction?.Invoke(cmd);

                using var reader = cmd.ExecuteReader();
                readerAction?.Invoke(reader);
            }
            catch (Exception ex)
            {
                LoggerHelper.LogException(ex, "ExecuteQuery");
            }
        }
    }
}
