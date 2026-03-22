using System.Drawing;
using System.Windows.Forms;
using HotelManagementSystem.Models;
using hotel_management_system.Helpers;

namespace HotelManagementSystem.Helpers
{
    public static class StaffDialogHelper
    {
        public static Staff ShowDialog(Form parent, Staff existing = null)
        {
            var dlg = new Form
            {
                Text = existing == null ? "Add Staff" : "Edit Staff",
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                BackColor = Color.White,
                Size = new Size(460, 550),
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

            // --- Full Name ---
            var lblName = Label("Full Name:");
            var txtName = new TextBox { Left = 170, Top = y, Width = 240 };
            y += 40;

            // --- Position ---
            var lblPosition = Label("Position:");
            var txtPosition = new TextBox { Left = 170, Top = y, Width = 240 };
            y += 40;

            // --- Phone ---
            var lblPhone = Label("Phone:");
            var txtPhone = new TextBox { Left = 170, Top = y, Width = 240 };
            y += 40;

            // --- Email ---
            var lblEmail = Label("Email:");
            var txtEmail = new TextBox { Left = 170, Top = y, Width = 240 };
            y += 40;

            // --- Hire Date ---
            var lblHireDate = Label("Hire Date:");
            var dtHireDate = new DateTimePicker
            {
                Left = 170,
                Top = y,
                Width = 240,
                Format = DateTimePickerFormat.Short
            };
            y += 40;

            // --- Salary ---
            var lblSalary = Label("Salary (₴):");
            var txtSalary = new TextBox { Left = 170, Top = y, Width = 240 };
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
            cbStatus.Items.AddRange(new[] { "Active", "On Leave", "Terminated" });
            y += 50;

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

            dlg.Controls.AddRange(new Control[]
            {
                lblName, txtName,
                lblPosition, txtPosition,
                lblPhone, txtPhone,
                lblEmail, txtEmail,
                lblHireDate, dtHireDate,
                lblSalary, txtSalary,
                lblStatus, cbStatus,
                btnOk, btnCancel
            });

            // --- Prefill if editing ---
            if (existing != null)
            {
                txtName.Text = existing.FullName;
                txtPosition.Text = existing.Position;
                txtPhone.Text = existing.Phone;
                txtEmail.Text = existing.Email;
                dtHireDate.Value = DateTime.Parse(existing.HireDate);
                txtSalary.Text = existing.Salary.ToString("F0");
                cbStatus.Text = existing.Status;
            }

            dlg.AcceptButton = btnOk;
            dlg.CancelButton = btnCancel;

            var result = dlg.ShowDialog(parent);
            if (result != DialogResult.OK)
                return null;

            // --- Validation ---
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtPosition.Text))
            {
                ToastHelper.ShowToast(parent, "Full name and position are required!", ToastType.Warning);
                return null;
            }

            if (!decimal.TryParse(txtSalary.Text, out decimal salary) || salary < 0)
            {
                ToastHelper.ShowToast(parent, "Invalid salary value!", ToastType.Error);
                return null;
            }

            // --- Return Staff Object ---
            return new Staff
            {
                Id = existing?.Id ?? 0,
                FullName = txtName.Text.Trim(),
                Position = txtPosition.Text.Trim(),
                Phone = txtPhone.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                HireDate = dtHireDate.Value.ToString("yyyy-MM-dd"),
                Salary = salary,
                Status = cbStatus.Text
            };
        }
    }
}
