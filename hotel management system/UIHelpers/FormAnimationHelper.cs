
namespace hotel_management_system.UIHelpers
{
    /// <summary>
    /// Хелпер для анімації появи WinForms (fade-in effect).
    /// </summary>
    public static class FormAnimationHelper
    {
        /// <summary>
        /// Одноразова плавна поява форми через зміну Opacity від 0 до 1.
        /// Використовується всередині, але можна викликати й напряму.
        /// </summary>
        public static void ApplyFadeIn(Form form, int intervalMs = 15, double step = 0.05)
        {
            if (form == null)
                return;

            // Якщо форму вже закрили — нічого не робимо
            if (form.IsDisposed)
                return;

            // Починаємо з повної прозорості
            form.Opacity = 0;

            var timer = new System.Windows.Forms.Timer
            {
                Interval = intervalMs
            };

            timer.Tick += (s, e) =>
            {
                if (form.IsDisposed)
                {
                    timer.Stop();
                    timer.Dispose();
                    return;
                }

                if (form.Opacity >= 1)
                {
                    form.Opacity = 1;     // щоб не перелізти за 1.0
                    timer.Stop();
                    timer.Dispose();
                }
                else
                {
                    form.Opacity += step; // збільшуємо прозорість
                }
            };

            timer.Start();
        }

        /// <summary>
        /// Підписує форму так, щоб КОЖНОГО РАЗУ, коли вона стає Visible,
        /// виконувався плавний fade-in (у т.ч. після Hide()/Show()).
        /// Викликати один раз у конструкторі форми.
        /// </summary>
        public static void AttachFadeInOnVisible(Form form, int intervalMs = 15, double step = 0.05)
        {
            if (form == null)
                return;

            // На початку зробимо форму прозорою —
            // перший показ теж буде плавним
            form.Opacity = 0;

            form.VisibleChanged += (s, e) =>
            {
                if (form.IsDisposed)
                    return;

                // Нас цікавить момент, коли форма стала видимою
                if (!form.Visible)
                    return;

                ApplyFadeIn(form, intervalMs, step);
            };
        }
    }
}
