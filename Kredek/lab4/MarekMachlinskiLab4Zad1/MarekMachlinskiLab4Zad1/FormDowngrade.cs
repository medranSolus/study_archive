using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace MarekMachlinskiLab4Zad1
{
    /// <summary>
    /// Umożliwia zdegradowanie użytkownika
    /// </summary>
    public partial class FormDowngrade : Form
    {
        /// <summary>
        /// Obiekt okna nadrzędnego
        /// </summary>
        readonly FormMainWindow parent = null;

        /// <summary>
        /// Inicjalizuje listę z krasnoludami do zdegradowania
        /// </summary>
        /// <param name="newParent">Okno nadrzędne</param>
        public FormDowngrade(FormMainWindow newParent)
        {
            InitializeComponent();
            parent = newParent;
            if (parent != null)
                dataGridViewUsers.DataSource = parent.Users.GetAll().Where(user => user.ClanName == parent.CurrentUser.ClanName && user.RankId < parent.CurrentUser.RankId && user.RankId > 2).Select(user => new { Name = user.Login, Rank = user.Rank.Name }).ToList();
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
        /// Degraduje użytkownika i zamyka okno
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAccept_Click(object sender, EventArgs e)
        {
            Tables.User user = parent.Users.GetById(dataGridViewUsers.CurrentCell.Value.ToString());
            user.Rank = parent.Ranks.GetById(user.RankId - 1);
            parent.Users.Update(user, user.Login);
            Close();
        }
    }
}
