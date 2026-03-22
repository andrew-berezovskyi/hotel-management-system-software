using HotelManagementSystem.Models;
using hotel_management_system.Services;
using hotel_management_system.Helpers;

namespace hotel_management_system.UIHelpers
{
    public static class BookingDialogHelper
    {
        public static Booking ShowDialog(Form parent, Booking existing = null)
        {
            var userService = new UserService();
            var roomService = new RoomService();

            // отримання списків користувачів і кімнат
            var users = userService.GetAllUsers().Select(u => new { u.Id, u.FullName }).ToList();
            var rooms = roomService.GetAllRooms().Select(r => new { r.Id, r.Number, r.Price }).ToList();

            // створення вікна
            var dlg = new Form
            {
                Text = existing == null ? "Add Booking" : "Edit Booking",
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                BackColor = Color.White,
                Size = new Size(480, 540),
                MinimizeBox = false,
                MaximizeBox = false
            };

            int y = 25;
            int labelWidth = 140;

            Label Label(string text) => new Label
            {
                Text = text,
                Left = 20,
                Top = y + 5,
                Width = labelWidth
            };

            // --- Поля ---
            var lblUser = Label("Guest:");
            var cbUser = new ComboBox { Left = 170, Top = y, Width = 260, DropDownStyle = ComboBoxStyle.DropDownList };
            cbUser.DataSource = users;
            cbUser.DisplayMember = "FullName";
            cbUser.ValueMember = "Id";
            y += 40;

            var lblRoom = Label("Room:");
            var cbRoom = new ComboBox { Left = 170, Top = y, Width = 260, DropDownStyle = ComboBoxStyle.DropDownList };
            cbRoom.DataSource = rooms;
            cbRoom.DisplayMember = "Number";
            cbRoom.ValueMember = "Id";
            y += 40;

            var lblCheckIn = Label("Check-In Date:");
            var dtCheckIn = new DateTimePicker { Left = 170, Top = y, Width = 260, Format = DateTimePickerFormat.Short };
            y += 40;

            var lblCheckOut = Label("Check-Out Date:");
            var dtCheckOut = new DateTimePicker { Left = 170, Top = y, Width = 260, Format = DateTimePickerFormat.Short };
            y += 40;

            var lblDays = Label("Days:");
            var txtDays = new TextBox { Left = 170, Top = y, Width = 260, ReadOnly = true };
            y += 40;

            var lblTotal = Label("Total Price (₴):");
            var txtTotal = new TextBox { Left = 170, Top = y, Width = 260, ReadOnly = true };
            y += 40;

            var lblPayment = Label("Payment Method:");
            var cbPayment = new ComboBox { Left = 170, Top = y, Width = 260, DropDownStyle = ComboBoxStyle.DropDownList };
            cbPayment.Items.AddRange(new[] { "Unpaid", "Cash", "Card", "Bank Transfer", "Online" });
            y += 40;

            var lblStatus = Label("Status:");
            var cbStatus = new ComboBox { Left = 170, Top = y, Width = 260, DropDownStyle = ComboBoxStyle.DropDownList };
            cbStatus.Items.AddRange(new[] { "Active", "Cancelled", "Completed" });
            y += 40;

            var lblNotes = Label("Notes:");
            var txtNotes = new TextBox { Left = 170, Top = y, Width = 260, Height = 60, Multiline = true };
            y += 80;

            var btnOk = new Button
            {
                Text = "OK",
                Left = 250,
                Top = y,
                Width = 80,
                DialogResult = DialogResult.OK
            };
            var btnCancel = new Button
            {
                Text = "Cancel",
                Left = 340,
                Top = y,
                Width = 80,
                DialogResult = DialogResult.Cancel
            };

            dlg.Controls.AddRange(new Control[]
            {
                lblUser, cbUser,
                lblRoom, cbRoom,
                lblCheckIn, dtCheckIn,
                lblCheckOut, dtCheckOut,
                lblDays, txtDays,
                lblTotal, txtTotal,
                lblPayment, cbPayment,
                lblStatus, cbStatus,
                lblNotes, txtNotes,
                btnOk, btnCancel
            });

            // --- Локальна функція оновлення ---
            void UpdatePrice()
            {
                if (cbRoom.SelectedItem == null) return;

                var room = (dynamic)cbRoom.SelectedItem;
                decimal pricePerDay = room.Price;

                var days = (dtCheckOut.Value.Date - dtCheckIn.Value.Date).Days;
                if (days < 1) days = 0;

                txtDays.Text = days.ToString();
                txtTotal.Text = days > 0 ? (pricePerDay * days).ToString("F0") : "0";
            }

            // події для оновлення при зміні дати або кімнати
            dtCheckIn.ValueChanged += (_, __) => UpdatePrice();
            dtCheckOut.ValueChanged += (_, __) => UpdatePrice();
            cbRoom.SelectedIndexChanged += (_, __) => UpdatePrice();

            // якщо редагування
            if (existing != null)
            {
                cbUser.SelectedValue = existing.UserId;
                cbRoom.SelectedValue = existing.RoomId;
                dtCheckIn.Value = DateTime.Parse(existing.CheckInDate);
                dtCheckOut.Value = DateTime.Parse(existing.CheckOutDate);
                txtNotes.Text = existing.Notes;
                cbPayment.Text = existing.PaymentMethod;
                cbStatus.Text = existing.Status;
                UpdatePrice();
            }
            else
            {
                UpdatePrice();
            }

            dlg.AcceptButton = btnOk;
            dlg.CancelButton = btnCancel;

            // показ
            var result = dlg.ShowDialog(parent);
            if (result != DialogResult.OK) return null;

            if (dtCheckOut.Value <= dtCheckIn.Value)
            {
                ToastHelper.ShowToast(parent, "Check-out must be after check-in!", ToastType.Warning);
                return null;
            }

            if (!decimal.TryParse(txtTotal.Text, out decimal total) || total <= 0)
            {
                ToastHelper.ShowToast(parent, "Invalid total price!", ToastType.Error);
                return null;
            }

            return new Booking
            {
                Id = existing?.Id ?? 0,
                UserId = Convert.ToInt32(cbUser.SelectedValue),
                RoomId = Convert.ToInt32(cbRoom.SelectedValue),
                CheckInDate = dtCheckIn.Value.ToString("yyyy-MM-dd"),
                CheckOutDate = dtCheckOut.Value.ToString("yyyy-MM-dd"),
                TotalPrice = total,
                PaymentMethod = cbPayment.Text,
                Status = cbStatus.Text,
                Notes = txtNotes.Text
            };
        }
    }
}
