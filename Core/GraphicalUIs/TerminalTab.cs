using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OSDeveloper.Assets;
using OSDeveloper.Core.Logging;
using OSDeveloper.Core.MiscUtils;

namespace OSDeveloper.Core.GraphicalUIs
{
	/// <summary>
	///  ターミナルタブを表します。
	///  ターミナルタブとは画面下部に表示される複数のコンソール出力、エラー一覧、タスク一覧等を
	///  管理しタブ形式で表示するコントロールです。
	/// </summary>
	public partial class TerminalTab : TabControl
	{
		private Logger _logger;

		/// <summary>
		///  型'<see cref="OSDeveloper.Core.GraphicalUIs.TerminalTab"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		public TerminalTab()
		{
			_logger = Logger.GetSystemLogger(nameof(TerminalTab));
			this.InitializeComponent();
			_logger.Trace(nameof(TerminalTab) + " is constructed");
		}

		/// <summary>
		///  <see cref="System.Windows.Forms.TabControl.DrawItem"/>を発生させます。
		/// </summary>
		/// <param name="e">
		///  イベントデータを格納している<see cref="System.Windows.Forms.DrawItemEventArgs"/>オブジェクトです。
		/// </param>
		protected override void OnDrawItem(DrawItemEventArgs e)
		{
			this.SuspendLayout();
			base.OnDrawItem(e);

			// タブの情報をログに書き込む
			string caption = this.TabPages[e.Index].Text;
			_logger.Trace($"drawing the tab-item {caption}...");
			_logger.Debug($"the bounds of the tab-item is {e.Bounds}");
			_logger.Debug($"the status of the tab-item is {e.State}");

			// タブの見出しを表示
			Brush back1, back2;
			Rectangle back2_r;
			if (e.State.HasFlag(DrawItemState.Selected)) {
				// 選択されている場合は明るい色で表示
				back1 = SystemBrushes.ControlLightLight;
				back2 = Brushes.Aqua;
				back2_r = new Rectangle(e.Bounds.X, e.Bounds.Y + 18, e.Bounds.Width, e.Bounds.Height - 24);
			} else {
				// 選択されていない場合は暗い色で表示
				back1 = SystemBrushes.Control;
				back2 = Brushes.DeepSkyBlue;
				back2_r = new Rectangle(e.Bounds.X, e.Bounds.Y + 18, e.Bounds.Width, e.Bounds.Height - 18);
			}
			e.Graphics.FillRectangle(back1, e.Bounds);
			e.Graphics.FillRectangle(back2, back2_r);
			// 非デザイン時のみ、独自のフォント(現在は、IPAexゴシック)で表示
			if (!this.IsDesignMode()) {
				using (var font = FontResources.CreateTabFont()) {
					e.Graphics.DrawString(caption, font, SystemBrushes.ControlText, e.Bounds.Left + 4, e.Bounds.Top + 4);
				}
			} else {
				e.Graphics.DrawString(caption, e.Font, SystemBrushes.ControlText, e.Bounds.Left + 4, e.Bounds.Top + 4);
			}

			// 選択されている場合は、閉じるボタンを表示
			if (e.State.HasFlag(DrawItemState.Selected)) {
				var closebtn = new Rectangle(e.Bounds.Right - 16, e.Bounds.Top + 4, 12, 12);
				e.Graphics.FillRectangle(SystemBrushes.ButtonFace, closebtn);
				e.Graphics.DrawRectangle(SystemPens.ButtonShadow, closebtn);
				e.Graphics.DrawLine(
					SystemPens.ControlText,
					closebtn.Left + 4, closebtn.Top + 4, closebtn.Right - 4, closebtn.Bottom - 4);
				e.Graphics.DrawLine(
					SystemPens.ControlText,
					closebtn.Right - 4, closebtn.Top + 4, closebtn.Left + 4, closebtn.Bottom - 4);
			}

			// 後片付け
			e.DrawFocusRectangle();
			_logger.Trace($"drawed the tab-item {caption}");
			this.ResumeLayout();
		}
	}
}
