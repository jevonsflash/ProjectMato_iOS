using Xamarin.Forms;

namespace ProjectMato.iOS.Model
{
    public class PlaylistInfo : MusicCollectionInfo
    {
        public int PlaylistId { get; set; }

        public bool IsHidden { get; set; }

        public bool IsRemovable { get; set; }

        public ImageSource PlaylistArt { get; set; }

    }
}