using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace MarekMachlinskiLab4
{
    public partial class FormMainWindow : Form
    {
        /// <summary>
        /// Obiekt umożliwiający łączenie się z bazą danych
        /// </summary>
        private readonly Models.AppContext context = new Models.AppContext();
        /// <summary>
        /// Repozytorium z obiektami gier
        /// </summary>
        public Repositories.RWRepository<Models.Game> Games { get; set; }

        /// <summary>
        /// Konstruktor inicjalizujący repozytorium gier
        /// </summary>
        public FormMainWindow()
        {
            InitializeComponent();
            Games = new Repositories.RWRepository<Models.Game>(context);
        }

        /// <summary>
        /// Kliknięcie przycisku odświeżającego widok bazy danych
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            GetAllDataToDataGridView();
        }

        /// <summary>
        /// Odświeża bazę danych w obiekcie dataGridViewGames
        /// </summary>
        private void GetAllDataToDataGridView()
        {
            dataGridViewGames.DataSource = Games.GetAll().Select(x => new
            {
                Id = x.Id,
                Name = x.Name,
                Producent = x.Producent
            }).ToList();
        }

        /// <summary>
        /// Dodaje nowe pole do bazy danych
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (textBoxName.Text.Length > 0 && textBoxProducent.Text.Length > 0)
            {
                Games.Create(new Models.Game { Name = textBoxName.Text, Producent = textBoxProducent.Text });
                GetAllDataToDataGridView();
            }
        }
    }
}
