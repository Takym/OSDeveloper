using System;
using System.Text;
using OSDeveloper.IO.ItemManagement;
using OSDeveloper.Resources;
using TakymLib;
using TakymLib.IO;

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
			this.ItemType   = null;//typeof(DefaultItemExtendedDetail);
		}

		/// <exception cref="System.InvalidCastException"/>
		public FileType(FileFormat format, string name, Type item, params string[] extensions)
		{
			this.Format = format;
			this.Name = name;
			this.Extensions = extensions;

			if (typeof(ItemExtendedDetail).IsAssignableFrom(item)) {
				this.ItemType = item;
			} else {
				var e = new InvalidCastException(
						string.Format(
							ErrorMessages.InvalidCast,
							item.FullName,
							typeof(ItemExtendedDetail).FullName));
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
		public FileMetadata CreateMetadata(PathString filename)
		{
			if (this.Extensions.ContainsValue(filename.GetExtension())) {
				var result = ItemList.GetFile(filename, this.Format);
				if (this.ItemType != null) {
					try {
						result.ExtendedDetail = Activator.CreateInstance(this.ItemType) as ItemExtendedDetail;
					} catch (Exception e) {
						Program.Logger.Notice($"The exception occurred in {nameof(FileType)}, filename:{filename}");
						Program.Logger.Exception(e);
					}
				}
				return result;
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
		internal  SystemFileType(FileFormat format, string name, bool removable, params string[] extensions)
			: base(format, name, extensions)
		{
			this.Removable = removable;
		}
		internal SystemFileType(FileFormat format, string name, Type item, bool removable, params string[] extensions)
			: base(format, name, item, extensions)
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
