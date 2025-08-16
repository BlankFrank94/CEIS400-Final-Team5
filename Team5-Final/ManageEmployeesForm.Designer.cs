namespace Team5_Final
{
    partial class ManageEmployeesForm
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
            this.dgvEmployees = new System.Windows.Forms.DataGridView();
            this.btnAdmin = new System.Windows.Forms.Button();
            this.btnRevoke = new System.Windows.Forms.Button();
            this.btnTerminate = new System.Windows.Forms.Button();
            this.btnRestore = new System.Windows.Forms.Button();
            this.btnAddEmployee = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployees)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvEmployees
            // 
            this.dgvEmployees.AllowUserToAddRows = false;
            this.dgvEmployees.AllowUserToDeleteRows = false;
            this.dgvEmployees.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvEmployees.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEmployees.Dock = System.Windows.Forms.DockStyle.Left;
            this.dgvEmployees.Location = new System.Drawing.Point(0, 0);
            this.dgvEmployees.Name = "dgvEmployees";
            this.dgvEmployees.ReadOnly = true;
            this.dgvEmployees.RowHeadersVisible = false;
            this.dgvEmployees.RowHeadersWidth = 62;
            this.dgvEmployees.RowTemplate.Height = 28;
            this.dgvEmployees.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEmployees.Size = new System.Drawing.Size(593, 521);
            this.dgvEmployees.TabIndex = 0;
            // 
            // btnAdmin
            // 
            this.btnAdmin.Location = new System.Drawing.Point(629, 127);
            this.btnAdmin.Name = "btnAdmin";
            this.btnAdmin.Size = new System.Drawing.Size(158, 33);
            this.btnAdmin.TabIndex = 1;
            this.btnAdmin.Text = "Make Admin";
            this.btnAdmin.UseVisualStyleBackColor = true;
            this.btnAdmin.Click += new System.EventHandler(this.btnAdmin_Click);
            // 
            // btnRevoke
            // 
            this.btnRevoke.Location = new System.Drawing.Point(629, 166);
            this.btnRevoke.Name = "btnRevoke";
            this.btnRevoke.Size = new System.Drawing.Size(158, 33);
            this.btnRevoke.TabIndex = 2;
            this.btnRevoke.Text = "Revoke Admin";
            this.btnRevoke.UseVisualStyleBackColor = true;
            this.btnRevoke.Click += new System.EventHandler(this.btnRevoke_Click);
            // 
            // btnTerminate
            // 
            this.btnTerminate.Location = new System.Drawing.Point(629, 205);
            this.btnTerminate.Name = "btnTerminate";
            this.btnTerminate.Size = new System.Drawing.Size(158, 33);
            this.btnTerminate.TabIndex = 3;
            this.btnTerminate.Text = "Terminate";
            this.btnTerminate.UseVisualStyleBackColor = true;
            this.btnTerminate.Click += new System.EventHandler(this.btnTerminate_Click);
            // 
            // btnRestore
            // 
            this.btnRestore.Location = new System.Drawing.Point(629, 244);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(158, 33);
            this.btnRestore.TabIndex = 4;
            this.btnRestore.Text = "Restore";
            this.btnRestore.UseVisualStyleBackColor = true;
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // btnAddEmployee
            // 
            this.btnAddEmployee.Location = new System.Drawing.Point(629, 283);
            this.btnAddEmployee.Name = "btnAddEmployee";
            this.btnAddEmployee.Size = new System.Drawing.Size(158, 33);
            this.btnAddEmployee.TabIndex = 5;
            this.btnAddEmployee.Text = "Add Employee";
            this.btnAddEmployee.UseVisualStyleBackColor = true;
            this.btnAddEmployee.Click += new System.EventHandler(this.btnAddEmployee_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(629, 322);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(158, 33);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ManageEmployeesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(872, 521);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnAddEmployee);
            this.Controls.Add(this.btnRestore);
            this.Controls.Add(this.btnTerminate);
            this.Controls.Add(this.btnRevoke);
            this.Controls.Add(this.btnAdmin);
            this.Controls.Add(this.dgvEmployees);
            this.Name = "ManageEmployeesForm";
            this.Text = "ManageEmployeesForm";
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployees)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvEmployees;
        private System.Windows.Forms.Button btnAdmin;
        private System.Windows.Forms.Button btnRevoke;
        private System.Windows.Forms.Button btnTerminate;
        private System.Windows.Forms.Button btnRestore;
        private System.Windows.Forms.Button btnAddEmployee;
        private System.Windows.Forms.Button btnClose;
    }
}