using System;
using System.Data;
using System.Windows.Forms;
using Team5_Final.Logic;

namespace Team5_Final
{
    public partial class AdminForm : Form
    {
        private readonly InventoryService _svc = new InventoryService();
        private string _employeeId; // admin’s own ID

        public AdminForm()
        {
            InitializeComponent();
            this.Load += AdminForm_Load;
        }

        // --- form load --------------------------------------------------------
        private void AdminForm_Load(object sender, EventArgs e)
        {
            // if the dataset/tableadapter line was auto-added and you still want it, keep it.
            // this.equipmentLogTableTableAdapter.Fill(this.cEIS400Team5DBDataSet.EquipmentLogTable);

            if (!string.Equals(CurrentUser.Role, "Admin", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Admin access required.");
                Close();
                return;
            }

            _employeeId = CurrentUser.EmployeeId;
            lblUser.Text = $"Admin: {CurrentUser.Name ?? _employeeId}";

            LoadMyActive();
            FillAvailable();
            FillReturnCombo();
        }

        // --- data binding helpers --------------------------------------------
        private void LoadMyActive()
        {
            var dt = _svc.EmployeeActiveCheckouts(_employeeId); // LogID, Equipment, DateCheckedOut, ...
            dgvMyTools.AutoGenerateColumns = true;
            dgvMyTools.DataSource = dt;
        }

        private void FillAvailable()
        {
            var dt = _svc.AvailableEquipment(); // EquipmentID, Name, MinSkillLevel
            cboAvailable.DataSource = null;
            cboAvailable.DisplayMember = "Name";
            cboAvailable.ValueMember = "EquipmentID";
            cboAvailable.DataSource = dt;
        }

        private void FillReturnCombo()
        {
            var dt = _svc.EmployeeActiveCheckouts(_employeeId); // needs LogID + Equipment
            cboReturn.DataSource = null;
            cboReturn.DisplayMember = "Equipment";
            cboReturn.ValueMember = "LogID";
            cboReturn.DataSource = dt;
        }

        // --- actions ----------------------------------------------------------
        private void btnCheckout_Click(object sender, EventArgs e)
        {
            if (cboAvailable.SelectedValue == null)
            {
                MessageBox.Show("Select a tool to check out.");
                return;
            }

            int equipmentId = Convert.ToInt32(cboAvailable.SelectedValue);
            var (ok, msg) = _svc.Checkout(_employeeId, equipmentId);
            if (!ok)
            {
                MessageBox.Show(msg, "Checkout", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            LoadMyActive();
            FillAvailable();
            FillReturnCombo();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            if (cboReturn.SelectedValue == null)
            {
                MessageBox.Show("Select a tool to return.");
                return;
            }

            int logId = Convert.ToInt32(cboReturn.SelectedValue);

            // Ask the two questions
            bool isDamaged = MessageBox.Show(
                "Is the item damaged?",
                "Return",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes;

            bool isLost = MessageBox.Show(
                "Is the item lost?",
                "Return",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes;

            // Optional sanity check: if they picked both, confirm
            if (isDamaged && isLost)
            {
                var both = MessageBox.Show(
                    "You marked this BOTH damaged and lost. Continue?",
                    "Confirm flags",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);
                if (both == DialogResult.No) return;
            }

            var (ok, msg) = _svc.Return(logId, isDamaged, isLost);
            MessageBox.Show(msg, "Return", MessageBoxButtons.OK,
                ok ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

            if (!ok) return;

            // Refresh UI
            LoadMyActive();
            FillAvailable();
            FillReturnCombo();
        }


        private void btnReport_Click(object sender, EventArgs e)
        {
            using (var rf = new ReportForm())
            {
                rf.StartPosition = FormStartPosition.CenterParent;
                rf.ShowDialog(this);
            }
        }

        private void btnEmployees_Click(object sender, EventArgs e)
        {
            // Double-check admin (defense in depth)
            if (!string.Equals(CurrentUser.Role, "Admin", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Admin access required.");
                return;
            }

            using (var f = new ManageEmployeesForm())
            {
                f.StartPosition = FormStartPosition.CenterParent;
                f.ShowDialog(this);
            }

            // If managing employees affects what this screen shows (usually it doesn't),
            // you can refresh here. Keeping it for parity:
            LoadMyActive();
            FillAvailable();
            FillReturnCombo();
        }


        private void btnInventory_Click(object sender, EventArgs e)
        {
            // Only admins should ever be here, but double-check anyway
            if (!string.Equals(CurrentUser.Role, "Admin", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Admin access required.");
                return;
            }

            // Open the inventory manager as a modal dialog
            using (var f = new ManageInventoryForm())
            {
                f.StartPosition = FormStartPosition.CenterParent;
                f.ShowDialog(this);
            }

            // After closing, refresh the Admin screen (in case tools were added/edited/removed)
            FillAvailable();   // repopulates checkout dropdown
            LoadMyActive();    // refreshes admin's active checkouts grid
            FillReturnCombo(); // refreshes return dropdown
        }


        // when admin form closes, sign out & return to login
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            CurrentUser.Clear();
            Application.OpenForms["MainForm"]?.Show();
        }
    }
}
