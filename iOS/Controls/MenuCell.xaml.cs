using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMato.iOS.Enums;
using Xamarin.Forms;

namespace ProjectMato.iOS.Controls
{
    public partial class MenuCell
    {
        public MenuCell()
        {
            InitializeComponent();
        }

        public MenuPage Host { get; set; }

        public MenuCode Code { get; set; }

        public static readonly BindableProperty ContentTextProperty =
    BindableProperty.Create(
        nameof(ContentText),
        typeof(string),
        typeof(MenuCell),
        "",
        BindingMode.TwoWay,
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            ((MenuCell)bindable).ContentText = newValue as string;
            ((MenuCell)bindable).LabelContentText.Text = newValue as string;

        });

        public string ContentText { get; set; }

        public static readonly BindableProperty ContentImageProperty =
    BindableProperty.Create(
        nameof(ContentImage),
        typeof(ImageSource),
        typeof(MenuCell),
        null,
        BindingMode.TwoWay,
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            ((MenuCell)bindable).ContentImage = newValue as ImageSource;
            ((MenuCell)bindable).ImageContentImage.Source = newValue as ImageSource;

        });
        public ImageSource ContentImage { get; set; }

        protected override void OnTapped()
        {
            base.OnTapped();

            Host.Selected(this.Code);
        }
    }
}
