using Dna;
using Microsoft.Extensions.DependencyInjection;

namespace Transpiler.Core
{
	/// <summary>
	/// Extension methods for the <see cref="FrameworkConstruction"/>
	/// </summary>
	public static class FrameworkConstructionExtensions
	{
		/// <summary>
		/// Injects the translator context needed for transpiler application
		/// </summary>
		/// <param name="construction"></param>
		/// <returns></returns>
		public static FrameworkConstruction AddTranslatorContext(this FrameworkConstruction construction)
		{
			// Bind to a single instance of Translator Context
			construction.Services.AddSingleton<TranslatorContext>();

			// Return the construction for chaining
			return construction;
		}
	}
}