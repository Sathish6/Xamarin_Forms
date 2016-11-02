using SQLite;

namespace DownloadingMultipleDocument
{
    public class BookTable
    {
        public BookTable()
        {

        }
        [PrimaryKey, AutoIncrement]
        public int SNO { get; set;}
        public string BookName { get; set; }
        public string URL { get; set; }
        public bool Downloaded { get; set; }

        
    }
}
