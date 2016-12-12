using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using xTFS.Helpers;
using xTFS.Rest;

namespace xTFS.ViewModels
{
	public class WorkItemsListViewModel : ViewModelBase
	{
		private readonly ITfsService _tfsService;

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
		}

	}
}
