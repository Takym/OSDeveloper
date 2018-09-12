using System.Windows.Forms;
using OSDeveloper.Core.FileManagement;
using OSDeveloper.Core.GraphicalUIs;

namespace OSDeveloper.Core.Editors
{
	/// <summary>
	///  エディタを表すメインウィンドウに表示されるMDI子ウィンドウです。
	/// </summary>
	public class EditorWindow : Form
	{
		private readonly MainWindowBase _parent;
		private new Form MdiParent { get; set; }

		/// <summary>
		///  このエディタで編集するファイルを取得または設定します。
		/// </summary>
		public FileMetadata TargetFile { get; set; }

		/// <summary>
		///  型'<see cref="OSDeveloper.Core.Editors.EditorWindow"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		/// <param name="parent">親ウィンドウです。</param>
		public EditorWindow(MainWindowBase parent)
		{
			_parent = parent;
			base.MdiParent = parent;
		}


	}
}
