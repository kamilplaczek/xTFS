using RestSharp.Portable;
using RestSharp.Portable.Authenticators;
using RestSharp.Portable.HttpClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using xTFS.Rest.Exceptions;

namespace xTFS.Rest
{
	public class BaseService : IDisposable, IBaseService
	{
		private readonly RestClient _restClient = new RestClient();

		public void Dispose()
		{
			_restClient.Dispose();
		}

		public void Init(string username, string password, string baseUrl)
		{
			_restClient.BaseUrl = new Uri(baseUrl);
			_restClient.Authenticator = new HttpBasicAuthenticator(username, password);
		}

		protected async Task<T> ExecuteRequest<T>(string url, Method method, object body = null)
		{
			var request = CreateRequest(url, method, body);
			var result = await _restClient.Execute<T>(request);
			HandleResult(result);
			return result.Data;
		}

		private RestRequest CreateRequest(string url, Method method, object body = null)
		{
			var request = new RestRequest(url, method);
			if (body != null)
			{
				request.AddBody(body);
			}
			return request;
		}

		private void HandleResult(IRestResponse result)
		{
			if (result.StatusCode != HttpStatusCode.OK)
			{
				if (result.RawBytes != null && result.RawBytes.Any())
				{
					throw new ServiceException(Encoding.UTF8.GetString(result.RawBytes, 0, result.RawBytes.Length));
				}
				throw new ServiceException(result.StatusCode);
			}
		}
	}
}
