using System.IO;

namespace Transpiler
{
	public class Transpiler
	{
		public static void Main(string[] args)
		{
			string codeFilePath = @"C:\Users\konts\source\repos\Transpiler\Transpiler\Translators\TranslatorsFromC\Test.c";
			string patternsFilePath = @"C:\Users\konts\source\repos\Transpiler\Transpiler\Translators\TranslatorsFromC\CToPascal.json";

			var jsonReader = new JsonReader(patternsFilePath);
			var streamReader = new StreamReader(codeFilePath);
			string[] code = streamReader.ReadToEnd().Split('\n');
			streamReader.Close();

			var translator = new CToPascalTranslator(code, jsonReader);
			string translatedCode = translator.Translate();

			string translatedCodeFilePath = codeFilePath.Replace(".c", ".pas");
			var streamWriter = new StreamWriter(translatedCodeFilePath);
			streamWriter.Write(translatedCode);
			streamWriter.Close();
		}
	}
}