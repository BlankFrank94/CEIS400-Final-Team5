using System;
using System.Data;
using System.Windows.Forms;
using Team5_Final.Data;

namespace Team5_Final
{
    public partial class ManageInventoryForm : Form
    {
        private readonly DataManager _data = new DataManager();
        private int? _currentId = null;              // EquipmentID selected from Current Inventory
        private DataTable _templates;                // distinct names (templates)

        public ManageInventoryForm()
        {
            InitializeComponent();

            // hooks
            this.Load += ManageInventoryForm_Load;
            cboTools.SelectedIndexChanged += cboTools_SelectedIndexChanged;
            cboName.SelectedIndexChanged += cboName_SelectedIndexChanged; // auto-fill when picking a template
        }

        // -------- load / bind ------------------------------------------------
        private void ManageInventoryForm_Load(object sender, EventArgs e)
        {
            txtEquipmentId.ReadOnly = true;

            numMinSkill.Minimum = 0; numMinSkill.Maximum = 10; numMinSkill.Value = 1;
            numPrice.Minimum = 0; numPrice.Maximum = 1_000_000; numPrice.DecimalPlaces = 2;
            numPrice.ThousandsSeparator = true;

            LoadTemplates();       // load name templates for auto-fill
            RefreshToolList();     // load current inventory list
            ClearFields();
        }

        private void LoadTemplates()
        {
            // Pull distinct names (+ their typical Description/MinSkillLevel) from DB
            var dt = _data.GetEquipmentTemplates();

            // Insert a blank option at the top
            var blank = dt.NewRow();
            blank["Name"] = "";           // what shows in the textbox
            blank["Description"] = DBNull.Value; // keeps things clean
            blank["MinSkillLevel"] = DBNull.Value;
            dt.Rows.InsertAt(blank, 0);

            _templates = dt;

            cboName.DisplayMember = "Name";
            cboName.ValueMember = "Name";
            cboName.DataSource = _templates;

            // Good typing UX
            cboName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboName.AutoCompleteSource = AutoCompleteSource.ListItems;

            // Start on the blank row
            cboName.SelectedIndex = 0;
        }


        private void RefreshToolList(int? keepSelectedId = null)
        {
            var dt = _data.GetAllEquipment();

            // Insert blank option at the top for adding new tools
            var selRow = dt.NewRow();
            selRow["EquipmentID"] = DBNull.Value;
            selRow["EquipmentName"] = "(Add New Tool)";
            dt.Rows.InsertAt(selRow, 0);

            cboTools.DisplayMember = "EquipmentName";
            cboTools.ValueMember = "EquipmentID";
            cboTools.DataSource = dt;

            if (keepSelectedId.HasValue)
                cboTools.SelectedValue = keepSelectedId.Value;
            else
                cboTools.SelectedIndex = 0;
        }

        // When a template name is chosen, prefill description & min skill
        private void cboName_SelectedIndexChanged(object sender, EventArgs e)
        {
            // If the user is typing a custom value (not selecting), bail
            if (cboName.SelectedIndex < 0) return;

            // Index 0 is our blank row — clear fields and stop
            if (cboName.SelectedIndex == 0)
            {
                txtDescription.Clear();
                numMinSkill.Value = 1;
                return;
            }

            if (cboName.SelectedItem is DataRowView drv)
            {
                // Fill only Description & Min skill; leave Condition & Price alone
                txtDescription.Text = Convert.ToString(drv["Description"] ?? "");
                numMinSkill.Value = SafeInt(drv["MinSkillLevel"], 1);
            }
        }


        // Current Inventory selection -> load tool into editor
        private void cboTools_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTools.SelectedValue == null || cboTools.SelectedValue == DBNull.Value)
            {
                _currentId = null;
                ClearFields(); // leaves everything blank so user can add new
                return;
            }


            _currentId = Convert.ToInt32(cboTools.SelectedValue);

