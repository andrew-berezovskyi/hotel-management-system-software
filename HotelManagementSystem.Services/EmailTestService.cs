using hotel_management_system.Helpers;

namespace hotel_management_system.Services
{
    public static class EmailTestService
    {
        public static async Task TestEmailAsync(System.Windows.Forms.Form form, string email)
        {
            if (!ValidationHelper.IsValidEmail(email))
            {
                ToastHelper.ShowToast(form, "Invalid email format!", ToastType.Error);
                return;
            }

            var service = new EmailService();
            bool result = await service.SendOTPAsync(email, "123456");

            ToastHelper.ShowToast(form,
                result ? "Test email sent successfully!" : "Failed to send test email!",
                result ? ToastType.Success : ToastType.Error);
        }
    }
}
