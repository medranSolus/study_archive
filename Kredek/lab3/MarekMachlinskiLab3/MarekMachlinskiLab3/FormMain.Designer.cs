namespace MarekMachlinskiLab3
{
    partial class FormMain
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
            this.labelName = new System.Windows.Forms.Label();
            this.dataGridViewPizzas = new System.Windows.Forms.DataGridView();
            this.buttonShow = new System.Windows.Forms.Button();
            this.textBoxQuery = new System.Windows.Forms.TextBox();
            this.buttonShowQuery = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPizzas)).BeginInit();
            this.SuspendLayout();
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelName.Location = new System.Drawing.Point(12, 9);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(223, 31);
            this.labelName.TabIndex = 0;
            this.labelName.Text = "Marek Machliński";
            // 
            // dataGridViewPizzas
            // 
            this.dataGridViewPizzas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPizzas.Location = new System.Drawing.Point(18, 53);
            this.dataGridViewPizzas.Name = "dataGridViewPizzas";
            this.dataGridViewPizzas.Size = new System.Drawing.Size(580, 385);
            this.dataGridViewPizzas.TabIndex = 1;
            // 
            // buttonShow
            // 
            this.buttonShow.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonShow.Location = new System.Drawing.Point(604, 53);
            this.buttonShow.Name = "buttonShow";
            this.buttonShow.Size = new System.Drawing.Size(184, 42);
            this.buttonShow.TabIndex = 2;
            this.buttonShow.Text = "Show";
            this.buttonShow.UseVisualStyleBackColor = true;
            this.buttonShow.Click += new System.EventHandler(this.buttonShow_Click);
            // 
            // textBoxQuery
            // 
            this.textBoxQuery.Location = new System.Drawing.Point(604, 273);
            this.textBoxQuery.Name = "textBoxQuery";
            this.textBoxQuery.Size = new System.Drawing.Size(184, 20);
            this.textBoxQuery.TabIndex = 3;
            // 
            // buttonShowQuery
            // 
            this.buttonShowQuery.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonShowQuery.Location = new System.Drawing.Point(604, 214);
            this.buttonShowQuery.Name = "buttonShowQuery";
            this.buttonShowQuery.Size = new System.Drawing.Size(184, 53);
            this.buttonShowQuery.TabIndex = 4;
            this.buttonShowQuery.Text = "Show where price is higher than";
            this.buttonShowQuery.UseVisualStyleBackColor = true;
            this.buttonShowQuery.Click += new System.EventHandler(this.buttonShowQuery_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonShowQuery);
            this.Controls.Add(this.textBoxQuery);
            this.Controls.Add(this.buttonShow);
            this.Controls.Add(this.dataGridViewPizzas);
            this.Controls.Add(this.labelName);
            this.Name = "FormMain";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPizzas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.DataGridView dataGridViewPizzas;
        private System.Windows.Forms.Button buttonShow;
        private System.Windows.Forms.TextBox textBoxQuery;
        private System.Windows.Forms.Button buttonShowQuery;
    }
}

