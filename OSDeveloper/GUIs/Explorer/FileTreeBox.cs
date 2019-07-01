using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using OSDeveloper.GUIs.Features;
using OSDeveloper.IO;
using OSDeveloper.IO.ItemManagement;
using OSDeveloper.IO.Logging;
using OSDeveloper.Native;
using OSDeveloper.Projects;
using OSDeveloper.Resources;
using TakymLib.IO;

namespace OSDeveloper.GUIs.Explorer
{
	public partial class FileTreeBox : UserControl
	{
		#region プロパティ
		internal const    string                       _sys32 = "C:\\WINDOWS\\System32\\";
		internal const    string                       _psver = "WindowsPowerShell\\v1.0";
		private  readonly Logger                       _logger;
		private  readonly FormMain                     _mwnd;
		private           FileTreeNode                 _wksp_root;
		private           bool                         _selected_root;
		private  readonly List<SolutionTreeNode>       _solutions;
		private           FolderMetadata               _dir;
		public            FolderMetadata               Directory { get => _dir; set => this.SetFolder(value); }
		public   event    DirectoryChangedEventHandler DirectoryChanged;
		#endregion

		#region コンストラクタ

		public FileTreeBox(FormMain mwnd)
		{
			_logger = Logger.Get(nameof(FileTreeBox));
			_mwnd   = mwnd;
			this.InitializeComponent();
			this.SuspendLayout();

			#region リソース設定
			btnRefresh.Image        = Libosdev.GetIcon(Libosdev.Icons.MiscRefresh,  out uint vRef).ToBitmap();
			btnRefresh.Text         = ExplorerTexts.BtnRefresh;
			btnRefresh.ToolTipText  = ExplorerTexts.BtnRefresh;
			btnExpand.Image         = Libosdev.GetIcon(Libosdev.Icons.MiscExpand,   out uint vExp).ToBitmap();
			btnExpand.Text          = ExplorerTexts.BtnExpand;
			btnExpand.ToolTipText   = ExplorerTexts.BtnExpand;
			btnCollapse.Image       = Libosdev.GetIcon(Libosdev.Icons.MiscCollapse, out uint vCol).ToBitmap();
			btnCollapse.Text        = ExplorerTexts.BtnCollapse;
			btnCollapse.ToolTipText = ExplorerTexts.BtnCollapse;
			openMenu.Text           = ExplorerTexts.PopupMenu_Open;
			openMenu.Font           = new Font(openMenu.Font, FontStyle.Bold);
			openInMenu.Text         = ExplorerTexts.PopupMenu_OpenIn;
			defaultAppMenu.Text     = ExplorerTexts.PopupMenu_OpenIn_DefaultApp;
			defaultAppMenu.Image    = Shell32.GetSmallImageAt(11, true);
			explorerMenu.Text       = ExplorerTexts.PopupMenu_OpenIn_Explorer;
			explorerMenu.Image      = Shell32.GetIconFrom($"{_sys32}explorer.exe",             0, false).ToBitmap();
			cmdMenu.Text            = ExplorerTexts.PopupMenu_OpenIn_Cmd;
			cmdMenu.Image           = Shell32.GetIconFrom($"{_sys32}cmd.exe",                  0, false).ToBitmap();
			powershellMenu.Text     = ExplorerTexts.PopupMenu_OpenIn_PowerShell;
			powershellMenu.Image    = Shell32.GetIconFrom($"{_sys32}{_psver}\\powershell.exe", 0, false).ToBitmap();
			createFileMenu.Text     = ExplorerTexts.PopupMenu_CreateFile;
			createDirMenu.Text      = ExplorerTexts.PopupMenu_CreateDir;
			additemMenu.Text        = ExplorerTexts.PopupMenu_Additem;
			generateNewMenu.Text    = ExplorerTexts.PopupMenu_Additem_GenerateNew;
			fromSystemMenu.Text     = ExplorerTexts.PopupMenu_Additem_FromSystem;
			cloneMenu.Text          = ExplorerTexts.PopupMenu_Clone;
			copyMenu.Text           = ExplorerTexts.PopupMenu_Copy;
			cutMenu.Text            = ExplorerTexts.PopupMenu_Cut;
			pasteMenu.Text          = ExplorerTexts.PopupMenu_Paste;
			removeMenu.Text         = ExplorerTexts.PopupMenu_Remove;
			deleteMenu.Text         = ExplorerTexts.PopupMenu_Delete;
			renameMenu.Text         = ExplorerTexts.PopupMenu_Rename;
			propertyMenu.Text       = ExplorerTexts.PopupMenu_Property;
			#endregion

			iconList.Images.AddRange(IconList.CreateImageArray());
			ofd.Filter = FileTypeRegistry.CreateFullSPFs();
			_solutions = new List<SolutionTreeNode>();
			this.ResumeLayout(false);
			this.PerformLayout();

			_logger.Trace($"constructed {nameof(FileTreeBox)}");
		}

