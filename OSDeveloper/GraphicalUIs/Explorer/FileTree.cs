using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using OSDeveloper.GraphicalUIs.Terminal;
using OSDeveloper.IO;
using OSDeveloper.IO.ItemManagement;
using OSDeveloper.IO.Logging;
using OSDeveloper.Native;
using OSDeveloper.Resources;

namespace OSDeveloper.GraphicalUIs.Explorer
{
	// TODO: エクスプローラで開く、限定の外部アプリで開く メニューを実装する

	public partial class FileTree : UserControl
	{
		#region プロパティ

		private readonly Logger   _logger;
		private readonly FormMain _mwnd;

		public FolderMetadata Directory
		{
			get
			{
				return _dir;
			}

			set
			{
				_dir = value;
				this.OnDirectoryChanged(new EventArgs());
			}
		}
		private FolderMetadata _dir;

		public event EventHandler DirectoryChanged;

		#endregion

		#region コンストラクタ

		public FileTree(FormMain mwnd)
		{
			_logger = Logger.Get(nameof(FileTree));
			_mwnd   = mwnd;
			this.InitializeComponent();

			btnRefresh.Image  = Libosdev.GetIcon(Libosdev.Icons.MiscRefresh,  out uint vRef).ToBitmap();
			btnRefresh.Text   = ExplorerTexts.BtnRefresh;
			btnExpand.Image   = Libosdev.GetIcon(Libosdev.Icons.MiscExpand,   out uint vExp).ToBitmap();
			btnExpand.Text    = ExplorerTexts.BtnExpand;
			btnCollapse.Image = Libosdev.GetIcon(Libosdev.Icons.MiscCollapse, out uint vCol).ToBitmap();
			btnCollapse.Text  = ExplorerTexts.BtnCollapse;

			renameMenu.Text   = ExplorerTexts.PopupMenu_Rename;
			deleteMenu.Text   = ExplorerTexts.PopupMenu_Delete;
			propertyMenu.Text = ExplorerTexts.PopupMenu_Property;

			iconList.Images.AddRange(IconList.CreateImageArray());

			_logger.Trace($"constructed {nameof(FileTree)}");
		}

		#endregion

		#region 読み込みイベント
		protected override void OnLoad(EventArgs e)
		{
			_logger.Trace($"executing {nameof(OnLoad)}...");
			base.OnLoad(e);

			_logger.Trace($"completed {nameof(OnLoad)}");
		}

		protected virtual async void OnDirectoryChanged(EventArgs e)
		{
			_logger.Trace($"executing {nameof(OnDirectoryChanged)}...");
			_logger.Info($"started to load the dir: {this.Directory.Path}");
			this.DirectoryChanged?.Invoke(this, e);

			// ファイルまたはディレクトリを読み込んでFileTreeNode生成する。
			var node = await Task.Run(() => {
				if (this.Directory.IsRemoved){ // 削除されているディレクトリが参照された場合
					return new RemovedTreeNode(this.Directory);
				} else {
					return this.CreateTreeNode(this.Directory);
				}
			});

			// TreeViewに生成したFileTreeNodeを追加する。
			treeView.Nodes.Clear();
			treeView.Nodes.Add(node);

			// 一番上のノード(Root Node)の設定を変更する。
			if (node is FileTreeNode ftn) {
				ftn.Text = $"{this.Directory.Path.GetFileNameWithoutExtension()} ({this.Directory.Path})";
				ftn.Expand();
				this.SetStyleToTreeNode(ftn);
			}

			_logger.Info($"finished to load the dir: {this.Directory.Path}");
			_logger.Trace($"completed {nameof(OnDirectoryChanged)}");
		}

