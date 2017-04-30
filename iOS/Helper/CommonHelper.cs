using GalaSoft.MvvmLight.Messaging;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace ProjectMato.iOS.Helper
{
    public class CommonHelper
    {
        public static void GoPage(string pageName)
        {
            Messenger.Default.Send<string>(pageName, TokenHelper.WindowToken);

        }
    }
}