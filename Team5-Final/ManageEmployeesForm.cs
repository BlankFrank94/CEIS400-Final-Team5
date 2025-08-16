using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Team5_Final.Data;

namespace Team5_Final
{
    public partial class ManageEmployeesForm : Form
    {
        private readonly DataManager _data = new DataManager();

        public ManageEmployeesForm()
        {
            InitializeComponent();   // Set up the form's UI components
        }

        // Runs when the form is loaded, loads the employee list
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ReloadGrid();
        }

        // Loads employee data into the DataGridView
        private void ReloadGrid()
        {
            try
            {
                var dt = _data.GetEmployeesForAdmin();
                dgvEmployees.DataSource = dt;

                // Hide the Password column for security
                if (dgvEmployees.Columns.Contains("Password"))
                    dgvEmployees.Columns["Password"].Visible = false;

                // Rename headers for better readability
                if (dgvEmployees.Columns.Contains("EmployeeID")) dgvEmployees.Columns["EmployeeID"].HeaderText = "ID";
                if (dgvEmployees.Columns.Contains("FirstName")) dgvEmployees.Columns["FirstName"].HeaderText = "First";
                if (dgvEmployees.Columns.Contains("LastName")) dgvEmployees.Columns["LastName"].HeaderText = "Last";
                if (dgvEmployees.Columns.Contains("SkillLevel")) dgvEmployees.Columns["SkillLevel"].HeaderText = "Skill";
                if (dgvEmployees.Columns.Contains("JobStatus")) dgvEmployees.Columns["JobStatus"].HeaderText = "Status";

                dgvEmployees.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load employees:\n" + ex.Message);
            }
        }

        // Returns the Employee IDs of all selected rows
        private string[] SelectedEmployeeIds()
        {
            return dgvEmployees.SelectedRows
                .Cast<DataGridViewRow>()
                .Where(r => r.DataBoundItem is DataRowView)
                .Select(r => ((DataRowView)r.DataBoundItem)["EmployeeID"]?.ToString())
                .Where(id => !string.IsNullOrWhiteSpace(id))
                .ToArray();
        }

        // Shows a small input box to get user text input
        private static string Prompt(string label, string @default = "")
        {
            using (var f = new Form { Width = 360, Height = 150, StartPosition = FormStartPosition.CenterParent, Text = label })
            {
                var tb = new TextBox { Left = 12, Top = 12, Width = 320, Text = @default };
                var ok = new Button { Left = 172, Width = 75, Top = 50, Text = "OK", DialogResult = DialogResult.OK };
                var cancel = new Button { Left = 257, Width = 75, Top = 50, Text = "Cancel", DialogResult = DialogResult.Cancel };
                f.Controls.Add(tb); f.Controls.Add(ok); f.Controls.Add(cancel);
                f.AcceptButton = ok; f.CancelButton = cancel;
                return f.ShowDialog() == DialogResult.OK ? tb.Text : null;
            }
        }

        // Sets the selected employees' role to Admin
        private void btnAdmin_Click(object sender, EventArgs e)
        {
            var ids = SelectedEmployeeIds();
            if (ids.Length == 0) { MessageBox.Show("Select one or more employees."); return; }

            try
            {
                foreach (var id in ids) _data.SetEmployeeRole(id, "Admin");
                ReloadGrid();
                MessageBox.Show("Admin role granted.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Operation failed:\n" + ex.Message);
            }
        }

        // Changes the selected employees' role back to User
        private void btnRevoke_Click(object sender, EventArgs e)
        {
            var ids = SelectedEmployeeIds();
            if (ids.Length == 0) { MessageBox.Show("Select one or more employees."); return; }

            try
            {
                foreach (var id in ids) _data.SetEmployeeRole(id, "User");
                ReloadGrid();
                MessageBox.Show("Admin role revoked.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Operation failed:\n" + ex.Message);
            }
        }

        // Marks selected employees as Terminated and clears their role
        private void btnTerminate_Click(object sender, EventArgs e)
        {
            var ids = SelectedEmployeeIds();
            if (ids.Length == 0)
            {
                MessageBox.Show("Select one or more employees.");
                return;
            }

            if (MessageBox.Show("Mark selected employee(s) as TERMINATED and clear their role?",
                "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                int terminatedCount = 0;
                int skippedCount = 0;

                foreach (var id in ids)
                {
                    var row = _data.GetEmployeeById(id); // get employee row
                    if (row == null) continue;

                    var status = Convert.ToString(row["JobStatus"]);
                    if (string.Equals(status, "Terminated", StringComparison.OrdinalIgnoreCase))
                    {
                        skippedCount++;
                        continue; // already terminated, skip
                    }

                    _data.SetEmployeeRole(id, null); // Clear role
                    _data.SetEmployeeStatus(id, "Terminated"); // Update status
                    terminatedCount++;
                }

                ReloadGrid();

                if (terminatedCount > 0)
                    MessageBox.Show($"Terminated {terminatedCount} employee(s).");

                if (skippedCount > 0)
                    MessageBox.Show($"{skippedCount} employee(s) were already terminated and skipped.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Operation failed:\n" + ex.Message);
            }
        }


        // Restores terminated employees back to Active with User role
        private void btnRestore_Click(object sender, EventArgs e)
        {
            var ids = SelectedEmployeeIds();
            if (ids.Length == 0) { MessageBox.Show("Select one or more employees."); return; }

            try
            {
                int restored = 0;

                foreach (var id in ids)
                {
                    var row = _data.GetEmployeeById(id);
                    if (row == null) continue;

                    var status = Convert.ToString(row["JobStatus"]);
                    if (!string.Equals(status, "Terminated", StringComparison.OrdinalIgnoreCase))
                        continue; // Skip if not terminated

                    restored += _data.RestoreEmployee(id);
                }

                if (restored == 0)
                    MessageBox.Show("No terminated employees selected to restore.");
                else
                    MessageBox.Show($"Restored {restored} employee(s).");

                ReloadGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Operation failed:\n" + ex.Message);
            }
        }

        // Adds a new employee with provided details
        private void btnAddEmployee_Click(object sender, EventArgs e)
        {
            var pw = Prompt("Temp password:", "0000");
            if (pw == null) return;

            var fn = Prompt("First name:");
            if (fn == null) return;

            var ln = Prompt("Last name:");
            if (ln == null) return;

            var skillText = Prompt("Skill (0-10):", "1");
            if (skillText == null) return;
            if (!int.TryParse(skillText, out int skill)) skill = 1;

            try
            {
                // Create a unique ID from initials
                string id = _data.GenerateEmployeeId(fn, ln);

                _data.AddEmployee(id, pw, fn, ln, skill, "User", "Active");
                ReloadGrid();
                MessageBox.Show($"Employee added.\nID: {id}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Add failed:\n" + ex.Message);
            }
        }

        // Closes the form
        private void btnClose_Click(object sender, EventArgs e) => Close();
    }
}
