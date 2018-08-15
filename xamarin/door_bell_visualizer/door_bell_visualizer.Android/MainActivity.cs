using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Media;

namespace door_bell_visualizer.Droid
{
    [Activity(Label = "door_bell_visualizer", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);



            LoadApplication(new App());



        }

        protected override void OnStart()
        {
            using (Ringtone r = RingtoneManager.GetRingtone(ApplicationContext, RingtoneManager.GetDefaultUri(RingtoneType.Ringtone)))
            {
                r.Play();
            }
            base.OnStart();
        }
    }



}

