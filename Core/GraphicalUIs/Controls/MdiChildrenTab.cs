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
	/// </summary>
	public partial class MdiChildrenTab : Control
	{
		/// <summary>
		///  このコントロールで管理するMDIクライアントです。
		/// </summary>
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
