using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackMyWalks.Services;
using TrackMyWalks.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TrackMyWalks.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WalkEntryPage : ContentPage
	{
		WalkEntryPageViewModel _viewModel => BindingContext as WalkEntryPageViewModel;

		public WalkEntryPage ()
		{
			InitializeComponent ();

			Title = "New Walk Entry Page";
			BindingContext = new WalkEntryPageViewModel(DependencyService.Get<INavigationService>());
			SetBinding(TitleProperty, new Binding(BaseViewModel.PageTitlePropertyName));
		}

		private async void SaveWalkItem_Clicked(object sender, EventArgs e)
		{
			if(await DisplayAlert("Save Walk Entry Item", "Proceed and save changes?", "OK", "Cancel"))
			{
				if(!_viewModel.ValidateFormDetailsAndSave())
				{
					await DisplayAlert("Validation Error", "Title and description are required.", "Ok");
				} else
				{
					await _viewModel.Navigation.RemoveViewFromStack();
				}
			} else
			{
				await _viewModel.Navigation.RemoveViewFromStack();
			}
			
		}
	}
}