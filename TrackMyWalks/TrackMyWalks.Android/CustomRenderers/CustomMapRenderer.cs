using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using TrackMyWalks.Droid.CustomRenderers;
using TrackMyWalks.Views.MapOverlay;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomMapOverlay), typeof(CustomMapRenderer))]
namespace TrackMyWalks.Droid.CustomRenderers
{
	public class CustomMapRenderer : MapRenderer
	{
		CustomMapOverlay formsMap;

		public CustomMapRenderer(Context context) : base(context)
		{
			
		}

		protected override void OnElementChanged(ElementChangedEventArgs<Map> e)
		{
			base.OnElementChanged(e);

			if(e.OldElement == null)
			{
				formsMap = (CustomMapOverlay)e.NewElement;
				Control.GetMapAsync(this);
			}
		}

		protected override void OnMapReady(GoogleMap map)
		{
			base.OnMapReady(map);

			var polylineOptions = new PolylineOptions();
			polylineOptions.InvokeColor(0x66FF0000);

			foreach(var position in formsMap.RouteCoordinates)
			{
				polylineOptions.Add(new LatLng(position.Latitude, position.Longitude));
			}

			NativeMap.AddPolyline(polylineOptions);
		}
	}
}