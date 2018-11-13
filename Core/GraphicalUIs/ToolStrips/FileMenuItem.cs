using System;
using System.Windows.Forms;
using OSDeveloper.Core.Editors;
using OSDeveloper.Core.FileManagement;
using static OSDeveloper.Core.GraphicalUIs.ToolStrips.MenuStripManager;

namespace OSDeveloper.Core.GraphicalUIs.ToolStrips
{
	partial class MenuStripManager { } // デザイナ避け

	/// <summary>
	///  ファイルを管理するメニューを表します。
	/// </summary>
	public class FileMenuItem : MainMenuItem
	{
		private readonly MainWindowBase _mwnd_base;
		private readonly ToolStripMenuItem _newfile, _open, _reload, _save, _saveAs, _saveAll, _print, _printPreview, _exit;
		private readonly SaveFileDialog _sfd;
		private readonly PrintDialog _pd;
		private readonly PrintPreviewDialog _ppd;

		/// <summary>
		///  このメニューの操作対象を指定して、
		///  型'<see cref="OSDeveloper.Core.GraphicalUIs.ToolStrips.FileMenuItem"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		/// <param name="mwndBase">このメニューの親ウィンドウです。</param>
		public FileMenuItem(MainWindowBase mwndBase)
		{
			_mwnd_base = mwndBase;
			_newfile = new ToolStripMenuItem();
			_open = new ToolStripMenuItem();
			_reload = new ToolStripMenuItem();
			_save = new ToolStripMenuItem();
			_saveAs = new ToolStripMenuItem();
			_saveAll = new ToolStripMenuItem();
			_print = new ToolStripMenuItem();
			_printPreview = new ToolStripMenuItem();
			_exit = new ToolStripMenuItem();
			_sfd = new SaveFileDialog();
			_pd = new PrintDialog();
			_ppd = new PrintPreviewDialog();

			_newfile.Text = MenuTexts.File_Newfile;
			_newfile.ShortcutKeys = Keys.Control | Keys.N;
			_newfile.Click += this._newfile_Click;
			_open.Text = MenuTexts.File_Open;
			_open.ShortcutKeys = Keys.Control | Keys.O;
			_open.Click += this._open_Click;
			_reload.Text = MenuTexts.File_Reload;
			_reload.ShortcutKeys = Keys.Control | Keys.R;
			_reload.Click += this._reload_Click;
			_save.Text = MenuTexts.File_Save;
			_save.ShortcutKeys = Keys.Control | Keys.S;
			_save.Click += this._save_Click;
			_saveAs.Text = MenuTexts.File_SaveAs;
			_saveAs.ShortcutKeys = Keys.Control | Keys.Shift | Keys.S;
			_saveAs.Click += this._saveAs_Click;
			_saveAll.Text = MenuTexts.File_SaveAll;
			_saveAll.ShortcutKeys = Keys.Control | Keys.Alt | Keys.S;
			_saveAll.Click += this._saveAll_Click;
			_print.Text = MenuTexts.File_Print;
			_print.ShortcutKeys = Keys.Control | Keys.P;
			_print.Click += this._print_Click;
			_printPreview.Text = MenuTexts.File_PrintPreview;
			_printPreview.ShortcutKeys = Keys.Control | Keys.Shift | Keys.P;
			_printPreview.Click += this._printPreview_Click;
			_exit.Text = MenuTexts.File_Exit;
			_exit.ShortcutKeys = Keys.Control | Keys.Shift | Keys.Alt | Keys.Escape;
			_exit.Click += this._exit_Click;

			this.DropDownItems.Add(_newfile);
			this.DropDownItems.Add(_open);
			this.DropDownItems.Add(_reload);
			this.DropDownItems.Add(new ToolStripSeparator());
			this.DropDownItems.Add(_save);
			this.DropDownItems.Add(_saveAs);
			this.DropDownItems.Add(_saveAll);
			this.DropDownItems.Add(new ToolStripSeparator());
			this.DropDownItems.Add(_print);
			this.DropDownItems.Add(_printPreview);
			this.DropDownItems.Add(new ToolStripSeparator());
			this.DropDownItems.Add(_exit);
		}

		private void _newfile_Click(object sender, EventArgs e)
		{
			Logger.Trace($"{nameof(FileMenuItem)}: _newfile begin");
			_mwnd_base.SetStatusMessage(MainWindowStatusMessage.Unimplemented(_newfile.Text));
			Logger.Trace($"{nameof(FileMenuItem)}: _newfile end");
		}

		private void _open_Click(object sender, EventArgs e)
		{
			Logger.Trace($"{nameof(FileMenuItem)}: _open begin");
			_mwnd_base.SetStatusMessage(MainWindowStatusMessage.Unimplemented(_open.Text));
			Logger.Trace($"{nameof(FileMenuItem)}: _open end");
		}

