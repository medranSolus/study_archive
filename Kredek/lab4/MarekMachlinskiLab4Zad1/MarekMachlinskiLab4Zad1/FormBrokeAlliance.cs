using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace MarekMachlinskiLab4Zad1
{
    /// <summary>
    /// Okno umożliwiające zerwanie sojuszy
    /// </summary>
    public partial class FormBrokeAlliance : Form
    {
        /// <summary>
        /// Obiekt okna nadrzędnego
        /// </summary>
        readonly FormMainWindow parent = null;

        /// <summary>
        /// Inicjalizuję listę klanów w sojuszu z klanem użytkownika
        /// </summary>
        /// <param name="newParent">Okno nadrzędne</param>
        public FormBrokeAlliance(FormMainWindow newParent)
        {
            InitializeComponent();
            parent = newParent;
            if (parent != null)
            {
                List<Tables.Clan> alliedClans = parent.Alliances.GetAll().Where(clan => clan.Clan1Name == parent.CurrentUser.ClanName).Select(clan => clan.Clan2).ToList();
                alliedClans.AddRange(parent.Alliances.GetAll().Where(clan => clan.Clan2Name == parent.CurrentUser.ClanName).Select(clan => clan.Clan1));
                dataGridViewClans.DataSource = alliedClans.Select(clan => new { clan.Name }).ToList();
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
        /// Zrywa sojusz i zamyka okno
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAccept_Click(object sender, EventArgs e)
        {
            List<Tables.Alliance> alliances = parent.Alliances.GetAll().Where(alliance => (alliance.Clan1Name == parent.CurrentUser.ClanName && alliance.Clan2Name == dataGridViewClans.CurrentCell.Value.ToString()) || ((alliance.Clan2Name == parent.CurrentUser.ClanName && alliance.Clan1Name == dataGridViewClans.CurrentCell.Value.ToString()))).ToList();
            if (alliances.Count == 1)
                parent.Alliances.Delete(alliances.First());
            Close();
        }
    }
}
