using System;
using System.Text;
using System.Linq;

namespace Transpiler.Core
{
	public class PascalToCTranslator : BaseTranslatorFromPascal
	{
		public PascalToCTranslator(string[] code, IPatternsReader patternsReader)
			: base(code, patternsReader)
		{
		}

		public override string Translate()
		{
			int i = 0;
			bool deleteNextRow = false;
			string declaredVariables = DeclaringVariables();

			while (i < Code.Length)
			{
				if (deleteNextRow)
				{
					Code[i] = "\n";
					deleteNextRow = false;
				}

				if (Code[i] == "Begin" && (Code[i - 1] == "\n" || Code[i - 1] == "\r" || Code[i - 1] == "\t" || string.IsNullOrEmpty(Code[i - 1])))
				{
					Code[i] = Code[i].Replace("Begin", $"int main()\nBegin\n\t{declaredVariables}\n");
				}

				i++;
			}

			string code = base.Translate();

			code = code.Insert(0, "#include <stdio.h>\n\n");

			return code;
		}

		private string DeclaringVariables()
		{
			var variables = new StringBuilder("");

			foreach (var variablesType in Variables)
			{
				switch (variablesType.Key)
				{
					case "char":
						variables.Append("char ");
						break;

					case "integer":
					case "boolean":
						variables.Append("int ");
						break;

					case "real":
						variables.Append("double ");
						break;

					case "string":
						variables.Append("char[] ");
						break;

					default:
						break;
				}

				for (int i = 0; i < variablesType.Value.Count; i++)
				{
					variables.Append($"{variablesType.Value[i]}");

					if (i < variablesType.Value.Count - 1)
					{
						variables.Append(",");
					}
				}

				variables.Append(";\n\t");
			}

			return variables.ToString();

		}
	}
}