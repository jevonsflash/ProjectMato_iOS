using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace ProjectMato.iOS.Controls
{
    public class CarouselView : ExtendedScrollView
    {

        readonly StackLayout _stack;

        int _selectedIndex;

        public CarouselView()
        {
            Orientation = ScrollOrientation.Horizontal;

            _stack = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Spacing = 0
            };

            Content = _stack;
        }

        public IList<View> Children
        {
            get
            {
                return _stack.Children;
            }
        }

        private bool _layingOutChildren;
        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            base.LayoutChildren(x, y, width, height);
            if (_layingOutChildren)
                return;
            _layingOutChildren = true;
            if (IsScreenWidth)
            {
                _layingOutChildren = true;
                foreach (var child in Children)
                {
                    child.WidthRequest = width;
                }
                _layingOutChildren = false;
            }

        }

        public static readonly BindableProperty SelectedIndexProperty =
            BindableProperty.Create(
                nameof(SelectedIndex),
                typeof(int),
                typeof(CarouselView),
                0,
                BindingMode.TwoWay,
                propertyChanged: async (bindable, oldValue, newValue) =>
                {
                    await ((CarouselView)bindable).UpdateSelectedItem();
                }
            );

        public int SelectedIndex
        {
            get
            {
                return (int)GetValue(SelectedIndexProperty);
            }
            set
            {
                SetValue(SelectedIndexProperty, value);
            }
        }


        async Task UpdateSelectedItem()
        {
            await Task.Delay(300);
            SelectedItem = SelectedIndex > -1 ? Children[SelectedIndex].BindingContext : null;
        }

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(
                nameof(ItemsSource),
                typeof(IList),
                typeof(CarouselView),
                null,
                propertyChanging: (bindableObject, oldValue, newValue) =>
                {
                    ((CarouselView)bindableObject).ItemsSourceChanging();
                },
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    ((CarouselView)bindableObject).ItemsSourceChanged();
                }
            );

        public IList ItemsSource
        {
            get
            {
                return (IList)GetValue(ItemsSourceProperty);
            }
            set
            {
                SetValue(ItemsSourceProperty, value);
            }
        }

        public void ItemsSourceChanging()
        {
            if (ItemsSource == null) return;
            _selectedIndex = ItemsSource.IndexOf(SelectedItem);
        }

        public void ItemsSourceChanged()
        {
            if (ItemsSource == null)
                return;

            _stack.Children.Clear();
            foreach (var item in ItemsSource)
            {
                var view = (View)ItemTemplate.CreateContent();
                var bindableObject = view as BindableObject;
                if (bindableObject != null)
                    bindableObject.BindingContext = item;
                var tapGesture = new TapGestureRecognizer();
                tapGesture.Tapped += TapGesture_Tapped;
                view.GestureRecognizers.Add(tapGesture);
                _stack.Children.Add(view);
            }

            if (_selectedIndex >= 0)
            {
                SelectedIndex = _selectedIndex;
            }
            //this.SelectedIndex = 1;
        }

        private void TapGesture_Tapped(object sender, EventArgs e)
        {
            var view = sender as View;
            if (view != null) SelectedItem = view.BindingContext;
        }

        public DataTemplate ItemTemplate
        {
            get;
            set;
        }
        public bool IsScreenWidth
        {
            get;
            set;
        }

        public static readonly BindableProperty SelectedItemProperty =
            BindableProperty.Create(
                nameof(SelectedItem),
                typeof(object),
                typeof(CarouselView),
                null,
                BindingMode.TwoWay,
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    ((CarouselView)bindable).UpdateSelectedIndex();
                }
            );

        public object SelectedItem
        {
            get
            {
                return GetValue(SelectedItemProperty);
            }
            set
            {
                SetValue(SelectedItemProperty, value);
            }
        }

        void UpdateSelectedIndex()
        {
            if (SelectedItem == BindingContext) return;

            SelectedIndex = Children
                .Select(c => c.BindingContext)
                .ToList()
                .IndexOf(SelectedItem);
        }
    }
}