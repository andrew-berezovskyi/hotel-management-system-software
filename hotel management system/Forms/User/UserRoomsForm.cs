using hotel_management_system.Helpers;
using hotel_management_system.Services;
using hotel_management_system.UIHelpers;
using HotelManagementSystem.Forms;
using HotelManagementSystem.Models;
using ReaLTaiizor.Controls;


namespace hotel_management_system.Forms
{
    public partial class UserRoomsForm : Form
    {
        private readonly RoomService _roomService = new RoomService();

        public UserRoomsForm()
        {
            InitializeComponent(); 

            FormAnimationHelper.AttachFadeInOnVisible(this);

            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;

            WireEvents();
            LoadRooms();

            EventBus.Publish("RoomsChanged");
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            EventBus.Publish("RoomsChanged");
            base.OnFormClosed(e);
        }

        private void WireEvents()
        {
            // повернення на UserDashboard — тільки Close()
            btnBack.Click += (_, __) =>
            {
                var parent = Application.OpenForms.OfType<UserDashboardForm>().FirstOrDefault();
                if (parent != null)
                {
                    if (!parent.Visible) parent.Show();
                    parent.WindowState = FormWindowState.Normal;
                    parent.BringToFront();
                }
                this.Close();
            };

            btnFilter.Click += (_, __) => LoadRooms();
            btnClear.Click += (_, __) =>
            {
                txtSearch.Text = "";
                cbType.SelectedIndex = 0;
                cbStatus.SelectedIndex = 0;
                LoadRooms();
            };
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            var parent = Application.OpenForms.OfType<UserDashboardForm>().FirstOrDefault();
            if (parent != null)
            {
                if (!parent.Visible) parent.Show();
                parent.WindowState = FormWindowState.Normal;
                parent.BringToFront();
            }

            this.Close();
        }

        /// <summary>
        /// Асинхронне завантаження кімнат з тостом та без блокування UI.
        /// </summary>
        private async void LoadRooms()
        {
            try
            {
                ToastHelper.ShowToast(this, "Loading rooms...", ToastType.Info);

                string term = txtSearch.Text?.Trim().ToLower();
                string typeFilter = cbType.SelectedItem?.ToString();
                string statusFilter = cbStatus.SelectedItem?.ToString();

                // Виносимо запит до БД + LINQ-фільтрацію в бекграунд
                var rooms = await Task.Run(() =>
                {
                    var list = _roomService.GetAllRooms();

                    if (!string.IsNullOrWhiteSpace(term))
                    {
                        list = list.Where(r =>
                                (r.Number ?? "").ToLower().Contains(term) ||
                                (r.Type ?? "").ToLower().Contains(term) ||
                                r.Price.ToString().Contains(term)
                            )
                            .ToList();
                    }

                    if (typeFilter != null && typeFilter != "All types")
                    {
                        list = list
                            .Where(r => string.Equals(r.Type, typeFilter, StringComparison.OrdinalIgnoreCase))
                            .ToList();
                    }

                    if (statusFilter != null && statusFilter != "Any status")
                    {
                        list = list
                            .Where(r => string.Equals(r.Status, statusFilter, StringComparison.OrdinalIgnoreCase))
                            .ToList();
                    }

                    return list;
                });

                // Оновлюємо UI вже в головному потоці
                flowRooms.SuspendLayout();
                flowRooms.Controls.Clear();

                foreach (var room in rooms)
                    flowRooms.Controls.Add(CreateRoomCard(room));

                flowRooms.ResumeLayout();

                ToastHelper.ShowToast(this, $"Loaded {rooms.Count} rooms.", ToastType.Success);
            }
            catch (Exception ex)
            {
                LoggerHelper.LogException(ex, "UserRoomsForm.LoadRooms");
                ToastHelper.ShowToast(this, "Failed to load rooms.", ToastType.Error);
            }
        }

        private Control CreateRoomCard(Room room)
        {
            // контейнер картки
            var card = new System.Windows.Forms.Panel
            {
                Width = 300,
                Height = 300,
                BackColor = Color.FromArgb(35, 40, 55),
                Margin = new Padding(15),
                Padding = new Padding(12)
            };

            // фото
            var pic = new PictureBox
            {
                Width = 276,
                Height = 150,
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.FromArgb(45, 50, 70)
            };

            var img = RoomPresentationHelper.GetImageForRoom(room);
            if (img != null)
            {
                pic.Image = img;
            }
            else if (!string.IsNullOrWhiteSpace(room.ImagePath))
            {
                try { pic.ImageLocation = room.ImagePath; } catch { /* ignore */ }
            }

            card.Controls.Add(pic);

            // назва/номер
            var lblNumber = new Label
            {
                Text = $"Room {room.Number}",
                Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(0, 160),
                AutoSize = true
            };
            card.Controls.Add(lblNumber);

            // тип
            var lblType = new Label
            {
                Text = room.Type,
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                ForeColor = Color.Gainsboro,
                Location = new Point(0, 186),
                AutoSize = true
            };
            card.Controls.Add(lblType);

            // ціна
            var lblPrice = new Label
            {
                Text = $"₴ {room.Price:F0} / night",
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                ForeColor = Color.Gainsboro,
                Location = new Point(0, 208),
                AutoSize = true
            };
            card.Controls.Add(lblPrice);

            // статус
            var statusPill = new Label
            {
                AutoSize = false,
                Width = 110,
                Height = 24,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(166, 206),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.White,
                Text = room.Status
            };
            switch ((room.Status ?? "").ToLower())
            {
                case "available":
                    statusPill.BackColor = Color.FromArgb(60, 200, 120);
                    break;
                case "booked":
                case "occupied":
                    statusPill.BackColor = Color.FromArgb(200, 120, 60);
                    break;
                case "maintenance":
                    statusPill.BackColor = Color.FromArgb(160, 160, 160);
                    break;
                default:
                    statusPill.BackColor = Color.FromArgb(100, 100, 120);
                    break;
            }
            card.Controls.Add(statusPill);

            // кнопка бронювання
            var btnBook = new HopeButton
            {
                Text = "Book",
                Size = new Size(120, 36),
                Location = new Point(78, 250),
                PrimaryColor = Color.FromArgb(80, 120, 255),
                Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold)
            };
            btnBook.Click += (_, __) => HandleBook(room);
            card.Controls.Add(btnBook);

            return card;
        }

        private void HandleBook(Room room)
        {
            using (var dlg = new UserBookingForm(room))
            {
                var result = dlg.ShowDialog(this);
                if (result == DialogResult.OK)
                    LoadRooms();
            }
        }
    }
}
