using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Notee.Helpers
{
    public static class TheTheme
    {
        public static void SetTheme()
        {
            switch (Settings.Theme)
            {
                //default
                case 0:
                    App.Current.UserAppTheme = OSAppTheme.Unspecified;
                    break;
                //light
                case 1:
                    App.Current.UserAppTheme = OSAppTheme.Light;
                    break;
                //dark
                case 2:
                    App.Current.UserAppTheme = OSAppTheme.Dark;
                    break;
            }

            var nav = App.Current.MainPage as Xamarin.Forms.NavigationPage;

            var e = DependencyService.Get<IEnvironment>();
            if (App.Current.RequestedTheme == OSAppTheme.Dark)
            {
                e?.SetStatusBarColor(Color.FromHex("080808"), false);
                if (nav != null)
                {
                    nav.BarBackgroundColor = Color.FromHex("080808");
                    nav.BarTextColor = Color.White;
                }
            }
            else
            {
                e?.SetStatusBarColor(Color.FromHex("F2F2F2"), true);
                if (nav != null)
                {
                    nav.BarBackgroundColor = Color.FromHex("F2F2F2");
                    nav.BarTextColor = Color.Black;
                }
            }
        }
    }
}
