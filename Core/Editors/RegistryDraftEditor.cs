using OSDeveloper.Core.GraphicalUIs;

namespace OSDeveloper.Core.Editors
{
	/// <summary>
	///  レジストリの下書きを変種します。
	/// </summary>
	public partial class RegistryDraftEditor : EditorWindow
	{
		/// <summary>
		///  親ウィンドウを指定して、
		///  型'<see cref="OSDeveloper.Core.Editors.RegistryDraftEditor"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		/// <param name="parent">親ウィンドウです。</param>
		public RegistryDraftEditor(MainWindowBase parent) : base(parent)
		{
			this.InitializeComponent();
		}

		/// <summary>
		///  親ウィンドウを指定せずに単独のウィンドウとして、
		///  型'<see cref="OSDeveloper.Core.Editors.RegistryDraftEditor"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		public RegistryDraftEditor()
		{
			this.InitializeComponent();
		}

		private void btnRefresh_Click(object sender, System.EventArgs e)
		{
			// 再読み込みする必要は無いので何もしない
		}

		private void btnExpand_Click(object sender, System.EventArgs e)
		{
			treeView.ExpandAll();
		}

		private void btnCollapse_Click(object sender, System.EventArgs e)
		{
			treeView.CollapseAll();
		}
	}
}
