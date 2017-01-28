using System.Collections.Generic;
using Xamarin.Forms;

namespace ProjectMato.iOS
{
    public abstract class MusicCollectionInfo
    {
        public string Title
        {
            get;
            set;
        }

        public string GroupHeader { get; set; }

        public IEnumerable<MusicInfo> Musics { get; set; }

    }
}