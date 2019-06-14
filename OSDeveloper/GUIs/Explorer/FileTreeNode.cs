using System.Drawing;
using System.IO;
using System.Windows.Forms;
using OSDeveloper.GUIs.Editors;
using OSDeveloper.GUIs.Terminal;
using OSDeveloper.IO;
using OSDeveloper.IO.ItemManagement;
using OSDeveloper.IO.Logging;
using OSDeveloper.Resources;

namespace OSDeveloper.GUIs.Explorer
{
	public class FileTreeNode : TreeNode
	{
		protected Logger       Logger   { get; }
		public    ItemMetadata Metadata { get; }
		public    ItemProperty Property { get; set; }
		public    EditorWindow Editor   { get; set; }

		public FolderMetadata Folder
		{
			get => this.Metadata as FolderMetadata;
		}

		public FileMetadata File
		{
			get => this.Metadata as FileMetadata;
		}

		private protected FileTreeNode()
		{
			this.Logger = Logger.Get(this.GetType().Name);
		}

		public FileTreeNode(ItemMetadata meta) : this()
		{
			this.Metadata = meta;
			this.Text     = meta.Name;
			this.SetStyle();
			this.Logger.Trace($"constructed {nameof(FileTreeNode)}, path:{meta.Path}");
		}

		public void SetStyle()
		{
			if (this.Metadata.CanAccess) {
				if (this.File != null) {
					switch (this.File.Format) {
						case FileFormat.Binary:
							this.ImageIndex         = IconList.BinaryFile;
							this.SelectedImageIndex = IconList.BinaryFile;
							break;
						case FileFormat.Text:
							this.ImageIndex         = IconList.TextFile;
							this.SelectedImageIndex = IconList.TextFile;
							break;
						case FileFormat.Program:
							this.ImageIndex         = IconList.ProgramFile;
							this.SelectedImageIndex = IconList.ProgramFile;
							break;
						case FileFormat.SourceCode:
							this.ImageIndex         = IconList.SourceFile;
							this.SelectedImageIndex = IconList.SourceFile;
							break;
						case FileFormat.Resource:
							this.ImageIndex         = IconList.ResourceFile;
							this.SelectedImageIndex = IconList.ResourceFile;
							break;
						case FileFormat.Document:
							this.ImageIndex         = IconList.DocumentFile;
							this.SelectedImageIndex = IconList.DocumentFile;
							break;
						case FileFormat.Solution:
							this.ImageIndex         = IconList.SolutionFile;
							this.SelectedImageIndex = IconList.SolutionFile;
							break;
						default:
							this.ImageIndex         = IconList.File;
							this.SelectedImageIndex = IconList.File;
							break;
					}
				} else if (this.Folder != null) {
					switch (this.Folder.Format) {
						case FolderFormat.Directory:
							if (this.Folder.IsEmpty()) {
								this.ImageIndex         = IconList.Directory;
								this.SelectedImageIndex = IconList.Directory;
							} else if (this.IsExpanded) {
								this.ImageIndex         = IconList.DirOpened;
								this.SelectedImageIndex = IconList.DirOpened;
							} else {
								this.ImageIndex         = IconList.DirClosed;
								this.SelectedImageIndex = IconList.DirClosed;
							}
							break;
						case FolderFormat.FloppyDisk:
							this.ImageIndex         = IconList.FloppyDisk;
							this.SelectedImageIndex = IconList.FloppyDisk;
							break;
						case FolderFormat.HardDisk:
							this.ImageIndex         = IconList.HardDisk;
							this.SelectedImageIndex = IconList.HardDisk;
							break;
						case FolderFormat.OpticalDisc:
							this.ImageIndex         = IconList.OpticalDisc;
							this.SelectedImageIndex = IconList.OpticalDisc;
							break;
						case FolderFormat.Junction:
							if (this.Folder.IsEmpty()) {
								this.ImageIndex         = IconList.Junction;
								this.SelectedImageIndex = IconList.Junction;
							} else if (this.IsExpanded) {
								this.ImageIndex         = IconList.JunOpened;
								this.SelectedImageIndex = IconList.JunOpened;
							} else {
								this.ImageIndex         = IconList.JunClosed;
								this.SelectedImageIndex = IconList.JunClosed;
							}
							break;
						case FolderFormat.Solution:
							this.ImageIndex         = IconList.Solution;
							this.SelectedImageIndex = IconList.Solution;
							break;
						default:
							if (this.Folder.IsEmpty()) {
								this.ImageIndex         = IconList.Folder;
								this.SelectedImageIndex = IconList.Folder;
							} else if (this.IsExpanded) {
								this.ImageIndex         = IconList.FolderOpened;
								this.SelectedImageIndex = IconList.FolderOpened;
							} else {
								this.ImageIndex         = IconList.FolderClosed;
								this.SelectedImageIndex = IconList.FolderClosed;
							}
							break;
					}
				}
				byte r = 0, g = 0, b = 0;
				if (this.Metadata.Attributes.HasFlag(FileAttributes.System)) {
					g |= 0x80;
				}
				if (this.Metadata.Attributes.HasFlag(FileAttributes.Compressed) ||
					this.Metadata.Attributes.HasFlag(FileAttributes.Encrypted)) {
					b |= 0x80;
				}
				if (this.Metadata.Attributes.HasFlag(FileAttributes.Hidden) ||
					this.Metadata.Attributes.HasFlag(FileAttributes.Temporary)) {
					r |= 0x7F; g |= 0x7F; b |= 0x7F;
				}
				this.ForeColor = Color.FromArgb(r, g, b);
			} else {
				this.ImageIndex         = IconList.CannotAccess;
				this.SelectedImageIndex = IconList.CannotAccess;
				this.ForeColor          = Color.Red;
			}

			Logger.Notice($"finished to set styles to: {this.Metadata.Path}");
			Logger.Info($"icon id:{this.ImageIndex}, color:{this.ForeColor}");
		}
	}

	internal class RemovedTreeNode : FileTreeNode
	{
		private protected RemovedTreeNode() : base() { }

		public RemovedTreeNode(ItemMetadata meta) : base(meta)
		{
			this.Text = $"{ExplorerTexts.RemovedTreeNode} ({meta.Path})";
			this.Logger.Trace($"constructed {nameof(RemovedTreeNode)}, path:{meta.Path}");
		}
	}

	internal sealed class DummyTreeNode : RemovedTreeNode
	{
		public readonly static DummyTreeNode Instance = new DummyTreeNode();

		private DummyTreeNode() : base()
		{
			this.Text = this.GetType().FullName;
			this.BackColor = Color.FromArgb(0xCC, 0xCC, 0xCC);
			this.ForeColor = Color.FromArgb(0x22, 0x22, 0x22);
			this.Logger.Trace($"constructed {nameof(DummyTreeNode)}");
		}
	}
}
