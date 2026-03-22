using HotelManagementSystem.Models;
using hotel_management_system.Services;
using hotel_management_system.Helpers;

namespace hotel_management_system.UIHelpers
{
    public static class PaymentDialogHelper
    {
        public static Payment ShowDialog(Form parent, Payment existing = null)
        {
            var bookingService = new BookingService();

            // get all bookings for dropdown
            var bookings = bookingService.GetAllBookings()
                .Select(b => new { b.Id, Display = $"#{b.Id} | Room {b.RoomId}" })
                .ToList();

            var dlg = new Form
            {
                Text = existing == null ? "Add Payment" : "Edit Payment",
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                BackColor = Color.White,
                Size = new Size(460, 500),
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

            // --- Booking ---
            var lblBooking = Label("Booking:");
            var cbBooking = new ComboBox
            {
                Left = 170,
                Top = y,
                Width = 240,
                DropDownStyle = ComboBoxStyle.DropDownList,
                DataSource = bookings,
                DisplayMember = "Display",
                ValueMember = "Id"
            };
            y += 40;

            // --- Amount ---
            var lblAmount = Label("Amount (₴):");
            var txtAmount = new TextBox { Left = 170, Top = y, Width = 240 };
            y += 40;

            // --- Date ---
            var lblDate = Label("Payment Date:");
            var dtPaymentDate = new DateTimePicker
            {
                Left = 170,
                Top = y,
                Width = 240,
                Format = DateTimePickerFormat.Short
            };
            y += 40;

            // --- Method ---
            var lblMethod = Label("Method:");
            var cbMethod = new ComboBox
            {
                Left = 170,
                Top = y,
                Width = 240,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cbMethod.Items.AddRange(new[]
            {
                "Cash",
                "Card",
                "Bank Transfer",
                "Online"
            });
            y += 40;

            // --- Status ---
            var lblStatus = Label("Status:");
            var cbStatus = new ComboBox
            {
                Left = 170,
                Top = y,
                Width = 240,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cbStatus.Items.AddRange(new[] { "Pending", "Completed", "Cancelled" });
            y += 40;

            // --- Notes ---
            var lblNotes = Label("Notes:");
            var txtNotes = new TextBox
            {
                Left = 170,
                Top = y,
                Width = 240,
                Height = 60,
                Multiline = true
            };
            y += 80;

            // --- Buttons ---
            var btnOk = new Button
            {
                Text = "OK",
                Left = 240,
                Top = y,
                Width = 80,
                DialogResult = DialogResult.OK
            };
            var btnCancel = new Button
            {
                Text = "Cancel",
                Left = 330,
                Top = y,
                Width = 80,
                DialogResult = DialogResult.Cancel
            };

            // --- ToolTips for payment methods ---
            var toolTip = new ToolTip();
            toolTip.SetToolTip(cbMethod,
                "Cash — paid in cash at the reception\n" +
                "Card — paid via POS terminal or online card payment\n" +
                "Bank Transfer — transfer between bank accounts\n" +
                "Online — online payment via website or app");

            dlg.Controls.AddRange(new Control[]
            {
                lblBooking, cbBooking,
                lblAmount, txtAmount,
                lblDate, dtPaymentDate,
                lblMethod, cbMethod,
                lblStatus, cbStatus,
                lblNotes, txtNotes,
                btnOk, btnCancel
            });

            // --- populate existing values if editing ---
            if (existing != null)
            {
                cbBooking.SelectedValue = existing.BookingId;
                txtAmount.Text = existing.Amount.ToString("F2");
                dtPaymentDate.Value = DateTime.Parse(existing.PaymentDate);
                cbMethod.Text = existing.PaymentMethod;
                cbStatus.Text = existing.Status;
                txtNotes.Text = existing.Notes;
            }

            dlg.AcceptButton = btnOk;
            dlg.CancelButton = btnCancel;

            var result = dlg.ShowDialog(parent);
            if (result != DialogResult.OK)
                return null;

            // --- validation ---
            if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
            {
                ToastHelper.ShowToast(parent, "Invalid amount entered!", ToastType.Error);
                return null;
            }

            return new Payment
            {
                Id = existing?.Id ?? 0,
                BookingId = Convert.ToInt32(cbBooking.SelectedValue),
                Amount = amount,
                PaymentDate = dtPaymentDate.Value.ToString("yyyy-MM-dd"),
                PaymentMethod = cbMethod.Text,
                Status = cbStatus.Text,
                Notes = txtNotes.Text
            };
        }
    }
}
