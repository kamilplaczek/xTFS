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
using xTFS.Rest.Exceptions;
using xTFS.Rest.Models;
using xTFS.Services;

namespace xTFS.ViewModels
{
	public class WorkItemsListViewModel : BaseViewModel
	{
		private readonly ITfsService _tfsService;
		private ObservableCollection<WorkItem> _workItems;

		public ObservableCollection<WorkItem> WorkItems
		{
			get
			{
				return _workItems;
			}
			set
			{
				Set(ref _workItems, value);
			}
		}

		public ICommand SelectWorkItemCommand
		{
			get
			{
				return new RelayCommand<WorkItem>((item) =>
				{
					_navService.NavigateTo(Locator.WorkItemDetailsPage);
					MessagingCenter.Send(this, Messages.SetWorkItemMessage, item);
				});
			}
		}

		public ICommand CreateWorkItemCommand
		{
			get
			{
				return new RelayCommand(() =>
				{
					_navService.NavigateTo(Locator.WorkItemDetailsPage);
					var model = new WorkItem()
					{
						Fields = new WorkItemFields()
					};
					MessagingCenter.Send(this, Messages.SetWorkItemMessage, model);
				});
			}
		}

		public WorkItemsListViewModel(ITfsService tfsService, IExtNavigationService navService, IPopupService popupService) : base(navService, popupService)
		{
			_tfsService = tfsService;
			MessagingCenter.Subscribe<IterationsListViewModel, IEnumerable<int>>(this, Messages.SetWorkItemsListMessage, async (sender, args) =>
			{
				if (WorkItems != null)
				{
					WorkItems.Clear();
				}
				await GetWorkItems(args);
			});
		}

		private async Task GetWorkItems(IEnumerable<int> ids)
		{
			try
			{
				IsBusy = true;
				var workItems = await _tfsService.GetWorkItems(ids);
				WorkItems = new ObservableCollection<WorkItem>(workItems.Value);
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
