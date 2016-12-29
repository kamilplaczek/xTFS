using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace xTFS.Rest.Enums
{
	public enum WorkItemPatchOperator
	{
		[EnumMember(Value = "add")]
		Add,
		[EnumMember(Value = "replace")]
		Replace,
		[EnumMember(Value = "remove")]
		Remove,
		[EnumMember(Value = "test")]
		Test
	}
}
