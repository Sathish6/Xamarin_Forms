using System.IO;
using System.Threading.Tasks;

namespace DownloadingMultipleDocument
{
    public interface IDownloader
    {
        void DownloadFile(string URL);

        Stream ConvertFileStream(string URL);
    }
}
