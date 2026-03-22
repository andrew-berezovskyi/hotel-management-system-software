using hotel_management_system.Helpers;
using hotel_management_system.Services;

namespace HotelManagementSystem.Services
{
    public class ContactService
    {
        private readonly EmailService _emailService = new EmailService();

        public async Task<bool> SendUserMessageAsync(string fromName, string fromEmail, string subject, string message)
        {
            var hotelEmail = ConfigHelper.GetHotelEmail();
            if (string.IsNullOrWhiteSpace(hotelEmail))
                hotelEmail = ConfigHelper.GetEmail(); // fallback

            string fullSubject = $"[Contact] {subject}";
            string fullBody =
                $"Message from: {fromName} <{fromEmail}>\n\n" +
                "------------------------------------------\n\n" +
                message;

            return await _emailService.SendEmailAsync(hotelEmail, fullSubject, fullBody);
        }
    }
}
