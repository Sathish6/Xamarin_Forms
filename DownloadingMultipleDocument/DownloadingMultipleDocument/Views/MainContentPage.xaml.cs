using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DownloadingMultipleDocument
{
    public partial class MainContentPage : ContentPage
    {
        double width;
        ApplicationData appDataParser;
        private List<BookTable> m_homePageData;
        private bool isDocumentDownloaded;
        public List<BookTable> HomePageDataCollection
        {
            get
            {
                return m_homePageData;
            }
        }
            
        public MainContentPage()
        {
            InitializeComponent();
            width = 500;
          
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            appDataParser = new ApplicationData();
            m_homePageData = appDataParser.HomeInfoCollection;
            int numberOfRows,numberOfColumns;
            if (Device.Idiom == TargetIdiom.Tablet)
            {
                parentGrid.RowSpacing = 20;
                parentGrid.ColumnSpacing = 20;
                int number = 0;
                numberOfRows = (m_homePageData.Count())/3;
                for(int i=0;i<=numberOfRows; i++)
                {
                    for(int j=0;j<=2;j++)
                    {
                        if (number < m_homePageData.Count)
                        {
                            BookTable bookData = m_homePageData[number];
                            TileGrid homeGrid = new TileGrid(bookData);
                            parentGrid.Children.Add(homeGrid, j, i);
                            number++;
                        }
                    }
                }
            }
        }        
    }
}
