using System;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using OSDeveloper.Core.GraphicalUIs;
using OSDeveloper.Core.GraphicalUIs.Controls;
using OSDeveloper.Core.GraphicalUIs.ToolStrips;
using OSDeveloper.Core.Logging;
using OSDeveloper.Core.Settings;
using OSDeveloper.Properties;

namespace OSDeveloper.App
{
	internal static partial class Program // デザイナ避け
	{
		#region 起動処理
		[STAThread()]
		static int Main(string[] args)
		{
			// Application初期化
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			// システム設定適応
			ConfigManager.ApplySystemSettings();

			// ロガー初期化
			Logger l = Logger.GetSystemLogger("system");
			l.Trace($"The application is started with command-line: {{{string.Join(", ", args)}}}");
			l.Trace("System.Windows.Forms.Application was initialized already");
			l.Trace("Syatem Settings was loaded already");

			// ログ用に言語情報取得
			var tmp_culture = Thread.CurrentThread.CurrentUICulture;
			Thread.CurrentThread.CurrentUICulture = new CultureInfo("ja-JP");
			string culture = tmp_culture.Name
				+ "; EN: " + tmp_culture.EnglishName
				+ "; 日: " + tmp_culture.DisplayName
				+ "; ::: " + tmp_culture.NativeName;
			Thread.CurrentThread.CurrentUICulture = tmp_culture;

			// システム設定をログに書き込み
			l.Debug($"{nameof(Application)}.{nameof(Application.VisualStyleState)} = {Application.VisualStyleState}");
			l.Debug($"{nameof(CultureInfo)}.{nameof(CultureInfo.CurrentCulture)} = {culture}");
			l.Debug($"{nameof(LogFile)}.{nameof(LogFile.NoInternalLog)} = {LogFile.NoInternalLog}");
			l.Debug($"{nameof(LogFile)}.{nameof(LogFile.InternalNameKind)} = {LogFile.InternalNameKind}");
			l.Debug($"{nameof(MenuStripManager)}.{nameof(MenuStripManager.UseEXDialog)} = {MenuStripManager.UseEXDialog}");

			// メインウィンドウ表示
			Application.Run(new FormMain());

			// Runメソッドが終了したタイミングでロガーが破棄される為、
			// ここでロガーを参照してはいけない

			return 0;
		}
		#endregion
	}

	public sealed partial class FormMain : MainWindowBase
	{
		private Logger _logger;
		private string _base_title;
		private ToolStripPanel _menu_container;
		private Panel _explorer_container;
		private TerminalTab _terminal_container;
		private Splitter _explorer_splitter, _terminal_splitter;
		private MdiChildrenTab _children_tab;

