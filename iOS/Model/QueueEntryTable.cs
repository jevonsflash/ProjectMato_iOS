using System;
using System.Collections.Generic;
using System.Text;
using ProjectMato.iOS.Server;
using SQLite.Net.Attributes;

namespace ProjectMato.iOS.Model
{

    [Table("QueueEntryTable")]
    public class QueueEntryTable : BaseTable
    {
        [PrimaryKey, AutoIncrement]
        public int QueueEntryId { get; set; }

        public static class Properties
        {
            public const string Rank = "Rank";
            public const string QueueEntryId = "QueueEntryId";
            public const string MusicTitle = "MusicTitle";
        }

        public QueueEntryTable()
        {
            Rank = 0;
            MusicTitle = string.Empty;
        }

        public QueueEntryTable(string musicTitle,int rank)
        {
            MusicTitle = musicTitle;
            Rank = rank;
        }

        public int Rank { get; set; }

        public string MusicTitle { get; set; }


    }
}
