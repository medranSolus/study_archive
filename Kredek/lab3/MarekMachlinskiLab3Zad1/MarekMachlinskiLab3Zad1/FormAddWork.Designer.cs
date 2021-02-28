namespace MarekMachlinskiLab3Zad1
{
    partial class FormAddWork
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAddWork));
            this.dataGridViewProcesses = new System.Windows.Forms.DataGridView();
            this.dataGridViewTasks = new System.Windows.Forms.DataGridView();
            this.buttonAccept = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelInstruction = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProcesses)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTasks)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewProcesses
            // 
            this.dataGridViewProcesses.AllowUserToAddRows = false;
            this.dataGridViewProcesses.AllowUserToDeleteRows = false;
            this.dataGridViewProcesses.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewProcesses.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dataGridViewProcesses.BackgroundColor = System.Drawing.Color.SaddleBrown;
            this.dataGridViewProcesses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewProcesses.GridColor = System.Drawing.Color.Goldenrod;
            this.dataGridViewProcesses.Location = new System.Drawing.Point(12, 34);
            this.dataGridViewProcesses.Name = "dataGridViewProcesses";
            this.dataGridViewProcesses.ReadOnly = true;
            this.dataGridViewProcesses.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.SaddleBrown;
            this.dataGridViewProcesses.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Monotype Corsiva", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dataGridViewProcesses.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dataGridViewProcesses.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.AntiqueWhite;
            this.dataGridViewProcesses.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridViewProcesses.Size = new System.Drawing.Size(215, 235);
            this.dataGridViewProcesses.TabIndex = 12;
            // 
            // dataGridViewTasks
            // 
            this.dataGridViewTasks.AllowUserToAddRows = false;
            this.dataGridViewTasks.AllowUserToDeleteRows = false;
            this.dataGridViewTasks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewTasks.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dataGridViewTasks.BackgroundColor = System.Drawing.Color.SaddleBrown;
            this.dataGridViewTasks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTasks.GridColor = System.Drawing.Color.Goldenrod;
            this.dataGridViewTasks.Location = new System.Drawing.Point(233, 34);
            this.dataGridViewTasks.Name = "dataGridViewTasks";
            this.dataGridViewTasks.ReadOnly = true;
            this.dataGridViewTasks.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.SaddleBrown;
            this.dataGridViewTasks.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Monotype Corsiva", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dataGridViewTasks.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dataGridViewTasks.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.AntiqueWhite;
            this.dataGridViewTasks.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridViewTasks.Size = new System.Drawing.Size(215, 235);
            this.dataGridViewTasks.TabIndex = 13;
            // 
            // buttonAccept
            // 
            this.buttonAccept.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonAccept.Location = new System.Drawing.Point(292, 275);
            this.buttonAccept.Name = "buttonAccept";
            this.buttonAccept.Size = new System.Drawing.Size(73, 31);
            this.buttonAccept.TabIndex = 14;
            this.buttonAccept.Text = "Accept";
            this.buttonAccept.UseVisualStyleBackColor = true;
            this.buttonAccept.Click += new System.EventHandler(this.buttonAccept_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonCancel.Location = new System.Drawing.Point(371, 275);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(77, 31);
            this.buttonCancel.TabIndex = 15;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // labelInstruction
            // 
            this.labelInstruction.AutoSize = true;
            this.labelInstruction.Font = new System.Drawing.Font("Monotype Corsiva", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelInstruction.ForeColor = System.Drawing.Color.White;
            this.labelInstruction.Location = new System.Drawing.Point(75, 9);
            this.labelInstruction.Name = "labelInstruction";
            this.labelInstruction.Size = new System.Drawing.Size(289, 22);
            this.labelInstruction.TabIndex = 16;
            this.labelInstruction.Text = "Choose process connected with your task:";
            // 
            // FormAddWork
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SaddleBrown;
            this.ClientSize = new System.Drawing.Size(460, 315);
            this.Controls.Add(this.labelInstruction);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonAccept);
            this.Controls.Add(this.dataGridViewTasks);
            this.Controls.Add(this.dataGridViewProcesses);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormAddWork";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Work";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProcesses)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTasks)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewProcesses;
        private System.Windows.Forms.DataGridView dataGridViewTasks;
        private System.Windows.Forms.Button buttonAccept;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelInstruction;
    }
}