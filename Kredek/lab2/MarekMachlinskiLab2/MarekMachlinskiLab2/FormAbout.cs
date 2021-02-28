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
    public partial class FormAbout : Form
    {
        /// <summary>
        /// Nazwa do przekazania
        /// </summary>
        public string nameTransfer;

        /// <summary>
        /// Konstruktor domyślny
        /// </summary>
        public FormAbout()
        {
            InitializeComponent();
            CheckForChange();
        }

        /// <summary>
        /// Sprawdza zmiany tekstu
        /// </summary>
        private async void CheckForChange()
        {
            for (; ; )
            {
                await Task.Delay(1);
                if (nameTransfer != labelName.Text)
                    labelName.Text = nameTransfer;
            }
        }
    }
}
