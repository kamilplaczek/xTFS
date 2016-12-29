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
		Task<IEnumerable<int>> GetWorkItemIdsByIteration(string project, string iteration);
		Task<CollectionResponse<WorkItem>> GetWorkItems(IEnumerable<int> ids);
		Task<WorkItem> GetWorkItemDetails(int id);
		Task<WorkItem> UpdateWorkItem(int id, IEnumerable<WorkItemPatch> patches);
		Task<CollectionResponse<TeamMember>> GetTeamMembers(string project, string team);
	}
}
