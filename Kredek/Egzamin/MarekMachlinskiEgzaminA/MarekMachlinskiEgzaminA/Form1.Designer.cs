namespace MarekMachlinskiEgzaminA
{
    partial class MainForm
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
            this.labelName = new System.Windows.Forms.Label();
            this.A1Prawo = new System.Windows.Forms.Button();
            this.A1Lewo = new System.Windows.Forms.Button();
            this.A3NoweOkno = new System.Windows.Forms.Button();
            this.TextBox = new System.Windows.Forms.TextBox();
            this.A4Pracownicy = new System.Windows.Forms.Button();
            this.dataGridViewMain = new System.Windows.Forms.DataGridView();
            this.A5BezPrzelozonych = new System.Windows.Forms.Button();
            this.A6Produkty = new System.Windows.Forms.Button();
            this.A7PracownicyIZamowienia = new System.Windows.Forms.Button();
            this.A2Dodaj = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMain)).BeginInit();
            this.SuspendLayout();
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(437, 21);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(90, 13);
            this.labelName.TabIndex = 0;
            this.labelName.Text = "Marek Machliński";
            // 
            // A1Prawo
            // 
            this.A1Prawo.Location = new System.Drawing.Point(115, 12);
            this.A1Prawo.Name = "A1Prawo";
            this.A1Prawo.Size = new System.Drawing.Size(97, 31);
            this.A1Prawo.TabIndex = 1;
            this.A1Prawo.Text = "A1 W prawo";
            this.A1Prawo.UseVisualStyleBackColor = true;
            this.A1Prawo.Click += new System.EventHandler(this.A1Prawo_Click);
            // 
            // A1Lewo
            // 
            this.A1Lewo.Location = new System.Drawing.Point(12, 12);
            this.A1Lewo.Name = "A1Lewo";
            this.A1Lewo.Size = new System.Drawing.Size(97, 31);
            this.A1Lewo.TabIndex = 2;
            this.A1Lewo.Text = "A1 W lewo";
            this.A1Lewo.UseVisualStyleBackColor = true;
            this.A1Lewo.Click += new System.EventHandler(this.A1Lewo_Click);
            // 
            // A3NoweOkno
            // 
            this.A3NoweOkno.Location = new System.Drawing.Point(12, 93);
            this.A3NoweOkno.Name = "A3NoweOkno";
            this.A3NoweOkno.Size = new System.Drawing.Size(107, 36);
            this.A3NoweOkno.TabIndex = 3;
            this.A3NoweOkno.Text = "A3 Nowe Okno";
            this.A3NoweOkno.UseVisualStyleBackColor = true;
            this.A3NoweOkno.Click += new System.EventHandler(this.A3NoweOkno_Click);
            // 
            // TextBox
            // 
            this.TextBox.Location = new System.Drawing.Point(125, 102);
            this.TextBox.Name = "TextBox";
            this.TextBox.Size = new System.Drawing.Size(210, 20);
            this.TextBox.TabIndex = 4;
            // 
            // A4Pracownicy
            // 
            this.A4Pracownicy.Location = new System.Drawing.Point(12, 157);
            this.A4Pracownicy.Name = "A4Pracownicy";
            this.A4Pracownicy.Size = new System.Drawing.Size(93, 29);
            this.A4Pracownicy.TabIndex = 5;
            this.A4Pracownicy.Text = "A4 Pracownicy";
            this.A4Pracownicy.UseVisualStyleBackColor = true;
            this.A4Pracownicy.Click += new System.EventHandler(this.A4Pracownicy_Click);
            // 
            // dataGridViewMain
            // 
            this.dataGridViewMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMain.Location = new System.Drawing.Point(23, 209);
            this.dataGridViewMain.Name = "dataGridViewMain";
            this.dataGridViewMain.Size = new System.Drawing.Size(480, 219);
            this.dataGridViewMain.TabIndex = 6;
            // 
            // A5BezPrzelozonych
            // 
            this.A5BezPrzelozonych.Location = new System.Drawing.Point(125, 157);
            this.A5BezPrzelozonych.Name = "A5BezPrzelozonych";
            this.A5BezPrzelozonych.Size = new System.Drawing.Size(148, 32);
            this.A5BezPrzelozonych.TabIndex = 7;
            this.A5BezPrzelozonych.Text = "A5 Bez Przełożonych";
            this.A5BezPrzelozonych.UseVisualStyleBackColor = true;
            this.A5BezPrzelozonych.Click += new System.EventHandler(this.A5BezPrzelozonych_Click);
            // 
            // A6Produkty
            // 
            this.A6Produkty.Location = new System.Drawing.Point(339, 152);
            this.A6Produkty.Name = "A6Produkty";
            this.A6Produkty.Size = new System.Drawing.Size(152, 38);
            this.A6Produkty.TabIndex = 8;
            this.A6Produkty.Text = "A6 Produkty";
            this.A6Produkty.UseVisualStyleBackColor = true;
            this.A6Produkty.Click += new System.EventHandler(this.A6Produkty_Click);
            // 
            // A7PracownicyIZamowienia
            // 
            this.A7PracownicyIZamowienia.Location = new System.Drawing.Point(577, 157);
            this.A7PracownicyIZamowienia.Name = "A7PracownicyIZamowienia";
            this.A7PracownicyIZamowienia.Size = new System.Drawing.Size(152, 32);
            this.A7PracownicyIZamowienia.TabIndex = 9;
            this.A7PracownicyIZamowienia.Text = "A7 Pracownicy i zamówienia";
            this.A7PracownicyIZamowienia.UseVisualStyleBackColor = true;
            this.A7PracownicyIZamowienia.Click += new System.EventHandler(this.A7PracownicyIZamowienia_Click);
            // 
            // A2Dodaj
            // 
            this.A2Dodaj.Location = new System.Drawing.Point(12, 49);
            this.A2Dodaj.Name = "A2Dodaj";
            this.A2Dodaj.Size = new System.Drawing.Size(105, 21);
            this.A2Dodaj.TabIndex = 10;
            this.A2Dodaj.Text = "A2 Dodaj";
            this.A2Dodaj.UseVisualStyleBackColor = true;
            this.A2Dodaj.Click += new System.EventHandler(this.A2Dodaj_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1257, 450);
            this.Controls.Add(this.A2Dodaj);
            this.Controls.Add(this.A7PracownicyIZamowienia);
            this.Controls.Add(this.A6Produkty);
            this.Controls.Add(this.A5BezPrzelozonych);
            this.Controls.Add(this.dataGridViewMain);
            this.Controls.Add(this.A4Pracownicy);
            this.Controls.Add(this.TextBox);
            this.Controls.Add(this.A3NoweOkno);
            this.Controls.Add(this.A1Lewo);
            this.Controls.Add(this.A1Prawo);
            this.Controls.Add(this.labelName);
            this.Name = "MainForm";
            this.Text = "EgzaminA";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Button A1Prawo;
        private System.Windows.Forms.Button A1Lewo;
        private System.Windows.Forms.Button A3NoweOkno;
        private System.Windows.Forms.TextBox TextBox;
        private System.Windows.Forms.Button A4Pracownicy;
        private System.Windows.Forms.DataGridView dataGridViewMain;
        private System.Windows.Forms.Button A5BezPrzelozonych;
        private System.Windows.Forms.Button A6Produkty;
        private System.Windows.Forms.Button A7PracownicyIZamowienia;
        private System.Windows.Forms.Button A2Dodaj;
    }
}

