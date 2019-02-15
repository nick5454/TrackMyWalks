using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TrackMyWalks.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SplashPage : ContentPage
	{
		public SplashPage ()
		{
			InitializeComponent ();
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();

			await Task.Delay(3000);

			Application.Current.MainPage = new NavigationPage(new WalksMainPage())
			{
				BarBackgroundColor = Color.CadetBlue,
				BarTextColor = Color.White
			};

			App.NavService.XFNavigation = Application.Current.MainPage.Navigation;
		}
	}
}