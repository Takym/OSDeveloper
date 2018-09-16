using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using OSDeveloper.Core.Editors;
using OSDeveloper.Core.FileManagement;
using OSDeveloper.Core.Logging;
using OSDeveloper.Native;

namespace OSDeveloper.Core.GraphicalUIs
{
	/// <summary>
	///  メイン画面の左側に表示されるエクスプローラを表します。
	///  このクラスは継承できません。
	/// </summary>
	[DefaultEvent(nameof(DirectoryChanged))]
	public partial class Explorer : UserControl
	{
		private Logger _logger;
		private MainWindowBase _mwndbase;

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
				this.OnDirectoryChanged(new EventArgs());
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
		public Explorer(MainWindowBase mwndbase)
		{
			_logger = Logger.GetSystemLogger(nameof(Explorer));
			_mwndbase = mwndbase;
			InitializeComponent();
		}

		private void Explorer_Load(object sender, System.EventArgs e)
		{
			_logger.Trace("The OnLoad event of Explorer was called");
			_logger.Info("Setting the tool strip bar for Explorer...");
			tolbtnRefresh.Image = Libosdev.GetIcon("Refresh", this, out int vREF).ToBitmap();
			tolbtnRefresh.Text = ExplorerTexts.Refresh;
			tolbtnRefresh.ToolTipText = ExplorerTexts.Refresh;
			tolbtnExpand.Image = Libosdev.GetIcon("Expand", this, out int vEXP).ToBitmap();
			tolbtnExpand.Text = ExplorerTexts.Expand;
			tolbtnExpand.ToolTipText = ExplorerTexts.Expand;
			tolbtnCollapse.Image = Libosdev.GetIcon("Collapse", this, out int vCOL).ToBitmap();
			tolbtnCollapse.Text = ExplorerTexts.Collapse;
			tolbtnCollapse.ToolTipText = ExplorerTexts.Collapse;

			_logger.Info("Setting the popup strip bar for Explorer...");
			popup_openeditor.Text = ExplorerTexts.Popup_OpenEditor;
			popup_rename.Text = ExplorerTexts.Popup_Rename;

			_logger.Info("Setting the file icons for Explorer...");
			imageList.Images.Add(Libosdev.GetIcon("Folder",      this, out int v0));
			imageList.Images.Add(Libosdev.GetIcon("FolderClose", this, out int v1));
			imageList.Images.Add(Libosdev.GetIcon("FolderOpen",  this, out int v2));
			imageList.Images.Add(Libosdev.GetIcon("File",        this, out int v3));
			imageList.Images.Add(Libosdev.GetIcon("BinaryFile",  this, out int v4));
			imageList.Images.Add(Libosdev.GetIcon("TextFile",    this, out int v5));

			_logger.Info($"GetIcon HResults = REF:{vREF}, EXP:{vEXP}, COL:{vCOL}, 0:{v0}, 1:{v1}, 2:{v2}, 3:{v3}, 4:{v4}, 5:{v5}");

			_logger.Info("Explorer control was initialized");
			_logger.Trace("Finished OnLoad event of Explorer");
		}

		private const int IconFolder      = 0;
		private const int IconFolderClose = 1;
		private const int IconFolderOpen  = 2;
		private const int IconFile        = 3;
		private const int IconBinFile     = 4;
		private const int IconTxtFile     = 5;

		/// <summary>
		///  <see cref="OSDeveloper.Core.GraphicalUIs.Explorer.DirectoryChanged"/>イベントを発生させます。
		/// </summary>
		protected virtual void OnDirectoryChanged(EventArgs e)
		{
			_logger.Trace("The OnDirectoryChanged event of Explorer was called");

			treeView.Nodes.Clear();
			if (_dir != null) {
				var root = new FileTreeNode($"{Path.GetFileNameWithoutExtension(_dir.FilePath)} ({_dir.FilePath})", _dir);
				root.Expand();
				treeView.Nodes.Add(root);
				this.SetDirectoryTo(root, _dir);
				this.SetIconEx(root);
			}

			this.DirectoryChanged?.Invoke(this, e);
			_logger.Trace("Finished OnDirectoryChanged event of Explorer");
		}

		private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
		{
			_logger.Trace("The OnAfterSelect event of Explorer was called");

			_logger.Trace("Finished OnAfterSelect event of Explorer");
		}

