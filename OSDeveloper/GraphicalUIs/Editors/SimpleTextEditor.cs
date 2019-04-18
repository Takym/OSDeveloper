using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;
using OSDeveloper.GraphicalUIs.Features;
using OSDeveloper.IO.ItemManagement;
using TakymLib;
using TakymLib.IO;

namespace OSDeveloper.GraphicalUIs.Editors
{
	[DesignerCategory("")] // デザイナ避け
	public partial class SimpleTextEditor : EditorWindow, IFileSaveLoadFeature, IPrintingFeature, IClipboardFeature
	{
		private TextBox       _textBox;
		private PrintDocument _pdoc;

		public PrintDocument PrintDocument
		{
			get
			{
				return _pdoc;
			}
		}

		public bool UseCustomDialogs => false;

		public SimpleTextEditor(FormMain mwnd, ItemMetadata metadata) : base(mwnd, metadata)
		{
			this.SuspendLayout();

			_textBox            = new TextBox();
			_textBox.Multiline  = true;
			_textBox.ScrollBars = ScrollBars.Both;
			_textBox.Dock       = DockStyle.Fill;
			_textBox.Text       = (metadata as FileMetadata)?.ReadAllText();
			_textBox.Font       = new Font(_textBox.Font.FontFamily, 12.5F);

			_pdoc               = new PrintDocument();
			_pdoc.DocumentName  = metadata.Name;
			_pdoc.PrintPage    += this._pdoc_PrintPage;

			this.Controls.Add(_textBox);
			this.ResumeLayout(false);
			this.PerformAutoScale();
		}

		public void Reload()
		{
			_textBox.Text = (this.Item as FileMetadata)?.ReadAllText();
		}

		public void Save()
		{
			(this.Item as FileMetadata)?.WriteAllText(_textBox.Text);
		}

		public void SaveAs(PathString filename)
		{
			using (var sw = new StreamWriter(filename)) {
				sw.Write(_textBox.Text);
			}
		}

		public void Cut()
		{
			_textBox.Cut();
		}

		public void Copy()
		{
			_textBox.Copy();
		}

		public void Paste()
		{
			_textBox.Paste();
		}

		public void SelectAll()
		{
			_textBox.SelectAll();
		}

		public void ClearSelection()
		{
			_textBox.SelectionLength = 0;
		}

		public void DeleteSelection()
		{
			_textBox.SelectedText = string.Empty;
		}

		public void ShowPrintDialog(bool useExDialog)
		{
			throw new NotImplementedException();
		}

		public Form ShowPrintPreviewDialog(bool useExDialog)
		{
			throw new NotImplementedException();
		}

		public void ShowPageSetupDialog(bool useExDialog)
		{
			throw new NotImplementedException();
		}

		private bool   _is_printing;
		private string _text;
		private int    _pos;
		private void _pdoc_PrintPage(object sender, PrintPageEventArgs e)
		{
			this.Logger.Trace($"executing {nameof(_pdoc_PrintPage)}...");

			var f = _textBox.Font;
			var g = e.Graphics;
			var r = e.PageBounds;
			var h = 0;
			if (!_is_printing) {
				_text            = _textBox.Text.CRtoLF() + '\n';
				_pos             = 0;
				_is_printing     = true;
				_textBox.Enabled = false;
			}
			while (_pos < _text.Length && h < r.Height + f.Height) {
				int a = _text.IndexOf('\n', _pos) - _pos;
retry:
				if (a < 0) continue;
				string line = _text.Substring(_pos, a);
				if (g.MeasureString(line, f).Width >= r.Width) {
					--a;
					goto retry;
				}
				_pos += a + 1;
				using (var b = new SolidBrush(_textBox.ForeColor)) {
					g.DrawString(line, f, b, r.X, r.Y + h);
				}
				h += f.Height;
			}
			if (_pos >= _text.Length) {
				_text            = null;
				_pos             = 0;
				_is_printing     = false;
				_textBox.Enabled = true;
			} else {
				e.HasMorePages = true;
			}

			this.Logger.Trace($"completed {nameof(_pdoc_PrintPage)}");
		}
	}
}
