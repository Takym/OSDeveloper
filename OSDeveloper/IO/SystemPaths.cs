using System;
using System.IO;
using System.Reflection;
using TakymLib.IO;

namespace OSDeveloper.IO
{
	public static class SystemPaths
	{
		/// <summary>
		///  プログラムが格納されているディレクトリ
		/// </summary>
		public static readonly PathString Program
			= ((PathString)(Assembly.GetExecutingAssembly().Location)).GetDirectory();

		/// <summary>
		///  非埋め込みリソースディレクトリ
		/// </summary>
		public readonly static PathString Resources
			= Program.Bond("Resources");

		/// <summary>
		///  限定設定ディレクトリ
		/// </summary>
		public readonly static PathString DefaultSettings
			= Program.Bond("Settings");

		/// <summary>
		///  システム拡張機能ディレクトリ
		/// </summary>
		public readonly static PathString SystemExtensions
			= Program.Bond("Extensions");

		/// <summary>
		///  ワークスペースディレクトリ
		/// </summary>
		public static PathString Workspace
		{
			get => _cwd;
		}
		private static PathString _cwd;

		/// <summary>
		///  ワークスペースの構成設定ディレクトリ
		/// </summary>
		public static PathString WorkspaceConfig
		{
			get => _cwd.Bond(".osdev");
		}

		/// <summary>
		///  環境設定ディレクトリ
		/// </summary>
		public static PathString Settings
		{
			get => WorkspaceConfig.Bond("config");
		}

		/// <summary>
		///  拡張機能ディレクトリ
		/// </summary>
		public static PathString Extensions
		{
			get => WorkspaceConfig.Bond("plugins");
		}

		/// <summary>
		///  一時ディレクトリ
		/// </summary>
		public static PathString Temporary
		{
			get => WorkspaceConfig.Bond("temp");
		}

		/// <summary>
		///  キャッシュディレクトリ
		/// </summary>
		public static PathString Caches
		{
			get => Temporary.Bond("caches");
		}

		/// <summary>
		///  バックアップディレクトリ
		/// </summary>
		public static PathString Backups
		{
			get => Temporary.Bond("backups");
		}

		/// <summary>
		///  ログディレクトリ
		/// </summary>
		public static PathString Logs
		{
			get => Temporary.Bond("logs");
		}

		static SystemPaths()
		{
			Directory.CreateDirectory(Resources);
			Directory.CreateDirectory(DefaultSettings);
			Directory.CreateDirectory(SystemExtensions);

#if DEBUG
			SetWorkspace(Program);
#else
			SetWorkspace(Environment.CurrentDirectory);
#endif
		}

		public static void SetWorkspace(string path)
		{
			_cwd = new PathString(path);
			Directory.CreateDirectory(_cwd);
			Directory.CreateDirectory(Settings);
			Directory.CreateDirectory(Extensions);
			Directory.CreateDirectory(Temporary);
			Directory.CreateDirectory(Caches);
			Directory.CreateDirectory(Backups);
			Directory.CreateDirectory(Logs);
			Environment.CurrentDirectory = _cwd;
			new DirectoryInfo(WorkspaceConfig).Attributes |= FileAttributes.Hidden;
		}
	}
}
