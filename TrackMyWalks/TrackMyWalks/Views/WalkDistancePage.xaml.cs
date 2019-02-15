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

namespace TrackMyWalks.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WalkDistancePage : ContentPage
	{
		WalkDistancePageViewModel _viewModel => BindingContext as WalkDistancePageViewModel;

		public WalkDistancePage ()
		{
			InitializeComponent ();

			this.Title = "Distance Travelled Information";
			this.BindingContext = new WalkDistancePageViewModel(DependencyService.Get<INavigationService>());

			customMap.Pins.Add(new Pin
			{
				Type = PinType.Place,
				Position = new Position(
					_viewModel.Latitude,
					_viewModel.Longitude),
					Label = _viewModel.Title,
					Address = "Difficulty: " + _viewModel.Difficulty + " Total Distance: " + _viewModel.Distance,
					Id = _viewModel.Title
			});

			// create a region around of 1 kilometer
			customMap.MoveToRegion(
				MapSpan.FromCenterAndRadius(new Position(_viewModel.Latitude, _viewModel.Longitude), Distance.FromKilometers(1.0))
				);
		}

		private async void EndThisTrail_Clicked(object sender, EventArgs e)
		{
			App.SelectedItem = null;
			await _viewModel.Navigation.BackToMainPage();
		}
	}
}