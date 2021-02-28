using System;
using System.Windows.Forms;

namespace MarekMachlinskiLab4Zad1
{
    /// <summary>
    /// Umożliwia stworzenie klanu
    /// </summary>
    public partial class FormCreateClan : Form
    {
        /// <summary>
        /// Obiekt okna nadrzędnego
        /// </summary>
        readonly FormMainWindow parent = null;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="newParent">Okno nadrzędne</param>
        public FormCreateClan(FormMainWindow newParent)
        {
            InitializeComponent();
            parent = newParent;
        }

        /// <summary>
        /// Zamyka okno i nic nie robi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Dodaje nowy klan, ustawia jako jego przywódcę obecnego uzytkownika i zamyka okno
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAccept_Click(object sender, EventArgs e)
        {
            if (textBoxName.TextLength > 0)
            {
                if (parent != null)
                {
                    if (parent.Clans.GetById(textBoxName.Text) == null)
                    {
                        parent.Clans.Create(new Tables.Clan() { Name = textBoxName.Text, CreationDate = DateTime.Now });
                        parent.CurrentUser.ClanName = textBoxName.Text;
                        parent.CurrentUser.Clan = parent.Clans.GetById(parent.CurrentUser.ClanName);
                        parent.CurrentUser.RankId = 7;
                        parent.CurrentUser.Rank = parent.Ranks.GetById(parent.CurrentUser.RankId);
                        parent.Users.Update(parent.CurrentUser, parent.CurrentUser.Login);
                        Close();
                    }
                    else
                        MessageBox.Show("There is already clan with that name.");
                }
                else
                    MessageBox.Show("Wrong reference to parent window. Try again later.");
            }
        }
    }
}
