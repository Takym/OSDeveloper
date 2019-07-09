using System;
using System.IO;
using System.Text;
using TakymLib.Resources;

namespace TakymLib.IO
{
	/// <summary>
	///  パス文字列を表します。このクラスは不変で、継承する事はできません。
	/// </summary>
	public sealed class PathString : IEquatable<PathString>, IComparable<PathString>, IComparable
	{
		private readonly string _path, _value;

		/// <summary>
		///  指定された文字列をパスとして設定して、
		///  型'<see cref="TakymLib.IO.PathString"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		/// <param name="path">新しいインスタンスに設定するパス文字列です。</param>
		/// <exception cref="System.ArgumentException">
		///  パス文字列が不適切な場合に発生します
		/// </exception>
		/// <exception cref="System.ArgumentNullException">
		///  <paramref name="path"/>が<see langword="null"/>の場合に発生します。
		/// </exception>
		public PathString(string path)
		{
			path = path ?? throw new ArgumentNullException(nameof(path));
			try {
				_path = Path.GetFullPath(path);
				_value = path;
			} catch (Exception e) {
				throw new ArgumentException(
					string.Format(ErrorMessages.PathString_InvalidFormat, path), nameof(path), e);
			}
		}

		#region パス文字列に対する操作

		/// <summary>
		///  現在のインスタンスをディレクトリ名と仮定して、
		///  指定されたファイル名を結合します。
		/// </summary>
		/// <param name="filename">ファイル名です。</param>
		/// <returns>結合して出来た新しいパス文字列を表すインスタンスです。</returns>
		/// <exception cref="System.ArgumentException">
		///  ファイル名が不適切な場合に発生します。
		/// </exception>
		/// <exception cref="System.ArgumentNullException">
		///  ファイル名が<see langword="null"/>の場合に発生します。
		/// </exception>
		public PathString Bond(string filename)
		{
			return new PathString(Path.Combine(_path, filename));
		}

		/// <summary>
		///  指定されたパス文字列を一つのパス文字列に結合します。
		/// </summary>
		/// <param name="paths">結合する複数のパス文字列です。</param>
		/// <returns>結合して出来た新しいパス文字列を表すインスタンスです。</returns>
		/// <exception cref="System.ArgumentException">
		///  パス文字列が不適切な場合に発生します。
		/// </exception>
		/// <exception cref="System.ArgumentNullException">
		///  パス文字列が<see langword="null"/>の場合に発生します。
		/// </exception>
		public PathString Combine(params PathString[] paths)
		{
			paths = paths ?? throw new ArgumentNullException(nameof(paths));
			string[] vs = new string[paths.Length + 1];
			vs[0] = _path;
			for (int i = 1; i < vs.Length; ++i) {
				vs[i] = paths[i - 1]._path;
			}
			return new PathString(Path.Combine(vs));
		}

		/// <summary>
		///  指定されたパス文字列を一つのパス文字列に結合します。
		/// </summary>
		/// <param name="paths">結合する複数のパス文字列です。</param>
		/// <returns>結合して出来た新しいパス文字列を表すインスタンスです。</returns>
		/// <exception cref="System.ArgumentException">
		///  パス文字列が不適切な場合に発生します。
		/// </exception>
		/// <exception cref="System.ArgumentNullException">
		///  パス文字列が<see langword="null"/>の場合に発生します。
		/// </exception>
		public PathString Combine(params string[] paths)
		{
			paths = paths ?? throw new ArgumentNullException(nameof(paths));
			string[] vs = new string[paths.Length + 1];
			vs[0] = _path;
			for (int i = 1; i < vs.Length; ++i) {
				vs[i] = paths[i - 1];
			}
			return new PathString(Path.Combine(vs));
		}

		/// <summary>
		///  現在のパス文字列の拡張子を変更します。
		/// </summary>
		/// <param name="extension">新しい拡張子です。</param>
		/// <returns>指定された拡張子を利用している新しいパス文字列のインスタンスです。</returns>
		/// <exception cref="System.ArgumentException">
		///  指定された拡張子が不適切な場合に発生します。
		/// </exception>
		public PathString ChangeExtension(string extension)
		{
			return new PathString(Path.ChangeExtension(_path, extension));
		}

