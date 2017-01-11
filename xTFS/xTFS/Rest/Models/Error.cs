using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xTFS.Rest.Models
{
	public class Error
	{
		public int Count { get; set; }
		public ErrorValue Value { get; set; }
	}

	public class ErrorValue
	{
		public string Message { get; set; }
	}
}
