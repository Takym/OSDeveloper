using System.Windows.Forms;
using OSDeveloper.Core.FileManagement;
using OSDeveloper.Core.GraphicalUIs;
using OSDeveloper.Core.Logging;
using OSDeveloper.Core.MiscUtils;
using OSDeveloper.Native;

namespace OSDeveloper.Core.Editors
{
	/// <summary>
	///  エディタを表すメインウィンドウに表示されるMDI子ウィンドウです。
	/// </summary>
	public class EditorWindow : Form
	{
		private readonly MainWindowBase _parent;
		private new Form MdiParent { get; set; } // プロパティの隠蔽

		/// <summary>
		///  このエディタ専用のロガーを取得します。
		/// </summary>
		protected Logger Logger
		{
			get
			{
				return _logger;
			}
		}
		private readonly Logger _logger;

		/// <summary>
		///  このエディタで編集するファイルを取得または設定します。
		/// </summary>
		public FileMetadata TargetFile
		{
			get
			{
				return _target;
			}

			set
			{
				_target = value;
				this.Text = _target.Name;
			}
		}
		private FileMetadata _target;

		/// <summary>
		///  親ウィンドウを指定して、
		///  型'<see cref="OSDeveloper.Core.Editors.EditorWindow"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		/// <param name="parent">親ウィンドウです。</param>
		public EditorWindow(MainWindowBase parent)
		{
			if (this.DesignMode || WinFormUtils.DesignMode) {
				_parent = parent;
				if (parent != null) base.MdiParent = parent;
				this.Text = "EditorWindow (* this editor window is design mode!)";
			} else {
				_logger = Logger.GetSystemLogger(this.GetType().Name);
				_parent = parent;
				_target = null;

				if (parent != null) { // nullでなければ、MDI子ウィンドウにする
					_logger.Debug($"MDI Parent = {parent.GetType().FullName}, Name:{parent.Name}, Text:{parent.Text}");
					base.MdiParent = parent;
				}

				_logger.Info("Loading the icon...");
				this.Icon = Libosdev.GetIcon("EditorWindow", this, out int v0);
				_logger.Info("HResult = " + v0);

				if (this.GetType().Equals(typeof(EditorWindow))) { // 自分自身なら
					this.Text = "<Base Object of EditorWindow>";
				} else {
					this.Text = this.GetType().Name;
				}

				_logger.Info("New editor window is initialized");
				_logger.Notice($"The class type of this window is: {this.GetType().FullName}");
				_logger.Notice($"The caption of this window is: {this.Text}");
			}
		}

		/// <summary>
		///  親ウィンドウを指定せずに単独のウィンドウとして、
		///  型'<see cref="OSDeveloper.Core.Editors.EditorWindow"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		public EditorWindow() : this(null) { }
	}
}
