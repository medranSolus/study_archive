namespace MarekMachlinskiLab4Zad1
{
    partial class FormDowngrade
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.buttonAccept = new System.Windows.Forms.Button();
            this.dataGridViewUsers = new System.Windows.Forms.DataGridView();
            this.buttonCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUsers)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonAccept
            // 
            this.buttonAccept.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.buttonAccept.BackColor = System.Drawing.Color.Black;
            this.buttonAccept.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAccept.Font = new System.Drawing.Font("Matura MT Script Capitals", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAccept.ForeColor = System.Drawing.Color.Red;
            this.buttonAccept.Location = new System.Drawing.Point(268, 12);
            this.buttonAccept.Name = "buttonAccept";
            this.buttonAccept.Size = new System.Drawing.Size(168, 87);
            this.buttonAccept.TabIndex = 37;
            this.buttonAccept.Text = "Downgrade Dwarf";
            this.buttonAccept.UseVisualStyleBackColor = false;
            this.buttonAccept.Click += new System.EventHandler(this.buttonAccept_Click);
            // 
            // dataGridViewUsers
            // 
            this.dataGridViewUsers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewUsers.BackgroundColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Matura MT Script Capitals", 16F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Gold;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.DarkGoldenrod;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.LightGoldenrodYellow;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewUsers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Matura MT Script Capitals", 16F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Gold;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.DarkGoldenrod;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.LightGoldenrodYellow;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewUsers.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewUsers.GridColor = System.Drawing.Color.Gold;
            this.dataGridViewUsers.Location = new System.Drawing.Point(12, 12);
            this.dataGridViewUsers.Name = "dataGridViewUsers";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Matura MT Script Capitals", 16F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Gold;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.DarkGoldenrod;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.LightGoldenrodYellow;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewUsers.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewUsers.Size = new System.Drawing.Size(250, 412);
            this.dataGridViewUsers.TabIndex = 36;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.buttonCancel.BackColor = System.Drawing.Color.Black;
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancel.Font = new System.Drawing.Font("Matura MT Script Capitals", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCancel.ForeColor = System.Drawing.Color.Silver;
            this.buttonCancel.Location = new System.Drawing.Point(268, 105);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(108, 50);
            this.buttonCancel.TabIndex = 35;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // FormDowngrade
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::MarekMachlinskiLab4Zad1.Properties.Resources.DialogBackground;
            this.ClientSize = new System.Drawing.Size(448, 436);
            this.Controls.Add(this.buttonAccept);
            this.Controls.Add(this.dataGridViewUsers);
            this.Controls.Add(this.buttonCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDowngrade";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Downgrade";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUsers)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonAccept;
        private System.Windows.Forms.DataGridView dataGridViewUsers;
        private System.Windows.Forms.Button buttonCancel;
    }
}