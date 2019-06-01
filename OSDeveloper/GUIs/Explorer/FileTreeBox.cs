using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using OSDeveloper.GUIs.Editors;
using OSDeveloper.GUIs.Features;
using OSDeveloper.GUIs.Terminal;
using OSDeveloper.IO;
using OSDeveloper.IO.Configuration;
using OSDeveloper.IO.ItemManagement;
using OSDeveloper.IO.Logging;
using OSDeveloper.Native;
using OSDeveloper.Projects;
using OSDeveloper.Resources;
using TakymLib.IO;
using Folder = System.IO.Directory;

namespace OSDeveloper.GUIs.Explorer
{
	// TODO: 切り取りメニュー、Bash(WSL)メニュー、新しい項目を生成メニューの処理の実装

	public partial class FileTreeBox : UserControl
	{
		#region プロパティ

		private const    string                 _psver = "WindowsPowerShell\\v1.0";
		private readonly Logger                 _logger;
		private readonly FormMain               _mwnd;
		private          FileTreeNode           _root;
		private          bool                   _selected_root;
		private          bool                   _use_wsl_exe;
		private readonly List<SolutionTreeNode> _solutions;

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

		public FileTreeBox(FormMain mwnd)
		{
			_logger = Logger.Get(nameof(FileTreeBox));
			_mwnd   = mwnd;
			this.InitializeComponent();

			this.SuspendLayout();
			_use_wsl_exe      = SettingManager.System.UseWSLCommand;
			btnRefresh.Image  = Libosdev.GetIcon(Libosdev.Icons.MiscRefresh,  out uint vRef).ToBitmap();
			btnRefresh.Text   = ExplorerTexts.BtnRefresh;
			btnExpand.Image   = Libosdev.GetIcon(Libosdev.Icons.MiscExpand,   out uint vExp).ToBitmap();
			btnExpand.Text    = ExplorerTexts.BtnExpand;
			btnCollapse.Image = Libosdev.GetIcon(Libosdev.Icons.MiscCollapse, out uint vCol).ToBitmap();
			btnCollapse.Text  = ExplorerTexts.BtnCollapse;

			openMenu.Text            = ExplorerTexts.PopupMenu_Open;
			openMenu.Font            = new Font(openMenu.Font, FontStyle.Bold);
			openInMenu.Text          = ExplorerTexts.PopupMenu_OpenIn;
			{
				defaultAppMenu.Text  = ExplorerTexts.PopupMenu_OpenIn_DefaultApp;
				defaultAppMenu.Image = Shell32.GetSmallImageAt(11, true);
				explorerMenu.Text    = ExplorerTexts.PopupMenu_OpenIn_Explorer;
				explorerMenu.Image   = Shell32.GetIconFrom("%SystemRoot%\\explorer.exe", 0, false).ToBitmap();
				cmdMenu.Text         = ExplorerTexts.PopupMenu_OpenIn_Cmd;
				cmdMenu.Image        = Shell32.GetIconFrom("%SystemRoot%\\System32\\cmd.exe", 0, false).ToBitmap();
				powershellMenu.Text  = ExplorerTexts.PopupMenu_OpenIn_PowerShell;
				powershellMenu.Image = Shell32.GetIconFrom($"%SystemRoot%\\System32\\{_psver}\\powershell.exe", 0, false).ToBitmap();
				bashMenu.Text        = _use_wsl_exe ? ExplorerTexts.PopupMenu_OpenIn_BashWsl : ExplorerTexts.PopupMenu_OpenIn_Bash;
				bashMenu.Image       = Shell32.GetSmallImageAt(2, false);
			}
			createFileMenu.Text      = ExplorerTexts.PopupMenu_CreateFile;
			createDirMenu.Text       = ExplorerTexts.PopupMenu_CreateDir;
			additemMenu.Text         = ExplorerTexts.PopupMenu_Additem;
			{
				generateNewMenu.Text = ExplorerTexts.PopupMenu_Additem_GenerateNew;
				fromSystemMenu.Text  = ExplorerTexts.PopupMenu_Additem_FromSystem;
			}
			cloneMenu.Text           = ExplorerTexts.PopupMenu_Clone;
			copyMenu.Text            = ExplorerTexts.PopupMenu_Copy;
			cutMenu.Text             = ExplorerTexts.PopupMenu_Cut;
			pasteMenu.Text           = ExplorerTexts.PopupMenu_Paste;
			removeMenu.Text          = ExplorerTexts.PopupMenu_Remove;
			deleteMenu.Text          = ExplorerTexts.PopupMenu_Delete;
			renameMenu.Text          = ExplorerTexts.PopupMenu_Rename;
			propertyMenu.Text        = ExplorerTexts.PopupMenu_Property;

			iconList.Images.AddRange(IconList.CreateImageArray());
			ofd.Filter = FileTypeRegistry.CreateFullSPFs();
			_solutions = new List<SolutionTreeNode>();
			this.ResumeLayout();

			_logger.Trace($"constructed {nameof(FileTreeBox)}");
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

			// 削除済みのアイテムをリストから削除する。
			ItemList.ClearRemovedItems();

			// ファイルまたはディレクトリを読み込んでFileTreeNode生成する。
			var node = await Task.Run(() => {
				if (this.Directory.IsRemoved){ // 削除されているディレクトリが参照された場合
					return new RemovedTreeNode(this.Directory);
				} else {
					return this.CreateTreeNode(this.Directory);
				}
			});

			// ソリューションの保存＆再読み込み
			this.SaveSolutions();
			this.ReloadSolutions();

			// 既に追加されているFileTreeNodeを削除する。
			treeView.Nodes.Clear();

			// TreeViewに生成したFileTreeNodeを追加する。
			treeView.Nodes.Add(node);
			treeView.Nodes.AddRange(_solutions.ToArray());

			// 一番上のノード(Root Node)の設定を変更する。
			if (node is FileTreeNode ftn) {
				ftn.Text = $"{this.Directory.Path.GetFileNameWithoutExtension()} ({this.Directory.Path})";
				//ftn.Expand(); // ツリーを展開しない。
				this.SetStyleToTreeNode(ftn);
				_root = ftn;
			}

			// コントロール全体を更新
			this.Update();

			_logger.Info($"finished to load the dir: {this.Directory.Path}");
			_logger.Trace($"completed {nameof(OnDirectoryChanged)}");
		}

