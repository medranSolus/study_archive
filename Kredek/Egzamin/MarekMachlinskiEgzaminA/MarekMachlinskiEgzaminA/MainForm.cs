using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarekMachlinskiEgzaminA
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Dane do zadania A2
        /// </summary>
        List<Tuple<string, int, DateTime>> a2Data = new List<Tuple<string, int, DateTime>>();

        /// <summary>
        /// Kontruktor
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            
        }

        /// <summary>
        /// Przesunięcie w lewo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void A1Lewo_Click(object sender, EventArgs e)
        {
            labelName.Location = new Point(labelName.Location.X - 10, labelName.Location.Y);
        }

        /// <summary>
        /// Przesunięcie w prawo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void A1Prawo_Click(object sender, EventArgs e)
        {
            labelName.Location = new Point(labelName.Location.X + 10, labelName.Location.Y);
        }

        /// <summary>
        /// Wyświetlenie nowego okna
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void A3NoweOkno_Click(object sender, EventArgs e)
        {
            NewWindow newWindow = new NewWindow(TextBox.Text);
            newWindow.Show();
        }

        /// <summary>
        /// Wyświetla pracowników z literką 'e'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void A4Pracownicy_Click(object sender, EventArgs e)
        {
            using (NorthwindEntities context = new NorthwindEntities())
            {
                var employees = context.Employees.Where(item => item.FirstName.Contains("e")).ToList();
                dataGridViewMain.DataSource = employees;
            }
        }

        /// <summary>
        /// Wyświetla pracowników bez przełożonych
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void A5BezPrzelozonych_Click(object sender, EventArgs e)
        {
            using (NorthwindEntities context = new NorthwindEntities())
            {
                var employees = context.Employees.Where(item => item.ReportsTo == null).ToList();
                dataGridViewMain.DataSource = employees;
            }
        }

        /// <summary>
        /// Wyświetlanie produktów z ceną w przedziale 9 i 16
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void A6Produkty_Click(object sender, EventArgs e)
        {
            using (NorthwindEntities context = new NorthwindEntities())
            {
                var products = context.Products.Where(item => item.UnitPrice >= 9 && item.UnitPrice <= 16).ToList();
                products.Sort(new Comparison<Product>((product1, product2) => Int32.Parse((product1.UnitPrice < product2.UnitPrice).ToString())));
                dataGridViewMain.DataSource = products;
            }
        }

        /// <summary>
        /// Wyświetla pracowników i liczbę ich zamówień
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void A7PracownicyIZamowienia_Click(object sender, EventArgs e)
        {
            using (NorthwindEntities context = new NorthwindEntities())
            {
                var employees = context.Employees.ToList();
                List<Tuple<string, string, int>> data = new List<Tuple<string, string, int>>();
                foreach (var employee in employees)
                {
                    data.Add(new Tuple<string, string, int>(employee.FirstName, employee.LastName, context.Orders.Where(item => item.EmployeeID == employee.EmployeeID).Count()));
                }
                dataGridViewMain.DataSource = data;
            }
        }

        /// <summary>
        /// Dodaje dane do listy a2Data i je wyświetla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void A2Dodaj_Click(object sender, EventArgs e)
        {
            a2Data.Add(new Tuple<string, int, DateTime>("Position", a2Data.Count, DateTime.Now));
            dataGridViewMain.DataSource = a2Data;
        }
    }
}
