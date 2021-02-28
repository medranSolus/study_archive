namespace MarekMachlinskiLab2
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
            this.labelName = new System.Windows.Forms.Label();
            this.buttonNewWindow = new System.Windows.Forms.Button();
            this.textBoxTransferText = new System.Windows.Forms.TextBox();
            this.textBoxNotepad = new System.Windows.Forms.TextBox();
            this.buttonShow = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelName.Location = new System.Drawing.Point(12, 9);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(199, 29);
            this.labelName.TabIndex = 0;
            this.labelName.Text = "Marek Machliński";
            // 
            // buttonNewWindow
            // 
            this.buttonNewWindow.Location = new System.Drawing.Point(664, 397);
            this.buttonNewWindow.Name = "buttonNewWindow";
            this.buttonNewWindow.Size = new System.Drawing.Size(124, 41);
            this.buttonNewWindow.TabIndex = 1;
            this.buttonNewWindow.Text = "Utwórz nowe okno";
            this.buttonNewWindow.UseVisualStyleBackColor = true;
            this.buttonNewWindow.Click += new System.EventHandler(this.buttonNewWindow_Click);
            // 
            // textBoxTransferText
            // 
            this.textBoxTransferText.Location = new System.Drawing.Point(593, 371);
            this.textBoxTransferText.Name = "textBoxTransferText";
            this.textBoxTransferText.Size = new System.Drawing.Size(195, 20);
            this.textBoxTransferText.TabIndex = 2;
            this.textBoxTransferText.TextChanged += new System.EventHandler(this.textBoxTransferText_TextChanged);
            // 
            // textBoxNotepad
            // 
            this.textBoxNotepad.Location = new System.Drawing.Point(12, 41);
            this.textBoxNotepad.Multiline = true;
            this.textBoxNotepad.Name = "textBoxNotepad";
            this.textBoxNotepad.Size = new System.Drawing.Size(575, 397);
            this.textBoxNotepad.TabIndex = 3;
            this.textBoxNotepad.WordWrap = false;
            // 
            // buttonShow
            // 
            this.buttonShow.Location = new System.Drawing.Point(593, 41);
            this.buttonShow.Name = "buttonShow";
            this.buttonShow.Size = new System.Drawing.Size(75, 31);
            this.buttonShow.TabIndex = 4;
            this.buttonShow.Text = "Show";
            this.buttonShow.UseVisualStyleBackColor = true;
            this.buttonShow.Click += new System.EventHandler(this.buttonShow_Click);
            // 
            // FormMainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonShow);
            this.Controls.Add(this.textBoxNotepad);
            this.Controls.Add(this.textBoxTransferText);
            this.Controls.Add(this.buttonNewWindow);
            this.Controls.Add(this.labelName);
            this.Name = "FormMainWindow";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Button buttonNewWindow;
        private System.Windows.Forms.TextBox textBoxTransferText;
        private System.Windows.Forms.TextBox textBoxNotepad;
        private System.Windows.Forms.Button buttonShow;
    }
}

