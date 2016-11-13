using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xTFS.Rest.Models;

namespace xTFS.Rest
{
	public interface ITfsService : IBaseService
	{
		Task<ProjectCollection> GetProjects();
	}
}
