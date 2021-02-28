namespace MarekMachlinskiLab4
{
    partial class FormMainWindow
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
            this.dataGridViewGames = new System.Windows.Forms.DataGridView();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.labelGames = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.labelName = new System.Windows.Forms.Label();
            this.labelProducent = new System.Windows.Forms.Label();
            this.textBoxProducent = new System.Windows.Forms.TextBox();
            this.buttonAdd = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewGames)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewGames
            // 
            this.dataGridViewGames.AllowUserToAddRows = false;
            this.dataGridViewGames.AllowUserToDeleteRows = false;
            this.dataGridViewGames.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewGames.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewGames.Location = new System.Drawing.Point(12, 56);
            this.dataGridViewGames.Name = "dataGridViewGames";
            this.dataGridViewGames.ReadOnly = true;
            this.dataGridViewGames.Size = new System.Drawing.Size(552, 382);
            this.dataGridViewGames.TabIndex = 0;
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Location = new System.Drawing.Point(575, 174);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(213, 32);
            this.buttonRefresh.TabIndex = 1;
            this.buttonRefresh.Text = "Refresh";
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // labelGames
            // 
            this.labelGames.AutoSize = true;
            this.labelGames.Font = new System.Drawing.Font("Modern No. 20", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelGames.Location = new System.Drawing.Point(12, 24);
            this.labelGames.Name = "labelGames";
            this.labelGames.Size = new System.Drawing.Size(85, 29);
            this.labelGames.TabIndex = 2;
            this.labelGames.Text = "Games";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(575, 56);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(213, 20);
            this.textBoxName.TabIndex = 3;
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Font = new System.Drawing.Font("Modern No. 20", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelName.Location = new System.Drawing.Point(570, 24);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(77, 29);
            this.labelName.TabIndex = 4;
            this.labelName.Text = "Name";
            // 
            // labelProducent
            // 
            this.labelProducent.AutoSize = true;
            this.labelProducent.Font = new System.Drawing.Font("Modern No. 20", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelProducent.Location = new System.Drawing.Point(570, 79);
            this.labelProducent.Name = "labelProducent";
            this.labelProducent.Size = new System.Drawing.Size(125, 29);
            this.labelProducent.TabIndex = 6;
            this.labelProducent.Text = "Producent";
            // 
            // textBoxProducent
            // 
            this.textBoxProducent.Location = new System.Drawing.Point(575, 111);
            this.textBoxProducent.Name = "textBoxProducent";
            this.textBoxProducent.Size = new System.Drawing.Size(213, 20);
            this.textBoxProducent.TabIndex = 5;
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(575, 137);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(213, 31);
            this.buttonAdd.TabIndex = 7;
            this.buttonAdd.Text = "Add Game";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // FormMainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.labelProducent);
            this.Controls.Add(this.textBoxProducent);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.labelGames);
            this.Controls.Add(this.buttonRefresh);
            this.Controls.Add(this.dataGridViewGames);
            this.Name = "FormMainWindow";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewGames)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewGames;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.Label labelGames;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelProducent;
        private System.Windows.Forms.TextBox textBoxProducent;
        private System.Windows.Forms.Button buttonAdd;
    }
}

