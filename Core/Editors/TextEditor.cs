using System;
using OSDeveloper.Core.GraphicalUIs;

namespace OSDeveloper.Core.Editors
{
	public partial class TextEditor : EditorWindow
	{
		public TextEditor(MainWindowBase parent) : base(parent)
		{
			InitializeComponent();
		}

		private void TextEditor_Load(object sender, EventArgs e)
		{
			tbox.Text = "abcd\n"
			          + "Hello, World!!\n"
			          + "The quick brown fox jumped over the lazy dogs.\n"
			          + "0123456789 ABCDEFGHIJKLMNOPQRSTUVWXYZ\n"
			          + "\thoge\tfuga\tpiyo\tfoobar\n"
			          + "テストてすとUnicodeの実験文字文字文字あいうえおｶｷｸｹｺ\n";
		}
	}
}
