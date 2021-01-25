using System.IO;

namespace Transpiler
{
	public class Transpiler
	{
		public static void Main(string[] args)
		{
			string codeFilePath = @"C:\Users\konts\source\repos\Transpiler\Transpiler\Translators\TranslatorsFromPascal\Test.pas";
			string patternsFilePath = @"C:\Users\konts\source\repos\Transpiler\Transpiler\Translators\TranslatorsFromPascal\PascalToC\PascalToC.json";

			var jsonReader = new JsonReader(patternsFilePath);
			var streamReader = new StreamReader(codeFilePath);
			string[] code = streamReader.ReadToEnd().Replace("\r", "\n").Split('\n');
			streamReader.Close();

			var translator = new PascalToCTranslator(code, jsonReader);
			string translatedCode = translator.Translate();

			string translatedCodeFilePath = codeFilePath.Replace(".pas", ".c");
			var streamWriter = new StreamWriter(translatedCodeFilePath);
			streamWriter.Write(translatedCode);
			streamWriter.Close();
		}
	}
}