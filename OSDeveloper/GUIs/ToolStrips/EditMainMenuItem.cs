using System.Windows.Forms;
using OSDeveloper.GUIs.Editors;
using OSDeveloper.GUIs.Features;
using OSDeveloper.Resources;

namespace OSDeveloper.GUIs.ToolStrips
{
	public partial class EditMainMenuItem : MainMenuItem
	{
		private readonly ToolStripMenuItem _undo, _redo;
		private readonly ToolStripMenuItem _cut, _copy, _paste, _delete;
		private readonly ToolStripMenuItem _selectAll, _clear;

		public EditMainMenuItem(FormMain mwnd) : base(mwnd)
		{
			this.Name = "EDIT";
			this.Text = MenuTexts.Edit;

			_undo      = new ToolStripMenuItem();
			_redo      = new ToolStripMenuItem();
			_cut       = new ToolStripMenuItem();
			_copy      = new ToolStripMenuItem();
			_paste     = new ToolStripMenuItem();
			_delete    = new ToolStripMenuItem();
			_selectAll = new ToolStripMenuItem();
			_clear     = new ToolStripMenuItem();

			_undo.Name              = nameof(_undo);
			_undo.Text              = MenuTexts.Edit_Undo;
			_undo.Click            += this._undo_Click;
			_undo.ShortcutKeys      = Keys.Control | Keys.Z;

			_redo.Name               = nameof(_redo);
			_redo.Text               = MenuTexts.Edit_Redo;
			_redo.Click             += this._redo_Click;
			_redo.ShortcutKeys       = Keys.Control | Keys.Y;

			_cut.Name                = nameof(_cut);
			_cut.Text                = MenuTexts.Edit_Cut;
			_cut.Click              += this._cut_Click;
			_cut.ShortcutKeys        = Keys.Control | Keys.X;

			_copy.Name               = nameof(_copy);
			_copy.Text               = MenuTexts.Edit_Copy;
			_copy.Click             += this._copy_Click;
			_copy.ShortcutKeys       = Keys.Control | Keys.C;

			_paste.Name              = nameof(_paste);
			_paste.Text              = MenuTexts.Edit_Paste;
			_paste.Click            += this._paste_Click;
			_paste.ShortcutKeys      = Keys.Control | Keys.V;

			_delete.Name             = nameof(_delete);
			_delete.Text             = MenuTexts.Edit_Delete;
			_delete.Click           += this._delete_Click;
			_delete.ShortcutKeys     = Keys.Delete;

			_selectAll.Name          = nameof(_selectAll);
			_selectAll.Text          = MenuTexts.Edit_SelectAll;
			_selectAll.Click        += this._selectAll_Click;
			_selectAll.ShortcutKeys  = Keys.Control | Keys.A;

			_clear.Name              = nameof(_clear);
			_clear.Text              = MenuTexts.Edit_Clear;
			_clear.Click            += this._clear_Click;
			_clear.ShortcutKeys      = Keys.Control | Keys.Shift | Keys.A;

			this.DropDownItems.Add(_undo);
			this.DropDownItems.Add(_redo);
			this.DropDownItems.Add(new ToolStripSeparator());
			this.DropDownItems.Add(_cut);
			this.DropDownItems.Add(_copy);
			this.DropDownItems.Add(_paste);
			this.DropDownItems.Add(_delete);
			this.DropDownItems.Add(new ToolStripSeparator());
			this.DropDownItems.Add(_selectAll);
			this.DropDownItems.Add(_clear);

			_logger.Trace($"constructed {nameof(EditMainMenuItem)}");
		}

		private void _undo_Click(object sender, System.EventArgs e)
		{
			_logger.Trace($"executing {nameof(_undo_Click)}...");

			if (_mwnd.ActiveMdiChild is EditorWindow editor && editor is IUndoRedoFeature urf) {
				if (urf.CanUndo) {
					urf.Undo();
					_mwnd.StatusMessageLeft = FormMainRes.Status_Ready;
				} else {
					_mwnd.StatusMessageLeft = FormMainRes.Status_CannotUndo;
				}
			} else {
				_mwnd.StatusMessageLeft = string.Format(FormMainRes.Status_NotSupported, _undo.Text);
			}

			_logger.Trace($"completed {nameof(_undo_Click)}");
		}

