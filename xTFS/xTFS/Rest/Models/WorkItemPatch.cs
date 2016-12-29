using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xTFS.Rest.Enums;

namespace xTFS.Rest.Models
{
	public class WorkItemPatch
	{
		[JsonConverter(typeof(StringEnumConverter))]
		public WorkItemPatchOperator Op { get; set; }
		public string Path { get; set; }
		public object Value { get; set; }
	}
}