		#region UI/UX生成処理
		public FormMain()
		{
			_logger = Logger.GetSystemLogger(nameof(FormMain));
			_logger.Trace("The constructor of FormMain was called");

			// コントロールのインスタンスを生成
			_logger.Info("Generating the instances pf controls...");
			_menu_container = new ToolStripPanel();
			_explorer_container = new Panel();
			_terminal_container = new TerminalTab();
			_explorer_splitter = new Splitter();
			_terminal_splitter = new Splitter();
			_children_tab = new MdiChildrenTab();
			this.CreateInstances();

			// レイアウトロジック停止
			_logger.Info("Suspending the layout logic...");
			_mainmenu				.SuspendLayout();
			_toolmenu				.SuspendLayout();
			_status_bar				.SuspendLayout();
			_menu_container			.SuspendLayout();
			_explorer_container		.SuspendLayout();
			_terminal_container		.SuspendLayout();
			_explorer				.SuspendLayout();
			_terminal_empty			.SuspendLayout();
			_children_tab			.SuspendLayout();
			this					.SuspendLayout();

			// コントロールを初期化
			{
				// FormMain
				_logger.Info("Creating the window control...");
				this.Name = nameof(FormMain);
				this.Text = _base_title = $"{ASMINFO.Caption} {ASMINFO.Edition} ";
				this.ClientSize = new Size(800, 600);
				this.Icon = Resources.FormMain;
				this.IsMdiContainer = true;
			}

			{
				// _mainmenu
				_logger.Info("Creating the main menu...");
				this.BuildMainMenu();
				this.MainMenuStrip = _mainmenu;
			}

			{
				// _container
				_logger.Info("Creating the menu container...");
				_menu_container.Name = nameof(_menu_container);
				_menu_container.Dock = DockStyle.Top;
				_menu_container.BackColor = Color.FromArgb(0xAE, 0xAE, 0xAE);
				_menu_container.Orientation = Orientation.Horizontal;

				// _toolmenu
				_logger.Info("Creating the tool menu...");
				this.BuildToolMenu();

				_menu_container.Join(_toolmenu);
			}

			{
				// _status_bar
				_logger.Info("Creating the status bar...");
				this.BuildStatudBar();
				this.SetStatusMessage(MainWindowStatusMessage.Preparing());
			}

			{
				// _explorer_container
				_logger.Info("Creating the explorer container...");
				_explorer_container.Name = nameof(_explorer_container);
				_explorer_container.Dock = DockStyle.Left;
				_explorer_container.BorderStyle = BorderStyle.Fixed3D;

				_logger.Info("Creating the explorer panel...");
				this.BuildExplorer();

				// _explorer_splitter
				_logger.Info("Creating the explorer splitter...");
				_explorer_splitter.Name = nameof(_explorer_splitter);
				_explorer_splitter.Dock = DockStyle.Left;

				// _terminal_container
				_logger.Info("Creating the terminal container...");
				_terminal_container.Name = nameof(_terminal_container);
				_terminal_container.Dock = DockStyle.Bottom;
				_terminal_container.Height += 48;

				_logger.Info("Creating the terminal tabs...");
				this.BuildTerminal();
				_terminal_container.TabPages.Add(_terminal_empty);

				// _terminal_splitter
				_logger.Info("Creating the terminal splitter...");
				_terminal_splitter.Name = nameof(_terminal_splitter);
				_terminal_splitter.Dock = DockStyle.Bottom;
			}

			{
				// _children_tab
				_logger.Info("Creating the MDI children tabs...");
				_children_tab.Name = nameof(_children_tab);
				_children_tab.MdiClient = this.MdiClient;
			}

			// その他
			_logger.Info("Setting misc properties...");
			ToolStripManager.Renderer = new ToolStripRendererEx(new ToolStripColorTable());
			Application.ThreadException += this.Application_ThreadException;

			// コントロールを貼り付け
			_logger.Info("Adding controls to the form...");
			this.Controls.Add(_children_tab);
			this.Controls.Add(_terminal_splitter);
			this.Controls.Add(_terminal_container);
			this.Controls.Add(_explorer_splitter);
			this.Controls.Add(_explorer_container);
			this.Controls.Add(_status_bar);
			this.Controls.Add(_menu_container);
			this.Controls.Add(_mainmenu);

			// レイアウトロジック再開
			_logger.Info("Resuming the layout logic...");
			_mainmenu				.ResumeLayout(false);	_mainmenu				.PerformLayout();
			_toolmenu				.ResumeLayout(false);	_toolmenu				.PerformLayout();
			_status_bar				.ResumeLayout(false);	_status_bar				.PerformLayout();
			_menu_container			.ResumeLayout(false);	_menu_container			.PerformLayout();
			_explorer_container		.ResumeLayout(false);	_explorer_container		.PerformLayout();
			_terminal_container		.ResumeLayout(false);	_terminal_container		.PerformLayout();
			_explorer				.ResumeLayout(false);	_explorer				.PerformLayout();
			_terminal_empty			.ResumeLayout(false);	_terminal_empty			.PerformLayout();
			_children_tab			.ResumeLayout(false);	_children_tab			.PerformLayout();
			this					.ResumeLayout(false);	this					.PerformLayout();
			_logger.Trace("Finished construction of FormMain");
		}
		#endregion
	}
}
