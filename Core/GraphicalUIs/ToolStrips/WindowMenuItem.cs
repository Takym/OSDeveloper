using System;
using System.Windows.Forms;

namespace OSDeveloper.Core.GraphicalUIs.ToolStrips
{
	partial class MenuStripManager { } // デザイナ避け

	/// <summary>
	///  子ウィンドウの状態を管理するメニューを表します。
	/// </summary>
	public class WindowMenuItem : MainMenuItem
	{
		private readonly MainWindowBase _mwnd_base;
		private readonly InternalMenuItem _icons, _cascade, _hori, _vert;
		private readonly ToolStripMenuItem _close_all;

		/// <summary>
		///  このメニューの操作対象を指定して、
		///  型'<see cref="OSDeveloper.Core.GraphicalUIs.ToolStrips.WindowMenuItem"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		/// <param name="mwndBase">このメニューの親ウィンドウです。</param>
		public WindowMenuItem(MainWindowBase mwndBase)
		{
			_mwnd_base = mwndBase;
			_icons = new InternalMenuItem(mwndBase, MdiLayout.ArrangeIcons);
			_cascade = new InternalMenuItem(mwndBase, MdiLayout.Cascade);
			_hori = new InternalMenuItem(mwndBase, MdiLayout.TileHorizontal);
			_vert = new InternalMenuItem(mwndBase, MdiLayout.TileVertical);
			_close_all = new ToolStripMenuItem();

			_icons.Text = MenuText.Window_ArrangeIcons;
			_icons.ShortcutKeys = Keys.Alt | Keys.Left;
			_cascade.Text = MenuText.Window_Cascade;
			_cascade.ShortcutKeys = Keys.Alt | Keys.Up;
			_hori.Text = MenuText.Window_Hori;
			_hori.ShortcutKeys = Keys.Alt | Keys.Down;
			_vert.Text = MenuText.Window_Vert;
			_vert.ShortcutKeys = Keys.Alt | Keys.Right;
			_close_all.Text = MenuText.Window_CloseAll;
			_close_all.ShortcutKeys = Keys.Alt | Keys.Delete;
			_close_all.Click += this._close_all_Click;

			this.DropDownItems.Add(_icons);
			this.DropDownItems.Add(_cascade);
			this.DropDownItems.Add(_hori);
			this.DropDownItems.Add(_vert);
			this.DropDownItems.Add(_close_all);
			this.DropDownItems.Add(new ToolStripSeparator());
		}

		private void _close_all_Click(object sender, EventArgs e)
		{
			foreach (var form in _mwnd_base.MdiChildren) {
				form.Close();
			}
		}

		private sealed class InternalMenuItem : ToolStripMenuItem
		{
			private MainWindowBase _mwnd_base;
			private readonly MdiLayout _layout;

			public InternalMenuItem(MainWindowBase mwndBase, MdiLayout layout)
			{
				_mwnd_base = mwndBase;
				_layout = layout;
			}

			protected override void OnClick(EventArgs e)
			{
				base.OnClick(e);
				_mwnd_base.LayoutMdi(_layout);
			}
		}
	}
}
