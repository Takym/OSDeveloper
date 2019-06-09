﻿using System;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using OSDeveloper.IO.Configuration;
using OSDeveloper.IO.Logging;

namespace OSDeveloper.GUIs.Controls.SettingPanels.Configuration
{
	public partial class StartupSettings : UserControl
	{
		private readonly Logger       _logger;
		private readonly FormSettings _parent;

		public StartupSettings(FormSettings parent)
		{
			_logger = Logger.Get(nameof(StartupSettings));
			_parent = parent;
			this.InitializeComponent();
			_logger.Trace($"constructed {nameof(StartupSettings)}");
		}

		private void StartupSettings_Load(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(StartupSettings_Load)}...");

			switch (SettingManager.System.VisualStyle) {
				case VisualStyleState.ClientAndNonClientAreasEnabled:
					visualstyle.CheckState = CheckState.Checked;
					break;
				case VisualStyleState.NonClientAreaEnabled:
					visualstyle.CheckState = CheckState.Unchecked;
					break;
				default:
					visualstyle.CheckState = CheckState.Indeterminate;
					break;
			}

			var clist = CultureInfo.GetCultures(CultureTypes.AllCultures);
			for (int i = 0; i < clist.Length; ++i) {
				var l = new Locale(clist[i]);
				cmbxLang.Items.Add(l);
				if (clist[i].Name == SettingManager.System.Language.Name) {
					cmbxLang.SelectedItem = l;
				}
			}

			_logger.Trace($"completed {nameof(StartupSettings_Load)}");
		}

		private void visualstyle_CheckedChanged(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(visualstyle_CheckedChanged)}...");

			if (visualstyle.Checked) {
				SettingManager.System.VisualStyle = VisualStyleState.ClientAndNonClientAreasEnabled;
			} else {
				SettingManager.System.VisualStyle = VisualStyleState.NoneEnabled;
			}

			_logger.Trace($"completed {nameof(visualstyle_CheckedChanged)}");
		}

		private void cmbxLang_SelectedIndexChanged(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(cmbxLang_SelectedIndexChanged)}...");

			if (cmbxLang.SelectedItem is Locale l) {
				SettingManager.System.Language = l.Culture;
			}

			_logger.Trace($"completed {nameof(cmbxLang_SelectedIndexChanged)}");
		}

		private class Locale
		{
			public CultureInfo Culture { get; }

			public Locale(CultureInfo culture)
			{
				this.Culture = culture;
			}

			public override string ToString()
			{
				return $"{this.Culture.NativeName}/{this.Culture.DisplayName}" +
					$"/{this.Culture.EnglishName}/{this.Culture.Name}({this.Culture.LCID})";
			}
		}
	}
}
