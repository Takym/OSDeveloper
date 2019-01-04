using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
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
		#region 大域変数
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
		#endregion

		#region 初期化
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
			_logger.Trace($"executing {nameof(Explorer_Load)}...");
			_logger.Info("Setting the tool strip bar of Explorer...");
			tolbtnRefresh.Image        = Libosdev.GetIcon("Refresh",  this, out int vREF).ToBitmap();
			tolbtnRefresh.Text         = ExplorerTexts.Refresh;
			tolbtnRefresh.ToolTipText  = ExplorerTexts.Refresh;
			tolbtnExpand.Image         = Libosdev.GetIcon("Expand",   this, out int vEXP).ToBitmap();
			tolbtnExpand.Text          = ExplorerTexts.Expand;
			tolbtnExpand.ToolTipText   = ExplorerTexts.Expand;
			tolbtnCollapse.Image       = Libosdev.GetIcon("Collapse", this, out int vCOL).ToBitmap();
			tolbtnCollapse.Text        = ExplorerTexts.Collapse;
			tolbtnCollapse.ToolTipText = ExplorerTexts.Collapse;
			_logger.Info($"GetIcon HResults = REF:{vREF}, EXP:{vEXP}, COL:{vCOL}");

			_logger.Info("Setting the popup strip bar of Explorer...");
			popup_openeditor.Text = ExplorerTexts.Popup_OpenEditor;
			popup_rename.Text     = ExplorerTexts.Popup_Rename;
			popup_delete.Text     = ExplorerTexts.Popup_Delete;

			_logger.Info("Setting the file icons for Explorer...");
			imageList.Images.Add(Libosdev.GetIcon("Folder",         this, out int v0));
			imageList.Images.Add(Libosdev.GetIcon("FolderClose",    this, out int v1));
			imageList.Images.Add(Libosdev.GetIcon("FolderOpen",     this, out int v2));
			imageList.Images.Add(Libosdev.GetIcon("File",           this, out int v3));
			imageList.Images.Add(Libosdev.GetIcon("BinaryFile",     this, out int v4));
			imageList.Images.Add(Libosdev.GetIcon("TextFile",       this, out int v5));
			imageList.Images.Add(Libosdev.GetIcon("ProgramFile",    this, out int v6));
			imageList.Images.Add(Libosdev.GetIcon("SourceCodeFile", this, out int v7));
			imageList.Images.Add(Libosdev.GetIcon("ResourceFile",   this, out int v8));
			imageList.Images.Add(Libosdev.GetIcon("DocumentFile",   this, out int v9));

			_logger.Info($"GetIcon HResults = 0:{v0}, 1:{v1}, 2:{v2}, 3:{v3}, 4:{v4}, 5:{v5}, 6:{v6}, 7:{v7}, 8:{v8}, 9:{v9}");

			_logger.Info("Explorer control was initialized");
			_logger.Trace($"completed {nameof(Explorer_Load)}");
		}

		private const int IconFolder      = 0;
		private const int IconFolderClose = 1;
		private const int IconFolderOpen  = 2;
		private const int IconFile        = 3;
		private const int IconBinFile     = 4;
		private const int IconTxtFile     = 5;
		private const int IconPrgFile     = 6;
		private const int IconSrcFile     = 7;
		private const int IconResFile     = 8;
		private const int IconDocFile     = 9;

		/// <summary>
		///  <see cref="OSDeveloper.Core.GraphicalUIs.Explorer.DirectoryChanged"/>イベントを発生させます。
		/// </summary>
		protected virtual void OnDirectoryChanged(EventArgs e)
		{
			_logger.Trace($"executing {nameof(OnDirectoryChanged)}...");

			treeView.Nodes.Clear();
			if (_dir != null) {
				var root = new FileTreeNode($"{Path.GetFileNameWithoutExtension(_dir.FilePath)} ({_dir.FilePath})", _dir);
				root.Expand();
				treeView.Nodes.Add(root);
				this.SetDirectoryTo(root, _dir);
				this.SetIconEx(root);
			}

			this.DirectoryChanged?.Invoke(this, e);
			_logger.Trace($"completed {nameof(OnDirectoryChanged)}");
		}
		#endregion

		#region tree view
		private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
		{
			_logger.Trace($"executing {nameof(treeView_AfterSelect)}...");

			_logger.Trace($"completed {nameof(treeView_AfterSelect)}");
		}

		private void treeView_AfterExpand(object sender, TreeViewEventArgs e)
		{
			_logger.Trace($"executing {nameof(treeView_AfterExpand)}...");

			this.SetIcon(e.Node as FileTreeNode);

			_logger.Trace($"completed {nameof(treeView_AfterExpand)}");
		}

		private void treeView_AfterCollapse(object sender, TreeViewEventArgs e)
		{
			_logger.Trace($"executing {nameof(treeView_AfterCollapse)}...");

			this.SetIcon(e.Node as FileTreeNode);

			_logger.Trace($"completed {nameof(treeView_AfterCollapse)}");
		}

		private void treeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
		{
			_logger.Trace($"executing {nameof(treeView_AfterLabelEdit)}...");

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

			_logger.Trace($"completed {nameof(treeView_AfterLabelEdit)}");
		}

		private void treeView_AfterCheck(object sender, TreeViewEventArgs e)
		{
			_logger.Trace($"executing {nameof(treeView_AfterCheck)}...");

			_logger.Trace($"completed {nameof(treeView_AfterCheck)}");
		}

		private void treeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			_logger.Trace($"executing {nameof(treeView_NodeMouseDoubleClick)}...");

			if (e.Node is FileTreeNode node) {
				if (node.IsNotDir()) {
					this.OpenEditor(node);
				}
			}

			_logger.Trace($"completed {nameof(treeView_NodeMouseDoubleClick)}");
		}
		#endregion

		#region tool button
		private void tolbtnRefresh_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(tolbtnRefresh_Click)}...");

			_dir.Reload();
			this.OnDirectoryChanged(new EventArgs());

			_logger.Trace($"completed {nameof(tolbtnRefresh_Click)}");
		}

		private void tolbtnExpand_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(tolbtnExpand_Click)}...");

			treeView.ExpandAll();

			_logger.Trace($"completed {nameof(tolbtnExpand_Click)}");
		}

		private void tolbtnCollapse_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(tolbtnCollapse_Click)}...");

			treeView.CollapseAll();

			_logger.Trace($"completed {nameof(tolbtnCollapse_Click)}");
		}
		#endregion

		#region context menu
		private void popup_openeditor_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(popup_openeditor_Click)}...");

			if (treeView.SelectedNode is FileTreeNode node) {
				this.OpenEditor(node);
			}

			_logger.Trace($"completed {nameof(popup_openeditor_Click)}");
		}

		private void popup_rename_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(popup_rename_Click)}...");

			if (treeView.SelectedNode is FileTreeNode node) {
				node.BeginEdit();
			}

			_logger.Trace($"completed {nameof(popup_rename_Click)}");
		}

		private void popup_delete_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(popup_rename_Click)}...");

			if (treeView.SelectedNode is FileTreeNode node) {
				if (node.ParentDir != null && node.ParentDir.Dir != null) {
					if (node.IsNotDir()) {
						node.ParentDir.Dir.RemoveFile(node.File.Name);
					} else {
						node.ParentDir.Dir.RemoveDirectory(node.File.Name);
					}
					node.Remove();
				}
			}

			_logger.Trace($"completed {nameof(popup_rename_Click)}");
		}
		#endregion

		#region 便利関数
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
				case FileFormat.Program:
					node.ImageIndex = IconPrgFile;
					node.SelectedImageIndex = IconPrgFile;
					break;
				case FileFormat.SourceCode:
					node.ImageIndex = IconSrcFile;
					node.SelectedImageIndex = IconSrcFile;
					break;
				case FileFormat.Resource:
					node.ImageIndex = IconResFile;
					node.SelectedImageIndex = IconResFile;
					break;
				case FileFormat.Document:
					node.ImageIndex = IconDocFile;
					node.SelectedImageIndex = IconDocFile;
					break;
				default:
					node.ImageIndex = IconFile;
					node.SelectedImageIndex = IconFile;
					break;
			}
		}

		private void OpenEditor(FileTreeNode ftn)
		{
			var editor = ftn.File.CreateEditor(_mwndbase);
			if (editor == null) {
				MessageBox.Show(this, ExplorerTexts.CannotViewFile, _mwndbase.Text);
			} else {
				editor.Show();
			}
		}

		private class FileTreeNode : TreeNode
		{
			internal FileTreeNode ParentDir
			{
				get
				{
					return this.Parent as FileTreeNode;
				}
			}
			internal FileMetadata File { get; }
			internal DirMetadata Dir
			{
				get
				{
					return this.File as DirMetadata;
				}
			}

			internal FileTreeNode(string text, FileMetadata file) : base(text)
			{
				this.File = file;
			}

			internal bool IsNotDir()
			{
				return this.File.Format != FileFormat.Directory;
			}
		}
		#endregion
	}
}
