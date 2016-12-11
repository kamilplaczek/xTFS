using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xTFS.Rest.Models
{
	public class CollectionResponse<T>
	{
		public int Count { get; set; }
		public IEnumerable<T> Value { get; set; }
	}
}
