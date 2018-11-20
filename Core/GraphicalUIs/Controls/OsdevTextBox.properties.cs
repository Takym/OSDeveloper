using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using OSDeveloper.Assets;
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
				_lines = value.CRtoLF().Split('\n');
				base.Text = value;
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
				_lines = value;
				base.Text = string.Join("\r\n", value);
			}
		}
		private string[] _lines;
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
	}
}
