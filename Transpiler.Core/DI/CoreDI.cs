using Dna;

namespace Transpiler.Core
{
    /// <summary>
    /// A shorthand access class to get DI services with nice clean short code
    /// </summary>
    public static class CoreDI
    {
        /// <summary>
        /// A shortcut to access the <see cref="TranslatorContext"/>
        /// </summary>
        public static TranslatorContext TranslatorContext => Framework.Service<TranslatorContext>();
    }
}
