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
			this.riskySettings = new System.Windows.Forms.GroupBox();
			this.allowRisky = new System.Windows.Forms.CheckBox();
			this.showDelMenu = new System.Windows.Forms.CheckBox();
			this.riskySettings.SuspendLayout();
			this.SuspendLayout();
			// 
			// visualstyle
			// 
			this.visualstyle.AutoSize = true;
			this.visualstyle.Location = new System.Drawing.Point(8, 40);
			this.visualstyle.Name = "visualstyle";
			this.visualstyle.Size = new System.Drawing.Size(79, 16);
			this.visualstyle.TabIndex = 1;
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
			this.labelDesc.TabIndex = 0;
			this.labelDesc.Text = "labelDesc";
			// 
			// cmbxLang
			// 
			this.cmbxLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbxLang.FormattingEnabled = true;
			this.cmbxLang.Location = new System.Drawing.Point(8, 88);
			this.cmbxLang.Name = "cmbxLang";
			this.cmbxLang.Size = new System.Drawing.Size(496, 20);
			this.cmbxLang.TabIndex = 3;
			this.cmbxLang.SelectedIndexChanged += new System.EventHandler(this.cmbxLang_SelectedIndexChanged);
			// 
			// labelLang
			// 
			this.labelLang.AutoSize = true;
			this.labelLang.Location = new System.Drawing.Point(8, 64);
			this.labelLang.Name = "labelLang";
			this.labelLang.Size = new System.Drawing.Size(53, 12);
			this.labelLang.TabIndex = 2;
			this.labelLang.Text = "labelLang";
			// 
			// riskySettings
			// 
			this.riskySettings.Controls.Add(this.showDelMenu);
			this.riskySettings.Controls.Add(this.allowRisky);
			this.riskySettings.Location = new System.Drawing.Point(8, 120);
			this.riskySettings.Name = "riskySettings";
			this.riskySettings.Size = new System.Drawing.Size(496, 72);
			this.riskySettings.TabIndex = 4;
			this.riskySettings.TabStop = false;
			this.riskySettings.Text = "riskySettings";
			// 
			// allowRisky
			// 
			this.allowRisky.AutoSize = true;
			this.allowRisky.Location = new System.Drawing.Point(8, 24);
			this.allowRisky.Name = "allowRisky";
			this.allowRisky.Size = new System.Drawing.Size(79, 16);
			this.allowRisky.TabIndex = 0;
			this.allowRisky.Text = "allowRisky";
			this.allowRisky.UseVisualStyleBackColor = true;
			this.allowRisky.CheckedChanged += new System.EventHandler(this.allowRisky_CheckedChanged);
			// 
			// showDelMenu
			// 
			this.showDelMenu.AutoSize = true;
			this.showDelMenu.Location = new System.Drawing.Point(8, 48);
			this.showDelMenu.Name = "showDelMenu";
			this.showDelMenu.Size = new System.Drawing.Size(94, 16);
			this.showDelMenu.TabIndex = 1;
			this.showDelMenu.Text = "showDelMenu";
			this.showDelMenu.UseVisualStyleBackColor = true;
			this.showDelMenu.CheckedChanged += new System.EventHandler(this.showDelMenu_CheckedChanged);
			// 
			// StartupSettings
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.riskySettings);
			this.Controls.Add(this.labelLang);
			this.Controls.Add(this.cmbxLang);
			this.Controls.Add(this.labelDesc);
			this.Controls.Add(this.visualstyle);
			this.Name = "StartupSettings";
			this.Size = new System.Drawing.Size(512, 256);
			this.Load += new System.EventHandler(this.StartupSettings_Load);
			this.riskySettings.ResumeLayout(false);
			this.riskySettings.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckBox visualstyle;
		private System.Windows.Forms.Label labelDesc;
		private System.Windows.Forms.ComboBox cmbxLang;
		private System.Windows.Forms.Label labelLang;
		private System.Windows.Forms.GroupBox riskySettings;
		private System.Windows.Forms.CheckBox allowRisky;
		private System.Windows.Forms.CheckBox showDelMenu;
	}
}
