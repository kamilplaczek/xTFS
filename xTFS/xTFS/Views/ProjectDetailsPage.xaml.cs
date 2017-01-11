using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using xTFS.Helpers;
using xTFS.ViewModels;

namespace xTFS.Views
{
	public partial class ProjectDetailsPage : MasterDetailPage
	{
		public ProjectDetailsPage()
		{
			InitializeComponent();
			MessagingCenter.Subscribe<IterationsListViewModel, bool>(this, Messages.SetIterationsListPresentedMessage, (sender, present) =>
			{
				IsPresented = present;
			});
		}
	}
}
