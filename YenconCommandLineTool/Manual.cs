using System;
using System.Reflection;
using YenconCommandLineTool.Resources;

namespace YenconCommandLineTool
{
	static class Manual
	{
		public static void Help()
		{
			Console.WriteLine(Messages.ManualTitle);
			Console.WriteLine($"about     {Messages.Man_About}");
			Console.WriteLine($"adds      {Messages.Man_Adds}");
			Console.WriteLine($"binhdr    {Messages.Man_Binhdr}");
			Console.WriteLine($"exit      {Messages.Man_Exit}");
			Console.WriteLine($"goroot    {Messages.Man_Goroot}");
			Console.WriteLine($"help      {Messages.Man_Help}");
			Console.WriteLine($"into      {Messages.Man_Into}");
			Console.WriteLine($"list      {Messages.Man_List}");
			Console.WriteLine($"load      {Messages.Man_Load}");
			Console.WriteLine($"loadb     {Messages.Man_Loadb}");
			Console.WriteLine($"loadt     {Messages.Man_Loadt}");
			Console.WriteLine($"quit      {Messages.Man_Quit}");
			Console.WriteLine($"save      {Messages.Man_Save}");
			Console.WriteLine($"saveb     {Messages.Man_Saveb}");
			Console.WriteLine($"savet     {Messages.Man_Savet}");
			Console.WriteLine($"set       {Messages.Man_Set}");
			Console.WriteLine($"ver       {Messages.Man_Ver}");
		}

		public static void Help(string cmd)
		{
			switch (cmd) {
				case "help":
					Console.WriteLine(Messages.Man_Help_Full);
					break;
				case "loadb":
					Console.WriteLine(Messages.Man_Loadb_Full);
					break;
				case "saveb":
					Console.WriteLine(Messages.Man_Saveb_Full);
					break;
				case "loadt":
					Console.WriteLine(Messages.Man_Loadt_Full);
					break;
				case "savet":
					Console.WriteLine(Messages.Man_Savet_Full);
					break;
				case "load":
					Console.WriteLine(Messages.Man_Load_Full);
					break;
				case "save":
					Console.WriteLine(Messages.Man_Save_Full);
					break;
				case "set":
					Console.WriteLine(Messages.Man_Set_Full);
					break;
				default:
					Console.WriteLine(Messages.ManualNotFound, cmd);
					break;
			}
		}

		public static void Version(Assembly asm)
		{
			var asmn = asm.GetName();
			Console.WriteLine(asmn.FullName);
			Console.WriteLine($"{Messages.Ver_Version}: {asmn.Version}");
			Console.WriteLine($"{Messages.Ver_Copyright}: {asm.GetCustomAttribute<AssemblyCopyrightAttribute>().Copyright}");
			Console.WriteLine($"{Messages.Ver_Description}: {asm.GetCustomAttribute<AssemblyDescriptionAttribute>().Description}");
		}
	}
}
