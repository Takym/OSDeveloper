using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using OSDeveloper.IO;
using OSDeveloper.IO.ItemManagement;
using OSDeveloper.IO.Logging;
using OSDeveloper.Resources;

namespace OSDeveloper.GUIs.Terminal
{
	public partial class ItemProperty : TabPage
	{
		private readonly Logger       _logger;
		private readonly ItemMetadata _meta;

		public ItemProperty(ItemMetadata meta)
		{
			_logger = Logger.Get(nameof(ItemProperty));
			_meta   = meta;

			this.InitializeComponent();
			this.SuspendLayout();

			propertyGrid.SelectedObject = new ItemMetadataWrapper(_meta);

			// コントロール設定
			this.Controls.Add(propertyGrid);
			this.Text = _meta.Name;

			this.ResumeLayout(false);
			this.PerformLayout();

			_logger.Trace($"constructed {nameof(ItemProperty)}");
		}

		public class ItemMetadataWrapper
		{
			private readonly ItemMetadata _meta;
			private          string       _filetype;

			public ItemMetadataWrapper(ItemMetadata meta)
			{
				_meta = meta;
			}

			[OsdevCategory(nameof(TerminalTexts), nameof(TerminalTexts.ItemProperty_Name))]
			[OsdevDisplayName(nameof(TerminalTexts), nameof(TerminalTexts.ItemProperty_Name_FileName))]
			[OsdevDescription(nameof(TerminalTexts), nameof(TerminalTexts.ItemProperty_Name_FileName_Description))]
			public string FileName
			{
				get
				{
					return _meta.Name;
				}
			}

			[OsdevCategory(nameof(TerminalTexts), nameof(TerminalTexts.ItemProperty_Name))]
			[OsdevDisplayName(nameof(TerminalTexts), nameof(TerminalTexts.ItemProperty_Name_FullPath))]
			[OsdevDescription(nameof(TerminalTexts), nameof(TerminalTexts.ItemProperty_Name_FullPath_Description))]
			public string FullPath
			{
				get
				{
					return _meta.Path;
				}
			}

			[OsdevCategory(nameof(TerminalTexts), nameof(TerminalTexts.ItemProperty_Name))]
			[OsdevDisplayName(nameof(TerminalTexts), nameof(TerminalTexts.ItemProperty_Name_FileType))]
			[OsdevDescription(nameof(TerminalTexts), nameof(TerminalTexts.ItemProperty_Name_FileType_Description))]
			public string FileType
			{
				get
				{
					if (_filetype == null) {
						var ft = FileTypeRegistry.GetByExtension(_meta.Path.GetExtension());
						var sb = new StringBuilder();
						for (int i = 0; i < ft.Length; ++i) {
							if (i != 0) sb.Append(" | ");
							sb.Append(ft[i].GetLocalizedDisplayName());
						}
						_filetype = sb.ToString();
					}
					return _filetype;
				}
			}

			[OsdevCategory(nameof(TerminalTexts), nameof(TerminalTexts.ItemProperty_Attribute))]
			[OsdevDisplayName(nameof(TerminalTexts), nameof(TerminalTexts.ItemProperty_Attribute_Attributes))]
			[OsdevDescription(nameof(TerminalTexts), nameof(TerminalTexts.ItemProperty_Attribute_Attributes_Description))]
			public FileAttributes Attributes
			{
				get
				{
					return _meta.Attributes;
				}
			}

			[OsdevCategory(nameof(TerminalTexts), nameof(TerminalTexts.ItemProperty_Attribute))]
			[OsdevDisplayName(nameof(TerminalTexts), nameof(TerminalTexts.ItemProperty_Attribute_FileFormat))]
			[OsdevDescription(nameof(TerminalTexts), nameof(TerminalTexts.ItemProperty_Attribute_FileFormat_Description))]
			public FileFormat FileFormat
			{
				get
				{
					return (_meta as FileMetadata)?.Format ?? FileFormat.Unknown;
				}
			}

