using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using OSDeveloper.IO;
using OSDeveloper.IO.ItemManagement;
using OSDeveloper.IO.Logging;
using OSDeveloper.Native;
using OSDeveloper.Projects;
using OSDeveloper.Resources;

namespace OSDeveloper.GUIs.Explorer
{
	public partial class FileTreeBox : UserControl
	{
		#region プロパティ
		private const    string                       _sys32 = "%SystemRoot%\\System32\\";
		private const    string                       _psver = "WindowsPowerShell\\v1.0";
		private readonly Logger                       _logger;
		private readonly FormMain                     _mwnd;
		private          FileTreeNode                 _wksp_root;
		private readonly List<SolutionTreeNode>       _solutions;
		private          FolderMetadata               _dir;
		public           FolderMetadata               Directory { get => _dir; set => this.SetFolder(value); }
		public  event    DirectoryChangedEventHandler DirectoryChanged;
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
				this.SaveSolutions();
				this.ReloadSolutions();
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

		public void SetFolder(FolderMetadata folder)
		{
			var old = _dir;
			_dir = folder;
			this.OnDirectoryChanged(new DirectoryChangedEventArgs(old, folder, false));
		}

		#endregion

		#region 私的関数

		private FileTreeNode CreateTreeNode(ItemMetadata item)
		{
			if (item == null)   return DummyTreeNode.Instance;
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
				if (dirs[i].Format == FolderFormat.Solution) {
retry:
					_logger.Info($"reloading solution \"{dirs[i].Name}\"...");
					try {
						var sln = new Solution(dirs[i].Name);
						var stn = new SolutionTreeNode(sln);
						stn.Load();
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

		#endregion

		#endregion

		#region ボタン

		private void btnRefresh_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(btnRefresh_Click)}...");

			this.OnDirectoryChanged(new DirectoryChangedEventArgs(_dir, _dir, false));

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

		private void treeView_AfterCollapse(object sender, TreeViewEventArgs e)
		{
			_logger.Trace($"executing {nameof(treeView_AfterCollapse)}...");

			_logger.Trace($"completed {nameof(treeView_AfterCollapse)}");
		}

		private void treeView_AfterExpand(object sender, TreeViewEventArgs e)
		{
			_logger.Trace($"executing {nameof(treeView_AfterExpand)}...");

			_logger.Trace($"completed {nameof(treeView_AfterExpand)}");
		}

		private void treeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
		{
			_logger.Trace($"executing {nameof(treeView_AfterLabelEdit)}...");

			_logger.Trace($"completed {nameof(treeView_AfterLabelEdit)}");
		}

		private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
		{
			_logger.Trace($"executing {nameof(treeView_AfterSelect)}...");

			_logger.Trace($"completed {nameof(treeView_AfterSelect)}");
		}

		#endregion

		#region コンテキストメニュー

		#region 開く系メニュー

		private void openMenu_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(openMenu_Click)}...");

			_logger.Trace($"completed {nameof(openMenu_Click)}");
		}

		private void defaultAppMenu_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(defaultAppMenu_Click)}...");

			_logger.Trace($"completed {nameof(defaultAppMenu_Click)}");
		}

		private void explorerMenu_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(explorerMenu_Click)}...");

			_logger.Trace($"completed {nameof(explorerMenu_Click)}");
		}

		private void cmdMenu_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(cmdMenu_Click)}...");

			_logger.Trace($"completed {nameof(cmdMenu_Click)}");
		}

		private void powershellMenu_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(powershellMenu_Click)}...");

			_logger.Trace($"completed {nameof(powershellMenu_Click)}");
		}


		#endregion

		#region 作る系メニュー

		private void createMenu_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(createMenu_Click)}...");

			if (sender is ToolStripMenuItem tsmi) {
				_logger.Notice($"clicked menu is: {tsmi.Name}");
			}

			_logger.Trace($"completed {nameof(createMenu_Click)}");
		}

		private void generateNewMenu_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(generateNewMenu_Click)}...");

			_logger.Trace($"completed {nameof(generateNewMenu_Click)}");
		}

		private void fromSystemMenu_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(fromSystemMenu_Click)}...");

			_logger.Trace($"completed {nameof(fromSystemMenu_Click)}");
		}

		#endregion

		#region 操作系メニュー

		private void cloneMenu_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(cloneMenu_Click)}...");

			_logger.Trace($"completed {nameof(cloneMenu_Click)}");
		}

		private void copyMenu_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(copyMenu_Click)}...");

			_logger.Trace($"completed {nameof(copyMenu_Click)}");
		}

		private void cutMenu_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(cutMenu_Click)}...");

			_logger.Trace($"completed {nameof(cutMenu_Click)}");
		}

		private void pasteMenu_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(pasteMenu_Click)}...");

			_logger.Trace($"completed {nameof(pasteMenu_Click)}");
		}

		private void removeMenu_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(removeMenu_Click)}...");

			_logger.Trace($"completed {nameof(removeMenu_Click)}");
		}

		private void deleteMenu_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(deleteMenu_Click)}...");

			_logger.Trace($"completed {nameof(deleteMenu_Click)}");
		}

		private void renameMenu_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(renameMenu_Click)}...");

			_logger.Trace($"completed {nameof(renameMenu_Click)}");
		}

		#endregion

		#region プロパティ メニュー

		private void propertyMenu_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(propertyMenu_Click)}...");

			_logger.Trace($"completed {nameof(propertyMenu_Click)}");
		}

		#endregion

		#endregion
	}
}
