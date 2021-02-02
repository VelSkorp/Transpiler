using System.Collections.Generic;

namespace Transpiler.Core
{
	public interface IPatternsReader
	{
		string GetValue(string key);
		List<string> GetKeys();
	}
}