		#endregion

		#region 独自イベント

		protected virtual async void OnDirectoryChanged(DirectoryChangedEventArgs e)
		{
			_logger.Trace($"executing {nameof(OnDirectoryChanged)}...");
			_logger.Info($"started to load the dir: {this.Directory.Path}");

			// 削除済みのアイテムをリストから削除する。
			await Task.Run(() => ItemList.ClearRemovedItems());

			// ファイルまたはディレクトリを読み込んでFileTreeNode生成する。
			var node = await Task.Run(() => {
				if (this.Directory.IsRemoved){ // 削除されているディレクトリが参照された場合
					return new RemovedTreeNode(this.Directory);
				} else {
					return this.CreateTreeNode(this.Directory);
				}
			});

			// ソリューションの読み込み
			await Task.Run(() => {
				if (e.IsRefreshing) {
					this.SaveSolutions();
				}
				this.LoadSolutions();
			});

			// 既に追加されているFileTreeNodeを削除する。
			treeView.Nodes.Clear();

			// TreeViewに生成したFileTreeNodeを追加する。
			treeView.Nodes.Add(node);
			treeView.Nodes.AddRange(_solutions.ToArray());

			// 一番上のノード(Root Node)の設定を変更する。
			node.Text  = $"{this.Directory.Path.GetFileNameWithoutExtension()} ({this.Directory.Path})";
			_wksp_root = node;

			// コントロール全体を更新
			this.Update();

			_logger.Info($"finished to load the dir: {this.Directory.Path}");

			// null合体演算子 (?) は使わない。(非同期処理の為)
			var h = this.DirectoryChanged;
			if (h != null) {
				h.Invoke(this, e);
			}
			_logger.Trace($"completed {nameof(OnDirectoryChanged)}");
		}

		#endregion

		#region 便利関数

		#region 公開関数

		public void OpenEditor()
		{
			if (treeView.SelectedNode is FileTreeNode ftn) {
				if (ftn.Editor == null || ftn.Editor.IsDisposed) {
					ftn.Editor = ftn.Metadata.ExtendedDetail.CreateEditor(_mwnd);
				}
				if (ftn.Editor is IFileLoadFeature flf && !flf.Loaded) {
					flf.Reload();
				}
				if (ftn.Editor != null) {
					ftn.Editor.WindowState = FormWindowState.Normal;
					ftn.Editor.Show();
					ftn.Editor.Focus();
				}
			}
		}

		public void OpenProperty()
		{
			if (treeView.SelectedNode is FileTreeNode ftn) {
				if (ftn.Property == null || ftn.Property.IsDisposed) {
					ftn.Property = ftn.Metadata.ExtendedDetail.CreatePropTab();
				}
				_mwnd.OpenTab(ftn.Property);
			}
		}

		public void SetFolder(FolderMetadata folder)
		{
			var old = _dir;
			_dir = folder;
			this.OnDirectoryChanged(new DirectoryChangedEventArgs(old, folder, false));
		}

		#endregion

		#region 内部関数

		internal FileTreeNode CreateTreeNode(ItemMetadata item)
		{
			if (item == null) return DummyTreeNode.Instance;
			if (item.IsRemoved) return new RemovedTreeNode(item);

			var result = new FileTreeNode(item);
			result.ContextMenuStrip = popupMenu;
			if (result.Folder != null && result.Folder.CanAccess && !result.Folder.IsEmpty()) {
				result.Folder.Refresh();
				var fo = result.Folder.GetFolders();
				for (int i = 0; i < fo.Length; ++i) {
					if (fo[i] != null) result.Nodes.Add(this.CreateTreeNode(fo[i]));
				}
				var fi = result.Folder.GetFiles();
				for (int i = 0; i < fi.Length; ++i) {
					if (fi[i] != null) result.Nodes.Add(this.CreateTreeNode(fi[i]));
				}
			}

			_logger.Info($"loaded the dir or file: {item.Path}");
			return result;
		}

