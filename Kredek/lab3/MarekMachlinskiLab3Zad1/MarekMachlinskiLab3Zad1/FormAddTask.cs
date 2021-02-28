using System;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace MarekMachlinskiLab3Zad1
{
    /// <summary>
    /// Obsługuje dodanie nowego zadania do tabeli TasksToDo
    /// </summary>
    public partial class FormAddTask : Form
    {
        /// <summary>
        /// Ustawia minimalną datę zadania
        /// </summary>
        public FormAddTask()
        {
            InitializeComponent();
            monthCalendarDeadline.MinDate = DateTime.Now.Date;
        }

        /// <summary>
        /// Zamyka okno i nie robi nic
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Dodaje nowe zadanie do tabeli TasksToDo i zamyka okno
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void buttonAccept_Click(object sender, EventArgs e)
        {
            if (textBoxName.Text.Length > 0)
            {
                string query = @"INSERT INTO TasksToDo (Name, Deadline) VALUES (@Name, @Deadline)";
                using (SqlConnection connection = new SqlConnection($@"Data Source={FormMain.DataSource}; database=Organiser; Trusted_Connection=yes;"))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = textBoxName.Text;
                    command.Parameters.Add("@Deadline", SqlDbType.Date).Value = monthCalendarDeadline.SelectionStart.Date;
                    await connection.OpenAsync();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                Close();
            }
        }
    }
}
