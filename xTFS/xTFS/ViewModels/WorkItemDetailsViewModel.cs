using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
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
using xTFS.Rest.Enums;
using xTFS.Rest.Models;

namespace xTFS.ViewModels
{
	public class WorkItemDetailsViewModel : BaseViewModel
	{
		private readonly ITfsService _tfsService;

		private WorkItem _workItem;
		private ObservableCollection<string> _teamMembers;
		private ObservableCollection<string> _iterations;
		private ObservableCollection<string> _workItemStates;
		private string _selectedTeamMember;
		private string _selectedIteration;
		private string _selectedState;

		public ObservableCollection<string> WorkItemStates
		{
			get
			{
				return _workItemStates;
			}
			set
			{
				Set(ref _workItemStates, value);
			}
		}

		public string SelectedTeamMember
		{
			get
			{
				return _selectedTeamMember;
			}
			set
			{
				Set(ref _selectedTeamMember, value);
			}
		}

		public string SelectedState
		{
			get
			{
				return _selectedState;
			}
			set
			{
				Set(ref _selectedState, value);
			}
		}

		public string SelectedIteration
		{
			get
			{
				return _selectedIteration;
			}
			set
			{
				Set(ref _selectedIteration, value);
			}
		}

		public ObservableCollection<string> TeamMembers
		{
			get
			{
				return _teamMembers;
			}
			set
			{
				Set(ref _teamMembers, value);
			}
		}

		public ObservableCollection<string> Iterations
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

		public WorkItem WorkItem
		{
			get
			{
				return _workItem;
			}
			set
			{
				Set(ref _workItem, value);
			}
		}

		public ICommand CancelCommand
		{
			get
			{
				return new RelayCommand(() =>
				{
					GoBack();
				});
			}
		}

		public ICommand SaveCommand
		{
			get
			{
				return new RelayCommand(async () =>
				{
					await SaveWorkItem(_workItem);
				});
			}
		}

		public WorkItemDetailsViewModel(ITfsService tfsService, IExtNavigationService navService) : base(navService)
		{
			_tfsService = tfsService;
			MessagingCenter.Subscribe<WorkItemsListViewModel, WorkItem>(this, Messages.SetWorkItemMessage, async (sender, args) =>
			{
				await GetWorkItemDetails(args);
			});
		}

		private async Task GetWorkItemDetails(WorkItem item)
		{
			// TODO: possibly refactor for cleaner way to get project context
			var project = SimpleIoc.Default.GetInstance<IterationsListViewModel>()?.Project;
			if (project != null)
			{
				var iterations = await _tfsService.GetIterations(project.Id, project.DefaultTeam.Id);
				var teamMembers = await _tfsService.GetTeamMembers(project.Id, project.DefaultTeam.Id);
				TeamMembers = new ObservableCollection<string>(teamMembers.Value.Select(m => m.DisplayName));
				Iterations = new ObservableCollection<string>(iterations.Value.Select(i => $"{project.Name}\\{i.Name}"));
				WorkItemStates = new ObservableCollection<string>(GetEnumMemberValues(typeof(WorkItemState)));
				WorkItem = await _tfsService.GetWorkItemDetails(item.Id);
				SelectedIteration = Iterations.FirstOrDefault(i => i == WorkItem.Fields.Iteration);
				SelectedTeamMember = TeamMembers.FirstOrDefault(m => WorkItem.Fields.AssignedTo.Contains(m));
				SelectedState = WorkItemStates.FirstOrDefault(s => s == WorkItem.Fields.State);
			}
			else
			{
				// something went terribly wrong - invalid app state
				GoBack();
			}
		}

		private async Task SaveWorkItem(WorkItem item)
		{
			// prep patches
			var patches = new List<WorkItemPatch>();
			var fields = item.Fields;
			patches.Add(new WorkItemPatch()
			{
				Op = WorkItemPatchOperator.Add,
				Value = fields.Title,
				Path = GetWorkItemPatchPath(nameof(fields.Title))
			});
			patches.Add(new WorkItemPatch()
			{
				Op = WorkItemPatchOperator.Add,
				Value = fields.Description,
				Path = GetWorkItemPatchPath(nameof(fields.Description))
			});
			patches.Add(new WorkItemPatch()
			{
				Op = WorkItemPatchOperator.Add,
				Value = fields.Priority,
				Path = GetWorkItemPatchPath(nameof(fields.Priority))
			});
			patches.Add(new WorkItemPatch()
			{
				Op = WorkItemPatchOperator.Add,
				Value = _selectedIteration,
				Path = GetWorkItemPatchPath(nameof(fields.Iteration))
			});
			patches.Add(new WorkItemPatch()
			{
				Op = WorkItemPatchOperator.Add,
				Value = _selectedState,
				Path = GetWorkItemPatchPath(nameof(fields.State))
			});
			if (!String.IsNullOrEmpty(_selectedTeamMember))
			{
				patches.Add(new WorkItemPatch()
				{
					Op = WorkItemPatchOperator.Add,
					Value = _selectedTeamMember,
					Path = GetWorkItemPatchPath(nameof(fields.AssignedTo))
				});
			}
			var result = await _tfsService.UpdateWorkItem(item.Id, patches);
			GoBack();
		}

		private string GetWorkItemPatchPath(string member)
		{
			return $"/fields/{typeof(WorkItemFields).GetSerializedMemberName(member)}";
		}

		private void GoBack()
		{
			SelectedTeamMember = null;
			SelectedIteration = null;
			SelectedState = null;
			_navService.GoBack();
		}
	}
}
