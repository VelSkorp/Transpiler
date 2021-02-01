using Dna;
using System.IO;

namespace Transpiler
{
	public class Transpiler
	{
		public static void Main(string[] args)
		{
			string codeFilePath = @"C:\Users\konts\source\repos\Transpiler\Transpiler\Translators\TranslatorsFromPascal\Test.pas";
			string patternsFilePath = @"C:\Users\konts\source\repos\Transpiler\Transpiler\Translators\TranslatorsFromPascal\PascalToC\PascalToC.json";

			// Setup the Dna Framework
			Framework.Construct<DefaultFrameworkConstruction>()
				.AddFileLogger()
				.AddTranslatorContext()
				.Build();

			var streamReader = new StreamReader(codeFilePath);
			string[] code = streamReader.ReadToEnd().Replace("\r", "\n").Split('\n');
			streamReader.Close();

			var patternsReader = StringToPatternsReader.Convert("Json", patternsFilePath);

			DI.TranslatorContext.Translator = StringToTranslator.Convert("PascalToC", code, patternsReader);

			string translatedCode = DI.TranslatorContext.Translator.Translate();

			string translatedCodeFilePath = codeFilePath.Replace(".pas", ".c");
			var streamWriter = new StreamWriter(translatedCodeFilePath);
			streamWriter.Write(translatedCode);
			streamWriter.Close();
		}
	}
}