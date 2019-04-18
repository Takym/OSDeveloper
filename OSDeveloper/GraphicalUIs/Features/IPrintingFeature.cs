using System.Drawing.Printing;
using System.Windows.Forms;

namespace OSDeveloper.GraphicalUIs.Features
{
	public interface IPrintingFeature
	{
		PrintDocument PrintDocument { get; }
		bool UseCustomDialogs { get; }
		void ShowPrintDialog(bool useExDialog);
		Form ShowPrintPreviewDialog(bool useExDialog);
		void ShowPageSetupDialog(bool useExDialog);
	}
}
