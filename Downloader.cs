using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Net;


namespace Helper.Utility_Classes
{
    public class Downloader : IDisposable

    {

        public Downloader()
        {
            Thread t = new Thread(new ThreadStart(workerThread));
            t.IsBackground = true;
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
        }
      
        public void Dispose()
        {
        }

        private void stopThread()
        {
        }
        private void workerThread()
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://192.168.34.114" + "//Questions//");
            request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

            // This example assumes the FTP site uses anonymous logon.

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);

            while (!reader.EndOfStream)
            {
                String filename = reader.ReadLine();

                //you now have the file name, you can use it to download this specific file
                string[] file = filename.Split(' ');
                DownloadFile("ftp://192.168.34.114" + "//Questions//", file[file.Length - 1], "F:\\");
            }

            Console.WriteLine("Directory List Complete, status {0}", response.StatusDescription);

            reader.Close();
            response.Close();

        }
        public static void DownloadFile(string FtpUrl, string FileNameToDownload, string tempDirPath)
        {
            string ResponseDescription = "";
            string PureFileName = new FileInfo(FileNameToDownload).Name;
            string DownloadedFilePath = tempDirPath + "/" + PureFileName;
            string downloadUrl = String.Format("{0}{1}", FtpUrl, FileNameToDownload);
            FtpWebRequest req = (FtpWebRequest)FtpWebRequest.Create(downloadUrl);
            req.Method = WebRequestMethods.Ftp.DownloadFile;
            req.UseBinary = true;
            req.Proxy = null;
            try
            {
                FtpWebResponse response = (FtpWebResponse)req.GetResponse();
                Stream stream = response.GetResponseStream();
                byte[] buffer = new byte[2048];
                FileStream fs = new FileStream(DownloadedFilePath, FileMode.Create);
                int ReadCount = stream.Read(buffer, 0, buffer.Length);
                while (ReadCount > 0)
                {
                    fs.Write(buffer, 0, ReadCount);
                    ReadCount = stream.Read(buffer, 0, buffer.Length);
                }
                ResponseDescription = response.StatusDescription;
                fs.Close();
                stream.Close();
            }
            catch (Exception e)
            {
            }
        }

    }
}