		#endregion

		#region 私的関数

		private void SaveSolutions()
		{
			for (int i = 0; i < _solutions.Count; ++i) {
				_logger.Info($"saving solution \"{_solutions[i].Solution.Name}\"...");
				_solutions[i].Save();
			}
		}

		private void LoadSolutions()
		{
			// リスト初期化
			_solutions.Clear();

			// ディレクトリがソリューションならリストに追加
			var dirs = this.Directory.GetFolders();
			for (int i = 0; i < dirs.Length; ++i) {
				if (dirs[i].Format == FolderFormat.Solution) {
retry:
					_logger.Info($"loading solution \"{dirs[i].Name}\"...");
					try {
						var sln = new Solution(dirs[i].Name);
						var stn = new SolutionTreeNode(sln);
						stn.ContextMenuStrip = popupMenu;
						stn.Load();
						stn.LoadItems();
						_solutions.Add(stn);
					} catch (Exception e) {
						// ソリューションファイルに不正がある場合はスキップする。
						_logger.Exception(e);
						var dr = ((DialogResult)(_mwnd.Invoke(new Func<DialogResult>(() => {
							return MessageBox.Show(
								_mwnd,
								e.Message,
								_mwnd.Text,
								MessageBoxButtons.AbortRetryIgnore,
								MessageBoxIcon.Error
							);
						}))));
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

		#endregion

		#endregion

		#region ボタン

		private void btnRefresh_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(btnRefresh_Click)}...");

			this.OnDirectoryChanged(new DirectoryChangedEventArgs(_dir, _dir, true));

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

		#region ツリービュー

		/* ---- Click ---- */

		private void treeView_DoubleClick(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(treeView_DoubleClick)}...");

			this.OpenEditor();
			if (treeView.SelectedNode is FileTreeNode ftn) {
				if (ftn.Folder != null && ftn.IsExpanded) {
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

			if (e.Node == _wksp_root) {
				cloneMenu .Enabled = false;
				cutMenu   .Enabled = false;
				removeMenu.Enabled = false;
				deleteMenu.Enabled = false;
				renameMenu.Enabled = false;
				_selected_root = true;
			} else if (_selected_root) {
				cloneMenu .Enabled = true;
				cutMenu   .Enabled = true;
				removeMenu.Enabled = true;
				deleteMenu.Enabled = true;
				renameMenu.Enabled = true;
				_selected_root = false;
			}
			_logger.Info($"the selected node is: {e.Node.FullPath}");

			_logger.Trace($"completed {nameof(treeView_BeforeSelect)}");
		}

		private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
		{
			_logger.Trace($"executing {nameof(treeView_AfterSelect)}...");

			if (e.Node is FileTreeNode ftn) {
				if (ftn.Folder != null && ftn.IsExpanded) {
					openMenu.Text = ExplorerTexts.PopupMenu_Open_DirClose;
				} else {
					openMenu.Text = ExplorerTexts.PopupMenu_Open;
				}
			}

			_logger.Trace($"completed {nameof(treeView_AfterSelect)}");
		}

		/* ---- LabelEdit ---- */

		private void treeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
		{
			_logger.Trace($"executing {nameof(treeView_AfterLabelEdit)}...");

			if (e.Node is FileTreeNode ftn) {
				try {
					if (!string.IsNullOrWhiteSpace(e.Label) && e.Label != ftn.Metadata.Name) {
						ftn.Rename(e.Label);
					}
				} catch (Exception error) {
					_logger.Exception(error);
					MessageBox.Show(
						_mwnd,
						ExplorerTexts.Msgbox_CannotChangeName,
						_mwnd.Text,
						MessageBoxButtons.OK,
						MessageBoxIcon.Error
					);
				}
			}

			_logger.Trace($"completed {nameof(treeView_AfterLabelEdit)}");
		}

		/* ---- Expand/Collapse ---- */

		private void treeView_AfterExpand(object sender, TreeViewEventArgs e)
		{
			_logger.Trace($"executing {nameof(treeView_AfterExpand)}...");

			if (e.Node is FileTreeNode ftn) {
				ftn.SetStyle();
				if (ftn.Folder != null && ftn.IsExpanded) {
					openMenu.Text = ExplorerTexts.PopupMenu_Open_DirClose;
				} else {
					openMenu.Text = ExplorerTexts.PopupMenu_Open;
				}
			}

			_logger.Trace($"completed {nameof(treeView_AfterExpand)}");
		}

		private void treeView_AfterCollapse(object sender, TreeViewEventArgs e)
		{
			_logger.Trace($"executing {nameof(treeView_AfterCollapse)}...");

			if (e.Node is FileTreeNode ftn) {
				ftn.SetStyle();
				if (ftn.Folder != null && ftn.IsExpanded) {
					openMenu.Text = ExplorerTexts.PopupMenu_Open_DirClose;
				} else {
					openMenu.Text = ExplorerTexts.PopupMenu_Open;
				}
			}

			_logger.Trace($"completed {nameof(treeView_AfterCollapse)}");
		}

		#endregion

		#region コンテキストメニュー

		#region 開く系メニュー

		private void openMenu_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(openMenu_Click)}...");

			this.OpenEditor();
			if (treeView.SelectedNode is FileTreeNode ftn) {
				if (ftn.Folder != null) {
					if (ftn.IsExpanded) {
						ftn.Collapse();
						openMenu.Text = ExplorerTexts.PopupMenu_Open;
					} else {
						ftn.Expand();
						openMenu.Text = ExplorerTexts.PopupMenu_Open_DirClose;
					}
				}
			}

			_logger.Trace($"completed {nameof(openMenu_Click)}");
		}

		private void defaultAppMenu_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(defaultAppMenu_Click)}...");

			if (treeView.SelectedNode is FileTreeNode ftn) {
				try {
					Process.Start(ftn.Metadata.Path);
				} catch (Win32Exception error) when (error.ErrorCode == unchecked((int)(0x80004005))) {
					_logger.Exception(error);
					MessageBox.Show(_mwnd,
						string.Format(ExplorerTexts.Msgbox_CannotOpenIn_DefaultApp, ftn.Metadata.Path),
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

			if (treeView.SelectedNode is FileTreeNode ftn) {
				if (ftn.File == null) { // ファイルでないなら (フォルダなら)
					Process.Start("C:\\WINDOWS\\explorer.exe", $"\"{ftn.Metadata.Path}\"");
				} else { // ファイルなら (フォルダでないなら)
					Process.Start("C:\\WINDOWS\\explorer.exe", $"\"{ftn.File.Parent.Path}\"");
				}
			}

			_logger.Trace($"completed {nameof(explorerMenu_Click)}");
		}

		private void cmdMenu_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(cmdMenu_Click)}...");

			if (treeView.SelectedNode is FileTreeNode ftn) {
				if (ftn.File == null) { // ファイルでないなら (フォルダなら)
					Process.Start($"{_sys32}cmd.exe", $"/K cd /D \"{ftn.Metadata.Path}\"");
				} else { // ファイルなら (フォルダでないなら)
					Process.Start($"{_sys32}cmd.exe", $"/K cd /D \"{ftn.File.Parent.Path}\"");
				}
			}

			_logger.Trace($"completed {nameof(cmdMenu_Click)}");
		}

		private void powershellMenu_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(powershellMenu_Click)}...");

