using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using OSDeveloper.GUIs.Controls;
using OSDeveloper.GUIs.Explorer;
using OSDeveloper.GUIs.Terminal;
using OSDeveloper.GUIs.ToolStrips;
using OSDeveloper.IO.Configuration;
using OSDeveloper.IO.Logging;
using OSDeveloper.Native;
using OSDeveloper.Resources;

namespace OSDeveloper
{
	[DesignerCategory("")] // デザイナ避け
	public sealed partial class FormMain : Form
	{
		private readonly Logger         _logger;
		private readonly MdiClient      _mdi_client;
		private readonly MdiChildrenTab _mdi_tab;
		private readonly FileTreeBox       _explorer;
		private readonly TabControlEx   _terminal;
		private readonly Splitter       _splitter_vert;
		private readonly Splitter       _splitter_hori;

		public FormMain(string[] args)
		{
			_logger = Logger.Get(nameof(FormMain));
			_logger.Trace("constructing the main window...");

			// コントロールのインスタンス生成
			_logger.Trace("creating controls...");
			_mdi_tab       = new MdiChildrenTab();
			_main_menu     = new MenuStrip();
			_tool_menu     = new ToolStrip();
			_status_bar    = new StatusStrip();
			_explorer      = new FileTreeBox(this);
			_terminal      = new TabControlEx();
			_splitter_vert = new Splitter();
			_splitter_hori = new Splitter();

			// レイアウト停止
			_logger.Trace("suspending the control layouts...");
			this          .SuspendLayout();
			_mdi_tab      .SuspendLayout();
			_main_menu    .SuspendLayout();
			_tool_menu    .SuspendLayout();
			_status_bar   .SuspendLayout();
			_explorer     .SuspendLayout();
			_terminal     .SuspendLayout();
			_splitter_vert.SuspendLayout();
			_splitter_hori.SuspendLayout();

			{ // MDI親ウィンドウとして設定
				_logger.Trace($"setting the main window as MDI parent...");
				this.IsMdiContainer = true;
				for (int i = 0; i < this.Controls.Count; ++i) {
					if (this.Controls[i] is MdiClient mc) {
						_mdi_client = mc;
						break;
					}
				}
			}

			{ // _mdi_tab
				_logger.Trace($"setting {nameof(_mdi_tab)}...");
				_mdi_tab.Name      = nameof(_mdi_tab);
				_mdi_tab.Dock      = DockStyle.Top;
				_mdi_tab.Size      = new Size(192, 32);
				_mdi_tab.MdiClient = _mdi_client;
				_mdi_tab.Font      = SystemFonts.IconTitleFont;
			}

			this.BuildMenus();
			this.BuildMainMenuItems();
			this.BuildToolMenuItems();
			this.BuildStatusLabels();

			{ // _explorer
				_logger.Trace($"setting {nameof(_explorer)}...");
				_explorer.Name        = nameof(_explorer);
				_explorer.Dock        = DockStyle.Left;
				_explorer.Size        = new Size(200, 200);
				_explorer.BorderStyle = BorderStyle.Fixed3D;
			}

			{ // _terminal
				_logger.Trace($"setting {nameof(_terminal)}...");
				_terminal.Name = nameof(_terminal);
				_terminal.Dock = DockStyle.Bottom;
				_terminal.Size = new Size(200, 200);
			}

			{ // _splitter_vert
				_logger.Trace($"setting {nameof(_splitter_vert)}...");
				_splitter_vert.Name = nameof(_splitter_vert);
				_splitter_vert.Dock = DockStyle.Left;
			}

			{ // _splitter_hori
				_logger.Trace($"setting {nameof(_splitter_hori)}...");
				_splitter_hori.Name = nameof(_splitter_hori);
				_splitter_hori.Dock = DockStyle.Bottom;
			}

			{ // FormMain
				_logger.Trace($"setting {nameof(FormMain)}...");
				this.Name        = nameof(FormMain);
				this.Text        = $"{ASMINFO.Caption} {ASMINFO.Edition}";
				this.Icon        = Libosdev.GetIcon(FormMainRes.Icon);
				this.MinimumSize = new Size(600, 450);
				this.ClientSize  = SettingManager.System.MainWindowPosition.Size;

				if (SettingManager.System.MainWindowPosition.X > -1 &&
					SettingManager.System.MainWindowPosition.Y > -1) {
					this.StartPosition = FormStartPosition.Manual;
					this.Location      = SettingManager.System.MainWindowPosition.Location;
				}
			}

			// ToolStrip系コントロールに関する設定
			ToolStripManager.Renderer = new ToolStripRendererEx(new ToolStripColorTable());

			// コントロール登録
			_logger.Trace($"adding controls...");
			this.Controls.Add(_mdi_tab);
			this.Controls.Add(_splitter_hori);
			this.Controls.Add(_splitter_vert);
			this.Controls.Add(_terminal);
			this.Controls.Add(_explorer);
			this.Controls.Add(_status_bar);
			this.Controls.Add(_tool_menu);
			this.Controls.Add(_main_menu);
			this.MainMenuStrip = _main_menu;

			// レイアウト再開
			_logger.Trace("resuming the control layouts...");
			_splitter_hori .ResumeLayout(false);		_splitter_hori .PerformLayout();
			_splitter_vert .ResumeLayout(false);		_splitter_vert .PerformLayout();
			_terminal      .ResumeLayout(false);		_terminal      .PerformLayout();
			_explorer      .ResumeLayout(false);		_explorer      .PerformLayout();
			_status_bar    .ResumeLayout(false);		_status_bar    .PerformLayout();
			_tool_menu     .ResumeLayout(false);		_tool_menu     .PerformLayout();
			_main_menu     .ResumeLayout(false);		_main_menu     .PerformLayout();
			_mdi_tab       .ResumeLayout(false);		_mdi_tab       .PerformLayout();
			this           .ResumeLayout(false);		this           .PerformLayout();

			_logger.Trace("constructed the main window");
		}
	}
}
