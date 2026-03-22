using hotel_management_system.Helpers;
using hotel_management_system.Services;
using hotel_management_system.UIHelpers;
using HotelManagementSystem.Models;


namespace hotel_management_system.Forms
{
    public partial class UserAccountForm : Form
    {
        private readonly BookingService _bookingService = new BookingService();
        private readonly BindingSource _bs = new BindingSource();

        public UserAccountForm()
        {
            InitializeComponent();

            FormAnimationHelper.AttachFadeInOnVisible(this);

            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;

            btnBack.Click += (_, __) => this.Close();
            btnRefresh.Click += (_, __) => LoadBookings();
            btnCancelBooking.Click += (_, __) => CancelSelectedBooking();

            LoadUserInfo();
            SetupGrid();
            dgvBookings.CellFormatting += DgvBookings_CellFormatting;
            LoadBookings();
        }

        private void LoadUserInfo()
        {
            var user = AuthHelper.CurrentUser;
            if (user == null)
            {
                lblUserInfo.Text = "Not logged in.";
                return;
            }

            lblUserInfo.Text = $"{user.FullName}  |  {user.Email}  |  {user.Phone}";
        }

        private void SetupGrid()
        {
            dgvBookings.AutoGenerateColumns = false;
            dgvBookings.ReadOnly = true;
            dgvBookings.RowHeadersVisible = false;
            dgvBookings.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBookings.MultiSelect = false;
            dgvBookings.Columns.Clear();

            dgvBookings.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ID", DataPropertyName = "Id", Width = 60 });
            dgvBookings.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Room", DataPropertyName = "RoomNumber", Width = 100 });
            dgvBookings.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Check-In", DataPropertyName = "CheckInDate", Width = 100 });
            dgvBookings.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Check-Out", DataPropertyName = "CheckOutDate", Width = 100 });
            dgvBookings.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Total ₴", DataPropertyName = "TotalPrice", Width = 80 });
            dgvBookings.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Payment", DataPropertyName = "PaymentMethod", Width = 100 });
            dgvBookings.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Status", DataPropertyName = "Status", Width = 100 });
            dgvBookings.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Notes", DataPropertyName = "Notes", Width = 220 });

            dgvBookings.DataSource = _bs;
        }

        private void LoadBookings()
        {
            var user = AuthHelper.CurrentUser;
            if (user == null)
            {
                ToastHelper.ShowToast(this, "Please log in again.", ToastType.Error);
                NavigationHelper.LogoutToLogin(this);
                return;
            }

            _bs.DataSource = _bookingService.GetUserBookings(user.Id);
        }

        private void CancelSelectedBooking()
        {
            if (dgvBookings.CurrentRow?.DataBoundItem is not Booking selected)
                return;

            if (selected.Status != "Active")
            {
                ToastHelper.ShowToast(this, "Only ACTIVE bookings can be cancelled.", ToastType.Warning);
                return;
            }

            var user = AuthHelper.CurrentUser;
            if (user == null)
            {
                ToastHelper.ShowToast(this, "Please log in again.", ToastType.Error);
                NavigationHelper.LogoutToLogin(this);
                return;
            }

            if (MessageBox.Show(
                    $"Cancel booking #{selected.Id} for room {selected.RoomNumber}?",
                    "Confirm",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            // важливо: у selected вже є UserId та RoomId
            if (_bookingService.CancelBookingByUser(selected))
            {
                ToastHelper.ShowToast(this, "Booking cancelled.", ToastType.Success);
                LoadBookings();
            }
            else
            {
                ToastHelper.ShowToast(this, "Failed to cancel booking.", ToastType.Error);
            }
        }

        private void DgvBookings_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvBookings.Rows[e.RowIndex];

            string statusText;

            // Якщо грід забінджений на List<Booking>
            if (row.DataBoundItem is Booking booking)
            {
                statusText = booking.Status;
            }
            else
            {
                // Якщо джерело — DataTable, читаємо зі стовпця "Status"
                DataGridViewCell statusCell = null;

                if (dgvBookings.Columns.Contains("Status"))
                {
                    statusCell = row.Cells["Status"];
                }
                else
                {
                    statusCell = row.Cells
                        .Cast<DataGridViewCell>()
                        .FirstOrDefault(c => c.OwningColumn.HeaderText == "Status");
                }

                statusText = statusCell?.Value?.ToString() ?? string.Empty;
            }

            ApplyStatusStyle(row, statusText);
        }

        private void ApplyStatusStyle(DataGridViewRow row, string status)
        {
            status = (status ?? string.Empty).Trim();

            var baseBack = Color.FromArgb(35, 40, 55);
            var activeFore = Color.White;
            var completedFore = Color.FromArgb(120, 220, 150);
            var cancelledFore = Color.FromArgb(170, 170, 170);

            row.DefaultCellStyle.BackColor = baseBack;

            switch (status)
            {
                case "Completed":
                    row.DefaultCellStyle.ForeColor = completedFore;
                    break;

                case "Cancelled":
                    row.DefaultCellStyle.ForeColor = cancelledFore;
                    break;

                default: // Active або будь-що інше
                    row.DefaultCellStyle.ForeColor = activeFore;
                    break;
            }
        }
    }
}
