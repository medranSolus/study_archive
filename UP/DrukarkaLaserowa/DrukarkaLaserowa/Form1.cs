using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrukarkaLaserowa
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(textBoxInpuput.Text))
            {
                var stream = File.Create("file.txt");
                string marginTop = "\x1B" + "&l" + (int)numericUpDownMarginTop.Value + "E";
                stream.Write(marginTop.Select(x => (byte)x).ToArray(), 0, marginTop.ToArray().Length);
                if (checkBoxBold.Checked)
                {
                    string bold = "\x1B" + "(s5B";
                    stream.Write(bold.Select(x => (byte)x).ToArray(), 0, bold.ToArray().Length);
                }
                if (checkBoxUnderline.Checked)
                {
                    string underline = "\x1B" + "&d0D";
                    stream.Write(underline.Select(x => (byte)x).ToArray(), 0, underline.ToArray().Length);
                }
                stream.Write(textBoxInpuput.Text.Select(x=>(byte)x).ToArray(), 0, textBoxInpuput.Text.ToArray().Length);
                string end = "\x1B" + "E";
                stream.Write(end.Select(x => (byte)x).ToArray(), 0, end.ToArray().Length);
                stream.Close();
                File.Copy("file.txt", "LPT3");
            }
        }

        private void buttonPrintImg_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ShowDialog();
            if(dialog.CheckPathExists)
            {
                using (var stream = File.Create("file"))
                {
                    string code = "\x1B" + "*p30x40Y";
                    stream.Write(code.Select(x => (byte)x).ToArray(), 0, code.ToArray().Length); // Offset
                    code = "\x1B" + "*t100R";
                    stream.Write(code.Select(x => (byte)x).ToArray(), 0, code.ToArray().Length); // Resolution
                    code = "\x1B" + "*r0F";
                    stream.Write(code.Select(x => (byte)x).ToArray(), 0, code.ToArray().Length); // Presentation
                    code = "\x1B" + "*r186T";
                    stream.Write(code.Select(x => (byte)x).ToArray(), 0, code.ToArray().Length); // Height
                    code = "\x1B" + "*r137S";
                    stream.Write(code.Select(x => (byte)x).ToArray(), 0, code.ToArray().Length); // Width
                    code = "\x1B" + "*r1A";
                    stream.Write(code.Select(x => (byte)x).ToArray(), 0, code.ToArray().Length); // Start
                    Bitmap bitmap = new Bitmap(dialog.FileName);
                    ImageConverter ic = new ImageConverter();
                    var a = (byte[])ic.ConvertTo(bitmap, typeof(byte[]));
                    stream.Write(a, 0, a.Length);
                    code = "\x1B" + "*rC";
                    stream.Write(code.Select(x => (byte)x).ToArray(), 0, code.ToArray().Length); // End
                }

                File.Copy("file", "LPT3");
            }
        }
    }
}
