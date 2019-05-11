using TakymLib.IO;
using Yencon;

namespace OSDeveloper.Projects
{
	public interface IProjectItem
	{
		string     Name     { get; }
		PathString HintPath { get; }
		IProject   Parent   { get; }

		void WriteTo (YSection section);
		void ReadFrom(YSection section);
	}
}