		private void treeView_AfterExpand(object sender, TreeViewEventArgs e)
		{
			_logger.Trace("The OnAfterExpand event of Explorer was called");

			this.SetIcon(e.Node as FileTreeNode);

			_logger.Trace("Finished OnAfterExpand event of Explorer");
		}

		private void treeView_AfterCollapse(object sender, TreeViewEventArgs e)
		{
			_logger.Trace("The OnAfterCollapse event of Explorer was called");

			this.SetIcon(e.Node as FileTreeNode);

			_logger.Trace("Finished OnAfterCollapse event of Explorer");
		}

		private void treeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
		{
			_logger.Trace("The OnAfterLabelEdit event of Explorer was called");

			if (e.Node is FileTreeNode node) {
				try {
					if (e.Label != null) {
						node.File.Rename(e.Label);
						_dir.Reload();
					}
				} catch (Exception error) {
					_logger.Exception(error);
					MessageBox.Show(this, error.Message, ExplorerTexts.CannotRename, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					e.CancelEdit = true;
				}
			}

			_logger.Trace("Finished OnAfterLabelEdit event of Explorer");
		}

		private void treeView_AfterCheck(object sender, TreeViewEventArgs e)
		{
			_logger.Trace("The OnAfterCheck event of Explorer was called");

			_logger.Trace("Finished OnAfterCheck event of Explorer");
		}

		private void treeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			_logger.Trace("The OnNodeMouseDoubleClick event of Explorer was called");

			if (e.Node is FileTreeNode node) {
				this.OpenEditor(node);
			}

			_logger.Trace("Finished OnNodeMouseDoubleClick event of Explorer");
		}

		private void tolbtnRefresh_Click(object sender, EventArgs e)
		{
			_logger.Trace("The OnClick event of Refresh button in Explorer was called");

			_dir.Reload();
			this.OnDirectoryChanged(new EventArgs());

			_logger.Trace("Finished OnClick event of Refresh button in Explorer");
		}

		private void tolbtnExpand_Click(object sender, EventArgs e)
		{
			_logger.Trace("The OnClick event of Expand button in Explorer was called");

			treeView.ExpandAll();

			_logger.Trace("Finished OnClick event of Expand button in Explorer");
		}

		private void tolbtnCollapse_Click(object sender, EventArgs e)
		{
			_logger.Trace("The OnClick event of Collapse button in Explorer was called");

			treeView.CollapseAll();

			_logger.Trace("Finished OnClick event of Collapse button in Explorer");
		}

		private void popup_openeditor_Click(object sender, EventArgs e)
		{
			_logger.Trace("The OnClick event of OpenEditor popup-menu in Explorer was called");

			if (treeView.SelectedNode is FileTreeNode node) {
				this.OpenEditor(node);
			}

			_logger.Trace("Finished OnClick event of OpenEditor popup-menu in Explorer");
		}

		private void popup_rename_Click(object sender, EventArgs e)
		{
			_logger.Trace("The OnClick event of Rename popup-menu in Explorer was called");

			if (treeView.SelectedNode is FileTreeNode node) {
				node.BeginEdit();
			}

			_logger.Trace("Finished OnClick event of Rename popup-menu in Explorer");
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
				case FileFormat.BinaryFile:
					node.ImageIndex = IconBinFile;
					node.SelectedImageIndex = IconBinFile;
					break;
				case FileFormat.TextFile:
					node.ImageIndex = IconTxtFile;
					node.SelectedImageIndex = IconTxtFile;
					break;
				default:
					node.ImageIndex = IconFile;
					node.SelectedImageIndex = IconFile;
					break;
			}
		}

		private void OpenEditor(FileTreeNode ftn)
		{
			if (ftn.IsNotDir()) {
				EditorWindow editor = new EditorWindow(_mwndbase);
				editor.TargetFile = ftn.File;
				editor.Show();
			}
		}

		private class FileTreeNode : TreeNode
		{
			internal FileMetadata File { get; }

			internal FileTreeNode(string text, FileMetadata file) : base(text)
			{
				this.File = file;
			}

			internal bool IsNotDir()
			{
				return this.File.Format != FileFormat.Directory;
			}
		}
	}
}
