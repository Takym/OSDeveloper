﻿namespace OSDeveloper.Core.Editors
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
			this.osdevTextBox1 = new OSDeveloper.Core.GraphicalUIs.Controls.OsdevTextBox();
			this.SuspendLayout();
			// 
			// osdevTextBox1
			// 
			this.osdevTextBox1.BackColor = System.Drawing.Color.Black;
			this.osdevTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.osdevTextBox1.Lines = new string[] {
        "osdevTextBox1"};
			this.osdevTextBox1.Location = new System.Drawing.Point(0, 0);
			this.osdevTextBox1.Name = "osdevTextBox1";
			this.osdevTextBox1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.osdevTextBox1.Size = new System.Drawing.Size(800, 450);
			this.osdevTextBox1.TabIndex = 0;
			this.osdevTextBox1.Text = "osdevTextBox1";
			this.osdevTextBox1.TextChanged += new System.EventHandler(this.osdevTextBox1_TextChanged);
			// 
			// TextEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.osdevTextBox1);
			this.Name = "TextEditor";
			this.Text = "TextEditor";
			this.Load += new System.EventHandler(this.TextEditor_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private GraphicalUIs.Controls.OsdevTextBox osdevTextBox1;
	}
}