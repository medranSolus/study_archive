namespace MarekMachlinskiLab4Zad1
{
    partial class FormRegister
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
            this.buttonAccept = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelPassword = new System.Windows.Forms.Label();
            this.labelLogin = new System.Windows.Forms.Label();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.textBoxLogin = new System.Windows.Forms.TextBox();
            this.textBoxPasswordRepeat = new System.Windows.Forms.TextBox();
            this.labelPasswordRepeat = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonAccept
            // 
            this.buttonAccept.BackColor = System.Drawing.Color.Black;
            this.buttonAccept.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAccept.Font = new System.Drawing.Font("Matura MT Script Capitals", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAccept.ForeColor = System.Drawing.Color.Gold;
            this.buttonAccept.Location = new System.Drawing.Point(621, 320);
            this.buttonAccept.Name = "buttonAccept";
            this.buttonAccept.Size = new System.Drawing.Size(133, 50);
            this.buttonAccept.TabIndex = 36;
            this.buttonAccept.Text = "Register";
            this.buttonAccept.UseVisualStyleBackColor = false;
            this.buttonAccept.Click += new System.EventHandler(this.buttonAccept_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.Color.Black;
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancel.Font = new System.Drawing.Font("Matura MT Script Capitals", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCancel.ForeColor = System.Drawing.Color.Silver;
            this.buttonCancel.Location = new System.Drawing.Point(17, 320);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(133, 50);
            this.buttonCancel.TabIndex = 35;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.BackColor = System.Drawing.Color.Transparent;
            this.labelPassword.Font = new System.Drawing.Font("Matura MT Script Capitals", 26F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPassword.ForeColor = System.Drawing.Color.LightGoldenrodYellow;
            this.labelPassword.Location = new System.Drawing.Point(196, 163);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(190, 47);
            this.labelPassword.TabIndex = 38;
            this.labelPassword.Text = "Password:";
            // 
            // labelLogin
            // 
            this.labelLogin.AutoSize = true;
            this.labelLogin.BackColor = System.Drawing.Color.Transparent;
            this.labelLogin.Font = new System.Drawing.Font("Matura MT Script Capitals", 26F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLogin.ForeColor = System.Drawing.Color.LightGoldenrodYellow;
            this.labelLogin.Location = new System.Drawing.Point(253, 116);
            this.labelLogin.Name = "labelLogin";
            this.labelLogin.Size = new System.Drawing.Size(133, 47);
            this.labelLogin.TabIndex = 37;
            this.labelLogin.Text = "Login:";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.BackColor = System.Drawing.Color.DarkRed;
            this.textBoxPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxPassword.Font = new System.Drawing.Font("Matura MT Script Capitals", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPassword.ForeColor = System.Drawing.Color.LightGoldenrodYellow;
            this.textBoxPassword.Location = new System.Drawing.Point(392, 173);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(260, 36);
            this.textBoxPassword.TabIndex = 40;
            // 
            // textBoxLogin
            // 
            this.textBoxLogin.BackColor = System.Drawing.Color.DarkRed;
            this.textBoxLogin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxLogin.Font = new System.Drawing.Font("Matura MT Script Capitals", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxLogin.ForeColor = System.Drawing.Color.LightGoldenrodYellow;
            this.textBoxLogin.Location = new System.Drawing.Point(392, 124);
            this.textBoxLogin.Name = "textBoxLogin";
            this.textBoxLogin.Size = new System.Drawing.Size(260, 36);
            this.textBoxLogin.TabIndex = 39;
            // 
            // textBoxPasswordRepeat
            // 
            this.textBoxPasswordRepeat.BackColor = System.Drawing.Color.DarkRed;
            this.textBoxPasswordRepeat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxPasswordRepeat.Font = new System.Drawing.Font("Matura MT Script Capitals", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPasswordRepeat.ForeColor = System.Drawing.Color.LightGoldenrodYellow;
            this.textBoxPasswordRepeat.Location = new System.Drawing.Point(392, 222);
            this.textBoxPasswordRepeat.Name = "textBoxPasswordRepeat";
            this.textBoxPasswordRepeat.PasswordChar = '*';
            this.textBoxPasswordRepeat.Size = new System.Drawing.Size(260, 36);
            this.textBoxPasswordRepeat.TabIndex = 42;
            // 
            // labelPasswordRepeat
            // 
            this.labelPasswordRepeat.AutoSize = true;
            this.labelPasswordRepeat.BackColor = System.Drawing.Color.Transparent;
            this.labelPasswordRepeat.Font = new System.Drawing.Font("Matura MT Script Capitals", 26F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPasswordRepeat.ForeColor = System.Drawing.Color.LightGoldenrodYellow;
            this.labelPasswordRepeat.Location = new System.Drawing.Point(75, 212);
            this.labelPasswordRepeat.Name = "labelPasswordRepeat";
            this.labelPasswordRepeat.Size = new System.Drawing.Size(311, 47);
            this.labelPasswordRepeat.TabIndex = 41;
            this.labelPasswordRepeat.Text = "Repeat Password:";
            // 
            // FormRegister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::MarekMachlinskiLab4Zad1.Properties.Resources.Flag;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(766, 382);
            this.Controls.Add(this.textBoxPasswordRepeat);
            this.Controls.Add(this.labelPasswordRepeat);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.textBoxLogin);
            this.Controls.Add(this.labelPassword);
            this.Controls.Add(this.labelLogin);
            this.Controls.Add(this.buttonAccept);
            this.Controls.Add(this.buttonCancel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormRegister";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormRegister";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonAccept;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.Label labelLogin;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.TextBox textBoxLogin;
        private System.Windows.Forms.TextBox textBoxPasswordRepeat;
        private System.Windows.Forms.Label labelPasswordRepeat;
    }
}