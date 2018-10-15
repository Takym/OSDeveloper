using System;
using System.Windows.Forms;
using OSDeveloper.Core.Editors;
using static OSDeveloper.Core.GraphicalUIs.ToolStrips.MenuStripManager;

namespace OSDeveloper.Core.GraphicalUIs.ToolStrips
{
	partial class MenuStripManager { } // デザイナ避け

	/// <summary>
	///  編集を手助けするメニューを表します。
	/// </summary>
	public class EditMenuItem : MainMenuItem
	{
		private readonly MainWindowBase _mwnd_base;
		private readonly ToolStripMenuItem _undo, _redo;
		private readonly ToolStripMenuItem _copy, _paste, _cut, _delete;
		private readonly ToolStripMenuItem _select_all, _clear_selection;

		/// <summary>
		///  このメニューの操作対象を指定して、
		///  型'<see cref="OSDeveloper.Core.GraphicalUIs.ToolStrips.EditMenuItem"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		/// <param name="mwndBase">このメニューの親ウィンドウです。</param>
		public EditMenuItem(MainWindowBase mwndBase)
		{
			_mwnd_base = mwndBase;
			_undo = new ToolStripMenuItem();
			_redo = new ToolStripMenuItem();
			_copy = new ToolStripMenuItem();
			_paste = new ToolStripMenuItem();
			_cut = new ToolStripMenuItem();
			_delete = new ToolStripMenuItem();
			_select_all = new ToolStripMenuItem();
			_clear_selection = new ToolStripMenuItem();

			_undo.Text = MenuText.Edit_Undo;
			_undo.ShortcutKeys = Keys.Control | Keys.Z;
			_undo.Click += this._undo_Click;
			_redo.Text = MenuText.Edit_Redo;
			_redo.ShortcutKeys = Keys.Control | Keys.Y;
			_redo.Click += this._redo_Click;
			_copy.Text = MenuText.Edit_Copy;
			_copy.ShortcutKeys = Keys.Control | Keys.C;
			_copy.Click += this._copy_Click;
			_paste.Text = MenuText.Edit_Paste;
			_paste.ShortcutKeys = Keys.Control | Keys.V;
			_paste.Click += this._paste_Click;
			_cut.Text = MenuText.Edit_Cut;
			_cut.ShortcutKeys = Keys.Control | Keys.X;
			_cut.Click += this._cut_Click;
			_delete.Text = MenuText.Edit_Delete;
			_delete.ShortcutKeys = Keys.Delete;
			_delete.Click += this._delete_Click;
			_select_all.Text = MenuText.Edit_SelectAll;
			_select_all.ShortcutKeys = Keys.Control | Keys.A;
			_select_all.Click += this._select_all_Click;
			_clear_selection.Text = MenuText.Edit_ClearSelection;
			_clear_selection.Click += this._clear_selection_Click;

			this.DropDownItems.Add(_undo);
			this.DropDownItems.Add(_redo);
			this.DropDownItems.Add(new ToolStripSeparator());
			this.DropDownItems.Add(_copy);
			this.DropDownItems.Add(_paste);
			this.DropDownItems.Add(_cut);
			this.DropDownItems.Add(_delete);
			this.DropDownItems.Add(new ToolStripSeparator());
			this.DropDownItems.Add(_select_all);
			this.DropDownItems.Add(_clear_selection);
		}

		private void _undo_Click(object sender, EventArgs e)
		{
			Logger.Trace($"{nameof(EditMenuItem)}: _undo begin");

			var editor = _mwnd_base.GetActiveEditor();
			if (editor is IUndoRedoFeature urf) {
				urf.Undo();
				_mwnd_base.SetStatusMessage(MainWindowStatusMessage.Ready());
			} else {
				_mwnd_base.SetStatusMessage(MainWindowStatusMessage.Unimplemented(_undo.Text));
			}

			Logger.Trace($"{nameof(EditMenuItem)}: _undo end");
		}

