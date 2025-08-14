using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Team5_Final.Logic;

namespace Team5_Final
{

    public partial class MainForm : Form
    {
        private readonly InventoryService _svc = new InventoryService();
        public MainForm()
        {
            InitializeComponent();
            Load += MainForm_Load;
            btnCheckout.Click += BtnCheckout_Click;
            btnReturn.Click += BtnReturn_Click;
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            RefreshAll();
        }


    private void RefreshAll()
        {
            // Employees
            var emps = _svc.Employees();
            cbEmployee.DataSource = emps;
            cbEmployee.DisplayMember = "LastName";
            cbEmployee.ValueMember = "EmployeeID";

            // Available tools
            var eq = _svc.AvailableEquipment();
            cbEquipment.DataSource = eq;
            cbEquipment.DisplayMember = "Name";
            cbEquipment.ValueMember = "EquipmentID";

            // Active checkouts
            dgvActive.AutoGenerateColumns = true;
            dgvActive.DataSource = _svc.ActiveCheckouts();
            if (dgvActive.Columns.Contains("LogID"))
                dgvActive.Columns["LogID"].Visible = false;
        }

        private void BtnCheckout_Click(object sender, EventArgs e)
        {
            if (cbEmployee.SelectedValue == null || cbEquipment.SelectedValue == null)
            {
                MessageBox.Show("Pick employee and tool.");
                return;
            }

            string empId = cbEmployee.SelectedValue.ToString();
            int eqId = Convert.ToInt32(cbEquipment.SelectedValue);
            var (ok, msg) = _svc.Checkout(empId, eqId);
            MessageBox.Show(msg);
            if (ok) RefreshAll();
        }

        private void BtnReturn_Click(object sender, EventArgs e)
        {
            if (dgvActive.CurrentRow == null)
            {
                MessageBox.Show("Select a checkout row first.");
                return;
            }

            int logId = Convert.ToInt32(((DataRowView)dgvActive.CurrentRow.DataBoundItem)["LogID"]);
            // New prompts for Damage / Lost Check
            bool isDamaged = MessageBox.Show(
                "Is the item damaged?",
                "Return Item",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes;

            bool isLost = MessageBox.Show(
                "Is the item lost?",
                "Return Item",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes;

            // Updated call 
            var (ok, msg) = _svc.Return(logId, DateTime.Now, isDamaged, isLost);

            MessageBox.Show(msg);
            if (ok) RefreshAll();
        }

        private void cbEquipment_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
