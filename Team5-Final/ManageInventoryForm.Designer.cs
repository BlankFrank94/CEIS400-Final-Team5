namespace Team5_Final
{
    partial class ManageInventoryForm
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
            this.lblName = new System.Windows.Forms.Label();
            this.lblMinSkill = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblCondition = new System.Windows.Forms.Label();
            this.gbEditor = new System.Windows.Forms.GroupBox();
            this.numPrice = new System.Windows.Forms.NumericUpDown();
            this.numMinSkill = new System.Windows.Forms.NumericUpDown();
            this.lblPrice = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.cboCondition = new System.Windows.Forms.ComboBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.cboTools = new System.Windows.Forms.ComboBox();
            this.lblCurrentInventory = new System.Windows.Forms.Label();
            this.lblD = new System.Windows.Forms.Label();
            this.txtEquipmentId = new System.Windows.Forms.TextBox();
            this.cboName = new System.Windows.Forms.ComboBox();
            this.gbEditor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinSkill)).BeginInit();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(17, 33);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(51, 20);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Name";
            // 
            // lblMinSkill
            // 
            this.lblMinSkill.AutoSize = true;
            this.lblMinSkill.Location = new System.Drawing.Point(17, 70);
            this.lblMinSkill.Name = "lblMinSkill";
            this.lblMinSkill.Size = new System.Drawing.Size(66, 20);
            this.lblMinSkill.TabIndex = 1;
            this.lblMinSkill.Text = "Min Skill";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(17, 105);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(89, 20);
            this.lblDescription.TabIndex = 2;
            this.lblDescription.Text = "Description";
            // 
            // lblCondition
            // 
            this.lblCondition.AutoSize = true;
            this.lblCondition.Location = new System.Drawing.Point(17, 144);
            this.lblCondition.Name = "lblCondition";
            this.lblCondition.Size = new System.Drawing.Size(76, 20);
            this.lblCondition.TabIndex = 3;
            this.lblCondition.Text = "Condition";
            // 
            // gbEditor
            // 
            this.gbEditor.Controls.Add(this.cboName);
            this.gbEditor.Controls.Add(this.txtEquipmentId);
            this.gbEditor.Controls.Add(this.lblD);
            this.gbEditor.Controls.Add(this.lblCurrentInventory);
            this.gbEditor.Controls.Add(this.cboTools);
            this.gbEditor.Controls.Add(this.btnClose);
            this.gbEditor.Controls.Add(this.btnDelete);
            this.gbEditor.Controls.Add(this.btnUpdate);
            this.gbEditor.Controls.Add(this.btnAdd);
            this.gbEditor.Controls.Add(this.cboCondition);
            this.gbEditor.Controls.Add(this.txtDescription);
            this.gbEditor.Controls.Add(this.lblPrice);
            this.gbEditor.Controls.Add(this.numMinSkill);
            this.gbEditor.Controls.Add(this.numPrice);
            this.gbEditor.Controls.Add(this.lblName);
            this.gbEditor.Controls.Add(this.lblMinSkill);
            this.gbEditor.Controls.Add(this.lblCondition);
            this.gbEditor.Controls.Add(this.lblDescription);
            this.gbEditor.Dock = System.Windows.Forms.DockStyle.Left;
            this.gbEditor.Location = new System.Drawing.Point(0, 0);
            this.gbEditor.Name = "gbEditor";
            this.gbEditor.Size = new System.Drawing.Size(517, 527);
            this.gbEditor.TabIndex = 5;
            this.gbEditor.TabStop = false;
            this.gbEditor.Text = "Tool Details";
            // 
            // numPrice
            // 
            this.numPrice.DecimalPlaces = 2;
            this.numPrice.Location = new System.Drawing.Point(131, 178);
            this.numPrice.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numPrice.Name = "numPrice";
            this.numPrice.Size = new System.Drawing.Size(120, 26);
            this.numPrice.TabIndex = 4;
            this.numPrice.ThousandsSeparator = true;
            // 
            // numMinSkill
            // 
            this.numMinSkill.Location = new System.Drawing.Point(131, 64);
            this.numMinSkill.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numMinSkill.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numMinSkill.Name = "numMinSkill";
            this.numMinSkill.Size = new System.Drawing.Size(120, 26);
            this.numMinSkill.TabIndex = 5;
            this.numMinSkill.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Location = new System.Drawing.Point(17, 184);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(44, 20);
            this.lblPrice.TabIndex = 6;
            this.lblPrice.Text = "Price";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(131, 99);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(191, 26);
            this.txtDescription.TabIndex = 7;
            // 
            // cboCondition
            // 
            this.cboCondition.FormattingEnabled = true;
            this.cboCondition.Items.AddRange(new object[] {
            "NEW",
            "GOOD",
            "FAIR",
            "POOR",
            "BROKEN"});
            this.cboCondition.Location = new System.Drawing.Point(131, 136);
            this.cboCondition.Name = "cboCondition";
            this.cboCondition.Size = new System.Drawing.Size(121, 28);
            this.cboCondition.TabIndex = 9;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(32, 328);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(93, 37);
            this.btnAdd.TabIndex = 10;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAddTool_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(131, 328);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(93, 37);
            this.btnUpdate.TabIndex = 11;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdateTool_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(230, 328);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(93, 37);
            this.btnDelete.TabIndex = 12;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.BtnDeleteTool_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(329, 328);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(93, 37);
            this.btnClose.TabIndex = 13;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // cboTools
            // 
            this.cboTools.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTools.FormattingEnabled = true;
            this.cboTools.Location = new System.Drawing.Point(173, 269);
            this.cboTools.Name = "cboTools";
            this.cboTools.Size = new System.Drawing.Size(159, 28);
            this.cboTools.TabIndex = 14;
            // 
            // lblCurrentInventory
            // 
            this.lblCurrentInventory.AutoSize = true;
            this.lblCurrentInventory.Location = new System.Drawing.Point(17, 272);
            this.lblCurrentInventory.Name = "lblCurrentInventory";
            this.lblCurrentInventory.Size = new System.Drawing.Size(131, 20);
            this.lblCurrentInventory.TabIndex = 15;
            this.lblCurrentInventory.Text = "Current Inventory";
            // 
            // lblD
            // 
            this.lblD.AutoSize = true;
            this.lblD.Location = new System.Drawing.Point(366, 272);
            this.lblD.Name = "lblD";
            this.lblD.Size = new System.Drawing.Size(26, 20);
            this.lblD.TabIndex = 16;
            this.lblD.Text = "ID";
            // 
            // txtEquipmentId
            // 
            this.txtEquipmentId.Location = new System.Drawing.Point(398, 270);
            this.txtEquipmentId.Name = "txtEquipmentId";
            this.txtEquipmentId.ReadOnly = true;
            this.txtEquipmentId.Size = new System.Drawing.Size(66, 26);
            this.txtEquipmentId.TabIndex = 17;
            // 
            // cboName
            // 
            this.cboName.FormattingEnabled = true;
            this.cboName.Location = new System.Drawing.Point(131, 24);
            this.cboName.Name = "cboName";
            this.cboName.Size = new System.Drawing.Size(121, 28);
            this.cboName.TabIndex = 18;
            // 
            // ManageInventoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(924, 527);
            this.Controls.Add(this.gbEditor);
            this.Name = "ManageInventoryForm";
            this.Text = "ManageInventoryForm";
            this.gbEditor.ResumeLayout(false);
            this.gbEditor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinSkill)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblMinSkill;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblCondition;
        private System.Windows.Forms.GroupBox gbEditor;
        private System.Windows.Forms.NumericUpDown numMinSkill;
        private System.Windows.Forms.NumericUpDown numPrice;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.ComboBox cboCondition;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ComboBox cboTools;
        private System.Windows.Forms.Label lblCurrentInventory;
        private System.Windows.Forms.TextBox txtEquipmentId;
        private System.Windows.Forms.Label lblD;
        private System.Windows.Forms.ComboBox cboName;
    }
}