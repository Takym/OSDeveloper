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
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			this.SuspendLayout();
			base.OnPaint(e);

			using (Font f = new Font("MS Gothic", 12, FontStyle.Regular, GraphicsUnit.Point)){
				string[] lines = this.Text.CRtoLF().Split('\n');
				for (int i = 0; i < lines.Length; ++i) {
					int y = i * f.Height;
					e.Graphics.DrawString(lines[i], f, Brushes.Black, new Point(0, y));
				}
			}

			this.ResumeLayout(false);
		}
	}
}
