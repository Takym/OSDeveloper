using System.Windows.Forms;
using OSDeveloper.GUIs.ToolStrips;
using OSDeveloper.Resources;

namespace OSDeveloper
{
	partial class FormMain
	{
		// フォームに直接設置するメニューコントロール
		private readonly MenuStrip   _main_menu;
		private readonly ToolStrip   _tool_menu;
		private readonly StatusStrip _status_bar;

		private void BuildMenus()
		{
			#region _main_menu
			_logger.Trace($"setting {nameof(_main_menu)}...");
			_main_menu.Name = nameof(_main_menu);
			_main_menu.Dock = DockStyle.Top;
			#endregion

			#region _tool_menu
			_logger.Trace($"setting {nameof(_tool_menu)}...");
			_tool_menu.Name = nameof(_tool_menu);
			_tool_menu.Dock = DockStyle.Top;
			#endregion

			#region _status_bar
			_logger.Trace($"setting {nameof(_status_bar)}...");
			_status_bar.Name = nameof(_status_bar);
			_status_bar.Text = FormMainRes.Status_Preparing;
			_status_bar.Dock = DockStyle.Bottom;
			#endregion
		}

		#region メインメニュー
		private FileMainMenuItem _menu_file;
		private EditMainMenuItem _menu_edit;
		private ViewMainMenuItem _menu_view;
		private ToolMainMenuItem _menu_tool;
		private WindMainMenuItem _menu_wind;
		private HelpMainMenuItem _menu_help;

		private void BuildMainMenuItems()
		{
			_logger.Trace($"building menu items for {nameof(_main_menu)}...");

			// _menu_file
			_menu_file = new FileMainMenuItem(this);

			// _menu_edit
			_menu_edit = new EditMainMenuItem(this);

			// _menu_view
			_menu_view = new ViewMainMenuItem(this);

			// _menu_tool
			_menu_tool = new ToolMainMenuItem(this);

			// _menu_window
			_menu_wind = new WindMainMenuItem(this);

			// _menu_help
			_menu_help = new HelpMainMenuItem(this);

			_main_menu.Items.Add(_menu_file);
			_main_menu.Items.Add(_menu_edit);
			_main_menu.Items.Add(_menu_view);
			_main_menu.Items.Add(_menu_tool);
			_main_menu.Items.Add(_menu_wind);
			_main_menu.Items.Add(_menu_help);
			_main_menu.MdiWindowListItem = _menu_wind;
		}

		#endregion

		#region ツールメニュー
		private ToolStripButton _toolbtn_reload;
		private ToolStripButton _toolbtn_save, _toolbtn_saveAs, _toolbtn_saveAll, _toolbtn_saveAllAs;
		private ToolStripButton _toolbtn_print, _toolbtn_printPreview, _toolbtn_pageSetup;
		private ToolStripButton _toolbtn_showSettings;

