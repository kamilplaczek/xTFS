using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace xTFS.Rest.Exceptions
{
	public class ServiceException : Exception
	{
		public HttpStatusCode StatusCode { get; private set; }

		public ServiceException(string message) : base(message)
		{
		}

		public ServiceException(HttpStatusCode statusCode)
			: base(String.Format("Request failed. {0}", statusCode))
		{
			StatusCode = statusCode;
		}

		public ServiceException(Exception e) : base(e.Message)
		{
		}
	}
}
