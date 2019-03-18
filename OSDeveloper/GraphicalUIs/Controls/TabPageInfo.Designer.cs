namespace OSDeveloper.GraphicalUIs.Controls
{
	partial class TabPageInfo
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
			this.lblTitle = new System.Windows.Forms.Label();
			this.btnDispose = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lblTitle
			// 
			this.lblTitle.AutoSize = true;
			this.lblTitle.Location = new System.Drawing.Point(8, 8);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(40, 12);
			this.lblTitle.TabIndex = 0;
			this.lblTitle.Text = "lblTitle";
			// 
			// btnDispose
			// 
			this.btnDispose.Location = new System.Drawing.Point(8, 32);
			this.btnDispose.Name = "btnDispose";
			this.btnDispose.Size = new System.Drawing.Size(75, 23);
			this.btnDispose.TabIndex = 1;
			this.btnDispose.Text = "btnDispose";
			this.btnDispose.UseVisualStyleBackColor = true;
			this.btnDispose.Click += new System.EventHandler(this.btnDispose_Click);
			// 
			// TabPageInfo
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.Controls.Add(this.btnDispose);
			this.Controls.Add(this.lblTitle);
			this.Name = "TabPageInfo";
			this.Size = new System.Drawing.Size(256, 64);
			this.Load += new System.EventHandler(this.TabPageInfo_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblTitle;
		private System.Windows.Forms.Button btnDispose;
	}
}
