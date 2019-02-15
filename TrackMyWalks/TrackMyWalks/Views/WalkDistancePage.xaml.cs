using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackMyWalks.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;
using TrackMyWalks.Services;
using Plugin.Geolocator.Abstractions;
using TrackMyWalks.Views.MapOverlay;

namespace TrackMyWalks.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WalkDistancePage : ContentPage
	{
		WalkDistancePageViewModel _viewModel => BindingContext as WalkDistancePageViewModel;
		Task<Plugin.Geolocator.Abstractions.Position> origPosition;

		public WalkDistancePage ()
		{
			InitializeComponent ();

			this.Title = "Distance Travelled Information";
			this.BindingContext = new WalkDistancePageViewModel(DependencyService.Get<INavigationService>());

			origPosition = _viewModel.GetCurrentLocation();
			_viewModel.CoordsChanged += Location_CoordsChanged;
			_viewModel.OnStartUpdate();

			customMap = new CustomMapOverlay
			{
				MapType = MapType.Street
			};

			customMap.Pins.Clear();

			CreatePinPlaceholder(PinType.Place,
				origPosition.Result.Latitude,
				origPosition.Result.Longitude,
				"",
				"My Location", 1);

			CreatePinPlaceholder(PinType.Place,
				_viewModel.Latitude,
				_viewModel.Longitude,
				_viewModel.Title,
				"Difficulty: " + _viewModel.Difficulty + "Total Distance: " + _viewModel.Distance, 2);

			customMap.RouteCoordinates.Add(new Xamarin.Forms.Maps.Position(
				origPosition.Result.Latitude,
				origPosition.Result.Longitude));

			customMap.RouteCoordinates.Add(new Xamarin.Forms.Maps.Position(
				_viewModel.Latitude,
				_viewModel.Longitude));

			customMap.MoveToRegion(MapSpan.FromCenterAndRadius(
				new Xamarin.Forms.Maps.Position(
					origPosition.Result.Latitude,
					origPosition.Result.Longitude),
					Distance.FromKilometers(1)));

			Content = customMap;

			// create a region around of 1 kilometer
			customMap.MoveToRegion(
				MapSpan.FromCenterAndRadius(new Xamarin.Forms.Maps.Position(_viewModel.Latitude, _viewModel.Longitude), Distance.FromKilometers(1.0))
				);
		}

		public void CreatePinPlaceholder(PinType pinType,
			double latitude,
			double longitude,
			string label,
			string address,
			int id)
		{
			customMap.Pins.Add(new Pin
			{
				Type = pinType,
				Position = new Xamarin.Forms.Maps.Position(latitude, longitude),
				Label = label,
				Address = address,
				Id = id
			});

			customMap.IsShowingUser = true;
		}

		void Location_CoordsChanged(object sender, PositionEventArgs e)
		{
			Device.BeginInvokeOnMainThread(() =>
			{
				var distancetraveled = origPosition.Result.CalculateDistance(e.Position, GeolocatorUtils.DistanceUnits.Kilometers);

				CreatePinPlaceholder(PinType.SavedPin,
					e.Position.Latitude,
					e.Position.Longitude,
					String.Format("traveled: {0:0.00} KM", distancetraveled), 
					"", 3);
			});
		}
		private async void EndThisTrail_Clicked(object sender, EventArgs e)
		{
			App.SelectedItem = null;
			_viewModel.OnStopUpdate();
			await _viewModel.Navigation.BackToMainPage();
		}
	}
}