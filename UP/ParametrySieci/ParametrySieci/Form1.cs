using Modbus.Device;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        private void StartGetting()
        {

            using (var client = new TcpClient("169.254.2.10", 502))
            {
                using (var stream = client.GetStream())
                {
                    var voltage = GetRegister(stream, 0x1002);
                    var current = GetRegister(stream, 0x1010);
                    var power = GetRegister(stream, 0x1030)*1000;
                    var cosfi = GetRegister(stream, 0x1020);
                    labelVoltage.Invoke(new Action(() => {
                        labelVoltage.Text = "Voltage:" + voltage.ToString()+"V";
                        labelCurrent.Text = "Current:" + current.ToString()+"A";
                        labelCosFi.Text = "CosFi:" + cosfi.ToString();
                        labelActive.Text = "Power:" + power.ToString()+"W";
                    }));
                
                }
            }
        }

        private static float GetRegister(NetworkStream stream, ushort register)
        {
            byte[] request = { 0x00, 0x01, 0x00, 0x00, 0x00, 0x06, 0x01, 0x03, (byte)(register>>8), (byte)register, 0x00, 0x02 };
            stream.Write(request, 0, request.Length);
            byte[] response = new byte[1024];
            stream.Read(response, 0, response.Length);
            byte[] rev = new byte[4];
            Array.ConstrainedCopy(response, 9, rev, 0, 4);
            Array.Reverse(rev);
            var voltage = BitConverter.ToInt32(rev, 0);
            return voltage/1000.0f;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            Task.Run(()=> {
                while (true)
                {
                    StartGetting();
                    Thread.Sleep(500);
                }
                });
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }
    }
}
