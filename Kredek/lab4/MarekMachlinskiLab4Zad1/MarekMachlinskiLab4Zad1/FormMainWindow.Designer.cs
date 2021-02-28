namespace MarekMachlinskiLab4Zad1
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMainWindow));
            this.textBoxLogin = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.labelLogin = new System.Windows.Forms.Label();
            this.labelPassword = new System.Windows.Forms.Label();
            this.labelTittle = new System.Windows.Forms.Label();
            this.buttonLogIn = new System.Windows.Forms.Button();
            this.dataGridViewTables = new System.Windows.Forms.DataGridView();
            this.labelName = new System.Windows.Forms.Label();
            this.labelLevel = new System.Windows.Forms.Label();
            this.buttonCreateCertificate = new System.Windows.Forms.Button();
            this.buttonListOfCertificates = new System.Windows.Forms.Button();
            this.buttonDeleteMember = new System.Windows.Forms.Button();
            this.buttonAddNewMember = new System.Windows.Forms.Button();
            this.labelClan = new System.Windows.Forms.Label();
            this.buttonWar = new System.Windows.Forms.Button();
            this.buttonDeclareAlliance = new System.Windows.Forms.Button();
            this.buttonPeace = new System.Windows.Forms.Button();
            this.buttonBrokeAlliance = new System.Windows.Forms.Button();
            this.buttonWarWith = new System.Windows.Forms.Button();
            this.buttonAllianceWith = new System.Windows.Forms.Button();
            this.buttonClansList = new System.Windows.Forms.Button();
            this.buttonClanMembers = new System.Windows.Forms.Button();
            this.buttonLogOut = new System.Windows.Forms.Button();
            this.buttonPromote = new System.Windows.Forms.Button();
            this.buttonDowngrade = new System.Windows.Forms.Button();
            this.buttonRegister = new System.Windows.Forms.Button();
            this.buttonUsersList = new System.Windows.Forms.Button();
            this.buttonCreateClan = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTables)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxLogin
            // 
            this.textBoxLogin.BackColor = System.Drawing.Color.DarkRed;
            this.textBoxLogin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxLogin.Font = new System.Drawing.Font("Matura MT Script Capitals", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxLogin.ForeColor = System.Drawing.Color.LightGoldenrodYellow;
            this.textBoxLogin.Location = new System.Drawing.Point(483, 267);
            this.textBoxLogin.Name = "textBoxLogin";
            this.textBoxLogin.Size = new System.Drawing.Size(260, 36);
            this.textBoxLogin.TabIndex = 0;
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.BackColor = System.Drawing.Color.DarkRed;
            this.textBoxPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxPassword.Font = new System.Drawing.Font("Matura MT Script Capitals", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPassword.ForeColor = System.Drawing.Color.LightGoldenrodYellow;
            this.textBoxPassword.Location = new System.Drawing.Point(483, 316);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(260, 36);
            this.textBoxPassword.TabIndex = 1;
            // 
            // labelLogin
            // 
            this.labelLogin.AutoSize = true;
            this.labelLogin.BackColor = System.Drawing.Color.Transparent;
            this.labelLogin.Font = new System.Drawing.Font("Matura MT Script Capitals", 26F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLogin.ForeColor = System.Drawing.Color.LightGoldenrodYellow;
            this.labelLogin.Location = new System.Drawing.Point(344, 259);
            this.labelLogin.Name = "labelLogin";
            this.labelLogin.Size = new System.Drawing.Size(133, 47);
            this.labelLogin.TabIndex = 2;
            this.labelLogin.Text = "Login:";
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.BackColor = System.Drawing.Color.Transparent;
            this.labelPassword.Font = new System.Drawing.Font("Matura MT Script Capitals", 26F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPassword.ForeColor = System.Drawing.Color.LightGoldenrodYellow;
            this.labelPassword.Location = new System.Drawing.Point(287, 306);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(190, 47);
            this.labelPassword.TabIndex = 3;
            this.labelPassword.Text = "Password:";
            // 
            // labelTittle
            // 
            this.labelTittle.AutoSize = true;
            this.labelTittle.BackColor = System.Drawing.Color.Transparent;
            this.labelTittle.Font = new System.Drawing.Font("Matura MT Script Capitals", 40F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTittle.ForeColor = System.Drawing.Color.LightGoldenrodYellow;
            this.labelTittle.Location = new System.Drawing.Point(419, 159);
            this.labelTittle.Name = "labelTittle";
            this.labelTittle.Size = new System.Drawing.Size(421, 72);
            this.labelTittle.TabIndex = 4;
            this.labelTittle.Text = "Dwarven Stone";
            // 
            // buttonLogIn
            // 
            this.buttonLogIn.AutoSize = true;
            this.buttonLogIn.BackColor = System.Drawing.Color.DarkRed;
            this.buttonLogIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLogIn.Font = new System.Drawing.Font("Matura MT Script Capitals", 26F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonLogIn.ForeColor = System.Drawing.Color.Gold;
            this.buttonLogIn.Location = new System.Drawing.Point(749, 284);
            this.buttonLogIn.Name = "buttonLogIn";
            this.buttonLogIn.Size = new System.Drawing.Size(147, 59);
            this.buttonLogIn.TabIndex = 5;
            this.buttonLogIn.Text = "Log in";
            this.buttonLogIn.UseVisualStyleBackColor = false;
            this.buttonLogIn.Click += new System.EventHandler(this.buttonLogIn_Click);
            // 
            // dataGridViewTables
            // 
            this.dataGridViewTables.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewTables.BackgroundColor = System.Drawing.Color.DarkRed;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Matura MT Script Capitals", 16F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Gold;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.DarkGoldenrod;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.LightGoldenrodYellow;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTables.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewTables.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Matura MT Script Capitals", 16F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Gold;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.DarkGoldenrod;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.LightGoldenrodYellow;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTables.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewTables.Enabled = false;
            this.dataGridViewTables.GridColor = System.Drawing.Color.Gold;
            this.dataGridViewTables.Location = new System.Drawing.Point(729, 12);
            this.dataGridViewTables.Name = "dataGridViewTables";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Matura MT Script Capitals", 16F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Gold;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.DarkGoldenrod;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.LightGoldenrodYellow;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTables.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewTables.Size = new System.Drawing.Size(444, 581);
            this.dataGridViewTables.TabIndex = 6;
            this.dataGridViewTables.Visible = false;
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.BackColor = System.Drawing.Color.Transparent;
            this.labelName.Enabled = false;
            this.labelName.Font = new System.Drawing.Font("Matura MT Script Capitals", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelName.ForeColor = System.Drawing.Color.Gold;
            this.labelName.Location = new System.Drawing.Point(12, 9);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(145, 53);
            this.labelName.TabIndex = 7;
            this.labelName.Text = "Name";
            this.labelName.Visible = false;
            // 
            // labelLevel
            // 
            this.labelLevel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelLevel.AutoSize = true;
            this.labelLevel.BackColor = System.Drawing.Color.Transparent;
            this.labelLevel.Enabled = false;
            this.labelLevel.Font = new System.Drawing.Font("Matura MT Script Capitals", 26F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLevel.ForeColor = System.Drawing.Color.LightGoldenrodYellow;
            this.labelLevel.Location = new System.Drawing.Point(12, 99);
            this.labelLevel.Name = "labelLevel";
            this.labelLevel.Size = new System.Drawing.Size(119, 47);
            this.labelLevel.TabIndex = 8;
            this.labelLevel.Text = "Level";
            this.labelLevel.Visible = false;
            // 
            // buttonCreateCertificate
            // 
            this.buttonCreateCertificate.BackColor = System.Drawing.Color.Black;
            this.buttonCreateCertificate.Enabled = false;
            this.buttonCreateCertificate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCreateCertificate.Font = new System.Drawing.Font("Matura MT Script Capitals", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCreateCertificate.ForeColor = System.Drawing.Color.Gold;
            this.buttonCreateCertificate.Location = new System.Drawing.Point(12, 157);
            this.buttonCreateCertificate.Name = "buttonCreateCertificate";
            this.buttonCreateCertificate.Size = new System.Drawing.Size(244, 85);
            this.buttonCreateCertificate.TabIndex = 9;
            this.buttonCreateCertificate.Text = "Save Commitment Certificate";
            this.buttonCreateCertificate.UseVisualStyleBackColor = false;
            this.buttonCreateCertificate.Visible = false;
            this.buttonCreateCertificate.Click += new System.EventHandler(this.buttonCreateCertificate_Click);
            // 
            // buttonListOfCertificates
            // 
            this.buttonListOfCertificates.BackColor = System.Drawing.Color.Black;
            this.buttonListOfCertificates.Enabled = false;
            this.buttonListOfCertificates.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonListOfCertificates.Font = new System.Drawing.Font("Matura MT Script Capitals", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonListOfCertificates.ForeColor = System.Drawing.Color.Gold;
            this.buttonListOfCertificates.Location = new System.Drawing.Point(12, 248);
            this.buttonListOfCertificates.Name = "buttonListOfCertificates";
            this.buttonListOfCertificates.Size = new System.Drawing.Size(224, 85);
            this.buttonListOfCertificates.TabIndex = 10;
            this.buttonListOfCertificates.Text = "List of Created Certificates";
            this.buttonListOfCertificates.UseVisualStyleBackColor = false;
            this.buttonListOfCertificates.Visible = false;
            this.buttonListOfCertificates.Click += new System.EventHandler(this.buttonListOfCertificates_Click);
            // 
            // buttonDeleteMember
            // 
            this.buttonDeleteMember.AutoSize = true;
            this.buttonDeleteMember.BackColor = System.Drawing.Color.DarkRed;
            this.buttonDeleteMember.Enabled = false;
            this.buttonDeleteMember.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDeleteMember.Font = new System.Drawing.Font("Matura MT Script Capitals", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDeleteMember.ForeColor = System.Drawing.Color.LightGoldenrodYellow;
            this.buttonDeleteMember.Location = new System.Drawing.Point(12, 469);
            this.buttonDeleteMember.Name = "buttonDeleteMember";
            this.buttonDeleteMember.Size = new System.Drawing.Size(437, 59);
            this.buttonDeleteMember.TabIndex = 11;
            this.buttonDeleteMember.Text = "Banish Dwarf from Karak Izor";
            this.buttonDeleteMember.UseVisualStyleBackColor = false;
            this.buttonDeleteMember.Visible = false;
            this.buttonDeleteMember.Click += new System.EventHandler(this.buttonDeleteMember_Click);
            // 
            // buttonAddNewMember
            // 
            this.buttonAddNewMember.AutoSize = true;
            this.buttonAddNewMember.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.buttonAddNewMember.Enabled = false;
            this.buttonAddNewMember.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAddNewMember.Font = new System.Drawing.Font("Matura MT Script Capitals", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAddNewMember.ForeColor = System.Drawing.Color.LightGoldenrodYellow;
            this.buttonAddNewMember.Location = new System.Drawing.Point(12, 534);
            this.buttonAddNewMember.Name = "buttonAddNewMember";
            this.buttonAddNewMember.Size = new System.Drawing.Size(429, 59);
            this.buttonAddNewMember.TabIndex = 12;
            this.buttonAddNewMember.Text = "Welcome Dwarf to Karak Izor";
            this.buttonAddNewMember.UseVisualStyleBackColor = false;
            this.buttonAddNewMember.Visible = false;
            this.buttonAddNewMember.Click += new System.EventHandler(this.buttonAddNewMember_Click);
            // 
            // labelClan
            // 
            this.labelClan.AutoSize = true;
            this.labelClan.BackColor = System.Drawing.Color.Transparent;
            this.labelClan.Enabled = false;
            this.labelClan.Font = new System.Drawing.Font("Matura MT Script Capitals", 26F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelClan.ForeColor = System.Drawing.Color.LightGoldenrodYellow;
            this.labelClan.Location = new System.Drawing.Point(13, 52);
            this.labelClan.Name = "labelClan";
            this.labelClan.Size = new System.Drawing.Size(96, 47);
            this.labelClan.TabIndex = 13;
            this.labelClan.Text = "Clan";
            this.labelClan.Visible = false;
            // 
            // buttonWar
            // 
            this.buttonWar.AutoSize = true;
            this.buttonWar.BackColor = System.Drawing.Color.DarkRed;
            this.buttonWar.Enabled = false;
            this.buttonWar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonWar.Font = new System.Drawing.Font("Matura MT Script Capitals", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonWar.ForeColor = System.Drawing.Color.Gold;
            this.buttonWar.Location = new System.Drawing.Point(262, 339);
            this.buttonWar.Name = "buttonWar";
            this.buttonWar.Size = new System.Drawing.Size(204, 59);
            this.buttonWar.TabIndex = 14;
            this.buttonWar.Text = "Declare War!";
            this.buttonWar.UseVisualStyleBackColor = true;
            this.buttonWar.Visible = false;
            this.buttonWar.Click += new System.EventHandler(this.buttonWar_Click);
            // 
            // buttonDeclareAlliance
            // 
            this.buttonDeclareAlliance.AutoSize = true;
            this.buttonDeclareAlliance.BackColor = System.Drawing.Color.DarkRed;
            this.buttonDeclareAlliance.Enabled = false;
            this.buttonDeclareAlliance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDeclareAlliance.Font = new System.Drawing.Font("Matura MT Script Capitals", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDeclareAlliance.ForeColor = System.Drawing.Color.Gold;
            this.buttonDeclareAlliance.Location = new System.Drawing.Point(12, 339);
            this.buttonDeclareAlliance.Name = "buttonDeclareAlliance";
            this.buttonDeclareAlliance.Size = new System.Drawing.Size(244, 59);
            this.buttonDeclareAlliance.TabIndex = 15;
            this.buttonDeclareAlliance.Text = "Declare Alliance";
            this.buttonDeclareAlliance.UseVisualStyleBackColor = true;
            this.buttonDeclareAlliance.Visible = false;
            this.buttonDeclareAlliance.Click += new System.EventHandler(this.buttonDeclareAlliance_Click);
            // 
            // buttonPeace
            // 
            this.buttonPeace.AutoSize = true;
            this.buttonPeace.BackColor = System.Drawing.Color.DarkRed;
            this.buttonPeace.Enabled = false;
            this.buttonPeace.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPeace.Font = new System.Drawing.Font("Matura MT Script Capitals", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPeace.ForeColor = System.Drawing.Color.Gold;
            this.buttonPeace.Location = new System.Drawing.Point(262, 404);
            this.buttonPeace.Name = "buttonPeace";
            this.buttonPeace.Size = new System.Drawing.Size(196, 59);
            this.buttonPeace.TabIndex = 16;
            this.buttonPeace.Text = "Ensure Peace";
            this.buttonPeace.UseVisualStyleBackColor = true;
            this.buttonPeace.Visible = false;
            this.buttonPeace.Click += new System.EventHandler(this.buttonPeace_Click);
            // 
            // buttonBrokeAlliance
            // 
            this.buttonBrokeAlliance.AutoSize = true;
            this.buttonBrokeAlliance.BackColor = System.Drawing.Color.DarkRed;
            this.buttonBrokeAlliance.Enabled = false;
            this.buttonBrokeAlliance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonBrokeAlliance.Font = new System.Drawing.Font("Matura MT Script Capitals", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonBrokeAlliance.ForeColor = System.Drawing.Color.Gold;
            this.buttonBrokeAlliance.Location = new System.Drawing.Point(12, 404);
            this.buttonBrokeAlliance.Name = "buttonBrokeAlliance";
            this.buttonBrokeAlliance.Size = new System.Drawing.Size(224, 59);
            this.buttonBrokeAlliance.TabIndex = 17;
            this.buttonBrokeAlliance.Text = "Broke Alliance";
            this.buttonBrokeAlliance.UseVisualStyleBackColor = true;
            this.buttonBrokeAlliance.Visible = false;
            this.buttonBrokeAlliance.Click += new System.EventHandler(this.buttonBrokeAlliance_Click);
            // 
            // buttonWarWith
            // 
            this.buttonWarWith.AutoSize = true;
            this.buttonWarWith.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.buttonWarWith.Enabled = false;
            this.buttonWarWith.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonWarWith.Font = new System.Drawing.Font("Matura MT Script Capitals", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonWarWith.ForeColor = System.Drawing.Color.LightGoldenrodYellow;
            this.buttonWarWith.Location = new System.Drawing.Point(499, 339);
            this.buttonWarWith.Name = "buttonWarWith";
            this.buttonWarWith.Size = new System.Drawing.Size(224, 59);
            this.buttonWarWith.TabIndex = 18;
            this.buttonWarWith.Text = "At War with...";
            this.buttonWarWith.UseVisualStyleBackColor = false;
            this.buttonWarWith.Visible = false;
            this.buttonWarWith.Click += new System.EventHandler(this.buttonWarWith_Click);
            // 
            // buttonAllianceWith
            // 
            this.buttonAllianceWith.AutoSize = true;
            this.buttonAllianceWith.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.buttonAllianceWith.Enabled = false;
            this.buttonAllianceWith.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAllianceWith.Font = new System.Drawing.Font("Matura MT Script Capitals", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAllianceWith.ForeColor = System.Drawing.Color.LightGoldenrodYellow;
            this.buttonAllianceWith.Location = new System.Drawing.Point(505, 274);
            this.buttonAllianceWith.Name = "buttonAllianceWith";
            this.buttonAllianceWith.Size = new System.Drawing.Size(218, 59);
            this.buttonAllianceWith.TabIndex = 19;
            this.buttonAllianceWith.Text = "Alliance with...";
            this.buttonAllianceWith.UseVisualStyleBackColor = false;
            this.buttonAllianceWith.Visible = false;
            this.buttonAllianceWith.Click += new System.EventHandler(this.buttonAllianceWith_Click);
            // 
            // buttonClansList
            // 
            this.buttonClansList.AutoSize = true;
            this.buttonClansList.BackColor = System.Drawing.Color.Black;
            this.buttonClansList.Enabled = false;
            this.buttonClansList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClansList.Font = new System.Drawing.Font("Matura MT Script Capitals", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonClansList.ForeColor = System.Drawing.Color.Gold;
            this.buttonClansList.Location = new System.Drawing.Point(519, 534);
            this.buttonClansList.Name = "buttonClansList";
            this.buttonClansList.Size = new System.Drawing.Size(204, 59);
            this.buttonClansList.TabIndex = 20;
            this.buttonClansList.Text = "List of Clans";
            this.buttonClansList.UseVisualStyleBackColor = false;
            this.buttonClansList.Visible = false;
            this.buttonClansList.Click += new System.EventHandler(this.buttonClansList_Click);
            // 
            // buttonClanMembers
            // 
            this.buttonClanMembers.AutoSize = true;
            this.buttonClanMembers.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.buttonClanMembers.Enabled = false;
            this.buttonClanMembers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClanMembers.Font = new System.Drawing.Font("Matura MT Script Capitals", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonClanMembers.ForeColor = System.Drawing.Color.LightGoldenrodYellow;
            this.buttonClanMembers.Location = new System.Drawing.Point(511, 404);
            this.buttonClanMembers.Name = "buttonClanMembers";
            this.buttonClanMembers.Size = new System.Drawing.Size(212, 59);
            this.buttonClanMembers.TabIndex = 21;
            this.buttonClanMembers.Text = "Clan Members";
            this.buttonClanMembers.UseVisualStyleBackColor = false;
            this.buttonClanMembers.Visible = false;
            this.buttonClanMembers.Click += new System.EventHandler(this.buttonClanMembers_Click);
            // 
            // buttonLogOut
            // 
            this.buttonLogOut.BackColor = System.Drawing.Color.Black;
            this.buttonLogOut.Enabled = false;
            this.buttonLogOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLogOut.Font = new System.Drawing.Font("Matura MT Script Capitals", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonLogOut.ForeColor = System.Drawing.Color.Silver;
            this.buttonLogOut.Location = new System.Drawing.Point(582, 12);
            this.buttonLogOut.Name = "buttonLogOut";
            this.buttonLogOut.Size = new System.Drawing.Size(141, 50);
            this.buttonLogOut.TabIndex = 22;
            this.buttonLogOut.Text = "Log out";
            this.buttonLogOut.UseVisualStyleBackColor = false;
            this.buttonLogOut.Visible = false;
            this.buttonLogOut.Click += new System.EventHandler(this.buttonLogOut_Click);
            // 
            // buttonPromote
            // 
            this.buttonPromote.AutoSize = true;
            this.buttonPromote.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.buttonPromote.Enabled = false;
            this.buttonPromote.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPromote.Font = new System.Drawing.Font("Matura MT Script Capitals", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPromote.ForeColor = System.Drawing.Color.LightGoldenrodYellow;
            this.buttonPromote.Location = new System.Drawing.Point(497, 209);
            this.buttonPromote.Name = "buttonPromote";
            this.buttonPromote.Size = new System.Drawing.Size(226, 59);
            this.buttonPromote.TabIndex = 23;
            this.buttonPromote.Text = "Promote Dwarf";
            this.buttonPromote.UseVisualStyleBackColor = false;
            this.buttonPromote.Visible = false;
            this.buttonPromote.Click += new System.EventHandler(this.buttonPromote_Click);
            // 
            // buttonDowngrade
            // 
            this.buttonDowngrade.AutoSize = true;
            this.buttonDowngrade.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.buttonDowngrade.Enabled = false;
            this.buttonDowngrade.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDowngrade.Font = new System.Drawing.Font("Matura MT Script Capitals", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDowngrade.ForeColor = System.Drawing.Color.LightGoldenrodYellow;
            this.buttonDowngrade.Location = new System.Drawing.Point(455, 144);
            this.buttonDowngrade.Name = "buttonDowngrade";
            this.buttonDowngrade.Size = new System.Drawing.Size(268, 59);
            this.buttonDowngrade.TabIndex = 24;
            this.buttonDowngrade.Text = "Downgrade Dwarf";
            this.buttonDowngrade.UseVisualStyleBackColor = false;
            this.buttonDowngrade.Visible = false;
            this.buttonDowngrade.Click += new System.EventHandler(this.buttonDowngrade_Click);
            // 
            // buttonRegister
            // 
            this.buttonRegister.AutoSize = true;
            this.buttonRegister.BackColor = System.Drawing.Color.Black;
            this.buttonRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRegister.Font = new System.Drawing.Font("Matura MT Script Capitals", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRegister.ForeColor = System.Drawing.Color.Gold;
            this.buttonRegister.Location = new System.Drawing.Point(12, 12);
            this.buttonRegister.Name = "buttonRegister";
            this.buttonRegister.Size = new System.Drawing.Size(175, 59);
            this.buttonRegister.TabIndex = 25;
            this.buttonRegister.Text = "Registration";
            this.buttonRegister.UseVisualStyleBackColor = false;
            this.buttonRegister.Click += new System.EventHandler(this.buttonRegister_Click);
            // 
            // buttonUsersList
            // 
            this.buttonUsersList.AutoSize = true;
            this.buttonUsersList.BackColor = System.Drawing.Color.Black;
            this.buttonUsersList.Enabled = false;
            this.buttonUsersList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonUsersList.Font = new System.Drawing.Font("Matura MT Script Capitals", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonUsersList.ForeColor = System.Drawing.Color.Gold;
            this.buttonUsersList.Location = new System.Drawing.Point(512, 469);
            this.buttonUsersList.Name = "buttonUsersList";
            this.buttonUsersList.Size = new System.Drawing.Size(211, 59);
            this.buttonUsersList.TabIndex = 26;
            this.buttonUsersList.Text = "List of Users";
            this.buttonUsersList.UseVisualStyleBackColor = false;
            this.buttonUsersList.Visible = false;
            this.buttonUsersList.Click += new System.EventHandler(this.buttonUsersList_Click);
            // 
            // buttonCreateClan
            // 
            this.buttonCreateClan.AutoSize = true;
            this.buttonCreateClan.BackColor = System.Drawing.Color.Black;
            this.buttonCreateClan.Enabled = false;
            this.buttonCreateClan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCreateClan.Font = new System.Drawing.Font("Matura MT Script Capitals", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCreateClan.ForeColor = System.Drawing.Color.Gold;
            this.buttonCreateClan.Location = new System.Drawing.Point(12, 534);
            this.buttonCreateClan.Name = "buttonCreateClan";
            this.buttonCreateClan.Size = new System.Drawing.Size(172, 59);
            this.buttonCreateClan.TabIndex = 27;
            this.buttonCreateClan.Text = "Create Clan";
            this.buttonCreateClan.UseVisualStyleBackColor = false;
            this.buttonCreateClan.Visible = false;
            this.buttonCreateClan.Click += new System.EventHandler(this.buttonCreateClan_Click);
            // 
            // FormMainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::MarekMachlinskiLab4Zad1.Properties.Resources.LoginBackground;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1185, 605);
            this.Controls.Add(this.buttonCreateClan);
            this.Controls.Add(this.buttonUsersList);
            this.Controls.Add(this.buttonRegister);
            this.Controls.Add(this.buttonDowngrade);
            this.Controls.Add(this.buttonPromote);
            this.Controls.Add(this.buttonLogOut);
            this.Controls.Add(this.buttonClanMembers);
            this.Controls.Add(this.buttonClansList);
            this.Controls.Add(this.buttonAllianceWith);
            this.Controls.Add(this.buttonWarWith);
            this.Controls.Add(this.buttonBrokeAlliance);
            this.Controls.Add(this.buttonPeace);
            this.Controls.Add(this.buttonDeclareAlliance);
            this.Controls.Add(this.buttonWar);
            this.Controls.Add(this.labelClan);
            this.Controls.Add(this.buttonAddNewMember);
            this.Controls.Add(this.buttonDeleteMember);
            this.Controls.Add(this.buttonListOfCertificates);
            this.Controls.Add(this.buttonCreateCertificate);
            this.Controls.Add(this.labelLevel);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.dataGridViewTables);
            this.Controls.Add(this.buttonLogIn);
            this.Controls.Add(this.labelTittle);
            this.Controls.Add(this.labelPassword);
            this.Controls.Add(this.labelLogin);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.textBoxLogin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dwarven Stone";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTables)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxLogin;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label labelLogin;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.Label labelTittle;
        private System.Windows.Forms.Button buttonLogIn;
        private System.Windows.Forms.DataGridView dataGridViewTables;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelLevel;
        private System.Windows.Forms.Button buttonCreateCertificate;
        private System.Windows.Forms.Button buttonListOfCertificates;
        private System.Windows.Forms.Button buttonDeleteMember;
        private System.Windows.Forms.Button buttonAddNewMember;
        private System.Windows.Forms.Label labelClan;
        private System.Windows.Forms.Button buttonWar;
        private System.Windows.Forms.Button buttonDeclareAlliance;
        private System.Windows.Forms.Button buttonPeace;
        private System.Windows.Forms.Button buttonBrokeAlliance;
        private System.Windows.Forms.Button buttonWarWith;
        private System.Windows.Forms.Button buttonAllianceWith;
        private System.Windows.Forms.Button buttonClansList;
        private System.Windows.Forms.Button buttonClanMembers;
        private System.Windows.Forms.Button buttonLogOut;
        private System.Windows.Forms.Button buttonPromote;
        private System.Windows.Forms.Button buttonDowngrade;
        private System.Windows.Forms.Button buttonRegister;
        private System.Windows.Forms.Button buttonUsersList;
        private System.Windows.Forms.Button buttonCreateClan;
    }
}

