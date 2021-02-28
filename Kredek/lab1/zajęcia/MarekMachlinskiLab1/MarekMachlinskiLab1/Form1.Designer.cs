namespace MarekMachlinskiLab1
{
    partial class Form1
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.labelName = new System.Windows.Forms.Label();
            this.buttonChangeColor = new System.Windows.Forms.Button();
            this.textBoxNumber1 = new System.Windows.Forms.TextBox();
            this.buttonShow = new System.Windows.Forms.Button();
            this.labelPlus = new System.Windows.Forms.Label();
            this.textBoxNumber2 = new System.Windows.Forms.TextBox();
            this.labelEquals = new System.Windows.Forms.Label();
            this.timerCount = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.labelName.Font = new System.Drawing.Font("Arial Narrow", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelName.Location = new System.Drawing.Point(12, 9);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(166, 29);
            this.labelName.TabIndex = 0;
            this.labelName.Text = "Marek Machliński";
            this.labelName.Click += new System.EventHandler(this.labelName_Click);
            // 
            // buttonChangeColor
            // 
            this.buttonChangeColor.Location = new System.Drawing.Point(12, 78);
            this.buttonChangeColor.Name = "buttonChangeColor";
            this.buttonChangeColor.Size = new System.Drawing.Size(136, 46);
            this.buttonChangeColor.TabIndex = 1;
            this.buttonChangeColor.Text = "Change Color";
            this.buttonChangeColor.UseVisualStyleBackColor = true;
            this.buttonChangeColor.Click += new System.EventHandler(this.buttonChangeColor_Click);
            // 
            // textBoxNumber1
            // 
            this.textBoxNumber1.Location = new System.Drawing.Point(154, 92);
            this.textBoxNumber1.Name = "textBoxNumber1";
            this.textBoxNumber1.Size = new System.Drawing.Size(73, 20);
            this.textBoxNumber1.TabIndex = 2;
            // 
            // buttonShow
            // 
            this.buttonShow.Location = new System.Drawing.Point(357, 79);
            this.buttonShow.Name = "buttonShow";
            this.buttonShow.Size = new System.Drawing.Size(136, 45);
            this.buttonShow.TabIndex = 3;
            this.buttonShow.Text = "Show";
            this.buttonShow.UseVisualStyleBackColor = true;
            this.buttonShow.Click += new System.EventHandler(this.buttonShow_Click);
            // 
            // labelPlus
            // 
            this.labelPlus.AutoSize = true;
            this.labelPlus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelPlus.Location = new System.Drawing.Point(233, 93);
            this.labelPlus.Name = "labelPlus";
            this.labelPlus.Size = new System.Drawing.Size(16, 17);
            this.labelPlus.TabIndex = 4;
            this.labelPlus.Text = "+";
            // 
            // textBoxNumber2
            // 
            this.textBoxNumber2.Location = new System.Drawing.Point(255, 92);
            this.textBoxNumber2.Name = "textBoxNumber2";
            this.textBoxNumber2.Size = new System.Drawing.Size(74, 20);
            this.textBoxNumber2.TabIndex = 5;
            // 
            // labelEquals
            // 
            this.labelEquals.AutoSize = true;
            this.labelEquals.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelEquals.Location = new System.Drawing.Point(335, 93);
            this.labelEquals.Name = "labelEquals";
            this.labelEquals.Size = new System.Drawing.Size(16, 17);
            this.labelEquals.TabIndex = 6;
            this.labelEquals.Text = "=";
            // 
            // timerCount
            // 
            this.timerCount.Interval = 1;
            this.timerCount.Tick += new System.EventHandler(this.timerCount_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(503, 141);
            this.Controls.Add(this.labelEquals);
            this.Controls.Add(this.textBoxNumber2);
            this.Controls.Add(this.labelPlus);
            this.Controls.Add(this.buttonShow);
            this.Controls.Add(this.textBoxNumber1);
            this.Controls.Add(this.buttonChangeColor);
            this.Controls.Add(this.labelName);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Button buttonChangeColor;
        private System.Windows.Forms.TextBox textBoxNumber1;
        private System.Windows.Forms.Button buttonShow;
        private System.Windows.Forms.Label labelPlus;
        private System.Windows.Forms.TextBox textBoxNumber2;
        private System.Windows.Forms.Label labelEquals;
        private System.Windows.Forms.Timer timerCount;
    }
}

