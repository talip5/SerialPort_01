using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Ports;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SerialPort_01
{
    public partial class Form1 : Form
    {
        public int number = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           string[] Ports= System.IO.Ports.SerialPort.GetPortNames();
         
            foreach (var item in Ports)
            {
                comboBox1.Items.Add(item);
            }
         
            button2.Enabled = false;
            button3.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            serialPort1.PortName=comboBox1.Text;
            serialPort1.BaudRate=9600;
            serialPort1.Parity=Parity.None;
            serialPort1.StopBits=StopBits.One;
            serialPort1.DataBits=8;

            serialPort1.Open();

            if (serialPort1.IsOpen)
            {
                button1.Enabled = false;
                  button2.Enabled = true;
                button3.Enabled = true;

            }

            //try
            //{
            //    serialPort1.Open();
            //}
            //catch (Exception ex)
            //{

            //    MessageBox.Show($"seri port")
            //}
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Write(textBox1.Text);
                textBox1.Clear();
            }
        }

        public delegate void verigoster(string s);

        public void textyaz(string s)
        {   
            textBox2.Clear();
            textBox2.Text += s;
            this.Text = number.ToString();
        }
        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string gelenveri = serialPort1.ReadExisting();
            textBox2.Invoke(new verigoster(textyaz),gelenveri);
            number++;
            
        }
    }
}
