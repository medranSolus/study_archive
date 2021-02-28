using System;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace MarekMachlinskiLab3Zad1
{
    /// <summary>
    /// Obsługuje skojarzenie procesu z zadaniem i wstawia nową pozycję do tabeli Works
    /// </summary>
    public partial class FormAddWork : Form
    {
        /// <summary>
        /// Wyświetla możliwe do skojarzenia procesy i zadania z tabel Processes i TasksToDo
        /// </summary>
        public FormAddWork()
        {
            InitializeComponent();
            using (SqlConnection connection = new SqlConnection($@"Data Source={FormMain.DataSource}; database=Organiser; Trusted_Connection=yes;"))
            {
                SqlDataAdapter dataProcesses = new SqlDataAdapter("SELECT Name FROM Processes", connection);
                SqlDataAdapter dataTasks = new SqlDataAdapter("SELECT Name FROM TasksToDo", connection);
                DataTable table = new DataTable();
                dataProcesses.Fill(table);
                dataGridViewProcesses.DataSource = table;
                table.Dispose();
                table = new DataTable();
                dataTasks.Fill(table);
                dataGridViewTasks.DataSource = table;
            }
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
        /// Dodaje nową pozycję do tabeli Works i zamyka okno
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void buttonAccept_Click(object sender, EventArgs e)
        {
            int taskId = 0;
            string queryTasks = @"SELECT Id FROM TasksToDo WHERE Name=@Name";
            string queryInsert = @"INSERT INTO Works(ProcessName, TaskToDoId) VALUES (@ProcessName, @TaskToDoId)";
            using (SqlConnection connection = new SqlConnection($@"Data Source={FormMain.DataSource}; database=Organiser; Trusted_Connection=yes;"))
            using (SqlCommand commandTasks = new SqlCommand(queryTasks, connection))
            using (SqlCommand commandInsert = new SqlCommand(queryInsert, connection))
            {
                commandTasks.Parameters.Add("@Name", SqlDbType.NVarChar).Value = dataGridViewTasks.CurrentCell.Value;
                await connection.OpenAsync();
                using (SqlDataReader reader = commandTasks.ExecuteReader())
                {
                    if (reader.Read())
                        taskId = Convert.ToInt32(reader[0]);
                    reader.Close();
                }
                if(taskId > 0)
                {
                    commandInsert.Parameters.Add("@ProcessName", SqlDbType.NVarChar).Value = dataGridViewProcesses.CurrentCell.Value;
                    commandInsert.Parameters.Add("@TaskToDoId", SqlDbType.Int).Value = taskId;
                    commandInsert.ExecuteNonQuery();
                }
                connection.Close();
            }
            Close();
        }
    }
}
