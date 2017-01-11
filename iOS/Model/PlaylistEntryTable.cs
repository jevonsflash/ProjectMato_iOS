using SQLite.Net.Attributes;

namespace ProjectMato.iOS.Model
{
    /// <summary>
    /// Defines the structure of the PlaylistEntry table for the SQLite database 
    /// 
    /// Playlist is stored by having a Playlist Table that tracks ID and Name, and then a linked list for each Playlist
    /// whose elements are all stored here
    /// </summary>
    [Table("PlaylistEntryTable")]
    public class PlaylistEntryTable : BaseTable
    {
        [PrimaryKey, AutoIncrement]
        public int RowId { get; set; }

        public static class Properties
        {
            public const string RowId = "RowId";

            public const string PlaylistId = "PlaylistId";
            public const string SongId = "SongId";
            public const string MusicTitle = "MusicTitle";
   
        }

        public PlaylistEntryTable()
        {
            PlaylistId = 0;
            SongId = 0;
            MusicTitle = string.Empty;
        }

        public PlaylistEntryTable(int playlistId, string musicTitle)
        {
            PlaylistId = playlistId;
            SongId = 0;
            MusicTitle = musicTitle;

        }

        [Indexed]
        public int PlaylistId { get; set; }

        public int SongId { get; set; }
        public string MusicTitle { get; set; }

    }
}
