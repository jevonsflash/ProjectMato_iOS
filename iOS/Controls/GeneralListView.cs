﻿using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ProjectMato.iOS.Controls
{
    public class GeneralListView : ListView
    {
        public GeneralListView()
        {
            this.BackgroundColor = Color.Transparent;
            this.SeparatorVisibility = SeparatorVisibility.None;
        }
    }
}
