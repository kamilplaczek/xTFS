using Newtonsoft.Json;
using RestSharp.Portable;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xTFS.Rest.Base
{
	/// <summary>
	/// The default JSON deserializer using Json.Net
	/// </summary>
	public class JsonNetDeserializer : IDeserializer
	{
		/// <summary>
		/// Deserialize the response
		/// </summary>
		/// <typeparam name="T">Object type to deserialize the result to</typeparam>
		/// <param name="response">The response to deserialize the result from</param>
		/// <returns>The deserialized object</returns>
		public T Deserialize<T>(IRestResponse response)
		{
			return JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(response.RawBytes, 0, response.RawBytes.Length));
		}

		/// <summary>
		/// Configure the JsonSerializer
		/// </summary>
		/// <param name="serializer">The serializer to configure</param>
		protected virtual void ConfigureSerializer(JsonSerializer serializer)
		{
		}
	}
}
