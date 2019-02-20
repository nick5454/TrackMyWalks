using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrackMyWalks.Models;
using TrackMyWalks.Services;

namespace TrackMyWalks.ViewModels
{
	public class WalkEntryPageViewModel : BaseViewModel
	{
		bool isAdding;
		public WalkEntryPageViewModel(INavigationService navService) : base(navService)
		{
			if(App.SelectedItem == null )
			{
				PageTitle = "Adding Trail Details";
				App.SelectedItem = new WalkDataModel();

				isAdding = true;
				Title = "New Trail Entry";
				Difficulty = "Easy";
				Distance = 1.0;
			} else
			{
				PageTitle = "Editing Trail Details";
				isAdding = false;
			}
		}

		public async Task<bool> ValidateFormDetailsAndSave()
		{
			if (App.SelectedItem != null && 
				!string.IsNullOrWhiteSpace(App.SelectedItem.Title) && 
				!string.IsNullOrWhiteSpace(App.SelectedItem.Description) &&
				!string.IsNullOrWhiteSpace(App.SelectedItem.ImageUrl))
			{
				// save to database or model
				if (IsProcessBusy) return false;

				IsProcessBusy = true;

				await AzureDatabase.SaveWalkEntry(App.SelectedItem, isAdding);

				IsProcessBusy = false;
			} else
			{
				IsProcessBusy = false;
				return false;
			}

			IsProcessBusy = false;
			return true;
		}

		public string Title
		{
			get => App.SelectedItem.Title;
			set { App.SelectedItem.Title = value; OnPropertyChanged(); }
		}

		public string Description
		{
			get => App.SelectedItem.Description;
			set { App.SelectedItem.Description = value; OnPropertyChanged(); }
		}

		public double Latitude
		{
			get => App.SelectedItem.Latitude;
			set { App.SelectedItem.Latitude = value; OnPropertyChanged(); }
		}

		public double Longitude
		{
			get => App.SelectedItem.Longitude;
			set { App.SelectedItem.Longitude = value; OnPropertyChanged(); }
		}

		public double Distance
		{
			get => App.SelectedItem.Distance;
			set { App.SelectedItem.Distance = value; OnPropertyChanged(); }
		}

		public string Difficulty
		{
			get => App.SelectedItem.Difficulty;
			set { App.SelectedItem.Difficulty = value; OnPropertyChanged(); }
		}
		public string ImageUrl
		{
			get => App.SelectedItem.ImageUrl;
			set { App.SelectedItem.ImageUrl = value; OnPropertyChanged(); }
		}

		public async Task GetMyLocation()
		{
			var position = await new LocationService().GetCurrentPosition();

			if (position == null) return;

			if(App.SelectedItem.Latitude.Equals(0) && App.SelectedItem.Longitude.Equals(0))
			{
				Latitude = position.Latitude;
				Longitude = position.Longitude;
			}
		}

		public override async Task InitAsync()
		{
			await Task.Factory.StartNew(async () =>
		   {
			   IsProcessBusy = false;
			   await GetMyLocation();
		   });
		}
	}
}
