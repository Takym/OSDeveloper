namespace OSDeveloper.GUIs.Controls.SettingPanels.Configuration
{
	partial class EnvironmentSettings
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
			this.labelDesc = new System.Windows.Forms.Label();
			this.useExdialog = new System.Windows.Forms.CheckBox();
			this.useWsl = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// labelDesc
			// 
			this.labelDesc.AutoSize = true;
			this.labelDesc.Location = new System.Drawing.Point(8, 8);
			this.labelDesc.Name = "labelDesc";
			this.labelDesc.Size = new System.Drawing.Size(55, 12);
			this.labelDesc.TabIndex = 0;
			this.labelDesc.Text = "labelDesc";
			// 
			// useExdialog
			// 
			this.useExdialog.AutoSize = true;
			this.useExdialog.Location = new System.Drawing.Point(8, 40);
			this.useExdialog.Name = "useExdialog";
			this.useExdialog.Size = new System.Drawing.Size(85, 16);
			this.useExdialog.TabIndex = 1;
			this.useExdialog.Text = "useExdialog";
			this.useExdialog.UseVisualStyleBackColor = true;
			this.useExdialog.CheckedChanged += new System.EventHandler(this.useExdialog_CheckedChanged);
			// 
			// useWsl
			// 
			this.useWsl.AutoSize = true;
			this.useWsl.Location = new System.Drawing.Point(8, 64);
			this.useWsl.Name = "useWsl";
			this.useWsl.Size = new System.Drawing.Size(60, 16);
			this.useWsl.TabIndex = 2;
			this.useWsl.Text = "useWsl";
			this.useWsl.UseVisualStyleBackColor = true;
			this.useWsl.CheckedChanged += new System.EventHandler(this.useWsl_CheckedChanged);
			// 
			// EnvironmentSettings
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.useWsl);
			this.Controls.Add(this.useExdialog);
			this.Controls.Add(this.labelDesc);
			this.Name = "EnvironmentSettings";
			this.Size = new System.Drawing.Size(256, 150);
			this.Load += new System.EventHandler(this.EnvironmentSettings_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label labelDesc;
		private System.Windows.Forms.CheckBox useExdialog;
		private System.Windows.Forms.CheckBox useWsl;
	}
}
