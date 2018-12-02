using System;
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

			// スクロールバー設定
			hScrollBar.Cursor = Cursors.Arrow;
			vScrollBar.Cursor = Cursors.Arrow;
			this.Controls.Add(hScrollBar);
			this.Controls.Add(vScrollBar);

			_logger.Trace(nameof(OsdevTextBox) + " is constructed");
		}

		/// <summary>
		///  <see cref="System.Windows.Forms.Control.TextChanged"/>イベントを発生させます。
		/// </summary>
		/// <param name="e">
		///  イベントデータを格納している<see cref="System.EventArgs"/>オブジェクトです。
		/// </param>
		protected override void OnTextChanged(EventArgs e)
		{
			_logger.Trace($"executing {nameof(OnTextChanged)}...");

			base.OnTextChanged(e);
			this.Invalidate();

			_logger.Trace($"completed {nameof(OnTextChanged)}");
		}

		private void hScrollBar_Scroll(object sender, ScrollEventArgs e)
		{
			_logger.Trace($"executing {nameof(hScrollBar_Scroll)}...");

			_logger.Trace($"completed {nameof(hScrollBar_Scroll)}");
		}

		private void vScrollBar_Scroll(object sender, ScrollEventArgs e)
		{
			_logger.Trace($"executing {nameof(vScrollBar_Scroll)}...");

			_line = e.NewValue;
			this.Invalidate();

			_logger.Trace($"completed {nameof(vScrollBar_Scroll)}");
		}

#pragma warning disable CS0809 // 旧形式のメンバーが、旧形式でないメンバーをオーバーライドします
		/// <summary>
		///  背景描画イベントを無効化します。
		/// </summary>
		/// <param name="e">利用されていません。互換性の為に存在しています。</param>
		[Obsolete("このクラスでは背景描画イベントは利用されていません。")]
		protected sealed override void OnPaintBackground(PaintEventArgs e)
		{
			// 処理速度向上のため背景描画停止。
		}
#pragma warning restore CS0809 // 旧形式のメンバーが、旧形式でないメンバーをオーバーライドします
	}
}
