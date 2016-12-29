using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace xTFS.Helpers
{
	public static class Extensions
	{
		public static TValue GetAttribute<TAttribute, TValue>(this Type type, string memberName, Func<TAttribute, TValue> valueSelector, bool inherit = false) where TAttribute : Attribute
		{
			var member = type.GetTypeInfo().DeclaredMembers.FirstOrDefault(m => m.Name == memberName);
			if (member != null)
			{
				var att = member.GetCustomAttributes(typeof(TAttribute), inherit).FirstOrDefault() as TAttribute;
				if (att != null)
				{
					return valueSelector(att);
				}
			}
			return default(TValue);
		}

		public static string GetSerializedMemberName(this Type type, string memberName)
		{
			return GetAttribute<JsonPropertyAttribute, string>(type, memberName, p => p.PropertyName);
		}
	}
}