		private TreeNode CreateTreeNode(ItemMetadata item)
		{
			if (item == null)   return DummyTreeNode.Instance;
			if (item.IsRemoved) return new RemovedTreeNode(item);

			var result = new FileTreeNode(item);
			result.ContextMenuStrip = popupMenu;
			if (result.Folder != null && result.Folder.CanAccess && !result.Folder.IsEmpty()) {
				result.Folder.Refresh();
				var fo = result.Folder.GetFolders();
				for (int i = 0; i < fo.Length; ++i) {
					if (fo[i] != null /*&& !fo[i].IsRemoved*/) result.Nodes.Add(this.CreateTreeNode(fo[i]));
				}
				var fi = result.Folder.GetFiles();
				for (int i = 0; i < fi.Length; ++i) {
					if (fi[i] != null /*&& !fi[i].IsRemoved*/) result.Nodes.Add(this.CreateTreeNode(fi[i]));
				}
			}
			this.SetStyleToTreeNode(result);

			_logger.Info($"loaded the dir or file: {item.Path}");
			return result;
		}

		private void SetStyleToTreeNode(FileTreeNode node)
		{
			if (node.Metadata.CanAccess) {
				if (node.File != null) {
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
						case FileFormat.Solution:
							node.ImageIndex         = IconList.SolutionFile;
							node.SelectedImageIndex = IconList.SolutionFile;
							break;
						default:
							node.ImageIndex         = IconList.File;
							node.SelectedImageIndex = IconList.File;
							break;
					}
				} else if (node.Folder != null) {
					switch (node.Folder.Format) {
						case FolderFormat.Directory:
							if (node.Folder.IsEmpty()) {
								node.ImageIndex         = IconList.Directory;
								node.SelectedImageIndex = IconList.Directory;
							} else if (node.IsExpanded) {
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
							} else if (node.IsExpanded) {
								node.ImageIndex         = IconList.JunOpened;
								node.SelectedImageIndex = IconList.JunOpened;
							} else {
								node.ImageIndex         = IconList.JunClosed;
								node.SelectedImageIndex = IconList.JunClosed;
							}
							break;
						case FolderFormat.Solution:
							node.ImageIndex         = IconList.Solution;
							node.SelectedImageIndex = IconList.Solution;
							break;
						default:
							if (node.Folder.IsEmpty()) {
								node.ImageIndex         = IconList.Folder;
								node.SelectedImageIndex = IconList.Folder;
							} else if (node.IsExpanded) {
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

		private void SaveSolutions()
		{
			for (int i = 0; i < _solutions.Count; ++i) {
				_logger.Info($"saving solution \"{_solutions[i].Solution.Name}\"...");
				_solutions[i].Save();
			}
		}

		private void ReloadSolutions()
		{
			// リスト初期化
			_solutions.Clear();

			// ディレクトリがソリューションならリストに追加
			var dirs = this.Directory.GetFolders();
			for (int i = 0; i < dirs.Length; ++i) {
				if (dirs[i].Path.Bond(dirs[i].Name + ".osdev_sln").Exists()) {
retry:
					_logger.Info($"reloading solution \"{dirs[i].Name}\"...");
					try {
						var sln = new Solution(dirs[i].Name);
						var stn = new SolutionTreeNode(sln);
						stn.Refresh(this);
						_solutions.Add(stn);
					} catch (Exception e) {
						// ソリューションファイルに不正がある場合はスキップする。
						_logger.Exception(e);
						var dr = MessageBox.Show(
							_mwnd,
							e.Message,
							_mwnd.Text,
							MessageBoxButtons.AbortRetryIgnore,
							MessageBoxIcon.Error
						);
						if (dr == DialogResult.Abort) {
							return; // 読み込みを停止する。
						} else if (dr == DialogResult.Retry) {
							goto retry; // もう一度試す。
						} else if (dr == DialogResult.Ignore) {
							continue; // 無視して続行する。
						}
					}
				}
			}
		}

		private FolderMetadata GetFolderFromNode(FileTreeNode node, out bool selectedFile)
		{
			if (node.Folder == null) {
				selectedFile = true;
				return node.File.Parent;
			} else {
				selectedFile = false;
				return node.Folder;
			}
		}

		private void AddItemAsTreeNode(ItemMetadata meta, FileTreeNode node, bool selectedFile)
		{
			var newnode = this.CreateTreeNode(meta);
			if (selectedFile) {
				node.Parent.Nodes.Add(newnode);
				node.Parent.Expand();
			} else {
				node.Nodes.Add(newnode);
				node.Expand();
			}
			treeView.Invalidate();
			(node as ProjectItemTreeNode)?.AddItem(meta);
			newnode.BeginEdit();
		}

		#endregion

		#region 共通機能

		public void OpenEditor()
		{
			if (treeView.SelectedNode is FileTreeNode node) {
				if (node.Editor == null || node.Editor.IsDisposed) {
					node.Editor = node.Metadata.ExtendedDetail.CreateEditor(_mwnd);
				}
				if (node.Editor is IFileLoadFeature flf && !flf.Loaded) {
					flf.Reload();
				}
				node.Editor?.Show();
				node.Editor?.Focus();
			}
		}

		internal void UpdatePItemNode(ProjectItemTreeNode node)
		{
			node.ContextMenuStrip = popupMenu;
			this.SetStyleToTreeNode(node);
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

		private void treeView_DoubleClick(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(treeView_DoubleClick)}...");

			this.OpenEditor();
			if (treeView.SelectedNode is FileTreeNode node) {
				if (node.Folder != null && node.IsExpanded) {
					openMenu.Text = ExplorerTexts.PopupMenu_Open_DirClose;
				} else {
					openMenu.Text = ExplorerTexts.PopupMenu_Open;
				}
			}

			_logger.Trace($"completed {nameof(treeView_DoubleClick)}");
		}

		private void treeView_MouseClick(object sender, MouseEventArgs e)
		{
			_logger.Trace($"executing {nameof(treeView_MouseClick)}...");

			if (e.Button.HasFlag(MouseButtons.Right)) {
				treeView.SelectedNode = treeView.GetNodeAt(e.Location);
			}

			_logger.Trace($"completed {nameof(treeView_MouseClick)}");
		}

		/* ---- Select ---- */

		private void treeView_BeforeSelect(object sender, TreeViewCancelEventArgs e)
		{
			_logger.Trace($"executing {nameof(treeView_BeforeSelect)}...");

			if (e.Node is FileTreeNode node) {
				if (node == _root) {
					cloneMenu .Visible = false;
					cloneMenu .Enabled = false;
					//cutMenu .Visible = false;
					//cutMenu .Enabled = false;
					removeMenu.Visible = false;
					removeMenu.Enabled = false;
					deleteMenu.Visible = false;
					deleteMenu.Enabled = false;
					renameMenu.Visible = false;
					renameMenu.Enabled = false;
					_selected_root = true;
				} else if (_selected_root) {
					cloneMenu .Visible = true;
					cloneMenu .Enabled = true;
					//cutMenu .Visible = true;
					//cutMenu .Enabled = true;
					removeMenu.Visible = true;
					removeMenu.Enabled = true;
					deleteMenu.Visible = true;
					deleteMenu.Enabled = true;
					renameMenu.Visible = true;
					renameMenu.Enabled = true;
					_selected_root = false;
				}
				_logger.Info($"the selected node is: {node.FullPath}");
			}

			_logger.Trace($"completed {nameof(treeView_BeforeSelect)}");
		}

		private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
		{
			_logger.Trace($"executing {nameof(treeView_AfterSelect)}...");

			if (e.Node is FileTreeNode node) {
				if (node.Folder != null && node.IsExpanded) {
					openMenu.Text = ExplorerTexts.PopupMenu_Open_DirClose;
				} else {
					openMenu.Text = ExplorerTexts.PopupMenu_Open;
				}
			}

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

		private void openMenu_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(propertyMenu_Click)}...");

			this.OpenEditor();
			if (treeView.SelectedNode is FileTreeNode node) {
				if (node.Folder != null) {
					if (node.IsExpanded) {
						node.Collapse();
						openMenu.Text = ExplorerTexts.PopupMenu_Open;
					} else {
						node.Expand();
						openMenu.Text = ExplorerTexts.PopupMenu_Open_DirClose;
					}
				}
			}

			_logger.Trace($"completed {nameof(propertyMenu_Click)}");
		}

		private void defaultAppMenu_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(defaultAppMenu_Click)}...");

			if (treeView.SelectedNode is FileTreeNode node) {
				try {
					Process.Start(node.Metadata.Path);
				} catch (Win32Exception error) when (error.ErrorCode == unchecked((int)(0x80004005))) {
					_logger.Exception(error);
					MessageBox.Show(_mwnd,
						string.Format(ExplorerTexts.Msgbox_CannotOpenIn_DefaultApp, node.Metadata.Path),
						_mwnd.Text,
						MessageBoxButtons.OK,
						MessageBoxIcon.Warning);
				}
			}

			_logger.Trace($"completed {nameof(defaultAppMenu_Click)}");
		}

		private void explorerMenu_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(explorerMenu_Click)}...");

