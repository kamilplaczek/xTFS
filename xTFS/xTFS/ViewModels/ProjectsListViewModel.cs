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
using xTFS.Rest.Models;
using xTFS.Services;

namespace xTFS.ViewModels
{
	public class ProjectsListViewModel : BaseViewModel
	{
		private readonly ITfsService _tfsService;
		private readonly IPopupService _popupService;
		private readonly IExtNavigationService _navService;

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

		public ProjectsListViewModel(ITfsService tfsService, IPopupService popupService, IExtNavigationService navService)
		{
			_tfsService = tfsService;
			_popupService = popupService;
			_navService = navService;
			MessagingCenter.Subscribe<LoginViewModel, CollectionResponse<Project>>(this, Messages.SetProjectsListMessage, (sender, args) =>
			{
				Projects = new ObservableCollection<Project>(args.Value);
			});
		}
		private async Task GetProjects()
		{
			var projects = await _tfsService.GetProjects();
			Projects = new ObservableCollection<Project>(projects.Value);
		}
	}
}
