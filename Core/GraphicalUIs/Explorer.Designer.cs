namespace OSDeveloper.Core.GraphicalUIs
{
	partial class Explorer
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Explorer));
			this.treeView = new System.Windows.Forms.TreeView();
			this.popupMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.popup_openeditor = new System.Windows.Forms.ToolStripMenuItem();
			this.popup_rename = new System.Windows.Forms.ToolStripMenuItem();
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.mainContainer = new System.Windows.Forms.ToolStripContainer();
			this.toolStrip = new System.Windows.Forms.ToolStrip();
			this.tolbtnRefresh = new System.Windows.Forms.ToolStripButton();
			this.tolbtnExpand = new System.Windows.Forms.ToolStripButton();
			this.tolbtnCollapse = new System.Windows.Forms.ToolStripButton();
			this.popupMenu.SuspendLayout();
			this.mainContainer.ContentPanel.SuspendLayout();
			this.mainContainer.TopToolStripPanel.SuspendLayout();
			this.mainContainer.SuspendLayout();
			this.toolStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// treeView
			// 
			this.treeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.treeView.ContextMenuStrip = this.popupMenu;
			this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeView.ImageIndex = 0;
			this.treeView.ImageList = this.imageList;
			this.treeView.LabelEdit = true;
			this.treeView.Location = new System.Drawing.Point(0, 0);
			this.treeView.Name = "treeView";
			this.treeView.PathSeparator = "/";
			this.treeView.SelectedImageIndex = 0;
			this.treeView.Size = new System.Drawing.Size(300, 275);
			this.treeView.TabIndex = 0;
			this.treeView.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeView_AfterLabelEdit);
			this.treeView.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterCheck);
			this.treeView.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterCollapse);
			this.treeView.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterExpand);
			this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
			this.treeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseDoubleClick);
			// 
			// popupMenu
			// 
			this.popupMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.popup_openeditor,
            this.popup_rename});
			this.popupMenu.Name = "popupMenu";
			this.popupMenu.Size = new System.Drawing.Size(138, 48);
			// 
			// popup_openeditor
			// 
			this.popup_openeditor.Name = "popup_openeditor";
			this.popup_openeditor.Size = new System.Drawing.Size(137, 22);
			this.popup_openeditor.Text = "open_editor";
			this.popup_openeditor.Click += new System.EventHandler(this.popup_openeditor_Click);
			// 
			// popup_rename
			// 
			this.popup_rename.Name = "popup_rename";
			this.popup_rename.Size = new System.Drawing.Size(137, 22);
			this.popup_rename.Text = "rename";
			this.popup_rename.Click += new System.EventHandler(this.popup_rename_Click);
			// 
			// imageList
			// 
			this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
			this.imageList.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// mainContainer
			// 
			this.mainContainer.BottomToolStripPanelVisible = false;
			// 
			// mainContainer.ContentPanel
			// 
			this.mainContainer.ContentPanel.Controls.Add(this.treeView);
			this.mainContainer.ContentPanel.Size = new System.Drawing.Size(300, 275);
			this.mainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mainContainer.LeftToolStripPanelVisible = false;
			this.mainContainer.Location = new System.Drawing.Point(0, 0);
			this.mainContainer.Name = "mainContainer";
			this.mainContainer.RightToolStripPanelVisible = false;
			this.mainContainer.Size = new System.Drawing.Size(300, 300);
			this.mainContainer.TabIndex = 1;
			this.mainContainer.Text = "mainContainer";
			// 
			// mainContainer.TopToolStripPanel
			// 
			this.mainContainer.TopToolStripPanel.BackColor = System.Drawing.SystemColors.ControlDark;
			this.mainContainer.TopToolStripPanel.Controls.Add(this.toolStrip);
			// 
			// toolStrip
			// 
			this.toolStrip.Dock = System.Windows.Forms.DockStyle.None;
			this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tolbtnRefresh,
            this.tolbtnExpand,
            this.tolbtnCollapse});
			this.toolStrip.Location = new System.Drawing.Point(6, 0);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.Size = new System.Drawing.Size(112, 25);
			this.toolStrip.TabIndex = 0;
			// 
			// tolbtnRefresh
			// 
			this.tolbtnRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tolbtnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("tolbtnRefresh.Image")));
			this.tolbtnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tolbtnRefresh.Name = "tolbtnRefresh";
			this.tolbtnRefresh.Size = new System.Drawing.Size(23, 22);
			this.tolbtnRefresh.Text = "tolbtnRefresh";
			this.tolbtnRefresh.ToolTipText = "tolbtnRefresh";
			this.tolbtnRefresh.Click += new System.EventHandler(this.tolbtnRefresh_Click);
			// 
			// tolbtnExpand
			// 
			this.tolbtnExpand.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tolbtnExpand.Image = ((System.Drawing.Image)(resources.GetObject("tolbtnExpand.Image")));
			this.tolbtnExpand.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tolbtnExpand.Name = "tolbtnExpand";
			this.tolbtnExpand.Size = new System.Drawing.Size(23, 22);
			this.tolbtnExpand.Text = "tolbtnExpand";
			this.tolbtnExpand.ToolTipText = "tolbtnExpand";
			this.tolbtnExpand.Click += new System.EventHandler(this.tolbtnExpand_Click);
			// 
			// tolbtnCollapse
			// 
			this.tolbtnCollapse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tolbtnCollapse.Image = ((System.Drawing.Image)(resources.GetObject("tolbtnCollapse.Image")));
			this.tolbtnCollapse.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tolbtnCollapse.Name = "tolbtnCollapse";
			this.tolbtnCollapse.Size = new System.Drawing.Size(23, 22);
			this.tolbtnCollapse.Text = "tolbtnCollapse";
			this.tolbtnCollapse.ToolTipText = "tolbtnCollapse";
			this.tolbtnCollapse.Click += new System.EventHandler(this.tolbtnCollapse_Click);
			// 
			// Explorer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.mainContainer);
			this.Name = "Explorer";
			this.Size = new System.Drawing.Size(300, 300);
			this.Load += new System.EventHandler(this.Explorer_Load);
			this.popupMenu.ResumeLayout(false);
			this.mainContainer.ContentPanel.ResumeLayout(false);
			this.mainContainer.TopToolStripPanel.ResumeLayout(false);
			this.mainContainer.TopToolStripPanel.PerformLayout();
			this.mainContainer.ResumeLayout(false);
			this.mainContainer.PerformLayout();
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TreeView treeView;
		private System.Windows.Forms.ImageList imageList;
		private System.Windows.Forms.ToolStripContainer mainContainer;
		private System.Windows.Forms.ToolStrip toolStrip;
		private System.Windows.Forms.ToolStripButton tolbtnRefresh;
		private System.Windows.Forms.ToolStripButton tolbtnExpand;
		private System.Windows.Forms.ToolStripButton tolbtnCollapse;
		private System.Windows.Forms.ContextMenuStrip popupMenu;
		private System.Windows.Forms.ToolStripMenuItem popup_openeditor;
		private System.Windows.Forms.ToolStripMenuItem popup_rename;
	}
}