		private TreeNode CreateTreeNode(ItemMetadata item)
		{
			// == null だと、正しく動作しない可能性がある
			if (item is null) return DummyTreeNode.Instance;

			var result = new FileTreeNode(item);
			result.ContextMenuStrip = popupMenu;
			// != null だと、正しく動作しない可能性がある
			if (!(result.Folder is null) && result.Folder.CanAccess && !result.Folder.IsEmpty()) {
				var fo = result.Folder.GetFolders();
				for (int i = 0; i < fo.Length; ++i) {
					if (!fo[i].IsRemoved) result.Nodes.Add(this.CreateTreeNode(fo[i]));
				}
				var fi = result.Folder.GetFiles();
				for (int i = 0; i < fi.Length; ++i) {
					if (!fi[i].IsRemoved) result.Nodes.Add(this.CreateTreeNode(fi[i]));
				}
			}
			this.SetStyleToTreeNode(result);

			_logger.Info($"loaded the dir or file: {item.Path}");
			return result;
		}

		private void SetStyleToTreeNode(FileTreeNode node)
		{
			if (node.Metadata.CanAccess) {
				// != null だと、正しく動作しない可能性がある
				if (!(node.File is null)) {
					switch (node.File.Format) {
						case FileFormat.Binary:
							node.ImageIndex         = IconList.BinaryFile;
							node.SelectedImageIndex = IconList.BinaryFile;
							break;
						case FileFormat.Text:
							node.ImageIndex         = IconList.TextFile;
							node.SelectedImageIndex = IconList.TextFile;
							break;
						case FileFormat.Program:
							node.ImageIndex         = IconList.ProgramFile;
							node.SelectedImageIndex = IconList.ProgramFile;
							break;
						case FileFormat.SourceCode:
							node.ImageIndex         = IconList.SourceFile;
							node.SelectedImageIndex = IconList.SourceFile;
							break;
						case FileFormat.Resource:
							node.ImageIndex         = IconList.ResourceFile;
							node.SelectedImageIndex = IconList.ResourceFile;
							break;
						case FileFormat.Document:
							node.ImageIndex         = IconList.DocumentFile;
							node.SelectedImageIndex = IconList.DocumentFile;
							break;
						default:
							node.ImageIndex         = IconList.File;
							node.SelectedImageIndex = IconList.File;
							break;
					}
				} else if (!(node.Folder is null)) { // != null だと、正しく動作しない可能性がある
					switch (node.Folder.Format) {
						case FolderFormat.Directory:
							if (node.Folder.IsEmpty()) {
								node.ImageIndex         = IconList.Directory;
								node.SelectedImageIndex = IconList.Directory;
							//} else if (!node.IsExpanded) { // .NET の仕様なのか IsExpanded の値は何故か反転している。
							} else if (node.IsExpanded) { // 何故か直ってた
								node.ImageIndex         = IconList.DirOpened;
								node.SelectedImageIndex = IconList.DirOpened;
							} else {
								node.ImageIndex         = IconList.DirClosed;
								node.SelectedImageIndex = IconList.DirClosed;
							}
							break;
						case FolderFormat.FloppyDisk:
							node.ImageIndex         = IconList.FloppyDisk;
							node.SelectedImageIndex = IconList.FloppyDisk;
							break;
						case FolderFormat.HardDisk:
							node.ImageIndex         = IconList.HardDisk;
							node.SelectedImageIndex = IconList.HardDisk;
							break;
						case FolderFormat.OpticalDisc:
							node.ImageIndex         = IconList.OpticalDisc;
							node.SelectedImageIndex = IconList.OpticalDisc;
							break;
						case FolderFormat.Junction:
							if (node.Folder.IsEmpty()) {
								node.ImageIndex         = IconList.Junction;
								node.SelectedImageIndex = IconList.Junction;
							} else if (!node.IsExpanded) { // .NET の仕様なのか IsExpanded の値は何故か反転している。
								node.ImageIndex         = IconList.JunOpened;
								node.SelectedImageIndex = IconList.JunOpened;
							} else {
								node.ImageIndex         = IconList.JunClosed;
								node.SelectedImageIndex = IconList.JunClosed;
							}
							break;
						default:
							if (node.Folder.IsEmpty()) {
								node.ImageIndex         = IconList.Folder;
								node.SelectedImageIndex = IconList.Folder;
							} else if (!node.IsExpanded) { // .NET の仕様なのか IsExpanded の値は何故か反転している。
								node.ImageIndex         = IconList.FolderOpened;
								node.SelectedImageIndex = IconList.FolderOpened;
							} else {
								node.ImageIndex         = IconList.FolderClosed;
								node.SelectedImageIndex = IconList.FolderClosed;
							}
							break;
					}
				}
				byte r = 0, g = 0, b = 0;
				if (node.Metadata.Attributes.HasFlag(FileAttributes.System)) {
					g |= 0x80;
				}
				if (node.Metadata.Attributes.HasFlag(FileAttributes.Compressed) ||
					node.Metadata.Attributes.HasFlag(FileAttributes.Encrypted)) {
					b |= 0x80;
				}
				if (node.Metadata.Attributes.HasFlag(FileAttributes.Hidden) ||
					node.Metadata.Attributes.HasFlag(FileAttributes.Temporary)) {
					r |= 0x7F; g |= 0x7F; b |= 0x7F;
				}
				node.ForeColor = Color.FromArgb(r, g, b);
			} else {
				node.ImageIndex         = IconList.CannotAccess;
				node.SelectedImageIndex = IconList.CannotAccess;
				node.ForeColor = Color.Red;
			}

			_logger.Notice($"finished to set styles to: {node.Metadata.Path}");
			_logger.Info($"icon id:{node.ImageIndex}, color:{node.ForeColor}");
		}

