using System.Drawing;
using System.Windows.Forms;

namespace OSDeveloper.App
{
	public class ToolStripRendererEx : ToolStripProfessionalRenderer
	{
		public ToolStripRendererEx(ToolStripColorTable colorTable) : base(colorTable)
		{
			this.RoundedEdges = false;
		}
	}

	public class ToolStripColorTable : ProfessionalColorTable
	{
		public ToolStripColorTable()
		{
			this.UseSystemColors = true;
		}

		#region メインメニュー 背景
		public override Color MenuStripGradientBegin
		{
			get
			{
				return Color.FromArgb(0xA7, 0x9C, 0xB5);
			}
		}

		public override Color MenuStripGradientEnd
		{
			get
			{
				return Color.FromArgb(0xB5, 0x9C, 0xA7);
			}
		}
		#endregion

		#region ツールメニュー 背景
		public override Color ToolStripGradientBegin
		{
			get
			{
				return Color.FromArgb(0xA7, 0x9C, 0xB5);
			}
		}

		public override Color ToolStripGradientMiddle
		{
			get
			{
				return Color.FromArgb(0xAE, 0x9C, 0xAE);
			}
		}

		public override Color ToolStripGradientEnd
		{
			get
			{
				return Color.FromArgb(0xB5, 0x9C, 0xA7);
			}
		}
		#endregion
	}
}
