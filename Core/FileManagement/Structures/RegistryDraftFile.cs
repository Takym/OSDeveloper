using OSDeveloper.Core.Editors;
using OSDeveloper.Core.GraphicalUIs;

namespace OSDeveloper.Core.FileManagement.Structures
{
	/// <summary>
	///  レジストリの下書きを表すファイルです。
	/// </summary>
	public class RegistryDraftFile : FileMetadata
	{
		/// <summary>
		///  ファイルのフルパスを指定して、
		///  型'<see cref="OSDeveloper.Core.FileManagement.Structures.RegistryDraftFile"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		/// <param name="filename">読み込むファイルの名前です。</param>
		public RegistryDraftFile(string filename) : base(filename, FileFormat.TextFile) { }

		/// <summary>
		///  このファイルを編集するためのエディタウィンドウを生成します。
		/// </summary>
		/// <param name="mwndbase">このエディタのMDI親ウィンドウです。</param>
		/// <returns>新しく生成されたエディタウィンドウオブジェクトです。</returns>
		public override EditorWindow CreateEditor(MainWindowBase mwndbase)
		{
			var result = new RegistryDraftEditor(mwndbase);
			result.TargetFile = this;
			return result;
		}
	}
}
