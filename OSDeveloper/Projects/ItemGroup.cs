using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSDeveloper.Projects
{
	public class ItemGroup : Project
	{
		public ItemGroup(Solution root, Project parent, string name) : base(root, parent, name) { }

		[Flags()]
		public enum FolderKind
		{
			Input      = 0b__0_000_01,
			Output     = 0b__0_000_10,
			Compile    = 0b__0_001_00,
			Asset      = 0b__0_010_00,
			Executable = 0b__0_100_00,
			Temporary  = 0b__1_000_00,

			SourceCode = Input | Compile,
			Resource   = Input | Asset,
			Document   = Input | Asset,
			Library    = Input | Compile | Executable,
			ToolKit    = Input | Executable,
			Object     = Output | Compile | Temporary,
			Debug      = Output | Temporary,
			Binary     = Output | Executable,
			Package    = Output | Executable,
		}
	}
}
