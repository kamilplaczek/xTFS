using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace xTFS.Rest.Enums
{
	public enum WorkItemState
	{
		[EnumMember(Value = "In Progress")]
		InProgress,
		[EnumMember(Value = "To Do")]
		ToDo,
		[EnumMember(Value = "Removed")]
		Removed,
		[EnumMember(Value = "Done")]
		Done
	}
}