		private void _redo_Click(object sender, EventArgs e)
		{
			Logger.Trace($"{nameof(EditMenuItem)}: _redo begin");

			var editor = _mwnd_base.GetActiveEditor();
			if (editor is IUndoRedoFeature urf) {
				urf.Redo();
				_mwnd_base.SetStatusMessage(MainWindowStatusMessage.Ready());
			} else {
				_mwnd_base.SetStatusMessage(MainWindowStatusMessage.Unimplemented(_redo.Text));
			}

			Logger.Trace($"{nameof(EditMenuItem)}: _redo end");
		}

		private void _copy_Click(object sender, EventArgs e)
		{
			Logger.Trace($"{nameof(EditMenuItem)}: _copy begin");

			var editor = _mwnd_base.GetActiveEditor();
			if (editor is IClipboardFeature cf) {
				cf.Copy();
				_mwnd_base.SetStatusMessage(MainWindowStatusMessage.Ready());
			} else {
				_mwnd_base.SetStatusMessage(MainWindowStatusMessage.Unimplemented(_copy.Text));
			}

			Logger.Trace($"{nameof(EditMenuItem)}: _copy end");
		}

		private void _paste_Click(object sender, EventArgs e)
		{
			Logger.Trace($"{nameof(EditMenuItem)}: _paste begin");

			var editor = _mwnd_base.GetActiveEditor();
			if (editor is IClipboardFeature cf) {
				cf.Paste();
				_mwnd_base.SetStatusMessage(MainWindowStatusMessage.Ready());
			} else {
				_mwnd_base.SetStatusMessage(MainWindowStatusMessage.Unimplemented(_paste.Text));
			}

			Logger.Trace($"{nameof(EditMenuItem)}: _paste end");
		}

		private void _cut_Click(object sender, EventArgs e)
		{
			Logger.Trace($"{nameof(EditMenuItem)}: _cut begin");

			var editor = _mwnd_base.GetActiveEditor();
			if (editor is IClipboardFeature cf) {
				cf.Cut();
				_mwnd_base.SetStatusMessage(MainWindowStatusMessage.Ready());
			} else {
				_mwnd_base.SetStatusMessage(MainWindowStatusMessage.Unimplemented(_cut.Text));
			}

			Logger.Trace($"{nameof(EditMenuItem)}: _cut end");
		}

		private void _delete_Click(object sender, EventArgs e)
		{
			Logger.Trace($"{nameof(EditMenuItem)}: _delete begin");

			var editor = _mwnd_base.GetActiveEditor();
			if (editor is IClipboardFeature cf) {
				cf.Delete();
				_mwnd_base.SetStatusMessage(MainWindowStatusMessage.Ready());
			} else {
				_mwnd_base.SetStatusMessage(MainWindowStatusMessage.Unimplemented(_delete.Text));
			}

			Logger.Trace($"{nameof(EditMenuItem)}: _delete end");
		}

		private void _select_all_Click(object sender, EventArgs e)
		{
			Logger.Trace($"{nameof(EditMenuItem)}: _select_all begin");

			var editor = _mwnd_base.GetActiveEditor();
			if (editor is ISelectionFeature sf) {
				sf.SelectAll();
				_mwnd_base.SetStatusMessage(MainWindowStatusMessage.Ready());
			} else {
				_mwnd_base.SetStatusMessage(MainWindowStatusMessage.Unimplemented(_select_all.Text));
			}

			Logger.Trace($"{nameof(EditMenuItem)}: _select_all end");
		}

		private void _clear_selection_Click(object sender, EventArgs e)
		{
			Logger.Trace($"{nameof(EditMenuItem)}: _clear_selection begin");

			var editor = _mwnd_base.GetActiveEditor();
			if (editor is ISelectionFeature sf) {
				sf.ClearSelection();
				_mwnd_base.SetStatusMessage(MainWindowStatusMessage.Ready());
			} else {
				_mwnd_base.SetStatusMessage(MainWindowStatusMessage.Unimplemented(_clear_selection.Text));
			}

			Logger.Trace($"{nameof(EditMenuItem)}: _clear_selection end");
		}
	}
}
