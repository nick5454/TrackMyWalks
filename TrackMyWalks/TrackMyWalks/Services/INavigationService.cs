using System;
using System.Collections.Generic;
using System.Text;
using TrackMyWalks.ViewModels;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TrackMyWalks.Services
{
    public interface INavigationService
    {
		Task<Page> RemoveViewFromStack();

		Task BackToMainPage();

		Task NavigateTo<TVM>() where TVM : BaseViewModel;
    }
}
