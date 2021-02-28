using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace MarekMachlinskiLab4Zad1
{
    /// <summary>
    /// Okno dodające użytkownika do klanu
    /// </summary>
    public partial class FormAddMember : Form
    {
        /// <summary>
        /// Obiekt okna nadrzędnego
        /// </summary>
        readonly FormMainWindow parent = null;

        /// <summary>
        /// Inicjalizuję listę niezrzeszonych użytkowników
        /// </summary>
        /// <param name="newParent">Okno nadrzędne</param>
        public FormAddMember(FormMainWindow newParent)
        {
            InitializeComponent();
            parent = newParent;
            if (parent != null)
                dataGridViewUsers.DataSource = parent.Users.GetAll().Where(user => user.ClanName == "No Clan").Select(user =>new { Name = user.Login }).ToList();
            else
            {
                MessageBox.Show("Wrong type of parent window!");
                Close();
            }
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
        /// Dodaje nowego członka klanu i zamyka okno
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAccept_Click(object sender, EventArgs e)
        {
            Tables.User user = parent.Users.GetById(dataGridViewUsers.CurrentCell.Value.ToString());
            user.ClanName = parent.CurrentUser.ClanName;
            user.Clan = parent.CurrentUser.Clan;
            user.RankId = 2;
            user.Rank = parent.Ranks.GetById(user.RankId);
            parent.Users.Update(user, user.Login);
            Close();
        }
    }
}
