using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace MarekMachlinskiLab4Zad1
{
    /// <summary>
    /// Zawiera pokój z klanami
    /// </summary>
    public partial class FormMakePeace : Form
    {
        /// <summary>
        /// Obiekt okna nadrzędnego
        /// </summary>
        readonly FormMainWindow parent = null;

        /// <summary>
        /// Inicjalizuje listę klanów w wojnie z klanem użytkownika
        /// </summary>
        /// <param name="newParent">Okno nadrzędne</param>
        public FormMakePeace(FormMainWindow newParent)
        {
            InitializeComponent();
            parent = newParent;
            if (parent != null)
            {
                List<Tables.Clan> clansAtWar = parent.Wars.GetAll().Where(clan => clan.Clan1Name == parent.CurrentUser.ClanName).Select(clan => clan.Clan2).ToList();
                clansAtWar.AddRange(parent.Wars.GetAll().Where(clan => clan.Clan2Name == parent.CurrentUser.ClanName).Select(clan => clan.Clan1));
                dataGridViewClans.DataSource = clansAtWar.Select(clan => new { clan.Name }).ToList();
            }
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
        /// Kończy wojnę klanów i zamyka okno
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAccept_Click(object sender, EventArgs e)
        {
            List<Tables.War> wars = parent.Wars.GetAll().Where(war => (war.Clan1Name == parent.CurrentUser.ClanName && war.Clan2Name == dataGridViewClans.CurrentCell.Value.ToString())||((war.Clan2Name == parent.CurrentUser.ClanName && war.Clan1Name == dataGridViewClans.CurrentCell.Value.ToString()))).ToList();
            if(wars.Count == 1)
                parent.Wars.Delete(wars.First());
            Close();
        }
    }
}
