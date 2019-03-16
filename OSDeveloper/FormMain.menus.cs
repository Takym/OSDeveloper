using System.Windows.Forms;
using OSDeveloper.GraphicalUIs.ToolStrips;
using OSDeveloper.MiscUtils;
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
		private MainMenuItem _menu_file;
		private MainMenuItem _menu_view;
		private MainMenuItem _menu_tool;
		private MainMenuItem _menu_window;

		private void BuildMainMenuItems()
		{
			_logger.Trace($"building menu items for {nameof(_main_menu)}...");

			// _menu_file
			_menu_file = new FileMainMenuItem(this);

			// _menu_view
			_menu_view = new ViewMainMenuItem(this);

			// _menu_tool
			_menu_tool = new ToolMainMenuItem(this);

			// _menu_window
			_menu_window = new WindowMainMenuItem(this);

			_main_menu.Items.Add(_menu_file);
			_main_menu.Items.Add(_menu_view);
			_main_menu.Items.Add(_menu_tool);
			_main_menu.Items.Add(_menu_window);
			_main_menu.MdiWindowListItem = _menu_window;
		}

		#endregion

		#region ツールメニュー
#if DEBUG
		private ToolStripButton _toolbtn_for_test;
#endif

		private void BuildToolMenuItems()
		{
			_logger.Trace($"building menu items for {nameof(_tool_menu)}...");

#if DEBUG
			// _toolbtn_for_test
			// TODO: デバッグ用のツールバーボタン
			_toolbtn_for_test = new ToolStripButton("TEST");
			_toolbtn_for_test.Click += (sender, e) => {
				Form f = new Form();
				f.MdiParent = this;
				f.Text = StringUtils.GetRandomText();
				f.Show();
			};

			_tool_menu.Items.Add(_toolbtn_for_test);
#endif
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
			var m1 = _status_label1.Margin;
			m1.Right = this.ClientSize.Width / 2 - (_status_label1.Width + 16);
			_status_label1.Margin = m1;
		}

		#endregion
	}
}
