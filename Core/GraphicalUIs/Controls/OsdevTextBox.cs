using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using OSDeveloper.Core.Logging;

namespace OSDeveloper.Core.GraphicalUIs.Controls
{
	/// <summary>
	///  <see langword="OSDeveloper"/>のテキストエディタで利用される専用のテキストボックスです。
	/// </summary>
	[Docking(DockingBehavior.Ask)]
	[DefaultProperty(nameof(Text))]
	[DefaultEvent(nameof(TextChanged))]
	public partial class OsdevTextBox : Control
	{
		private Logger _logger;

		/// <summary>
		///  型'<see cref="OSDeveloper.Core.GraphicalUIs.Controls.OsdevTextBox"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		public OsdevTextBox()
		{
			_logger = Logger.GetSystemLogger(nameof(OsdevTextBox));

			// コントロールの基本設定
			this.InitializeComponent();
			this.SetStyle(
				ControlStyles.UserPaint |
				ControlStyles.Opaque |
				ControlStyles.ResizeRedraw |
				ControlStyles.Selectable |
				ControlStyles.UserMouse |
				ControlStyles.AllPaintingInWmPaint |
				ControlStyles.OptimizedDoubleBuffer,
				true);
			this.ResetFont();
			this.ResetBackColor();
			this.ResetForeColor();
			this.ResetCursor();
			this.ResetRightToLeft();
			this.ResetGridColor();
			this.ResetSelectionColor();

			// スクロールバー設定
			vScrollBar.Cursor = Cursors.Arrow;
			hScrollBar.Cursor = Cursors.Arrow;
			this.Controls.Add(vScrollBar);
			this.Controls.Add(hScrollBar);

			// コマンドタブ設定
			this.CommandTab = new OsdevTextBoxTab(this);

			// 文字コードリスト初期化
			_text = new List<uint>();

			_logger.Trace(nameof(OsdevTextBox) + " is constructed");
		}

		#region デザイナで生成されたイベント
		private void vScrollBar_Scroll(object sender, ScrollEventArgs e)
		{
			_logger.Trace($"executing {nameof(vScrollBar_Scroll)}...");

			_row_sb = e.NewValue;
			this.Invalidate();

			_logger.Trace($"completed {nameof(vScrollBar_Scroll)}");
		}

		private void hScrollBar_Scroll(object sender, ScrollEventArgs e)
		{
			_logger.Trace($"executing {nameof(hScrollBar_Scroll)}...");

			_col_sb = e.NewValue;
			this.Invalidate();

			_logger.Trace($"completed {nameof(hScrollBar_Scroll)}");
		}
		#endregion
	}
}
