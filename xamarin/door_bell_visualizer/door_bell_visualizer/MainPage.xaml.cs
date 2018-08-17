using Plugin.FirebasePushNotification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace door_bell_visualizer
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
			CrossFirebasePushNotification.Current.OnTokenRefresh += (s,p) => System.Diagnostics.Debug.WriteLine($"TOKEN : {p.Token}");
			CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
			{

				System.Diagnostics.Debug.WriteLine($"Received:");
				foreach (var x in p.Data)
				{
					System.Diagnostics.Debug.WriteLine($"{x.Key}: {x.Value}");
				}

			};

		}
	}
}
