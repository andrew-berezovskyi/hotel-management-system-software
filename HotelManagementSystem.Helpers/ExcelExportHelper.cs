using System.Text;
using System.Windows.Forms;

namespace hotel_management_system.Helpers
{
    public static class ExcelExportHelper
    {
        /// <summary>
        /// Exports any DataGridView to a CSV file (readable by Excel).
        /// </summary>
        public static void ExportDataGridViewToCsv(Form parent, DataGridView grid, string defaultFileName)
        {
            if (grid == null || grid.Rows.Count == 0)
            {
                ToastHelper.ShowToast(parent, "No data to export.", ToastType.Warning);
                return;
            }

            using var dialog = new SaveFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv",
                FileName = $"{defaultFileName}_{DateTime.Now:yyyyMMdd_HHmm}.csv",
                Title = "Export to Excel/CSV"
            };

            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                var sb = new StringBuilder();

                // Header row
                var headers = grid.Columns.Cast<DataGridViewColumn>()
                    .Select(c => "\"" + c.HeaderText + "\"");
                sb.AppendLine(string.Join(",", headers));

                // Data rows
                foreach (DataGridViewRow row in grid.Rows)
                {
                    if (row.IsNewRow) continue;

                    var cells = row.Cells.Cast<DataGridViewCell>()
                        .Select(c => "\"" + (c.Value?.ToString()?.Replace("\"", "\"\"") ?? "") + "\"");
                    sb.AppendLine(string.Join(",", cells));
                }

                File.WriteAllText(dialog.FileName, sb.ToString(), Encoding.UTF8);
                ToastHelper.ShowToast(parent, "Export completed successfully!", ToastType.Success);
            }
            catch (Exception ex)
            {
                ToastHelper.ShowToast(parent, $"Export failed: {ex.Message}", ToastType.Error);
            }
        }

        /// <summary>
        /// Exports a generic list of objects to CSV using reflection.
        /// </summary>
        public static void ExportListToCsv<T>(Form parent, IEnumerable<T> list, string defaultFileName)
        {
            if (list == null || !list.Any())
            {
                ToastHelper.ShowToast(parent, "No data to export.", ToastType.Warning);
                return;
            }

            using var dialog = new SaveFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv",
                FileName = $"{defaultFileName}_{DateTime.Now:yyyyMMdd_HHmm}.csv",
                Title = "Export to Excel/CSV"
            };

            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                var props = typeof(T).GetProperties();
                var sb = new StringBuilder();

                // Header
                sb.AppendLine(string.Join(",", props.Select(p => "\"" + p.Name + "\"")));

                // Data
                foreach (var item in list)
                {
                    var values = props.Select(p =>
                    {
                        var val = p.GetValue(item)?.ToString()?.Replace("\"", "\"\"");
                        return "\"" + val + "\"";
                    });
                    sb.AppendLine(string.Join(",", values));
                }

                File.WriteAllText(dialog.FileName, sb.ToString(), Encoding.UTF8);
                ToastHelper.ShowToast(parent, "Export completed successfully!", ToastType.Success);
            }
            catch (Exception ex)
            {
                ToastHelper.ShowToast(parent, $"Export failed: {ex.Message}", ToastType.Error);
            }
        }
    }
}
