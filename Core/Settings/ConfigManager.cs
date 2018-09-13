using System.IO;
using System.Text;
using OSDeveloper.Core.FileManagement;

namespace OSDeveloper.Core.Settings
{
	/// <summary>
	///  ソフトウェア全体の設定を管理します。
	/// </summary>
	public static class ConfigManager
	{
		/// <summary>
		///  システムの設定を取得します。
		///  この設定を変更した場合はアプリケーションの再起動が必要になる可能性があります。
		/// </summary>
		public static YenconSection System
		{
			get
			{
				return _system;
			}
		}
		private static YenconSection _system;

		/// <summary>
		///  <see cref="OSDeveloper.Core.Settings.ConfigManager.System"/>
		///  の設定データが格納されているファイルのファイルパスを取得します。
		/// </summary>
		public static PathString PathOfSystem { get; }

		static ConfigManager()
		{
			PathOfSystem = SystemPaths.Settings.Bond("system.inix");
			Load();
		}

		/// <summary>
		///  現在の設定を破棄して、ファイルから設定データを読み込みます。
		///  <see cref="OSDeveloper.Core.Settings.YenconSection"/>のインスタンスも再生成されます。
		/// </summary>
		public static void Load()
		{
			// ---- System ----
			using (FileStream fs = new FileStream(PathOfSystem, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read))
			using (StreamReader sr = new StreamReader(fs, Encoding.Unicode)) {
				var parser = new YenconParser(sr.ReadToEnd());
				parser.Analyze();
				_system = new YenconSection(parser.GetNodes());
			}
		}

		/// <summary>
		///  現在の設定データをファイルに保存します。
		/// </summary>
		public static void Save()
		{
			using (StreamWriter sw = new StreamWriter(SystemPaths.Settings.Bond("system.inix"), false, Encoding.Unicode)) {
				sw.Write(_system.ToStringWithoutBrace());
			}
		}
	}
}
