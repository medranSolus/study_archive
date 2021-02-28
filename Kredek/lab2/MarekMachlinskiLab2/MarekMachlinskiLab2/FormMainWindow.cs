using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarekMachlinskiLab2
{
    public partial class FormMainWindow : Form
    {
        /// <summary>
        /// Nowe okno
        /// </summary>
        FormAbout formAbout = new FormAbout();

        /// <summary>
        /// Konstruktor domyślny
        /// </summary>
        public FormMainWindow()
        {
            InitializeComponent();
            textBoxNotepad.ScrollBars = ScrollBars.Vertical;
        }

        /// <summary>
        /// Przycisk tworzący nowe okno
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonNewWindow_Click(object sender, EventArgs e)
        {
            if (formAbout.IsDisposed)
                formAbout = new FormAbout();
            formAbout.nameTransfer = labelName.Text;
            formAbout.Show();
        }

        /// <summary>
        /// Reakcja na zmianę tekstu, przesłanie do nowego okna
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxTransferText_TextChanged(object sender, EventArgs e)
        {
            if (!formAbout.IsDisposed)
                formAbout.nameTransfer = textBoxTransferText.Text;
        }

        /// <summary>
        /// Dodawanie tekstu do notatnika
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonShow_Click(object sender, EventArgs e)
        {
            Horse horse = new Horse("Jack", "Black");
            textBoxNotepad.Text += horse.ToString() + horse.Move();
        }
    }
}
