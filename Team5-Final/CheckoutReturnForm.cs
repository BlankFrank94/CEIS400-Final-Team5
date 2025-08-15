using System;
using System.Data;
using System.Windows.Forms;
using Team5_Final.Logic;

namespace Team5_Final
{
    public partial class CheckoutReturnForm : Form
    {
        // Service object to handle database and business logic
        private readonly InventoryService _svc = new InventoryService();

        // Tracks the logged-in employee ID
        private string _employeeId;

        public CheckoutReturnForm()
        {
            InitializeComponent(); // Sets up form controls
        }

        private void cboAvailable_SelectedIndexChanged(object sender, EventArgs e)
        {
            // No action needed when the available tools dropdown changes
        }

        private void CheckoutReturnForm_Load(object sender, EventArgs e)
        {
            // Load equipment data into the dataset (designer-generated binding)
            this.equipment_TableTableAdapter.Fill(this.cEIS400Team5DBDataSet.Equipment_Table);

            // Get the logged-in user's Employee ID
            _employeeId = CurrentUser.EmployeeId;

            // Display who is signed in (use name if available, otherwise ID)
            lblUser.Text = "Signed in: " + (CurrentUser.Name ?? _employeeId);

            // Fill the UI with current data
            RefreshMine();      // My checked-out tools
            FillAvailable();    // Available tools
            FillReturnCombo();  // Tools I can return
        }

        private void RefreshMine()
        {
            // Get a list of tools currently checked out by this employee
            var dt = _svc.EmployeeActiveCheckouts(_employeeId);

            // Allow automatic column creation in the DataGridView
            dgvMyTools.AutoGenerateColumns = true;

            // Bind the data to the DataGridView
            dgvMyTools.DataSource = dt;
        }

        private void FillReturnCombo()
        {
            // Get the tools this employee has checked out
            var dt = _svc.EmployeeActiveCheckouts(_employeeId);

            // Remove any previous binding (avoids mixing with designer data)
            cboReturn.DataSource = null;

            // Bind dropdown to show "Equipment" text but store "LogID" value
            cboReturn.DataSource = dt;
            cboReturn.DisplayMember = "Equipment";
            cboReturn.ValueMember = "LogID";
        }

        private void FillAvailable()
        {
            // Get all equipment that is currently available for checkout
            var dt = _svc.AvailableEquipment();

            // Clear any old binding
            cboAvailable.DataSource = null;

            // Bind dropdown to show "Name" but store "EquipmentID"
            cboAvailable.DataSource = dt;
            cboAvailable.DisplayMember = "Name";
            cboAvailable.ValueMember = "EquipmentID";
        }

        private void btnCheckout_Click(object sender, EventArgs e)
        {
            // Ensure the user has selected something to check out
            if (cboAvailable.SelectedValue == null)
            {
                MessageBox.Show("Select a tool to check out.");
                return;
            }

            // Get the EquipmentID from the selected item
            int equipmentId = Convert.ToInt32(cboAvailable.SelectedValue);

            // Attempt to check out the tool
            var result = _svc.Checkout(_employeeId, equipmentId);

            // If checkout fails, show warning
            if (!result.ok)
            {
                MessageBox.Show(result.msg, "Checkout", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Refresh UI so new data is visible
            RefreshMine();
            FillAvailable();
            FillReturnCombo();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            // Ensure the user has selected a tool to return
            if (cboReturn.SelectedValue == null)
            {
                MessageBox.Show("Select a tool to return.");
                return;
            }

            // Get the LogID for the selected tool
            int logId = Convert.ToInt32(cboReturn.SelectedValue);

            // Ask if the item is damaged
            bool isDamaged = MessageBox.Show(
                "Is the item damaged?",
                "Return",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes;

            // Ask if the item is lost
            bool isLost = MessageBox.Show(
                "Is the item lost?",
                "Return",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes;

            // Attempt to process the return with damage/lost flags
            var result = _svc.Return(logId, isDamaged, isLost);

            // If return fails, show warning
            if (!result.ok)
            {
                MessageBox.Show(result.msg, "Return", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Refresh UI so updated data is visible
            RefreshMine();
            FillAvailable();
            FillReturnCombo();
        }
    }
}
