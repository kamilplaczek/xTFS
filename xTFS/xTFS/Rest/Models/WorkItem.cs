using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xTFS.Rest.Models
{
	public class WorkItem
	{
		public int Id { get; set; }
		public int Rev { get; set; }
		// TODO: finish mapping WI fields
		public string Url { get; set; }
	}
}
