using System.Collections.Generic;

namespace Transpiler
{
	public interface IPatternsReader
	{
		string GetValue(string key);
		List<string> GetKeys();
	}
}