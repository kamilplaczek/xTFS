using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using xTFS.Helpers;
using xTFS.Navigation;
using xTFS.Rest.Exceptions;
using xTFS.Services;

namespace xTFS.ViewModels
{
	public class BaseViewModel : ViewModelBase
	{
		protected readonly IExtNavigationService _navService;
		protected readonly IPopupService _popupService;

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

		public BaseViewModel(IExtNavigationService navService, IPopupService popupService)
		{
			_navService = navService;
			_popupService = popupService;
		}

		protected IEnumerable<string> GetEnumMemberValues(Type t)
		{
			var typeInfo = t.GetTypeInfo();
			if (typeInfo.IsEnum)
			{
				var values = typeInfo.DeclaredMembers.Select(x => t.GetAttribute<EnumMemberAttribute, string>(x.Name, e => e.Value));
				return values;
			}
			return null;
		}

		protected void HandleServiceException(ServiceException e)
		{
			_popupService.DisplayAlert("Error", e.Message);
		}
	}
}
