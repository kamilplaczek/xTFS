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
using xTFS.Rest;
using xTFS.Rest.Models;

namespace xTFS.ViewModels
{
	public class IterationsListViewModel : BaseViewModel
	{
		private readonly ITfsService _tfsService;

		private ObservableCollection<Iteration> _iterations;
		private ProjectDetails _project;

		public ObservableCollection<Iteration> Iterations
		{
			get
			{
				return _iterations;
			}
			set
			{
				Set(ref _iterations, value);
			}
		}

		public ProjectDetails Project
		{
			get
			{
				return _project;
			}
			set
			{
				Set(ref _project, value);
			}
		}

		public ICommand SelectIterationCommand
		{
			get
			{
				return new RelayCommand<Iteration>(async (iteration) =>
				{
					await SetIteration(iteration);
				});
			}
		}

		public ICommand GoBackCommand
		{
			get
			{
				return new RelayCommand(() =>
				{
					_navService.SetMainPage(Locator.ProjectsListPage);
				});
			}
		}

		public IterationsListViewModel(ITfsService tfsService, IExtNavigationService navService) : base(navService)
		{
			_tfsService = tfsService;
			MessagingCenter.Subscribe<ProjectsListViewModel, Project>(this, Messages.SetProjectMessage, async (sender, args) =>
			{
				await GetProjectDetails(args.Id);
			});
		}

		private async Task GetProjectDetails(string id)
		{
			Project = await _tfsService.GetProject(id);
			var iterations = await _tfsService.GetIterations(id, _project.DefaultTeam.Id);
			if (iterations.Value.Any())
			{
				Iterations = new ObservableCollection<Iteration>(iterations.Value);
				var now = DateTime.Now;
				var selectedIteration = Iterations.FirstOrDefault(i => i.Attributes.StartDate.HasValue && i.Attributes.StartDate.Value <= now
					&& i.Attributes.EndDate.HasValue && i.Attributes.EndDate.Value >= now) ?? Iterations.First();
				await SetIteration(selectedIteration);
			}
		}

		private async Task SetIteration(Iteration iteration)
		{
			var ids = await _tfsService.GetWorkItemIdsByIteration(_project.Name, iteration.Name);
			MessagingCenter.Send(this, Messages.SetIterationMessage, ids);
			_navService.NavigateTo(Locator.WorkItemsListPage);
		}
	}
}
