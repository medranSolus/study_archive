namespace MarekMachlinskiLab2Zad1
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
            this.elementHostMedia = new System.Windows.Forms.Integration.ElementHost();
            this.labelPlay = new System.Windows.Forms.Label();
            this.pictureBoxAbout = new System.Windows.Forms.PictureBox();
            this.pictureBoxMenuBackground = new System.Windows.Forms.PictureBox();
            this.labelScore = new System.Windows.Forms.Label();
            this.labelLevelEnd = new System.Windows.Forms.Label();
            this.progressBarBossLife = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAbout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMenuBackground)).BeginInit();
            this.SuspendLayout();
            // 
            // elementHostMedia
            // 
            this.elementHostMedia.BackColor = System.Drawing.Color.Black;
            this.elementHostMedia.BackColorTransparent = true;
            this.elementHostMedia.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.elementHostMedia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elementHostMedia.Enabled = false;
            this.elementHostMedia.Location = new System.Drawing.Point(0, 0);
            this.elementHostMedia.Name = "elementHostMedia";
            this.elementHostMedia.Size = new System.Drawing.Size(852, 480);
            this.elementHostMedia.TabIndex = 0;
            this.elementHostMedia.Child = null;
            // 
            // labelPlay
            // 
            this.labelPlay.AutoSize = true;
            this.labelPlay.BackColor = System.Drawing.Color.Yellow;
            this.labelPlay.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelPlay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelPlay.Font = new System.Drawing.Font("SeasideResortNF", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPlay.ForeColor = System.Drawing.Color.Red;
            this.labelPlay.Location = new System.Drawing.Point(338, 270);
            this.labelPlay.Name = "labelPlay";
            this.labelPlay.Size = new System.Drawing.Size(174, 49);
            this.labelPlay.TabIndex = 2;
            this.labelPlay.Text = "Zagraj";
            this.labelPlay.Click += new System.EventHandler(this.labelPlay_Click);
            // 
            // pictureBoxAbout
            // 
            this.pictureBoxAbout.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxAbout.BackgroundImage = global::MarekMachlinskiLab2Zad1.Properties.Resources.MenuBackground;
            this.pictureBoxAbout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxAbout.Image = global::MarekMachlinskiLab2Zad1.Properties.Resources.AboutButton;
            this.pictureBoxAbout.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxAbout.Name = "pictureBoxAbout";
            this.pictureBoxAbout.Size = new System.Drawing.Size(90, 90);
            this.pictureBoxAbout.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxAbout.TabIndex = 3;
            this.pictureBoxAbout.TabStop = false;
            this.pictureBoxAbout.Click += new System.EventHandler(this.pictureBoxAbout_Click);
            // 
            // pictureBoxMenuBackground
            // 
            this.pictureBoxMenuBackground.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxMenuBackground.Image = global::MarekMachlinskiLab2Zad1.Properties.Resources.MenuBackground;
            this.pictureBoxMenuBackground.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxMenuBackground.Name = "pictureBoxMenuBackground";
            this.pictureBoxMenuBackground.Size = new System.Drawing.Size(852, 480);
            this.pictureBoxMenuBackground.TabIndex = 1;
            this.pictureBoxMenuBackground.TabStop = false;
            // 
            // labelScore
            // 
            this.labelScore.AutoSize = true;
            this.labelScore.BackColor = System.Drawing.Color.Black;
            this.labelScore.Enabled = false;
            this.labelScore.Font = new System.Drawing.Font("SeasideResortNF", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelScore.ForeColor = System.Drawing.Color.Red;
            this.labelScore.Location = new System.Drawing.Point(12, 37);
            this.labelScore.Name = "labelScore";
            this.labelScore.Size = new System.Drawing.Size(212, 41);
            this.labelScore.TabIndex = 4;
            this.labelScore.Text = "Punkty: 0";
            this.labelScore.Visible = false;
            // 
            // labelLevelEnd
            // 
            this.labelLevelEnd.AutoSize = true;
            this.labelLevelEnd.BackColor = System.Drawing.Color.Black;
            this.labelLevelEnd.Enabled = false;
            this.labelLevelEnd.Font = new System.Drawing.Font("SeasideResortNF", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLevelEnd.ForeColor = System.Drawing.Color.Red;
            this.labelLevelEnd.Location = new System.Drawing.Point(248, 129);
            this.labelLevelEnd.Name = "labelLevelEnd";
            this.labelLevelEnd.Size = new System.Drawing.Size(467, 41);
            this.labelLevelEnd.TabIndex = 5;
            this.labelLevelEnd.Text = "Zaraz kolejny poziom!";
            this.labelLevelEnd.Visible = false;
            // 
            // progressBarBossLife
            // 
            this.progressBarBossLife.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.progressBarBossLife.Enabled = false;
            this.progressBarBossLife.ForeColor = System.Drawing.Color.Gold;
            this.progressBarBossLife.Location = new System.Drawing.Point(15, 8);
            this.progressBarBossLife.Name = "progressBarBossLife";
            this.progressBarBossLife.Size = new System.Drawing.Size(489, 26);
            this.progressBarBossLife.Step = 1;
            this.progressBarBossLife.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBarBossLife.TabIndex = 6;
            this.progressBarBossLife.Value = 100;
            this.progressBarBossLife.Visible = false;
            // 
            // FormMainWindow
            // 
            this.AccessibleName = "";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(852, 480);
            this.Controls.Add(this.progressBarBossLife);
            this.Controls.Add(this.labelLevelEnd);
            this.Controls.Add(this.labelScore);
            this.Controls.Add(this.pictureBoxAbout);
            this.Controls.Add(this.labelPlay);
            this.Controls.Add(this.pictureBoxMenuBackground);
            this.Controls.Add(this.elementHostMedia);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMainWindow";
            this.Text = "Boogie Dancer";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAbout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMenuBackground)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Integration.ElementHost elementHostMedia;
        private System.Windows.Forms.PictureBox pictureBoxMenuBackground;
        private System.Windows.Forms.Label labelPlay;
        private System.Windows.Forms.PictureBox pictureBoxAbout;
        private System.Windows.Forms.Label labelScore;
        private System.Windows.Forms.Label labelLevelEnd;
        private System.Windows.Forms.ProgressBar progressBarBossLife;
    }
}

