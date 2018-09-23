using System;
using System.Reflection;

namespace OSDeveloper.Core.FileManagement
{
	/// <summary>
	///  ファイルの種類を表します。この型は不変です。
	/// </summary>
	public sealed class FileType
	{
		/// <summary>
		///  このファイルのファイル形式を取得します。
		/// </summary>
		public FileFormat Format { get; }

		/// <summary>
		///  このファイルの種類を表す文字列を取得します。
		/// </summary>
		public string Name { get; }

		/// <summary>
		///  このファイルのピリオドの付かない拡張子の一覧を表す配列を取得します。
		/// </summary>
		public string[] Extensions { get; }

		/// <summary>
		///  ファイルを表すメタ情報の種類を取得します。
		/// </summary>
		public Type MetadataType { get; }

		private ConstructorInfo _ctor_of_metadata;

		/// <summary>
		///  ファイルのフォーマットと種類と拡張子を指定して、
		///  型'<see cref="OSDeveloper.Core.FileManagement.FileType"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		/// <param name="format">ファイルのフォーマットです。</param>
		/// <param name="name">ファイルの種類です。</param>
		/// <param name="ext">ァイルのピリオドの付かない拡張子の一覧を表す配列です。</param>
		public FileType(FileFormat format, string name, params string[] ext)
		{
			this.Format = format;
			this.Name = name;
			this.Extensions = ext;
			this.MetadataType = typeof(FileMetadata);
			_ctor_of_metadata = this.MetadataType.GetConstructor(new Type[] { typeof(string) });
		}

		/// <summary>
		///  ファイルのフォーマットと種類と拡張子を指定して、
		///  型'<see cref="OSDeveloper.Core.FileManagement.FileType"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		/// <param name="format">ファイルのフォーマットです。</param>
		/// <param name="metadataType">ファイルを表すメタ情報の種類です。</param>
		/// <param name="name">ファイルの種類です。</param>
		/// <param name="ext">ァイルのピリオドの付かない拡張子の一覧を表す配列です。</param>
		/// <exception cref="System.ArgumentException" />
		public FileType(FileFormat format, Type metadataType, string name, params string[] ext)
		{
			this.Format = format;
			this.Name = name;
			this.Extensions = ext;

			if (typeof(FileMetadata).IsAssignableFrom(metadataType)) {
				_ctor_of_metadata = metadataType.GetConstructor(new Type[] { typeof(string) });
			} else {
				throw new ArgumentException();
			}
		}

		/// <summary>
		///  ローカライズされたこのファイルを表す表示名を取得します。
		/// </summary>
		/// <returns>ファイルの種類を表す判読可能な文字列です。</returns>
		public string GetLocalizedDisplayName()
		{
			return FileTypeNames.ResourceManager.GetString(this.Name);
		}

		/// <summary>
		///  指定された拡張子がこのファイルの拡張子一覧に含まれているかどうかを判定します。
		/// </summary>
		/// <param name="ext">判定対象の拡張子です。</param>
		/// <returns>含まれる場合は<see langword="true"/>、含まれない場合は<see langword="false"/>です。</returns>
		public bool Contains(string ext)
		{
			for (int i = 0; i < this.Extensions.Length; ++i) {
				if (this.Extensions[i] == ext) {
					return true;
				}
			}
			return false;
		}

		/// <summary>
		///  指定されたファイル名からファイル情報オブジェクトを生成します。
		/// </summary>
		/// <param name="filename">開くファイルのファイルパスです。</param>
		/// <returns>生成された<see cref="OSDeveloper.Core.FileManagement.FileMetadata"/>オブジェクトです。</returns>
		public FileMetadata CreateMetadata(string filename)
		{
			var result = _ctor_of_metadata.Invoke(new object[] { filename });
			return result as FileMetadata;
		}
	}
}
