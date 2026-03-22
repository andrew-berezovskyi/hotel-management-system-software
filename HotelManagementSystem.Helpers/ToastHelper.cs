using System.Drawing;
using System.Windows.Forms;

namespace hotel_management_system.Helpers
{
    public enum ToastType { Success, Error, Warning, Info }

    public static class ToastHelper
    {
        public static async void ShowToast(Form parent, string message, ToastType type = ToastType.Info, int duration = 2500)
        {
            if (parent == null || parent.IsDisposed) return;
            if (string.IsNullOrWhiteSpace(message)) return;

            // Колір фону залежно від типу
            Color backColor = type switch
            {
                ToastType.Success => Color.FromArgb(80, 200, 120),
                ToastType.Error => Color.FromArgb(200, 60, 60),
                ToastType.Warning => Color.FromArgb(255, 180, 50),
                _ => Color.FromArgb(70, 100, 180)
            };

            int width = Math.Min(400, message.Length * 9 + 40);
            int height = 40;

            var toast = new Label
            {
                AutoSize = false,
                Width = width,
                Height = height,
                Text = message,
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.White,
                BackColor = backColor,
                BorderStyle = BorderStyle.None,
                Padding = new Padding(10, 0, 10, 0)
            };

            // Центр внизу
            toast.Left = (parent.ClientSize.Width - toast.Width) / 2;
            toast.Top = parent.ClientSize.Height - toast.Height - 15;

            parent.Controls.Add(toast);
            toast.BringToFront();

            // Плавна поява
            toast.Visible = true;
            for (int i = 0; i <= 20; i++)
            {
                int alpha = (int)(i * 12.75);
                toast.BackColor = Color.FromArgb(alpha, toast.BackColor);
                await Task.Delay(10);
            }

            await Task.Delay(duration);

            // Плавне зникнення
            for (int i = 20; i >= 0; i--)
            {
                int alpha = (int)(i * 12.75);
                toast.BackColor = Color.FromArgb(alpha, toast.BackColor);
                await Task.Delay(10);
            }

            parent.Controls.Remove(toast);
            toast.Dispose();
        }
    }
}