		/// <summary>
		///  現在のパス文字列のファイル名を変更します。
		/// </summary>
		/// <param name="filename">新しいファイル名です。</param>
		/// <returns>指定されたファイル名を利用している新しいパス文字列のインスタンスです。</returns>
		/// <exception cref="System.ArgumentException">
		///  指定されたファイル名が不適切な場合に発生します。
		/// </exception>
		/// <exception cref="System.ArgumentNullException">
		///  <paramref name="filename"/>が<see langword="null"/>の場合に発生します。
		/// </exception>
		/// <exception cref="System.IO.PathTooLongException">
		///  新しいパス文字列が長過ぎる場合に発生します。
		/// </exception>
		public PathString ChangeFileName(string filename)
		{
			return new PathString(Path.Combine(Path.GetDirectoryName(_path), filename));
		}

		/// <summary>
		///  親ディレクトリを表すパス文字列を取得します。
		/// </summary>
		/// <returns>
		///  新しく生成された親ディレクトリを表すパス文字列です。
		///  親ディレクトリを持たない場合は<see langword="null"/>が返ります。
		/// </returns>
		/// <exception cref="System.ArgumentException">
		///  内部的な不具合により発生する可能性があります。通常は発生しません。
		/// </exception>
		/// <exception cref="System.ArgumentNullException">
		///  内部的な不具合により発生する可能性があります。通常は発生しません。
		/// </exception>
		/// <exception cref="System.IO.PathTooLongException">
		///  内部的な不具合により発生する可能性があります。通常は発生しません。
		/// </exception>
		public PathString GetDirectory()
		{
			string p = Path.GetDirectoryName(_path);
			if (string.IsNullOrEmpty(p)) {
				return null;
			} else {
				return new PathString(p);
			}
		}

		/// <summary>
		///  現在のパス文字列がファイルシステム上に存在せず、
		///  新しいファイルまたはディレクトリが生成可能なパス文字列を生成します。
		/// </summary>
		/// <returns>新たな新規作成可能なパス文字列です。</returns>
		/// <exception cref="System.ArgumentException">
		///  内部的な不具合により発生する可能性があります。通常は発生しません。
		/// </exception>
		/// <exception cref="System.ArgumentOutOfRangeException">
		///  内部的な不具合により発生する可能性があります。通常は発生しません。
		/// </exception>
		public PathString EnsureName()
		{
			int i = 0;
			var path = this;
			while (path.Exists()) {
				path = this.ChangeExtension(++i + "." + this.GetExtension());
			}
			return path;
		}

		#endregion

		#region パスに関する情報取得

		/// <summary>
		///  親ディレクトリ名を表す文字列を取得します。
		/// </summary>
		/// <returns>パス文字列ではなく標準型の<see cref="string"/>です。</returns>
		/// <exception cref="System.ArgumentException">
		///  内部的な不具合により発生する可能性があります。通常は発生しません。
		/// </exception>
		/// <exception cref="System.IO.PathTooLongException">
		///  内部的な不具合により発生する可能性があります。通常は発生しません。
		/// </exception>
		public string GetDirectoryName()
		{
			return Path.GetDirectoryName(_path);
		}

		/// <summary>
		///  ファイル名を表す文字列を取得します。
		/// </summary>
		/// <returns>パス文字列ではなく標準型の<see cref="string"/>です。</returns>
		/// <exception cref="System.ArgumentException">
		///  内部的な不具合により発生する可能性があります。通常は発生しません。
		/// </exception>
		public string GetFileName()
		{
			return Path.GetFileName(_path);
		}

		/// <summary>
		///  現在のパス文字列の拡張子を取得します。
		/// </summary>
		/// <returns>ピリオドを覗く拡張子を表す文字列です。</returns>
		/// <exception cref="System.ArgumentException">
		///  内部的な不具合により発生する可能性があります。通常は発生しません。
		/// </exception>
		/// <exception cref="System.ArgumentOutOfRangeException">
		///  内部的な不具合により発生する可能性があります。通常は発生しません。
		/// </exception>
		public string GetExtension()
		{
			string result = Path.GetExtension(_path);
			if (string.IsNullOrEmpty(result)) {
				return string.Empty;
			} else {
				// Remove(0, 1)で最初のピリオドを除去する。
				return result.Remove(0, 1);
			}
		}

		/// <summary>
		///  拡張子を覗くファイル名を表す文字列を取得します。
		/// </summary>
		/// <returns>パス文字列ではなく標準型の<see cref="string"/>です。</returns>
		/// <exception cref="System.ArgumentException">
		///  内部的な不具合により発生する可能性があります。通常は発生しません。
		/// </exception>
		public string GetFileNameWithoutExtension()
		{
			return Path.GetFileNameWithoutExtension(_path);
		}

