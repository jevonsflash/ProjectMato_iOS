using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ProjectMato.iOS.Model;
using SQLite.Net;
using SQLite.Net.Platform.XamarinIOS;
using Xamarin.Forms;

namespace ProjectMato.iOS.Server
{
    /// <summary>
    /// Manages the SQLite database. All updates to the database should be done through this class
    /// </summary>
    public class DatabaseManager
    {


        public SQLiteConnection SqlConnection;

        private static DatabaseManager _current;
        public static DatabaseManager Current
        {
            get
            {
                if (_current == null)
                {
                    _current = new DatabaseManager();
                }

                return _current;
            }
        }
        private SQLiteConnection GetConnection()
        {
            var sqliteFilename = "MatoPlayerDB.db3";
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
            string libraryPath = Path.Combine(documentsPath, "..", "Library"); // Library folder
            var path = Path.Combine(libraryPath, sqliteFilename);



            var conn = new SQLiteConnection(new SQLitePlatformIOS(), path);

            // Return the database connection 
            return conn;
        }
        public void Connect()
        {
            SqlConnection = GetConnection();




            //var result= SqlConnection.Execute(
            //     "select count(*) as 'count' from sqlite_master where type = 'table' and name =  'ArtistTable'");


            try
            {
                SqlConnection.CreateTable<PlaylistTable>();
                SqlConnection.CreateTable<PlaylistEntryTable>();

                SqlConnection.CreateTable<BackgroundTable>();
                SqlConnection.CreateTable<QueueEntryTable>();
                SqlConnection.CreateTable<SettingTable>();

            }
            catch (Exception e)
            {

                throw;
            }




        }

        public void Disconnect()
        {
            if (SqlConnection != null)
            {
                SqlConnection.Close();

                SqlConnection = null;
            }
        }

        #region FETCH



        internal List<PlaylistTable> FetchPlaylists()
        {
            return SqlConnection.Table<PlaylistTable>().ToList<PlaylistTable>();
        }

        internal List<PlaylistEntryTable> FetchPlaylistEntriesForPlaylist(int playlistId)
        {
            return SqlConnection.Query<PlaylistEntryTable>("SELECT * FROM PlaylistEntryTable WHERE PlaylistId = ?", playlistId).ToList<PlaylistEntryTable>();
        }

        internal List<PlaylistEntryTable> FetchPlaylistEntriesForPlaylistAndSongId(int playlistId, int songId)
        {
            return SqlConnection.Query<PlaylistEntryTable>("SELECT * FROM PlaylistEntryTable WHERE PlaylistId = ? AND SongId = ?", playlistId, songId).ToList<PlaylistEntryTable>();
        }

        internal List<PlaylistEntryTable> FetchPlaylistEntriesForPlaylistAndSongTitle(int playlistId, string songTitle)
        {
            return SqlConnection.Query<PlaylistEntryTable>("SELECT * FROM PlaylistEntryTable WHERE PlaylistId = ? AND MusicTitle = ?", playlistId, songTitle).ToList<PlaylistEntryTable>();
        }
        internal List<BackgroundTable> FetchBackgroundItems()
        {
            return SqlConnection.Table<BackgroundTable>().ToList<BackgroundTable>();
        }

        internal List<QueueEntryTable> FetchQueueEntryTables()
        {
            return SqlConnection.Table<QueueEntryTable>().ToList();
        }

        internal List<SettingTable> FetchSettingTables()
        {
            return SqlConnection.Table<SettingTable>().ToList();
        }
        #endregion

        #region ADD

        internal int AddQueueEntryTable(QueueEntryTable queueEntry)
        {
            SqlConnection.Insert(queueEntry);
            return queueEntry.QueueId;
        }

        internal int AddQueueEntryTables(IEnumerable<QueueEntryTable> queueEntrys)
        {
            var result = SqlConnection.InsertAll(queueEntrys);
            return result;
        }

        internal int AddPlaylistEntry(PlaylistEntryTable newPlaylistEntry)
        {
            SqlConnection.Insert(newPlaylistEntry);
            return newPlaylistEntry.PlaylistEntryId;
        }

        internal int AddPlaylist(PlaylistTable newPlaylist)
        {
            SqlConnection.Insert(newPlaylist);
            return newPlaylist.PlaylistId;
        }


        internal int AddBackgroundEntry(BackgroundTable entry)
        {
            SqlConnection.Insert(entry);
            return entry.BackgroundId;
        }

        internal int AddSettingTable(SettingTable setting)
        {
            SqlConnection.Insert(setting);
            return setting.SettingId;
        }

        #endregion



        private string PrepareStringForLike(string query)
        {
            return '%' + query.ToLower() + '%';
        }

        #region QUERY


        internal BackgroundTable QuerySelectedBackground()
        {
            return SqlConnection.Query<BackgroundTable>("SELECT * FROM BackgroundTable WHERE IsSel =  '1'").FirstOrDefault();
        }

        internal QueueEntryTable QueryQueueEntryByMusicTitle(string musicTitle)
        {
            return SqlConnection.Query<QueueEntryTable>(string.Format("SELECT * FROM QueueEntryTable WHERE MusicTitle = {0}", musicTitle)).FirstOrDefault();
        }

        #endregion

        #region SEARCH

        #endregion

        #region DELETE



        internal int DeletePlaylistEntry(int rowId)
        {
            return SqlConnection.Delete<PlaylistEntryTable>(rowId);
        }

        internal int DeletePlaylist(int playlistId)
        {
            SqlConnection.Query<PlaylistEntryTable>("DELETE FROM PlaylistEntryTable WHERE PlaylistId = ?", playlistId);

            return SqlConnection.Delete<PlaylistTable>(playlistId);
        }

        internal int DeleteQueueItem(int rowId)
        {
            return SqlConnection.Delete<QueueEntryTable>(rowId);
        }


        #endregion

        #region OTHER


        internal void ClearBackground()
        {
            SqlConnection.DeleteAll<BackgroundTable>();
        }

        internal void ClearSetting()
        {
            SqlConnection.DeleteAll<SettingTable>();
        }

        internal void ClearQueue()
        {
            SqlConnection.DeleteAll<QueueEntryTable>();

        }

        internal void Update(BaseTable entry)
        {
            SqlConnection.Update(entry);
        }

        internal void InterchangeQueueTable(string firstMusicTitle, string secondMusicTitle)
        {
            SqlConnection.Execute(String.Format(@"update QueueTable set id=((select id from QueueTable where name='{0}')+(select id from QueueTable where name='{1}')) where name='{0}';
update QueueTable set id = ((select id from QueueTable where name = '{0}') - (select id from QueueTable where name = '{1}')) where name = '{1}';
            update QueueTable set id = ((select id from QueueTable where name = '{0}')-(select id from QueueTable where name = '{1}')) where name = '{0}';
            ",firstMusicTitle,secondMusicTitle),null);
        }
        #endregion
    }
}
