using System;
using TrackMyWalks.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TrackMyWalks.Views;
using TrackMyWalks.Services;
using TrackMyWalks.ViewModels;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TrackMyWalks
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			NavService = DependencyService.Get<INavigationService>() as NavigationService;
		}

		protected override void OnStart()
		{
			// Handle when your app starts
			if (Device.RuntimePlatform.Equals(Device.Android))
			{
				MainPage = new NavigationPage(new SplashPage());
			}
			else { 
				MainPage = new NavigationPage(new WalksMainPage())
				{
					BarBackgroundColor = Color.IndianRed,
					BarTextColor = Color.White
				};
			}

			NavService.XFNavigation = MainPage.Navigation;
			NavService.RegisterViewMapping(typeof(WalksMainPageViewModel), typeof(WalksMainPage));
			NavService.RegisterViewMapping(typeof(WalkEntryPageViewModel), typeof(WalkEntryPage));
			NavService.RegisterViewMapping(typeof(WalkTrailInfoPageViewModel), typeof(WalkTrailInfoPage));
			NavService.RegisterViewMapping(typeof(WalkDistancePageViewModel), typeof(WalkDistancePage));
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}

		public static WalkDataModel SelectedItem { get; set; }

		public static NavigationService NavService { get; set; }
	}
}
