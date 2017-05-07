using ProjectMato.iOS.Server;
using SQLite.Net.Attributes;

namespace ProjectMato.iOS.Model
{
    /// <summary>
    /// Defines the structure of the Background table for the SQLite database 
    /// </summary>
    [Table("BackgroundTable")]
    public class BackgroundTable : BaseTable
    {
        [PrimaryKey, AutoIncrement]
        public int BackgroundId { get; set; }

        public static class Properties
        {
            public const string BackgroundId = "BackgroundId";
            public const string Title = "Title";
            public const string Name = "Name";
            public const string Img = "Img";
            public const string IsSel = "IsSel";
            public const string ColorA = "ColorA";
            public const string ColorB = "ColorB";
            public const string ColorC = "ColorC";
        }

        public BackgroundTable()
        {
            Title = string.Empty;
            Name = string.Empty;
            Img = string.Empty;
            IsSel = false;
        }
        public BackgroundTable(string title, string name, string img, bool isSel, string colorA, string colorB, string colorC)
        {
            Title = title;
            Name = name;
            Img = img;
            IsSel = isSel;
            ColorA = colorA;
            ColorB = colorB;
            ColorC = colorC;
        }

        public string Title { get; set; }
        public string Name { get; set; }
        public string Img { get; set; }
        private bool _isSel;
        public bool IsSel
        {
            get { return _isSel; }
            set
            {
                _isSel = value;
                RaisePropertyChanged(nameof(IsSel));
            }
        }
        public string ColorA { get; set; }
        public string ColorB { get; set; }
        public string ColorC { get; set; }
    }
}