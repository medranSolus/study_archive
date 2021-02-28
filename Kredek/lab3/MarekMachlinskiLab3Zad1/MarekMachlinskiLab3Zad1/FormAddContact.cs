using System;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace MarekMachlinskiLab3Zad1
{
    /// <summary>
    /// Obsługuje dodanie do tabeli Contacts nowej pozycji
    /// </summary>
    public partial class FormAddContact : Form
    {
        /// <summary>
        /// Konstruktor inicjalizujący okno
        /// </summary>
        public FormAddContact()
        {
            InitializeComponent();
            monthCalendarBornPicker.MaxDate = DateTime.Now;
        }

        /// <summary>
        /// Dodaje do tabeli Contacts nową pozycję i zamyka okno
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void buttonAccept_Click(object sender, EventArgs e)
        {
            if(textBoxName.Text.Length > 0 && textBoxNumber.Text.Length == 9 && monthCalendarBornPicker.SelectionStart != DateTime.Today)
            {
                string query = @"INSERT INTO Contacts (Name, Born, PhoneNumber) VALUES (@Name, @Born, @PhoneNumber)";
                using (SqlConnection connection = new SqlConnection($@"Data Source={FormMain.DataSource}; database=Organiser; Trusted_Connection=yes;"))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = textBoxName.Text;
                    command.Parameters.Add("@Born", SqlDbType.Date).Value = monthCalendarBornPicker.SelectionStart.Date;
                    command.Parameters.Add("@PhoneNumber", SqlDbType.NChar).Value = textBoxNumber.Text;
                    await connection.OpenAsync();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                Close();
            }
        }

        /// <summary>
        /// Sprawdza czy wpisano do pola numeru telefonu cyfry
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxNumber_TextChanged(object sender, EventArgs e)
        {
            if(!Char.IsDigit(textBoxNumber.Text.Last()))
            {
                MessageBox.Show("Wrong number, enter only digits.");
                textBoxNumber.Text = textBoxNumber.Text.Remove(textBoxNumber.Text.Length - 1);
            }
        }

        /// <summary>
        /// Zamyka okno nie dodając nic do tabeli Contacts
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
