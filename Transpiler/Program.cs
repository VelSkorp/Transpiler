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
			string codeFilePath = @"C:\Users\konts\source\repos\Transpiler\Transpiler\Test.c";
			string patternsFilePath = @"C:\Users\konts\source\repos\Transpiler\Transpiler\CToPascal.json";

			var translator = new CToPascalTranslator(codeFilePath, patternsFilePath);
			var a = translator.Translate();

		}
	}
}