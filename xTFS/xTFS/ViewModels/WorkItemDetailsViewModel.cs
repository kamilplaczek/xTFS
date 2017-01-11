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
using xTFS.Rest.Exceptions;
using xTFS.Rest.Models;
using xTFS.Services;

namespace xTFS.ViewModels
{
	public class WorkItemDetailsViewModel : BaseViewModel
	{
		private readonly ITfsService _tfsService;

		private string _projectName;
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

		public WorkItemDetailsViewModel(ITfsService tfsService, IExtNavigationService navService, IPopupService popupService) : base(navService, popupService)
		{
			_tfsService = tfsService;
			MessagingCenter.Subscribe<WorkItemsListViewModel, WorkItem>(this, Messages.SetWorkItemMessage, async (sender, args) =>
			{
				await InitForm();
				await SetWorkItemDetails(args);
			});
		}

		private async Task SetWorkItemDetails(WorkItem item)
		{
			try
			{
				if (item.Id != 0)
				{
					item = await _tfsService.GetWorkItemDetails(item.Id);
					SelectedIteration = Iterations.FirstOrDefault(i => i == item.Fields.Iteration);
					SelectedTeamMember = TeamMembers.FirstOrDefault(m => item.Fields.AssignedTo.Contains(m));
					SelectedState = WorkItemStates.FirstOrDefault(s => s == item.Fields.State);
				}
				WorkItem = item;
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

		private async Task InitForm()
		{
			try
			{
				IsBusy = true;
				var project = SimpleIoc.Default.GetInstance<IterationsListViewModel>()?.Project;
				if (project != null)
				{
					_projectName = project.Name;
					var iterations = await _tfsService.GetIterations(project.Id, project.DefaultTeam.Id);
					var teamMembers = await _tfsService.GetTeamMembers(project.Id, project.DefaultTeam.Id);
					TeamMembers = new ObservableCollection<string>(teamMembers.Value.Select(m => m.DisplayName));
					Iterations = new ObservableCollection<string>(iterations.Value.Select(i => $"{project.Name}\\{i.Name}"));
					WorkItemStates = new ObservableCollection<string>(GetEnumMemberValues(typeof(WorkItemState)));
				}
				else
				{
					// something went terribly wrong - invalid app state
					GoBack();
				}
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
			try
			{
				WorkItem result;
				if (_workItem.Id != 0)
				{
					result = await _tfsService.UpdateWorkItem(item.Id, patches);
				}
				else
				{
					result = await _tfsService.CreateTask(_projectName, patches);
				}
				GoBack();
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
