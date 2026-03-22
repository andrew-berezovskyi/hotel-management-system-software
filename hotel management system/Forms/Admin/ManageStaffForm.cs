using hotel_management_system.Helpers;
using hotel_management_system.Services;
using hotel_management_system.UIHelpers;
using HotelManagementSystem.Helpers;
using System.Data;


namespace hotel_management_system.Forms
{
    public partial class ManageStaffForm : Form
    {
        private readonly StaffService _staffService;

        public ManageStaffForm()
        {
            InitializeComponent();

            // 🔹 Плавна анімація появи форми
            FormAnimationHelper.AttachFadeInOnVisible(this);

            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;

            _staffService = new StaffService();

            LoadStaff();

            btnAdd.Click += BtnAdd_Click;
            btnEdit.Click += BtnEdit_Click;
            btnDelete.Click += BtnDelete_Click;
            btnRefresh.Click += BtnRefresh_Click;
            btnSearch.Click += BtnSearch_Click;
            btnClear.Click += BtnClear_Click;
            btnExport.Click += BtnExport_Click;
            btnBack.Click += BtnBack_Click;
        }

        // -------------------- LOAD --------------------
        private void LoadStaff(string term = null)
        {
            try
            {
                var list = _staffService.GetAllStaff(term);

                dgvStaff.DataSource = list.Select(s => new
                {
                    s.Id,
                    s.FullName,
                    s.Position,
                    s.Phone,
                    s.Email,
                    s.HireDate,
                    s.Salary,
                    s.Status
                }).ToList();

                dgvStaff.Columns["Id"].Visible = false;
            }
            catch (Exception ex)
            {
                ToastHelper.ShowToast(this, $"Failed to load staff: {ex.Message}", ToastType.Error);
            }
        }

        // -------------------- ADD --------------------
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            var staff = StaffDialogHelper.ShowDialog(this);
            if (staff == null) return;

            if (_staffService.AddStaff(staff))
            {
                ToastHelper.ShowToast(this, "Staff added successfully!", ToastType.Success);
                LoadStaff();
            }
            else
            {
                ToastHelper.ShowToast(this, "Failed to add staff!", ToastType.Error);
            }
        }

        // -------------------- EDIT --------------------
        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (dgvStaff.SelectedRows.Count == 0)
            {
                ToastHelper.ShowToast(this, "Select a staff member first.", ToastType.Warning);
                return;
            }

            int id = Convert.ToInt32(dgvStaff.SelectedRows[0].Cells["Id"].Value);
            var staff = _staffService.GetStaffById(id);
            if (staff == null)
            {
                ToastHelper.ShowToast(this, "Staff not found.", ToastType.Error);
                return;
            }

            var updated = StaffDialogHelper.ShowDialog(this, staff);
            if (updated == null) return;

            if (_staffService.UpdateStaff(updated))
            {
                ToastHelper.ShowToast(this, "Staff updated successfully!", ToastType.Success);
                LoadStaff();
            }
            else
            {
                ToastHelper.ShowToast(this, "Failed to update staff.", ToastType.Error);
            }
        }

        // -------------------- DELETE --------------------
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvStaff.SelectedRows.Count == 0)
            {
                ToastHelper.ShowToast(this, "Select a staff member first.", ToastType.Warning);
                return;
            }

            int id = Convert.ToInt32(dgvStaff.SelectedRows[0].Cells["Id"].Value);
            var confirm = MessageBox.Show("Are you sure you want to delete this staff member?",
                "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                if (_staffService.DeleteStaff(id))
                {
                    ToastHelper.ShowToast(this, "Staff deleted successfully.", ToastType.Success);
                    LoadStaff();
                }
                else
                {
                    ToastHelper.ShowToast(this, "Failed to delete staff.", ToastType.Error);
                }
            }
        }

        // -------------------- REFRESH --------------------
        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            LoadStaff();
            ToastHelper.ShowToast(this, "List refreshed.", ToastType.Info);
        }

        // -------------------- SEARCH --------------------
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string term = txtSearch.Text.Trim();
            LoadStaff(term);
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            LoadStaff();
        }

        // -------------------- EXPORT --------------------
        private void BtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvStaff.Rows.Count == 0)
                {
                    ToastHelper.ShowToast(this, "No data to export.", ToastType.Warning);
                    return;
                }

                var data = new List<Dictionary<string, object>>();
                foreach (DataGridViewRow row in dgvStaff.Rows)
                {
                    if (row.DataBoundItem == null) continue;

                    var dict = new Dictionary<string, object>();
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        dict[cell.OwningColumn.HeaderText] = cell.Value;
                    }
                    data.Add(dict);
                }

                ExcelExportHelper.ExportDataGridViewToCsv(this, dgvStaff, "Staff_List");
                ToastHelper.ShowToast(this, "Export completed successfully!", ToastType.Success);
            }
            catch (Exception ex)
            {
                ToastHelper.ShowToast(this, $"Export failed: {ex.Message}", ToastType.Error);
            }
        }

        // -------------------- BACK --------------------
        private void BtnBack_Click(object sender, EventArgs e)
        {
            NavigationHelper.ReturnToDashboard(this);
        }
    }
}
