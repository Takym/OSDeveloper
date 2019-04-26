using System;
using System.IO;
using OSDeveloper.IO.Logging;
using TakymLib.IO;
using Yencon;

namespace OSDeveloper.IO.Configuration
{
	public static partial class SettingManager
	{
		private static bool                  _is_initialized;
		private static Logger                _logger;
		private static YenconStringConverter _txt_cnvtr;
		private static YenconBinaryConverter _bin_cnvtr;
		private static YSection              _y_system_inix, _y_system_cfg;
		private static PathString            _p_system_inix, _p_system_cfg;

		#region コンストラクタ/デストラクタ
		static SettingManager()
		{
			Init();
		}

		internal static void Init()
		{
			if (!_is_initialized) {
				_logger = Logger.Get(nameof(SettingManager));
				_txt_cnvtr = new YenconStringConverter();
				_bin_cnvtr = new YenconBinaryConverter();
				_is_initialized = true;

				Load();
				_logger.Trace("constructed the setting manager");
			}
		}

		internal static void Final()
		{
			if (_is_initialized) {
				_logger.Trace("destructing the setting manager...");
				Save();

				_txt_cnvtr = null;
				_bin_cnvtr = null;
				_logger = null;
				_is_initialized = false;
			}
		}
		#endregion

		#region Load/Save
		public static void Load()
		{
			if (!_is_initialized) throw new ObjectDisposedException(nameof(SettingManager));

			{ // system
				_p_system_inix = SystemPaths.       Settings.Bond("system.inix");
				_p_system_cfg  = SystemPaths.DefaultSettings.Bond("system.cfg");
				_y_system_inix = File.Exists(_p_system_inix) ? _txt_cnvtr.Load(_p_system_inix) : new YSection();
				_y_system_cfg  = File.Exists(_p_system_cfg ) ? _bin_cnvtr.Load(_p_system_cfg ) : new YSection();
				System.EnsureAvailable();
				_logger.Info("loaded the configuration data of \'system\'");
			} // system
		}

		public static void Save()
		{
			if (!_is_initialized) throw new ObjectDisposedException(nameof(SettingManager));

			{ // system
				_txt_cnvtr.Save(_p_system_inix, _y_system_inix);
				_bin_cnvtr.Save(_p_system_cfg,  _y_system_cfg);
			} // system
		}
		#endregion

		[Obsolete("既定値の設定が遅いので非推奨", true)]
		private static T GetKey<T>(YSection inix, YSection cfg, string keyName, T defaultValue) where T : YNode
		{
			defaultValue.Name = keyName;
			return inix.GetNode(keyName)  as T
				?? cfg .GetNode(keyName)  as T
				?? cfg .Add(defaultValue) as T;
		}

		private static T GetKey<T>(YSection inix, YSection cfg, string keyName, Func<T> defaultValueFactory) where T : YNode
		{
			var result = inix.GetNode(keyName) as T ?? cfg.GetNode(keyName) as T;
			if (result == null) {
				result = defaultValueFactory();
				result.Name = keyName;
				cfg.Add(result);
			}
			return result;
		}
	}
}
