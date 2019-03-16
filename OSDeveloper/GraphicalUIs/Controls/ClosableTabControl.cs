using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using OSDeveloper.IO.Logging;

namespace OSDeveloper.GraphicalUIs.Controls
{
	public partial class ClosableTabControl : TabControl
	{
		private readonly Logger _logger;
		private int _closebtn_clicked;

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
			_closebtn_clicked = -1;

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
			Rectangle pageRect = new Rectangle(
			page.Bounds.X      - 2,
			page.Bounds.Y      - 2,
			page.Bounds.Width  + 5,
			page.Bounds.Height + 5);
			TabRenderer.DrawTabPage(e.Graphics, pageRect);
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
				} else {
					state = TabItemState.Normal;
				}

				if (this.SelectedIndex == i) {
					// 選択中のタブを強調表示
					tabRect.Height += 1;
				}

				// タブ描画
				TabRenderer.DrawTabItem(e.Graphics,
					new Rectangle(tabRect.X, tabRect.Y, tabRect.Width, tabRect.Height + 1),
					page.Text,
					page.Font,
					TextFormatFlags.EndEllipsis,
					false,
					state);

				// 閉じるボタン描画
				int x1 = tabRect.X + tabRect.Width - 17;
				int y1 = tabRect.Y;
				int x2 = tabRect.X + tabRect.Width - 1;
				int y2 = tabRect.Y + 16;
				if (_closebtn_clicked == i) { // 左クリック中
					e.Graphics.FillRectangle(SystemBrushes.ControlDark, x1, y1, 16, 16);
				}
				e.Graphics.DrawLine(SystemPens.ControlDarkDark, x1, y1, x2, y2);
				e.Graphics.DrawLine(SystemPens.ControlDarkDark, x1, y2, x2, y1);
				e.Graphics.DrawRectangle(SystemPens.ControlDarkDark, x1, y1, 16, 16);
			}

end:
			this.ResumeLayout();
			_logger.Trace($"completed {nameof(OnPaint)}");
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
	}
}
