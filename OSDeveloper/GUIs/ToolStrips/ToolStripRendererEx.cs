using System.Drawing;
using System.Windows.Forms;
using OSDeveloper.GUIs.Design;

namespace OSDeveloper.GUIs.ToolStrips
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
		public ColorTheme ColorTheme { get; set; }

		public ToolStripColorTable()
		{
			this.UseSystemColors = true;
			this.ColorTheme = ColorThemes.Purple;
		}

		#region メインメニュー 背景
		public override Color MenuStripGradientBegin
		{
			get
			{
				return this.ColorTheme.Light;
			}
		}

		public override Color MenuStripGradientEnd
		{
			get
			{
				return this.ColorTheme.Dark;
			}
		}
		#endregion

		#region ツールメニュー 背景
		public override Color ToolStripGradientBegin
		{
			get
			{
				return this.ColorTheme.Light;
			}
		}

		public override Color ToolStripGradientMiddle
		{
			get
			{
				return this.ColorTheme.Normal;
			}
		}

		public override Color ToolStripGradientEnd
		{
			get
			{
				return this.ColorTheme.Dark;
			}
		}
		#endregion
	}
}