		#endregion

		#region Button イベント
		private void btnRefresh_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(btnRefresh_Click)}...");

			// ディレクトリが再読み込みされて、ツリービューが再設定される。
			_dir.Refresh();
			this.OnDirectoryChanged(new EventArgs());

			_logger.Trace($"completed {nameof(btnRefresh_Click)}");
		}

		private void btnExpand_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(btnExpand_Click)}...");
			treeView.ExpandAll();
			_logger.Trace($"completed {nameof(btnExpand_Click)}");
		}

		private void btnCollapse_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(btnCollapse_Click)}...");
			treeView.CollapseAll();
			_logger.Trace($"completed {nameof(btnCollapse_Click)}");
		}
		#endregion

		#region TreeView イベント

		/* ---- Select ---- */

		private void treeView_BeforeSelect(object sender, TreeViewCancelEventArgs e)
		{
			_logger.Trace($"executing {nameof(treeView_BeforeSelect)}...");

			_logger.Trace($"completed {nameof(treeView_BeforeSelect)}");
		}

		private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
		{
			_logger.Trace($"executing {nameof(treeView_AfterSelect)}...");

			_logger.Trace($"completed {nameof(treeView_AfterSelect)}");
		}

		/* ---- Expand ---- */

		private void treeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
		{
			_logger.Trace($"executing {nameof(treeView_BeforeExpand)}...");

			_logger.Trace($"completed {nameof(treeView_BeforeExpand)}");
		}

		private void treeView_AfterExpand(object sender, TreeViewEventArgs e)
		{
			_logger.Trace($"executing {nameof(treeView_AfterExpand)}...");

			if (e.Node is FileTreeNode node) {
				this.SetStyleToTreeNode(node);
			}

			_logger.Trace($"completed {nameof(treeView_AfterExpand)}");
		}

		/* ---- Collapse ---- */

		private void treeView_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
		{
			_logger.Trace($"executing {nameof(treeView_BeforeCollapse)}...");

			_logger.Trace($"completed {nameof(treeView_BeforeCollapse)}");
		}

		private void treeView_AfterCollapse(object sender, TreeViewEventArgs e)
		{
			_logger.Trace($"executing {nameof(treeView_AfterCollapse)}...");

			if (e.Node is FileTreeNode node) {
				this.SetStyleToTreeNode(node);
			}

			_logger.Trace($"completed {nameof(treeView_AfterCollapse)}");
		}

		/* ---- LabelEdit ---- */

		private void treeView_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
		{
			_logger.Trace($"executing {nameof(treeView_BeforeLabelEdit)}...");

			_logger.Trace($"completed {nameof(treeView_BeforeLabelEdit)}");
		}

		private void treeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
		{
			_logger.Trace($"executing {nameof(treeView_AfterLabelEdit)}...");

			if (e.Node is FileTreeNode node) {
				if (node.Metadata.Rename(e.Label)) {
					_logger.Info($"the item name changed: {node.Metadata.Path}");
				} else {
					e.CancelEdit = true;
					MessageBox.Show(this,
						ExplorerTexts.Msgbox_CannotChangeName,
						ASMINFO.Caption,
						MessageBoxButtons.OK,
						MessageBoxIcon.Warning);
				}
			}

			_logger.Trace($"completed {nameof(treeView_AfterLabelEdit)}");
		}

		/* ---- Check ---- */
		/* // チェックボックスは利用しない

		private void treeView_BeforeCheck(object sender, TreeViewCancelEventArgs e)
		{
			_logger.Trace($"executing {nameof(treeView_BeforeCheck)}...");

			_logger.Trace($"completed {nameof(treeView_BeforeCheck)}");
		}

		private void treeView_AfterCheck(object sender, TreeViewEventArgs e)
		{
			_logger.Trace($"executing {nameof(treeView_AfterCheck)}...");

			_logger.Trace($"completed {nameof(treeView_AfterCheck)}");
		}

		//*/

		#endregion

		#region ContextMenuStrip イベント

		private void renameMenu_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(renameMenu_Click)}...");

			treeView.SelectedNode.BeginEdit();

			_logger.Trace($"completed {nameof(renameMenu_Click)}");
		}

		private void deleteMenu_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(deleteMenu_Click)}...");

			if (treeView.SelectedNode is FileTreeNode node) {
				if (node.Metadata.Delete()) {
					node.Remove();
				} else {
					MessageBox.Show(this,
						ExplorerTexts.Msgbox_CannotDelete,
						ASMINFO.Caption,
						MessageBoxButtons.OK,
						MessageBoxIcon.Warning);
				}
			}

			_logger.Trace($"completed {nameof(deleteMenu_Click)}");
		}

		private void propertyMenu_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(propertyMenu_Click)}...");

			if (treeView.SelectedNode is FileTreeNode node) {
				// == null だと、正しく動作しない可能性がある
				if (node.Property is null || node.Property.IsDisposed) {
					node.Property = new ItemProperty(node.Metadata);
				}
				_mwnd.OpenTab(node.Property);
			}

			_logger.Trace($"completed {nameof(propertyMenu_Click)}");
		}

		#endregion

		#region FileTreeViewItem クラス

		private class FileTreeNode : TreeNode
		{
			public ItemMetadata Metadata { get; }
			public ItemProperty Property { get; set; }

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

		#endregion

		#region DummyTreeNode クラス

		public sealed class DummyTreeNode : TreeNode
		{
			public readonly static DummyTreeNode Instance = new DummyTreeNode();

			private DummyTreeNode()
			{
				this.Text      = this.GetType().FullName;
				this.BackColor = Color.FromArgb(0xCC, 0xCC, 0xCC);
				this.ForeColor = Color.FromArgb(0x22, 0x22, 0x22);
			}
		}

		#endregion

		#region RemovedTreeNode クラス

		private class RemovedTreeNode : TreeNode
		{
			public ItemMetadata Metadata { get; }

			public RemovedTreeNode(ItemMetadata meta)
			{
				this.Metadata = meta;
				this.Text = $"{ExplorerTexts.RemovedTreeNode} ({meta.Path})";
			}
		}

		#endregion
	}
}
