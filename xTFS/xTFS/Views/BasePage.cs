using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using xTFS.ViewModels;

namespace xTFS.Views
{
	public class BasePage : ContentPage
	{
		protected override void OnAppearing()
		{
			base.OnAppearing();
			var vm = BindingContext as BaseViewModel;
			if (vm != null)
			{
				// add acitivity indicator to curent page content
				var indicator = new ActivityIndicator();
				indicator.SetBinding(ActivityIndicator.IsRunningProperty, nameof(vm.IsBusy));
				indicator.SetBinding(ActivityIndicator.IsVisibleProperty, nameof(vm.IsBusy));
				var content = Content;
				var rootGrid = new Grid();
				rootGrid.Children.Add(content);
				rootGrid.Children.Add(indicator);
				Content = rootGrid;
			}
		}
	}
}
