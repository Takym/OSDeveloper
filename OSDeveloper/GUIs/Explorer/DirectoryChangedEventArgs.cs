using System;
using OSDeveloper.IO.ItemManagement;

namespace OSDeveloper.GUIs.Explorer
{
	public class DirectoryChangedEventArgs : EventArgs
	{
		public FolderMetadata OldDirectory { get; }
		public FolderMetadata NewDirectory { get; }
		public bool           IsRefreshing { get; }

		public DirectoryChangedEventArgs(FolderMetadata oldDir, FolderMetadata newDir, bool isRefreshing)
		{
			this.OldDirectory = oldDir;
			this.NewDirectory = newDir;
			this.IsRefreshing = isRefreshing;
		}
	}

	public delegate void DirectoryChangedEventHandler(object sender, DirectoryChangedEventArgs e);
}
