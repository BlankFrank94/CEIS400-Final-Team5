using System;
using System.Data;
using System.Windows.Forms;
using Team5_Final.Data;
using Team5_Final.Logic;

namespace Team5_Final
{
    public partial class MainForm : Form
    {
        private readonly InventoryService _svc = new InventoryService();
        private readonly DataManager _data = new DataManager();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Load employees into combo
            var dt = _svc.Employees(); // columns: EmployeeID, FirstName, LastName, SkillLevel
            if (!dt.Columns.Contains("FullName"))
                dt.Columns.Add("FullName", typeof(string));

            foreach (DataRow r in dt.Rows)
                r["FullName"] = string.Format("{0} {1}", r["FirstName"], r["LastName"]);

            cmbEmployees.DisplayMember = "FullName";
            cmbEmployees.ValueMember = "EmployeeID";
            cmbEmployees.DataSource = dt;

            // remove any previous “dashboard” grids from this form
            // (per your new layout: login + report only)
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (cmbEmployees.SelectedValue == null)
            {
                MessageBox.Show("Please select an employee.");
                return;
            }

            string employeeId = cmbEmployees.SelectedValue.ToString();
            string fullName, role, msg;

            // Attempt login with selected employee and password
            if (!_data.TryLoginByEmployeeId(employeeId, txtPassword.Text, out fullName, out role, out msg))
            {
                MessageBox.Show(msg, "Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Clear();
                txtPassword.Focus();
                return;
            }

            // Store the logged-in user's info for use in other forms
            CurrentUser.Set(employeeId, fullName, role);

            // Navigate to the checkout/return page
            var f = new CheckoutReturnForm();
            f.StartPosition = FormStartPosition.CenterScreen;

            f.FormClosed += (s, args) =>
            {
                // when child closes, clear auth and show login again
                CurrentUser.Clear();
                this.Show();
                txtPassword.Clear();
                txtPassword.Focus();
            };

            this.Hide();
            f.Show();
        }

        // Generate Report Function, Need to add admin check
        private void btnReport_Click(object sender, EventArgs e)
        {
            var rf = new ReportForm();
            rf.StartPosition = FormStartPosition.CenterParent;
            rf.ShowDialog(this);
        }
    }
}
