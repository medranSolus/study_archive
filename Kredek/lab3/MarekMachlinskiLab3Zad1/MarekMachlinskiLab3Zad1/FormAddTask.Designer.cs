namespace MarekMachlinskiLab3Zad1
{
    partial class FormAddTask
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAddTask));
            this.buttonAccept = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelName = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.monthCalendarDeadline = new System.Windows.Forms.MonthCalendar();
            this.labelDeadline = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonAccept
            // 
            this.buttonAccept.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonAccept.Location = new System.Drawing.Point(16, 122);
            this.buttonAccept.Name = "buttonAccept";
            this.buttonAccept.Size = new System.Drawing.Size(73, 31);
            this.buttonAccept.TabIndex = 8;
            this.buttonAccept.Text = "Accept";
            this.buttonAccept.UseVisualStyleBackColor = true;
            this.buttonAccept.Click += new System.EventHandler(this.buttonAccept_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonCancel.Location = new System.Drawing.Point(16, 176);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(73, 31);
            this.buttonCancel.TabIndex = 9;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Font = new System.Drawing.Font("Monotype Corsiva", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelName.ForeColor = System.Drawing.Color.White;
            this.labelName.Location = new System.Drawing.Point(44, 8);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(55, 22);
            this.labelName.TabIndex = 10;
            this.labelName.Text = "Name:";
            // 
            // textBoxName
            // 
            this.textBoxName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxName.Location = new System.Drawing.Point(105, 12);
            this.textBoxName.MaxLength = 31;
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(166, 20);
            this.textBoxName.TabIndex = 11;
            // 
            // monthCalendarDeadline
            // 
            this.monthCalendarDeadline.Location = new System.Drawing.Point(101, 45);
            this.monthCalendarDeadline.MaxDate = new System.DateTime(2200, 12, 31, 0, 0, 0, 0);
            this.monthCalendarDeadline.MaxSelectionCount = 1;
            this.monthCalendarDeadline.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.monthCalendarDeadline.Name = "monthCalendarDeadline";
            this.monthCalendarDeadline.TabIndex = 12;
            // 
            // labelDeadline
            // 
            this.labelDeadline.AutoSize = true;
            this.labelDeadline.Font = new System.Drawing.Font("Monotype Corsiva", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelDeadline.ForeColor = System.Drawing.Color.White;
            this.labelDeadline.Location = new System.Drawing.Point(22, 45);
            this.labelDeadline.Name = "labelDeadline";
            this.labelDeadline.Size = new System.Drawing.Size(77, 22);
            this.labelDeadline.TabIndex = 13;
            this.labelDeadline.Text = "Deadline:";
            // 
            // FormAddTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SaddleBrown;
            this.ClientSize = new System.Drawing.Size(389, 223);
            this.Controls.Add(this.labelDeadline);
            this.Controls.Add(this.monthCalendarDeadline);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonAccept);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormAddTask";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Task";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonAccept;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.MonthCalendar monthCalendarDeadline;
        private System.Windows.Forms.Label labelDeadline;
    }
}