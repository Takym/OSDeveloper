using System;
using System.ComponentModel;
using System.Windows.Forms;
using OSDeveloper.GraphicalUIs.Terminal;
using OSDeveloper.IO.ItemManagement;
using OSDeveloper.IO.Logging;

namespace OSDeveloper.GraphicalUIs.Editors
{
	public partial class EditorWindow : Form
	{
		public ItemMetadata Item { get; }
		protected FormMain MainWindow { get; }
		protected EditorProperty Property { get; private set; }
		protected Logger Logger { get; }

		public EditorWindow(FormMain mwnd, ItemMetadata metadata)
		{
			this.Logger = Logger.Get(nameof(EditorWindow));
			this.InitializeComponent();
			this.MainWindow = mwnd;
			this.MdiParent = mwnd;
			this.Item = metadata;
			this.Text = metadata.Name;
			this.Logger.Info($"the metadata is: {metadata.Path}");
			this.Logger.Trace($"constructed {nameof(EditorWindow)}");
		}

		protected override void OnLoad(EventArgs e)
		{
			this.Logger.Trace($"executing {nameof(OnLoad)}...");
			base.OnLoad(e);

			this.Logger.Trace($"completed {nameof(OnLoad)}");
		}

		protected override void OnHelpButtonClicked(CancelEventArgs e)
		{
			this.Logger.Trace($"executing {nameof(OnHelpButtonClicked)}...");
			base.OnHelpButtonClicked(e);

			e.Cancel = true;
			if (this.Property == null || this.Property.IsDisposed) {
				this.Property = new EditorProperty(this.Item, this);
			}
			this.MainWindow.OpenTab(this.Property);

			this.Logger.Trace($"completed {nameof(OnHelpButtonClicked)}");
		}
	}
}
