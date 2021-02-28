using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace MarekMachlinskiLab4Zad1
{
    /// <summary>
    /// Okno do usuwania członków klanu
    /// </summary>
    public partial class FormDeleteMember : Form
    {
        /// <summary>
        /// Obiekt okna nadrzędnego
        /// </summary>
        readonly FormMainWindow parent = null;

        /// <summary>
        /// Inicjuje listę członków klanu
        /// </summary>
        /// <param name="newParent">Okno nadrzędne</param>
        public FormDeleteMember(FormMainWindow newParent)
        {
            InitializeComponent();
            parent = newParent;
            if (parent != null)
                dataGridViewUsers.DataSource = parent.Users.GetAll().Where(user => user.ClanName == parent.CurrentUser.ClanName  && user.RankId < parent.CurrentUser.RankId).Select(user => new { Name = user.Login, Rank = user.Rank.Name }).ToList();
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
        /// Usuwa członka i zamyka okno
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAccept_Click(object sender, EventArgs e)
        {
            Tables.User user = parent.Users.GetById(dataGridViewUsers.CurrentCell.Value.ToString());
            user.ClanName = "No Clan";
            user.Clan = parent.Clans.GetById(user.ClanName);
            user.RankId = 1;
            user.Rank = parent.Ranks.GetById(user.RankId);
            parent.Users.Update(user, user.Login);
            Close();
        }
    }
}
