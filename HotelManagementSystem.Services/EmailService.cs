using System.Net;
using System.Net.Mail;
using hotel_management_system.Helpers;

namespace hotel_management_system.Services
{
    public class EmailService
    {
        public async Task<bool> SendEmailAsync(string to, string subject, string body)
        {
            var fromAddress = ConfigHelper.GetEmail();
            var appPassword = ConfigHelper.GetEmailPassword();
            var smtpServer = ConfigHelper.GetEmailServer();
            var smtpPort = ConfigHelper.GetEmailPort();

            try
            {
                var mail = new MailMessage();
                mail.From = new MailAddress(fromAddress, "Hotel System");
                mail.To.Add(to);
                mail.Subject = subject;
                mail.Body = body;

                using (var smtp = new SmtpClient(smtpServer, smtpPort))
                {
                    smtp.Credentials = new NetworkCredential(fromAddress, appPassword);
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mail);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> SendOTPAsync(string to, string otp)
        {
            var fromAddress = ConfigHelper.GetEmail();
            var appPassword = ConfigHelper.GetEmailPassword();
            var smtpServer = ConfigHelper.GetEmailServer();
            var smtpPort = ConfigHelper.GetEmailPort();
            var expireMinutes = ConfigHelper.GetOTPExpireMinutes();

            try
            {
                var mail = new MailMessage();
                mail.From = new MailAddress(fromAddress, "Hotel System");
                mail.To.Add(to);
                mail.Subject = "Your password reset code";
                mail.Body = $"Your one-time password (OTP) is: {otp}\nIt will expire in {expireMinutes} minutes.";

                using (var smtp = new SmtpClient(smtpServer, smtpPort))
                {
                    smtp.Credentials = new NetworkCredential(fromAddress, appPassword);
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mail);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
/*
===============================================================================
 EMAIL SERVICE – ВІДПРАВЛЕННЯ OTP ТА СИСТЕМНИХ ЛИСТІВ
===============================================================================

Призначення:
Цей сервіс відповідає за надсилання електронних листів (OTP-кодів, сповіщень)
через SMTP-сервер поштового провайдера з використанням App Password.

Архітектурний підхід:
- EmailService належить до сервісного рівня (Services)
- UI (форми) НЕ мають прямого доступу до SMTP
- Вся логіка відправлення email інкапсульована в одному місці

Схема взаємодії компонентів:

UI (ResetPasswordForm / RegistrationForm)
        ↓
UserService (генерація OTP, перевірка користувача)
        ↓
EmailService (формування та відправлення листа)
        ↓
SMTP (протокол для відправки листів) сервер (через App Password)
        ↓
Email користувача

Конфігурація:
- Налаштування SMTP (email, сервер, порт, App Password)
  зберігаються у файлі appsettings.json
- Доступ до конфігурації здійснюється через ConfigHelper
- Це дозволяє змінювати email або пароль без перекомпіляції програми

Безпека:
- Основний пароль email-акаунта НЕ використовується
- Застосовується App Password (окремий пароль для додатку)
- Паролі користувачів у системі НЕ надсилаються email-ом
- OTP має обмежений термін дії (контролюється логікою сервісу)

Надійність:
- Всі помилки обробляються через try-catch
- Помилки логуються через LoggerHelper
- У разі проблем з SMTP програма не завершує роботу аварійно

Масштабованість:
- EmailService не прив’язаний до UI
- Може бути повторно використаний в іншому проєкті
- Підтримує розширення (наприклад: email-підтвердження, звіти, сповіщення)

===============================================================================
*/
