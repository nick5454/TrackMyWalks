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

			await BeginTrailWalk.RotateTo(360, 1000);
			BeginTrailWalk.Rotation = 0;

			await BeginTrailWalk.RotateTo(15, 10000, new Easing(t =>
				Math.Sin(Math.PI * t) * Math.Sin(Math.PI * 20 * t)));

			await _viewModel.Navigation.NavigateTo<WalkDistancePageViewModel>();
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();

			double offset = 1000;

			// more crazy animation
			//foreach(View view in TrailInfoScollView.Children)
			//{
			//	view.TranslationX = offset;
			//	offset *= 1;
			//	await Task.WhenAny(view.TranslateTo(0, 0, 1000, Easing.SpringOut), Task.Delay(100));
			//}
			//var animation = new Animation(v =>
			//	BeginTrailWalk.BackgroundColor = Color.FromHsla(v, 1, 0.5), start: 0, end: 1);

			//animation.Commit(this, "BeginWalkCustomAnimation",
			//	16,
			//	5000,
			//	Easing.Linear, (v, c) =>
			//		BackgroundColor = Color.Default, () => true);
		}
	}
}