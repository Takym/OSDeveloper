using System;
using System.IO;
using Yencon.Exceptions;
using Yencon.Resources;

namespace Yencon
{
	/// <summary>
	///  ヱンコンの種類を判定します。このクラスは静的です。
	/// </summary>
	public static class YenconFormatRecognition
	{
		/// <summary>
		///  ヱンコンとテキストの変換器を取得します。
		/// </summary>
		public static YenconStringConverter StringConverter { get; }

		/// <summary>
		///  ヱンコンとバイナリの変換器を取得します。
		/// </summary>
		public static YenconBinaryConverter BinaryConverter { get; }

		static YenconFormatRecognition()
		{
			StringConverter = new YenconStringConverter();
			BinaryConverter = new YenconBinaryConverter();
		}

		/// <summary>
		///  指定されたファイルのヱンコンの種類を判定します。
		/// </summary>
		/// <param name="filename">判定するファイルのファイル名です。</param>
		/// <returns>ヱンコンの種類を表す型'<see cref="Yencon.YenconType"/>'の値です。</returns>
		/// <exception cref="System.IO.IOException">
		///  ファイルの入力に失敗した場合に発生します。
		/// </exception>
		public static YenconType GetYenconType(string filename)
		{
			try {
				YenconType result;
				using (var fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
				using (var br = new BinaryReader(fs))
				using (var sr = new StreamReader(fs)) {
					var hdr = new YenconBinaryHeader();
					try {
						hdr.FromBinary(br.ReadBytes(12));
						result = hdr.CheckVersion() ? YenconType.Binary : YenconType.Unknown;
					} catch (InvalidHeaderException) {
						fs.Position = 0;
						var tknzr = new YenconStringTokenizer(sr.ReadToEnd());
						try {
							tknzr.Scan();
							result = YenconType.Text;
						} catch (InvalidSyntaxException) {
							result = YenconType.Unknown;
						}
					}
				}
				return result;
			} catch (Exception e) {
				throw new IOException(string.Format(ErrorMessages.YenconFormatRecognition_IOException, filename), e);
			}
		}

		/// <summary>
		///  指定されたファイルを読み込みます。ヱンコンの種類は自動的に判別されます。
		/// </summary>
		/// <param name="filename">読み込むヱンコン環境設定ファイルの名前です。</param>
		/// <returns>
		///  指定されたファイルがヱンコン環境設定ファイルなら新しい型'<see cref="Yencon.YSection"/>'のインスタンス、
		///  それ以外の場合は<see langword="null"/>を返します。
		/// </returns>
		/// <exception cref="System.IO.IOException">
		///  ファイルの入力に失敗した場合に発生します。
		/// </exception>
		public static YSection Load(string filename)
		{
			var type = GetYenconType(filename);
			try {
				if (type == YenconType.Text) {
					return StringConverter.Load(filename);
				} else if (type == YenconType.Binary) {
					return BinaryConverter.Load(filename);
				} else {
					return null;
				}
			} catch (Exception e) {
				throw new IOException(string.Format(ErrorMessages.YenconFormatRecognition_IOException, filename), e);
			}
		}

		/// <summary>
		///  指定されたファイルに指定されたヱンコンオブジェクトを書き込みます。
		/// </summary>
		/// <param name="filename"><paramref name="obj"/>の保存先のファイル名です。</param>
		/// <param name="obj">保存するヱンコンオブジェクトです。</param>
		/// <param name="type">ヱンコンの種類です。既定でバイナリ形式です。</param>
		/// <exception cref="System.IO.IOException">
		///  ファイルの出力に失敗した場合に発生します。
		/// </exception>
		public static void Save(string filename, YSection obj, YenconType type = YenconType.Binary)
		{
			try {
				if (type == YenconType.Text) {
					StringConverter.Save(filename, obj);
				} else if (type == YenconType.Binary) {
					BinaryConverter.Save(filename, obj);
				}
			} catch (Exception e) {
				throw new IOException(string.Format(ErrorMessages.YenconFormatRecognition_IOException, filename), e);
			}
		}
	}

	/// <summary>
	///  ヱンコンの種類を表します。
	/// </summary>
	public enum YenconType
	{
		/// <summary>
		///  リソース形式を表します。
		/// </summary>
		Resource,

		/// <summary>
		///  テキスト形式を表します。
		/// </summary>
		Text,

		/// <summary>
		///  バイナリ形式を表します。
		/// </summary>
		Binary,

		/// <summary>
		///  不明の種類を表します。この値は<see cref="Yencon.YenconType.Resource"/>と同じです。
		/// </summary>
		Unknown = Resource
	}
}
