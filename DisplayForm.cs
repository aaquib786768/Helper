using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Helper
{
    public partial class DisplayForm : Form
    {
        string[] fileList;
        int imgIndex;
        string path = MainForm.backupFtp;
        
        public DisplayForm()
        {
            InitializeComponent();
            lblWaterMark.BackColor = Color.Transparent;
            
        }

        public void RefereshImgList()
        {
            fileList = Directory.GetFiles(path);
            imgIndex = 0;
            pictureBox1.ImageLocation = fileList[0];
            lblWaterMark.Text = fileList[0];
        }

        private void DisplayForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Bounds = Screen.PrimaryScreen.Bounds;
            if (MainForm.isAdmin)
            {
                path = MainForm.priorFtp;
            }
            else
            {
                path = MainForm.backupFtp;
            }
            RefereshImgList();
            
        }

        public void ImageNext()
        {
            if (imgIndex == fileList.Length - 1)
            {
                imgIndex = 0;
            }
            else
            {
                imgIndex++;
            }
            pictureBox1.ImageLocation = fileList[imgIndex].ToString();
            lblWaterMark.Text = fileList[imgIndex];
        }

        public void ImagePrevious()
        {
            if (imgIndex == 0)
            {
                imgIndex = fileList.Length - 1;
            }
            else
            {
                imgIndex--;
            }
            pictureBox1.ImageLocation = fileList[imgIndex];
            lblWaterMark.Text = fileList[imgIndex];
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
