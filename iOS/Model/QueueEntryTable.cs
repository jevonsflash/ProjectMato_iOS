using System;
using System.Collections.Generic;
using System.Text;
using SQLite.Net.Attributes;

namespace ProjectMato.iOS.Model
{

    [Table("QueueEntryTable")]
    public class QueueEntryTable : BaseTable
    {
        [PrimaryKey, AutoIncrement]
        public int RowId { get; set; }

        public static class Properties
        {
            public const string RowId = "RowId";

            public const string Rank = "Rank";
            public const string SongId = "SongId";
            public const string MusicTitle = "MusicTitle";

        }

        public QueueEntryTable()
        {

            SongId = 0;
            MusicTitle = string.Empty;
        }

        public QueueEntryTable(string musicTitle)
        {

            SongId = 0;
            MusicTitle = musicTitle;

        }

        [Indexed]
        public int SongId { get; set; }

        public int Rank { get; set; }

        public string MusicTitle { get; set; }


    }
}
