/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:xTFS"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using Xamarin.Forms;
using xTFS.Helpers;
using xTFS.Navigation;
using xTFS.Rest;
using xTFS.Services;
using xTFS.Views;

namespace xTFS.ViewModels
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
			ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

			SimpleIoc.Default.Register<LoginViewModel>();
			SimpleIoc.Default.Register<SettingsViewModel>();
			SimpleIoc.Default.Register<ProjectsListViewModel>();
			SimpleIoc.Default.Register<IterationsListViewModel>();
			SimpleIoc.Default.Register<WorkItemsListViewModel>();
			SimpleIoc.Default.Register<WorkItemDetailsViewModel>();

			var nav = new ExtNavigationService();
			nav.Configure(Locator.LoginPage, typeof(LoginPage));
			nav.Configure(Locator.SettingsPage, typeof(SettingsPage));
			nav.Configure(Locator.ProjectsListPage, typeof(ProjectsListPage));
			nav.Configure(Locator.ProjectDetailsPage, typeof(ProjectDetailsPage));
			nav.Configure(Locator.IterationsListPage, typeof(IterationsListPage));
			nav.Configure(Locator.WorkItemsListPage, typeof(WorkItemsListPage));
			nav.Configure(Locator.WorkItemDetailsPage, typeof(WorkItemDetailsPage));

			SimpleIoc.Default.Register<IExtNavigationService>(() => nav);
			SimpleIoc.Default.Register<IPopupService, PopupService>();
			SimpleIoc.Default.Register<ITfsService, TfsService>();
		}

		public LoginViewModel Login
		{
			get
			{
				return ServiceLocator.Current.GetInstance<LoginViewModel>();
			}
		}

		public SettingsViewModel Settings
		{
			get
			{
				return ServiceLocator.Current.GetInstance<SettingsViewModel>();
			}
		}

		public ProjectsListViewModel ProjectsList
		{
			get
			{
				return ServiceLocator.Current.GetInstance<ProjectsListViewModel>();
			}
		}

		public IterationsListViewModel IterationsList
		{
			get
			{
				return ServiceLocator.Current.GetInstance<IterationsListViewModel>();
			}
		}

		public WorkItemsListViewModel WorkItemsList
		{
			get
			{
				return ServiceLocator.Current.GetInstance<WorkItemsListViewModel>();
			}
		}

		public WorkItemDetailsViewModel WorkItemDetails
		{
			get
			{
				return ServiceLocator.Current.GetInstance<WorkItemDetailsViewModel>();
			}
		}

		public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}