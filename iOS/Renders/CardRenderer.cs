
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using System.ComponentModel;
using System.Drawing;
using ProjectMato.iOS.Controls;
using ProjectMato.iOS.Renders;
using XLabs.Forms.Controls;

[assembly: ExportRenderer(typeof(GeneralCardView), typeof(CardRenderer))]

namespace ProjectMato.iOS.Renders
{
    public sealed class CardRenderer : ExtendedScrollViewRenderer
    {
        UIScrollView _native;
        public CardRenderer()
        {
            PagingEnabled = true;
            ShowsHorizontalScrollIndicator = false;

        }
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
                return;

            _native = (UIScrollView)NativeView;
            _native.Scrolled += NativeScrolled;

            _native.DecelerationEnded += _native_DecelerationEnded;
        }
        private void _native_DecelerationEnded(object sender, EventArgs e)
        {
            ((GeneralCardView)Element).OnFlippedEventTrigger();
        }

        void NativeScrolled(object sender, EventArgs e)
        {
            var center = _native.ContentOffset.X + (_native.Bounds.Width / 2);
            ((GeneralCardView)Element).RealTimeIndex = ((int)center) / ((int)_native.Bounds.Width);
        }


    }
}