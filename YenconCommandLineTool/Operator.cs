using System;
using Yencon;

namespace YenconCommandLineTool
{
	public static class Operator
	{
		public static void List(YSection section)
		{
			var k = section.SubKeys;
			for (int i = 0; i < k.Length; ++i) {
				Console.WriteLine($"{k[i].Name}");
				Console.WriteLine($"\t{k[i].GetType().FullName}");
				Console.WriteLine($"\t{k[i].GetValue()?.ToString()}");
				Console.WriteLine(new string('-', 16));
			}
		}

		public static void Set(YSection section, string type, string name, string value)
		{
#if RELEASE
			try {
#endif
				switch (type) {
					case "str":
						var str = new YString();
						str.Name = name;
						str.Text = value;
						section.Add(str);
						break;
					case "num":
						var num = new YNumber();
						num.Name = name;
						if (ulong.TryParse(value, out var val1)) {
							num.UInt64Value = val1;
						}
						section.Add(num);
						break;
					case "flg":
						var flg = new YBoolean();
						flg.Name = name;
						if (bool.TryParse(value, out var val2)) {
							flg.Flag = val2;
						}
						section.Add(flg);
						break;
					default: // case "nul"
						var nul = new YNullOrEmpty();
						nul.Name = name;
						section.Add(nul);
						break;
				}
#if RELEASE
			} catch (Exception e) {
				Program.ShowError(e);
			}
#endif
		}

		public static void Adds(YSection section, string name)
		{
			section.Add(new YSection() { Name = name });
		}
	}
}
