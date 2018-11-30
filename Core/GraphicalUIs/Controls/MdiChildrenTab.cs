using System;
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
					_children.Clear();
				}
				value.ControlAdded += this._mdi_client_ControlAdded;
				value.ControlRemoved += this._mdi_client_ControlAdded;
				_children.AddRange(value.MdiChildren);
				_mdi_client = value;
			}
		}
		private MdiClient _mdi_client;

		/// <summary>
		///  タブボタンのカラーテーマを取得または設定します。
		/// </summary>
		[Browsable(false)]
		public OsdevColorTheme ButtonColor { get; set; }

		private Logger _logger;
		private List<Form> _children;

		/// <summary>
		///  型'<see cref="OSDeveloper.Core.GraphicalUIs.Controls.MdiChildrenTab"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		public MdiChildrenTab()
		{
			_logger = Logger.GetSystemLogger(nameof(MdiChildrenTab));
			_children = new List<Form>();
			this.InitializeComponent();
			this.ResetButtonColor();
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


			int wid = (this.Width - 4) / _children.Count;
			int hei = this.Height - 4;
			int x = 2, y = 2;
			var g = e.Graphics;
			g.Clear(this.BackColor);

			using (Brush back = new SolidBrush(this.ButtonColor.Light))
			using (Brush fore = new SolidBrush(this.ForeColor))
			using (Pen border = new Pen(this.ButtonColor.Dark)) {
				for (int i = 0; i < _children.Count; ++i) {
					g.FillRectangle(back, x, y, wid, hei);
					g.DrawRectangle(border, x, y, wid, hei);
					g.DrawString(_children[i].Text, this.Font, fore, new Rectangle(x + 4, y + 4, wid - 8, hei - 8));
					x += wid;
				}
			}

			this.ResumeLayout(false);
			_logger.Trace($"completed {nameof(OnPaint)}");
		}

		private void _mdi_client_ControlAdded(object sender, ControlEventArgs e)
		{
			_logger.Trace($"executing {nameof(_mdi_client_ControlAdded)}...");

			if (e.Control is Form f) {
				_children.Add(f);
				_logger.Info($"the added form is: {f.Text}");
				this.Invalidate();
			}

			_logger.Trace($"completed {nameof(_mdi_client_ControlAdded)}");
		}

		private void _mdi_client_ControlRemoved(object sender, ControlEventArgs e)
		{
			_logger.Trace($"executing {nameof(_mdi_client_ControlRemoved)}...");

			if (e.Control is Form f) {
				_children.Remove(f);
				_logger.Info($"the removed form is: {f.Text}");
				this.Invalidate();
			}

			_logger.Trace($"completed {nameof(_mdi_client_ControlRemoved)}");
		}
	}
}
