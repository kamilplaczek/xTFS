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
	}
}
