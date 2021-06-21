
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace Notee.Droid
{
    public static class OnRun 
    {
        public static bool isfirst = true;
    }
    [Activity(Label = "Notee", MainLauncher =true, NoHistory =true)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            if (AppInfo.RequestedTheme == AppTheme.Dark)
            {
                SetTheme(Resource.Style.DarkTheme);
            }
            else
            {
                SetTheme(Resource.Style.LightTheme);
            }


            base.OnCreate(savedInstanceState);
        }

        protected override async void OnResume()
        {
            base.OnResume();
            await SimulateStartUp();
        }

        async Task SimulateStartUp()
        {
            if (OnRun.isfirst == true)
            {
                OnRun.isfirst = false;
                await Task.Delay(TimeSpan.FromSeconds(2.5));
                StartActivity(new Intent(Android.App.Application.Context, typeof(MainActivity)));
            }
        }
    }
}