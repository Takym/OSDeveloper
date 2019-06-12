using System.Drawing;
using System.Windows.Forms;
using OSDeveloper.GUIs.Editors;
using OSDeveloper.GUIs.Terminal;
using OSDeveloper.IO.ItemManagement;
using OSDeveloper.Resources;

namespace OSDeveloper.GUIs.Explorer
{
	public class FileTreeNode : TreeNode
	{
		public ItemMetadata Metadata { get; }
		public ItemProperty Property { get; set; }
		public EditorWindow Editor { get; set; }

		public FolderMetadata Folder
		{
			get => this.Metadata as FolderMetadata;
		}

		public FileMetadata File
		{
			get => this.Metadata as FileMetadata;
		}

		public FileTreeNode(ItemMetadata meta)
		{
			this.Metadata = meta;
			this.Text = meta.Name;
		}
	}

	internal sealed class DummyTreeNode : TreeNode
	{
		public readonly static DummyTreeNode Instance = new DummyTreeNode();

		private DummyTreeNode()
		{
			this.Text = this.GetType().FullName;
			this.BackColor = Color.FromArgb(0xCC, 0xCC, 0xCC);
			this.ForeColor = Color.FromArgb(0x22, 0x22, 0x22);
		}
	}

	internal class RemovedTreeNode : TreeNode
	{
		public ItemMetadata Metadata { get; }

		public RemovedTreeNode(ItemMetadata meta)
		{
			this.Metadata = meta;
			this.Text = $"{ExplorerTexts.RemovedTreeNode} ({meta.Path})";
		}
	}
}
