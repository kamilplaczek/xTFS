using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xTFS.Rest.Models
{
	public class TeamMember
	{
		public string Id { get; set; }
		public string DisplayName { get; set; }
		public string UniqueName { get; set; }
		public string ImageUrl { get; set; }
	}
}
