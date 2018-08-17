using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Plugin.FirebasePushNotification;

namespace door_bell_visualizer.Droid
{
    [Application]
    public class MainApplication : Application
    {
        private readonly Ringer ringer;

        public MainApplication(IntPtr handle, JniHandleOwnership transer) : base(handle, transer)
        {
            ringer = new Ringer(ApplicationContext);
        }



        public override void OnCreate()
        {
            base.OnCreate();



            //Set the default notification channel for your app when running Android Oreo
            if ((int)Build.VERSION.SdkInt >= 26)
            {
                //Change for your default notification channel id here
                FirebasePushNotificationManager.DefaultNotificationChannelId = "FirebasePushNotificationChannel";

                //Change for your default notification channel name here
                FirebasePushNotificationManager.DefaultNotificationChannelName = "General";
            }


            //If debug you should reset the token each time.
#if DEBUG
            FirebasePushNotificationManager.Initialize(this, true);
#else
              FirebasePushNotificationManager.Initialize(this,false);
#endif

            //Handle notification when app is closed here
            CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            {
                if (p.Data.TryGetValue("body", out object val))
                {
                    switch ((string)val)
                    {
                        case "ring":
                            ringer.Play();
                            break;
                        case "stop":
                            ringer.Stop();
                            break;
                    }

                }

            };


        }

    }


}