using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using UIKit;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace ProjectMato.iOS.Controls
{
    public class GeneralCardView : ExtendedScrollView
    {
        public event EventHandler<OnFlippedEventArgs> OnFlipped;
        public bool IsFlipEnable=false;
        public GeneralCardView()
        {
            Orientation = ScrollOrientation.Horizontal;
            this.Init();
        }
        public void OnFlippedEventTrigger()
        {
            var onflippedEventArges = new OnFlippedEventArgs();
            if (_flipDirection < 0)
            {
                onflippedEventArges.FlipType = FlipType.后退;
                this.OnFlipped?.Invoke(this, onflippedEventArges);

            }
            else if (_flipDirection > 0)
            {
                onflippedEventArges.FlipType = FlipType.前进;
                this.OnFlipped?.Invoke(this, onflippedEventArges);

            }
            this._flipDirection = 0;

        }
        public static readonly BindableProperty RealTimeIndexProperty =
                   BindableProperty.Create<GeneralCardView, int>(
                       carousel => carousel.RealTimeIndex,
                       0,
                       BindingMode.TwoWay,
                       propertyChanged:RealTimeIndexPropertyChanged
                     
                   );

        private static void RealTimeIndexPropertyChanged(BindableObject bindable, int oldValue, int newValue)
        {
            if (((GeneralCardView)bindable).IsFlipEnable)
            {

                if (newValue > oldValue)
                {
                    ((GeneralCardView)bindable)._flipDirection++;
                }
                if (newValue < oldValue)
                {
                    ((GeneralCardView)bindable)._flipDirection--;
                }

            }
        }

        public int RealTimeIndex
        {
            get
            {
                return (int)GetValue(RealTimeIndexProperty);
            }
            set
            {
                SetValue(RealTimeIndexProperty, value);
            }
        }

        private int _flipDirection;

        public void Init()
        {
            this.IsFlipEnable = false;
            this.AnimateScroll = false;
            this.Position = new Point(375, 0);
            this.AnimateScroll = true;
            this.IsFlipEnable = true;
        }
    }


}
