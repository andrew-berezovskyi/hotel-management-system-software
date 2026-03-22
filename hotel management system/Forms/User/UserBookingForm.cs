using hotel_management_system.Helpers;
using hotel_management_system.Services;
using hotel_management_system.UIHelpers;
using HotelManagementSystem.Models;


namespace hotel_management_system.Forms
{
    public partial class UserBookingForm : Form
    {
        private readonly Room _room;
        private readonly BookingService _bookingService = new BookingService();

        public UserBookingForm(Room room)
        {
            _room = room ?? throw new ArgumentNullException(nameof(room));

            InitializeComponent();

            // 🔹 Плавна анімація появи форми
            FormAnimationHelper.AttachFadeInOnVisible(this);

            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;

            BuildRuntimeDefaults();
            WireEvents();
        }

        // ---------- Init ----------
        private void BuildRuntimeDefaults()
        {
            lblTitle.Text = "Booking";
            lblRoom.Text = $"Room {_room.Number}  •  {_room.Type}";
            lblPrice.Text = $"Price: ₴{_room.Price:F0} / night";

            // Картинка через helper (ресурси)
            var img = RoomPresentationHelper.GetImageForRoom(_room);
            if (img != null)
            {
                picRoom.Image = img;
            }
            else if (!string.IsNullOrWhiteSpace(_room.ImagePath))
            {
                try { picRoom.ImageLocation = _room.ImagePath; } catch { /* ignore */ }
            }

            dtIn.Value = DateTime.Today.AddDays(1);
            dtOut.Value = DateTime.Today.AddDays(2);

            cbPayment.Items.Clear();
            cbPayment.Items.AddRange(new object[] { "Online", "Card", "Cash", "Unpaid" });
            cbPayment.SelectedIndex = 0;

            // Опис + правий блок
            lblDescriptionTitle.Text = "About this room";
            lblDescription.Text = RoomPresentationHelper.GetDescription(_room.Type);

            lblHighlightsTitle.Text = "Room highlights";
            lblHighlights.Text = RoomPresentationHelper.GetHighlights(_room.Type);

            lblServicesTitle.Text = "Included services";
            lblServices.Text = RoomPresentationHelper.GetIncludedServices();

            Recalc();
        }

        private void WireEvents()
        {
            btnBack.Click += (_, __) => this.Close();
            btnConfirm.Click += (_, __) => SaveBooking();

            dtIn.ValueChanged += (_, __) => Recalc();
            dtOut.ValueChanged += (_, __) => Recalc();
        }

        // ---------- View helpers ----------
        private void Recalc()
        {
            if (dtOut.Value.Date <= dtIn.Value.Date)
                dtOut.Value = dtIn.Value.Date.AddDays(1);

            var nights = (dtOut.Value.Date - dtIn.Value.Date).Days;
            lblNights.Text = $"Nights: {nights}";
            var total = nights * _room.Price;
            lblTotal.Text = $"Total: ₴{total:F0}";
        }

        // ---------- Save ----------
        private void SaveBooking()
        {
            var user = AuthHelper.CurrentUser;
            if (user == null)
            {
                MessageBox.Show("Please log in to complete the booking.", "Login required",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                NavigationHelper.LogoutToLogin(this);
                return;
            }

            var checkIn = dtIn.Value.Date;
            var checkOut = dtOut.Value.Date;
            var nights = (checkOut - checkIn).Days;

            if (nights < 1)
            {
                ToastHelper.ShowToast(this, "Check-out must be after check-in.", ToastType.Warning);
                return;
            }

            bool isFree = _bookingService.IsRoomAvailable(_room.Id, checkIn, checkOut);
            if (!isFree)
            {
                ToastHelper.ShowToast(this,
                    "This room is already booked for the selected dates.",
                    ToastType.Warning);
                return;
            }

            var booking = new Booking
            {
                UserId = user.Id,
                RoomId = _room.Id,
                CheckInDate = checkIn.ToString("yyyy-MM-dd"),
                CheckOutDate = checkOut.ToString("yyyy-MM-dd"),
                TotalPrice = nights * _room.Price,
                Status = "Active",
                PaymentMethod = cbPayment.SelectedItem?.ToString() ?? "Unpaid",
                Notes = txtNotes.Text?.Trim() ?? string.Empty
            };

            var ok = _bookingService.AddBooking(booking);
            if (!ok)
            {
                ToastHelper.ShowToast(this, "Failed to create booking.", ToastType.Error);
                return;
            }

            EventBus.RaiseRoomsChanged();
            EventBus.RaiseBookingCreated(booking.Id);

            ToastHelper.ShowToast(this, "Booking confirmed!", ToastType.Success);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
