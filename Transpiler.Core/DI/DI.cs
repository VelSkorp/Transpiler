using Dna;

namespace Transpiler
{
    /// <summary>
    /// A shorthand access class to get DI services with nice clean short code
    /// </summary>
    public static class DI
    {
        /// <summary>
        /// A shortcut to access the <see cref="TranslatorContext"/>
        /// </summary>
        public static TranslatorContext TranslatorContext => Framework.Service<TranslatorContext>();
    }
}
