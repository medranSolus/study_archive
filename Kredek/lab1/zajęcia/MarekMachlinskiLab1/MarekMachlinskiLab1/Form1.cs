using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarekMachlinskiLab1
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Czy zmieniono kolory
        /// </summary>
        bool isColorChanged = false;

        /// <summary>
        /// Czy zmieniono czcionkę
        /// </summary>
        bool isFontChanged = false;

        /// <summary>
        /// Ilość błędnych wypisań dodawania
        /// </summary>
        int count = 0;

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Zmiana czcionki
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void labelName_Click(object sender, EventArgs e)
        {
            if (!isFontChanged)
            {
                labelName.Font = new Font("Old English Text MT", labelName.Font.Size);
                isFontChanged = true;
            }
            else
            {
                labelName.Font = new Font("Arial Narrow", labelName.Font.Size);
                isFontChanged = false;
            }
        }

        /// <summary>
        /// Prycisk zmieniający kolor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonChangeColor_Click(object sender, EventArgs e)
        {
            timerCount.Start();
            if (!isColorChanged)
            {
                this.BackColor = Color.BlanchedAlmond;
                this.buttonChangeColor.BackColor = Color.YellowGreen;
                this.buttonChangeColor.Text = "Kolor zmieniono";
                this.buttonChangeColor.ForeColor = Color.Crimson;
                isColorChanged = true;
            }
            else
            {
                this.BackColor = SystemColors.GrayText;
                this.buttonChangeColor.BackColor = Color.Ivory;
                this.buttonChangeColor.Text = "Change color";
                this.buttonChangeColor.ForeColor = Color.Black;
                isColorChanged = false;
            }
        }

        /// <summary>
        /// Pokazuje komunikat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonShow_Click(object sender, EventArgs e)
        {
            int first = 0;
            int second = 0;
            if (!Int32.TryParse(this.textBoxNumber1.Text, out first) || !Int32.TryParse(this.textBoxNumber2.Text, out second))
                ++count;
            if (count == 5)
            {
                MessageBox.Show("WPISZ COŚ IDIOTO ALBO WON Z UCZELNI!");
                changeColor();
                count = 0;
            }
            else
                MessageBox.Show((first + second).ToString());
        }

        /// <summary>
        /// Zmienia kolor co 10 sekund
        /// </summary>
        private async void changeColor()
        {
            for (int i = 0; i < 15; i++)
            {
                BackColor = Color.BlueViolet;
                await Task.Delay(100);
                BackColor = Color.Red;
                await Task.Delay(100);
            }
            BackColor = SystemColors.GrayText;
        }

        /// <summary>
        /// Zmiana koloru 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerCount_Tick(object sender, EventArgs e)
        {
            ++timerCount.Interval;
            if(timerCount.Interval == 50)
            {
                timerCount.Interval = 1;
                labelName.BackColor = Color.Black;
            }
        }
    }
}
