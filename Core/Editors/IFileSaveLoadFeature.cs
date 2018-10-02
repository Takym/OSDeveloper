using OSDeveloper.Core.FileManagement;

namespace OSDeveloper.Core.Editors
{
	/// <summary>
	///  ファイルの保存と読み込みをサポートするエディタを表します。
	/// </summary>
	public interface IFileSaveLoadFeature
	{
		/// <summary>
		///  このエディタで編集するファイルを取得または設定します。
		/// </summary>
		FileMetadata TargetFile { get; set; }

		/// <summary>
		///  このエディタがサポートしているファイルの種類を取得します。
		/// </summary>
		/// <returns>ファイルの種類を表すオブジェクトです。</returns>
		FileType GetFileType();

		/// <summary>
		///  ファイルを<see cref="OSDeveloper.Core.Editors.IFileSaveLoadFeature.TargetFile"/>に保存します。
		/// </summary>
		void Save();

		/// <summary>
		///  ファイルを別名で保存します。
		///  <see cref="OSDeveloper.Core.Editors.IFileSaveLoadFeature.TargetFile"/>は新しいファイルのファイル情報へ変更されます。
		/// </summary>
		/// <param name="path">保存先のファイルのパスです。</param>
		void SaveAs(string path);

		/// <summary>
		///  ファイルを再度読み込みます。変更内容は破棄されます。
		/// </summary>
		void Reload();

		/// <summary>
		///  ファイルを別の場所から読み込み、<see cref="OSDeveloper.Core.Editors.IFileSaveLoadFeature.TargetFile"/>を書き換えます。
		/// </summary>
		/// <param name="path">読み込み元のファイルのパスです。</param>
		/// <param name="saveCurrent">
		///  現在の変更内容を保存してから別のファイルを開く場合は<see langword="true"/>、
		///  保存しないで開く場合は<see langword="false"/>です。
		/// </param>
		void LoadFrom(string path, bool saveCurrent);
	}
}
