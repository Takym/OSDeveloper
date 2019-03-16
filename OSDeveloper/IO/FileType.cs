using System;
using System.Linq;
using System.Text;
using OSDeveloper.IO.ItemManagement;
using OSDeveloper.Resources;
using TakymLib;

namespace OSDeveloper.IO
{
	public class FileType
	{
		public  virtual  FileFormat Format     { get; }
		public  virtual  string     Name       { get; }
		public  virtual  string[]   Extensions { get; }
		public  virtual  Type       ItemType   { get; }

		protected FileType() { }
		public FileType(FileFormat format, string name, params string[] extensions)
		{
			this.Format     = format;
			this.Name       = name;
			this.Extensions = extensions;
			this.ItemType   = typeof(FileMetadata);
		}

		/// <exception cref="System.InvalidCastException"/>
		public FileType(FileFormat format, string name, Type item, params string[] extensions)
		{
			this.Format = format;
			this.Name = name;
			this.Extensions = extensions;

			if (typeof(ItemMetadata).IsAssignableFrom(item)) {
				this.ItemType = item;
			} else {
				var e = new InvalidCastException(
						string.Format(
							ErrorMessages.FileType_InvalidCast,
							item.FullName));
				throw new ArgumentException(
					ErrorMessages.FileType_Argument,
					nameof(item), e);
			}
		}

		public string CreateSPF()
		{
			// SPF = Search Pattern Filter
			StringBuilder filter = new StringBuilder();
			for (int i = 0; i < this.Extensions.Length; ++i) {
				if (i != 0) filter.Append(';');
				filter.Append($"*.{this.Extensions[i]}");
			}
			return $"{this.GetLocalizedDisplayName()} ({filter.ToString()})|{filter.ToString()}";
		}

		public virtual string GetLocalizedDisplayName()
		{
			return FileTypeNames.ResourceManager.GetString(this.Name) ?? this.Name;
		}

		/// <exception cref="System.ArgumentException"/>
		public ItemMetadata CreateMetadata(PathString filename, FolderMetadata parent)
		{
			if (this.Extensions.ContainsValue(filename.GetExtension())) {
				try {
					var c = this.ItemType.GetConstructor(new Type[] { typeof(PathString) });
					var o = c?.Invoke(new object[] { filename, parent, this.Format });
					return o as ItemMetadata;
				} catch (Exception e) {
					Program.Logger.Notice($"The exception occurred in {nameof(FileType)}");
					Program.Logger.Exception(e);
					return null;
				}
			} else {
				throw new ArgumentException(string.Format(
					ErrorMessages.FileType_CreateMetadata_ExtensionInvalid,
					filename,
					$"{{ {string.Join(", ", this.Extensions)} }}"));
			}
		}
	}

	internal class SystemFileType : FileType
	{
		internal bool Removable { get; }

		protected SystemFileType() { }
		internal  SystemFileType(FileFormat format, string name, bool removable = false, params string[] extensions)
			: base(format, name, extensions)
		{
			this.Removable = removable;
		}
	}

	internal sealed class AllSupportedFileTypes : SystemFileType
	{
		public override string Name
		{
			get
			{
				return nameof(AllSupportedFileTypes);
			}
		}

		public override string[] Extensions
		{
			get
			{
				return FileTypeRegistry.GetAllExtensions();
			}
		}
	}
}
