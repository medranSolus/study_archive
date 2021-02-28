using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarekMachlinskiLab3
{
    public partial class FormMain : Form
    {
        /// <summary>
        /// Połączenie z bazą danych
        /// </summary>
        SqlConnection connection;

        public FormMain()
        {
            InitializeComponent();
            connection = new SqlConnection(@"Data Source=MARKBOOK\MEDRANSQL; database=Pizzeria; Trusted_Connection=yes");
        }

        /// <summary>
        /// Wyświetlenie danych w tabeli
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonShow_Click(object sender, EventArgs e)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Pizzas", connection);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridViewPizzas.DataSource = table;
        }

        /// <summary>
        /// Pokazuje dane z kwerendy
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonShowQuery_Click(object sender, EventArgs e)
        {
            SqlDataAdapter adapter = new SqlDataAdapter($"SELECT * FROM Pizzas WHERE Price > 0{textBoxQuery.Text}", connection);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridViewPizzas.DataSource = table;
        }
    }
}
