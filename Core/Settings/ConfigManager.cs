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
				if (_system == null) {
					_system = YenconParser.Load(PathOfSystem);
				}
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
		}

		/// <summary>
		///  現在の設定データをファイルに保存します。
		/// </summary>
		public static void Save()
		{
			YenconParser.Save(PathOfSystem, _system);
		}
	}
}
