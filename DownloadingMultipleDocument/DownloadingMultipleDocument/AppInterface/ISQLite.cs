using System;
using SQLite;

namespace DownloadingMultipleDocument
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}

