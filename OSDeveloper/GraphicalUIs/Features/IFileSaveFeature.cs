using OSDeveloper.IO.ItemManagement;
using TakymLib.IO;

namespace OSDeveloper.GraphicalUIs.Features
{
	public interface IFileSaveFeature
	{
		ItemMetadata Item { get; }

		void Save();
		void SaveAs(PathString filename);
	}
}
