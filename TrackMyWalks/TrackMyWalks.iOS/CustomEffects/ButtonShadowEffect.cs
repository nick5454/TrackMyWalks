﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("nickturner")]
[assembly: ExportEffect(typeof(TrackMyWalks.iOS.CustomEffects.ButtonShadowEffect), "ButtonShadowEffect")]
namespace TrackMyWalks.iOS.CustomEffects
{
	public class ButtonShadowEffect : PlatformEffect
	{
		protected override void OnAttached()
		{
			try
			{
				Container.Layer.ShadowOpacity = 0.5f;
				Container.Layer.ShadowColor = UIColor.Black.CGColor;
				Container.Layer.ShadowRadius = 2;
			} catch ( Exception ex )
			{
				Console.WriteLine("Cannot set property on attached control. \n\nException: " + ex);
			}
		}

		protected override void OnDetached()
		{
			Container.Layer.ShadowOpacity = 0;
		}
	}
}