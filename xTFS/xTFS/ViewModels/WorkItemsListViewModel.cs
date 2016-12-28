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
using xTFS.Rest;
using xTFS.Rest.Models;

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
				return new RelayCommand<WorkItem>(async (item) =>
				{
				});
			}
		}

		public WorkItemsListViewModel(ITfsService tfsService)
		{
			_tfsService = tfsService;
			MessagingCenter.Subscribe<IterationsListViewModel, IEnumerable<int>>(this, Messages.SetIterationMessage, async (sender, args) => {
				await GetWorkItems(args);
			});
		}

		private async Task GetWorkItems(IEnumerable<int> ids)
		{
			var workItems = await _tfsService.GetWorkItems(ids);
			WorkItems = new ObservableCollection<WorkItem>(workItems.Value);
		}
	}
}
