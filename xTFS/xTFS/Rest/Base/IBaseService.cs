using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xTFS.Rest
{
	public interface IBaseService
	{
		void Init(string username, string password, string baseUrl);
	}
}
