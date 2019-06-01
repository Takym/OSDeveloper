namespace OSDeveloper.GUIs.Features
{
	public interface IFileLoadFeature
	{
		bool Loaded { get; }
		void Reload();
	}
}
