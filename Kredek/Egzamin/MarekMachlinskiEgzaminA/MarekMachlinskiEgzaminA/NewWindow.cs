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
    public partial class NewWindow : Form
    {
        /// <summary>
        /// Tekst na ekranie
        /// </summary>
        public string LabelText { get; set; } = "";

        /// <summary>
        /// Kontruktor
        /// </summary>
        public NewWindow(string LabelText)
        {
            InitializeComponent();
            label.Text = LabelText;
        }
    }
}
