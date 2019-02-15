using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrackMyWalks.Services;

namespace TrackMyWalks.ViewModels
{
    public class WalkDistancePageViewModel : BaseViewModel
    {
		LocationService location;
		public event EventHandler<PositionEventArgs> CoordsChanged;

		public WalkDistancePageViewModel(INavigationService navService) : base(navService) { }

		public string Title => App.SelectedItem.Title;
		public string Description => App.SelectedItem.Description;
		public double Latitude => App.SelectedItem.Latitude;
		public double Longitude => App.SelectedItem.Longitude;
		public double Distance => App.SelectedItem.Distance;
		public string Difficulty => App.SelectedItem.Difficulty;
		public string ImageUrl => App.SelectedItem.ImageUrl;

		public async Task<Position> GetCurrentLocation()
		{
			location = new LocationService();
			location.PositionChanged += (s, e) =>
			{
				CoordsChanged?.Invoke(s, e);
			};

			var position = await location.GetCurrentPosition();
			return position;
		}

		public async void OnStartUpdate()
		{
			await location.StartListening();
		}

		public void OnStopUpdate()
		{
			location.StopListening();
		}
		public override async Task InitAsync()
		{
			await Task.Factory.StartNew(() =>
			{

			});
		}
	}
}
