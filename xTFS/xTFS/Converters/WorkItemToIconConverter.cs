using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using xTFS.Rest.Enums;
using xTFS.Rest.Models;

namespace xTFS.Converters
{
	public class WorkItemToIconConverter : IValueConverter
	{
		private const string TaskNotDoneResourceKey = "TaskNotDoneIconStyle";
		private const string TaskDoneResourceKey = "TaskDoneIconStyle";
		private const string PBINotDoneResourceKey = "PBINotDoneIconStyle";
		private const string PBIDoneResourceKey = "PBIDoneIconStyle";
		private const string BugNotDoneResourceKey = "BugNotDoneIconStyle";
		private const string BugDoneResourceKey = "BugDoneIconStyle";

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var workItem = value as WorkItem;
			if (workItem != null)
			{
				string key = null;
				switch (workItem.Fields.WorkItemType)
				{
					case WorkItemType.Bug:
						{
							key = workItem.Fields.State == PBIState.Done.ToString() ? BugDoneResourceKey : BugNotDoneResourceKey;
							break;
						}
					case WorkItemType.PBI:
						{
							key = workItem.Fields.State == PBIState.Done.ToString() ? PBIDoneResourceKey : PBINotDoneResourceKey;
							break;
						}
					case WorkItemType.Task:
						{
							key = workItem.Fields.State == TaskState.Done.ToString() ? TaskDoneResourceKey : TaskNotDoneResourceKey;
							break;
						}
				}
				return Application.Current.Resources[key];
			}
			return null;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
