using System.Drawing;
using System.Windows.Forms;
using HotelManagementSystem.Models;

namespace hotel_management_system.Helpers
{
    public static class UserDialogHelper
    {
        public static User ShowDialog(Form parent, User existing)
        {
            var dlg = new Form
            {
                Text = existing == null ? "Add User" : "Edit User",
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MinimizeBox = false,
                MaximizeBox = false,
                ClientSize = new Size(420, existing == null ? 360 : 310),
                BackColor = Color.White
            };

            int y = 20;

            var tbFullName = CreateTextBox("Full name:", ref y, dlg);
            var tbUsername = CreateTextBox("Username:", ref y, dlg);
            var tbEmail = CreateTextBox("Email:", ref y, dlg);
            var tbPhone = CreateTextBox("Phone:", ref y, dlg);
            var tbPassport = CreateTextBox("ID card:", ref y, dlg);

            TextBox tbPassword = null;
            if (existing == null)
                tbPassword = CreateTextBox("Password:", ref y, dlg, true);

            var cbIsAdmin = new CheckBox { Text = "Admin", Left = 130, Top = y, Width = 80 };
            dlg.Controls.Add(cbIsAdmin);
            y += 50;

            var btnOk = new Button { Text = "OK", Left = 210, Top = y, Width = 80, DialogResult = DialogResult.OK };
            var btnCancel = new Button { Text = "Cancel", Left = 300, Top = y, Width = 80, DialogResult = DialogResult.Cancel };
            dlg.Controls.AddRange(new Control[] { btnOk, btnCancel });

            // Prefill
            if (existing != null)
            {
                tbFullName.Text = existing.FullName;
                tbUsername.Text = existing.Username;
                tbEmail.Text = existing.Email;
                tbPhone.Text = existing.Phone;
                tbPassport.Text = existing.PassportNumber;
                cbIsAdmin.Checked = existing.IsAdmin;
            }

            dlg.AcceptButton = btnOk;
            dlg.CancelButton = btnCancel;

            if (dlg.ShowDialog(parent) != DialogResult.OK) return null;

            var model = existing ?? new User();
            model.FullName = tbFullName.Text.Trim();
            model.Username = tbUsername.Text.Trim();
            model.Email = tbEmail.Text.Trim();
            model.Phone = tbPhone.Text.Trim();
            model.PassportNumber = tbPassport.Text.Trim();
            model.IsAdmin = cbIsAdmin.Checked;

            if (existing == null)
            {
                var plain = tbPassword.Text.Trim();
                if (string.IsNullOrWhiteSpace(plain))
                {
                    MessageBox.Show("Password is required.", "Warning");
                    return null;
                }
                model.PasswordHash = PasswordHelper.HashPassword(plain);
            }

            return model;
        }

        private static TextBox CreateTextBox(string label, ref int y, Form dlg, bool isPassword = false)
        {
            var lbl = new Label { Text = label, Left = 20, Top = y + 5, Width = 100 };
            var tb = new TextBox { Left = 130, Top = y, Width = 250, UseSystemPasswordChar = isPassword };
            dlg.Controls.Add(lbl);
            dlg.Controls.Add(tb);
            y += 40;
            return tb;
        }
    }
}

