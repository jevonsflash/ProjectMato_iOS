
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using System.ComponentModel;
using System.Drawing;
using ProjectMato.iOS.Controls;
using ProjectMato.iOS.Renders;
using XLabs.Forms.Controls;

[assembly:ExportRenderer(typeof(CarouselView), typeof(CarouselRenderer))]

namespace ProjectMato.iOS.Renders
{
    public sealed class CarouselRenderer : ExtendedScrollViewRenderer
    {
        UIScrollView _native;

        public CarouselRenderer()
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
            e.NewElement.PropertyChanged += ElementPropertyChanged;
        }

        private void _native_DecelerationEnded(object sender, EventArgs e)
        {
            ((CarouselView)Element).OnFlippedEventTrigger();
        }

        void NativeScrolled(object sender, EventArgs e)
        {
            var center = _native.ContentOffset.X + (_native.Bounds.Width / 2);
            ((CarouselView)Element).RealTimeIndex = ((int)center) / ((int)_native.Bounds.Width);
        }

        void ElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == CarouselView.SelectedIndexProperty.PropertyName && !Dragging)
            {
                ScrollToSelection(false);
            }
        }

        void ScrollToSelection(bool animate)
        {
            if (Element == null)
                return;

            _native.SetContentOffset(new CoreGraphics.CGPoint
                (_native.Bounds.Width *
                    Math.Max(0, ((CarouselView)Element).SelectedIndex),
                    _native.ContentOffset.Y),
                animate);
        }

        public override void Draw(CoreGraphics.CGRect rect)
        {
            base.Draw(rect);
            ScrollToSelection(false);
        }
    }
}