using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using xTFS.Helpers;
using xTFS.Navigation;

namespace xTFS.ViewModels
{
	public class SettingsViewModel : BaseViewModel
	{
		private readonly IExtNavigationService _navService;

		private string _address;

		public string Address
		{
			get
			{
				return _address;
			}
			set
			{
				Set(ref _address, value);
			}
		}

		public ICommand CancelCommand
		{
			get
			{
				return new RelayCommand(() =>
				{
					_navService.GoBack();
				});
			}
		}

		public ICommand SaveCommand
		{
			get
			{
				return new RelayCommand(() =>
				{
					Settings.TfsAddress = _address;
					_navService.GoBack();
				});
			}
		}

		public SettingsViewModel(IExtNavigationService navService)
		{
			_navService = navService;
			Address = Settings.TfsAddress;
		}
	}
}
