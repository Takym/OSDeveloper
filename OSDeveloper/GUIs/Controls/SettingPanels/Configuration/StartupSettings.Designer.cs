namespace OSDeveloper.GUIs.Controls.SettingPanels.Configuration
{
	partial class StartupSettings
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
			this.visualstyle = new System.Windows.Forms.CheckBox();
			this.labelDesc = new System.Windows.Forms.Label();
			this.cmbxLang = new System.Windows.Forms.ComboBox();
			this.labelLang = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// visualstyle
			// 
			this.visualstyle.AutoSize = true;
			this.visualstyle.Location = new System.Drawing.Point(8, 32);
			this.visualstyle.Name = "visualstyle";
			this.visualstyle.Size = new System.Drawing.Size(79, 16);
			this.visualstyle.TabIndex = 0;
			this.visualstyle.Text = "visualstyle";
			this.visualstyle.UseVisualStyleBackColor = true;
			this.visualstyle.CheckedChanged += new System.EventHandler(this.visualstyle_CheckedChanged);
			// 
			// labelDesc
			// 
			this.labelDesc.AutoSize = true;
			this.labelDesc.Location = new System.Drawing.Point(8, 8);
			this.labelDesc.Name = "labelDesc";
			this.labelDesc.Size = new System.Drawing.Size(55, 12);
			this.labelDesc.TabIndex = 1;
			this.labelDesc.Text = "labelDesc";
			// 
			// cmbxLang
			// 
			this.cmbxLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbxLang.FormattingEnabled = true;
			this.cmbxLang.Location = new System.Drawing.Point(8, 72);
			this.cmbxLang.Name = "cmbxLang";
			this.cmbxLang.Size = new System.Drawing.Size(496, 20);
			this.cmbxLang.TabIndex = 2;
			this.cmbxLang.SelectedIndexChanged += new System.EventHandler(this.cmbxLang_SelectedIndexChanged);
			// 
			// labelLang
			// 
			this.labelLang.AutoSize = true;
			this.labelLang.Location = new System.Drawing.Point(8, 56);
			this.labelLang.Name = "labelLang";
			this.labelLang.Size = new System.Drawing.Size(53, 12);
			this.labelLang.TabIndex = 3;
			this.labelLang.Text = "labelLang";
			// 
			// StartupSettings
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.labelLang);
			this.Controls.Add(this.cmbxLang);
			this.Controls.Add(this.labelDesc);
			this.Controls.Add(this.visualstyle);
			this.Name = "StartupSettings";
			this.Size = new System.Drawing.Size(512, 150);
			this.Load += new System.EventHandler(this.StartupSettings_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckBox visualstyle;
		private System.Windows.Forms.Label labelDesc;
		private System.Windows.Forms.ComboBox cmbxLang;
		private System.Windows.Forms.Label labelLang;
	}
}
