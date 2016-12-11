using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xTFS.Rest.Models
{
	public class ProjectDetails : Project
	{
		public Team DefaultTeam { get; set; }
	}
}
