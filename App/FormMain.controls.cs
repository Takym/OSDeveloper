using System.Windows.Forms;
using OSDeveloper.Core.FileManagement;
using OSDeveloper.Core.GraphicalUIs;
using OSDeveloper.Core.GraphicalUIs.ToolStrips;

namespace OSDeveloper.App
{
	partial class Program { } // デザイナ避け
	partial class FormMain
	{
		#region インスタンス生成
		void CreateInstances()
		{
			_mainmenu = new MenuStrip();
			_menu_file = new FileMenuItem();
			_menu_edit = new EditMenuItem();
			_menu_view = new ViewMenuItem();
			_menu_project = new ProjectMenuItem();
			_menu_build = new BuildMenuItem();
			_menu_debug = new DebugMenuItem();
			_menu_tool = new ToolMenuItem();
			_menu_window = new WindowMenuItem(this);
			_menu_help = new HelpMenuItem();

			_toolmenu = new ToolStrip();
			_tool_placeholder = new ToolStripStatusLabel();

			_status_bar = new StatusStrip();
			_status_label = new ToolStripStatusLabel();

			_explorer = new Explorer();
		}
		#endregion

		#region メインメニュー
		private MenuStrip _mainmenu;
		private MainMenuItem _menu_file;
		private MainMenuItem _menu_edit;
		private MainMenuItem _menu_view;
		private MainMenuItem _menu_project;
		private MainMenuItem _menu_build;
		private MainMenuItem _menu_debug;
		private MainMenuItem _menu_tool;
		private MainMenuItem _menu_window;
		private MainMenuItem _menu_help;

		void BuildMainMenu()
		{
			_mainmenu.Name = nameof(_mainmenu);
			_mainmenu.Dock = DockStyle.Top;
			_mainmenu.LayoutStyle = ToolStripLayoutStyle.StackWithOverflow;

			// _menu_file
			_menu_file.Name = nameof(_menu_file);
			_menu_file.Text = MainMenuText.Menu_File;

			// _menu_edit
			_menu_edit.Name = nameof(_menu_edit);
			_menu_edit.Text = MainMenuText.Menu_Edit;

			// _menu_view
			_menu_view.Name = nameof(_menu_view);
			_menu_view.Text = MainMenuText.Menu_View;

			// _menu_project
			_menu_project.Name = nameof(_menu_project);
			_menu_project.Text = MainMenuText.Menu_Project;

			// _menu_build
			_menu_build.Name = nameof(_menu_build);
			_menu_build.Text = MainMenuText.Menu_Build;

			// _menu_debug
			_menu_debug.Name = nameof(_menu_debug);
			_menu_debug.Text = MainMenuText.Menu_Debug;

			// _menu_tool
			_menu_tool.Name = nameof(_menu_tool);
			_menu_tool.Text = MainMenuText.Menu_Tool;

			// _menu_window
			_menu_window.Name = nameof(_menu_window);
			_menu_window.Text = MainMenuText.Menu_Window;

			// _menu_help
			_menu_help.Name = nameof(_menu_help);
			_menu_help.Text = MainMenuText.Menu_Help;

			_mainmenu.Items.Add(_menu_file);
			_mainmenu.Items.Add(_menu_edit);
			_mainmenu.Items.Add(_menu_view);
			_mainmenu.Items.Add(_menu_project);
			_mainmenu.Items.Add(_menu_build);
			_mainmenu.Items.Add(_menu_debug);
			_mainmenu.Items.Add(_menu_tool);
			_mainmenu.Items.Add(_menu_window);
			_mainmenu.Items.Add(_menu_help);
			_mainmenu.MdiWindowListItem = _menu_window;
		}
		#endregion

		#region ツールメニュー
		private ToolStrip _toolmenu;
		private ToolStripStatusLabel _tool_placeholder;

		void BuildToolMenu()
		{
			_toolmenu.Name = nameof(_toolmenu);
			_toolmenu.Dock = DockStyle.Top;
			_toolmenu.LayoutStyle = ToolStripLayoutStyle.StackWithOverflow;
			_toolmenu.GripStyle = ToolStripGripStyle.Visible;

			// _tool_placeholder
			_tool_placeholder.Name = nameof(_tool_placeholder);
			_tool_placeholder.Text = "Tool Menu";

			_toolmenu.Items.Add(_tool_placeholder);
		}
		#endregion

		#region ステータスバー
		private StatusStrip _status_bar;
		private ToolStripStatusLabel _status_label;

		void BuildStatudBar()
		{
			_status_bar.Name = nameof(_status_bar);
			_status_bar.Dock = DockStyle.Bottom;
			_status_bar.LayoutStyle = ToolStripLayoutStyle.Flow;

			// _status_label
			_status_label.Name = nameof(_status_label);
			_status_label.Text = "Status Bar";

			_status_bar.Items.Add(_status_label);
		}
		#endregion

		#region エクスプローラ
		private Explorer _explorer;

		public void BuildExplorer()
		{
			_explorer.Name = nameof(_explorer);
			_explorer.Dock = DockStyle.Fill;
			_explorer.Directory = new DirMetadata(SystemPaths.Workspace); // TODO: テスト用
			_explorer_container.Controls.Add(_explorer);
		}
		#endregion
	}
}
