using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace MarekMachlinskiLab4Zad1
{
    /// <summary>
    /// Okno dodające inne klany jako sojuszników
    /// </summary>
    public partial class FormMakeAlliance : Form
    {
        /// <summary>
        /// Obiekt okna nadrzędnego
        /// </summary>
        readonly FormMainWindow parent = null;

        /// <summary>
        /// Inicjalizuję listę klanów do zawarcia sojuszu
        /// </summary>
        /// <param name="newParent">Okno nadrzędne</param>
        public FormMakeAlliance(FormMainWindow newParent)
        {
            InitializeComponent();
            parent = newParent;
            if (parent != null)
                dataGridViewClans.DataSource = parent.Clans.GetAll().Where(clan => clan.Name != "No Clan" && clan.Name != parent.CurrentUser.ClanName && parent.Wars.GetAll().Where(war => war.Clan1Name == parent.CurrentUser.ClanName && war.Clan2Name == clan.Name).ToList().Count == 0 && parent.Wars.GetAll().Where(war => war.Clan2Name == parent.CurrentUser.ClanName && war.Clan1Name == clan.Name).ToList().Count == 0 && parent.Alliances.GetAll().Where(alliance => alliance.Clan1Name == parent.CurrentUser.ClanName && alliance.Clan2Name == clan.Name).ToList().Count == 0 && parent.Alliances.GetAll().Where(alliance => alliance.Clan2Name == parent.CurrentUser.ClanName && alliance.Clan1Name == clan.Name).ToList().Count == 0).Select(clan => new { clan.Name }).ToList();
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
        /// Tworzy sojusz z klanem i zamyka okno
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAccept_Click(object sender, EventArgs e)
        {
            parent.Alliances.Create(new Tables.Alliance() { Clan1 = parent.CurrentUser.Clan, Clan2 = parent.Clans.GetById(dataGridViewClans.CurrentCell.Value.ToString()) });
            Close();
        }
    }
}
