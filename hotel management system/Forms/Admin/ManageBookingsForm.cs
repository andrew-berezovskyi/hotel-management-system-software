using hotel_management_system.Helpers;
using HotelManagementSystem.Models;
using hotel_management_system.Services;
using hotel_management_system.UIHelpers;

namespace hotel_management_system.Forms
{
    public partial class ManageBookingsForm : Form
    {
        private readonly BookingService _bookingService = new BookingService();
        private readonly BindingSource _bs = new BindingSource();

        public ManageBookingsForm()
        {
            InitializeComponent();

            FormAnimationHelper.AttachFadeInOnVisible(this);

            // Базові параметри вікна
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;

            // Прив’язка подій
            btnBack.Click += (_, __) => NavigationHelper.ReturnToDashboard(this);
            btnRefresh.Click += (_, __) => LoadBookings();
            btnAdd.Click += (_, __) => AddBooking();
            btnEdit.Click += (_, __) => EditBooking();
            btnDelete.Click += (_, __) => DeleteBooking();

            SetupGrid();
            dgvBookings.CellFormatting += DgvBookings_CellFormatting;

            LoadBookings();
        }

        // ---------------- STATUS COLORING ----------------

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

                default:
                    row.DefaultCellStyle.ForeColor = activeFore;
                    break;
            }
        }

        // ---------------- EVENT BUS ----------------

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            EventBus.BookingCreated += OnBookingCreated;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            EventBus.BookingCreated -= OnBookingCreated;
            base.OnFormClosed(e);
        }

        private void OnBookingCreated(int _)
        {
            LoadBookings();
        }

        // ---------------- GRID ----------------

        private void SetupGrid()
        {
            dgvBookings.AutoGenerateColumns = false;
            dgvBookings.ReadOnly = true;
            dgvBookings.RowHeadersVisible = false;
            dgvBookings.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBookings.MultiSelect = false;
            dgvBookings.Columns.Clear();

            dgvBookings.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ID", DataPropertyName = "Id", Width = 60 });
            dgvBookings.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Guest", DataPropertyName = "UserName", Width = 160 });
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
            _bs.DataSource = _bookingService.GetAllBookings();
        }

        // ---------------- CRUD ----------------

        private void AddBooking()
        {
            var booking = BookingDialogHelper.ShowDialog(this, null);
            if (booking == null) return;

            if (_bookingService.AddBooking(booking))
                ToastHelper.ShowToast(this, "Booking added successfully.", ToastType.Success);
            else
                ToastHelper.ShowToast(this, "Failed to add booking.", ToastType.Error);

            LoadBookings();
        }

        private void EditBooking()
        {
            if (dgvBookings.CurrentRow?.DataBoundItem is not Booking selected) return;

            var updated = BookingDialogHelper.ShowDialog(this, selected);
            if (updated == null) return;

            if (_bookingService.UpdateBooking(updated))
                ToastHelper.ShowToast(this, "Booking updated successfully.", ToastType.Success);
            else
                ToastHelper.ShowToast(this, "Failed to update booking.", ToastType.Error);

            LoadBookings();
        }

        private void DeleteBooking()
        {
            if (dgvBookings.CurrentRow?.DataBoundItem is not Booking selected) return;

            if (MessageBox.Show(
                    $"Cancel booking #{selected.Id} for room {selected.RoomNumber}?",
                    "Confirm",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            if (_bookingService.CancelBookingByAdmin(selected))
                ToastHelper.ShowToast(this, "Booking cancelled.", ToastType.Success);
            else
                ToastHelper.ShowToast(this, "Failed to cancel booking.", ToastType.Error);

            LoadBookings();
        }
    }
}
