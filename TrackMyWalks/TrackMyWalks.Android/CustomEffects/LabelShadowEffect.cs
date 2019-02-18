using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportEffect(typeof(TrackMyWalks.Droid.CustomEffects.LabelShadowEffect), "LabelShadowEffect")]
namespace TrackMyWalks.Droid.CustomEffects
{
	public class LabelShadowEffect : PlatformEffect
	{
		protected override void OnAttached()
		{
			try
			{
				var control = Control as Android.Widget.TextView;
				float radius = 5;
				float distanceX = 4;
				float distanceY = 4;

				Android.Graphics.Color color = Color.White.ToAndroid();

				control.SetShadowLayer(radius, distanceX, distanceY, color);
			}
			catch (Exception ex)
			{
				Console.WriteLine("Cannot set property on attached control. \n\nError: " + ex);
			}
		}

		protected override void OnDetached()
		{
			
		}
	}

	
}