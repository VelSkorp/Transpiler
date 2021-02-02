using System;

namespace Transpiler
{
	public static class StringToTranslator
	{
		public static ITranslator Convert(string translatorName, string[] code, IPatternsReader patternsReader)
		{
			switch (translatorName)
			{
				case "CToPascal":
					return new CToPascalTranslator(code, patternsReader);

				case "PascalToC":
					return new PascalToCTranslator(code, patternsReader);

				default:
					return new BaseTranslator(code, patternsReader);
			}
		}
	}
}