using System.Collections.Generic;

namespace OSDeveloper.Projects
{
	public interface IProject : IProjectItem
	{
		IDEVersion          SavedVersion { get; }
		string              ProjectType  { get; }
		IList<IProject>     DependsOn    { get; }
		IList<IProjectItem> Contents     { get; }
	}
}
