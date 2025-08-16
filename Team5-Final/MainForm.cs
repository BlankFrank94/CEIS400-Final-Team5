using System;
using System.Data;
using System.Windows.Forms;
using Team5_Final.Data;
using Team5_Final.Logic;

namespace Team5_Final
{
    public partial class MainForm : Form
    {
        private readonly InventoryService _svc = new InventoryService(); // Handles inventory-related database operations
        private readonly DataManager _data = new DataManager(); // Handles user authentication and data management

        public MainForm()
        {
            InitializeComponent();
        }

        // Runs when the form loads
        private void MainForm_Load(object sender, EventArgs e)
        {
            // Get employee data from the database
            var dt = _svc.Employees(); // columns: EmployeeID, FirstName, LastName, SkillLevel

            // Add a FullName column if it doesn't exist
            if (!dt.Columns.Contains("FullName"))
                dt.Columns.Add("FullName", typeof(string));

            // Populate FullName for each employee
            foreach (DataRow r in dt.Rows)
                r["FullName"] = string.Format("{0} {1}", r["FirstName"], r["LastName"]);

            // Bind employees to the combo box
            cmbEmployees.DisplayMember = "FullName";
            cmbEmployees.ValueMember = "EmployeeID";
            cmbEmployees.DataSource = dt;
        }

        // Handles the login process when the Login button is clicked
        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Make sure an employee is selected
            if (cmbEmployees.SelectedValue == null)
            {
                MessageBox.Show("Please select an employee.");
                return;
            }

            string employeeId = cmbEmployees.SelectedValue.ToString();
            string fullName, role, msg;

            // Try to log in with entered password
            if (!_data.TryLoginByEmployeeId(employeeId, txtPassword.Text, out fullName, out role, out msg))
            {
                MessageBox.Show(msg, "Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Clear();
                txtPassword.Focus();
                return;
            }

            // Save logged-in user info
            CurrentUser.Set(employeeId, fullName, role);

            // Open AdminForm if Admin, otherwise open CheckoutReturnForm
            Form next;
            if (string.Equals(role, "Admin", StringComparison.OrdinalIgnoreCase))
            {
                next = new AdminForm();
            }
            else
            {
                next = new CheckoutReturnForm();
            }

            next.StartPosition = FormStartPosition.CenterScreen;

            // When the child form closes, log the user out and return to login screen
            next.FormClosed += (s, args) =>
            {
                CurrentUser.Clear();
                this.Show();
                txtPassword.Clear();
                txtPassword.Focus();
            };

            this.Hide();
            next.Show();
        }

        // Opens the report form when Report button is clicked
        private void btnReport_Click(object sender, EventArgs e)
        {
            var rf = new ReportForm();
            rf.StartPosition = FormStartPosition.CenterParent;
            rf.ShowDialog(this);
        }
    }
}
