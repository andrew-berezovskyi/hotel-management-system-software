using System.Drawing.Drawing2D;


namespace HotelApp
{
    /// <summary>
    /// Напівпрозора "glass" панель для центральної картки WelcomeForm.
    /// Малює заповнений прямокутник з альфою поверх фону форми.
    /// </summary>
    public class GlassPanel : Panel
    {
        private Color _fillColor = Color.FromArgb(190, 15, 23, 42);
        //        A    R   G   B  (A=прозорість, 0..255)

        /// <summary>
        /// Колір заповнення панелі (з урахуванням альфи).
        /// </summary>
        public Color FillColor
        {
            get => _fillColor;
            set
            {
                _fillColor = value;
                Invalidate();
            }
        }

        public GlassPanel()
        {
            BackColor = Color.Transparent;
            DoubleBuffered = true;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // Не викликаємо base, щоб не малювати стандартний BackColor
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            using (var brush = new SolidBrush(_fillColor))
            {
                e.Graphics.FillRectangle(brush, ClientRectangle);
            }
        }
    }
}
