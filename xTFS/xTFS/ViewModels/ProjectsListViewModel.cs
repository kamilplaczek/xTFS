using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using xTFS.Helpers;
using xTFS.Navigation;
using xTFS.Resources;
using xTFS.Rest;
using xTFS.Rest.Exceptions;
using xTFS.Rest.Models;
using xTFS.Services;

namespace xTFS.ViewModels
{
	public class ProjectsListViewModel : BaseViewModel
	{
		private readonly ITfsService _tfsService;

		private ObservableCollection<Project> _projects;

		public ObservableCollection<Project> Projects
		{
			get
			{
				return _projects;
			}
			set
			{
				Set(ref _projects, value);
			}
		}

		public ICommand RefreshProjectsCommand
		{
			get
			{
				return new RelayCommand(async () =>
				{
					await GetProjects();
				});
			}
		}
		public ICommand SelectProjectCommand
		{
			get
			{
				return new RelayCommand<Project>((project) =>
				{
					//var teams = await _tfsService.GetTeams(project.Id);
					//if (teams.Count > 1)
					//{
					//	var team = await _popupService.DisplayOptions(AppResources.SelectTeamActionSheetTitle, teams.Value.Select(t => t.Name).ToArray());
					//	if (!String.IsNullOrEmpty(team))
					//	{
					//		await SelectProject(project.Id, team);
					//	}
					//}
					//else
					//{
					//	var team = teams.Value.SingleOrDefault();
					//	if (team != null)
					//	{
					//		await SelectProject(project.Id, team.Id);
					//	}
					//	else
					//	{
					//		await _popupService.DisplayAlert(AppResources.ErrorPopupTitle, AppResources.NoTeamsForProjectError);
					//	}
					//}
					_navService.SetMainPage(Locator.ProjectDetailsPage);
					MessagingCenter.Send(this, Messages.SetProjectMessage, project);
				});
			}
		}

		public ProjectsListViewModel(ITfsService tfsService, IExtNavigationService navService, IPopupService popupService) : base(navService, popupService)
		{
			_tfsService = tfsService;
			MessagingCenter.Subscribe<LoginViewModel, CollectionResponse<Project>>(this, Messages.SetProjectsListMessage, (sender, args) =>
			{
				Projects = new ObservableCollection<Project>(args.Value);
			});
		}
		private async Task GetProjects()
		{
			try
			{
				IsBusy = true;
				var projects = await _tfsService.GetProjects();
				Projects = new ObservableCollection<Project>(projects.Value);
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
