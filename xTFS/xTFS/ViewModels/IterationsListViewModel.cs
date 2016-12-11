using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using xTFS.Helpers;
using xTFS.Rest;
using xTFS.Rest.Models;

namespace xTFS.ViewModels
{
	public class IterationsListViewModel : ViewModelBase
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


		public IterationsListViewModel(ITfsService tfsService)
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
			Iterations = new ObservableCollection<Iteration>(iterations.Value);
		}
	}
}
