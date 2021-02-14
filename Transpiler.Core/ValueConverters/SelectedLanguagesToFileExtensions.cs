using System;
using System.Diagnostics;

namespace Transpiler.Core
{
    public static class SelectedLanguagesToFileExtensions
    {
        public static string[] Convert(string selectedLanguages)
        {
            switch (selectedLanguages)
            {
                case "CToPascal":
                    return new string[] { ".c", ".pas" };

                case "PascalToC":
                    return new string[] { ".paas", ".c" };

                default:
                    Debugger.Break();
                    throw new InvalidOperationException("Converter needs correct selected languages");
            }
        }
    }
}