namespace MarekMachlinskiLab3Zad1
{
    partial class FormAddEvent
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAddEvent));
            this.buttonAccept = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelMeetings = new System.Windows.Forms.Label();
            this.labelContacts = new System.Windows.Forms.Label();
            this.dataGridViewMeetings = new System.Windows.Forms.DataGridView();
            this.dataGridViewContacts = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMeetings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewContacts)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonAccept
            // 
            this.buttonAccept.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonAccept.Location = new System.Drawing.Point(296, 289);
            this.buttonAccept.Name = "buttonAccept";
            this.buttonAccept.Size = new System.Drawing.Size(73, 31);
            this.buttonAccept.TabIndex = 7;
            this.buttonAccept.Text = "Accept";
            this.buttonAccept.UseVisualStyleBackColor = true;
            this.buttonAccept.Click += new System.EventHandler(this.buttonAccept_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonCancel.Location = new System.Drawing.Point(375, 289);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(77, 31);
            this.buttonCancel.TabIndex = 8;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // labelMeetings
            // 
            this.labelMeetings.AutoSize = true;
            this.labelMeetings.Font = new System.Drawing.Font("Monotype Corsiva", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelMeetings.ForeColor = System.Drawing.Color.White;
            this.labelMeetings.Location = new System.Drawing.Point(12, 9);
            this.labelMeetings.Name = "labelMeetings";
            this.labelMeetings.Size = new System.Drawing.Size(124, 22);
            this.labelMeetings.TabIndex = 9;
            this.labelMeetings.Text = "Choose Meeting:";
            // 
            // labelContacts
            // 
            this.labelContacts.AutoSize = true;
            this.labelContacts.Font = new System.Drawing.Font("Monotype Corsiva", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelContacts.ForeColor = System.Drawing.Color.White;
            this.labelContacts.Location = new System.Drawing.Point(233, 9);
            this.labelContacts.Name = "labelContacts";
            this.labelContacts.Size = new System.Drawing.Size(120, 22);
            this.labelContacts.TabIndex = 10;
            this.labelContacts.Text = "Choose Contact:";
            // 
            // dataGridViewMeetings
            // 
            this.dataGridViewMeetings.AllowUserToAddRows = false;
            this.dataGridViewMeetings.AllowUserToDeleteRows = false;
            this.dataGridViewMeetings.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewMeetings.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dataGridViewMeetings.BackgroundColor = System.Drawing.Color.SaddleBrown;
            this.dataGridViewMeetings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMeetings.GridColor = System.Drawing.Color.Goldenrod;
            this.dataGridViewMeetings.Location = new System.Drawing.Point(16, 48);
            this.dataGridViewMeetings.Name = "dataGridViewMeetings";
            this.dataGridViewMeetings.ReadOnly = true;
            this.dataGridViewMeetings.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.SaddleBrown;
            this.dataGridViewMeetings.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Monotype Corsiva", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dataGridViewMeetings.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dataGridViewMeetings.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.AntiqueWhite;
            this.dataGridViewMeetings.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridViewMeetings.Size = new System.Drawing.Size(215, 235);
            this.dataGridViewMeetings.TabIndex = 11;
            // 
            // dataGridViewContacts
            // 
            this.dataGridViewContacts.AllowUserToAddRows = false;
            this.dataGridViewContacts.AllowUserToDeleteRows = false;
            this.dataGridViewContacts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewContacts.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dataGridViewContacts.BackgroundColor = System.Drawing.Color.SaddleBrown;
            this.dataGridViewContacts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewContacts.GridColor = System.Drawing.Color.Goldenrod;
            this.dataGridViewContacts.Location = new System.Drawing.Point(237, 48);
            this.dataGridViewContacts.Name = "dataGridViewContacts";
            this.dataGridViewContacts.ReadOnly = true;
            this.dataGridViewContacts.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.SaddleBrown;
            this.dataGridViewContacts.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Monotype Corsiva", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dataGridViewContacts.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dataGridViewContacts.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.AntiqueWhite;
            this.dataGridViewContacts.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridViewContacts.Size = new System.Drawing.Size(215, 235);
            this.dataGridViewContacts.TabIndex = 12;
            // 
            // FormAddEvent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SaddleBrown;
            this.ClientSize = new System.Drawing.Size(464, 332);
            this.Controls.Add(this.dataGridViewContacts);
            this.Controls.Add(this.dataGridViewMeetings);
            this.Controls.Add(this.labelContacts);
            this.Controls.Add(this.labelMeetings);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonAccept);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormAddEvent";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Event";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMeetings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewContacts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonAccept;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelMeetings;
        private System.Windows.Forms.Label labelContacts;
        private System.Windows.Forms.DataGridView dataGridViewMeetings;
        private System.Windows.Forms.DataGridView dataGridViewContacts;
    }
}