            var drv = cboTools.SelectedItem as DataRowView;
            if (drv == null) { ClearFields(); return; }

            txtEquipmentId.Text = _currentId.ToString();

            // set fields from the selected inventory row
            var name = Convert.ToString(drv["EquipmentName"]);
            cboName.SelectedValue = name;            // try to select template row (for future auto-fill)
            if (cboName.SelectedIndex < 0) cboName.Text = name;

            txtDescription.Text = Convert.ToString(drv["Description"]);
            cboCondition.Text = Convert.ToString(drv["Condition"]);
            numPrice.Value = SafeDecimal(drv["Price"]);
            numMinSkill.Value = SafeInt(drv["MinSkillLevel"], 1);
        }

        private static decimal SafeDecimal(object o, decimal fallback = 0m)
            => (o == null || o == DBNull.Value) ? fallback : Convert.ToDecimal(o);

        private static int SafeInt(object o, int fallback = 0)
            => (o == null || o == DBNull.Value) ? fallback : Convert.ToInt32(o);

        private void ClearFields()
        {
            txtEquipmentId.Text = "";

            cboName.SelectedIndex = -1;  // clears selection
            cboName.Text = string.Empty; // clears typed text

            txtDescription.Clear();

            cboCondition.SelectedIndex = -1;
            cboCondition.Text = string.Empty;

            numMinSkill.Value = 1;
            numPrice.Value = 0;
        }

        // -------- buttons ----------------------------------------------------

        private void btnAddTool_Click(object sender, EventArgs e)
        {
            var (ok, msg) = ValidateInputs();
            if (!ok)
            {
                MessageBox.Show(msg, "Add Tool", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int newId = _data.AddEquipment(
                    cboName.Text.Trim(),
                    txtDescription.Text.Trim(),
                    cboCondition.Text.Trim(),
                    numPrice.Value,
                    (int)numMinSkill.Value);

                txtEquipmentId.Text = newId.ToString();
                RefreshToolList(keepSelectedId: newId);
                MessageBox.Show("Tool added.", "Add Tool");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Add failed:\n" + ex.Message, "Add Tool",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdateTool_Click(object sender, EventArgs e)
        {
            if (_currentId == null)
            {
                MessageBox.Show("Pick a tool to update from the list.", "Update Tool",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var (ok, msg) = ValidateInputs();
            if (!ok)
            {
                MessageBox.Show(msg, "Update Tool", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int rows = _data.UpdateEquipment(
                    _currentId.Value,
                    cboName.Text.Trim(),
                    txtDescription.Text.Trim(),
                    cboCondition.Text.Trim(),
                    numPrice.Value,
                    (int)numMinSkill.Value);

                if (rows == 1)
                {
                    RefreshToolList(keepSelectedId: _currentId);
                    MessageBox.Show("Tool updated.", "Update Tool");
                }
                else
                {
                    MessageBox.Show("No tool updated (was it deleted?).", "Update Tool");
                    RefreshToolList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update failed:\n" + ex.Message, "Update Tool",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDeleteTool_Click(object sender, EventArgs e)
        {
            if (_currentId == null)
            {
                MessageBox.Show("Pick a tool to delete from the list.", "Delete Tool",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Delete this tool?", "Delete Tool",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                int rows = _data.DeleteEquipment(_currentId.Value);
                MessageBox.Show(rows == 1 ? "Tool deleted." : "No tool deleted.", "Delete Tool");
                _currentId = null;
                RefreshToolList();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Delete failed:\n" + ex.Message, "Delete Tool",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e) => Close();

        // -------- validation --------------------------------------------------

        private (bool ok, string msg) ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(cboName.Text))
                return (false, "Name is required.");

            if (numMinSkill.Value < 0 || numMinSkill.Value > 10)
                return (false, "Min Skill must be between 0 and 10.");

            if (numPrice.Value < 0)
                return (false, "Price cannot be negative.");

            return (true, "OK");
        }
    }
}
