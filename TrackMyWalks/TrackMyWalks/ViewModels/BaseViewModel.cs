using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using TrackMyWalks.Services;

namespace TrackMyWalks.ViewModels
{
	public abstract class BaseViewModel : INotifyPropertyChanged
	{
		public INavigationService Navigation { get; set; }
		public const string PageTitlePropertyName = "PageTitle";

		protected BaseViewModel(INavigationService navService) {
			Navigation = navService;
		}

		string pageTitle;
		public string PageTitle
		{
			get => pageTitle;
			set { pageTitle = value; OnPropertyChanged(); }
		}

		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged([CallerMemberName] string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

		public abstract Task InitAsync();

		bool isProcessBusy;
		public bool IsProcessBusy
		{
			get => isProcessBusy;
			set { isProcessBusy = value; OnPropertyChanged(); }
		}
	}

	public abstract class BaseViewModel<TParam> : BaseViewModel
	{
		protected BaseViewModel(INavigationService navService) : base(navService) { }
	}
}
