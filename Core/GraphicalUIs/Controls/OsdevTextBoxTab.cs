using System.Windows.Forms;
using OSDeveloper.Core.Logging;

namespace OSDeveloper.Core.GraphicalUIs.Controls
{
	/// <summary>
	///  <see cref="OSDeveloper.Core.GraphicalUIs.Controls.OsdevTextBox"/>の
	///  コマンドモードで利用するタブページです。
	/// </summary>
	public partial class OsdevTextBoxTab : TabPage
	{
		private Logger _logger;
		private OsdevTextBox _target;

		/// <summary>
		///  型'<see cref="OSDeveloper.Core.GraphicalUIs.Controls.OsdevTextBoxTab"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		/// <param name="target">操作対象のテキストボックスです。</param>
		public OsdevTextBoxTab(OsdevTextBox target)
		{
			_logger = Logger.GetSystemLogger(nameof(OsdevTextBoxTab));

			this.InitializeComponent();
			_target = target;
			_logger.Info("target textbox = " + _target.Name);

			_logger.Trace(nameof(OsdevTextBoxTab) + " is constructed");
		}
	}
}
