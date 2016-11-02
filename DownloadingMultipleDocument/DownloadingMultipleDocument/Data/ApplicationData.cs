using SQLite;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace DownloadingMultipleDocument
{
    class ApplicationData
    {
        static object locker = new object();

        private static SQLiteConnection database;

        public SQLiteConnection AppDataBaseConnection
        {
            get
            {
                return database;
            }
        }
        
        IEnumerable<BookTable> m_homeInfoCollection;
        public List<BookTable> HomeInfoCollection
        {
            get
            {
                if (m_homeInfoCollection != null)
                    return m_homeInfoCollection.ToList();
                else
                    return null;
            }
        }
        public ApplicationData()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();
            m_homeInfoCollection = this.GetItems();
        }

        public IEnumerable<BookTable> GetItems()
        {
            return (from i in database.Table<BookTable>() select i).ToList();
        }


    }
}
