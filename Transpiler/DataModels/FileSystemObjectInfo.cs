using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Media;

namespace Transpiler
{
	public class FileSystemObjectInfo : BaseObject
	{
		public FileSystemObjectInfo(DriveInfo drive)
			: this(drive.RootDirectory)
		{
		}

		public FileSystemObjectInfo(FileSystemInfo info)
		{
			if (this is DummyFileSystemObjectInfo)
			{
				return;
			}

			Children = new ObservableCollection<FileSystemObjectInfo>();
			FileSystemInfo = info;

			if (info is DirectoryInfo)
			{
				ImageSource = FolderManager.GetImageSource(info.FullName, ItemState.Close);
				AddDummy();
			}
			else if (info is FileInfo)
			{
				ImageSource = FileManager.GetImageSource(info.FullName);
			}

			PropertyChanged += new PropertyChangedEventHandler(FileSystemObjectInfo_PropertyChanged);
		}


		public ObservableCollection<FileSystemObjectInfo> Children
		{
			get => GetValue<ObservableCollection<FileSystemObjectInfo>>("Children");
			private set => SetValue("Children", value);
		}

		public ImageSource ImageSource
		{
			get => GetValue<ImageSource>("ImageSource");
			private set => SetValue("ImageSource", value);
		}

		public bool IsExpanded
		{
			get => GetValue<bool>("IsExpanded");
			set => SetValue("IsExpanded", value);
		}

		public FileSystemInfo FileSystemInfo
		{
			get => GetValue<FileSystemInfo>("FileSystemInfo");
			private set => SetValue("FileSystemInfo", value);
		}

		private DriveInfo Drive
		{
			get => GetValue<DriveInfo>("Drive");
			set => SetValue("Drive", value);
		}

		private void AddDummy()
		{
			Children.Add(new DummyFileSystemObjectInfo());
		}

		private bool HasDummy()
		{
			return !object.ReferenceEquals(GetDummy(), null);
		}

		private DummyFileSystemObjectInfo GetDummy()
		{
			var list = Children.OfType<DummyFileSystemObjectInfo>().ToList();
			if (list.Count > 0) return list.First();
			return null;
		}

		private void RemoveDummy()
		{
			Children.Remove(GetDummy());
		}

		private void ExploreDirectories()
		{
			if (Drive?.IsReady == false)
			{
				return;
			}
			if (FileSystemInfo is DirectoryInfo info)
			{
				var directories = info.GetDirectories();
				foreach (var directory in directories.OrderBy(d => d.Name))
				{
					if ((directory.Attributes & FileAttributes.System) != FileAttributes.System &&
						(directory.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden)
					{
						var fileSystemObject = new FileSystemObjectInfo(directory);
						Children.Add(fileSystemObject);
					}
				}
			}
		}

		private void ExploreFiles()
		{
			if (Drive?.IsReady == false)
			{
				return;
			}
			if (FileSystemInfo is DirectoryInfo info)
			{
				var files = info.GetFiles();
				foreach (var file in files.OrderBy(d => d.Name))
				{
					if ((file.Attributes & FileAttributes.System) != FileAttributes.System &&
						(file.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden)
					{
						Children.Add(new FileSystemObjectInfo(file));
					}
				}
			}
		}

		private void FileSystemObjectInfo_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (FileSystemInfo is DirectoryInfo)
			{
				if (string.Equals(e.PropertyName, "IsExpanded", StringComparison.CurrentCultureIgnoreCase))
				{
					if (IsExpanded)
					{
						ImageSource = FolderManager.GetImageSource(FileSystemInfo.FullName, ItemState.Open);
						if (HasDummy())
						{
							RemoveDummy();
							ExploreDirectories();
							ExploreFiles();
						}
					}
					else
					{
						ImageSource = FolderManager.GetImageSource(FileSystemInfo.FullName, ItemState.Close);
					}
				}
			}
		}
	}
}