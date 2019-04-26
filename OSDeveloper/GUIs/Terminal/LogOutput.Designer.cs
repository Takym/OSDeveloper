namespace OSDeveloper.GUIs.Terminal
{
	partial class LogOutput
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
			this.listView = new System.Windows.Forms.ListView();
			this.col_CreatedDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.col_Level = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.col_Logger = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.col_Message = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.cmbLevel = new System.Windows.Forms.ComboBox();
			this.cmbLogger = new System.Windows.Forms.ComboBox();
			this.lblLevel = new System.Windows.Forms.Label();
			this.lblLogger = new System.Windows.Forms.Label();
			this.lblCount = new System.Windows.Forms.Label();
			this.nudCount = new System.Windows.Forms.NumericUpDown();
			this.controller = new System.Windows.Forms.Panel();
			this.btnRefresh = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.nudCount)).BeginInit();
			this.SuspendLayout();
			// 
			// listView
			// 
			this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.col_CreatedDate,
            this.col_Level,
            this.col_Logger,
            this.col_Message});
			this.listView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listView.Location = new System.Drawing.Point(0, 0);
			this.listView.Name = "listView";
			this.listView.Size = new System.Drawing.Size(121, 97);
			this.listView.TabIndex = 0;
			this.listView.UseCompatibleStateImageBehavior = false;
			this.listView.View = System.Windows.Forms.View.Details;
			// 
			// col_CreatedDate
			// 
			this.col_CreatedDate.Width = 134;
			// 
			// col_Level
			// 
			this.col_Level.Width = 48;
			// 
			// col_Logger
			// 
			this.col_Logger.Width = 128;
			// 
			// col_Message
			// 
			this.col_Message.Width = 512;
			// 
			// cmbLevel
			// 
			this.cmbLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbLevel.FormattingEnabled = true;
			this.cmbLevel.Location = new System.Drawing.Point(0, 0);
			this.cmbLevel.Name = "cmbLevel";
			this.cmbLevel.Size = new System.Drawing.Size(121, 20);
			this.cmbLevel.TabIndex = 2;
			this.cmbLevel.SelectedIndexChanged += new System.EventHandler(this.refreshLogsList);
			// 
			// cmbLogger
			// 
			this.cmbLogger.FormattingEnabled = true;
			this.cmbLogger.Location = new System.Drawing.Point(0, 0);
			this.cmbLogger.Name = "cmbLogger";
			this.cmbLogger.Size = new System.Drawing.Size(121, 20);
			this.cmbLogger.TabIndex = 3;
			this.cmbLogger.TextChanged += new System.EventHandler(this.refreshLogsList);
			// 
			// lblLevel
			// 
			this.lblLevel.AutoSize = true;
			this.lblLevel.Location = new System.Drawing.Point(0, 0);
			this.lblLevel.Name = "lblLevel";
			this.lblLevel.Size = new System.Drawing.Size(100, 12);
			this.lblLevel.TabIndex = 0;
			// 
			// lblLogger
			// 
			this.lblLogger.AutoSize = true;
			this.lblLogger.Location = new System.Drawing.Point(0, 0);
			this.lblLogger.Name = "lblLogger";
			this.lblLogger.Size = new System.Drawing.Size(100, 12);
			this.lblLogger.TabIndex = 0;
			// 
			// lblCount
			// 
			this.lblCount.AutoSize = true;
			this.lblCount.Location = new System.Drawing.Point(0, 0);
			this.lblCount.Name = "lblCount";
			this.lblCount.Size = new System.Drawing.Size(100, 12);
			this.lblCount.TabIndex = 0;
			// 
			// nudCount
			// 
			this.nudCount.Location = new System.Drawing.Point(0, 0);
			this.nudCount.Name = "nudCount";
			this.nudCount.Size = new System.Drawing.Size(120, 19);
			this.nudCount.TabIndex = 1;
			this.nudCount.ValueChanged += new System.EventHandler(this.refreshLogsList);
			// 
			// controller
			// 
			this.controller.Dock = System.Windows.Forms.DockStyle.Top;
			this.controller.Location = new System.Drawing.Point(0, 0);
			this.controller.Name = "controller";
			this.controller.Size = new System.Drawing.Size(24, 24);
			this.controller.TabIndex = 0;
			// 
			// btnRefresh
			// 
			this.btnRefresh.Location = new System.Drawing.Point(0, 0);
			this.btnRefresh.Name = "btnRefresh";
			this.btnRefresh.Size = new System.Drawing.Size(75, 23);
			this.btnRefresh.TabIndex = 4;
			this.btnRefresh.UseVisualStyleBackColor = true;
			this.btnRefresh.Click += new System.EventHandler(this.refreshLogsList);
			((System.ComponentModel.ISupportInitialize)(this.nudCount)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView listView;
		private System.Windows.Forms.ColumnHeader col_CreatedDate;
		private System.Windows.Forms.ColumnHeader col_Level;
		private System.Windows.Forms.ColumnHeader col_Logger;
		private System.Windows.Forms.ColumnHeader col_Message;
		private System.Windows.Forms.ComboBox cmbLevel;
		private System.Windows.Forms.ComboBox cmbLogger;
		private System.Windows.Forms.Label lblLevel;
		private System.Windows.Forms.Label lblLogger;
		private System.Windows.Forms.Label lblCount;
		private System.Windows.Forms.NumericUpDown nudCount;
		private System.Windows.Forms.Panel controller;
		private System.Windows.Forms.Button btnRefresh;
	}
}
