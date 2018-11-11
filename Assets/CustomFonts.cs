using System.Drawing.Text;
using System.Globalization;
using OSDeveloper.Core.FileManagement;
using OSDeveloper.Core.Logging;

namespace OSDeveloper.Assets
{
	internal static class CustomFonts
	{
		private static Logger _logger;
		private static PrivateFontCollection _pfc;

		static CustomFonts()
		{
			_logger = Logger.GetSystemLogger(nameof(CustomFonts));
			_pfc = new PrivateFontCollection();
			_pfc.AddFontFile(SystemPaths.Resources.Bond("IPAexfont00301\\ipaexg.ttf"));
			_pfc.AddFontFile(SystemPaths.Resources.Bond("IPAexfont00301\\ipaexm.ttf"));

			for (int i = 0; i < _pfc.Families.Length; ++i) {
				var item = _pfc.Families[i];
				string enus = item.GetName(CultureInfo.GetCultureInfo("en_US").LCID);
				string jajp = item.GetName(CultureInfo.GetCultureInfo("ja_JP").LCID);
				string cc = item.GetName(CultureInfo.CurrentCulture.LCID);
				_logger.Info($"Loaded a font: {i}, {item.Name}; EN:{enus}; 日:{jajp}; ::: {cc}");
			}
		}
	}
}
