using System;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace MarekMachlinskiLab3Zad1
{
    /// <summary>
    /// Obsługuje dodanie do tabeli Meetings nowej pozycji
    /// </summary>
    public partial class FormAddMeeting : Form
    {
        /// <summary>
        /// Ustawia minimaly zakres daty do wyboru
        /// </summary>
        public FormAddMeeting()
        {
            InitializeComponent();
            dateTimePickerMeetTime.Value = dateTimePickerMeetTime.MinDate = DateTime.Now;
            dateTimePickerMeetTime.Format = DateTimePickerFormat.Custom;
            dateTimePickerMeetTime.CustomFormat = "'Day: 'dd' Month: 'MM' Year: 'yyyy' Time: 'hh':'mm";
            dateTimePickerMeetTime.ShowUpDown = false;
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
        /// Dodaje do tabeli Meetings nową pozycję i zamyka okno
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void buttonAccept_Click(object sender, EventArgs e)
        {
            if (textBoxName.Text.Length > 0)
            {
                string query = @"INSERT INTO Meetings (Name, Location, Date, Time) VALUES (@Name, @Location, @Date, @Time)";
                using (SqlConnection connection = new SqlConnection($@"Data Source={FormMain.DataSource}; database=Organiser; Trusted_Connection=yes;"))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = textBoxName.Text;
                    command.Parameters.Add("@Location", SqlDbType.NVarChar).Value = textBoxLocation.Text;
                    command.Parameters.Add("@Date", SqlDbType.Date).Value = dateTimePickerMeetTime.Value.Date;
                    command.Parameters.Add("@Time", SqlDbType.Time).Value = dateTimePickerMeetTime.Value.TimeOfDay;
                    await connection.OpenAsync();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                Close();
            }
        }
    }
}
