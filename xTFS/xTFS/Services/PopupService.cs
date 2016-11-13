using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xTFS.Services
{
	public class PopupService : IPopupService
	{
		public async Task DisplayAlert(string title, string msg)
		{
			await App.Current.MainPage.DisplayAlert(title, msg, "OK");
		}
	}
}
