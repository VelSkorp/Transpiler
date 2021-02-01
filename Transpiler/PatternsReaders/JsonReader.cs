using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Transpiler
{
	public class JsonReader : IPatternsReader
	{
		private string _filePath;

		public JsonReader(string filePath)
		{
			_filePath = filePath;
		}

		public string GetValue(string key)
		{
			string json = Read();
			var data = (JObject)JsonConvert.DeserializeObject(json);

			var a = data.Properties().ToList();

			return data[key].Value<string>();
		}

		public List<string> GetKeys()
		{
			string json = Read();
			var data = (JObject)JsonConvert.DeserializeObject(json);

			return (from property in data.Properties()
					select property.Name).ToList();
		}

		private string Read()
		{
			return File.ReadAllText(_filePath);
		}
	}
}