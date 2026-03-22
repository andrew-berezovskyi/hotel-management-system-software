using HotelManagementSystem.Models;
using hotel_management_system.Services;
using hotel_management_system.Helpers;
using hotel_management_system.UIHelpers;


namespace hotel_management_system.Forms
{
    public partial class ManageUsersForm : Form
    {
        private readonly UserService _userService = new UserService();
        private readonly BindingSource _bs = new BindingSource();

        public ManageUsersForm()
        {
            InitializeComponent();

            FormAnimationHelper.AttachFadeInOnVisible(this);

            btnBack.Click += (_, __) => NavigationHelper.ReturnToDashboard(this);
            btnRefresh.Click += (_, __) => LoadUsers();
            btnSearch.Click += (_, __) => LoadUsers(txtSearch.Text);
            btnClear.Click += (_, __) =>
            {
                txtSearch.Text = "";
                LoadUsers();
            };
            btnAddUser.Click += (_, __) => AddUser();
            btnEditUser.Click += (_, __) => EditUser();
            btnDeleteUser.Click += (_, __) => DeleteUser();

            SetupGrid();
            LoadUsers();
        }

        private void SetupGrid()
        {
            dgvUsers.AutoGenerateColumns = false;
            dgvUsers.ReadOnly = true;
            dgvUsers.RowHeadersVisible = false;
            dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsers.MultiSelect = false;
            dgvUsers.Columns.Clear();

            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "ID",
                DataPropertyName = "Id",
                Width = 60
            });
            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Full Name",
                DataPropertyName = "FullName",
                Width = 180
            });
            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Username",
                DataPropertyName = "Username",
                Width = 140
            });
            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Email",
                DataPropertyName = "Email",
                Width = 200
            });
            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Phone",
                DataPropertyName = "Phone",
                Width = 120
            });
            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "ID Card",
                DataPropertyName = "PassportNumber",
                Width = 120
            });
            dgvUsers.Columns.Add(new DataGridViewCheckBoxColumn
            {
                HeaderText = "Admin",
                DataPropertyName = "IsAdmin",
                Width = 70
            });

            dgvUsers.DataSource = _bs;
        }

        private void LoadUsers(string term = null)
        {
            _bs.DataSource = _userService.GetAllUsers(term);
        }

        private void AddUser()
        {
            var user = UserDialogHelper.ShowDialog(this, null);
            if (user == null) return;

            if (_userService.AddUser(user))
                ToastHelper.ShowToast(this, "User added successfully.", ToastType.Success);
            else
                ToastHelper.ShowToast(this, "Failed to add user.", ToastType.Error);

            LoadUsers();
        }

        private void EditUser()
        {
            if (dgvUsers.CurrentRow?.DataBoundItem is not User selected)
            {
                ToastHelper.ShowToast(this, "Please select a user first.", ToastType.Info);
                return;
            }

            var updated = UserDialogHelper.ShowDialog(this, selected);
            if (updated == null) return;

            if (_userService.UpdateUser(updated))
                ToastHelper.ShowToast(this, "User updated successfully.", ToastType.Success);
            else
                ToastHelper.ShowToast(this, "Update failed.", ToastType.Error);

            LoadUsers();
        }

        private void DeleteUser()
        {
            if (dgvUsers.CurrentRow?.DataBoundItem is not User selected)
            {
                ToastHelper.ShowToast(this, "Please select a user first.", ToastType.Info);
                return;
            }

            var confirm = MessageBox.Show(
                $"Delete user '{selected.FullName}'?",
                "Confirm",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm != DialogResult.Yes)
                return;

            if (_userService.DeleteUser(selected.Id))
                ToastHelper.ShowToast(this, "User deleted successfully.", ToastType.Success);
            else
                ToastHelper.ShowToast(this, "Failed to delete user.", ToastType.Error);

            LoadUsers();
        }
    }
}
