namespace MarekMachlinskiLab3Zad1
{
    partial class FormAddContact
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAddContact));
            this.labelName = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.labelBorn = new System.Windows.Forms.Label();
            this.monthCalendarBornPicker = new System.Windows.Forms.MonthCalendar();
            this.labelNumber = new System.Windows.Forms.Label();
            this.textBoxNumber = new System.Windows.Forms.TextBox();
            this.buttonAccept = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Font = new System.Drawing.Font("Monotype Corsiva", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelName.ForeColor = System.Drawing.Color.White;
            this.labelName.Location = new System.Drawing.Point(12, 9);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(55, 22);
            this.labelName.TabIndex = 0;
            this.labelName.Text = "Name:";
            // 
            // textBoxName
            // 
            this.textBoxName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxName.Location = new System.Drawing.Point(73, 12);
            this.textBoxName.MaxLength = 31;
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(166, 20);
            this.textBoxName.TabIndex = 1;
            // 
            // labelBorn
            // 
            this.labelBorn.AutoSize = true;
            this.labelBorn.Font = new System.Drawing.Font("Monotype Corsiva", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelBorn.ForeColor = System.Drawing.Color.White;
            this.labelBorn.Location = new System.Drawing.Point(12, 45);
            this.labelBorn.Name = "labelBorn";
            this.labelBorn.Size = new System.Drawing.Size(49, 22);
            this.labelBorn.TabIndex = 2;
            this.labelBorn.Text = "Born:";
            // 
            // monthCalendarBornPicker
            // 
            this.monthCalendarBornPicker.Location = new System.Drawing.Point(73, 45);
            this.monthCalendarBornPicker.MaxDate = new System.DateTime(2018, 4, 22, 0, 0, 0, 0);
            this.monthCalendarBornPicker.MaxSelectionCount = 1;
            this.monthCalendarBornPicker.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.monthCalendarBornPicker.Name = "monthCalendarBornPicker";
            this.monthCalendarBornPicker.TabIndex = 3;
            // 
            // labelNumber
            // 
            this.labelNumber.AutoSize = true;
            this.labelNumber.Font = new System.Drawing.Font("Monotype Corsiva", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelNumber.ForeColor = System.Drawing.Color.White;
            this.labelNumber.Location = new System.Drawing.Point(245, 12);
            this.labelNumber.Name = "labelNumber";
            this.labelNumber.Size = new System.Drawing.Size(116, 22);
            this.labelNumber.TabIndex = 4;
            this.labelNumber.Text = "Phone Number:";
            // 
            // textBoxNumber
            // 
            this.textBoxNumber.Location = new System.Drawing.Point(367, 11);
            this.textBoxNumber.MaxLength = 9;
            this.textBoxNumber.Name = "textBoxNumber";
            this.textBoxNumber.Size = new System.Drawing.Size(143, 20);
            this.textBoxNumber.TabIndex = 5;
            this.textBoxNumber.TextChanged += new System.EventHandler(this.textBoxNumber_TextChanged);
            // 
            // buttonAccept
            // 
            this.buttonAccept.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonAccept.Location = new System.Drawing.Point(354, 176);
            this.buttonAccept.Name = "buttonAccept";
            this.buttonAccept.Size = new System.Drawing.Size(73, 31);
            this.buttonAccept.TabIndex = 6;
            this.buttonAccept.Text = "Accept";
            this.buttonAccept.UseVisualStyleBackColor = true;
            this.buttonAccept.Click += new System.EventHandler(this.buttonAccept_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonCancel.Location = new System.Drawing.Point(433, 176);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(77, 31);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // FormAddContact
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SaddleBrown;
            this.ClientSize = new System.Drawing.Size(523, 226);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonAccept);
            this.Controls.Add(this.textBoxNumber);
            this.Controls.Add(this.labelNumber);
            this.Controls.Add(this.monthCalendarBornPicker);
            this.Controls.Add(this.labelBorn);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.labelName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAddContact";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Contact";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label labelBorn;
        private System.Windows.Forms.MonthCalendar monthCalendarBornPicker;
        private System.Windows.Forms.Label labelNumber;
        private System.Windows.Forms.TextBox textBoxNumber;
        private System.Windows.Forms.Button buttonAccept;
        private System.Windows.Forms.Button buttonCancel;
    }
}