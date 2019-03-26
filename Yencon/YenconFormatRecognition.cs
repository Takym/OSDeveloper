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
		///  指定されたファイルのヱンコンの種類を判定します。
		/// </summary>
		/// <param name="filename">判定するファイルのファイル名です。</param>
		/// <returns>ヱンコンの種類を表す型'<see cref="Yencon.YenconType"/>'の値です。</returns>
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
