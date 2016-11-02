
using System;
using Xamarin.Forms;

namespace DownloadingMultipleDocument
{
    public partial class DescriptionPage : ContentPage
    {
        private double _width = 0;
        private double _height = 0;
        private string m_documentDownloadURL = string.Empty;
        private bool m_isDownload;
        private bool m_isNew;
        private string m_documentName;
        private BookTable m_homePageData;
        internal ApplicationData AppDataConnection;
        public DescriptionPage(BookTable homePageData)
        {
            InitializeComponent();
            AppDataConnection = new ApplicationData();
            this.m_homePageData = homePageData;
            m_documentDownloadURL = m_homePageData.URL;
            m_isDownload = m_homePageData.Downloaded;
            m_documentName = m_homePageData.BookName;
            if(m_isDownload)
            {
                ViewDownLoad.Text = "VIEW";
            }
            downloadProgress.IsVisible = false;
            DownloadProgressCalculator.Self.PropertyChanged += PropertyChange;
            
        }

        private void PropertyChange(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "DownloadProgress")
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    downloadProgress.Progress = (double)DownloadProgressCalculator.Self.DownloadProgress;
                });
            }
            if (e.PropertyName == "DownloadCompleted")
            {
                if (DownloadProgressCalculator.Self.DownloadCompleted)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        downloadProgress.IsVisible = false;

                        var documentStream = DependencyService.Get<IDownloader>().ConvertFileStream(m_documentDownloadURL);
                        if (!m_homePageData.Downloaded)
                        {
                            ViewDownLoad.Text = "VIEW";
                            ViewDownLoad.IsVisible = true;
                            m_isDownload = true;
                            m_homePageData.Downloaded = true;
                            UpdateHomePageTable(m_homePageData);
                        }
                    });
                }
            }
        }

        private void UpdateHomePageTable(BookTable page)
        {
            AppDataConnection.AppDataBaseConnection.Update(page);
            AppDataConnection.AppDataBaseConnection.Commit();
        }

        
        private void SetAuthorName(string authorName)
       {
            this.authorName.Text = "by "+authorName;
            this.authorName.FontSize= Device.GetNamedSize(NamedSize.Medium, typeof(Label));
            this.authorName.TextColor = Color.Gray;
        }

        
        private void SetThumbnailImage(ImageSource thumbnailImage)
        {
            this.image.Source = thumbnailImage;
            this.image.Aspect = Aspect.AspectFit;

        }
        private void OnClicked(object sender, EventArgs e)
        {
            var s = sender as Button;
            bool isDownLoad;
            if (ViewDownLoad.Text == "DOWNLOAD")
            {
                downloadProgress.IsVisible = true;
                isDownLoad = true;
                ViewDownLoad.IsVisible = false;
                DependencyService.Get<IDownloader>().DownloadFile(m_documentDownloadURL);

            }
        }
    }
}
