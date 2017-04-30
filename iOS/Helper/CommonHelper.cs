using System;
using GalaSoft.MvvmLight.Messaging;
using ProjectMato.iOS.Common;
using ProjectMato.iOS.Server;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace ProjectMato.iOS.Helper
{
    public class CommonHelper
    {
        public static void GoPage(string pageName, object[] args = null)
        {
            Messenger.Default.Send<WindowArg>(new WindowArg(pageName, args, false), TokenHelper.WindowToken);

        }

        public static void GoNavigate(string pageName, object[] args = null)
        {

            Messenger.Default.Send<WindowArg>(new WindowArg(pageName, args, true), TokenHelper.WindowToken);

        }

        public static int GetRamdonNum()
        {
            var r = new Random();
            return r.Next(100000, 999999);
        }
    }
}