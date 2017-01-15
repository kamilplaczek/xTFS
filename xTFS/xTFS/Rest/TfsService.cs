using RestSharp.Portable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xTFS.Resources;
using xTFS.Rest.Models;

namespace xTFS.Rest
{
	public class TfsService : BaseService, ITfsService
	{
		public async Task<ProjectDetails> GetProject(string id)
		{
			var result = await ExecuteRequest<ProjectDetails>($"/DefaultCollection/_apis/projects/{id}?api-version=1.0", Method.GET);
			return result;
		}

		public async Task<CollectionResponse<Project>> GetProjects()
		{
			var result = await ExecuteRequest<CollectionResponse<Project>>("/DefaultCollection/_apis/projects?api-version=1.0", Method.GET);
			return result;
		}

		public async Task<CollectionResponse<Team>> GetTeams(string projectId)
		{
			var result = await ExecuteRequest<CollectionResponse<Team>>($"DefaultCollection/_apis/projects/{projectId}/teams?api-version=1.0", Method.GET);
			return result;
		}

		public async Task<CollectionResponse<TeamMember>> GetTeamMembers(string project, string team)
		{
			var result = await ExecuteRequest<CollectionResponse<TeamMember>>($"DefaultCollection/_apis/projects/{project}/teams/{team}/members/?api-version=2.2", Method.GET);
			return result;
		}

		public async Task<CollectionResponse<Iteration>> GetIterations(string projectId, string teamId)
		{
			var result = await ExecuteRequest<CollectionResponse<Iteration>>($"DefaultCollection/{projectId}/{teamId}/_apis/work/teamsettings/iterations?api-version=v2.0-preview", Method.GET);
			return result;
		}

		public async Task<IEnumerable<int>> GetWorkItemIdsByIteration(string project, string iteration)
		{
			var query = String.Format(WIQLQueries.GetWorkItemsByIteration, project, iteration);
			var result = await GetWorkItemsByQuery(project, query);
			return result?.WorkItemRelations.Select(w => w.Target).Select(t => t.Id);
		}

		public async Task<CollectionResponse<WorkItem>> GetWorkItems(IEnumerable<int> ids)
		{
			var param = String.Join(",", ids);
			var result = await ExecuteRequest<CollectionResponse<WorkItem>>(
				$"DefaultCollection/_apis/wit/workitems?ids={param}&fields=System.Title,System.AssignedTo,System.WorkItemType,System.State,Microsoft.VSTS.Common.Priority&api-version=1.0", Method.GET);
			return result;
		}

		public async Task<WorkItem> GetWorkItemDetails(int id)
		{
			var result = await ExecuteRequest<CollectionResponse<WorkItem>>(
				$"DefaultCollection/_apis/wit/workitems?ids={id}&fields=System.Title,System.Description,System.AssignedTo,System.IterationPath,System.WorkItemType,System.State,Microsoft.VSTS.Common.Priority&api-version=1.0", Method.GET);
			return result?.Value?.FirstOrDefault();
		}

		public async Task<WorkItem> UpdateWorkItem(int id, IEnumerable<WorkItemPatch> patches)
		{
			var result = await ExecuteRequest<WorkItem>($"DefaultCollection/_apis/wit/workitems/{id}?api-version=1.0", Method.PATCH, patches, "application/json-patch+json");
			return result;
		}

		public async Task<WorkItem> CreateWorkItem(string project, string itemType, IEnumerable<WorkItemPatch> patches)
		{
			var result = await ExecuteRequest<WorkItem>($"DefaultCollection/{project}/_apis/wit/workitems/${itemType}?api-version=1.0", Method.PATCH, patches, "application/json-patch+json");
			return result;
		}

		private async Task<WIQLResponse> GetWorkItemsByQuery(string project, string query)
		{
			var body = new WIQLRequest()
			{
				Query = query
			};
			var result = await ExecuteRequest<WIQLResponse>($"DefaultCollection/{project}/_apis/wit/wiql?api-version=1.0", Method.POST, body);
			return result;
		}
	}
}
