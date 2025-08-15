namespace Team5_Final
{
    partial class CheckoutReturnForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblUser = new System.Windows.Forms.Label();
            this.dgvMyTools = new System.Windows.Forms.DataGridView();
            this.logIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.employeeIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.equipmentIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateCheckedOutDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.equipmentLogTableBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cEIS400Team5DBDataSet = new Team5_Final.CEIS400Team5DBDataSet();
            this.equipmentLogTableTableAdapter = new Team5_Final.CEIS400Team5DBDataSetTableAdapters.EquipmentLogTableTableAdapter();
            this.gbReturn = new System.Windows.Forms.GroupBox();
            this.btnReturn = new System.Windows.Forms.Button();
            this.cboReturn = new System.Windows.Forms.ComboBox();
            this.gbCheckout = new System.Windows.Forms.GroupBox();
            this.btnCheckout = new System.Windows.Forms.Button();
            this.cboAvailable = new System.Windows.Forms.ComboBox();
            this.equipmentLogTableBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.equipmentTableBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.equipment_TableTableAdapter = new Team5_Final.CEIS400Team5DBDataSetTableAdapters.Equipment_TableTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMyTools)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.equipmentLogTableBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cEIS400Team5DBDataSet)).BeginInit();
            this.gbReturn.SuspendLayout();
            this.gbCheckout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.equipmentLogTableBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.equipmentTableBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Location = new System.Drawing.Point(88, 78);
            this.lblUser.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(95, 20);
            this.lblUser.TabIndex = 0;
            this.lblUser.Text = "Signed in: ...";
            // 
            // dgvMyTools
            // 
            this.dgvMyTools.AllowUserToAddRows = false;
            this.dgvMyTools.AutoGenerateColumns = false;
            this.dgvMyTools.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMyTools.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.logIDDataGridViewTextBoxColumn,
            this.employeeIDDataGridViewTextBoxColumn,
            this.equipmentIDDataGridViewTextBoxColumn,
            this.dateCheckedOutDataGridViewTextBoxColumn});
            this.dgvMyTools.DataSource = this.equipmentLogTableBindingSource;
            this.dgvMyTools.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvMyTools.Location = new System.Drawing.Point(0, 461);
            this.dgvMyTools.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgvMyTools.Name = "dgvMyTools";
            this.dgvMyTools.RowHeadersWidth = 62;
            this.dgvMyTools.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMyTools.Size = new System.Drawing.Size(1200, 231);
            this.dgvMyTools.TabIndex = 1;
            // 
            // logIDDataGridViewTextBoxColumn
            // 
            this.logIDDataGridViewTextBoxColumn.DataPropertyName = "LogID";
            this.logIDDataGridViewTextBoxColumn.HeaderText = "LogID";
            this.logIDDataGridViewTextBoxColumn.MinimumWidth = 8;
            this.logIDDataGridViewTextBoxColumn.Name = "logIDDataGridViewTextBoxColumn";
            this.logIDDataGridViewTextBoxColumn.Width = 150;
            // 
            // employeeIDDataGridViewTextBoxColumn
            // 
            this.employeeIDDataGridViewTextBoxColumn.DataPropertyName = "EmployeeID";
            this.employeeIDDataGridViewTextBoxColumn.HeaderText = "EmployeeID";
            this.employeeIDDataGridViewTextBoxColumn.MinimumWidth = 8;
            this.employeeIDDataGridViewTextBoxColumn.Name = "employeeIDDataGridViewTextBoxColumn";
            this.employeeIDDataGridViewTextBoxColumn.Width = 150;
            // 
            // equipmentIDDataGridViewTextBoxColumn
            // 
            this.equipmentIDDataGridViewTextBoxColumn.DataPropertyName = "EquipmentID";
            this.equipmentIDDataGridViewTextBoxColumn.HeaderText = "EquipmentID";
            this.equipmentIDDataGridViewTextBoxColumn.MinimumWidth = 8;
            this.equipmentIDDataGridViewTextBoxColumn.Name = "equipmentIDDataGridViewTextBoxColumn";
            this.equipmentIDDataGridViewTextBoxColumn.Width = 150;
            // 
            // dateCheckedOutDataGridViewTextBoxColumn
            // 
            this.dateCheckedOutDataGridViewTextBoxColumn.DataPropertyName = "DateCheckedOut";
            this.dateCheckedOutDataGridViewTextBoxColumn.HeaderText = "DateCheckedOut";
            this.dateCheckedOutDataGridViewTextBoxColumn.MinimumWidth = 8;
            this.dateCheckedOutDataGridViewTextBoxColumn.Name = "dateCheckedOutDataGridViewTextBoxColumn";
            this.dateCheckedOutDataGridViewTextBoxColumn.Width = 150;
            // 
            // equipmentLogTableBindingSource
            // 
            this.equipmentLogTableBindingSource.DataMember = "EquipmentLogTable";
            this.equipmentLogTableBindingSource.DataSource = this.cEIS400Team5DBDataSet;
            // 
            // cEIS400Team5DBDataSet
            // 
            this.cEIS400Team5DBDataSet.DataSetName = "CEIS400Team5DBDataSet";
            this.cEIS400Team5DBDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // equipmentLogTableTableAdapter
            // 
            this.equipmentLogTableTableAdapter.ClearBeforeFill = true;
            // 
            // gbReturn
            // 
            this.gbReturn.Controls.Add(this.btnReturn);
            this.gbReturn.Controls.Add(this.cboReturn);
            this.gbReturn.Location = new System.Drawing.Point(686, 78);
            this.gbReturn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbReturn.Name = "gbReturn";
            this.gbReturn.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbReturn.Size = new System.Drawing.Size(300, 154);
            this.gbReturn.TabIndex = 2;
            this.gbReturn.TabStop = false;
            this.gbReturn.Text = "Return:";
            // 
            // btnReturn
            // 
            this.btnReturn.Location = new System.Drawing.Point(98, 109);
            this.btnReturn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(112, 35);
            this.btnReturn.TabIndex = 1;
            this.btnReturn.Text = "Return";
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // cboReturn
            // 
            this.cboReturn.FormattingEnabled = true;
            this.cboReturn.Location = new System.Drawing.Point(62, 43);
            this.cboReturn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cboReturn.Name = "cboReturn";
            this.cboReturn.Size = new System.Drawing.Size(180, 28);
            this.cboReturn.TabIndex = 0;
            // 
            // gbCheckout
            // 
            this.gbCheckout.Controls.Add(this.btnCheckout);
            this.gbCheckout.Controls.Add(this.cboAvailable);
            this.gbCheckout.Location = new System.Drawing.Point(686, 272);
            this.gbCheckout.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbCheckout.Name = "gbCheckout";
            this.gbCheckout.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbCheckout.Size = new System.Drawing.Size(300, 154);
            this.gbCheckout.TabIndex = 3;
            this.gbCheckout.TabStop = false;
            this.gbCheckout.Text = "Checkout:";
            // 
            // btnCheckout
            // 
            this.btnCheckout.Location = new System.Drawing.Point(98, 109);
            this.btnCheckout.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCheckout.Name = "btnCheckout";
            this.btnCheckout.Size = new System.Drawing.Size(112, 35);
            this.btnCheckout.TabIndex = 1;
            this.btnCheckout.Text = "Checkout";
            this.btnCheckout.UseVisualStyleBackColor = true;
            this.btnCheckout.Click += new System.EventHandler(this.btnCheckout_Click);
            // 
            // cboAvailable
            // 
            this.cboAvailable.FormattingEnabled = true;
            this.cboAvailable.Location = new System.Drawing.Point(62, 43);
            this.cboAvailable.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cboAvailable.Name = "cboAvailable";
            this.cboAvailable.Size = new System.Drawing.Size(180, 28);
            this.cboAvailable.TabIndex = 0;
            this.cboAvailable.SelectedIndexChanged += new System.EventHandler(this.cboAvailable_SelectedIndexChanged);
            // 
            // equipmentLogTableBindingSource1
            // 
            this.equipmentLogTableBindingSource1.DataMember = "EquipmentLogTable";
            this.equipmentLogTableBindingSource1.DataSource = this.cEIS400Team5DBDataSet;
            // 
            // equipmentTableBindingSource
            // 
            this.equipmentTableBindingSource.DataMember = "Equipment Table";
            this.equipmentTableBindingSource.DataSource = this.cEIS400Team5DBDataSet;
            // 
            // equipment_TableTableAdapter
            // 
            this.equipment_TableTableAdapter.ClearBeforeFill = true;
            // 
            // CheckoutReturnForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 692);
            this.Controls.Add(this.gbCheckout);
            this.Controls.Add(this.gbReturn);
            this.Controls.Add(this.dgvMyTools);
            this.Controls.Add(this.lblUser);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "CheckoutReturnForm";
            this.Text = "CheckoutReturnForm";
            this.Load += new System.EventHandler(this.CheckoutReturnForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMyTools)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.equipmentLogTableBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cEIS400Team5DBDataSet)).EndInit();
            this.gbReturn.ResumeLayout(false);
            this.gbCheckout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.equipmentLogTableBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.equipmentTableBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.DataGridView dgvMyTools;
        private CEIS400Team5DBDataSet cEIS400Team5DBDataSet;
        private System.Windows.Forms.BindingSource equipmentLogTableBindingSource;
        private CEIS400Team5DBDataSetTableAdapters.EquipmentLogTableTableAdapter equipmentLogTableTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn logIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn employeeIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn equipmentIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateCheckedOutDataGridViewTextBoxColumn;
        private System.Windows.Forms.GroupBox gbReturn;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.ComboBox cboReturn;
        private System.Windows.Forms.GroupBox gbCheckout;
        private System.Windows.Forms.Button btnCheckout;
        private System.Windows.Forms.ComboBox cboAvailable;
        private System.Windows.Forms.BindingSource equipmentLogTableBindingSource1;
        private System.Windows.Forms.BindingSource equipmentTableBindingSource;
        private CEIS400Team5DBDataSetTableAdapters.Equipment_TableTableAdapter equipment_TableTableAdapter;
    }
}