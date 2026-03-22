using hotel_management_system.Helpers;
using hotel_management_system.Forms;
using HotelManagementSystem.Forms;

namespace hotel_management_system.UIHelpers
{
    public static class NavigationHelper
    {
        // ---------------- MAIN SWITCH ----------------
        public static void SwitchToMain(Form current, Form nextMain)
        {
            if (current == null || current.IsDisposed || nextMain == null) return;

            var existing = Application.OpenForms.Cast<Form>()
                .FirstOrDefault(f => f.GetType() == nextMain.GetType());

            if (existing != null)
            {
                existing.WindowState = FormWindowState.Normal;
                existing.BringToFront();
                existing.Activate();
            }
            else
            {
                // головну форму НЕ прив’язуємо до Exit тут — нехай живе далі
                nextMain.Show();
            }

            if (!current.IsDisposed) current.Close();
        }

        // ------------- OPEN CHILD (повернення без алертів) -------------
        public static void OpenChild(Form parent, Form child)
        {
            if (parent == null || parent.IsDisposed || child == null) return;

            parent.Hide();

            child.FormClosed += (_, __) =>
            {
                if (!parent.IsDisposed)
                {
                    parent.WindowState = FormWindowState.Normal;
                    parent.Show();
                    parent.BringToFront();
                }
            };

            child.Show();
        }

        // ---------------- LOGOUT ----------------
        public static void LogoutToLogin(Form current)
        {
            if (current == null || current.IsDisposed) return;

            AuthHelper.Logout(); // очищаємо поточну сесію

            var login = Application.OpenForms.OfType<LoginForm>().FirstOrDefault();
            if (login != null)
            {
                login.WindowState = FormWindowState.Normal;
                login.BringToFront();
                login.Activate();
            }
            else
            {
                new LoginForm().Show();
            }

            if (!current.IsDisposed) current.Close();
        }

        // --------- OPTIONAL: вихід при закритті конкретної форми ---------
        public static void AttachExitOnClose(Form form)
        {
            if (form == null) return;
            form.FormClosing += (_, e) =>
            {
                // Якщо це останнє вікно – завершуємо застосунок
                if (Application.OpenForms.Count <= 1)
                    Application.Exit();
            };
        }

        // ------------- BACK / RETURN TO DASHBOARD (без попапів) -------------
        public static void ReturnToDashboard(Form current)
        {
            if (current == null || current.IsDisposed) return;

            var user = AuthHelper.CurrentUser;

            // Якщо сесії немає — тихо повертаємо на Login
            if (user == null)
            {
                var login = Application.OpenForms.OfType<LoginForm>().FirstOrDefault();
                if (login != null)
                {
                    login.WindowState = FormWindowState.Normal;
                    login.BringToFront();
                    login.Activate();
                }
                else
                {
                    new LoginForm().Show();
                }

                if (!current.IsDisposed) current.Close();
                return;
            }

            // Є сесія — відкриваємо правильний дашборд без алерта
            if (user.Role?.Equals("Admin", StringComparison.OrdinalIgnoreCase) == true)
                OpenOrActivate<AdminDashboardForm>();
            else
                OpenOrActivate<UserDashboardForm>();

            if (!current.IsDisposed) current.Close();
        }

        // ---------------- PRIVATE ----------------
        private static void OpenOrActivate<T>() where T : Form, new()
        {
            var existing = Application.OpenForms.OfType<T>().FirstOrDefault();
            if (existing != null)
            {
                existing.WindowState = FormWindowState.Normal;
                existing.BringToFront();
                existing.Activate();
            }
            else
            {
                new T().Show();
            }
        }
    }
}
