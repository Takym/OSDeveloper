using System;
using System.IO;
using System.Text;

namespace Yencon
{
	/// <summary>
	///  ヱンコンをテキスト形式に変換します。
	/// </summary>
	public class YenconStringConverter : IYenconConverter<string>
	{
		private YenconStringParser _parser;

		/// <summary>
		///  型'<see cref="Yencon.YenconStringConverter"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		public YenconStringConverter()
		{
			_parser = new YenconStringParser();
		}

		/// <summary>
		///  指定されたファイルからテキスト形式のヱンコンを読み取ります。
		/// </summary>
		/// <param name="filename">読み込み元のファイルのパスです。</param>
		/// <returns>読み込んだ全ての情報を保持するセクションオブジェクトです。</returns>
		/// <exception cref="Yencon.Exceptions.InvalidSyntaxException">
		///  不正なヱンコン文字列が渡された場合に発生します。
		/// </exception>
		/// <exception cref="System.ArgumentException">
		///  この関数で利用しているオブジェクトがこの例外を発生させる可能性があります。
		///  <see cref="System.IO.StreamReader.StreamReader(string, Encoding, bool)"/>が原因に成り得ます。
		/// </exception>
		/// <exception cref="System.ArgumentNullException">
		///  この関数で利用しているオブジェクトがこの例外を発生させる可能性があります。
		///  <see cref="System.IO.StreamReader.StreamReader(string, Encoding, bool)"/>が原因に成り得ます。
		/// </exception>
		/// <exception cref="System.IO.FileNotFoundException">
		///  この関数で利用しているオブジェクトがこの例外を発生させる可能性があります。
		///  <see cref="System.IO.StreamReader.StreamReader(string, Encoding, bool)"/>が原因に成り得ます。
		/// </exception>
		/// <exception cref="System.IO.DirectoryNotFoundException">
		///  この関数で利用しているオブジェクトがこの例外を発生させる可能性があります。
		///  <see cref="System.IO.StreamReader.StreamReader(string, Encoding, bool)"/>が原因に成り得ます。
		/// </exception>
		/// <exception cref="System.NotSupportedException">
		///  この関数で利用しているオブジェクトがこの例外を発生させる可能性があります。
		///  <see cref="System.IO.StreamReader.StreamReader(string, Encoding, bool)"/>が原因に成り得ます。
		/// </exception>
		/// <exception cref="System.OutOfMemoryException">
		///  この関数で利用しているオブジェクトがこの例外を発生させる可能性があります。
		///  <see cref="System.IO.StreamReader.ReadToEnd"/>が原因に成り得ます。
		/// </exception>
		/// <exception cref="System.IO.IOException">
		///  この関数で利用しているオブジェクトがこの例外を発生させる可能性があります。
		///  <see cref="System.IO.StreamReader.ReadToEnd"/>が原因に成り得ます。
		/// </exception>
		public YSection Load(string filename)
		{
			using (var sr = new StreamReader(filename, Encoding.Unicode, true)) {
				return this.ToYencon(sr.ReadToEnd());
			}
		}

		/// <summary>
		///  指定されたファイルに指定されたヱンコン値をテキスト形式で保存します。
		/// </summary>
		/// <param name="filename">書き込み先のファイルのパスです。</param>
		/// <param name="obj">書き込む全ての情報を保持するセクションオブジェクトです。</param>
		/// <exception cref="System.ArgumentNullException">
		///  <paramref name="obj"/>が<see langword="null"/>の場合に発生します。
		/// </exception>
		/// <exception cref="System.UnauthorizedAccessException">
		///  この関数で利用しているオブジェクトがこの例外を発生させる可能性があります。
		///  <see cref="System.IO.StreamWriter.StreamWriter(string, bool, Encoding)"/>が原因に成り得ます。
		/// </exception>
		/// <exception cref="System.ArgumentException">
		///  この関数で利用しているオブジェクトがこの例外を発生させる可能性があります。
		///  <see cref="System.IO.StreamWriter.StreamWriter(string, bool, Encoding)"/>が原因に成り得ます。
		/// </exception>
		/// <exception cref="System.IO.DirectoryNotFoundException">
		///  この関数で利用しているオブジェクトがこの例外を発生させる可能性があります。
		///  <see cref="System.IO.StreamWriter.StreamWriter(string, bool, Encoding)"/>が原因に成り得ます。
		/// </exception>
		/// <exception cref="System.IO.IOException">
		///  この関数で利用しているオブジェクトがこの例外を発生させる可能性があります。
		///  <see cref="System.IO.StreamWriter.StreamWriter(string, bool, Encoding)"/>と
		///  <see cref="System.IO.StreamWriter.Write(string)"/>が原因に成り得ます。
		/// </exception>
		/// <exception cref="System.IO.PathTooLongException">
		///  この関数で利用しているオブジェクトがこの例外を発生させる可能性があります。
		///  <see cref="System.IO.StreamWriter.StreamWriter(string, bool, Encoding)"/>が原因に成り得ます。
		/// </exception>
		/// <exception cref="System.Security.SecurityException">
		///  この関数で利用しているオブジェクトがこの例外を発生させる可能性があります。
		///  <see cref="System.IO.StreamWriter.StreamWriter(string, bool, Encoding)"/>が原因に成り得ます。
		/// </exception>
		/// <exception cref="System.ObjectDisposedException">
		///  この関数で利用しているオブジェクトがこの例外を発生させる可能性があります。
		///  <see cref="System.IO.StreamWriter.Write(string)"/>が原因に成り得ます。
		/// </exception>
		/// <exception cref="System.NotSupportedException">
		///  この関数で利用しているオブジェクトがこの例外を発生させる可能性があります。
		///  <see cref="System.IO.StreamWriter.Write(string)"/>が原因に成り得ます。
		/// </exception>
		public void Save(string filename, YSection obj)
		{
			using (var sw = new StreamWriter(filename, false, Encoding.Unicode)) {
				sw.Write(this.ToObject(obj));
			}
		}

		/// <summary>
		///  指定された文字列をヱンコンセクションに変換します。
		/// </summary>
		/// <param name="obj">ヱンコンセクションに変換する文字列です。</param>
		/// <returns>
		///  変換後のデータを保持するヱンコンセクションです。
		/// </returns>
		/// <exception cref="Yencon.Exceptions.InvalidSyntaxException">
		///  不正なヱンコン文字列が渡された場合に発生します。
		/// </exception>
		public YSection ToYencon(string obj)
		{
			var tokenizer = new YenconStringTokenizer(obj);
			return _parser.Parse(tokenizer.Scan());
		}

		/// <summary>
		///  指定されたヱンコンセクションを文字列に変換します。
		/// </summary>
		/// <param name="obj">文字列に変換するヱンコンセクションです。</param>
		/// <returns>変換後のデータを保持する文字列です。</returns>
		/// <exception cref="System.ArgumentNullException">
		///  <paramref name="obj"/>が<see langword="null"/>の場合に発生します。
		/// </exception>
		public string ToObject(YSection obj)
		{
			return obj?.ToString() ?? throw new ArgumentNullException(nameof(obj));
		}
	}
}
