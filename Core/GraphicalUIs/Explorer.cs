using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using OSDeveloper.Core.FileManagement;
using OSDeveloper.Native;

namespace OSDeveloper.Core.GraphicalUIs
{
	/// <summary>
	///  メイン画面の左側に表示されるエクスプローラを表します。
	/// </summary>
	[DefaultEvent(nameof(DirectoryChanged))]
	public partial class Explorer : UserControl
	{
		/// <summary>
		///  このエクスプローラで表示するディレクトリを取得または設定します。
		/// </summary>
		public DirMetadata Directory
		{
			get
			{
				return _dir;
			}

			set
			{
				_dir = value;
				this.OnDirectoryChanged();
			}
		}
		private DirMetadata _dir;

		/// <summary>
		///  ディレクトリが変更された時に発生します。
		/// </summary>
		public event EventHandler DirectoryChanged;

		/// <summary>
		///  型'<see cref="OSDeveloper.Core.GraphicalUIs.Explorer"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		public Explorer()
		{
			InitializeComponent();
		}

		private void Explorer_Load(object sender, System.EventArgs e)
		{
			imageList.Images.Add(Libosdev.GetIcon("Folder",      this, out int v0));
			imageList.Images.Add(Libosdev.GetIcon("FolderClose", this, out int v1));
			imageList.Images.Add(Libosdev.GetIcon("FolderOpen",  this, out int v2));
			imageList.Images.Add(Libosdev.GetIcon("BinaryFile",  this, out int v3));
			imageList.Images.Add(Libosdev.GetIcon("TextFile",    this, out int v4));
		}
		private const int IconFolder      = 0;
		private const int IconFolderClose = 1;
		private const int IconFolderOpen  = 2;
		private const int IconBinFile     = 3;
		private const int IconTxtFile     = 4;

		/// <summary>
		///  <see cref="OSDeveloper.Core.GraphicalUIs.Explorer.DirectoryChanged"/>イベントを発生させます。
		/// </summary>
		protected void OnDirectoryChanged()
		{
			treeView.Nodes.Clear();
			if (_dir != null) {
				var root = treeView.Nodes.Add($"{Path.GetFileNameWithoutExtension(_dir.FileName)} ({_dir.FileName})");
				root.ImageIndex = IconFolder;
				root.Expand();
				this.SetDirectoryTo(root, _dir);
			}

			this.DirectoryChanged?.Invoke(this, new EventArgs());
		}

		private void SetDirectoryTo(TreeNode tree, DirMetadata dir)
		{
			foreach (var item in dir.GetDirectories()) {
				var child = tree.Nodes.Add(item);
				child.ImageIndex = IconFolderClose;
				child.Collapse();
				this.SetDirectoryTo(child, dir.CreateDirectory(item));
			}

			foreach (var item in dir.GetFiles()) {
				var child = tree.Nodes.Add(item);
				var file = dir.CreateFile(item);
				if (file.Format == FileFormat.TextFile) {
					child.ImageIndex = IconTxtFile;
				} else {
					child.ImageIndex = IconBinFile;
				}
			}
		}

		private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
		{

		}

		private void treeView_AfterExpand(object sender, TreeViewEventArgs e)
		{
			e.Node.ImageIndex = IconFolderOpen;
		}

		private void treeView_AfterCollapse(object sender, TreeViewEventArgs e)
		{
			e.Node.ImageIndex = IconFolderClose;
		}

		private void treeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
		{

		}

		private void treeView_AfterCheck(object sender, TreeViewEventArgs e)
		{

		}
	}
}
