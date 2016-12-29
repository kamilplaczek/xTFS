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
	public class WorkItemFields
	{
		[JsonProperty(PropertyName = "System.State")]
		[JsonConverter(typeof(StringEnumConverter))]
		public WorkItemState State { get; set; }

		[JsonProperty(PropertyName = "System.WorkItemType")]
		[JsonConverter(typeof(StringEnumConverter))]
		public WorkItemType WorkItemType { get; set; }

		[JsonProperty(PropertyName = "System.AssignedTo")]
		public string AssignedTo { get; set; }

		[JsonProperty(PropertyName = "System.Title")]
		public string Title { get; set; }

		[JsonProperty(PropertyName = "System.Description")]
		public string Description { get; set; }

		[JsonProperty(PropertyName = "System.IterationPath")]
		public string Iteration { get; set; }

		[JsonProperty(PropertyName = "Microsoft.VSTS.Common.Priority")]
		public int Priority { get; set; }
	}
}