			if (treeView.SelectedNode is FileTreeNode ftn) {
				if (ftn.File == null) { // ファイルでないなら (フォルダなら)
					Process.Start(
						$"{_sys32}{_psver}\\powershell.exe",
						$"-NoExit -Command Set-Location -Path \"{ftn.Metadata.Path}\"");
				} else { // ファイルなら (フォルダでないなら)
					Process.Start(
						$"{_sys32}{_psver}\\powershell.exe",
						$"-NoExit -Command Set-Location -Path \"{ftn.File.Parent.Path}\"");
				}
			}

			_logger.Trace($"completed {nameof(powershellMenu_Click)}");
		}

		#endregion

		#region 作る系メニュー

		private void createMenu_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(createMenu_Click)}...");

			if (sender is ToolStripMenuItem tsmi) {
				_logger.Notice($"clicked menu is: {tsmi.Name}");
				if (treeView.SelectedNode is FileTreeNode ftn) {
					try {
						FileTreeNode newnode;
						if (tsmi.Name == "createFileMenu") {
							newnode = ftn.CreateFile("New File.txt");
						} else {
							newnode = ftn.CreateDir("New Folder");
						}
						newnode.Parent.Expand();
						newnode.BeginEdit();
					} catch (Exception error) {
						_logger.Exception(error);
						MessageBox.Show(
							_mwnd,
							ExplorerTexts.Msgbox_CannotCreate,
							_mwnd.Text,
							MessageBoxButtons.OK,
							MessageBoxIcon.Error
						);
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

			if (sender is ToolStripMenuItem tsmi) {
				_logger.Notice($"clicked menu is: {tsmi.Name}");
				if (treeView.SelectedNode is FileTreeNode ftn) {
					try {
						if (ofd.ShowDialog() == DialogResult.OK) {
							var meta    = ItemList.GetItem((PathString)(ofd.FileName));
							var newnode = ftn.AddItem(meta);
							newnode.Parent.Expand();
							newnode.BeginEdit();
						}
					} catch (Exception error) {
						_logger.Exception(error);
						MessageBox.Show(
							_mwnd,
							ExplorerTexts.Msgbox_CannotCreate,
							_mwnd.Text,
							MessageBoxButtons.OK,
							MessageBoxIcon.Error
						);
					}
				}
			}

			_logger.Trace($"completed {nameof(fromSystemMenu_Click)}");
		}

		#endregion

		#region 操作系メニュー

		private void cloneMenu_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(cloneMenu_Click)}...");

			if (treeView.SelectedNode is FileTreeNode ftn &&
				ftn.Parent            is FileTreeNode parent) {
				try {
					var newnode = parent.AddItem(ftn.Metadata);
					newnode.BeginEdit();
				} catch (Exception error) {
					_logger.Exception(error);
					MessageBox.Show(
						_mwnd,
						ExplorerTexts.Msgbox_CannotClone,
						_mwnd.Text,
						MessageBoxButtons.OK,
						MessageBoxIcon.Error
					);
				}
			}

			_logger.Trace($"completed {nameof(cloneMenu_Click)}");
		}

		private void copyMenu_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(copyMenu_Click)}...");

			if (treeView.SelectedNode is FileTreeNode ftn) {
				Clipboard.SetText(ftn.Metadata.Path);
			}

			_logger.Trace($"completed {nameof(copyMenu_Click)}");
		}

		private void cutMenu_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(cutMenu_Click)}...");

			MessageBox.Show($"not supported yet.");

			_logger.Trace($"completed {nameof(cutMenu_Click)}");
		}

		private void pasteMenu_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(pasteMenu_Click)}...");

			if (treeView.SelectedNode is FileTreeNode ftn && Clipboard.ContainsText()) {
				try {
					var meta    = ItemList.GetItem((PathString)(Clipboard.GetText()));
					var newnode = ftn.AddItem(meta);
					newnode.Parent.Expand();
					newnode.BeginEdit();
				} catch (Exception error) {
					_logger.Exception(error);
					MessageBox.Show(
						_mwnd,
						ExplorerTexts.Msgbox_CannotPaste,
						_mwnd.Text,
						MessageBoxButtons.OK,
						MessageBoxIcon.Error
					);
				}
			}

			_logger.Trace($"completed {nameof(pasteMenu_Click)}");
		}

		private void removeMenu_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(removeMenu_Click)}...");

			if (treeView.SelectedNode is FileTreeNode ftn && !ftn.TrashItem()) {
				MessageBox.Show(
					_mwnd,
					ExplorerTexts.Msgbox_CannotDelete,
					_mwnd.Text,
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
				);
			}

			_logger.Trace($"completed {nameof(removeMenu_Click)}");
		}

		private void deleteMenu_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(deleteMenu_Click)}...");

			if (treeView.SelectedNode is FileTreeNode ftn) {
				var dr = MessageBox.Show(
					_mwnd,
					string.Format(ExplorerTexts.Msgbox_ConfirmDelete, ftn.Metadata.Path),
					_mwnd.Text,
					MessageBoxButtons.YesNo,
					MessageBoxIcon.Question
				);
				if (dr == DialogResult.Yes && !ftn.Delete()) {
					MessageBox.Show(
						_mwnd,
						ExplorerTexts.Msgbox_CannotDelete,
						_mwnd.Text,
						MessageBoxButtons.OK,
						MessageBoxIcon.Error
					);
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

		#endregion

		#region プロパティ メニュー

		private void propertyMenu_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(propertyMenu_Click)}...");

			this.OpenProperty();

			_logger.Trace($"completed {nameof(propertyMenu_Click)}");
		}

		#endregion

		#endregion
	}
}
