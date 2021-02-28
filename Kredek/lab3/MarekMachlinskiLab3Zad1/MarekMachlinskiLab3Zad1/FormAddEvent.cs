using System;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace MarekMachlinskiLab3Zad1
{
    /// <summary>
    /// Obsługuje dodanie do tabeli Events nowej pozycji
    /// </summary>
    public partial class FormAddEvent : Form
    {
        /// <summary>
        /// Konstruktor wyświetlający w oknie tabele Meetings i Contacts
        /// </summary>
        public FormAddEvent()
        {
            InitializeComponent();
            using (SqlConnection connection = new SqlConnection($@"Data Source={FormMain.DataSource}; database=Organiser; Trusted_Connection=yes;"))
            {
                SqlDataAdapter dataMeetings = new SqlDataAdapter("SELECT Name FROM Meetings", connection);
                SqlDataAdapter dataContacts = new SqlDataAdapter("SELECT Name FROM Contacts", connection);
                DataTable table = new DataTable();
                dataMeetings.Fill(table);
                dataGridViewMeetings.DataSource = table;
                table.Dispose();
                table = new DataTable();
                dataContacts.Fill(table);
                dataGridViewContacts.DataSource = table;
            }
        }

        /// <summary>
        /// Zamyka okno nie dodając nic do tabeli Events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Dodaje do tabeli Events nową pozycję i zamyka okno
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void buttonAccept_Click(object sender, EventArgs e)
        {
            int meetingId = 0, contactId = 0;
            string queryMeetings = @"SELECT Id FROM Meetings WHERE Name=@Name";
            string queryContacts = @"SELECT Id FROM Contacts WHERE Name=@Name";
            string queryInsert = @"INSERT INTO Events (MeetingId, ContactId) VALUES (@MeetingId, @ContactId)";
            using (SqlConnection connection = new SqlConnection($@"Data Source={FormMain.DataSource}; database=Organiser; Trusted_Connection=yes;"))
            using (SqlCommand commandMeetings = new SqlCommand(queryMeetings, connection))
            using (SqlCommand commandContacts = new SqlCommand(queryContacts, connection))
            using (SqlCommand commandInsert = new SqlCommand(queryInsert, connection))
            {
                commandMeetings.Parameters.Add("@Name", SqlDbType.NVarChar).Value = dataGridViewMeetings.CurrentCell.Value;
                commandContacts.Parameters.Add("@Name", SqlDbType.NVarChar).Value = dataGridViewContacts.CurrentCell.Value;
                await connection.OpenAsync();
                using (SqlDataReader reader = commandMeetings.ExecuteReader())
                {
                    if(reader.Read())
                        meetingId = Convert.ToInt32(reader[0]);
                    reader.Close();
                }
                using (SqlDataReader reader = commandContacts.ExecuteReader())
                {
                    if (reader.Read())
                        contactId = Convert.ToInt32(reader[0]);
                    reader.Close();
                }
                if (meetingId != 0 && contactId != 0)
                {
                    commandInsert.Parameters.Add("@MeetingId", SqlDbType.Int).Value = meetingId;
                    commandInsert.Parameters.Add("@ContactId", SqlDbType.Int).Value = contactId;
                    commandInsert.ExecuteNonQuery();
                }
                connection.Close();
            }
            Close();
        }
    }
}
