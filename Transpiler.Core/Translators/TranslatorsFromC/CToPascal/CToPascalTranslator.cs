using System.Text;

namespace Transpiler.Core
{
	public class CToPascalTranslator: BaseTranslatorFromC
	{
		public CToPascalTranslator(string[] code, IPatternsReader patternsReader)
			: base(code, patternsReader)
		{
		}

		public override string Translate()
		{
			int i = 0;
			bool deleteNextRow = false;

			while (i < Code.Length)
			{
				if (deleteNextRow)
				{
					Code[i] = "\n";
					deleteNextRow = false;
				}

				if (Code[i].Contains("for "))
				{
					deleteNextRow = true;
				}

				i++;
			}

			var code = base.Translate();
			string declaredVariables = DeclaringVariables();

			code = code.Insert(0, declaredVariables);

			code = code.Remove(code.Length - 1, 1);

			return  code.Insert(code.Length, ".");
		}
		private string DeclaringVariables()
		{
			var variables = new StringBuilder("var ");

			foreach (var variablesType in Variables)
			{
				for (int i = 0; i < variablesType.Value.Count; i++)
				{
					variables.Append($"{variablesType.Value[i]}");

					if (i < variablesType.Value.Count - 1)
					{
						variables.Append(",");
					}
				}

				switch (variablesType.Key)
				{
					case "char ":
					case "unsigned char ":
					case "signed char ":
						variables.Append($":char");
						break;

					case "int ":
					case "unsigned int ":
					case "signed int ":
					case "short int ":
					case "unsigned short int ":
					case "signed short int ":
					case "long int ":
					case "unsigned long int ":
					case "signed long int ":
						variables.Append($":integer");
						break;

					case "float ":
					case "double ":
					case "long double ":
						variables.Append($":real");
						break;

					case "char* ":
					case "char[] ":
						variables.Append($":string");
						break;

					default:
						break;
				}


				variables.Append(";\n\t");
			}

			return variables.ToString();

		}
	}
}