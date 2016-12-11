using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace xTFS.Navigation
{
	public interface IExtNavigationService : INavigationService
	{
		void SetMainPage(string pageKey);
		Task NavigateToModal(string pageKey, object parameter = null);
		void Initialize(NavigationPage navigation);
	}
}
