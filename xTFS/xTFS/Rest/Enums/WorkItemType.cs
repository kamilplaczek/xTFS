using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace xTFS.Rest.Enums
{
	public enum WorkItemType
	{
		[EnumMember(Value = "Task")]
		Task,
		[EnumMember(Value = "Product Backlog Item")]
		PBI,
		[EnumMember(Value = "Bug")]
		Bug,
		[EnumMember(Value = "Feature")]
		Feature,
		[EnumMember(Value = "Epic")]
		Epic
	}
}
