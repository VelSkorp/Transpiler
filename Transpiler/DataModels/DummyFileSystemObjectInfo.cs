using System.IO;

namespace Transpiler
{
	internal class DummyFileSystemObjectInfo : FileSystemObjectInfo
	{
		public DummyFileSystemObjectInfo()
			: base(new DirectoryInfo("DummyFileSystemObjectInfo"))
		{
		}
	}
}