		/// <summary>
		///  コンストラクタに渡されたパス文字列を取得します。
		/// </summary>
		/// <returns>型'<see cref="string"/>'の値です。</returns>
		public string GetOriginalPath()
		{
			return _value;
		}

		/// <summary>
		///  現在のインスタンスが格納している絶対パスを取得します。
		/// </summary>
		/// <returns>型'<see cref="string"/>'の値です。</returns>
		public string GetAbsolutePath()
		{
			return _path;
		}

		/// <summary>
		///  指定された基底パスを基に現在のパス文字列から相対パスを生成します。
		/// </summary>
		/// <param name="basePath">現在のパスを相対パスへ変換する為の基底パスです。</param>
		/// <returns><paramref name="basePath"/>を利用して元のパスへ復元可能な相対パスです。</returns>
		public string GetRelativePath(PathString basePath)
		{
			string[] tp =          _path.Split(Path.DirectorySeparatorChar);
			string[] bp = basePath._path.Split(Path.DirectorySeparatorChar);
			var rp = new StringBuilder();
			int i = 0;
			while (i < tp.Length && i < bp.Length && tp[i] == bp[i]) ++i;
			int j = i;
			for (; i < bp.Length; ++i) {
				rp.Append("..\\");
			}
			for (; j < tp.Length; ++j) {
				rp.Append(tp[j]).Append("\\");
			}
			rp.Remove(rp.Length - 1, 1);
			return rp.ToString();
		}

		/// <summary>
		///  現在のパス文字列がファイルシステム上に存在しているかどうか判定します。
		/// </summary>
		/// <returns>存在する場合は<see langword="true"/>、存在しない場合は<see langword="false"/>です。</returns>
		public bool Exists()
		{
			return File.Exists(_path) || Directory.Exists(_path);
		}
		#endregion

		#region 比較処理

		/// <summary>
		///  指定されたオブジェクトが現在のインスタンスの値と等しいかどうか比較します。
		/// </summary>
		/// <param name="obj">比較するオブジェクトです。</param>
		/// <returns>等しい場合は<see langword="true"/>、等しくない場合は<see langword="false"/>です。</returns>
		public override bool Equals(object obj)
		{
			if (obj is PathString ps) {
				return _path == ps._path;
			}
			return false;
		}

		/// <summary>
		///  指定されたオブジェクトが現在のインスタンスの値と等しいかどうか比較します。
		/// </summary>
		/// <param name="other">比較するオブジェクトです。</param>
		/// <returns>等しい場合は<see langword="true"/>、等しくない場合は<see langword="false"/>です。</returns>
		public bool Equals(PathString other)
		{
			if (other is null) return false;
			return _path == other._path;
		}

		/// <summary>
		///  指定されたオブジェクトと現在のインスタンスの値を比較します。
		/// </summary>
		/// <param name="obj">比較するオブジェクトです。</param>
		/// <returns>
		///  等しい場合は<c>0</c>、
		///  このインスタンスの方が大きい場合(並び替えで<paramref name="obj"/>が前方になる場合)は正の整数(<c>0</c>より大きい値)、
		///  このインスタンスの方が小さい場合(並び替えで<paramref name="obj"/>が後方になる場合)は負の整数(<c>0</c>より小さい値)
		///  を返します。
		/// </returns>
		/// <exception cref="System.ArgumentException"/>
		public int CompareTo(object obj)
		{
			if (obj is PathString ps) {
				return _path.CompareTo(ps._path);
			} else {
				return _path.CompareTo(obj);
			}
		}

		/// <summary>
		///  指定されたオブジェクトと現在のインスタンスの値を比較します。
		/// </summary>
		/// <param name="other">比較するオブジェクトです。</param>
		/// <returns>
		///  等しい場合は<c>0</c>、
		///  このインスタンスの方が大きい場合(並び替えで<paramref name="other"/>が前方になる場合)は正の整数(<c>0</c>より大きい値)、
		///  このインスタンスの方が小さい場合(並び替えで<paramref name="other"/>が後方になる場合)は負の整数(<c>0</c>より小さい値)
		///  を返します。</returns>
		public int CompareTo(PathString other)
		{
			return _path.CompareTo(other._path);
		}

