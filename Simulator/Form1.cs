using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simulator
{
    public partial class Form1 : Form
    {
        Process myProcess = null;
        public Form1()
        {
            InitializeComponent();
            CreateServer();
        }

        void CreateServer()
        {
            try
            {
                myProcess = new Process();
                myProcess.StartInfo.UseShellExecute = false;
                myProcess.StartInfo.FileName = @"C:\Users\Roman\Desktop\OS_Project\ServerDemo\bin\Debug\ServerDemo.exe";
                myProcess.StartInfo.CreateNoWindow = true;
                myProcess.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void btn_Client_Click(object sender, EventArgs e)
        {
            Process.Start("C:/Users/Roman/Desktop/OS_Project/Сlient/bin/Debug/Сlient.exe");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            myProcess.Kill();
        }
    }
}
