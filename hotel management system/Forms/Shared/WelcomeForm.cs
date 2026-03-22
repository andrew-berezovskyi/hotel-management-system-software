using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using hotel_management_system.Forms;      // LoginForm
using hotel_management_system.UIHelpers;  // FormAnimationHelper
using hotel_management_system.Helpers;    // ConfigHelper

namespace HotelApp
{
    public partial class WelcomeForm : Form
    {
        // Шлях до фонового зображення
        private readonly string _bgImagePath;

        public WelcomeForm()
        {
            InitializeComponent();

            // Папка Resources поруч з exe + файл welcome_bg_blur.png
            _bgImagePath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Resources",
                "welcome_bg_blur.png"   // назва твого файлу
            );

            LoadBackgroundImageSafely();

            // Форма на весь екран
            this.WindowState = FormWindowState.Maximized;
            this.DoubleBuffered = true;

            // Центруємо карту + футер при старті й при зміні розміру
            CenterCardPanel();
            this.Resize += (_, __) => CenterCardPanel();

            SetupLoginButtonHover();

            // Обробник для кліка по Website
            lnkWebsite.LinkClicked += LnkWebsite_LinkClicked;

            // 🔹 Універсальна плавна анімація появи форми
            FormAnimationHelper.AttachFadeInOnVisible(this);
        }

        /// <summary>
        /// Відкриває сайт готелю у браузері.
        /// URL береться з ConfigHelper, без хардкодів.
        /// </summary>
        private void LnkWebsite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var url = ConfigHelper.GetHotelWebsite();

            if (string.IsNullOrWhiteSpace(url))
            {
                MessageBox.Show(
                    "Website address is not configured.",
                    "Website",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            // Якщо в конфізі раптом немає http/https – акуратно додамо https
            if (!url.StartsWith("http://", StringComparison.OrdinalIgnoreCase) &&
                !url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                url = "https://" + url.Trim();
            }

            try
            {
                Process.Start(new ProcessStartInfo(url)
                {
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Cannot open the website.\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Безпечно завантажує фонове зображення, не блокуючи файл.
        /// Якщо файла немає або сталася помилка – просто лишаємо BackColor.
        /// </summary>
        private void LoadBackgroundImageSafely()
        {
            try
            {
                if (File.Exists(_bgImagePath))
                {
                    using (var img = Image.FromFile(_bgImagePath))
                    {
                        this.BackgroundImage = (Image)img.Clone();
                    }

                    this.BackgroundImageLayout = ImageLayout.Stretch;
                }
                else
                {
                    this.BackgroundImage = null;
                }
            }
            catch
            {
                this.BackgroundImage = null;
            }
        }

        /// <summary>
        /// Центрує панель-карту (pnlCard),
        /// футер (lblFooter) і лінк Website (lnkWebsite)
        /// відносно поточного розміру вікна,
        /// а також вирівнює кнопку Log in та текст під нею по центру картки.
        /// </summary>
        private void CenterCardPanel()
        {
            if (pnlCard == null || lblFooter == null || lnkWebsite == null)
                return;

            int formWidth = this.ClientSize.Width;
            int formHeight = this.ClientSize.Height;

            // Центр по горизонталі
            int x = (formWidth - pnlCard.Width) / 2;
            if (x < 20) x = 20;

            // Центр по вертикалі з невеликим зсувом вгору
            int y = (formHeight - pnlCard.Height) / 2;
            if (y < 40) y = 40;

            pnlCard.Location = new Point(x, y);

            // Футер по центру внизу
            lblFooter.Width = formWidth;
            lblFooter.Location = new Point(0, formHeight - lblFooter.Height - 10);

            // Лінк Website по центру над футером
            lnkWebsite.AutoSize = true; // щоб ширина відповідала тексту
            int linkX = (formWidth - lnkWebsite.Width) / 2;
            int linkY = lblFooter.Top - lnkWebsite.Height - 4; // трохи вище футера

            if (linkY < pnlCard.Bottom + 10)
                linkY = pnlCard.Bottom + 10; // щоб не залізти на картку, якщо екран низький

            lnkWebsite.Location = new Point(linkX, linkY);

            // Центруємо кнопку та текст всередині картки
            CenterLoginAreaInsideCard();
        }

        /// <summary>
        /// Вирівнює кнопку Log in та текст "Continue to..." по центру pnlCard.
        /// </summary>
        private void CenterLoginAreaInsideCard()
        {
            if (btnLogin == null || lblSubtitle == null || pnlCard == null)
                return;

            int cardWidth = pnlCard.ClientSize.Width;
            int cardHeight = pnlCard.ClientSize.Height;

            // Кнопка по центру по горизонталі,
            // знизу залишаємо трохи місця під текст
            int btnX = (cardWidth - btnLogin.Width) / 2;
            int btnY = cardHeight - btnLogin.Height - 80; // ~80 px від нижнього краю картки

            if (btnX < 20) btnX = 20;
            if (btnY < 0) btnY = 0;

            btnLogin.Location = new Point(btnX, btnY);

            // Текст під кнопкою, по центру картки
            lblSubtitle.Width = cardWidth;
            int subtitleY = btnLogin.Bottom + 8; // невеликий відступ вниз

            // щоб текст не виліз за нижній край картки
            if (subtitleY > cardHeight - lblSubtitle.Height - 20)
                subtitleY = cardHeight - lblSubtitle.Height - 20;

            lblSubtitle.Location = new Point(0, subtitleY);
        }

        /// <summary>
        /// Hover-ефект для HopeButton Log in.
        /// </summary>
        private void SetupLoginButtonHover()
        {
            var normalColor = btnLogin.PrimaryColor;
            var hoverColor = Color.FromArgb(
                Math.Min(normalColor.R + 20, 255),
                Math.Min(normalColor.G + 20, 255),
                Math.Min(normalColor.B + 20, 255)
            );

            btnLogin.Cursor = Cursors.Hand;
            btnLogin.MouseEnter += (s, e) => btnLogin.PrimaryColor = hoverColor;
            btnLogin.MouseLeave += (s, e) => btnLogin.PrimaryColor = normalColor;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var loginForm = new LoginForm();
            loginForm.Show();
            this.Hide();
        }
    }
}