		/// <summary>
		///  指定された二つのオブジェクトが等しいかどうか比較します。
		/// </summary>
		/// <param name="left">左辺側のオブジェクトです。</param>
		/// <param name="right">右辺側のオブジェクトです。</param>
		/// <returns>等しい場合は<see langword="true"/>、等しくない場合は<see langword="false"/>です。</returns>
		public static bool operator ==(PathString left, PathString right)
		{
			if ((left is null) != (right is null)) return false;
			if ((left is null) && (right is null)) return true;
			return left.Equals(right);
		}

		/// <summary>
		///  指定された二つのオブジェクトが等しくないかどうか比較します。
		/// </summary>
		/// <param name="left">左辺側のオブジェクトです。</param>
		/// <param name="right">右辺側のオブジェクトです。</param>
		/// <returns>等しくない場合は<see langword="true"/>、等しい場合は<see langword="false"/>です。</returns>
		public static bool operator !=(PathString left, PathString right)
		{
			return !(left == right);
		}

		/// <summary>
		///  <paramref name="left"/>が<paramref name="right"/>より小さいかどうか判定します。
		/// </summary>
		/// <param name="left">左辺側のオブジェクトです。</param>
		/// <param name="right">右辺側のオブジェクトです。</param>
		/// <returns>
		/// <paramref name="left"/>が<paramref name="right"/>より小さい場合は<see langword="true"/>、
		/// <paramref name="left"/>が<paramref name="right"/>以上の場合は<see langword="false"/>です。
		/// </returns>
		public static bool operator <(PathString left, PathString right)
		{
			return left?.CompareTo(right) < 0;
		}

		/// <summary>
		///  <paramref name="left"/>が<paramref name="right"/>より大きいかどうか判定します。
		/// </summary>
		/// <param name="left">左辺側のオブジェクトです。</param>
		/// <param name="right">右辺側のオブジェクトです。</param>
		/// <returns>
		/// <paramref name="left"/>が<paramref name="right"/>より大きい場合は<see langword="true"/>、
		/// <paramref name="left"/>が<paramref name="right"/>以下の場合は<see langword="false"/>です。
		/// </returns>
		public static bool operator >(PathString left, PathString right)
		{
			return left?.CompareTo(right) > 0;
		}

		/// <summary>
		///  <paramref name="left"/>が<paramref name="right"/>以下かどうか判定します。
		/// </summary>
		/// <param name="left">左辺側のオブジェクトです。</param>
		/// <param name="right">右辺側のオブジェクトです。</param>
		/// <returns>
		/// <paramref name="left"/>が<paramref name="right"/>以下の場合は<see langword="true"/>、
		/// <paramref name="left"/>が<paramref name="right"/>より大きい場合は<see langword="false"/>です。
		/// </returns>
		public static bool operator <=(PathString left, PathString right)
		{
			return left?.CompareTo(right) <= 0;
		}

		/// <summary>
		///  <paramref name="left"/>が<paramref name="right"/>以上かどうか判定します。
		/// </summary>
		/// <param name="left">左辺側のオブジェクトです。</param>
		/// <param name="right">右辺側のオブジェクトです。</param>
		/// <returns>
		/// <paramref name="left"/>が<paramref name="right"/>以上の場合は<see langword="true"/>、
		/// <paramref name="left"/>が<paramref name="right"/>より小さい場合は<see langword="false"/>です。
		/// </returns>
		public static bool operator >=(PathString left, PathString right)
		{
			return left?.CompareTo(right) >= 0;
		}

		#endregion

		#region 変換処理

		/// <summary>
		///  現在のパス文字列を文字列に変換します。
		/// </summary>
		/// <returns>現在のパス文字列が格納している文字列です。</returns>
		public override string ToString()
		{
			return _path;
		}

		/// <summary>
		///  パス文字列から文字列への暗黙的な変換を認めます。
		/// </summary>
		/// <param name="path">変換前のパス文字列です。</param>
		public static implicit operator string(PathString path) => path._path;

		/// <summary>
		///  文字列からパス文字列への明示的な変換を認めます。
		/// </summary>
		/// <param name="path">変換前の文字列です。</param>
		public static explicit operator PathString(string path) => new PathString(path);

		#endregion

		#region その他

		/// <summary>
		///  現在のパス文字列のハッシュ値を取得します。
		/// </summary>
		/// <returns>ハッシュ値を表す符号付き32ビット整数値です。</returns>
		public override int GetHashCode()
		{
			return _path.GetHashCode();
		}

		#endregion
	}
}
