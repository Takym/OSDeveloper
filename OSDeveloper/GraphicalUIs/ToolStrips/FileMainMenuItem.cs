using System;
using System.Windows.Forms;
using OSDeveloper.GraphicalUIs.Editors;
using OSDeveloper.GraphicalUIs.Features;
using OSDeveloper.IO;
using OSDeveloper.Native;
using OSDeveloper.Resources;
using TakymLib.IO;

namespace OSDeveloper.GraphicalUIs.ToolStrips
{
	public partial class FileMainMenuItem : MainMenuItem
	{
		private readonly SaveFileDialog _sfd;
		private readonly ToolStripMenuItem _reload;
		private readonly ToolStripMenuItem _save, _saveAs, _saveAll, _saveAllAs;
		private readonly ToolStripMenuItem _exit;

		public FileMainMenuItem(FormMain mwnd) : base(mwnd)
		{
			this.Name = "FILE";
			this.Text = MenuTexts.File;

			_sfd       = new SaveFileDialog();
			_reload    = new ToolStripMenuItem();
			_save      = new ToolStripMenuItem();
			_saveAs    = new ToolStripMenuItem();
			_saveAll   = new ToolStripMenuItem();
			_saveAllAs = new ToolStripMenuItem();
			_exit      = new ToolStripMenuItem();

			_reload.Name             = nameof(_reload);
			_reload.Text             = MenuTexts.File_Reload;
			_reload.Click           += this._reload_Click;
			_reload.ShortcutKeys     = Keys.F5;

			_save.Name               = nameof(_save);
			_save.Text               = MenuTexts.File_Save;
			_save.Click             += this._save_Click;
			_save.ShortcutKeys       = Keys.Control | Keys.S;
			_save.Image              = Libosdev.GetIcon(Libosdev.Icons.MenuFileSave, out uint v0).ToBitmap();
			
			_saveAs.Name             = nameof(_saveAs);
			_saveAs.Text             = MenuTexts.File_SaveAs;
			_saveAs.Click           += this._saveAs_Click;
			_saveAs.ShortcutKeys     = Keys.Control | Keys.Shift | Keys.S;
			_saveAs.Image            = Libosdev.GetIcon(Libosdev.Icons.MenuFileSaveAs, out uint v1).ToBitmap();

			_saveAll.Name            = nameof(_saveAll);
			_saveAll.Text            = MenuTexts.File_SaveAll;
			_saveAll.Click          += this._saveAll_Click;
			_saveAll.ShortcutKeys    = Keys.Control | Keys.Alt | Keys.S;
			_saveAll.Image           = Libosdev.GetIcon(Libosdev.Icons.MenuFileSaveAll, out uint v2).ToBitmap();

			_saveAllAs.Name          = nameof(_saveAllAs);
			_saveAllAs.Text          = MenuTexts.File_SaveAllAs;
			_saveAllAs.Click        += this._saveAllAs_Click;
			_saveAllAs.ShortcutKeys  = Keys.Control | Keys.Shift | Keys.Alt | Keys.S;
			_saveAllAs.Image         = Libosdev.GetIcon(Libosdev.Icons.MenuFileSaveAllAs, out uint v3).ToBitmap();

			_exit.Name               = nameof(_exit);
			_exit.Text               = MenuTexts.File_Exit;
			_exit.Click             += this._exit_Click;
			_exit.ShortcutKeys       = Keys.Alt | Keys.F4;

			this.DropDownItems.Add(_reload);
			this.DropDownItems.Add(new ToolStripSeparator());
			this.DropDownItems.Add(_save);
			this.DropDownItems.Add(_saveAs);
			this.DropDownItems.Add(_saveAll);
			this.DropDownItems.Add(_saveAllAs);
			this.DropDownItems.Add(new ToolStripSeparator());
			this.DropDownItems.Add(_exit);

			_logger.Trace($"constructed {nameof(FileMainMenuItem)}");
		}

		private void _reload_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(_reload_Click)}...");

			if (_mwnd.ActiveMdiChild is EditorWindow editor && editor is IFileLoadFeature flf) {
				flf.Reload();
				_mwnd.StatusMessageLeft = FormMainRes.Status_Ready;
			} else {
				_mwnd.StatusMessageLeft = string.Format(FormMainRes.Status_NotSupported, _save.Text);
			}

			_logger.Trace($"completed {nameof(_reload_Click)}");
		}

		private void _save_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(_save_Click)}...");

			if (_mwnd.ActiveMdiChild is EditorWindow editor && editor is IFileSaveFeature fsf) {
				fsf.Save();
				_mwnd.StatusMessageLeft = FormMainRes.Status_Ready;
			} else {
				_mwnd.StatusMessageLeft = string.Format(FormMainRes.Status_NotSupported, _save.Text);
			}

			_logger.Trace($"completed {nameof(_save_Click)}");
		}

		private void _saveAs_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(_saveAs_Click)}...");

			if (_mwnd.ActiveMdiChild is EditorWindow editor && editor is IFileSaveFeature fsf) {
				_sfd.Reset();
				_sfd.OverwritePrompt = true;
				_sfd.AddExtension    = true;
				_sfd.Filter          = FileTypeRegistry.GetByExtension(fsf.Item.Path.GetExtension()).CreateSPFs();
				if (_sfd.ShowDialog() == DialogResult.OK) {
					fsf.SaveAs(new PathString(_sfd.FileName));
				}
				_mwnd.StatusMessageLeft = FormMainRes.Status_Ready;
			} else {
				_mwnd.StatusMessageLeft = string.Format(FormMainRes.Status_NotSupported, _saveAs.Text);
			}

			_logger.Trace($"completed {nameof(_saveAs_Click)}");
		}

		private void _saveAll_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(_saveAll_Click)}...");

			for (int i = 0; i < _mwnd.MdiChildren.Length; ++i) {
				var f = _mwnd.MdiChildren[i];
				if (f is EditorWindow editor && editor is IFileSaveFeature fsf) {
					fsf.Save();
				}
			}
			_mwnd.StatusMessageLeft = FormMainRes.Status_Ready;

			_logger.Trace($"completed {nameof(_saveAll_Click)}");
		}

		private void _saveAllAs_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(_saveAllAs_Click)}...");

			for (int i = 0; i < _mwnd.MdiChildren.Length; ++i) {
				var f = _mwnd.MdiChildren[i];
				if (f is EditorWindow editor && editor is IFileSaveFeature fsf) {
					_sfd.Reset();
					_sfd.OverwritePrompt = true;
					_sfd.AddExtension    = true;
					_sfd.Filter          = FileTypeRegistry.GetByExtension(fsf.Item.Path.GetExtension()).CreateSPFs();
					if (_sfd.ShowDialog() == DialogResult.OK) {
						fsf.SaveAs(new PathString(_sfd.FileName));
					}
				}
			}
			_mwnd.StatusMessageLeft = FormMainRes.Status_Ready;

			_logger.Trace($"completed {nameof(_saveAllAs_Click)}");
		}

		private void _exit_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(_exit_Click)}...");
			Application.Exit();
			_logger.Trace($"completed {nameof(_exit_Click)}");
		}
	}
}