		private void BuildToolMenuItems()
		{
			_logger.Trace($"building menu items for {nameof(_tool_menu)}...");

			_toolbtn_reload = new ToolStripButton(_menu_file.Reload.Text, _menu_file.Reload.Image);
			_toolbtn_reload.DisplayStyle  = ToolStripItemDisplayStyle.Image;
			_toolbtn_reload.Click        += (sender, e) => {
				_menu_file.Reload.PerformClick();
			};

			_toolbtn_save = new ToolStripButton(_menu_file.SaveMenu.Text, _menu_file.SaveMenu.Image);
			_toolbtn_save.DisplayStyle  = ToolStripItemDisplayStyle.Image;
			_toolbtn_save.Click        += (sender, e) => {
				_menu_file.SaveMenu.PerformClick();
			};

			_toolbtn_saveAs = new ToolStripButton(_menu_file.SaveAsMenu.Text, _menu_file.SaveAsMenu.Image);
			_toolbtn_saveAs.DisplayStyle  = ToolStripItemDisplayStyle.Image;
			_toolbtn_saveAs.Click        += (sender, e) => {
				_menu_file.SaveAsMenu.PerformClick();
			};

			_toolbtn_saveAll = new ToolStripButton(_menu_file.SaveAllMenu.Text, _menu_file.SaveAllMenu.Image);
			_toolbtn_saveAll.DisplayStyle  = ToolStripItemDisplayStyle.Image;
			_toolbtn_saveAll.Click        += (sender, e) => {
				_menu_file.SaveAllMenu.PerformClick();
			};

			_toolbtn_saveAllAs = new ToolStripButton(_menu_file.SaveAllAsMenu.Text, _menu_file.SaveAllAsMenu.Image);
			_toolbtn_saveAllAs.DisplayStyle  = ToolStripItemDisplayStyle.Image;
			_toolbtn_saveAllAs.Click        += (sender, e) => {
				_menu_file.SaveAllAsMenu.PerformClick();
			};

			_toolbtn_print = new ToolStripButton(_menu_file.Print.Text, _menu_file.Print.Image);
			_toolbtn_print.DisplayStyle  = ToolStripItemDisplayStyle.Image;
			_toolbtn_print.Click        += (sender, e) => {
				_menu_file.Print.PerformClick();
			};

			_toolbtn_printPreview = new ToolStripButton(_menu_file.PrintPreview.Text, _menu_file.PrintPreview.Image);
			_toolbtn_printPreview.DisplayStyle  = ToolStripItemDisplayStyle.Image;
			_toolbtn_printPreview.Click        += (sender, e) => {
				_menu_file.PrintPreview.PerformClick();
			};

			_toolbtn_pageSetup = new ToolStripButton(_menu_file.PageSetup.Text, _menu_file.PageSetup.Image);
			_toolbtn_pageSetup.DisplayStyle  = ToolStripItemDisplayStyle.Image;
			_toolbtn_pageSetup.Click        += (sender, e) => {
				_menu_file.PageSetup.PerformClick();
			};

			_toolbtn_showSettings = new ToolStripButton(_menu_tool.ShowSettings.Text, _menu_tool.ShowSettings.Image);
			_toolbtn_showSettings.DisplayStyle  = ToolStripItemDisplayStyle.Image;
			_toolbtn_showSettings.Click        += (sender, e) => {
				_menu_tool.ShowSettings.PerformClick();
			};

			_tool_menu.Items.Add(_toolbtn_reload);
			_tool_menu.Items.Add(new ToolStripSeparator());
			_tool_menu.Items.Add(_toolbtn_save);
			_tool_menu.Items.Add(_toolbtn_saveAs);
			_tool_menu.Items.Add(_toolbtn_saveAll);
			_tool_menu.Items.Add(_toolbtn_saveAllAs);
			_tool_menu.Items.Add(new ToolStripSeparator());
			_tool_menu.Items.Add(_toolbtn_print);
			_tool_menu.Items.Add(_toolbtn_printPreview);
			_tool_menu.Items.Add(_toolbtn_pageSetup);
			_tool_menu.Items.Add(new ToolStripSeparator());
			_tool_menu.Items.Add(_toolbtn_showSettings);
		}

		#endregion

		#region ステータスバー
		private ToolStripStatusLabel _status_label1;
		private ToolStripStatusLabel _status_label2;

		public string StatusMessageLeft
		{
			set
			{
				_logger.Notice($"{nameof(_status_label1)}: {value}");
				_status_label1.Text = value;
				this.SetStatusLabelLocation();
			}
		}

		public string StatusMessageRight
		{
			set
			{
				_logger.Notice($"{nameof(_status_label2)}: {value}");
				_status_label2.Text = value;
			}
		}

		private void BuildStatusLabels()
		{
			_logger.Trace($"building status labels for {nameof(_status_bar)}...");

			// _status_label1
			_status_label1 = new ToolStripStatusLabel();
			_status_label1.Name = nameof(_status_label1);
			_status_label1.Text = nameof(_status_label1);

			// _status_label2
			_status_label2 = new ToolStripStatusLabel();
			_status_label2.Name = nameof(_status_label2);
			_status_label2.Text = nameof(_status_label2);

			_status_bar.Items.Add(_status_label1);
			_status_bar.Items.Add(new ToolStripSeparator());
			_status_bar.Items.Add(_status_label2);
			this.SetStatusLabelLocation();
		}

		private void SetStatusLabelLocation()
		{
			this.SuspendLayout();
			var m1 = _status_label1.Margin;
			m1.Right = this.ClientSize.Width / 2 - (_status_label1.Width + 16);
			_status_label1.Margin = m1;
			this.ResumeLayout();
		}

		#endregion
	}
}
