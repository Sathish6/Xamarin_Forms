using System.ComponentModel;
using Xamarin.Forms;

namespace DownloadingMultipleDocument
{
    public class DownloadProgressCalculator: Application, INotifyPropertyChanged
    {
        public static DownloadProgressCalculator Self = new DownloadProgressCalculator();

        public string Url { get; private set; } = "https://s-media-cache-ak0.pinimg.com/originals/7e/a0/30/7ea0300be3d56a04bc3d00fccdbaf5d8.jpg";

        public event PropertyChangedEventHandler PropertyChanged;

        internal double downloadProgress;

        internal bool downloadCompleted;

        public long DownloadTotal { get; set; } = 1;

        public DownloadProgressCalculator()
        {
            Self = this;
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public double DownloadProgress
        {
            get { return downloadProgress; }
            set
            {
                downloadProgress = value;
                OnPropertyChanged("DownloadProgress");
            }
        }

        public bool DownloadCompleted
        {
            get
            {
                return downloadCompleted;
            }
            set
            {
                downloadCompleted = value;
                OnPropertyChanged("DownloadCompleted");
            }
        }
    }
}
