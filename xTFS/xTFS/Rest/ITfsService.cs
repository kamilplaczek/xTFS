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
		Task<ProjectDetails> GetProject(string id);
		Task<CollectionResponse<Project>> GetProjects();
		Task<CollectionResponse<Team>> GetTeams(string projectId);
		Task<CollectionResponse<Iteration>> GetIterations(string projectId, string teamId);
	}
}