			if (treeView.SelectedNode is FileTreeNode node) {
				if (node.File == null) { // ファイルでないなら (フォルダなら)
					Process.Start("C:\\WINDOWS\\explorer.exe", $"\"{node.Metadata.Path}\"");
				} else { // ファイルなら (フォルダでないなら)
					Process.Start("C:\\WINDOWS\\explorer.exe", $"\"{node.File.Parent.Path}\"");
				}
			}

			_logger.Trace($"completed {nameof(explorerMenu_Click)}");
		}

		private void cmdMenu_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(cmdMenu_Click)}...");

			if (treeView.SelectedNode is FileTreeNode node) {
				if (node.File == null) { // ファイルでないなら (フォルダなら)
					Process.Start("C:\\WINDOWS\\System32\\cmd.exe", $"/K cd /D \"{node.Metadata.Path}\"");
				} else { // ファイルなら (フォルダでないなら)
					Process.Start(
						"C:\\WINDOWS\\System32\\cmd.exe", $"/K cd /D \"{node.File.Parent.Path}\"");
				}
			}

			_logger.Trace($"completed {nameof(cmdMenu_Click)}");
		}

		private void powershellMenu_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(powershellMenu_Click)}...");

			if (treeView.SelectedNode is FileTreeNode node) {
				if (node.File == null) { // ファイルでないなら (フォルダなら)
					Process.Start(
						$"C:\\WINDOWS\\System32\\{_psver}\\powershell.exe",
						$"-NoExit -Command Set-Location -Path \"{node.Metadata.Path}\"");
				} else { // ファイルなら (フォルダでないなら)
					Process.Start(
						$"C:\\WINDOWS\\System32\\{_psver}\\powershell.exe",
						$"-NoExit -Command Set-Location -Path \"{node.File.Parent.Path}\"");
				}
			}

			_logger.Trace($"completed {nameof(powershellMenu_Click)}");
		}

		private void bashMenu_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(bashMenu_Click)}...");

			MessageBox.Show("not supported yet");
