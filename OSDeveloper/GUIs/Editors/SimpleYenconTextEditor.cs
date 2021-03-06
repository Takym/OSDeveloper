﻿using System;
using System.ComponentModel;
using System.Windows.Forms;
using OSDeveloper.IO.ItemManagement;
using TakymLib.IO;
using Yencon;

namespace OSDeveloper.GUIs.Editors
{
	[DesignerCategory("")] // デザイナ避け
	public partial class SimpleYenconTextEditor : SimpleTextEditor
	{
		private YenconStringConverter _conv;

		public SimpleYenconTextEditor(FormMain mwnd, ItemMetadata metadata) : base(mwnd, metadata)
		{
			_conv = YenconFormatRecognition.StringConverter;
		}

		public override void Reload()
		{
			if (this.Item is FileMetadata file) {
				this.TextBox.Text = YenconFormatRecognition.Load(file.Path).ToString();
				this.Text         = this.Item.Name;
				this.Loaded       = true;
			}
		}

		public override void Save()
		{
			if (this.Item is FileMetadata file) {
				try {
					var t = YenconFormatRecognition.GetYenconType(file.Path);
					YenconFormatRecognition.Save(file.Path, _conv.ToYencon(this.TextBox.Text), t);
					this.Text = this.Item.Name;
				} catch (Exception e) {
					this.Logger.Exception(e);
					MessageBox.Show(this,
						e.Message,
						this.Item.Name,
						MessageBoxButtons.OK,
						MessageBoxIcon.Error
					);
				}
			}
		}

		public override void SaveAs(PathString filename)
		{
			if (this.Item is FileMetadata file) {
				try {
					var t = YenconFormatRecognition.GetYenconType(file.Path);
					YenconFormatRecognition.Save(filename, _conv.ToYencon(this.TextBox.Text), t);
				} catch (Exception e) {
					this.Logger.Exception(e);
					MessageBox.Show(this,
						e.Message,
						this.Item.Name,
						MessageBoxButtons.OK,
						MessageBoxIcon.Error
					);
				}
			}
		}
	}
}
