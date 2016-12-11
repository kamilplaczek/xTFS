using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xTFS.Resources;

namespace xTFS.Services
{
	public class PopupService : IPopupService
	{
		public async Task DisplayAlert(string title, string msg)
		{
			await App.Current.MainPage.DisplayAlert(title, msg, AppResources.OkButton);
		}

		public async Task<string> DisplayOptions(string title, params string[] options)
		{
			var result = await App.Current.MainPage.DisplayActionSheet(title, AppResources.ActionSheetCancelAction, null, options);
			return result != AppResources.ActionSheetCancelAction ? result : null;
		}
	}
}
