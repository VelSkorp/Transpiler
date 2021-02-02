using System.Collections.Generic;
using System.Text;

namespace Transpiler
{
	public class BaseTranslatorFromPascal : BaseTranslator
	{
		protected Dictionary<string, List<string>> Variables { get; set; }

		public BaseTranslatorFromPascal(string[] code, IPatternsReader patternsReader)
			: base(code, patternsReader)
		{
			DataTypes = new string[] { "integer", "real", "boolean", "char", "string" };

			Variables = ParseVariables();
		}

		public override string Translate()
		{
			return base.Translate();
		}

		private Dictionary<string, List<string>> ParseVariables()
		{
			var variables = new Dictionary<string, List<string>>();
			var variablesNames = new List<string>();
			int i = 0;

			while (i < Code.Length)
			{
				string codeRow = Code[i];

				foreach (var dataType in DataTypes)
				{
					if (codeRow.Contains(dataType))
					{
						var variableName = new StringBuilder();

						for (int j = codeRow.IndexOf(' ') + 1; j < codeRow.IndexOf(':') + 1; j++)
						{
							if (codeRow[j] == ',' || codeRow[j] == ':')
							{
								variablesNames.Add(variableName.ToString());
								variableName.Clear();
							}
							else
							{
								variableName.Append(codeRow[j]);
							}
						}

						if (variables.ContainsKey(dataType))
						{
							variables[dataType].AddRange(variablesNames);
						}
						else
						{
							variables.Add(dataType, variablesNames);
						}

						Code[i] = "";
					}
				}

				i++;
			}

			return variables;
		}
	}
}