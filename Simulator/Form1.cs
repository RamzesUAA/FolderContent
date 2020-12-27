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
        }

        private void btn_Client_Click(object sender, EventArgs e)
        {
            Process.Start("C://Users//Roman//Desktop//OS_Project//Сlient//bin//Debug//Сlient.exe");
        }
    }
}
