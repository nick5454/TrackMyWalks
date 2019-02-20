using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TrackMyWalks.Models;
using TrackMyWalks.ViewModels;
using TrackMyWalks.Services;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TrackMyWalks.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WalksMainPage : ContentPage
	{
		WalksMainPageViewModel _viewModel => BindingContext as WalksMainPageViewModel;

		public WalksMainPage ()
		{
			InitializeComponent ();
			this.Title = "Track My Walks Listing";
			this.BindingContext = new WalksMainPageViewModel(DependencyService.Get<INavigationService>());
		}

		//private void InitializeWalks()
		//{
		//	var walksListModel = new ObservableCollection<WalkDataModel>()
		//	{
		//		new WalkDataModel
		//		{
		//			Id = 1,
		//			Title = "10 Mile Brook Trail, Margaret River",
		//			Description = "The 10 Mile Brook Trail starts in the Rotary Park near old Kate, a preserved steam engine at the northen edge of Margaret River.",
		//			Latitude = -33.9727604,
		//			Longitude = 115.0861599,
		//			Distance = 7.5,
		//			Difficulty = "Medium",
		//			ImageUrl = "http://trailswa.com.au/media/cache/media/images/trails/_mid/FullSizeRender1_600_480_c1.jpg"
		//		},
		//		new WalkDataModel
		//		{
		//			Id = 2,
		//			Title = "Ancient Empire Walk, Valley of the Ancients",
		//			Description = "The Ancient Empire is a 450 metre walk trail that takes you around and through some of the giant tingle trees including the most popular of the gnarled  veterans, known as Grandma Tingle",
		//			Latitude = -34.9749188,
		//			Longitude = 117.3560796,
		//			Distance = 450,
		//			Difficulty = "Hard",
		//			ImageUrl = "http://trailswa.com.au/media/cache/media/images/trails/_mid/Ancient_Empire_534480_c1.jpg"
		//		}
		//	};

		//	WalkEntriesListView.ItemsSource = walksListModel;

		//}

		private async void AddWalk_Clicked(object sender, EventArgs e)
		{
			App.SelectedItem = null;
			await _viewModel.Navigation.NavigateTo<WalkEntryPageViewModel>();
		}

		private async void WalkEntriesListView_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			App.SelectedItem = e.Item as WalkDataModel;
			await _viewModel.Navigation.NavigateTo<WalkTrailInfoPageViewModel>();
		}

		public async void OnEditItem(object sender, EventArgs e)
		{
			var selectedItem = (WalkDataModel)((MenuItem)sender).CommandParameter;
			App.SelectedItem = selectedItem;
			await _viewModel.Navigation.NavigateTo<WalkEntryPageViewModel>();
		}

		public async void OnDeleteItem(object sender, EventArgs e)
		{
			var selectedItem = (WalkDataModel)((MenuItem)sender).CommandParameter;

			if (await DisplayAlert("Delete Walk Entry Item", "Are you sure you want to delete this Walk Entry Item?", "OK", "Cancel"))
			{
				_viewModel.WalksListModel.Remove(selectedItem);

				await _viewModel.AzureDatabase.DeleteWalkEntry(selectedItem.Id.ToString());

				await DisplayAlert("Delete Walk Entry Item", selectedItem.Title + " has been deleted from the database", "Ok");
			}
			else
				return;
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();
			if(_viewModel != null)
			{
				await _viewModel.InitAsync();
			}

			WalkEntriesListView.Opacity = 0;
			await WalkEntriesListView.FadeTo(1, 4000);

			// crazy animation. annoying but is a good example
			//var parentAnimation = new Animation();

			//var ZoomInAnimation = new Animation(v => LoadingWalkInfo.Scale = v, 1, 2, Easing.BounceIn, null);

			//parentAnimation.Add(0, 0.5, ZoomInAnimation);

			//var ZoomOutAnimation = new Animation(v => LoadingWalkInfo.Scale = v, 2, 1, Easing.BounceOut, null);

			//parentAnimation.Insert(0.5, 1, ZoomOutAnimation);

			//parentAnimation.Commit(this, "CustomAnimation", 16, 5000, null, null);

			WalkEntriesListView.SetBinding(ItemsView<Cell>.ItemsSourceProperty, new Binding("."));
			WalkEntriesListView.BindingContext = _viewModel.WalksListModel;
		}
	}
}