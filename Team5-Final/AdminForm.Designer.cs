namespace Team5_Final
{
    partial class AdminForm
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
            this.btnInventory = new System.Windows.Forms.Button();
            this.btnEmployees = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.lblUser = new System.Windows.Forms.Label();
            this.gbCheckout = new System.Windows.Forms.GroupBox();
            this.btnCheckout = new System.Windows.Forms.Button();
            this.cboAvailable = new System.Windows.Forms.ComboBox();
            this.gbReturn = new System.Windows.Forms.GroupBox();
            this.btnReturn = new System.Windows.Forms.Button();
            this.cboReturn = new System.Windows.Forms.ComboBox();
            this.dgvMyTools = new System.Windows.Forms.DataGridView();
            this.equipmentLogTableBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cEIS400Team5DBDataSet = new Team5_Final.CEIS400Team5DBDataSet();
            this.equipmentLogTableTableAdapter = new Team5_Final.CEIS400Team5DBDataSetTableAdapters.EquipmentLogTableTableAdapter();
            this.gbCheckout.SuspendLayout();
            this.gbReturn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMyTools)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.equipmentLogTableBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cEIS400Team5DBDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // btnInventory
            // 
            this.btnInventory.Location = new System.Drawing.Point(207, 57);
            this.btnInventory.Name = "btnInventory";
            this.btnInventory.Size = new System.Drawing.Size(166, 34);
            this.btnInventory.TabIndex = 0;
            this.btnInventory.Text = "Manage Inventory";
            this.btnInventory.UseVisualStyleBackColor = true;
            this.btnInventory.Click += new System.EventHandler(this.btnInventory_Click);
            // 
            // btnEmployees
            // 
            this.btnEmployees.Location = new System.Drawing.Point(379, 57);
            this.btnEmployees.Name = "btnEmployees";
            this.btnEmployees.Size = new System.Drawing.Size(166, 34);
            this.btnEmployees.TabIndex = 1;
            this.btnEmployees.Text = "Manage Employee";
            this.btnEmployees.UseVisualStyleBackColor = true;
            this.btnEmployees.Click += new System.EventHandler(this.btnEmployees_Click);
            // 
            // btnReport
            // 
            this.btnReport.Location = new System.Drawing.Point(35, 57);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(166, 34);
            this.btnReport.TabIndex = 2;
            this.btnReport.Text = "Generate Report";
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Location = new System.Drawing.Point(102, 19);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(51, 20);
            this.lblUser.TabIndex = 3;
            this.lblUser.Text = "label1";
            // 
            // gbCheckout
            // 
            this.gbCheckout.Controls.Add(this.btnCheckout);
            this.gbCheckout.Controls.Add(this.cboAvailable);
            this.gbCheckout.Location = new System.Drawing.Point(568, 251);
            this.gbCheckout.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbCheckout.Name = "gbCheckout";
            this.gbCheckout.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbCheckout.Size = new System.Drawing.Size(300, 154);
            this.gbCheckout.TabIndex = 5;
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
            // 
            // gbReturn
            // 
            this.gbReturn.Controls.Add(this.btnReturn);
            this.gbReturn.Controls.Add(this.cboReturn);
            this.gbReturn.Location = new System.Drawing.Point(568, 57);
            this.gbReturn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbReturn.Name = "gbReturn";
            this.gbReturn.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbReturn.Size = new System.Drawing.Size(300, 154);
            this.gbReturn.TabIndex = 4;
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
            // dgvMyTools
            // 
            this.dgvMyTools.AllowUserToAddRows = false;
            this.dgvMyTools.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMyTools.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvMyTools.Location = new System.Drawing.Point(0, 415);
            this.dgvMyTools.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgvMyTools.Name = "dgvMyTools";
            this.dgvMyTools.RowHeadersWidth = 62;
            this.dgvMyTools.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMyTools.Size = new System.Drawing.Size(881, 133);
            this.dgvMyTools.TabIndex = 6;
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
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(881, 548);
            this.Controls.Add(this.dgvMyTools);
            this.Controls.Add(this.gbCheckout);
            this.Controls.Add(this.gbReturn);
            this.Controls.Add(this.lblUser);
            this.Controls.Add(this.btnReport);
            this.Controls.Add(this.btnEmployees);
            this.Controls.Add(this.btnInventory);
            this.Name = "AdminForm";
            this.Text = "AdminForm";
            this.Load += new System.EventHandler(this.AdminForm_Load);
            this.gbCheckout.ResumeLayout(false);
            this.gbReturn.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMyTools)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.equipmentLogTableBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cEIS400Team5DBDataSet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnInventory;
        private System.Windows.Forms.Button btnEmployees;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.GroupBox gbCheckout;
        private System.Windows.Forms.Button btnCheckout;
        private System.Windows.Forms.ComboBox cboAvailable;
        private System.Windows.Forms.GroupBox gbReturn;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.ComboBox cboReturn;
        private System.Windows.Forms.DataGridView dgvMyTools;
        private CEIS400Team5DBDataSet cEIS400Team5DBDataSet;
        private System.Windows.Forms.BindingSource equipmentLogTableBindingSource;
        private CEIS400Team5DBDataSetTableAdapters.EquipmentLogTableTableAdapter equipmentLogTableTableAdapter;
    }
}