using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using OSDeveloper.Core.MiscUtils;

namespace OSDeveloper.Core.GraphicalUIs.Controls
{
	/// <summary>
	///  <see langword="OSDeveloper"/>専用のテキストエディタです。
	/// </summary>
	[Docking(DockingBehavior.Ask)]
	[DefaultProperty(nameof(Text))]
	[DefaultEvent(nameof(TextChanged))]
	public partial class OsdevTextBox : Control
	{
		/// <summary>
		///  型'<see cref="OSDeveloper.Core.GraphicalUIs.Controls.OsdevTextBox"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		public OsdevTextBox()
		{
			InitializeComponent();
			this.SetStyle(
				ControlStyles.UserPaint |
				ControlStyles.Opaque |
				ControlStyles.ResizeRedraw |
				ControlStyles.Selectable |
				ControlStyles.AllPaintingInWmPaint |
				ControlStyles.OptimizedDoubleBuffer,
				true);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			this.SuspendLayout();
			base.OnPaint(e);

			e.Graphics.Clear(Color.Black);
			using (Font f = new Font("MS Gothic", 12, FontStyle.Regular, GraphicsUnit.Point)) {
				string[] lines = this.Text.CRtoLF().Split('\n');
				{
					int x = f.Height * 3;
					e.Graphics.DrawLine(Pens.Salmon, x, 0, x, this.Height);
					for (int i = 0; i < this.Width; ++i) {
						x = i * f.Height / 2;
						if (i % 2 == 0) {
							e.Graphics.DrawLine(Pens.LightSalmon, x, 0, x, f.Height);
						} else {
							e.Graphics.DrawLine(Pens.DarkSalmon, x, 0, x, f.Height / 2);
						}
					}
				}
				for (int i = 0; i < lines.Length; ++i) {
					int y = (i + 1) * f.Height;
					e.Graphics.DrawString($"{i + 1:D5}", f, Brushes.Salmon, new Point(0, y));
					e.Graphics.DrawString(lines[i], f, Brushes.White, new Point(f.Height * 3, y));
				}
			}

			this.ResumeLayout(false);
		}

		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			base.OnKeyPress(e);

			this.Text += e.KeyChar;
			e.Handled = true;
			this.Invalidate();
		}

		protected override void OnPaintBackground(PaintEventArgs pevent)
		{
			//base.OnPaintBackground(pevent);
		}
	}
}
