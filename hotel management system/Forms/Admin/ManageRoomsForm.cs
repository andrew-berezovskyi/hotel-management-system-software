using hotel_management_system.Helpers;
using hotel_management_system.Services;
using hotel_management_system.UIHelpers;
using HotelManagementSystem.Models;

namespace hotel_management_system.Forms
{
    public partial class ManageRoomsForm : Form
    {
        private readonly RoomService _roomService = new RoomService();
        private List<Room> _rooms = new();

        public ManageRoomsForm()
        {
            InitializeComponent();

            // 🔹 Плавна анімація появи форми
            FormAnimationHelper.AttachFadeInOnVisible(this);

            // Загальні параметри форми
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;

            // Прив’язка подій до кнопок
            btnBack.Click += btnBack_Click;
            btnAddRoom.Click += btnAdd_Click;
            btnEditRoom.Click += btnEdit_Click;
            btnDeleteRoom.Click += btnDeleteRoom_Click;
            btnRefresh.Click += (_, __) => LoadRooms();
            btnSearch.Click += (_, __) => ApplySearch();
            btnClear.Click += (_, __) =>
            {
                txtSearch.Text = string.Empty;
                LoadRooms();
            };

            // Натискання Enter у полі пошуку
            txtSearch.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    ApplySearch();
                    e.SuppressKeyPress = true;
                }
            };

            LoadRooms();
        }

        // ---------------------- ЛОГІКА ----------------------

        /// <summary>
        /// Завантаження списку всіх кімнат із сервісу.
        /// </summary>
        private void LoadRooms()
        {
            dgvRooms.Rows.Clear();
            _rooms = _roomService.GetAllRooms();

            foreach (var room in _rooms)
            {
                // Для адміна показуємо ті ж картинки, що й у юзерській панелі
                Image img = RoomPresentationHelper.GetImageForRoom(room)
                              ?? Resources.Resources.no_image;

                dgvRooms.Rows.Add(room.Id, room.Number, room.Type, room.Price, room.Status, img);
            }
        }

        /// <summary>
        /// Фільтрує список кімнат за текстом пошуку.
        /// </summary>
        private void ApplySearch()
        {
            string query = txtSearch.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(query))
            {
                LoadRooms();
                return;
            }

            var filtered = _rooms.Where(r =>
                    (r.Number ?? "").ToLower().Contains(query) ||
                    (r.Type ?? "").ToLower().Contains(query) ||
                    (r.Status ?? "").ToLower().Contains(query)
                )
                .ToList();

            dgvRooms.Rows.Clear();
            foreach (var room in filtered)
            {
                Image img = RoomPresentationHelper.GetImageForRoom(room)
                              ?? Resources.Resources.no_image;

                dgvRooms.Rows.Add(room.Id, room.Number, room.Type, room.Price, room.Status, img);
            }
        }

        /// <summary>
        /// Додає нову кімнату.
        /// </summary>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            var room = RoomDialogHelper.ShowDialog(this, null);
            if (room == null) return;

            if (_roomService.AddRoom(room))
                LoadRooms();
        }

        /// <summary>
        /// Редагує обрану кімнату.
        /// </summary>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvRooms.CurrentRow == null) return;

            // Беремо ID з гріда і шукаємо кімнату в списку _rooms
            int id = Convert.ToInt32(dgvRooms.CurrentRow.Cells["Id"].Value);
            var room = _rooms.FirstOrDefault(r => r.Id == id);
            if (room == null) return;

            var updated = RoomDialogHelper.ShowDialog(this, room);
            if (updated == null) return;

            updated.Id = room.Id;
            if (_roomService.UpdateRoom(updated))
                LoadRooms();
        }

        /// <summary>
        /// Видаляє вибрану кімнату.
        /// </summary>
        private void btnDeleteRoom_Click(object sender, EventArgs e)
        {
            if (dgvRooms.CurrentRow == null)
            {
                ToastHelper.ShowToast(this, "Select a room first.", ToastType.Info);
                return;
            }

            int id = Convert.ToInt32(dgvRooms.CurrentRow.Cells["Id"].Value);

            if (MessageBox.Show("Are you sure you want to delete this room?",
                "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bool deleted = _roomService.DeleteRoom(id);
                ToastHelper.ShowToast(this,
                    deleted ? "Room deleted." : "Failed to delete room.",
                    deleted ? ToastType.Success : ToastType.Error);

                LoadRooms();
            }
        }

        /// <summary>
        /// Повернення до адмінської панелі.
        /// </summary>
        private void btnBack_Click(object sender, EventArgs e)
        {
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm is AdminDashboardForm dash)
                {
                    dash.WindowState = FormWindowState.Normal;
                    dash.BringToFront();
                    dash.Activate();
                    this.Close();
                    return;
                }
            }

            new AdminDashboardForm().Show();
            this.Close();
        }
    }
}
