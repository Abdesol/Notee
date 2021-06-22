using Android.App;
using Android.Content;
using Android.OS;
using System;
using System.Threading.Tasks;
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
            base.OnCreate(savedInstanceState);
            
            if (AppInfo.RequestedTheme == AppTheme.Dark)
            {
                SetTheme(Resource.Style.DarkTheme);
            }
            else
            {
                SetTheme(Resource.Style.LightTheme);
            }

        }

        protected override async void OnResume()
        {
            base.OnResume();
            await SimulateStartUp();
        }

        async Task SimulateStartUp()
        {
            await Task.Delay(TimeSpan.FromSeconds(0.1));
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
            Finish();
        }
    }
}