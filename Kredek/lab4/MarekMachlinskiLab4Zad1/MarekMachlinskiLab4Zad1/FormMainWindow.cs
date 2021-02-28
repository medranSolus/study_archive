using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace MarekMachlinskiLab4Zad1
{
    /// <summary>
    /// Główne okno
    /// </summary>
    public partial class FormMainWindow : Form
    {
        /// <summary>
        /// Obiekt do łączenia z bazą danych
        /// </summary>
        private readonly Tables.AppContext context = new Tables.AppContext();

        /// <summary>
        /// Tablica sojuszy klanów
        /// </summary>
        public Repositories.RWRepositoryInt<Tables.Alliance> Alliances { get; set; }

        /// <summary>
        /// Tablica wojen klanów
        /// </summary>
        public Repositories.RWRepositoryInt<Tables.War> Wars { get; set; }

        /// <summary>
        /// Tablica stworzonych dokumentów
        /// </summary>
        public Repositories.RWRepositoryInt<Tables.DocumentCreation> DocumentCreations { get; set; }

        /// <summary>
        /// Tablica klanów
        /// </summary>
        public Repositories.RWRepositoryString<Tables.Clan> Clans { get; set; }

        /// <summary>
        /// Tablica użytkowników
        /// </summary>
        public Repositories.RWRepositoryString<Tables.User> Users { get; set; }

        /// <summary>
        /// Tablica rang użytkowników
        /// </summary>
        public Repositories.RWRepositoryInt<Tables.Rank> Ranks { get; set; }

        /// <summary>
        /// Aktualny użytkownik
        /// </summary>
        internal Tables.User CurrentUser { get; private set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public FormMainWindow()
        {
            InitializeComponent();
            Alliances = new Repositories.RWRepositoryInt<Tables.Alliance>(context);
            Wars = new Repositories.RWRepositoryInt<Tables.War>(context);
            DocumentCreations = new Repositories.RWRepositoryInt<Tables.DocumentCreation>(context);
            Clans = new Repositories.RWRepositoryString<Tables.Clan>(context);
            Users = new Repositories.RWRepositoryString<Tables.User>(context);
            Ranks = new Repositories.RWRepositoryInt<Tables.Rank>(context);
            CurrentUser = null;
        }

        /// <summary>
        /// Wyświetla możliwe opcje dla użytkownika w zależności od jego statusu
        /// </summary>
        private void SetUpRankButtons()
        {
            labelClan.Text = CurrentUser.ClanName;
            labelLevel.Text = CurrentUser.Rank.Name;
            if (CurrentUser.RankId != 1)
                buttonClanMembers.Enabled = buttonClanMembers.Visible = buttonWarWith.Enabled = buttonWarWith.Visible = buttonAllianceWith.Enabled = buttonAllianceWith.Visible = true;
            else
                buttonCreateClan.Enabled = buttonCreateClan.Visible = true;
            if (CurrentUser.RankId > 3)
            {
                buttonAddNewMember.Text = $"Welcome Dwarf to {labelClan.Text}";
                buttonDowngrade.Enabled = buttonDowngrade.Visible = buttonPromote.Enabled = buttonPromote.Visible = buttonAddNewMember.Enabled = buttonAddNewMember.Visible = true;
            }
            if (CurrentUser.RankId > 5)
            {
                buttonDeleteMember.Text = $"Banish Dwarf from {labelClan.Text}";
                buttonDeleteMember.Enabled = buttonDeleteMember.Visible = true;
            }
            if (CurrentUser.RankId == 7)
                buttonBrokeAlliance.Enabled = buttonBrokeAlliance.Visible = buttonDeclareAlliance.Enabled = buttonDeclareAlliance.Visible = buttonPeace.Enabled = buttonPeace.Visible = buttonWar.Enabled = buttonWar.Visible = true;
        }

        /// <summary>
        /// Loguje się za pomocą danych użytkownika
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonLogIn_Click(object sender, EventArgs e)
        {
            if (textBoxLogin.TextLength > 0 && textBoxPassword.TextLength > 0)
            {
                CurrentUser = Users.GetById(textBoxLogin.Text);
                if (CurrentUser != null && CurrentUser.Password == textBoxPassword.Text)
                {
                    BackgroundImage = Properties.Resources.NormalBackground;
                    labelName.Text = textBoxLogin.Text;
                    SetUpRankButtons();
                    labelLogin.Enabled = labelLogin.Visible = labelPassword.Enabled = labelPassword.Visible = labelTittle.Enabled = labelTittle.Visible = buttonLogIn.Enabled = buttonLogIn.Visible = buttonRegister.Enabled = buttonRegister.Visible = textBoxLogin.Enabled = textBoxLogin.Visible = textBoxPassword.Enabled = textBoxPassword.Visible = false;
                    dataGridViewTables.Enabled = dataGridViewTables.Visible = labelName.Enabled = labelName.Visible = labelLevel.Enabled = labelLevel.Visible = labelClan.Enabled = labelClan.Visible = buttonCreateCertificate.Enabled = buttonCreateCertificate.Visible = buttonListOfCertificates.Enabled = buttonListOfCertificates.Visible = buttonClansList.Enabled = buttonClansList.Visible = buttonLogOut.Enabled = buttonLogOut.Visible = buttonUsersList.Enabled = buttonUsersList.Visible = true;
                    textBoxLogin.Text = "";
                }
                else
                    MessageBox.Show("Wrong login or password.");
                textBoxPassword.Text = "";
            }
        }
        
        /// <summary>
        /// Wylogowywuje danego użytkownika
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonLogOut_Click(object sender, EventArgs e)
        {
            CurrentUser = null;
            dataGridViewTables.DataSource = null;
            BackgroundImage = Properties.Resources.LoginBackground;
            buttonCreateCertificate.Enabled = buttonCreateCertificate.Visible = buttonListOfCertificates.Enabled = buttonListOfCertificates.Visible = buttonDowngrade.Enabled = buttonDowngrade.Visible = buttonPromote.Enabled = buttonPromote.Visible = buttonDeleteMember.Enabled = buttonDeleteMember.Visible = buttonAddNewMember.Enabled = buttonAddNewMember.Visible = buttonClanMembers.Enabled = buttonClanMembers.Visible = buttonWarWith.Enabled = buttonWarWith.Visible = buttonAllianceWith.Enabled = buttonAllianceWith.Visible = buttonBrokeAlliance.Enabled = buttonBrokeAlliance.Visible = buttonDeclareAlliance.Enabled = buttonDeclareAlliance.Visible = buttonPeace.Enabled = buttonPeace.Visible = buttonWar.Enabled = buttonWar.Visible = dataGridViewTables.Enabled = dataGridViewTables.Visible = labelName.Enabled = labelName.Visible = labelLevel.Enabled = labelLevel.Visible = labelClan.Enabled = labelClan.Visible = buttonClansList.Enabled = buttonClansList.Visible = buttonLogOut.Enabled = buttonLogOut.Visible = buttonUsersList.Enabled = buttonUsersList.Visible = buttonCreateClan.Enabled = buttonCreateClan.Visible = false;
            labelLogin.Enabled = labelLogin.Visible = labelPassword.Enabled = labelPassword.Visible = labelTittle.Enabled = labelTittle.Visible = buttonLogIn.Enabled = buttonLogIn.Visible = buttonRegister.Enabled = buttonRegister.Visible = textBoxLogin.Enabled = textBoxLogin.Visible = textBoxPassword.Enabled = textBoxPassword.Visible = true;
        }

        /// <summary>
        /// Wyświetla listę stworzonych certifikatów przez użytkownika
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonListOfCertificates_Click(object sender, EventArgs e)
        {
            dataGridViewTables.DataSource = DocumentCreations.GetAll().Where(document => document.UserName==CurrentUser.Login).Select(document => new { document.CreationTime, Name=document.CreatedDocument }).ToList();
        }

        /// <summary>
        /// Tworzy certifikat przynależności użtkownika
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCreateCertificate_Click(object sender, EventArgs e)
        {
            DocumentCreations.Create(new Tables.DocumentCreation() { User = CurrentUser, CreationTime = DateTime.Now, CreatedDocument = $"{CurrentUser.Rank.Name} certificate" });
            using (Document document = new Document(PageSize.A4, 10, 10, 10, 10))
            using (PdfWriter writer = PdfWriter.GetInstance(document, new FileStream($"C:\\Users\\{Environment.UserName}\\Desktop\\Certificate_{CurrentUser.Login}_{DateTime.Now.ToFileTime()}.pdf", FileMode.Create)))
            {
                document.Open();
                BaseFont dwarvenBaseFont = BaseFont.CreateFont(@"Resources\Font.TTF", BaseFont.CP1252, BaseFont.EMBEDDED);
                Image logo = Image.GetInstance(@"Resources\Logo.png");
                logo.ScalePercent(50f);
                logo.Alignment = Element.ALIGN_CENTER;
                PdfPTable table = new PdfPTable(1);
                PdfPCell cellLogo = new PdfPCell();
                cellLogo.AddElement(logo);
                cellLogo.Border = Rectangle.NO_BORDER;
                table.Rows.Add(new PdfPRow(new PdfPCell[] { cellLogo }));
                PdfPCell cellHeader = new PdfPCell();
                Paragraph header = new Paragraph()
                {
                    Alignment = Element.ALIGN_CENTER,
                    Font = new Font(dwarvenBaseFont, 30f, iTextSharp.text.Font.BOLD, new BaseColor(220, 20, 60))
                };
                header.Add(@"Member of The Glorious Dwarven Race");
                header.SetLeading(0f, 1.2f);
                cellHeader.AddElement(header);
                cellHeader.Border = Rectangle.NO_BORDER;
                table.Rows.Add(new PdfPRow(new PdfPCell[] { cellHeader }));
                document.Add(table);
                Paragraph text = new Paragraph()
                {
                    Alignment = Element.ALIGN_LEFT,
                    Font = new Font(dwarvenBaseFont, 20f)
                };
                StringBuilder stringText = new StringBuilder($"{CurrentUser.Login} is from now henceforth known as a member of The Dwarfs and till the end of times should be treated with all the honours.{Environment.NewLine}The belongings of {CurrentUser.Login} is {CurrentUser.ClanName}");
                if (CurrentUser.ClanName == "No Clan")
                    stringText.Append($" as {CurrentUser.Login} is an Outcast and cannot find a place for itself.");
                else
                    stringText.Append($" and should be proud of as it is true honour for The Dwarf to share life with such a great companions.{Environment.NewLine}In this powerful clan {CurrentUser.Login} is known for holding the rank of {CurrentUser.Rank.Name} and it is truly right thing to do for such skilled hands to bear this duties.");
                text.Add(stringText.ToString());
                document.Add(text);
                document.Close();
            }
        }

        /// <summary>
        /// Tworzy nowy sojusz pomiędzy klanami
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDeclareAlliance_Click(object sender, EventArgs e)
        {
            FormMakeAlliance makeAlliance = new FormMakeAlliance(this);
            makeAlliance.ShowDialog();
        }

        /// <summary>
        /// Zrywa sojusz między klanami
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonBrokeAlliance_Click(object sender, EventArgs e)
        {
            FormBrokeAlliance brokeAlliance = new FormBrokeAlliance(this);
            brokeAlliance.ShowDialog();
        }

        /// <summary>
        /// Wywołuje wojnę między klanami
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonWar_Click(object sender, EventArgs e)
        {
            FormDeclareWar declareWar = new FormDeclareWar(this);
            declareWar.ShowDialog();
        }

        /// <summary>
        /// Zawiera pokój między klanami
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonPeace_Click(object sender, EventArgs e)
        {
            FormMakePeace makePeace = new FormMakePeace(this);
            makePeace.ShowDialog();
        }

        /// <summary>
        /// Usuwa członka klanu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDeleteMember_Click(object sender, EventArgs e)
        {
            FormDeleteMember deleteMember = new FormDeleteMember(this);
            deleteMember.ShowDialog();
        }

        /// <summary>
        /// Dodaje użytkownika do klanu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAddNewMember_Click(object sender, EventArgs e)
        {
            FormAddMember addMember = new FormAddMember(this);
            addMember.ShowDialog();
        }

        /// <summary>
        /// Wyświetla listę klanów w wojnie z klanem użytkownika
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonWarWith_Click(object sender, EventArgs e)
        {
            List<Tables.Clan> clansAtWar = Wars.GetAll().Where(clan => clan.Clan1Name == CurrentUser.ClanName).Select(clan => clan.Clan2).ToList();
            clansAtWar.AddRange(Wars.GetAll().Where(clan => clan.Clan2Name == CurrentUser.ClanName).Select(clan => clan.Clan1));
            dataGridViewTables.DataSource = clansAtWar;
        }

        /// <summary>
        /// Wyświetla członków klanu użytkownika
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClanMembers_Click(object sender, EventArgs e)
        {
            dataGridViewTables.DataSource = Users.GetAll().Where(user => user.ClanName == CurrentUser.ClanName).Select(user => new { Name = user.Login, Rank = user.Rank.Name }).ToList();
        }

        /// <summary>
        /// Wyświetla listę klanów
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClansList_Click(object sender, EventArgs e)
        {
            dataGridViewTables.DataSource = Clans.GetAll();
        }
    
        /// <summary>
        /// Tworzy sojusz z wybranym klanem
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAllianceWith_Click(object sender, EventArgs e)
        {
            List<Tables.Clan> alliedClans = Alliances.GetAll().Where(clan => clan.Clan1Name == CurrentUser.ClanName).Select(clan => clan.Clan2).ToList();
            alliedClans.AddRange(Alliances.GetAll().Where(clan => clan.Clan2Name == CurrentUser.ClanName).Select(clan => clan.Clan1));
            dataGridViewTables.DataSource = alliedClans;
        }

        /// <summary>
        /// Zwiększa rangę użytkownika
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonPromote_Click(object sender, EventArgs e)
        {
            FormPromote promote = new FormPromote(this);
            promote.ShowDialog();
        }

        /// <summary>
        /// Degraduje użytkownika
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDowngrade_Click(object sender, EventArgs e)
        {
            FormDowngrade downgrade = new FormDowngrade(this);
            downgrade.ShowDialog();
        }

        /// <summary>
        /// Dodaje użytkownika
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRegister_Click(object sender, EventArgs e)
        {
            FormRegister register = new FormRegister(this);
            register.ShowDialog();
        }

        /// <summary>
        /// Wyświetla listę użytkowników
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonUsersList_Click(object sender, EventArgs e)
        {
            dataGridViewTables.DataSource = Users.GetAll().Select(user => new { Name = user.Login, Clan = user.ClanName, Rank = user.Rank.Name }).ToList();
        }

        /// <summary>
        /// Tworzy nowy klan
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCreateClan_Click(object sender, EventArgs e)
        {
            FormCreateClan createClan = new FormCreateClan(this);
            createClan.ShowDialog();
            if (CurrentUser.ClanName != "No Clan")
            {
                buttonCreateClan.Enabled = buttonCreateClan.Visible = false;
                SetUpRankButtons();
            }
        }
    }
}
