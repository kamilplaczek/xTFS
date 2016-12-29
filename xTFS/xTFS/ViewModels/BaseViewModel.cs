using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xTFS.Navigation;

namespace xTFS.ViewModels
{
	public class BaseViewModel : ViewModelBase
	{
		protected readonly IExtNavigationService _navService;

		private bool _isBusy;

		public bool IsBusy
		{
			get
			{
				return _isBusy;
			}
			set
			{
				Set(ref _isBusy, value);
			}
		}

		public BaseViewModel(IExtNavigationService navService)
		{
			_navService = navService;
		}
	}
}
