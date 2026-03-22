
namespace HotelManagementSystem.Models
{
    public class Staff
    {
        public int Id { get; set; }
        public string FullName { get; set; }       // працівник
        public string Position { get; set; }       // посада
        public string Phone { get; set; }          // телефон
        public string Email { get; set; }          // email
        public string HireDate { get; set; }       // дата найму
        public decimal Salary { get; set; }        // зарплата
        public string Status { get; set; }         // Active / On Leave / Terminated
    }
}
