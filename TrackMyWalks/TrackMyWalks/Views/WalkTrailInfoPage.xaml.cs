using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackMyWalks.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TrackMyWalks.ViewModels;
using TrackMyWalks.Services;

namespace TrackMyWalks.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WalkTrailInfoPage : ContentPage
	{
		WalkTrailInfoPageViewModel _viewModel => BindingContext as WalkTrailInfoPageViewModel;

		public WalkTrailInfoPage ()
		{
			InitializeComponent ();

			this.Title = "Trail Walk Information";
			this.BindingContext = new WalkTrailInfoPageViewModel(DependencyService.Get<INavigationService>());
		}

		private async void BeginTrailWalk_Clicked(object sender, EventArgs e)
		{
			if (App.SelectedItem == null)
			{
				return;
			}

			await _viewModel.Navigation.NavigateTo<WalkDistancePageViewModel>();
		}
	}
}