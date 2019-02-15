using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using CoreLocation;
using MapKit;
using ObjCRuntime;
using TrackMyWalks.iOS;
using TrackMyWalks.Views.MapOverlay;

using Xamarin.Forms;
using Xamarin.Forms.Maps.iOS;
using Xamarin.Forms.Platform.iOS;
using TrackMyWalks.iOS.CustomRenderers;

[assembly: ExportRenderer(typeof(CustomMapOverlay), typeof(CustomMapRenderer))]
namespace TrackMyWalks.iOS.CustomRenderers
{
	public class CustomMapRenderer : MapRenderer
	{
		MKPolylineRenderer polyLineRenderer;
		protected override void OnElementChanged(ElementChangedEventArgs<View> e)
		{
			base.OnElementChanged(e);

			if(e.OldElement == null)
			{
				var formsMap = (CustomMapOverlay)e.NewElement;
				var nativeMap = Control as MKMapView;

				nativeMap.OverlayRenderer = GetOverlayRenderer;
				CLLocationCoordinate2D[] coords = new CLLocationCoordinate2D[formsMap.RouteCoordinates.Count];

				int index = 0;
				foreach(var position in formsMap.RouteCoordinates)
				{
					coords[index] = new CLLocationCoordinate2D(position.Latitude, position.Longitude);
					index++;
				}

				var routeOverlay = MKPolyline.FromCoordinates(coords);
				nativeMap.AddOverlay(routeOverlay);
			}
		}

		MKOverlayRenderer GetOverlayRenderer(MKMapView mapView, IMKOverlay overlayWrapper)
		{
			if(polyLineRenderer == null && !Equals(overlayWrapper, null))
			{
				var overlay = Runtime.GetNSObject(overlayWrapper.Handle) as IMKOverlay;
				polyLineRenderer = new MKPolylineRenderer(overlay as MKPolyline)
				{
					FillColor = UIColor.Red,
					StrokeColor = UIColor.Red,
					LineWidth = 3,
					Alpha = 0.4f
				};
			}

			return polyLineRenderer;
		}
	}
}