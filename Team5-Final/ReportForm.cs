using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Team5_Final.Logic;

namespace Team5_Final
{
    public partial class ReportForm : Form
    {
        private readonly InventoryService _svc = new InventoryService();

        public ReportForm()
        {
            InitializeComponent();
        }

        private void ReportForm_Load(object sender, EventArgs e)
        {
            var dt = _svc.ActiveCheckouts(); // existing method (all open checkouts)
            dgvAll.AutoGenerateColumns = true;
            dgvAll.DataSource = dt;
        }

        private void BtnExportCsv_Click(object sender, EventArgs e)
        {
            DataTable dt = null;

            if (dgvAll.DataSource is DataTable t)
                dt = t;
            else if (dgvAll.DataSource is BindingSource bs)
            {
                if (bs.DataSource is DataTable t2) dt = t2;
                else if (bs.List is DataView v) dt = v.ToTable();
            }
            else if (dgvAll.DataSource is DataView v2)
                dt = v2.ToTable();

            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("Nothing to export.");
                return;
            }

            using (var sfd = new SaveFileDialog { Filter = "CSV files|*.csv", FileName = "OpenCheckouts.csv" })
            {
                if (sfd.ShowDialog(this) != DialogResult.OK) return;

                var sb = new StringBuilder();

                // header
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (i > 0) sb.Append(",");
                    sb.Append("\"" + dt.Columns[i].ColumnName.Replace("\"", "\"\"") + "\"");
                }
                sb.AppendLine();

                // rows
                foreach (DataRow r in dt.Rows)
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        if (i > 0) sb.Append(",");
                        var val = r[i]?.ToString() ?? "";
                        sb.Append("\"" + val.Replace("\"", "\"\"") + "\"");
                    }
                    sb.AppendLine();
                }

                File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
                MessageBox.Show("Exported: " + sfd.FileName);
            }
        }


        private void ReportForm_Load_1(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'cEIS400Team5DBDataSet.EquipmentLogTable' table. You can move, or remove it, as needed.
            this.equipmentLogTableTableAdapter.Fill(this.cEIS400Team5DBDataSet.EquipmentLogTable);

        }
    }
}
