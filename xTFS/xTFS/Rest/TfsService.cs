using RestSharp.Portable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xTFS.Rest.Models;

namespace xTFS.Rest
{
	public class TfsService : BaseService, ITfsService
	{

		public async Task<ProjectCollection> GetProjects()
		{
			var result = await ExecuteRequest<ProjectCollection>("/DefaultCollection/_apis/projects?api-version=1.0", Method.GET);
			return result;
		}
	}
}
