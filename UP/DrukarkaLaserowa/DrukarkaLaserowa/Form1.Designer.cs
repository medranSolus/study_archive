namespace DrukarkaLaserowa
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
            this.textBoxInpuput = new System.Windows.Forms.TextBox();
            this.buttonPrint = new System.Windows.Forms.Button();
            this.checkBoxBold = new System.Windows.Forms.CheckBox();
            this.checkBoxUnderline = new System.Windows.Forms.CheckBox();
            this.numericUpDownMarginTop = new System.Windows.Forms.NumericUpDown();
            this.Margin = new System.Windows.Forms.Label();
            this.buttonPrintImg = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMarginTop)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxInpuput
            // 
            this.textBoxInpuput.Location = new System.Drawing.Point(99, 72);
            this.textBoxInpuput.Name = "textBoxInpuput";
            this.textBoxInpuput.Size = new System.Drawing.Size(228, 26);
            this.textBoxInpuput.TabIndex = 0;
            // 
            // buttonPrint
            // 
            this.buttonPrint.Location = new System.Drawing.Point(99, 136);
            this.buttonPrint.Name = "buttonPrint";
            this.buttonPrint.Size = new System.Drawing.Size(228, 47);
            this.buttonPrint.TabIndex = 1;
            this.buttonPrint.Text = "Print";
            this.buttonPrint.UseVisualStyleBackColor = true;
            this.buttonPrint.Click += new System.EventHandler(this.buttonPrint_Click);
            // 
            // checkBoxBold
            // 
            this.checkBoxBold.AutoSize = true;
            this.checkBoxBold.Location = new System.Drawing.Point(99, 106);
            this.checkBoxBold.Name = "checkBoxBold";
            this.checkBoxBold.Size = new System.Drawing.Size(67, 24);
            this.checkBoxBold.TabIndex = 2;
            this.checkBoxBold.Text = "Bold";
            this.checkBoxBold.UseVisualStyleBackColor = true;
            // 
            // checkBoxUnderline
            // 
            this.checkBoxUnderline.AutoSize = true;
            this.checkBoxUnderline.Location = new System.Drawing.Point(172, 106);
            this.checkBoxUnderline.Name = "checkBoxUnderline";
            this.checkBoxUnderline.Size = new System.Drawing.Size(103, 24);
            this.checkBoxUnderline.TabIndex = 3;
            this.checkBoxUnderline.Text = "Underline";
            this.checkBoxUnderline.UseVisualStyleBackColor = true;
            // 
            // numericUpDownMarginTop
            // 
            this.numericUpDownMarginTop.Location = new System.Drawing.Point(431, 110);
            this.numericUpDownMarginTop.Name = "numericUpDownMarginTop";
            this.numericUpDownMarginTop.Size = new System.Drawing.Size(120, 26);
            this.numericUpDownMarginTop.TabIndex = 4;
            // 
            // Margin
            // 
            this.Margin.AutoSize = true;
            this.Margin.Location = new System.Drawing.Point(427, 87);
            this.Margin.Name = "Margin";
            this.Margin.Size = new System.Drawing.Size(88, 20);
            this.Margin.TabIndex = 5;
            this.Margin.Text = "Margin Top";
            // 
            // buttonPrintImg
            // 
            this.buttonPrintImg.Location = new System.Drawing.Point(99, 189);
            this.buttonPrintImg.Name = "buttonPrintImg";
            this.buttonPrintImg.Size = new System.Drawing.Size(133, 48);
            this.buttonPrintImg.TabIndex = 6;
            this.buttonPrintImg.Text = "Print img";
            this.buttonPrintImg.UseVisualStyleBackColor = true;
            this.buttonPrintImg.Click += new System.EventHandler(this.buttonPrintImg_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonPrintImg);
            this.Controls.Add(this.Margin);
            this.Controls.Add(this.numericUpDownMarginTop);
            this.Controls.Add(this.checkBoxUnderline);
            this.Controls.Add(this.checkBoxBold);
            this.Controls.Add(this.buttonPrint);
            this.Controls.Add(this.textBoxInpuput);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMarginTop)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxInpuput;
        private System.Windows.Forms.Button buttonPrint;
        private System.Windows.Forms.CheckBox checkBoxBold;
        private System.Windows.Forms.CheckBox checkBoxUnderline;
        private System.Windows.Forms.NumericUpDown numericUpDownMarginTop;
        private System.Windows.Forms.Label Margin;
        private System.Windows.Forms.Button buttonPrintImg;
    }
}

