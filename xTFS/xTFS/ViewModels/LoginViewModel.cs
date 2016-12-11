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
using xTFS.Services;

namespace xTFS.ViewModels
{
	public class LoginViewModel : ViewModelBase
	{
		private readonly IExtNavigationService _navService;
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
						_tfsService.Init(_login, _password, Settings.TfsAddress);
						// retrieve projects list and check if credentials are valid
						var projects = await _tfsService.GetProjects();
						_navService.NavigateTo(Locator.ProjectsListPage);
						MessagingCenter.Send(this, Messages.SetProjectsListMessage, projects);
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

		public LoginViewModel(IExtNavigationService navService, IPopupService popupService, ITfsService tfsService)
		{
			_navService = navService;
			_popupService = popupService;
			_tfsService = tfsService;
		}
	}
}
