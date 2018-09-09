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
	///  このクラスは継承できません。
	/// </summary>
	[DefaultEvent(nameof(DirectoryChanged))]
	public sealed partial class Explorer : UserControl
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
				var root = new FileTreeNode($"{Path.GetFileNameWithoutExtension(_dir.FileName)} ({_dir.FileName})", _dir);
				root.Expand();
				treeView.Nodes.Add(root);
				this.SetDirectoryTo(root, _dir);
				this.SetIconEx(root);
			}

			this.DirectoryChanged?.Invoke(this, new EventArgs());
		}

		private void SetDirectoryTo(TreeNode tree, DirMetadata dir)
		{
			foreach (var item in dir.GetDirectories()) {
				var file = dir.CreateDirectory(item);
				var child = new FileTreeNode(item, file);
				child.Collapse();
				tree.Nodes.Add(child);
				this.SetDirectoryTo(child, file);
			}

			foreach (var item in dir.GetFiles()) {
				var file = dir.CreateFile(item);
				var child = new FileTreeNode(item, file);
				tree.Nodes.Add(child);
			}
		}

		private void SetIconEx(FileTreeNode node)
		{
			if (node == null) return;
			this.SetIcon(node);
			foreach (var item in node.Nodes) {
				this.SetIconEx(item as FileTreeNode);
			}
		}

		private void SetIcon(FileTreeNode node)
		{
			if (node == null) return;
			switch (node.File.Format) {
				case FileFormat.Directory:
					if (node.Nodes.Count == 0) {
						node.ImageIndex = IconFolder;
						node.SelectedImageIndex = IconFolder;
					} else if (node.IsExpanded) {
						node.ImageIndex = IconFolderOpen;
						node.SelectedImageIndex = IconFolderOpen;
					} else {
						node.ImageIndex = IconFolderClose;
						node.SelectedImageIndex = IconFolderClose;
					}
					break;
				case FileFormat.TextFile:
					node.ImageIndex = IconTxtFile;
					node.SelectedImageIndex = IconTxtFile;
					break;
				default:
					node.ImageIndex = IconBinFile;
					node.SelectedImageIndex = IconBinFile;
					break;
			}
		}

		private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
		{

		}

		private void treeView_AfterExpand(object sender, TreeViewEventArgs e)
		{
			this.SetIcon(e.Node as FileTreeNode);
		}

		private void treeView_AfterCollapse(object sender, TreeViewEventArgs e)
		{
			this.SetIcon(e.Node as FileTreeNode);
		}

		private void treeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
		{

		}

		private void treeView_AfterCheck(object sender, TreeViewEventArgs e)
		{

		}

		private class FileTreeNode : TreeNode
		{
			public FileMetadata File { get; }

			public FileTreeNode(string text, FileMetadata file) : base(text)
			{
				this.File = file;
			}
		}
	}
}