#if false
			if (treeView.SelectedNode is FileTreeNode node) {
				var psi = new ProcessStartInfo();
				psi.FileName = "C:\\WINDOWS\\System32\\cmd.exe";
				psi.UseShellExecute = true;
				if (node.File == null) { // ファイルでないなら (フォルダなら)
					psi.Arguments = $"/C cd /D \"{node.Metadata.Path}\" & call C:\\WINDOWS\\System32\\"
						+ (_use_wsl_exe ? "wsl.exe" : "bash.exe");
				} else { // ファイルなら (フォルダでないなら)
					psi.Arguments = $"/C cd /D \"{node.File.Parent.Path}\" & call C:\\WINDOWS\\System32\\"
						+ (_use_wsl_exe ? "wsl.exe" : "bash.exe");
				}
				Process.Start(psi);
			}
#endif

			_logger.Trace($"completed {nameof(bashMenu_Click)}");
		}

		private void createMenu_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(createMenu_Click)}...");

			string causedBy = (sender as ToolStripMenuItem)?.Name?.Trim();
			if (!string.IsNullOrEmpty(causedBy)) {
				_logger.Debug("caused by:" + (sender as ToolStripMenuItem)?.Name);
				if (treeView.SelectedNode is FileTreeNode node) {
					var dir = this.GetFolderFromNode(node, out bool selectedFile);
					var meta = causedBy == "createFileMenu" ? (ItemMetadata)
						dir.CreateFile("New File.txt")      :
						dir.CreateDir ("New Folder");
					if (meta == null) {
						MessageBox.Show(_mwnd,
							ExplorerTexts.Msgbox_CannotCreate,
							_mwnd.Text,
							MessageBoxButtons.OK,
							MessageBoxIcon.Warning);
					} else {
						this.AddItemAsTreeNode(meta, node, selectedFile);
					}
				}
			}

			_logger.Trace($"completed {nameof(createMenu_Click)}");
		}

		private void generateNewMenu_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(generateNewMenu_Click)}...");

			MessageBox.Show("not supported yet");

			_logger.Trace($"completed {nameof(generateNewMenu_Click)}");
		}

		private void fromSystemMenu_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(fromSystemMenu_Click)}...");

			if (treeView.SelectedNode is FileTreeNode node) {
				var dir = this.GetFolderFromNode(node, out bool selectedFile);
				var dr = ofd.ShowDialog();
				if (dr == DialogResult.OK) {
					var meta = dir.CreateFile(Path.GetFileName(ofd.FileName));
					meta.WriteAllBytes(File.ReadAllBytes(ofd.FileName));
					if (meta == null) {
						MessageBox.Show(_mwnd,
							ExplorerTexts.Msgbox_CannotCreate,
							_mwnd.Text,
							MessageBoxButtons.OK,
							MessageBoxIcon.Warning);
					} else {
						this.AddItemAsTreeNode(meta, node, selectedFile);
					}
				}
			}

			_logger.Trace($"completed {nameof(fromSystemMenu_Click)}");
		}

		private void cloneMenu_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(cloneMenu_Click)}...");

			if (treeView.SelectedNode is FileTreeNode node) {
				var newitem = node.Metadata.Copy(node.Metadata.Path.EnsureName());
				if (newitem != null && node.Metadata.Parent.AddItem(newitem)) {
					var newnode = this.CreateTreeNode(newitem);
					node.Parent.Nodes.Add(newnode);
					node.Parent.Expand();
					(node as ProjectItemTreeNode)?.AddItem(newitem);
					newnode.BeginEdit();
				} else {
					MessageBox.Show(_mwnd,
						ExplorerTexts.Msgbox_CannotClone,
						_mwnd.Text,
						MessageBoxButtons.OK,
						MessageBoxIcon.Warning);
				}
			}

			_logger.Trace($"completed {nameof(cloneMenu_Click)}");
		}

		private void copyMenu_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(copyMenu_Click)}...");

			if (treeView.SelectedNode is FileTreeNode node) {
				Clipboard.SetText(node.Metadata.Path);
			}

			_logger.Trace($"completed {nameof(copyMenu_Click)}");
		}

		private void cutMenu_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(cutMenu_Click)}...");

			MessageBox.Show("not supported yet");

			_logger.Trace($"completed {nameof(cutMenu_Click)}");
		}

		private void pasteMenu_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(pasteMenu_Click)}...");

			if (treeView.SelectedNode is FileTreeNode node && Clipboard.ContainsText()) {
				try {
					ItemMetadata meta = null;
					var dstdir = this.GetFolderFromNode(node, out bool selectedFile);
					var src = new PathString(Clipboard.GetText());
					var dst = dstdir.Path.Bond(src.GetFileName()).EnsureName();
					if (Folder.Exists(src)) {
						var srcdir = ItemList.GetDir(src);
						meta = srcdir.Copy(dst);
					} else if (File.Exists(src)) {
						var srcfile = ItemList.GetFile(src);
						meta = srcfile.Copy(dst);
					}
					meta = meta ?? throw new FileNotFoundException(ExplorerTexts.Msgbox_CannotPaste_DoesNotExist, src);
					this.AddItemAsTreeNode(meta, node, selectedFile);
				} catch (Exception error) {
					_logger.Exception(error);
					MessageBox.Show(_mwnd,
						ExplorerTexts.Msgbox_CannotPaste +
						$"\r\n{error.Message}",
						_mwnd.Text,
						MessageBoxButtons.OK,
						MessageBoxIcon.Warning);
				}
			}

			_logger.Trace($"completed {nameof(pasteMenu_Click)}");
		}

		private void removeMenu_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(removeMenu_Click)}...");

			if (treeView.SelectedNode is FileTreeNode node) {
				if (node.Metadata.TrashItem()) {
					var p = node.Parent;
					node.Remove();
					(node as ProjectItemTreeNode)?.RemoveItem(node.Metadata);
					if (p.Nodes.Count == 0 && p is FileTreeNode parent) {
						this.SetStyleToTreeNode(parent);
					}
				} else {
					MessageBox.Show(_mwnd,
						ExplorerTexts.Msgbox_CannotDelete,
						_mwnd.Text,
						MessageBoxButtons.OK,
						MessageBoxIcon.Warning);
				}
			}

			_logger.Trace($"completed {nameof(removeMenu_Click)}");
		}

		private void deleteMenu_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(deleteMenu_Click)}...");

			if (treeView.SelectedNode is FileTreeNode node) {
				var dr = MessageBox.Show(_mwnd,
					string.Format(ExplorerTexts.Msgbox_ConfirmDelete, node.Metadata.Path),
					_mwnd.Text,
					MessageBoxButtons.YesNo,
					MessageBoxIcon.Question);
				if (dr == DialogResult.Yes) {
					if (node.Metadata.Delete()) {
						var p = node.Parent;
						node.Remove();
						(node as ProjectItemTreeNode)?.RemoveItem(node.Metadata);
						if (p.Nodes.Count == 0 && p is FileTreeNode parent) {
							this.SetStyleToTreeNode(parent);
						}
					} else {
						MessageBox.Show(_mwnd,
							ExplorerTexts.Msgbox_CannotDelete,
							_mwnd.Text,
							MessageBoxButtons.OK,
							MessageBoxIcon.Warning);
					}
				}
			}

			_logger.Trace($"completed {nameof(deleteMenu_Click)}");
		}

		private void renameMenu_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(renameMenu_Click)}...");

			treeView.SelectedNode.BeginEdit();

			_logger.Trace($"completed {nameof(renameMenu_Click)}");
		}

		private void propertyMenu_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(propertyMenu_Click)}...");

			if (treeView.SelectedNode is FileTreeNode node) {
				if (node.Property == null || node.Property.IsDisposed) {
					node.Property = node.Metadata.ExtendedDetail.CreatePropTab();
				}
				_mwnd.OpenTab(node.Property);
			}

			_logger.Trace($"completed {nameof(propertyMenu_Click)}");
		}

		#endregion

		#region FileTreeNode クラス

		public class FileTreeNode : TreeNode
		{
			public ItemMetadata Metadata { get; }
			public ItemProperty Property { get; set; }
			public EditorWindow Editor   { get; set; }

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

		internal sealed class DummyTreeNode : TreeNode
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

		internal class RemovedTreeNode : TreeNode
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
