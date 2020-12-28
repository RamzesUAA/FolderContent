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
        public Form1()
        {
            InitializeComponent();
            CreateServer();
        }

        void CreateServer()
        {
            try
            {
                using (Process myProcess = new Process())
                {
                    myProcess.StartInfo.UseShellExecute = false;
                    // You can start any process, HelloWorld is a do-nothing example.
                    myProcess.StartInfo.FileName = @"C:\Users\Roman\Desktop\OS_Project\ServerDemo\bin\Debug\ServerDemo.exe";
                    myProcess.StartInfo.CreateNoWindow = true;
                    myProcess.Start();
                    // This code assumes the process you are starting will terminate itself.
                    // Given that is is started without a window so you cannot terminate it
                    // on the desktop, it must terminate itself or you can do it programmatically
                    // from this application using the Kill method.
                }
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
    }
}
