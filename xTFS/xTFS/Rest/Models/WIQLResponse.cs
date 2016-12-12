using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xTFS.Rest.Models
{
	public class WIQLResponse
	{
		public string QueryType { get; set; }
		public DateTime AsOf { get; set; }
		public List<WorkItemRelation> WorkItemRelations { get; set; }
	}

	public class WorkItemRelation
	{
		public Target Target { get; set; }
	}

	public class Target
	{
		public int Id { get; set; }
		public string Url { get; set; }
	}
}
