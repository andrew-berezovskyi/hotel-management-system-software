using hotel_management_system.Helpers;
using HotelManagementSystem.Models;

namespace hotel_management_system.UIHelpers
{
    public static class RoomDialogHelper
    {
        public static Room ShowDialog(Form parent, Room existing = null)
        {
            var dlg = new Form
            {
                Text = existing == null ? "Add Room" : "Edit Room",
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MinimizeBox = false,
                MaximizeBox = false,
                BackColor = Color.White,
                ClientSize = new Size(420, 320)
            };

            int y = 20;

            Label L(string text) => new Label
            {
                Text = text,
                Left = 20,
                Top = y + 5,
                Width = 110
            };

            // Number
            var lblNumber = L("Room number:");
            var txtNumber = new TextBox { Left = 140, Top = y, Width = 240 };
            dlg.Controls.Add(lblNumber);
            dlg.Controls.Add(txtNumber);
            y += 40;

            // Type (ComboBox)
            var lblType = L("Type:");
            var cbType = new ComboBox
            {
                Left = 140,
                Top = y,
                Width = 240,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cbType.Items.AddRange(RoomOptions.RoomTypes);
            dlg.Controls.Add(lblType);
            dlg.Controls.Add(cbType);
            y += 40;

            // Price
            var lblPrice = L("Price (₴/night):");
            var txtPrice = new TextBox { Left = 140, Top = y, Width = 240 };
            dlg.Controls.Add(lblPrice);
            dlg.Controls.Add(txtPrice);
            y += 40;

            // Status (ComboBox)
            var lblStatus = L("Status:");
            var cbStatus = new ComboBox
            {
                Left = 140,
                Top = y,
                Width = 240,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cbStatus.Items.AddRange(RoomOptions.RoomStatuses);
            dlg.Controls.Add(lblStatus);
            dlg.Controls.Add(cbStatus);
            y += 40;

            // Image path
            var lblImage = L("Image path:");
            var txtImage = new TextBox { Left = 140, Top = y, Width = 200 };
            var btnBrowse = new Button
            {
                Text = "...",
                Left = 345,
                Top = y - 1,
                Width = 35
            };
            btnBrowse.Click += (_, __) =>
            {
                using var ofd = new OpenFileDialog
                {
                    Filter = "Images|*.png;*.jpg;*.jpeg;*.bmp;*.gif",
                    Title = "Select room image"
                };
                if (ofd.ShowDialog(parent) == DialogResult.OK)
                    txtImage.Text = ofd.FileName;
            };
            dlg.Controls.Add(lblImage);
            dlg.Controls.Add(txtImage);
            dlg.Controls.Add(btnBrowse);
            y += 50;

            var btnOk = new Button
            {
                Text = "OK",
                Left = 220,
                Top = y,
                Width = 80,
                DialogResult = DialogResult.OK
            };
            var btnCancel = new Button
            {
                Text = "Cancel",
                Left = 310,
                Top = y,
                Width = 80,
                DialogResult = DialogResult.Cancel
            };
            dlg.Controls.Add(btnOk);
            dlg.Controls.Add(btnCancel);

            // Prefill for edit
            if (existing != null)
            {
                txtNumber.Text = existing.Number;
                cbType.SelectedItem = existing.Type;
                txtPrice.Text = existing.Price.ToString("F0");
                cbStatus.SelectedItem = existing.Status;
                txtImage.Text = existing.ImagePath;
            }
            else
            {
                cbType.SelectedIndex = 0;
                cbStatus.SelectedIndex = 0;
            }

            dlg.AcceptButton = btnOk;
            dlg.CancelButton = btnCancel;

            if (dlg.ShowDialog(parent) != DialogResult.OK)
                return null;

            // Validation
            if (string.IsNullOrWhiteSpace(txtNumber.Text))
            {
                ToastHelper.ShowToast(parent, "Room number is required!", ToastType.Warning);
                return null;
            }

            if (!decimal.TryParse(txtPrice.Text.Replace(",", "."), out var price) || price <= 0)
            {
                ToastHelper.ShowToast(parent, "Invalid price!", ToastType.Warning);
                return null;
            }

            if (cbType.SelectedItem == null || cbStatus.SelectedItem == null)
            {
                ToastHelper.ShowToast(parent, "Please select type and status.", ToastType.Warning);
                return null;
            }

            var room = existing ?? new Room();
            room.Number = txtNumber.Text.Trim();
            room.Type = cbType.SelectedItem.ToString();
            room.Price = price;
            room.Status = cbStatus.SelectedItem.ToString();
            room.ImagePath = string.IsNullOrWhiteSpace(txtImage.Text)
                ? string.Empty
                : txtImage.Text.Trim();

            return room;
        }
    }
}
