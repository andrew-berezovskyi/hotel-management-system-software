using hotel_management_system.Helpers;
using hotel_management_system.Services;
using hotel_management_system.UIHelpers;
using HotelManagementSystem.Models;

namespace hotel_management_system.Forms
{
    public partial class ManageReportsForm : Form
    {
        private readonly ReportService _reportService = new ReportService();

        public ManageReportsForm()
        {
            InitializeComponent();

            // 🔹 Плавна анімація появи форми
            FormAnimationHelper.AttachFadeInOnVisible(this);

            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;

            btnBack.Click += (_, __) => NavigationHelper.ReturnToDashboard(this);
            btnGenerate.Click += BtnGenerate_Click;
            btnExport.Click += BtnExport_Click;

            // початкові дати
            dtStart.Value = DateTime.Now.AddDays(-30);
            dtEnd.Value = DateTime.Now;
        }

        private void BtnGenerate_Click(object sender, EventArgs e)
        {
            var summary = _reportService.GetReport(dtStart.Value, dtEnd.Value);
            LoadReport(summary);
            ToastHelper.ShowToast(this, "Report generated successfully!", ToastType.Success);
        }

        private void LoadReport(ReportSummary s)
        {
            dgvReport.Rows.Clear();
            dgvReport.Columns.Clear();

            dgvReport.Columns.Add("Metric", "Metric");
            dgvReport.Columns.Add("Value", "Value");

            var data = new Dictionary<string, string>
            {
                { "Total Income", $"₴{s.TotalIncome:F2}" },
                { "Total Bookings", s.TotalBookings.ToString() },
                { "Active Bookings", s.ActiveBookings.ToString() },
                { "Completed Bookings", s.CompletedBookings.ToString() },
                { "Cancelled Bookings", s.CancelledBookings.ToString() },
                { "Total Rooms", s.TotalRooms.ToString() },
                { "Occupied Rooms", s.OccupiedRooms.ToString() },
                { "Available Rooms", s.AvailableRooms.ToString() },
                { "Total Customers", s.TotalCustomers.ToString() },
                { "Total Staff", s.TotalStaff.ToString() }
            };

            foreach (var kv in data)
                dgvReport.Rows.Add(kv.Key, kv.Value);
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            if (dgvReport.Rows.Count == 0)
            {
                ToastHelper.ShowToast(this, "Nothing to export!", ToastType.Warning);
                return;
            }

            ExcelExportHelper.ExportDataGridViewToCsv(this, dgvReport, "Hotel_Report");
        }
    }
}
