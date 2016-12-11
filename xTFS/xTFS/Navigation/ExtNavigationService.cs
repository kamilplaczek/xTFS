using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace xTFS.Navigation
{
	public class ExtNavigationService : IExtNavigationService
	{
		private readonly Dictionary<string, Type> _pagesByKey = new Dictionary<string, Type>();
		private NavigationPage _navigation;

		public string CurrentPageKey
		{
			get
			{
				lock (_pagesByKey)
				{
					if (_navigation.CurrentPage == null)
					{
						return null;
					}

					var pageType = _navigation.CurrentPage.GetType();

					return _pagesByKey.ContainsValue(pageType)
						? _pagesByKey.First(p => p.Value == pageType).Key
						: null;
				}
			}
		}

		public void GoBack()
		{
			_navigation.PopAsync();
		}

		public void SetMainPage(string pageKey)
		{
			var page = GetPage(pageKey, null);
			App.Current.MainPage = page;
		}

		public async Task NavigateToModal(string pageKey, object parameter = null)
		{
			var page = GetPage(pageKey, parameter);
			await _navigation.PushAsync(page);
		}

		public void NavigateTo(string pageKey)
		{
			NavigateTo(pageKey, null);
		}

		public void NavigateTo(string pageKey, object parameter)
		{
			var page = GetPage(pageKey, parameter);
			_navigation.PushAsync(page);
		}

		public void Configure(string pageKey, Type pageType)
		{
			lock (_pagesByKey)
			{
				if (_pagesByKey.ContainsKey(pageKey))
				{
					_pagesByKey[pageKey] = pageType;
				}
				else
				{
					_pagesByKey.Add(pageKey, pageType);
				}
			}
		}

		public void Initialize(NavigationPage navigation)
		{
			_navigation = navigation;
		}

		private Page GetPage(string pageKey, object parameter)
		{
			lock (_pagesByKey)
			{
				if (_pagesByKey.ContainsKey(pageKey))
				{
					var type = _pagesByKey[pageKey];
					ConstructorInfo constructor;
					object[] parameters;

					if (parameter == null)
					{
						constructor = type.GetTypeInfo()
							.DeclaredConstructors
							.FirstOrDefault(c => !c.GetParameters().Any());

						parameters = new object[]
						{
						};
					}
					else
					{
						constructor = type.GetTypeInfo()
							.DeclaredConstructors
							.FirstOrDefault(
								c =>
								{
									var p = c.GetParameters();
									return p.Count() == 1
										   && p[0].ParameterType == parameter.GetType();
								});

						parameters = new[]
						{
						parameter
					};
					}

					if (constructor == null)
					{
						throw new InvalidOperationException(
							"No suitable constructor found for page " + pageKey);
					}

					return constructor.Invoke(parameters) as Page;
				}
				else
				{
					throw new ArgumentException(
						string.Format(
							"No such page: {0}. Did you forget to call NavigationService.Configure?",
							pageKey),
						"pageKey");
				}
			}
		}
	}
}
