using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
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
					_navService.GoBack();
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
			WorkItem = await _tfsService.GetWorkItemDetails(item.Id);
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
			var result = await _tfsService.UpdateWorkItem(item.Id, patches);
			// TODO: patch rest of the fields
			_navService.GoBack();
		}

		private string GetWorkItemPatchPath(string member)
		{
			return $"/fields/{typeof(WorkItemFields).GetSerializedMemberName(member)}";
		}
	}
}
