using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xTFS.Rest.Models
{
	public class ProjectCollection
	{
		public int Count { get; set; }
		public IEnumerable<Project> Value { get; set; }
	}
}
