﻿using OSDeveloper.Resources;

namespace OSDeveloper.GUIs.ToolStrips
{
	public partial class HelpMainMenuItem : MainMenuItem
	{
		public HelpMainMenuItem(FormMain mwnd) : base(mwnd)
		{
			this.Name = "HELP";
			this.Text = MenuTexts.Help;

			_logger.Trace($"constructed {nameof(HelpMainMenuItem)}");
		}
	}
}
