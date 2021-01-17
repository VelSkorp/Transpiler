using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Transpiler
{
	public class Transpiler
	{
		public static void Main(string[] args)
		{
			var jsonReader = new JsonReader(@"C:\Users\konts\source\repos\ProgrammingLanguageConverter\ProgrammingLanguageConverter\CToPascal.json");
			var streamReader = new StreamReader(@"C:\Users\konts\source\repos\ProgrammingLanguageConverter\ProgrammingLanguageConverter\Test.c");
			var outupCode = new StringBuilder();
			string[] inputCode = streamReader.ReadToEnd().Split('\n');
			List<string> inputCodePatterns = jsonReader.GetKeys();

			foreach (var codeRow in inputCode)
			{
				string newRow = codeRow;

				foreach (var inputCodePattern in inputCodePatterns)
				{
					if (newRow.Contains(inputCodePattern))
					{
						string newValue = jsonReader.GetValue(inputCodePattern);
						newRow = newRow.Replace(inputCodePattern, newValue);
						break;
					}
					else if (inputCodePattern.Contains('{') && inputCodePattern.Contains('}'))
					{
						if (MatchesThePattern(codeRow, inputCodePattern, out Dictionary<string, string> keyValuePairs))
						{
							newRow = PutValuesInPattern(keyValuePairs, jsonReader.GetValue(inputCodePattern));
						}
					}
				}

				outupCode.Append(newRow);
			}


			var a = outupCode.ToString();
		}

		/// <summary>
		/// Сhecks if the pattern matches a line of code
		/// </summary>
		/// <returns>Returns true if the pattern matches the line of code</returns>
		public static bool MatchesThePattern(string codeRow, string pattern, out Dictionary<string, string> keyValuePairs)
		{
			keyValuePairs = new Dictionary<string, string>();
			var key = new StringBuilder();

			bool isKeyFound = false;
			int i = 0;

			codeRow = codeRow.Trim('\t');
			codeRow = codeRow.Trim('\r');

			while (i < pattern.Length)
			{
				if (pattern[i] == '{')
				{
					isKeyFound = true;
				}

				if (pattern[i] == '}' && isKeyFound)
				{
					isKeyFound = false;

					string value = GetValueForKey(key, codeRow, ref pattern);

					if (!keyValuePairs.ContainsKey(key.ToString()))
					{
						keyValuePairs.Add(key.ToString(), value);
					}

					i -= key.Length;
					i += value.Length - 1;

					key.Clear();
				}

				if (isKeyFound && pattern[i] != '{')
				{
					key.Append(pattern[i]);
				}

				i++;
			}

			return pattern == codeRow;
		}

		public static string PutValuesInPattern(Dictionary<string, string> keyValuePairs, string pattern)
		{
			var key = new StringBuilder();
			bool isKeyFound = false;
			int keyStart = 0;
			int i = 0;

			while (i < pattern.Length)
			{
				var a = pattern[i];

				if (pattern[i] == '{')
				{
					isKeyFound = true;
					keyStart = i;
				}

				if (pattern[i] == '}' && isKeyFound)
				{
					isKeyFound = false;

					string value = keyValuePairs.First((item) => item.Key == key.ToString()).Value;

					pattern = pattern.Remove(keyStart, i - keyStart + 1);
					pattern = pattern.Insert(keyStart, value);

					i -= key.Length;
					i += value.Length - 1;

					key.Clear();
				}

				if (isKeyFound && pattern[i] != '{')
				{
					key.Append(pattern[i]);
				}

				i++;
			}

			return pattern;
		}

		public static string GetValueForKey(StringBuilder inputKey, string code, ref string pattern)
		{
			var outpuKey = new StringBuilder();

			bool isKeyFound = false;
			int keyStart = 0;

			for (int i = 0; i < pattern.Length; i++)
			{
				if (pattern[i] == '{')
				{
					isKeyFound = true;
					keyStart = i;
					continue;
				}

				if (pattern[i] == '}')
				{
					isKeyFound = false;

					if (outpuKey.ToString() == inputKey.ToString())
					{
						return ParseValue(outpuKey, keyStart, code, ref pattern);
					}

					outpuKey.Clear();
				}

				if (isKeyFound)
				{
					outpuKey.Append(pattern[i]);
				}
			}

			return "";
		}

		public static string ParseValue(StringBuilder key, int keyStart, string code, ref string pattern)
		{
			if (keyStart >= code.Length)
			{
				pattern = pattern.Remove(pattern.IndexOf('{'), pattern.Length - pattern.IndexOf('{'));
				return "";
			}

			var value = new StringBuilder();

			for (int j = keyStart; j < code.Length; j++)
			{
				if (code[j] == ' ' || code[j] == ';' || code[j] == ')')
				{
					pattern = pattern.Remove(keyStart, key.Length + 2);
					pattern = pattern.Insert(keyStart, value.ToString());
					break;
				}

				value.Append(code[j]);
			}

			return value.ToString();
		}
	}
}