using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace Helper.Utility_Classes
{
    public class FtpHandler
    {
        public static void UploadFileOnFtpServer(string source,string destination)
        {
            try
            {
                string ftpfullpath = destination;
                FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(ftpfullpath);

                ftp.KeepAlive = true;
                ftp.UseBinary = true;
                ftp.Method = WebRequestMethods.Ftp.UploadFile;

                FileStream fs = File.OpenRead(source);
                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);
                fs.Close();

                Stream ftpstream = ftp.GetRequestStream();
                ftpstream.Write(buffer, 0, buffer.Length);
                ftpstream.Close();
            }
            catch (WebException ex)
            {
                String status = ((FtpWebResponse)ex.Response).StatusDescription;
            }

        }

        public static void DownloadFileFromFtpServer()
        {
            Downloader down = new Downloader();    
        }
        
    }
}
