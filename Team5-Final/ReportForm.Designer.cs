namespace Team5_Final
{
    partial class ReportForm
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
            this.cEIS400Team5DBDataSet = new Team5_Final.CEIS400Team5DBDataSet();
            this.cEIS400Team5DBDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnExportCsv = new System.Windows.Forms.Button();
            this.dgvAll = new System.Windows.Forms.DataGridView();
            this.equipmentLogTableBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.equipmentLogTableTableAdapter = new Team5_Final.CEIS400Team5DBDataSetTableAdapters.EquipmentLogTableTableAdapter();
            this.logIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.employeeIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.equipmentIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateCheckedOutDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.cEIS400Team5DBDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cEIS400Team5DBDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.equipmentLogTableBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // cEIS400Team5DBDataSet
            // 
            this.cEIS400Team5DBDataSet.DataSetName = "CEIS400Team5DBDataSet";
            this.cEIS400Team5DBDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // cEIS400Team5DBDataSetBindingSource
            // 
            this.cEIS400Team5DBDataSetBindingSource.DataSource = this.cEIS400Team5DBDataSet;
            this.cEIS400Team5DBDataSetBindingSource.Position = 0;
            // 
            // btnExportCsv
            // 
            this.btnExportCsv.Location = new System.Drawing.Point(311, 389);
            this.btnExportCsv.Name = "btnExportCsv";
            this.btnExportCsv.Size = new System.Drawing.Size(176, 23);
            this.btnExportCsv.TabIndex = 1;
            this.btnExportCsv.Text = "Export CSV";
            this.btnExportCsv.UseVisualStyleBackColor = true;
            this.btnExportCsv.Click += new System.EventHandler(this.BtnExportCsv_Click);
            // 
            // dgvAll
            // 
            this.dgvAll.AutoGenerateColumns = false;
            this.dgvAll.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAll.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.logIDDataGridViewTextBoxColumn,
            this.employeeIDDataGridViewTextBoxColumn,
            this.equipmentIDDataGridViewTextBoxColumn,
            this.dateCheckedOutDataGridViewTextBoxColumn});
            this.dgvAll.DataSource = this.equipmentLogTableBindingSource;
            this.dgvAll.Location = new System.Drawing.Point(183, 93);
            this.dgvAll.Name = "dgvAll";
            this.dgvAll.Size = new System.Drawing.Size(443, 228);
            this.dgvAll.TabIndex = 2;
            // 
            // equipmentLogTableBindingSource
            // 
            this.equipmentLogTableBindingSource.DataMember = "EquipmentLogTable";
            this.equipmentLogTableBindingSource.DataSource = this.cEIS400Team5DBDataSet;
            // 
            // equipmentLogTableTableAdapter
            // 
            this.equipmentLogTableTableAdapter.ClearBeforeFill = true;
            // 
            // logIDDataGridViewTextBoxColumn
            // 
            this.logIDDataGridViewTextBoxColumn.DataPropertyName = "LogID";
            this.logIDDataGridViewTextBoxColumn.HeaderText = "LogID";
            this.logIDDataGridViewTextBoxColumn.Name = "logIDDataGridViewTextBoxColumn";
            // 
            // employeeIDDataGridViewTextBoxColumn
            // 
            this.employeeIDDataGridViewTextBoxColumn.DataPropertyName = "EmployeeID";
            this.employeeIDDataGridViewTextBoxColumn.HeaderText = "EmployeeID";
            this.employeeIDDataGridViewTextBoxColumn.Name = "employeeIDDataGridViewTextBoxColumn";
            // 
            // equipmentIDDataGridViewTextBoxColumn
            // 
            this.equipmentIDDataGridViewTextBoxColumn.DataPropertyName = "EquipmentID";
            this.equipmentIDDataGridViewTextBoxColumn.HeaderText = "EquipmentID";
            this.equipmentIDDataGridViewTextBoxColumn.Name = "equipmentIDDataGridViewTextBoxColumn";
            // 
            // dateCheckedOutDataGridViewTextBoxColumn
            // 
            this.dateCheckedOutDataGridViewTextBoxColumn.DataPropertyName = "DateCheckedOut";
            this.dateCheckedOutDataGridViewTextBoxColumn.HeaderText = "DateCheckedOut";
            this.dateCheckedOutDataGridViewTextBoxColumn.Name = "dateCheckedOutDataGridViewTextBoxColumn";
            // 
            // ReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgvAll);
            this.Controls.Add(this.btnExportCsv);
            this.Name = "ReportForm";
            this.Text = "Report Form";
            this.Load += new System.EventHandler(this.ReportForm_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.cEIS400Team5DBDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cEIS400Team5DBDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.equipmentLogTableBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.BindingSource cEIS400Team5DBDataSetBindingSource;
        private CEIS400Team5DBDataSet cEIS400Team5DBDataSet;
        private System.Windows.Forms.Button btnExportCsv;
        private System.Windows.Forms.DataGridView dgvAll;
        private System.Windows.Forms.BindingSource equipmentLogTableBindingSource;
        private CEIS400Team5DBDataSetTableAdapters.EquipmentLogTableTableAdapter equipmentLogTableTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn logIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn employeeIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn equipmentIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateCheckedOutDataGridViewTextBoxColumn;
    }
}