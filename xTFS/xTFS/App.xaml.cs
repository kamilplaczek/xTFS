using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using xTFS.Helpers;
using xTFS.Navigation;
using xTFS.Views;

namespace xTFS
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			var firstPage = new NavigationPage(new LoginPage());
			MainPage = firstPage;
			var navService = SimpleIoc.Default.GetInstance<IExtNavigationService>();
			navService.Initialize(firstPage);
			if (!String.IsNullOrEmpty(Settings.TfsAddress) && !String.IsNullOrEmpty(Settings.Password) && !String.IsNullOrEmpty(Settings.Username))
			{
				MessagingCenter.Send(this, Messages.SignInMessage);
			}
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
