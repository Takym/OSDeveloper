using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using OSDeveloper.Assets;
using OSDeveloper.Core.Error;
using OSDeveloper.Core.MiscUtils;

namespace OSDeveloper.Core.GraphicalUIs.Controls
{
	partial class __ { } // デザイナ避け
	partial class OsdevTextBox
	{
		#region テキスト
		/// <summary>
		///  このテキストボックスに格納されている文字列を取得または設定します。
		/// </summary>
		public override string Text
		{
			get
			{
				return base.Text;
			}

			set
			{
				this.SetText(value);
			}
		}

		/// <summary>
		///  このテキストボックスに格納されているテキスト行を取得または設定します。
		///  配列の値には改行コード(LFやCR等)を含む場合は無視されます。
		/// </summary>
		[Browsable(true)]
		[Category(nameof(CategoryAttribute.Appearance))]
		[Description("このテキストボックスに格納されているテキスト行を表します。")]
		public virtual string[] Lines
		{
			get
			{
				return _lines;
			}

			set
			{
				// 折角分割されているけど、文字列設定処理は一つにしたいので結合
				this.SetText(string.Join("\n", value));
			}
		}
		private string[] _lines;
		private int _line;

		/// <summary>
		///  このテキストボックスに表示される文字列を設定します。
		/// </summary>
		/// <param name="text">表示する文字列です。</param>
		public void SetText(string text)
		{
			text = text.CRtoLF();
			_lines = text.Split('\n');
			base.Text = text;
			vScrollBar.Maximum = _lines.Length;
			this.Invalidate();
		}

		/// <summary>
		///  指定された場所に指定された字を追加します。
		/// </summary>
		/// <param name="pos">字を追加する場所です。</param>
		/// <param name="c">追加する字です。</param>
		/// <exception cref="System.ArgumentOutOfRangeException" />
		public void AddCharTo(Point pos, char c)
		{
			if (pos.X >= _lines.Length) {
				throw ErrorGen.ArgOutOfRange(pos.X, 0, _lines.Length - 1);
			}
			string l = _lines[pos.X];
			if (pos.Y >= l.Length) {
				throw ErrorGen.ArgOutOfRange(pos.Y, 0, l.Length - 1);
			}
			string start = l.Substring(0, pos.Y);
			string end   = l.Substring(pos.Y, l.Length - pos.Y);
			l = start + c + end;
			_lines[pos.X] = l;
			this.Invalidate();
			this.OnTextChanged(new EventArgs());
		}

		/// <summary>
		///  指定された場所の字を削除します。
		/// </summary>
		/// <param name="pos">削除する字の場所です。</param>
		/// <exception cref="System.ArgumentOutOfRangeException" />
		public void RemoveCharFrom(Point pos)
		{
			if (pos.X >= _lines.Length) {
				throw ErrorGen.ArgOutOfRange(pos.X, 0, _lines.Length - 1);
			}
			string l = _lines[pos.X];
			if (pos.Y >= l.Length) {
				throw ErrorGen.ArgOutOfRange(pos.Y, 0, l.Length - 1);
			}
			l.Remove(pos.Y);
			this.Invalidate();
			this.OnTextChanged(new EventArgs());
		}
		#endregion

		#region フォント
		/// <summary>
		///  このテキストボックスに表示されるテキストデータのフォントを取得します。
		///  このプロパティを変更しても反映されません。
		/// </summary>
		public sealed override Font Font
		{
			get
			{
				return _font;
			}

			set
			{
				// 変更不可能。
				// overrideしている為、setterも実装する必要がある。
			}
		}
		private Font _font;

		/// <summary>
		///  <see cref="OSDeveloper.Core.GraphicalUIs.Controls.OsdevTextBox.Font"/>プロパティを限定値にリセットします。
		/// </summary>
		public sealed override void ResetFont()
		{
			_font?.Dispose();
			if (this.IsDesignMode()) {
				_font = new Font("MS Gothic", 16, FontStyle.Regular, GraphicsUnit.Pixel);
			} else {
				_font = FontResources.CreateGothic();
			}
			base.Font = _font;
		}
		#endregion

		#region カーソル
		/// <summary>
		///  限定のカーソルを取得します。
		/// </summary>
		protected sealed override Cursor DefaultCursor
		{
			get
			{
				return Cursors.IBeam;
			}
		}

		/// <summary>
		///  <see cref="System.Windows.Forms.Control.Cursor"/>プロパティを限定値にリセットします。
		/// </summary>
		public sealed override void ResetCursor()
		{
			this.Cursor = this.DefaultCursor;
		}
		#endregion

		#region RTL言語
		/// <summary>
		///  このコントロールでは文字列を右から左に表示する事はできません。
		/// </summary>
		public sealed override RightToLeft RightToLeft
		{
			get
			{
				return RightToLeft.No;
			}

			set
			{
				// 変更不可能。
				// overrideしている為、setterも実装する必要がある。
			}
		}

		/// <summary>
		///  <see cref="OSDeveloper.Core.GraphicalUIs.Controls.OsdevTextBox.RightToLeft"/>プロパティを限定値にリセットします。
		/// </summary>
		public sealed override void ResetRightToLeft()
		{
			base.RightToLeft = RightToLeft.No;
		}
		#endregion

		#region 文字色
		/// <summary>
		///  <see cref="System.Windows.Forms.Control.BackColor"/>プロパティを限定値にリセットします。
		/// </summary>
		public override void ResetBackColor()
		{
			this.BackColor = Color.Black;
		}

		/// <summary>
		///  <see cref="System.Windows.Forms.Control.BackColor"/>プロパティを限定値にリセットします。
		/// </summary>
		public override void ResetForeColor()
		{
			this.ForeColor = Color.White;
		}
		#endregion

		#region カラーテーマ
		/// <summary>
		///  画面上に表示されるグリッドの色を取得または設定します。
		/// </summary>
		[Browsable(false)]
		public OsdevColorTheme GridColor
		{
			get
			{
				return _grid_col;
			}

			set
			{
				_grid_col = value;
				this.OnGridColorChanged(new EventArgs());
			}
		}
		private OsdevColorTheme _grid_col;

		/// <summary>
		///  <see cref="OSDeveloper.Core.GraphicalUIs.Controls.OsdevTextBox.GridColor"/>プロパティを限定値にリセットします。
		/// </summary>
		public virtual void ResetGridColor()
		{
			this.GridColor = OsdevColorThemes.Salmon;
		}
		#endregion

		#region コマンドタブ

		/// <summary>
		///  このテキストボックスで利用されているコマンドタブを取得します。
		/// </summary>
		public OsdevTextBoxTab CommandTab { get; }

		#endregion
	}
}
