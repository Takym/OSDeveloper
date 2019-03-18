using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using OSDeveloper.IO.Logging;

namespace OSDeveloper.GraphicalUIs.Controls
{
	public partial class ClosableTabControl : TabControl
	{
		private readonly Logger _logger;
		private int _closebtn_clicked, _closebtn_over, _mouse_over;

		protected List<TabPage> AllTabPages { get; }

		public ClosableTabControl()
		{
			_logger = Logger.Get(nameof(MdiChildrenTab));

			this.InitializeComponent();
			this.SetStyle(
				ControlStyles.UserPaint |
				ControlStyles.Opaque |
				ControlStyles.ResizeRedraw |
				ControlStyles.Selectable |
				ControlStyles.AllPaintingInWmPaint |
				ControlStyles.OptimizedDoubleBuffer,
				true);
			this.Appearance   = TabAppearance.Normal;
			this.Alignment    = TabAlignment.Top;
			this.DrawMode     = TabDrawMode.OwnerDrawFixed;
			this.SizeMode     = TabSizeMode.Fixed;
			this.ItemSize     = new Size(120, 20);
			this.Multiline    = false;
			this.AllTabPages  = new List<TabPage>();
			_closebtn_clicked = -1;
			_closebtn_over    = -1;
			_mouse_over       = -1;

			_logger.Trace($"constructed {nameof(MdiChildrenTab)}");
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			_logger.Trace($"executing {nameof(OnPaint)}...");
			this.SuspendLayout();
			base.OnPaint(e);

			// 参考: https://dobon.net/vb/dotnet/control/tabsidebug.html#paint

			// 背景描画
			e.Graphics.Clear(this.BackColor);
			if (this.TabPages.Count == 0) goto end;

			// タブページの枠描画
			if (this.SelectedIndex < 0) goto skip;
			var page = this.TabPages[this.SelectedIndex];
			if (Application.VisualStyleState == VisualStyleState.ClientAreaEnabled ||
				Application.VisualStyleState == VisualStyleState.ClientAndNonClientAreasEnabled) {
				var pageRect = new Rectangle(
					page.Bounds.X      - 2,
					page.Bounds.Y      - 2,
					page.Bounds.Width  + 5,
					page.Bounds.Height + 5);
				TabRenderer.DrawTabPage(e.Graphics, pageRect);
			}
skip:

			//タブの描画
			for (int i = 0; i < this.TabPages.Count; ++i) {
				var tabRect = this.GetTabRect(i);
				page = this.TabPages[i];

				// タブの状態取得
				TabItemState state;
				if (!this.Enabled) {
					state = TabItemState.Disabled;
				} else if (this.SelectedIndex == i) {
					state = TabItemState.Selected;
				} else if (_mouse_over == i) {
					state = TabItemState.Hot;
				} else {
					state = TabItemState.Normal;
				}

				if (this.SelectedIndex == i) {
					// 選択中のタブを強調表示
					tabRect.Height += 1;
				}

				// タブ描画
				if (Application.VisualStyleState == VisualStyleState.ClientAreaEnabled ||
					Application.VisualStyleState == VisualStyleState.ClientAndNonClientAreasEnabled) {
					TabRenderer.DrawTabItem(e.Graphics,
						new Rectangle(tabRect.X, tabRect.Y, tabRect.Width, tabRect.Height + 1),
						page.Text,
						page.Font,
						TextFormatFlags.EndEllipsis,
						state == TabItemState.Hot,
						state);
				} else {
					var rect = new Rectangle(tabRect.X, tabRect.Y, tabRect.Width, tabRect.Height + 1);
					switch (state) {
						case TabItemState.Selected:
							e.Graphics.FillRectangle(SystemBrushes.ControlLightLight, rect);
							e.Graphics.DrawRectangle(SystemPens.ControlDarkDark,      rect);
							break;
						case TabItemState.Disabled:
							e.Graphics.FillRectangle(SystemBrushes.ControlDark,  rect);
							e.Graphics.DrawRectangle(SystemPens.ControlDarkDark, rect);
							break;
						case TabItemState.Hot:
							e.Graphics.FillRectangle(SystemBrushes.ControlLight, rect);
							e.Graphics.DrawRectangle(SystemPens.ControlDarkDark, rect);
							break;
						default: //case TabItemState.Normal:
							e.Graphics.FillRectangle(SystemBrushes.Control,      rect);
							e.Graphics.DrawRectangle(SystemPens.ControlDarkDark, rect);
							break;
					}
					e.Graphics.DrawString(
						page.Text,
						page.Font,
						Brushes.Black,
						new RectangleF(tabRect.X + 2, tabRect.Y + 2, tabRect.Width - 2, tabRect.Height - 1));
				}

				// 閉じるボタン描画
				int x1 = tabRect.X + tabRect.Width - 17;
				int y1 = tabRect.Y;
				int x2 = tabRect.X + tabRect.Width - 1;
				int y2 = tabRect.Y + 16;
				if (_closebtn_clicked == i) { // 左クリック中
					e.Graphics.FillRectangle(SystemBrushes.ControlDark, x1, y1, 16, 16);
				} else if (_closebtn_over == i) { // カーソルが重なった
					e.Graphics.FillRectangle(SystemBrushes.MenuHighlight, x1, y1, 16, 16);
				} else { // 通常状態
					e.Graphics.FillRectangle(SystemBrushes.Control, x1, y1, 16, 16);
				}
				e.Graphics.DrawLine(SystemPens.ControlDarkDark, x1, y1, x2, y2);
				e.Graphics.DrawLine(SystemPens.ControlDarkDark, x1, y2, x2, y1);
				e.Graphics.DrawRectangle(SystemPens.ControlDarkDark, x1, y1, 16, 16);
			}

end:
			this.ResumeLayout();
			_logger.Trace($"completed {nameof(OnPaint)}");
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			_logger.Trace($"executing {nameof(OnMouseMove)}...");
			base.OnMouseMove(e);

			_closebtn_over = -1;
			_mouse_over    = -1;
			if (this.TabPages.Count == 0) goto end;
			for (int i = 0; i < this.TabPages.Count; ++i) {
				var rect = this.GetTabRect(i);
				int x1 = rect.X;
				int y1 = rect.Y;
				int x2 = rect.X + rect.Width;
				int y2 = rect.Y + rect.Height;
				if (x1 <= e.X && e.X < x2 && y1 <= e.Y && e.Y < y2) {
					_mouse_over = i;
					x1 += rect.Width  - 17;
					x2 -= 1;
					y2 -= rect.Height - 16;
					if (x1 <= e.X && e.X < x2 && y1 <= e.Y && e.Y < y2) {
						_closebtn_over = i;
					}
					goto end;
				}
			}

end:
			this.Invalidate();
			_logger.Trace($"completed {nameof(OnMouseMove)}");
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			_logger.Trace($"executing {nameof(OnMouseLeave)}...");
			base.OnMouseLeave(e);
			_closebtn_over = -1;
			_mouse_over    = -1;
			this.Invalidate();
			_logger.Trace($"completed {nameof(OnMouseLeave)}");
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			_logger.Trace($"executing {nameof(OnMouseDown)}...");
			base.OnMouseDown(e);

			if (this.TabPages.Count == 0) goto end;
			if (e.Button.HasFlag(MouseButtons.Left)) {
				for (int i = 0; i < this.TabPages.Count; ++i) {
					var rect = this.GetTabRect(i);
					int x1 = rect.X + rect.Width - 17;
					int y1 = rect.Y;
					int x2 = rect.X + rect.Width - 1;
					int y2 = rect.Y + 16;
					if (x1 <= e.X && e.X < x2 && y1 <= e.Y && e.Y < y2) {
						_closebtn_clicked = i; // クリックされていてもまだ閉じない
						goto end;
					}
				}
			}

end:
			this.Invalidate();
			_logger.Trace($"completed {nameof(OnMouseDown)}");
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			_logger.Trace($"executing {nameof(OnMouseUp)}...");
			base.OnMouseUp(e);

			if (this.TabPages.Count == 0) goto end;
			if (e.Button.HasFlag(MouseButtons.Left)) {
				for (int i = 0; i < this.TabPages.Count; ++i) {
					var rect = this.GetTabRect(i);
					int x1 = rect.X + rect.Width - 17;
					int y1 = rect.Y;
					int x2 = rect.X + rect.Width - 1;
					int y2 = rect.Y + 16;
					if (x1 <= e.X && e.X < x2 && y1 <= e.Y && e.Y < y2 &&
						_closebtn_clicked == i) {
						this.TabPages.RemoveAt(i); // タブをコントロールから削除する。Dispose はしない
						goto end;
					}
				}
			}

end:
			_closebtn_clicked = -1;
			this.Invalidate();
			_logger.Trace($"completed {nameof(OnMouseUp)}");
		}

		protected override void OnControlAdded(ControlEventArgs e)
		{
			_logger.Trace($"executing {nameof(OnControlAdded)}...");
			base.OnControlAdded(e);

			if (e.Control is TabPage page && !this.AllTabPages.Contains(page)) {
				this.AllTabPages.Add(page);
			}

			_logger.Trace($"completed {nameof(OnControlAdded)}");
		}
	}
}
