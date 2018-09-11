using System.Windows.Forms;
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
