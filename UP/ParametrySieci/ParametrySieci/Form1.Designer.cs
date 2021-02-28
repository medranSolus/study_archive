namespace WindowsFormsApp1
{
    partial class Form1
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
            this.labelVoltage = new System.Windows.Forms.Label();
            this.labelCurrent = new System.Windows.Forms.Label();
            this.labelActive = new System.Windows.Forms.Label();
            this.labelCosFi = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelVoltage
            // 
            this.labelVoltage.AutoSize = true;
            this.labelVoltage.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelVoltage.Location = new System.Drawing.Point(35, 22);
            this.labelVoltage.Name = "labelVoltage";
            this.labelVoltage.Size = new System.Drawing.Size(70, 25);
            this.labelVoltage.TabIndex = 1;
            this.labelVoltage.Text = "label1";
            // 
            // labelCurrent
            // 
            this.labelCurrent.AutoSize = true;
            this.labelCurrent.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelCurrent.Location = new System.Drawing.Point(37, 56);
            this.labelCurrent.Name = "labelCurrent";
            this.labelCurrent.Size = new System.Drawing.Size(70, 25);
            this.labelCurrent.TabIndex = 2;
            this.labelCurrent.Text = "label1";
            // 
            // labelActive
            // 
            this.labelActive.AutoSize = true;
            this.labelActive.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelActive.Location = new System.Drawing.Point(39, 92);
            this.labelActive.Name = "labelActive";
            this.labelActive.Size = new System.Drawing.Size(70, 25);
            this.labelActive.TabIndex = 3;
            this.labelActive.Text = "label1";
            // 
            // labelCosFi
            // 
            this.labelCosFi.AutoSize = true;
            this.labelCosFi.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelCosFi.Location = new System.Drawing.Point(41, 133);
            this.labelCosFi.Name = "labelCosFi";
            this.labelCosFi.Size = new System.Drawing.Size(70, 25);
            this.labelCosFi.TabIndex = 4;
            this.labelCosFi.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(302, 313);
            this.Controls.Add(this.labelCosFi);
            this.Controls.Add(this.labelActive);
            this.Controls.Add(this.labelCurrent);
            this.Controls.Add(this.labelVoltage);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelVoltage;
        private System.Windows.Forms.Label labelCurrent;
        private System.Windows.Forms.Label labelActive;
        private System.Windows.Forms.Label labelCosFi;
    }
}

