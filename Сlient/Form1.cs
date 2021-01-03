using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Сlient
{
    public partial class Form1 : Form
    {
        private long allFilesSize = 0;
        private List<FileInfo> fileInfoList;
        private Socket ClientSocket = null;
        public Form1()
        {
            InitializeComponent();
            dataGridView1.ScrollBars = ScrollBars.Vertical;
        }

        private List<FileInfo> ByteArrayToObject(byte[] arrBytes)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            List<FileInfo> obj = (List<FileInfo>)binForm.Deserialize(memStream);
            return obj;
        }

        private bool connected = false;
        private bool openPressed = false;
        

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (!connected)
            {
                btn_Connect_Click( sender, e);
            }
         
            setDataGridView();

            using (FolderBrowserDialog fbd = new FolderBrowserDialog() {Description = "Select your path."})
            {
                if ( fbd.ShowDialog() == DialogResult.OK)
                {
                    txtPath.Text = fbd.SelectedPath;
                    string path = fbd.SelectedPath;

                    string messageFromClient = path + " " + txtFormatFile.Text;

                    ClientSocket.Send(System.Text.Encoding.ASCII.GetBytes(messageFromClient), 0,
                        messageFromClient.Length, SocketFlags.None);

                    byte[] MsgFromServer = new byte[9999];
                    int size = ClientSocket.Receive(MsgFromServer);

                    fileInfoList = ByteArrayToObject(MsgFromServer);
                    SetDataGridWithData();
                }
            }
        }

        void SetDataGridWithData()
        {
            int number = 0;
            
            foreach (var fileInfo in fileInfoList)
            {
                dataGridView1.Rows.Add(
                    new object[]
                    {
                        ++number, fileInfo.Name, fileInfo.CreationTime
                    });
                allFilesSize += fileInfo.Length;
            }
            allFilesSize_label.Text = "Size of all files: "+ (allFilesSize/1024 ).ToString() + " KB";
        }

    
        private void setDataGridView()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.ColumnCount = 3;
            dataGridView1.Columns[0].Width = 40;
            dataGridView1.Columns[1].Width = 335;
            dataGridView1.Columns[2].Width = 332;
            dataGridView1.ColumnHeadersVisible = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.Columns[0].Name = "№";
            dataGridView1.Columns[1].Name = "Name";
            dataGridView1.Columns[2].Name = "Creation Time";
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font =
                new Font(dataGridView1.Font, FontStyle.Bold);
            dataGridView1.ShowEditingIcon = false;
            dataGridView1.AutoResizeRows(
                DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders);
            dataGridView1.AllowUserToAddRows = false;
        }

        private void btn_Connect_Click(object sender, EventArgs e)
        {
            int port = 13000;
            string IpAddress = "127.0.0.1";
            ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(IpAddress), port);
            ClientSocket.Connect(ep);
            connected = true;
        }
    }
}
