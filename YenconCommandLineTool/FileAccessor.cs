using System;
using Yencon;
using YenconCommandLineTool.Resources;

namespace YenconCommandLineTool
{
	static class FileAccessor
	{
		static readonly YenconStringConverter _txt_cnvtr;
		static readonly YenconBinaryConverter _bin_cnvtr;
		static YenconType _last_type;

		static FileAccessor()
		{
			_txt_cnvtr = new YenconStringConverter();
			_bin_cnvtr = new YenconBinaryConverter();
			_last_type = YenconType.Resource;
		}

		public static YSection Load(string filename)
		{
#if RELEASE
			try {
#endif
				_last_type = YenconFormatRecognition.GetYenconType(filename);
				if (_last_type == YenconType.Text) {
					return _txt_cnvtr.Load(filename);
				} else if (_last_type == YenconType.Binary) {
					return _bin_cnvtr.Load(filename);
				} else {
					return null;
				}
#if RELEASE
			} catch (Exception e) {
				Program.ShowError(e);
				return null;
			}
#endif
		}

		public static void Save(string filename, YSection root)
		{
#if RELEASE
			try {
#endif
				if (_last_type == YenconType.Text) {
					_txt_cnvtr.Save(filename, root);
				} else if (_last_type == YenconType.Binary) {
					_bin_cnvtr.Save(filename, root);
				} else {
					_last_type = YenconType.Text;
					_txt_cnvtr.Save(filename, root);
				}
#if RELEASE
			} catch (Exception e) {
				Program.ShowError(e);
			}
#endif
		}

		public static YSection LoadTxt(string filename)
		{
#if RELEASE
			try {
#endif
				_last_type = YenconType.Text;
				return _txt_cnvtr.Load(filename);
#if RELEASE
			} catch (Exception e) {
				Program.ShowError(e);
				return null;
			}
#endif
		}

		public static void SaveTxt(string filename, YSection root)
		{
#if RELEASE
			try {
#endif
				_last_type = YenconType.Text;
				_txt_cnvtr.Save(filename, root);
#if RELEASE
			} catch (Exception e) {
				Program.ShowError(e);
			}
#endif
		}

		public static YSection LoadBin(string filename)
		{
#if RELEASE
			try {
#endif
				_last_type = YenconType.Binary;
				return _bin_cnvtr.Load(filename);
#if RELEASE
			} catch (Exception e) {
				Program.ShowError(e);
				return null;
			}
#endif
		}

		public static void SaveBin(string filename, YSection root)
		{
#if RELEASE
			try {
#endif
				_last_type = YenconType.Binary;
				_bin_cnvtr.Save(filename, root);
#if RELEASE
			} catch (Exception e) {
				Program.ShowError(e);
			}
#endif
		}

		public static void ShowBinHeader()
		{
			Console.WriteLine($"{Messages.ShowBinHeader_KeyNameSize}: {_bin_cnvtr.Header.KeyNameSize}");
			Console.WriteLine($"{Messages.ShowBinHeader_KeyNameType}: {_bin_cnvtr.Header.KeyNameType}");
			Console.WriteLine("{3}: {4}:0x{0:X2}, {5}:0x{1:X2}, {6}:0x{2:X2}",
				_bin_cnvtr.Header.Implementation,
				_bin_cnvtr.Header.Compatibility,
				_bin_cnvtr.Header.Revision,
				Messages.ShowBinHeader_Version,
				Messages.ShowBinHeader_Implementation,
				Messages.ShowBinHeader_Compatibility,
				Messages.ShowBinHeader_Revision);
		}
	}
}
