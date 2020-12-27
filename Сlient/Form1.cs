using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Сlient
{
    public partial class Form1 : Form
    {
        private long allFilesSize = 0;
        private List<FileInfo> fileInfoList;
        public Form1()
        {
            InitializeComponent();
            dataGridView1.ScrollBars = ScrollBars.Vertical;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            setDataGridView();

            using (FolderBrowserDialog fbd = new FolderBrowserDialog() { Description = "Select your path." })
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    txtPath.Text = fbd.SelectedPath; 
                    fileInfoList  = ServerDemo(fbd.SelectedPath, txtFormatFile.Text);
                }
            }
            SetDataGridWithData();
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

        List<FileInfo> ServerDemo(string path, string extension)
        {
            List<FileInfo> fileInf = new List<FileInfo>();
            foreach (string item in Directory.GetFiles(path))
            {
                var rightExtension = System.IO.Path.GetExtension(item);
                if (rightExtension == extension)
                {
                    fileInf.Add(new FileInfo(item));
                }
            }
            return fileInf;
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtPath_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtFormatFile_TextChanged(object sender, EventArgs e)
        {

        }

        private void formatOfData_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
