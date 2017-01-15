using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace xTFS.Rest.Enums
{
	public enum TaskState
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

	public enum PBIState
	{
		[EnumMember(Value = "Approved")]
		Approved,
		[EnumMember(Value = "Committed")]
		Commited,
		[EnumMember(Value = "New")]
		New,
		[EnumMember(Value = "Removed")]
		Removed,
		[EnumMember(Value = "Done")]
		Done
	}
}
