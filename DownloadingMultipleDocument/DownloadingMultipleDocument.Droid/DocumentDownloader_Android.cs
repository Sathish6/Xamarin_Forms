using DownloadingMultipleDocument.Droid;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using Xamarin.Forms;

[assembly: Dependency(typeof(DocumentDownloader_Android))]

namespace DownloadingMultipleDocument.Droid
{
    public class DocumentDownloader_Android : IDownloader
    {
        WebClient m_webClient = new WebClient();
        ProgressBar downloadProg = new ProgressBar();
        public DocumentDownloader_Android()
        {

        }

        public void DownloadFile(string URL)
        {
            var ContentPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var fullFilename = Path.Combine(ContentPath, URL.Split('/').Last());
            if (!File.Exists(fullFilename))
            {
            ThreadPool.QueueUserWorkItem((object state) =>
            {
                var wc = new WebClient();
                wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(webClient_DownloadProgressChanged);
                wc.DownloadFileCompleted += new AsyncCompletedEventHandler(webClient_DownloadFileCompleted);
                wc.DownloadFileAsync(new Uri(URL), fullFilename);
            });
            }
        }

        public Stream ConvertFileStream(string URL)
        {
            var ContentPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var fullFilename = Path.Combine(ContentPath, URL.Split('/').Last());
            if (File.Exists(fullFilename))
                return new MemoryStream(File.ReadAllBytes(fullFilename));
            return null;
        }
        private void webClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            DownloadProgressCalculator.Self.DownloadCompleted = true;
        }

        private void webClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            DownloadProgressCalculator.Self.DownloadProgress = ((double)e.BytesReceived / (double)e.TotalBytesToReceive);
        }
    }
}