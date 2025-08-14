namespace Team5_Final
{
    partial class MainForm
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
            this.cbEmployee = new System.Windows.Forms.ComboBox();
            this.cbEquipment = new System.Windows.Forms.ComboBox();
            this.btnCheckout = new System.Windows.Forms.Button();
            this.btnReturn = new System.Windows.Forms.Button();
            this.dgvActive = new System.Windows.Forms.DataGridView();
            this.cEIS400Team5DBDataSet = new Team5_Final.CEIS400Team5DBDataSet();
            this.cEIS400Team5DBDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvActive)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cEIS400Team5DBDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cEIS400Team5DBDataSetBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // cbEmployee
            // 
            this.cbEmployee.DropDownHeight = 220;
            this.cbEmployee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEmployee.FormattingEnabled = true;
            this.cbEmployee.IntegralHeight = false;
            this.cbEmployee.Location = new System.Drawing.Point(30, 21);
            this.cbEmployee.Name = "cbEmployee";
            this.cbEmployee.Size = new System.Drawing.Size(121, 21);
            this.cbEmployee.TabIndex = 0;
            // 
            // cbEquipment
            // 
            this.cbEquipment.DropDownHeight = 220;
            this.cbEquipment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEquipment.FormattingEnabled = true;
            this.cbEquipment.IntegralHeight = false;
            this.cbEquipment.Location = new System.Drawing.Point(200, 21);
            this.cbEquipment.Name = "cbEquipment";
            this.cbEquipment.Size = new System.Drawing.Size(121, 21);
            this.cbEquipment.TabIndex = 1;
            this.cbEquipment.SelectedIndexChanged += new System.EventHandler(this.cbEquipment_SelectedIndexChanged);
            // 
            // btnCheckout
            // 
            this.btnCheckout.Location = new System.Drawing.Point(360, 21);
            this.btnCheckout.Name = "btnCheckout";
            this.btnCheckout.Size = new System.Drawing.Size(105, 23);
            this.btnCheckout.TabIndex = 2;
            this.btnCheckout.Text = "Checkout";
            this.btnCheckout.UseVisualStyleBackColor = true;
            // 
            // btnReturn
            // 
            this.btnReturn.Location = new System.Drawing.Point(523, 20);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(110, 23);
            this.btnReturn.TabIndex = 3;
            this.btnReturn.Text = "Return Selected";
            this.btnReturn.UseVisualStyleBackColor = true;
            // 
            // dgvActive
            // 
            this.dgvActive.AllowUserToAddRows = false;
            this.dgvActive.AutoGenerateColumns = false;
            this.dgvActive.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvActive.DataSource = this.cEIS400Team5DBDataSetBindingSource;
            this.dgvActive.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvActive.Location = new System.Drawing.Point(0, 300);
            this.dgvActive.Name = "dgvActive";
            this.dgvActive.ReadOnly = true;
            this.dgvActive.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvActive.Size = new System.Drawing.Size(800, 150);
            this.dgvActive.TabIndex = 4;
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgvActive);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.btnCheckout);
            this.Controls.Add(this.cbEquipment);
            this.Controls.Add(this.cbEmployee);
            this.Name = "MainForm";
            this.Text = "Main Form";
            ((System.ComponentModel.ISupportInitialize)(this.dgvActive)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cEIS400Team5DBDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cEIS400Team5DBDataSetBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbEmployee;
        private System.Windows.Forms.ComboBox cbEquipment;
        private System.Windows.Forms.Button btnCheckout;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.DataGridView dgvActive;
        private System.Windows.Forms.BindingSource cEIS400Team5DBDataSetBindingSource;
        private CEIS400Team5DBDataSet cEIS400Team5DBDataSet;
    }
}

