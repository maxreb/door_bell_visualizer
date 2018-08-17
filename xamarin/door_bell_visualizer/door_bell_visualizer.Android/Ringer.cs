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
using door_bell_visualizer.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(Ringer))]

namespace door_bell_visualizer.Droid
{
    class Ringer : IRinger
    {

        static Ringtone ringtone = null;

        public Ringer()
        {

        }

        public Ringer(Context ApplicationContext)
        {
            if (ringtone == null)
                ringtone = RingtoneManager.GetRingtone(ApplicationContext, RingtoneManager.GetDefaultUri(RingtoneType.Ringtone));
        }

        public void Play()
        {
            ringtone.Play();
        }

        public void Stop()
        {
            ringtone.Stop();

        }

    }
}