		private void _redo_Click(object sender, System.EventArgs e)
		{
			_logger.Trace($"executing {nameof(_redo_Click)}...");

			if (_mwnd.ActiveMdiChild is EditorWindow editor && editor is IUndoRedoFeature urf) {
				if (urf.CanRedo) {
					urf.Redo();
					_mwnd.StatusMessageLeft = FormMainRes.Status_Ready;
				} else {
					_mwnd.StatusMessageLeft = FormMainRes.Status_CannotRedo;
				}
			} else {
				_mwnd.StatusMessageLeft = string.Format(FormMainRes.Status_NotSupported, _redo.Text);
			}

			_logger.Trace($"completed {nameof(_redo_Click)}");
		}

		private void _cut_Click(object sender, System.EventArgs e)
		{
			_logger.Trace($"executing {nameof(_cut_Click)}...");

			if (_mwnd.ActiveMdiChild is EditorWindow editor && editor is IClipboardFeature cf) {
				cf.Cut();
				_mwnd.StatusMessageLeft = FormMainRes.Status_Ready;
			} else {
				_mwnd.StatusMessageLeft = string.Format(FormMainRes.Status_NotSupported, _cut.Text);
			}

			_logger.Trace($"completed {nameof(_cut_Click)}");
		}

		private void _copy_Click(object sender, System.EventArgs e)
		{
			_logger.Trace($"executing {nameof(_copy_Click)}...");

			if (_mwnd.ActiveMdiChild is EditorWindow editor && editor is IClipboardFeature cf) {
				cf.Copy();
				_mwnd.StatusMessageLeft = FormMainRes.Status_Ready;
			} else {
				_mwnd.StatusMessageLeft = string.Format(FormMainRes.Status_NotSupported, _copy.Text);
			}

			_logger.Trace($"completed {nameof(_copy_Click)}");
		}

		private void _paste_Click(object sender, System.EventArgs e)
		{
			_logger.Trace($"executing {nameof(_paste_Click)}...");

			if (_mwnd.ActiveMdiChild is EditorWindow editor && editor is IClipboardFeature cf) {
				cf.Paste();
				_mwnd.StatusMessageLeft = FormMainRes.Status_Ready;
			} else {
				_mwnd.StatusMessageLeft = string.Format(FormMainRes.Status_NotSupported, _paste.Text);
			}

			_logger.Trace($"completed {nameof(_paste_Click)}");
		}

		private void _delete_Click(object sender, System.EventArgs e)
		{
			_logger.Trace($"executing {nameof(_delete_Click)}...");

			if (_mwnd.ActiveMdiChild is EditorWindow editor && editor is ISelectionFeature sf) {
				sf.DeleteSelection();
				_mwnd.StatusMessageLeft = FormMainRes.Status_Ready;
			} else {
				_mwnd.StatusMessageLeft = string.Format(FormMainRes.Status_NotSupported, _delete.Text);
			}

			_logger.Trace($"completed {nameof(_delete_Click)}");
		}

		private void _selectAll_Click(object sender, System.EventArgs e)
		{
			_logger.Trace($"executing {nameof(_selectAll_Click)}...");

			if (_mwnd.ActiveMdiChild is EditorWindow editor && editor is ISelectionFeature sf) {
				sf.SelectAll();
				_mwnd.StatusMessageLeft = FormMainRes.Status_Ready;
			} else {
				_mwnd.StatusMessageLeft = string.Format(FormMainRes.Status_NotSupported, _selectAll.Text);
			}

			_logger.Trace($"completed {nameof(_selectAll_Click)}");
		}

		private void _clear_Click(object sender, System.EventArgs e)
		{
			_logger.Trace($"executing {nameof(_clear_Click)}...");

			if (_mwnd.ActiveMdiChild is EditorWindow editor && editor is ISelectionFeature sf) {
				sf.ClearSelection();
				_mwnd.StatusMessageLeft = FormMainRes.Status_Ready;
			} else {
				_mwnd.StatusMessageLeft = string.Format(FormMainRes.Status_NotSupported, _clear.Text);
			}

			_logger.Trace($"completed {nameof(_clear_Click)}");
		}
	}
}
