using System.Collections.Generic;
using Yencon;

namespace OSDeveloper.Projects
{
	public interface IProject
	{
		IDEVersion SavedVersion { get; }
		ProjectType ProjectType { get; }
		IProject Parent { get; }
		IList<IProject> Children { get; }
		IList<string> Contents { get; } // TODO: ファイルの登録先、現在設計中

		void WriteTo(YSection section);
		void ReadFrom(YSection section);
	}
}
