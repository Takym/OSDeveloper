using System;
using System.Windows.Forms;
using OSDeveloper.IO.ItemManagement;
using OSDeveloper.IO.Logging;
using OSDeveloper.Native;

namespace OSDeveloper.GraphicalUIs.Editors
{
	public partial class EditorWindow : Form
	{
		public ItemMetadata Item { get; }
		protected FormMain MainWindow { get; }
		protected Logger Logger { get; }

		public EditorWindow()
		{
			this.Logger = Logger.Get(nameof(EditorWindow));
			this.InitializeComponent();
			this.Icon = Libosdev.GetIcon(Libosdev.Icons.FormEditorWindow, out uint v);
			this.Logger.Notice("required information is insufficient");
			this.Logger.Warn($"{nameof(this.MainWindow)} is null");
			this.Logger.Warn($"{nameof(this.Item)} is null");
			this.Logger.Trace($"constructed {nameof(EditorWindow)}");
		}

		public EditorWindow(FormMain mwnd, ItemMetadata metadata)
		{
			this.Logger = Logger.Get(nameof(EditorWindow));
			this.InitializeComponent();
			this.Icon = Libosdev.GetIcon(Libosdev.Icons.FormEditorWindow, out uint v);
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
	}
}
