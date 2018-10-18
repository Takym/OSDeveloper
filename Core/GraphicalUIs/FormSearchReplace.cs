using System;
using System.Windows.Forms;
using OSDeveloper.Core.Editors;
using OSDeveloper.Core.Logging;

namespace OSDeveloper.Core.GraphicalUIs
{
	/// <summary>
	///  検索と置換を実行するウィンドウを表します。
	/// </summary>
	public partial class FormSearchReplace : Form
	{
		private Logger _logger;
		private ISearchReplaceFeature _srf;

		/// <summary>
		///  操作対象のエディタを指定して、
		///  型'<see cref="OSDeveloper.Core.GraphicalUIs.FormSearchReplace"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		/// <param name="srf">操作対象の<see cref="OSDeveloper.Core.Editors.ISearchReplaceFeature"/>です。</param>
		public FormSearchReplace(ISearchReplaceFeature srf)
		{
			_logger = Logger.GetSystemLogger(nameof(FormSearchReplace));
			_srf = srf;
			InitializeComponent();
			_logger.Trace($"Created the new instance of {nameof(FormSearchReplace)}");
		}

		private void FormSearchReplace_Load(object sender, EventArgs e)
		{
			_logger.Info($"{this.Text} was showed");
		}

		private void btnNext_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(btnNext_Click)}...");

			_srf.FindNext(tboxOld.Text);

			_logger.Trace($"completed {nameof(btnNext_Click)}");
		}

		private void btnCount_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(btnCount_Click)}...");

			string msg = string.Format(FormSearchReplaceTexts.MsgCount,
				tboxOld.Text,
				_srf.Find(tboxOld.Text));
			MessageBox.Show(this, msg, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

			_logger.Trace($"completed {nameof(btnCount_Click)}");
		}

		private void btnReplace_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(btnReplace_Click)}...");

			if (_srf.IsSelected) {
				_srf.ReplaceSelected(tboxNew.Text);
			} else {
				_srf.ReplaceNext(tboxOld.Text, tboxNew.Text);
			}

			_logger.Trace($"completed {nameof(btnReplace_Click)}");
		}

		private void btnRepAll_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(btnRepAll_Click)}...");

			_srf.ReplaceAll(tboxOld.Text, tboxNew.Text);

			_logger.Trace($"completed {nameof(btnRepAll_Click)}");
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(btnClose_Click)}...");

			this.Close();

			_logger.Trace($"completed {nameof(btnClose_Click)}");
		}
	}
}
