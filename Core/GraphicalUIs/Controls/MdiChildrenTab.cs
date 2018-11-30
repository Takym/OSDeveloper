﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OSDeveloper.Core.Logging;

namespace OSDeveloper.Core.GraphicalUIs.Controls
{
	/// <summary>
	///  MDI子フォームをタブ形式で切り替えます。
	///  このクラスは継承できません。
	/// </summary>
	public sealed partial class MdiChildrenTab : Control
	{
		/// <summary>
		///  このコントロールで管理するMDIクライアントを取得または設定します。
		/// </summary>
		[Browsable(false)]
		public MdiClient MdiClient
		{
			get
			{
				return _mdi_client;
			}
			set
			{
				if (_mdi_client != null) {
					_mdi_client.ControlAdded -= this._mdi_client_ControlAdded;
					_mdi_client.ControlRemoved -= this._mdi_client_ControlRemoved;
					(_mdi_client.Parent as Form).MdiChildActivate -= this._mdi_client_MdiChildActivate;
				}
				value.ControlAdded += this._mdi_client_ControlAdded;
				value.ControlRemoved += this._mdi_client_ControlAdded;
				(value.Parent as Form).MdiChildActivate += this._mdi_client_MdiChildActivate;
				_mdi_client = value;
			}
		}

		private MdiClient _mdi_client;

		/// <summary>
		///  タブボタンのカラーテーマを取得または設定します。
		/// </summary>
		[Browsable(false)]
		public OsdevColorTheme ButtonColor { get; set; }

		/// <summary>
		///  マウスイベントが発生しているタブボタンのカラーテーマを取得または設定します。
		/// </summary>
		[Browsable(false)]
		public OsdevColorTheme MouseActionButtonColor { get; set; }

		private Logger _logger;

		/// <summary>
		///  型'<see cref="OSDeveloper.Core.GraphicalUIs.Controls.MdiChildrenTab"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		public MdiChildrenTab()
		{
			_logger = Logger.GetSystemLogger(nameof(MdiChildrenTab));
			this.InitializeComponent();
			this.ResetButtonColor();
			this.ResetMouseActionButtonColor();
			this.SetStyle(
				ControlStyles.UserPaint |
				ControlStyles.Opaque |
				ControlStyles.ResizeRedraw |
				ControlStyles.Selectable |
				ControlStyles.UserMouse |
				ControlStyles.AllPaintingInWmPaint |
				ControlStyles.OptimizedDoubleBuffer,
				true);
		}

		/// <summary>
		///  <see cref="OSDeveloper.Core.GraphicalUIs.Controls.MdiChildrenTab.ButtonColor"/>プロパティを限定値にリセットします。
		/// </summary>
		public void ResetButtonColor()
		{
			this.ButtonColor = OsdevColorThemes.FreshBlue;
		}

		/// <summary>
		///  <see cref="OSDeveloper.Core.GraphicalUIs.Controls.MdiChildrenTab.MouseActionButtonColor"/>プロパティを限定値にリセットします。
		/// </summary>
		public void ResetMouseActionButtonColor()
		{
			this.MouseActionButtonColor = OsdevColorThemes.Cyan;
		}

		/// <summary>
		///  <see cref="System.Windows.Forms.Control.Paint"/>イベントを発生させます。
		/// </summary>
		/// <param name="e">
		///  イベントデータを格納している<see cref="System.Windows.Forms.PaintEventArgs"/>オブジェクトです。
		/// </param>
		protected override void OnPaint(PaintEventArgs e)
		{
			_logger.Trace($"executing {nameof(OnPaint)}...");
			this.SuspendLayout();
			base.OnPaint(e);

			// 処理に必要な変数を初期化
			var children = _mdi_client.MdiChildren;
			var g = e.Graphics;
			g.Clear(this.BackColor);
			if (children.Length == 0) goto end;
			int wid = (this.Width - 6) / children.Length;
			int hei = this.Height - 6;
			int x = 3, y = 3;

			// タブボタン描画
			using (Brush back_a = new SolidBrush(this.ButtonColor.Normal))
			using (Brush back_i = new SolidBrush(this.ButtonColor.Light))
			using (Brush fore = new SolidBrush(this.ForeColor))
			using (Pen border = new Pen(this.ButtonColor.Dark)) {
				for (int i = 0; i < children.Length; ++i) {
					if ((_mdi_client.Parent as Form)?.ActiveMdiChild == children[i]) {
						// 子フォームがアクティブなら強調表示
						g.FillRectangle(back_a, x, y, wid, hei);
					} else {
						// それ以外は通常表示
						g.FillRectangle(back_i, x, y, wid, hei);
					}
					g.DrawRectangle(border, x, y, wid, hei);
					g.DrawRectangle(border, x + 1, y + 1, wid - 1, hei - 1);
					g.DrawString($"{i+1}: {children[i].Text}", this.Font, fore, new Rectangle(x + 4, y + 4, wid - 8, hei - 8));
					x += wid;
				}
			}

end:
			this.ResumeLayout(false);
			_logger.Trace($"completed {nameof(OnPaint)}");
		}

		private void _mdi_client_ControlAdded(object sender, ControlEventArgs e)
		{
			_logger.Trace($"executing {nameof(_mdi_client_ControlAdded)}...");

			if (e.Control is Form f) {
				//_children.Add(f);
				_logger.Info($"the added form is: {f.Text}");
				this.Invalidate();
			}

			_logger.Trace($"completed {nameof(_mdi_client_ControlAdded)}");
		}

		private void _mdi_client_ControlRemoved(object sender, ControlEventArgs e)
		{
			_logger.Trace($"executing {nameof(_mdi_client_ControlRemoved)}...");

			if (e.Control is Form f) {
				//_children.Remove(f);
				_logger.Info($"the removed form is: {f.Text}");
				this.Invalidate();
			}

			_logger.Trace($"completed {nameof(_mdi_client_ControlRemoved)}");
		}

		private void _mdi_client_MdiChildActivate(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(_mdi_client_MdiChildActivate)}...");
			this.Invalidate();
			_logger.Trace($"completed {nameof(_mdi_client_MdiChildActivate)}");
		}
	}
}
