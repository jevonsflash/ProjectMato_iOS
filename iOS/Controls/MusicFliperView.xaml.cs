using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ProjectMato.iOS
{
    public partial class MusicFliperView : ContentView
    {
        public MusicFliperView()
        {
            InitializeComponent();
            this.AlbumArtBorder.SizeChanged += AlbumArtBorder_SizeChanged;
        }

        private void AlbumArtBorder_SizeChanged(object sender, EventArgs e)
        {
            AlbumArtImage.HeightRequest = AlbumArtBorder.Height - 10;
            AlbumArtImage.WidthRequest = AlbumArtBorder.Width - 10;

        }
    }
}

