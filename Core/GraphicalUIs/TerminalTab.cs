﻿using System.Drawing;
using System.Windows.Forms;
using OSDeveloper.Assets;
using OSDeveloper.Core.Logging;

namespace OSDeveloper.Core.GraphicalUIs
{
	/// <summary>
	///  ターミナルタブを表します。
	///  ターミナルタブとは画面下部に表示される複数のコンソール出力、エラー一覧、タスク一覧等を
	///  管理しタブ形式で表示するコントロールです。このコントロールはデザイナから利用する事はできません。
	/// </summary>
	public partial class TerminalTab : TabControl
	{
		private Logger _logger;
		private int _closebtn_clicked;

		/// <summary>
		///  型'<see cref="OSDeveloper.Core.GraphicalUIs.TerminalTab"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		public TerminalTab()
		{
			_logger = Logger.GetSystemLogger(nameof(TerminalTab));
			this.InitializeComponent();
			_closebtn_clicked = -1;
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
			_logger.Trace($"executing {nameof(OnDrawItem)}...");
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
			using (var font = FontResources.CreateTabFont()) {
				e.Graphics.DrawString(caption, font, SystemBrushes.ControlText, e.Bounds.Left + 4, e.Bounds.Top + 4);
			}

			// 閉じるボタン描画
			var closebtn = new Rectangle(e.Bounds.Right - 16, e.Bounds.Top + 4, 12, 12);
			// 背景と枠を描画
			if (e.Index == _closebtn_clicked) {
				// 押されているなら、明るい色で描画
				e.Graphics.FillRectangle(SystemBrushes.ControlDark, closebtn);
			} else {
				// 押されてないなら、普通の色で描画
				e.Graphics.FillRectangle(SystemBrushes.ButtonFace, closebtn);
			}
			e.Graphics.DrawRectangle(SystemPens.ButtonShadow, closebtn);
			// 罰印描画
			e.Graphics.DrawLine(
				SystemPens.ControlText,
				closebtn.Left + 4, closebtn.Top + 4, closebtn.Right - 4, closebtn.Bottom - 4);
			e.Graphics.DrawLine(
				SystemPens.ControlText,
				closebtn.Right - 4, closebtn.Top + 4, closebtn.Left + 4, closebtn.Bottom - 4);

			// 後片付け
			e.DrawFocusRectangle();
			_logger.Trace($"drawed the tab-item {caption}");
			this.ResumeLayout();
			_logger.Trace($"completed {nameof(OnDrawItem)}");
		}

		/// <summary>
		///  <see cref="System.Windows.Forms.Control.MouseDown"/>を発生させます。
		/// </summary>
		/// <param name="e">
		///  イベントデータを格納している<see cref="System.Windows.Forms.MouseEventArgs"/>オブジェクトです。
		/// </param>
		protected override void OnMouseDown(MouseEventArgs e)
		{
			_logger.Trace($"executing {nameof(OnMouseDown)}...");
			base.OnMouseDown(e);

			if (e.Button.HasFlag(MouseButtons.Left)) {
				var bounds = this.GetTabRect(this.SelectedIndex);
				var closebtn = new Rectangle(bounds.Right - 16, bounds.Top + 4, 12, 12);
				if (closebtn.Contains(e.Location)) {
					_closebtn_clicked = this.SelectedIndex;
					this.Invalidate();
					_logger.Info($"the close button of {_closebtn_clicked}:{this.SelectedTab.Text} clicked");
				}
			}

			_logger.Trace($"completed {nameof(OnMouseDown)}");
		}

		/// <summary>
		///  <see cref="System.Windows.Forms.Control.MouseUp"/>を発生させます。
		/// </summary>
		/// <param name="e">
		///  イベントデータを格納している<see cref="System.Windows.Forms.MouseEventArgs"/>オブジェクトです。
		/// </param>
		protected override void OnMouseUp(MouseEventArgs e)
		{
			_logger.Trace($"executing {nameof(OnMouseUp)}...");
			base.OnMouseUp(e);

			if (e.Button.HasFlag(MouseButtons.Left)) {
				var bounds = this.GetTabRect(this.SelectedIndex);
				var closebtn = new Rectangle(bounds.Right - 16, bounds.Top + 4, 12, 12);
				if (closebtn.Contains(e.Location) && _closebtn_clicked == this.SelectedIndex) {
					_logger.Info($"closing {_closebtn_clicked}:{this.SelectedTab.Text}...");
					this.TabPages.Remove(this.SelectedTab);
				} else {
					_logger.Info($"cancelled to close {_closebtn_clicked}:{this.SelectedTab.Text}");
				}
				this.Invalidate();
				_closebtn_clicked = -1;
			}

			_logger.Trace($"completed {nameof(OnMouseUp)}");
		}
	}
}
