using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using xTFS.Helpers;
using xTFS.Navigation;
using xTFS.Rest;
using xTFS.Rest.Exceptions;
using xTFS.Services;

namespace xTFS.ViewModels
{
	public class LoginViewModel : BaseViewModel
	{
		private readonly IPopupService _popupService;
		private readonly ITfsService _tfsService;

		private string _login;
		private string _password;

		public string Login
		{
			get
			{
				return _login;
			}
			set
			{
				Set(ref _login, value);
			}
		}

		public string Password
		{
			get
			{
				return _password;
			}
			set
			{
				Set(ref _password, value);
			}
		}

		public ICommand LoginCommand
		{
			get
			{
				return new RelayCommand(async () =>
				{
					if (String.IsNullOrEmpty(Settings.TfsAddress))
					{
						await _popupService.DisplayAlert("Error", "TFS address is required.");
						await _navService.NavigateToModal(Locator.SettingsPage);
					}
					else if (String.IsNullOrEmpty(_password) || String.IsNullOrEmpty(_login))
					{
						await _popupService.DisplayAlert("Error", "Credentials are required.");
					}
					else
					{
						await SignIn(_login, _password);
					}
				});
			}
		}

		public ICommand SettingsCommand
		{
			get
			{
				return new RelayCommand(async () =>
				{
					await _navService.NavigateToModal(Locator.SettingsPage);
				});
			}
		}

		public LoginViewModel(IExtNavigationService navService, IPopupService popupService, ITfsService tfsService) : base(navService, popupService)
		{
			_tfsService = tfsService;
			MessagingCenter.Subscribe<App>(this, Messages.SignInMessage, async (sender) =>
			{
				if (!String.IsNullOrEmpty(Settings.Password) && !String.IsNullOrEmpty(Settings.Username))
				{
					await SignIn(Settings.Username, Settings.Password);
				}
			});
		}

		private async Task SignIn(string username, string password)
		{
			_tfsService.Init(username, password, Settings.TfsAddress);
			// retrieve projects list and check if credentials are valid
			try
			{
				IsBusy = true;
				var projects = await _tfsService.GetProjects();
				// login successful - store username and password
				Settings.Username = username;
				Settings.Password = password;
				_navService.SetMainPage(Locator.ProjectsListPage);
				MessagingCenter.Send(this, Messages.SetProjectsListMessage, projects);
			}
			catch (ServiceException e)
			{
				HandleServiceException(e);
			}
			finally
			{
				IsBusy = false;
			}
		}
	}
}
