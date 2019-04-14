using System.Windows.Forms;
using OSDeveloper.GraphicalUIs.Editors;
using OSDeveloper.IO.ItemManagement;
using OSDeveloper.IO.Logging;

namespace OSDeveloper.GraphicalUIs.Terminal
{
	public partial class EditorProperty : TabPage
	{
		private readonly Logger       _logger;
		private readonly ItemMetadata _meta;

		public EditorProperty(ItemMetadata meta, EditorWindow editor)
		{
			_logger = Logger.Get(nameof(ItemProperty));
			_meta   = meta;

			this.InitializeComponent();
			this.SuspendLayout();

			propertyGrid.SelectedObject = editor;

			// コントロール設定
			this.Controls.Add(propertyGrid);
			this.Text = _meta.Name;

			this.ResumeLayout(false);
			this.PerformLayout();

			_logger.Trace($"constructed {nameof(EditorProperty)}");
		}
	}
}
