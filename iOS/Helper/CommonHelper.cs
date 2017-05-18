using System;
using System.Linq;
using GalaSoft.MvvmLight.Messaging;
using ProjectMato.iOS.Common;
using ProjectMato.iOS.Model;
using ProjectMato.iOS.Server;
using ProjectMato.iOS.ViewModel;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace ProjectMato.iOS.Helper
{
    public class CommonHelper
    {
        public static void GoPage(string pageName, object[] args = null)
        {
            
            MenuPageViewModel.Current.CurrentMenuCellInfo =
                MenuPageViewModel.Current.MainMenuCellInfos.FirstOrDefault(c => c.Code == pageName);
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

        public static int[] GetRandomArry(int minval, int maxval)
        {

            int[] arr = new int[maxval - minval + 1];
            int i;
            //初始化数组
            for (i = 0; i <= maxval - minval; i++)
            {
                arr[i] = i + minval;
            }
            //随机数
            Random r = new Random();
            for (int j = maxval - minval; j >= 1; j--)
            {
                int address = r.Next(0, j);
                int tmp = arr[address];
                arr[address] = arr[j];
                arr[j] = tmp;
            }
            //输出
            foreach (int k in arr)
            {
                Console.Write(k + " ");
            }
            return arr;
        }

        public static void SetTheme(BackgroundTable res)
        {
            App.Current.Resources["PhoneForegroundBrush"] = Color.FromHex(res.ColorB);
            App.Current.Resources["PhoneContrastBackgroundBrush"] = Color.FromHex(res.ColorA);
            App.Current.Resources["PhoneWeakenBackgroundBrush"] = Color.FromHex(res.ColorC);
            App.Current.Resources["PhoneBackgroundImage"] = res.Img;
        }
    }
}