using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Transpiler
{
	public class ShellManager
	{
		public static Icon GetIcon(string path, ItemType type, IconSize iconSize, ItemState state)
		{
			var attributes = (uint)(type == ItemType.Folder ? FileAttribute.Directory : FileAttribute.File);
			var flags = (uint)(ShellAttribute.Icon | ShellAttribute.UseFileAttributes);

			if (type == ItemType.Folder && state == ItemState.Open)
			{
				flags |= (uint)ShellAttribute.OpenIcon;
			}

			flags = iconSize == IconSize.Small ? flags | (uint)ShellAttribute.SmallIcon : flags | (uint)ShellAttribute.LargeIcon;

			var fileInfo = new ShellFileInfo();
			var size = (uint)Marshal.SizeOf(fileInfo);
			var result = Interop.SHGetFileInfo(path, attributes, out fileInfo, size, flags);

			if (result == IntPtr.Zero)
			{
				throw Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error());
			}

			try
			{
				return (Icon)Icon.FromHandle(fileInfo.hIcon).Clone();
			}
			catch
			{
				throw;
			}
			finally
			{
				Interop.DestroyIcon(fileInfo.hIcon);
			}
		}
	}
}