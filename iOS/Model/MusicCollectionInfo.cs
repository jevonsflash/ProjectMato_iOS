using System.Collections.Generic;
using Xamarin.Forms;

namespace ProjectMato.iOS
{
    public abstract class MusicCollectionInfo : IBasicInfo
    {
        public int Id
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public string GroupHeader { get; set; }

        public IEnumerable<MusicInfo> Musics { get; set; }

    }
}