﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrackMyWalks.Services;

namespace TrackMyWalks.ViewModels
{
    public class WalkTrailInfoPageViewModel : BaseViewModel
    {
		public WalkTrailInfoPageViewModel(INavigationService navService) : base(navService) { }

		public string Title => App.SelectedItem.Title;
		public string Description => App.SelectedItem.Description;
		public double Distance => App.SelectedItem.Distance;
		public string Difficulty => App.SelectedItem.Difficulty;
		public string ImageUrl => App.SelectedItem.ImageUrl;

		public override async Task InitAsync()
		{
			await Task.Factory.StartNew(() =>
			{

			});
		}
	}
}
