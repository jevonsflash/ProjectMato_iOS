using ProjectMato.iOS.Server;
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
        public int PlaylistEntryId { get; set; }

        public static class Properties
        {
            public const string Rank = "Rank";
            public const string PlaylistEntryId = "PlaylistEntryId";
            public const string PlaylistId = "PlaylistId";
            public const string MusicTitle = "MusicTitle";
        }

        public PlaylistEntryTable()
        {
            Rank = 0;
            PlaylistId = 0;
            MusicTitle = string.Empty;
        }

        public PlaylistEntryTable(int playlistId, string musicTitle, int rank)
        {
            PlaylistId = playlistId;
            MusicTitle = musicTitle;
            Rank = rank;
        }
        public int Rank { get; set; }

        public int PlaylistId { get; set; }

        public string MusicTitle { get; set; }

    }
}
