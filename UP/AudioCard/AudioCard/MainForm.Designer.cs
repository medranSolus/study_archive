namespace AudioCard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.buttonPlaySound = new System.Windows.Forms.Button();
            this.buttonPlayWMP = new System.Windows.Forms.Button();
            this.buttonPauseResumeWMP = new System.Windows.Forms.Button();
            this.buttonStopWMP = new System.Windows.Forms.Button();
            this.axWindowsMediaPlayer = new AxWMPLib.AxWindowsMediaPlayer();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonAxWMPGetFile = new System.Windows.Forms.Button();
            this.buttonPlayDirect = new System.Windows.Forms.Button();
            this.buttonShowWAV = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonPlaySound
            // 
            this.buttonPlaySound.Location = new System.Drawing.Point(12, 12);
            this.buttonPlaySound.Name = "buttonPlaySound";
            this.buttonPlaySound.Size = new System.Drawing.Size(83, 27);
            this.buttonPlaySound.TabIndex = 0;
            this.buttonPlaySound.Text = "Play Sound";
            this.buttonPlaySound.UseVisualStyleBackColor = true;
            this.buttonPlaySound.Click += new System.EventHandler(this.buttonPlaySound_Click);
            // 
            // buttonPlayWMP
            // 
            this.buttonPlayWMP.Location = new System.Drawing.Point(12, 45);
            this.buttonPlayWMP.Name = "buttonPlayWMP";
            this.buttonPlayWMP.Size = new System.Drawing.Size(82, 25);
            this.buttonPlayWMP.TabIndex = 1;
            this.buttonPlayWMP.Text = "Play WMP";
            this.buttonPlayWMP.UseVisualStyleBackColor = true;
            this.buttonPlayWMP.Click += new System.EventHandler(this.buttonPlayWMP_Click);
            // 
            // buttonPauseResumeWMP
            // 
            this.buttonPauseResumeWMP.Location = new System.Drawing.Point(12, 76);
            this.buttonPauseResumeWMP.Name = "buttonPauseResumeWMP";
            this.buttonPauseResumeWMP.Size = new System.Drawing.Size(174, 24);
            this.buttonPauseResumeWMP.TabIndex = 2;
            this.buttonPauseResumeWMP.Text = "Pause\\Resume WMP";
            this.buttonPauseResumeWMP.UseVisualStyleBackColor = true;
            this.buttonPauseResumeWMP.Click += new System.EventHandler(this.buttonPauseResumeWMP_Click);
            // 
            // buttonStopWMP
            // 
            this.buttonStopWMP.Location = new System.Drawing.Point(100, 46);
            this.buttonStopWMP.Name = "buttonStopWMP";
            this.buttonStopWMP.Size = new System.Drawing.Size(86, 24);
            this.buttonStopWMP.TabIndex = 3;
            this.buttonStopWMP.Text = "Stop WMP";
            this.buttonStopWMP.UseVisualStyleBackColor = true;
            this.buttonStopWMP.Click += new System.EventHandler(this.buttonStopWMP_Click);
            // 
            // axWindowsMediaPlayer
            // 
            this.axWindowsMediaPlayer.Enabled = true;
            this.axWindowsMediaPlayer.Location = new System.Drawing.Point(9, 181);
            this.axWindowsMediaPlayer.Name = "axWindowsMediaPlayer";
            this.axWindowsMediaPlayer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWindowsMediaPlayer.OcxState")));
            this.axWindowsMediaPlayer.Size = new System.Drawing.Size(223, 45);
            this.axWindowsMediaPlayer.TabIndex = 4;
            this.axWindowsMediaPlayer.Enter += new System.EventHandler(this.axWindowsMediaPlayer_Enter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 132);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "ActiceX Window Media Player";
            // 
            // buttonAxWMPGetFile
            // 
            this.buttonAxWMPGetFile.Location = new System.Drawing.Point(9, 151);
            this.buttonAxWMPGetFile.Name = "buttonAxWMPGetFile";
            this.buttonAxWMPGetFile.Size = new System.Drawing.Size(124, 24);
            this.buttonAxWMPGetFile.TabIndex = 6;
            this.buttonAxWMPGetFile.Text = "Choose file AxWMP";
            this.buttonAxWMPGetFile.UseVisualStyleBackColor = true;
            this.buttonAxWMPGetFile.Click += new System.EventHandler(this.buttonAxWMPGetFile_Click);
            // 
            // buttonPlayDirect
            // 
            this.buttonPlayDirect.Location = new System.Drawing.Point(100, 14);
            this.buttonPlayDirect.Name = "buttonPlayDirect";
            this.buttonPlayDirect.Size = new System.Drawing.Size(85, 24);
            this.buttonPlayDirect.TabIndex = 7;
            this.buttonPlayDirect.Text = "Play Direct";
            this.buttonPlayDirect.UseVisualStyleBackColor = true;
            this.buttonPlayDirect.Click += new System.EventHandler(this.buttonPlayDirect_Click);
            // 
            // buttonShowWAV
            // 
            this.buttonShowWAV.Location = new System.Drawing.Point(12, 106);
            this.buttonShowWAV.Name = "buttonShowWAV";
            this.buttonShowWAV.Size = new System.Drawing.Size(174, 23);
            this.buttonShowWAV.TabIndex = 8;
            this.buttonShowWAV.Text = "Show WAV header";
            this.buttonShowWAV.UseVisualStyleBackColor = true;
            this.buttonShowWAV.Click += new System.EventHandler(this.buttonShowWAV_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(244, 238);
            this.Controls.Add(this.buttonShowWAV);
            this.Controls.Add(this.buttonPlayDirect);
            this.Controls.Add(this.buttonAxWMPGetFile);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.axWindowsMediaPlayer);
            this.Controls.Add(this.buttonStopWMP);
            this.Controls.Add(this.buttonPauseResumeWMP);
            this.Controls.Add(this.buttonPlayWMP);
            this.Controls.Add(this.buttonPlaySound);
            this.Name = "MainForm";
            this.Text = "Audio Card";
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonPlaySound;
        private System.Windows.Forms.Button buttonPlayWMP;
        private System.Windows.Forms.Button buttonPauseResumeWMP;
        private System.Windows.Forms.Button buttonStopWMP;
        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonAxWMPGetFile;
        private System.Windows.Forms.Button buttonPlayDirect;
        private System.Windows.Forms.Button buttonShowWAV;
    }
}

