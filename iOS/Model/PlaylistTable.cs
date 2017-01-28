using System.Collections.Generic;
using ProjectMato.iOS.Server;
using SQLite.Net.Attributes;

namespace ProjectMato.iOS.Model
{
    /// <summary>
    /// Defines the structure of the Playlist table for the SQLite database 
    ///
    /// Playlist is stored by having a this Playlist Table that tracks ID and Name, and then a linked list for each Playlist
    /// whose elements are all stored in the PlaylstEntry Table
    /// </summary>
    [Table("PlaylistTable")]
    public class PlaylistTable : BaseTable
    {
        [PrimaryKey, AutoIncrement]
        public int PlaylistId { get; set; }

        public static class Properties
        {
            public const string RowId = "RowId";
            public const string PlaylistId = "PlaylistId";
            public const string Name = "Name";
            public const string IsHidden = "IsHidden";
            public const string IsRemovable = "IsHidden";
        }

        public PlaylistTable()
        {
            Name = string.Empty;
            IsHidden = false;
            IsRemovable = true;
        }

        public PlaylistTable(string name, bool isHidden, bool isRemovable)
        {
            Name = name;
            IsHidden = isHidden;
            IsRemovable = isRemovable;
        }

        public string Name { get; set; }

        public bool IsHidden { get; set; }

        public bool IsRemovable { get; set; }
    }
}
