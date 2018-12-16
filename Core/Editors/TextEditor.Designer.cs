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
			this.tbox.BackColor = System.Drawing.Color.Black;
			this.tbox.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.tbox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbox.Font = new System.Drawing.Font("ＭＳ ゴシック", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.tbox.ForeColor = System.Drawing.Color.White;
			this.tbox.Location = new System.Drawing.Point(0, 0);
			this.tbox.Name = "tbox";
			this.tbox.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.tbox.Size = new System.Drawing.Size(800, 450);
			this.tbox.TabIndex = 0;
			this.tbox.Text = "abcd\nHello, World!!\nThe quick brown fox jumped over the lazy dogs.\n0123456789 ABC" +
    "DEFGHIJKLMNOPQRSTUVWXYZ\n\thoge\tfuga\tpiyo\tfoobar\nテストてすとUnicodeの実験文字文字文字あいうえおｶｷｸｹｺ\n" +
    "";
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