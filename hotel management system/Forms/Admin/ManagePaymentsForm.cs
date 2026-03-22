using hotel_management_system.Helpers;
using hotel_management_system.Services;
using hotel_management_system.UIHelpers;
using HotelManagementSystem.Models;


namespace hotel_management_system.Forms
{
    public partial class ManagePaymentsForm : Form
    {
        private readonly PaymentService _paymentService = new PaymentService();
        private readonly BindingSource _bs = new BindingSource();

        public ManagePaymentsForm()
        {
            InitializeComponent();

            // 🔹 Плавна анімація появи форми
            FormAnimationHelper.AttachFadeInOnVisible(this);

            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;

            // Event bindings
            btnBack.Click += (_, __) => NavigationHelper.ReturnToDashboard(this);
            btnAddPayment.Click += (_, __) => AddPayment();
            btnEditPayment.Click += (_, __) => EditPayment();
            btnDeletePayment.Click += (_, __) => DeletePayment();
            btnRefresh.Click += (_, __) => LoadPayments();
            btnExport.Click += (_, __) => ExportToExcel();

            SetupGrid();
            LoadPayments();
        }

        private void SetupGrid()
        {
            dgvPayments.AutoGenerateColumns = false;
            dgvPayments.ReadOnly = true;
            dgvPayments.RowHeadersVisible = false;
            dgvPayments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPayments.MultiSelect = false;

            // ---- Темна тема, як у ManageBookings ----
            dgvPayments.BorderStyle = BorderStyle.None;
            dgvPayments.BackgroundColor = Color.FromArgb(25, 30, 45);
            dgvPayments.GridColor = Color.FromArgb(55, 60, 80);

            dgvPayments.EnableHeadersVisualStyles = false;
            dgvPayments.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(35, 40, 60);
            dgvPayments.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvPayments.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(35, 40, 60);
            dgvPayments.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;

            dgvPayments.DefaultCellStyle.BackColor = Color.FromArgb(25, 30, 45);
            dgvPayments.DefaultCellStyle.ForeColor = Color.White;
            dgvPayments.DefaultCellStyle.SelectionBackColor = Color.FromArgb(60, 90, 180);
            dgvPayments.DefaultCellStyle.SelectionForeColor = Color.White;

            dgvPayments.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(30, 35, 55);

            dgvPayments.Columns.Clear();

            dgvPayments.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "ID",
                DataPropertyName = "Id",
                Width = 60
            });
            dgvPayments.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Booking ID",
                DataPropertyName = "BookingId",
                Width = 100
            });
            dgvPayments.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Amount (₴)",
                DataPropertyName = "Amount",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N2",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });
            dgvPayments.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Date",
                DataPropertyName = "PaymentDate",
                Width = 110
            });
            dgvPayments.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Method",
                DataPropertyName = "PaymentMethod",
                Width = 130
            });
            dgvPayments.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Status",
                DataPropertyName = "Status",
                Width = 110
            });
            dgvPayments.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Notes",
                DataPropertyName = "Notes",
                Width = 260
            });

            dgvPayments.DataSource = _bs;
        }

        private void LoadPayments()
        {
            _bs.DataSource = _paymentService.GetAllPayments();
        }

        private void AddPayment()
        {
            var payment = PaymentDialogHelper.ShowDialog(this, null);
            if (payment == null) return;

            if (_paymentService.AddPayment(payment))
                ToastHelper.ShowToast(this, "Payment added successfully.", ToastType.Success);
            else
                ToastHelper.ShowToast(this, "Failed to add payment.", ToastType.Error);

            LoadPayments();
        }

        private void EditPayment()
        {
            if (dgvPayments.CurrentRow?.DataBoundItem is not Payment selected) return;

            var updated = PaymentDialogHelper.ShowDialog(this, selected);
            if (updated == null) return;

            if (_paymentService.UpdatePayment(updated))
                ToastHelper.ShowToast(this, "Payment updated.", ToastType.Success);
            else
                ToastHelper.ShowToast(this, "Failed to update payment.", ToastType.Error);

            LoadPayments();
        }

        private void DeletePayment()
        {
            if (dgvPayments.CurrentRow?.DataBoundItem is not Payment selected) return;

            if (MessageBox.Show($"Delete payment #{selected.Id}?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            if (_paymentService.DeletePayment(selected.Id))
                ToastHelper.ShowToast(this, "Payment deleted.", ToastType.Success);
            else
                ToastHelper.ShowToast(this, "Failed to delete payment.", ToastType.Error);

            LoadPayments();
        }

        private void ExportToExcel()
        {
            ExcelExportHelper.ExportDataGridViewToCsv(this, dgvPayments, "Payments");
        }
    }
}
