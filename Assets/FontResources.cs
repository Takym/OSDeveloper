using System.Drawing;
using System.Drawing.Text;
using System.Globalization;
using OSDeveloper.Core.FileManagement;
using OSDeveloper.Core.Logging;

namespace OSDeveloper.Assets
{
	/// <summary>
	///  <see langword="OSDeveloper"/>用の追加フォントを利用します。
	/// </summary>
	public static class FontResources
	{
		private static Logger _logger;
		private static PrivateFontCollection _pfc;

		static FontResources()
		{
			_logger = Logger.GetSystemLogger(nameof(FontResources));
			_logger.Trace("constructing the new static instance...");

			_pfc = new PrivateFontCollection();
			_pfc.AddFontFile(SystemPaths.Resources.Bond("IPAexfont00301\\ipaexg.ttf"));
			_pfc.AddFontFile(SystemPaths.Resources.Bond("IPAexfont00301\\ipaexm.ttf"));

			for (int i = 0; i < _pfc.Families.Length; ++i) {
				var item = _pfc.Families[i];
				string enus = item.GetName(CultureInfo.GetCultureInfo("en-US").LCID);
				string jajp = item.GetName(CultureInfo.GetCultureInfo("ja-JP").LCID);
				string cc = item.GetName(CultureInfo.CurrentCulture.LCID);
				_logger.Info($"Loaded a font: {i}, {item.Name}; EN:{enus}; 日:{jajp}; ::: {cc}");
			}

			_logger.Trace("constructed successfully");
		}

		/// <summary>
		///  静的コンストラクタが一度も呼び出されていない場合は、
		///  初期化を開始し、既に呼び出されている場合は何もしません。
		/// </summary>
		public static void Init()
		{
			_logger.Trace($"Initialized {nameof(FontResources)}");
		}

		/// <summary>
		///  ゴシック体のフォントを新しく生成します。
		/// </summary>
		/// <returns>生成されたフォントオブジェクトです。</returns>
		public static Font CreateGothic()
		{
			return new Font(_pfc.Families[0], 12, FontStyle.Regular, GraphicsUnit.Point);
		}

		/// <summary>
		///  明朝体のフォントを新しく生成します。
		/// </summary>
		/// <returns>生成されたフォントオブジェクトです。</returns>
		public static Font CreateSerif()
		{
			return new Font(_pfc.Families[1], 12, FontStyle.Regular, GraphicsUnit.Point);
		}
	}
}
