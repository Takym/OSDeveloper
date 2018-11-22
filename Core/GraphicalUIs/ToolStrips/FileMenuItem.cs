using System;
using System.Windows.Forms;
using OSDeveloper.Core.Editors;
using OSDeveloper.Core.FileManagement;
using OSDeveloper.Native;
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
		private readonly ToolStripMenuItem _newfile, _open, _reload;
		private readonly ToolStripMenuItem _save, _saveAs, _saveAll;
		private readonly ToolStripMenuItem _print, _printPreview, _pageSetup;
		private readonly ToolStripMenuItem _capture_entire, _capture_active;
		private readonly ToolStripMenuItem _exit;
		private readonly SaveFileDialog _sfd;
		private readonly PrintDialog _pd;
		private readonly PageSetupDialog _psd;

		/// <summary>
		///  このメニューの操作対象を指定して、
		///  型'<see cref="OSDeveloper.Core.GraphicalUIs.ToolStrips.FileMenuItem"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		/// <param name="mwndBase">このメニューの親ウィンドウです。</param>
		public FileMenuItem(MainWindowBase mwndBase)
		{
			_logger.Info($"creating a {nameof(FileMenuItem)}...");

			_mwnd_base = mwndBase;
			_newfile = new ToolStripMenuItem();
			_open = new ToolStripMenuItem();
			_reload = new ToolStripMenuItem();
			_save = new ToolStripMenuItem();
			_saveAs = new ToolStripMenuItem();
			_saveAll = new ToolStripMenuItem();
			_print = new ToolStripMenuItem();
			_printPreview = new ToolStripMenuItem();
			_pageSetup = new ToolStripMenuItem();
			_capture_entire = new ToolStripMenuItem();
			_capture_active = new ToolStripMenuItem();
			_exit = new ToolStripMenuItem();
			_sfd = new SaveFileDialog();
			_pd = new PrintDialog();
			_psd = new PageSetupDialog();

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
			_pageSetup.Text = MenuTexts.File_PageSetup;
			_pageSetup.Click += this._pageSetup_Click;
			_capture_entire.Text = MenuTexts.File_CaptureEntire;
			_capture_entire.ShortcutKeys = Keys.Shift | Keys.F1;
			_capture_entire.Click += this._capture_entire_Click;
			_capture_active.Text = MenuTexts.File_CaptureActive;
			_capture_active.ShortcutKeys = Keys.Shift | Keys.F2;
			_capture_active.Click += this._capture_active_Click;
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
			this.DropDownItems.Add(_pageSetup);
			this.DropDownItems.Add(new ToolStripSeparator());
			this.DropDownItems.Add(_capture_entire);
			this.DropDownItems.Add(_capture_active);
			this.DropDownItems.Add(new ToolStripSeparator());
			this.DropDownItems.Add(_exit);

			_logger.Info($"{nameof(FileMenuItem)} is created successfully");
		}

		private void _newfile_Click(object sender, EventArgs e)
		{
			_logger.Trace($"{nameof(FileMenuItem)}: {nameof(_newfile)} begin");
			_mwnd_base.SetStatusMessage(MainWindowStatusMessage.Unimplemented(_newfile.Text));
			_logger.Trace($"{nameof(FileMenuItem)}: {nameof(_newfile)} end");
		}

		private void _open_Click(object sender, EventArgs e)
		{
			_logger.Trace($"{nameof(FileMenuItem)}: {nameof(_open)} begin");
			_mwnd_base.SetStatusMessage(MainWindowStatusMessage.Unimplemented(_open.Text));
			_logger.Trace($"{nameof(FileMenuItem)}: {nameof(_open)} end");
		}

		private void _reload_Click(object sender, EventArgs e)
		{
			_logger.Trace($"{nameof(FileMenuItem)}: {nameof(_reload)} begin");

			var editor = _mwnd_base.GetActiveEditor();
			if (editor is IFileSaveLoadFeature fsl) {
				fsl.Reload();
				_mwnd_base.SetStatusMessage(MainWindowStatusMessage.Ready());
			} else {
				_mwnd_base.SetStatusMessage(MainWindowStatusMessage.Unimplemented(_reload.Text));
			}

			_logger.Trace($"{nameof(FileMenuItem)}: {nameof(_reload)} end");
		}

		private void _save_Click(object sender, EventArgs e)
		{
			_logger.Trace($"{nameof(FileMenuItem)}: {nameof(_save)} begin");

			var editor = _mwnd_base.GetActiveEditor();
			if (editor is IFileSaveLoadFeature fsl) {
				fsl.Save();
				_mwnd_base.SetStatusMessage(MainWindowStatusMessage.FileSaved(fsl.TargetFile.FilePath));
			} else {
				_mwnd_base.SetStatusMessage(MainWindowStatusMessage.Unimplemented(_save.Text));
			}

			_logger.Trace($"{nameof(FileMenuItem)}: {nameof(_save)} end");
		}

		private void _saveAs_Click(object sender, EventArgs e)
		{
			_logger.Trace($"{nameof(FileMenuItem)}: {nameof(_saveAs)} begin");

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

			_logger.Trace($"{nameof(FileMenuItem)}: {nameof(_saveAs)} end");
		}

		private void _saveAll_Click(object sender, EventArgs e)
		{
			_logger.Trace($"{nameof(FileMenuItem)}: {nameof(_saveAll)} begin");

			foreach (var win in _mwnd_base.MdiChildren) {
				var fsl = win as IFileSaveLoadFeature;
				if (fsl != null) {
					fsl.Save();
				}
			}
			_mwnd_base.SetStatusMessage(MainWindowStatusMessage.AllSaved());

			_logger.Trace($"{nameof(FileMenuItem)}: {nameof(_saveAll)} end");
		}

		private void _print_Click(object sender, EventArgs e)
		{
			_logger.Trace($"{nameof(FileMenuItem)}: {nameof(_print)} begin");

			var window = _mwnd_base.GetActiveEditor();
			if (window is IPrintingFeature p) {
				_pd.Reset();
				_pd.Document = p.PrintDocument;
				_pd.UseEXDialog = _use_ex_dialog;
				var dr = _pd.ShowDialog();
				if (dr == DialogResult.OK) {
					p.PrintDocument.Print();
					_mwnd_base.SetStatusMessage(MainWindowStatusMessage.PrintStarted());
				} else {
					_mwnd_base.SetStatusMessage(MainWindowStatusMessage.Ready());
				}
			} else if (window is ICustomPrintingFeature cp) {
				_mwnd_base.SetStatusMessage(MainWindowStatusMessage.PrintDialogShown());
				cp.ShowPrintDialog();
				_mwnd_base.SetStatusMessage(MainWindowStatusMessage.Ready());
			} else {
				_mwnd_base.SetStatusMessage(MainWindowStatusMessage.Unimplemented(_print.Text));
			}

			_logger.Trace($"{nameof(FileMenuItem)}: {nameof(_print)} end");
		}

		private void _printPreview_Click(object sender, EventArgs e)
		{
			_logger.Trace($"{nameof(FileMenuItem)}: {nameof(_printPreview)} begin");

			var window = _mwnd_base.GetActiveEditor();
			if (window is IPrintingFeature p) {
				var ppd = new PrintPreviewDialog();
				ppd.Document = p.PrintDocument;
				ppd.MdiParent = _mwnd_base;
				ppd.Show();
				_mwnd_base.SetStatusMessage(MainWindowStatusMessage.Ready());
			} else if (window is ICustomPrintingFeature cp) {
				cp.ShowPrintPreviewDialog();
				_mwnd_base.SetStatusMessage(MainWindowStatusMessage.Ready());
			} else {
				_mwnd_base.SetStatusMessage(MainWindowStatusMessage.Unimplemented(_printPreview.Text));
			}

			_logger.Trace($"{nameof(FileMenuItem)}: {nameof(_printPreview)} end");
		}

		private void _pageSetup_Click(object sender, EventArgs e)
		{
			_logger.Trace($"{nameof(FileMenuItem)}: {nameof(_pageSetup)} begin");

			var window = _mwnd_base.GetActiveEditor();
			if (window is IPrintingFeature p) {
				_psd.Reset();
				_psd.Document = p.PrintDocument;
				_psd.ShowDialog();
				_mwnd_base.SetStatusMessage(MainWindowStatusMessage.Ready());
			} else if (window is ICustomPrintingFeature cp) {
				cp.ShowPageSetupDialog();
				_mwnd_base.SetStatusMessage(MainWindowStatusMessage.Ready());
			} else {
				_mwnd_base.SetStatusMessage(MainWindowStatusMessage.Unimplemented(_pageSetup.Text));
			}

			_logger.Trace($"{nameof(FileMenuItem)}: {nameof(_pageSetup)} end");
		}

		private void _capture_entire_Click(object sender, EventArgs e)
		{
			_logger.Trace($"{nameof(FileMenuItem)}: {nameof(_capture_entire)} begin");

			var img = User32.CaptureControl(_mwnd_base);
			Clipboard.SetImage(img);
			_mwnd_base.SetStatusMessage(MainWindowStatusMessage.EntireCaptured());

			_logger.Trace($"{nameof(FileMenuItem)}: {nameof(_capture_entire)} end");
		}

		private void _capture_active_Click(object sender, EventArgs e)
		{
			_logger.Trace($"{nameof(FileMenuItem)}: {nameof(_capture_active)} begin");

			var c = _mwnd_base.ActiveControl ?? _mwnd_base.ActiveMdiChild;
			if (c != null) {
				var img = User32.CaptureControl(c);
				Clipboard.SetImage(img);
				_mwnd_base.SetStatusMessage(MainWindowStatusMessage.ActiveCaptured($"{c.Text}/{c.Name}"));
			} else {
				_mwnd_base.SetStatusMessage(MainWindowStatusMessage.Ready());
			}

			_logger.Trace($"{nameof(FileMenuItem)}: {nameof(_capture_active)} end");
		}

		private void _exit_Click(object sender, EventArgs e)
		{
			_logger.Trace($"{nameof(FileMenuItem)}: {nameof(_exit)} begin");

			_mwnd_base.Close();

			_logger.Trace($"{nameof(FileMenuItem)}: {nameof(_exit)} end");
		}
	}
}
