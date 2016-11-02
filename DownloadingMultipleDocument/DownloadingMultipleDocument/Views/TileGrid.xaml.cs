using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace DownloadingMultipleDocument
{
    public partial class TileGrid : Grid
    {
        public string downloadURL = string.Empty;
        private bool isDownload;
        private bool isNewTag;
        private BookTable homePageData;
        public TileGrid(BookTable homePageDetails)
        {
            this.homePageData = homePageDetails;
            InitializeComponent();
            if (Device.Idiom == TargetIdiom.Tablet)
            {
                HeightRequest = 400;
                WidthRequest = 130;
            }
            else
            {
                HeightRequest = 250;
                WidthRequest = 130;
            }

            SetDocumentName(homePageData.BookName);
            downloadURL = homePageData.URL;
            isDownload = homePageData.Downloaded;
        }

       
        internal string DocumentName
        {
            get; set;
        }
        private void SetDocumentName(string name)
        {
           
            documentName.Text = name;

            if (Device.Idiom == TargetIdiom.Tablet)
            {
                documentName.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
            }
            else
            {
                documentName.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label));
            }
            documentName.FontAttributes = FontAttributes.Bold;
        }


        int tapCount = 0;
        void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            Navigation.PushModalAsync(new DescriptionPage(this.homePageData));
        }

    }
}