			[OsdevCategory(nameof(TerminalTexts), nameof(TerminalTexts.ItemProperty_Attribute))]
			[OsdevDisplayName(nameof(TerminalTexts), nameof(TerminalTexts.ItemProperty_Attribute_FolderFormat))]
			[OsdevDescription(nameof(TerminalTexts), nameof(TerminalTexts.ItemProperty_Attribute_FolderFormat_Description))]
			public FolderFormat FolderFormat
			{
				get
				{
					return (_meta as FolderMetadata)?.Format ?? FolderFormat.Unknown;
				}
			}

			[OsdevCategory(nameof(TerminalTexts), nameof(TerminalTexts.ItemProperty_Attribute))]
			[OsdevDisplayName(nameof(TerminalTexts), nameof(TerminalTexts.ItemProperty_Attribute_CanAccess))]
			[OsdevDescription(nameof(TerminalTexts), nameof(TerminalTexts.ItemProperty_Attribute_CanAccess_Description))]
			public bool CanAccess
			{
				get
				{
					return _meta.CanAccess;
				}
			}

			[OsdevCategory(nameof(TerminalTexts), nameof(TerminalTexts.ItemProperty_Attribute))]
			[OsdevDisplayName(nameof(TerminalTexts), nameof(TerminalTexts.ItemProperty_Attribute_IsRemoved))]
			[OsdevDescription(nameof(TerminalTexts), nameof(TerminalTexts.ItemProperty_Attribute_IsRemoved_Description))]
			public bool IsRemoved
			{
				get
				{
					return _meta.IsRemoved;
				}
			}
			
			[OsdevCategory(nameof(TerminalTexts), nameof(TerminalTexts.ItemProperty_DateTime))]
			[OsdevDisplayName(nameof(TerminalTexts), nameof(TerminalTexts.ItemProperty_DateTime_CreationDate))]
			[OsdevDescription(nameof(TerminalTexts), nameof(TerminalTexts.ItemProperty_DateTime_CreationDate_Description))]
			public DateTime CreationTime
			{
				get
				{
					return _meta.Info.CreationTime;
				}
			}

			[OsdevCategory(nameof(TerminalTexts), nameof(TerminalTexts.ItemProperty_DateTime))]
			[OsdevDisplayName(nameof(TerminalTexts), nameof(TerminalTexts.ItemProperty_DateTime_LastAccessTime))]
			[OsdevDescription(nameof(TerminalTexts), nameof(TerminalTexts.ItemProperty_DateTime_LastAccessTime_Description))]
			public DateTime LastAccessTime
			{
				get
				{
					return _meta.Info.LastAccessTime;
				}
			}

			[OsdevCategory(nameof(TerminalTexts), nameof(TerminalTexts.ItemProperty_DateTime))]
			[OsdevDisplayName(nameof(TerminalTexts), nameof(TerminalTexts.ItemProperty_DateTime_LastWriteTime))]
			[OsdevDescription(nameof(TerminalTexts), nameof(TerminalTexts.ItemProperty_DateTime_LastWriteTime_Description))]
			public DateTime LastWriteTime
			{
				get
				{
					return _meta.Info.LastWriteTime;
				}
			}

			[OsdevCategory(nameof(TerminalTexts), nameof(TerminalTexts.ItemProperty_Size))]
			[OsdevDisplayName(nameof(TerminalTexts), nameof(TerminalTexts.ItemProperty_Size_Size))]
			[OsdevDescription(nameof(TerminalTexts), nameof(TerminalTexts.ItemProperty_Size_Size_Description))]
			public long Size
			{
				get
				{
					if (_meta is FileMetadata file) {
						if (file.Info is FileInfo info) {
							return info.Length;
						}
					} else if (_meta is FolderMetadata folder) {
						return folder.Count;
					}
					return 0;
				}
			}
		}
	}
}
