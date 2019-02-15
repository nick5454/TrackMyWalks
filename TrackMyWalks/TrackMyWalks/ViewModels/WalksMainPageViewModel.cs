using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using TrackMyWalks.Models;
using TrackMyWalks.Services;

namespace TrackMyWalks.ViewModels
{
    public class WalksMainPageViewModel : BaseViewModel
    {
		public ObservableCollection<WalkDataModel> WalksListModel;

		public WalksMainPageViewModel(INavigationService navService) : base(navService) { }

		public void GetWalkTrailItems()
		{
			WalksListModel = new ObservableCollection<WalkDataModel>
			{
				new WalkDataModel
				{
					Id = 1,
					Title = "10 Mile Brook Trail, Margaret River",
					Description = "The 10 Mile Brook Trail starts in the Rotary Park near old Kate, a preserved steam engine at the northen edge of Margaret River.",
					Latitude = -33.9727604,
					Longitude = 115.0861599,
					Distance = 7.5,
					Difficulty = "Medium",
					ImageUrl = "http://trailswa.com.au/media/cache/media/images/trails/_mid/FullSizeRender1_600_480_c1.jpg"
				},
				new WalkDataModel
				{
					Id = 2,
					Title = "Ancient Empire Walk, Valley of the Ancients",
					Description = "The Ancient Empire is a 450 metre walk trail that takes you around and through some of the giant tingle trees including the most popular of the gnarled  veterans, known as Grandma Tingle",
					Latitude = -34.9749188,
					Longitude = 117.3560796,
					Distance = 450,
					Difficulty = "Hard",
					ImageUrl = "http://trailswa.com.au/media/cache/media/images/trails/_mid/Ancient_Empire_534480_c1.jpg"
				}
			};

		}

		public override async Task InitAsync()
		{
			await Task.Factory.StartNew(() =>
			{
				GetWalkTrailItems();
			});
		}
	}
}
