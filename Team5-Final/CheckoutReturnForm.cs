using System;
using System.Data;
using System.Windows.Forms;
using Team5_Final.Logic;

namespace Team5_Final
{
    public partial class CheckoutReturnForm : Form
    {
        private readonly InventoryService _svc = new InventoryService();
        private string _employeeId;

        public CheckoutReturnForm()
        {
            InitializeComponent();
        }

        private void cboAvailable_SelectedIndexChanged(object sender, EventArgs e)
        {
            // no-op (we don't need to do anything on change right now)
        }


        private void CheckoutReturnForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'cEIS400Team5DBDataSet.Equipment_Table' table. You can move, or remove it, as needed.
            this.equipment_TableTableAdapter.Fill(this.cEIS400Team5DBDataSet.Equipment_Table);
            _employeeId = CurrentUser.EmployeeId;
            lblUser.Text = "Signed in: " + (CurrentUser.Name ?? _employeeId);

            RefreshMine();
            FillAvailable();
            FillReturnCombo();
        }

        private void RefreshMine()
        {
            var dt = _svc.EmployeeActiveCheckouts(_employeeId);
            dgvMyTools.AutoGenerateColumns = true;
            dgvMyTools.DataSource = dt;
        }

        private void FillReturnCombo()
        {
            var dt = _svc.EmployeeActiveCheckouts(_employeeId); // columns: LogID, Equipment, DateCheckedOut, ...

            // break any previous binding to avoid the Designer’s source
            cboReturn.DataSource = null;

            // bind to the runtime table that DOES have LogID
            cboReturn.DataSource = dt;
            cboReturn.DisplayMember = "Equipment";   // alias returned by the query
            cboReturn.ValueMember = "LogID";
        }

        private void FillAvailable()
        {
            var dt = _svc.AvailableEquipment(); // columns: EquipmentID, Name, MinSkillLevel
            cboAvailable.DataSource = null;
            cboAvailable.DataSource = dt;
            cboAvailable.DisplayMember = "Name";
            cboAvailable.ValueMember = "EquipmentID";
        }

        private void btnCheckout_Click(object sender, EventArgs e)
        {
            if (cboAvailable.SelectedValue == null)
            {
                MessageBox.Show("Select a tool to check out.");
                return;
            }

            int equipmentId = Convert.ToInt32(cboAvailable.SelectedValue);
            var result = _svc.Checkout(_employeeId, equipmentId);
            if (!result.ok)
            {
                MessageBox.Show(result.msg, "Checkout", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // refresh UI
            RefreshMine();
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
            var result = _svc.Return(logId);
            if (!result.ok)
            {
                MessageBox.Show(result.msg, "Return", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // refresh UI
            RefreshMine();
            FillAvailable();
            FillReturnCombo();
        }
    }
}