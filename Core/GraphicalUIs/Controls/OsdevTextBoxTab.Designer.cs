namespace OSDeveloper.Core.GraphicalUIs.Controls
{
	partial class OsdevTextBoxTab
	{
		/// <summary>
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region コンポーネント デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.output = new System.Windows.Forms.TextBox();
			this.input = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// output
			// 
			this.output.Dock = System.Windows.Forms.DockStyle.Fill;
			this.output.Location = new System.Drawing.Point(0, 0);
			this.output.Multiline = true;
			this.output.Name = "output";
			this.output.ReadOnly = true;
			this.output.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.output.Size = new System.Drawing.Size(100, 19);
			this.output.TabIndex = 0;
			this.output.WordWrap = false;
			// 
			// input
			// 
			this.input.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.input.Location = new System.Drawing.Point(0, 0);
			this.input.Name = "input";
			this.input.Size = new System.Drawing.Size(100, 19);
			this.input.TabIndex = 0;
			this.input.KeyUp += new System.Windows.Forms.KeyEventHandler(this.input_KeyUp);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TextBox output;
		private System.Windows.Forms.TextBox input;
	}
}