		private void _reload_Click(object sender, EventArgs e)
		{
			Logger.Trace($"{nameof(FileMenuItem)}: _reload begin");

			var editor = _mwnd_base.GetActiveEditor();
			if (editor is IFileSaveLoadFeature fsl) {
				fsl.Reload();
				_mwnd_base.SetStatusMessage(MainWindowStatusMessage.Ready());
			} else {
				_mwnd_base.SetStatusMessage(MainWindowStatusMessage.Unimplemented(_reload.Text));
			}

			Logger.Trace($"{nameof(FileMenuItem)}: _reload end");
		}

		private void _save_Click(object sender, EventArgs e)
		{
			Logger.Trace($"{nameof(FileMenuItem)}: _save begin");

			var editor = _mwnd_base.GetActiveEditor();
			if (editor is IFileSaveLoadFeature fsl) {
				fsl.Save();
				_mwnd_base.SetStatusMessage(MainWindowStatusMessage.FileSaved(fsl.TargetFile.FilePath));
			} else {
				_mwnd_base.SetStatusMessage(MainWindowStatusMessage.Unimplemented(_save.Text));
			}

			Logger.Trace($"{nameof(FileMenuItem)}: _save end");
		}

		private void _saveAs_Click(object sender, EventArgs e)
		{
			Logger.Trace($"{nameof(FileMenuItem)}: _saveAs begin");

			var editor = _mwnd_base.GetActiveEditor();
			if (editor is IFileSaveLoadFeature fsl) {
				_sfd.Reset();
				_sfd.Filter = fsl.GetFileType().CreateSPF();
				var dr = _sfd.ShowDialog();
				if (dr == DialogResult.Yes) {
					fsl.SaveAs(_sfd.FileName);
					_mwnd_base.SetStatusMessage(MainWindowStatusMessage.FileSaved(fsl.TargetFile.FilePath));
				} else {
					_mwnd_base.SetStatusMessage(MainWindowStatusMessage.Ready());
				}
			} else {
				_mwnd_base.SetStatusMessage(MainWindowStatusMessage.Unimplemented(_saveAs.Text));
			}

			Logger.Trace($"{nameof(FileMenuItem)}: _saveAs end");
		}

		private void _saveAll_Click(object sender, EventArgs e)
		{
			Logger.Trace($"{nameof(FileMenuItem)}: _saveAll begin");

			foreach (var win in _mwnd_base.MdiChildren) {
				var fsl = win as IFileSaveLoadFeature;
				if (fsl != null) {
					fsl.Save();
				}
			}
			_mwnd_base.SetStatusMessage(MainWindowStatusMessage.AllSaved());

			Logger.Trace($"{nameof(FileMenuItem)}: _saveAll end");
		}

		private void _print_Click(object sender, EventArgs e)
		{
			Logger.Trace($"{nameof(FileMenuItem)}: _print begin");

			var window = _mwnd_base.GetActiveEditor();
			if (window is IPrintingFeature p) {
				_pd.Reset();
				_pd.Document = p.PrintDocument;
				var dr = _pd.ShowDialog();
				if (dr == DialogResult.OK) {
					p.PrintDocument.Print();
					_mwnd_base.SetStatusMessage(MainWindowStatusMessage.PrintStarted());
				} else {
					_mwnd_base.SetStatusMessage(MainWindowStatusMessage.Ready());
				}
			} else {
				_mwnd_base.SetStatusMessage(MainWindowStatusMessage.Unimplemented(_print.Text));
			}

			Logger.Trace($"{nameof(FileMenuItem)}: _print end");
		}

		private void _printPreview_Click(object sender, EventArgs e)
		{
			Logger.Trace($"{nameof(FileMenuItem)}: _printPreview begin");

			var window = _mwnd_base.GetActiveEditor();
			if (window is IPrintingFeature p) {
				_ppd.Document = p.PrintDocument;
				_ppd.MdiParent = _mwnd_base;
				_ppd.Show();
				_mwnd_base.SetStatusMessage(MainWindowStatusMessage.Ready());
			} else {
				_mwnd_base.SetStatusMessage(MainWindowStatusMessage.Unimplemented(_printPreview.Text));
			}

			Logger.Trace($"{nameof(FileMenuItem)}: _printPreview end");
		}

		private void _exit_Click(object sender, EventArgs e)
		{
			Logger.Trace($"{nameof(FileMenuItem)}: _exit begin");

			_mwnd_base.Close();

			Logger.Trace($"{nameof(FileMenuItem)}: _exit end");
		}
	}
}
