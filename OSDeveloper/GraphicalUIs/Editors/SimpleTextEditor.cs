using System.ComponentModel;
using System.Windows.Forms;
using OSDeveloper.IO.ItemManagement;

namespace OSDeveloper.GraphicalUIs.Editors
{
	[DesignerCategory("")] // デザイナ避け
	public partial class SimpleTextEditor : EditorWindow
	{
		private TextBox _textBox;

		public SimpleTextEditor(FormMain mwnd, ItemMetadata metadata) : base(mwnd, metadata)
		{
			_textBox = new TextBox();
			_textBox.Multiline = true;
			_textBox.ScrollBars = ScrollBars.Both;
			_textBox.Dock = DockStyle.Fill;
			_textBox.Text = (metadata as FileMetadata)?.ReadAllText();
			this.Controls.Add(_textBox);
		}
	}
}
