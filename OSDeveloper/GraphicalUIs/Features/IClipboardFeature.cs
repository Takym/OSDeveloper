namespace OSDeveloper.GraphicalUIs.Features
{
	public interface IClipboardFeature : ISelectionFeature
	{
		void Cut();
		void Copy();
		void Paste();
	}
}
