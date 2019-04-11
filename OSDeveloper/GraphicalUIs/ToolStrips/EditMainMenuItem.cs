using OSDeveloper.Resources;

namespace OSDeveloper.GraphicalUIs.ToolStrips
{
	public partial class EditMainMenuItem : MainMenuItem
	{
		public EditMainMenuItem(FormMain mwnd) : base(mwnd)
		{
			this.Name = "EDIT";
			this.Text = MenuTexts.Edit;

			_logger.Trace($"constructed {nameof(EditMainMenuItem)}");
		}
	}
}
