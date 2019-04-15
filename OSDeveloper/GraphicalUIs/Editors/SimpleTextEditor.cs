﻿using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using OSDeveloper.GraphicalUIs.Features;
using OSDeveloper.IO.ItemManagement;
using TakymLib.IO;

namespace OSDeveloper.GraphicalUIs.Editors
{
	[DesignerCategory("")] // デザイナ避け
	public partial class SimpleTextEditor : EditorWindow, IFileSaveFeature
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

		public void Save()
		{
			(this.Item as FileMetadata)?.WriteAllText(_textBox.Text);
		}

		public void SaveAs(PathString filename)
		{
			using (var sw = new StreamWriter(filename)) {
				sw.Write(_textBox.Text);
			}
		}
	}
}
