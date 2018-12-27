using System;
using System.IO;
using System.Windows.Forms;
using OSDeveloper.Core.MiscUtils;

namespace OSDeveloper.Core.FileManagement
{
	/// <summary>
	///  <see langword="OSDeveloper"/>で利用されるディレクトリのパスを管理します。
	///  このクラスは静的です。
	/// </summary>
	public static class SystemPaths
	{
		/// <summary>
		///  <see langword="OSDeveloper"/>のプログラムが格納されているディレクトリを取得します。
		/// </summary>
		public readonly static PathString Program
			= ((PathString)(Application.StartupPath));

		/// <summary>
		///  リソースファイルを格納しているディレクトリのファイルパスを取得します。
		/// </summary>
		public readonly static PathString Resources
			= ((PathString)(Path.Combine(Application.StartupPath, "Assets")));

		/// <summary>
		///  外部ツールを格納しているディレクトリのファイルパスを取得します。
		/// </summary>
		public readonly static PathString Toolkits
			= ((PathString)(Path.Combine(Application.StartupPath, "ExternalTools")));

		/// <summary>
		///  現在のワークスペースディレクトリのパスを取得します。
		/// </summary>
		public static PathString Workspace
		{
			get
			{
				return _cwd;
			}
		}
		private static PathString _cwd;

		/// <summary>
		///  設定データを格納しているディレクトリのファイルパスを取得します。
		/// </summary>
		public static PathString Settings
		{
			get
			{
				return _cwd.Bond("Settings");
			}
		}

		/// <summary>
		///  拡張機能を格納しているディレクトリのファイルパスを取得します。
		/// </summary>
		public static PathString Extension
		{
			get
			{
				return _cwd.Bond("Extension");
			}
		}

		/// <summary>
		///  一時ファイルを格納しているディレクトリのファイルパスを取得します。
		/// </summary>
		public static PathString Temporary
		{
			get
			{
				return _cwd.Bond("Temp");
			}
		}

		/// <summary>
		///  貯蔵されたファイルを格納しているディレクトリのファイルパスを取得します。
		/// </summary>
		public static PathString Caches
		{
			get
			{
				return _cwd.Bond("Temp\\Caches");
			}
		}

		/// <summary>
		///  一時的なバックアップファイルを格納しているディレクトリのファイルパスを取得します。
		/// </summary>
		public static PathString Backups
		{
			get
			{
				return _cwd.Bond("Temp\\Backups");
			}
		}

		/// <summary>
		///  ログファイルを格納しているディレクトリのファイルパスを取得します。
		/// </summary>
		public static PathString Logs
		{
			get
			{
				return _cwd.Bond("Temp\\Logs");
			}
		}

		static SystemPaths()
		{
			if (WinFormUtils.DesignMode) {
				// デザイン時は一時ディレクトリにする
				SetWorkspace(((PathString)(Path.GetTempPath())).Bond(Path.GetRandomFileName()));
			} else {
				Directory.CreateDirectory(Resources);
				Directory.CreateDirectory(Toolkits);

#if DEBUG
				SetWorkspace(Program); // デバッグ時はアプリケーションの配置ディレクトリを、ワークスペースにする
#else
				SetWorkspace(new PathString(Environment.CurrentDirectory));
#endif
			}
		}

		/// <summary>
		///  ワークスペースディレクトリを設定します。
		/// </summary>
		/// <param name="cwd">新しいワークスペースディレクトリのパス文字列です。</param>
		public static void SetWorkspace(PathString cwd)
		{
			// HACK: ワークスペースを変更した時に必要な動作を全てここに書く。
			_cwd = new PathString(Path.GetFullPath(cwd));
			Directory.CreateDirectory(_cwd);

			// ディレクトリ生成
			Directory.CreateDirectory(Workspace);
			Directory.CreateDirectory(Settings);
			Directory.CreateDirectory(Extension);
			Directory.CreateDirectory(Temporary);
			Directory.CreateDirectory(Caches);
			Directory.CreateDirectory(Backups);
			Directory.CreateDirectory(Logs);

			// 準備が終わったら、ワークスペースを作業ディレクトリに設定
			Environment.CurrentDirectory = _cwd;
		}
	}

	/// <summary>
	///  ディレクトリとファイルのパスを表す文字列を保持する不変型です。
	///  このクラスは継承できません。
	/// </summary>
	public sealed class PathString//: System.String
	{
		private readonly string _value;

		/// <summary>
		///  型'<see cref="OSDeveloper.Core.FileManagement.PathString"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		/// <param name="path">このオブジェクトが保持するパス文字列です。</param>
		public PathString(string path)
		{
			_value = path;
		}

		/// <summary>
		///  指定されたファイル名を現在のインスタンスが保持しているパスと結合します。
		/// </summary>
		/// <param name="filename">結合対象のファイル名です。</param>
		/// <returns>結合されてできた新しいパス文字列です。</returns>
		public PathString Bond(string filename)
		{
			return new PathString(Path.Combine(_value, filename));
		}

		/// <summary>
		///  このパスの親ディレクトリを取得します。
		/// </summary>
		/// <returns>親ディレクトリを表すパス文字列です。</returns>
		public PathString GetParent()
		{
			return new PathString(Path.GetDirectoryName(_value));
		}

		/// <summary>
		///  このオブジェクトが格納しているファイルパスを文字列として返します。
		///  実際の変換処理は行われません。
		/// </summary>
		/// <returns>パス文字列です。</returns>
		public override string ToString()
		{
			return _value;
		}

#pragma warning disable CS1591 // 公開されている型またはメンバーの XML コメントがありません
		public static implicit operator string(PathString path) => path._value;
		public static explicit operator PathString(string path) => new PathString(path);
#pragma warning restore CS1591 // 公開されている型またはメンバーの XML コメントがありません
	}
}
