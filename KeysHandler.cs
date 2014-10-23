using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Helper.Utility_Classes
{
    class KeysHandler
    {
        public Form mainForm;
        private DisplayForm displayForm;
        private int count;
        private bool isMainFormHide;
        private bool isDisplayFormHide;
        
        public KeysHandler()
        {
            count = 1;
            isMainFormHide = false;
            isDisplayFormHide = true;
            displayForm = new DisplayForm();
        }

        public void HandleKey(Keys key)
        {
            try
            {
                switch (key)
                {
                    case Keys.W:
                        displayForm.ImageNext();
                        break;
                    case Keys.S:
                        displayForm.ImagePrevious();
                        break;
                    case Keys.C:
                        CaptureImage();
                        break;
                    case Keys.H:
                        ShowHideDisplayForm();
                        break;
                    case Keys.Z:
                        ShowHideMainForm();
                        break;
                    case Keys.D:
                        FtpHandler.DownloadFileFromFtpServer();
                        break;
                    case Keys.R:
                        displayForm.RefereshImgList();
                        break;
                }
            }
            catch (Exception e)
            { 
            }
        }

        
        private void ShowHideDisplayForm()
        {
            try
            {
                if (isDisplayFormHide)
                {
                    isDisplayFormHide = false;
                    displayForm.Show();
                }
                else
                {
                    isDisplayFormHide = true;
                    displayForm.Hide();
                }
            }
            catch(Exception e)
            {
               
            }
        }

        private void ShowHideMainForm()
        {
            if (isMainFormHide)
            {
                isMainFormHide = false;
                mainForm.WindowState = FormWindowState.Minimized;
                mainForm.Show();
                mainForm.WindowState = FormWindowState.Normal;
            }
            else
            {
                isMainFormHide = true;
                mainForm.Hide();
            }
        }

        private void CaptureImage()
        {
            if (MainForm.isAdmin)
            {
                new ScreenCapturer().CaptureScreenToFile(MainForm.priorFtp + (count++) + ".jpg",
                    ImageFormat.Png);
                FtpHandler.UploadFileOnFtpServer(MainForm.priorFtp + (count-1) + ".jpg", MainForm.backupFtp + "//Questions//" + (count -1)+ ".jpg");
            }
        }
    }
}
