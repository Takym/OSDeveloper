using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using OSDeveloper.IO;
using OSDeveloper.IO.ItemManagement;
using OSDeveloper.IO.Logging;
using OSDeveloper.Native;
using OSDeveloper.Resources;

namespace OSDeveloper.GUIs.Explorer
{
	public partial class FileTreeBox : UserControl
	{
		#region プロパティ

		private const    string                    _sys32 = "%SystemRoot%\\System32\\";
		private const    string                    _psver = "WindowsPowerShell\\v1.0";
		private readonly Logger                    _logger;
		private readonly FormMain                  _mwnd;
		private readonly List<SolutionTreeNodeOld> _solutions;

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
			_solutions = new List<SolutionTreeNodeOld>();
			this.ResumeLayout(false);
			this.PerformLayout();

			_logger.Trace($"constructed {nameof(FileTreeBox)}");
		}

		#endregion

		#region 独自イベント

		protected virtual void OnDirectoryChanged(EventArgs e)
		{
			_logger.Trace($"executing {nameof(OnDirectoryChanged)}...");

			// TODO: ここにコードを挿入。

			// null合体演算子 (?) は使わない。(非同期処理の為)
			var h = this.DirectoryChanged;
			if (h != null) {
				h.Invoke(this, e);
			}
			_logger.Trace($"completed {nameof(OnDirectoryChanged)}");
		}

		#endregion
	}
}
