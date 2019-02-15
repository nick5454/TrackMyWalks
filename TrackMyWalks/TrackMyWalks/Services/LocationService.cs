using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using TrackMyWalks.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(LocationService))]
namespace TrackMyWalks.Services
{
	public class LocationService : ILocationService
	{
		public event EventHandler<PositionEventArgs> PositionChanged;

		public async Task<Position> GetCurrentPosition()
		{
			Position position = null;
			try
			{
				var locator = CrossGeolocator.Current;
				locator.DesiredAccuracy = 200;

				position = await locator.GetLastKnownLocationAsync();
				if (position != null) return position;

				if (!locator.IsGeolocationAvailable || !locator.IsGeolocationEnabled)
				{
					return null;
				}

				position = await locator.GetPositionAsync(TimeSpan.FromSeconds(1), null, true);
			} catch ( Exception ex)
			{
				Debug.WriteLine("There was a problem getting the location: " + ex);
			}

			return position;
		}

		public async Task StartListening()
		{
			if (CrossGeolocator.Current.IsListening)
				return;

			if(Device.RuntimePlatform.Equals(Device.Android))
			{
				await CrossGeolocator.Current.StartListeningAsync(TimeSpan.FromSeconds(5), 10, true);
			} else
			{
				await CrossGeolocator.Current.StartListeningAsync(TimeSpan.FromSeconds(1), 100, true, new ListenerSettings
				{
					ActivityType = ActivityType.AutomotiveNavigation,
					AllowBackgroundUpdates = true,
					DeferLocationUpdates = true,
					DeferralTime = TimeSpan.FromSeconds(1),
					ListenForSignificantChanges = false,
					PauseLocationUpdatesAutomatically = false
				});
			}

			CrossGeolocator.Current.PositionChanged += (s, e) =>
			{
				PositionChanged?.Invoke(s, e);
			};
		}

		public async void StopListening()
		{
			if (!CrossGeolocator.Current.IsListening) return;

			await CrossGeolocator.Current.StopListeningAsync();
			CrossGeolocator.Current.PositionChanged -= PositionChanged;
		}
	}
}
