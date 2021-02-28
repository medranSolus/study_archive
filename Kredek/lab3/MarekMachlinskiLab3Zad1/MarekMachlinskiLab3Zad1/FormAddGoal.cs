using System;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace MarekMachlinskiLab3Zad1
{
    /// <summary>
    /// Obsługuje dodanie do tabeli LifeGoals nowej pozycji
    /// </summary>
    public partial class FormAddGoal : Form
    {
        /// <summary>
        /// Inicjalizuje okno
        /// </summary>
        public FormAddGoal()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Zamyka okno nie robiąc nic
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Dodaje do tabeli LifeGoals nową pozycję i zamyka okno
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void buttonAccept_Click(object sender, EventArgs e)
        {
            if (textBoxName.Text.Length > 0 && textBoxDescription.Text.Length > 0)
            {
                string query = @"INSERT INTO LifeGoals (Name, Details, Completed) VALUES (@Name, @Details, 0)";
                using (SqlConnection connection = new SqlConnection($@"Data Source={FormMain.DataSource}; database=Organiser; Trusted_Connection=yes;"))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = textBoxName.Text;
                    command.Parameters.Add("@Details", SqlDbType.NVarChar).Value = textBoxDescription.Text;
                    await connection.OpenAsync();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                Close();
            }
        }
    }
}
