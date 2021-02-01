using System;
using System.Diagnostics;

namespace Transpiler
{
    public static class StringToPatternsReader
    {
        public static IPatternsReader Convert(string patternsReaderName, string filePath)
        {
            switch (patternsReaderName)
            {
                case "Json":
                    return new JsonReader(filePath);

                default:
                    Debugger.Break();
                    throw new InvalidOperationException("Converter needs correct patterns reader name");
            }
        }
    }
}