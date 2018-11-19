namespace OSDeveloper.Core.Editors
{
	partial class TextEditor
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.tbox = new OSDeveloper.Core.GraphicalUIs.Controls.OsdevTextBox();
			this.SuspendLayout();
			// 
			// tbox
			// 
			this.tbox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbox.Location = new System.Drawing.Point(0, 0);
			this.tbox.Name = "tbox";
			this.tbox.Size = new System.Drawing.Size(800, 450);
			this.tbox.TabIndex = 0;
			this.tbox.Text = "abcd\nHello, World!!\nThe quick brown fox jumped over the lazy dogs.\n0123456789 ABCDEFGHIJKLMNOPQRSTUVWXYZ\n\thoge\tfuga\tpiyo\tfoobar\nテストてすとUnicodeの実験文字文字文字あいうえおｶｷｸｹｺ\n";
			// 
			// TextEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.tbox);
			this.Name = "TextEditor";
			this.Text = "TextEditor";
			this.Load += new System.EventHandler(this.TextEditor_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private GraphicalUIs.Controls.OsdevTextBox tbox;
	}
}