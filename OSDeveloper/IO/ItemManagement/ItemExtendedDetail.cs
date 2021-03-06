﻿using OSDeveloper.GUIs.Editors;
using OSDeveloper.GUIs.Terminal;

namespace OSDeveloper.IO.ItemManagement
{
	public abstract class ItemExtendedDetail
	{
		public ItemMetadata Metadata { get; internal set; }

		public abstract ItemProperty CreatePropTab();
		public abstract EditorWindow CreateEditor(FormMain mwnd);
	}

	public class DefaultItemExtendedDetail : ItemExtendedDetail
	{
		public override ItemProperty CreatePropTab()
		{
			return new ItemProperty(this.Metadata);
		}

		public override EditorWindow CreateEditor(FormMain mwnd)
		{
			return new EditorWindow(mwnd, this.Metadata);
		}
	}

	public class TextFileExtendedDetail : DefaultItemExtendedDetail
	{
		public override EditorWindow CreateEditor(FormMain mwnd)
		{
			return new SimpleTextEditor(mwnd, this.Metadata);
		}
	}

	public class YenconFileExtendedDetail : DefaultItemExtendedDetail
	{
		public override EditorWindow CreateEditor(FormMain mwnd)
		{
			return new SimpleYenconTextEditor(mwnd, this.Metadata);
		}
	}

	public class FolderExtendedDetail : DefaultItemExtendedDetail
	{
		public override EditorWindow CreateEditor(FormMain mwnd)
		{
			return null;
		}
	}
}
