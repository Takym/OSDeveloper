using System;
using Yencon;
using Yencon.Extension;

namespace OSDeveloper.Projects
{
	public class ItemGroup : Project
	{
		public FolderKind Kind { get; set; }

		public ItemGroup(Solution root, Project parent, string name) : base(root, parent, name) { }

		public override void WriteTo(YSection section)
		{
			base.WriteTo(section);
			this.Logger.Trace($"executing {nameof(ItemGroup)}.{nameof(this.WriteTo)} ({this.Name})...");
			section.SetNodeAsString("FolderKind", this.Kind.ToString());
			this.Logger.Trace($"completed {nameof(ItemGroup)}.{nameof(this.WriteTo)} ({this.Name})");
		}

		/// <exception cref="System.ArgumentException" />
		public override void ReadFrom(YSection section)
		{
			base.ReadFrom(section);
			this.Logger.Trace($"executing {nameof(ItemGroup)}.{nameof(this.ReadFrom)} ({this.Name})...");
			var kind = ((FolderKind)(section.GetNodeAsNumber("FolderKind")));
			_ = kind == FolderKind.Invalid && Enum.TryParse(section.GetNodeAsString("FolderKind"), out kind);
			this.Kind = kind;
			this.Logger.Trace($"completed {nameof(ItemGroup)}.{nameof(this.ReadFrom)} ({this.Name})...");
		}

		[Flags()]
		public enum FolderKind
		{
			Invalid    = 0,

			Input      = 0b__00000_01,
			Output     = 0b__00000_10,
			Compile    = 0b__00001_00,
			Asset      = 0b__00010_00,
			Program    = 0b__00100_00,
			Publish    = 0b__01000_00,
			Temporary  = 0b__10000_00,

			SourceCode = Input | Compile,
			Resource   = Input | Asset,
			Document   = Input | Asset | Publish,
			Library    = Input | Compile | Program,
			ToolKit    = Input | Program,
			Object     = Output | Compile | Temporary,
			Debug      = Output | Temporary,
			Binary     = Output | Program,
			Package    = Output | Program | Publish,
		}
	}
}
