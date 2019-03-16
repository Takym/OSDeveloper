using System.IO;

namespace OSDeveloper.IO
{
	public sealed class PathString
	{
		private readonly string _path, _value;

		public PathString(string path)
		{
			_path = Path.GetFullPath(path);
			_value = path;
		}

		#region パス文字列に対する操作
		public PathString Bond(string filename)
		{
			return new PathString(Path.Combine(_path, filename));
		}

		public PathString Combine(params PathString[] paths)
		{
			string[] vs = new string[paths.Length + 1];
			vs[0] = _path;
			for (int i = 1; i < vs.Length; ++i) {
				vs[i] = paths[i - 1]._path;
			}
			return new PathString(Path.Combine(vs));
		}

		public PathString Combine(params string[] paths)
		{
			string[] vs = new string[paths.Length + 1];
			vs[0] = _path;
			for (int i = 1; i < vs.Length; ++i) {
				vs[i] = paths[i - 1];
			}
			return new PathString(Path.Combine(vs));
		}

		public PathString ChangeExtension(string extension)
		{
			return new PathString(Path.ChangeExtension(_path, extension));
		}

		public PathString ChangeFileName(string filename)
		{
			return new PathString(Path.Combine(Path.GetDirectoryName(_path), filename));
		}

		public PathString GetDirectory()
		{
			return new PathString(Path.GetDirectoryName(_path));
		}
		#endregion

		#region パスに関する情報取得
		public string GetDirectoryName()
		{
			return Path.GetDirectoryName(_path);
		}

		public string GetFileName()
		{
			return Path.GetFileName(_path);
		}

		public string GetExtension()
		{
			// Remove(0, 1)で最初のピリオドを除外する。
			return Path.GetExtension(_path).Remove(0, 1);
		}

		public string GetFileNameWithoutExtension()
		{
			return Path.GetFileNameWithoutExtension(_path);
		}

		/// <summary>
		///  コンストラクタに渡されたパス文字列取得
		/// </summary>
		public string GetOriginalPath()
		{
			return _value;
		}

		public bool Exists()
		{
			return File.Exists(_path) || Directory.Exists(_path);
		}
		#endregion

		#region 比較処理
		public override bool Equals(object obj)
		{
			if (obj is PathString ps) {
				return _path == ps._path;
			}
			return false;
		}

		public static bool operator ==(PathString left, PathString right)
		{
			return left._path == right._path;
		}

		public static bool operator !=(PathString left, PathString right)
		{
			return left._path != right._path;
		}
		#endregion

		#region 変換処理
		public override string ToString()
		{
			return _path;
		}

		public static implicit operator string(PathString path) => path._path;
		public static explicit operator PathString(string path) => new PathString(path);
		#endregion

		#region その他
		public override int GetHashCode()
		{
			return _path.GetHashCode();
		}
		#endregion
	}
}
