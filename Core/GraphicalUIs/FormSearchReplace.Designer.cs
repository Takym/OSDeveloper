namespace OSDeveloper.Core.GraphicalUIs
{
	partial class FormSearchReplace
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
			this.labelDesc = new System.Windows.Forms.Label();
			this.labelOld = new System.Windows.Forms.Label();
			this.labelNew = new System.Windows.Forms.Label();
			this.tboxOld = new System.Windows.Forms.TextBox();
			this.tboxNew = new System.Windows.Forms.TextBox();
			this.btnClose = new System.Windows.Forms.Button();
			this.btnNext = new System.Windows.Forms.Button();
			this.btnCount = new System.Windows.Forms.Button();
			this.btnReplace = new System.Windows.Forms.Button();
			this.btnRepAll = new System.Windows.Forms.Button();
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
			// labelOld
			// 
			this.labelOld.AutoSize = true;
			this.labelOld.Location = new System.Drawing.Point(8, 56);
			this.labelOld.Name = "labelOld";
			this.labelOld.Size = new System.Drawing.Size(46, 12);
			this.labelOld.TabIndex = 1;
			this.labelOld.Text = "labelOld";
			// 
			// labelNew
			// 
			this.labelNew.AutoSize = true;
			this.labelNew.Location = new System.Drawing.Point(8, 112);
			this.labelNew.Name = "labelNew";
			this.labelNew.Size = new System.Drawing.Size(51, 12);
			this.labelNew.TabIndex = 3;
			this.labelNew.Text = "labelNew";
			// 
			// tboxOld
			// 
			this.tboxOld.Location = new System.Drawing.Point(8, 80);
			this.tboxOld.Name = "tboxOld";
			this.tboxOld.Size = new System.Drawing.Size(280, 19);
			this.tboxOld.TabIndex = 2;
			// 
			// tboxNew
			// 
			this.tboxNew.Location = new System.Drawing.Point(8, 136);
			this.tboxNew.Name = "tboxNew";
			this.tboxNew.Size = new System.Drawing.Size(280, 19);
			this.tboxNew.TabIndex = 4;
			// 
			// btnClose
			// 
			this.btnClose.Location = new System.Drawing.Point(304, 136);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(75, 23);
			this.btnClose.TabIndex = 5;
			this.btnClose.Text = "btnClose";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// btnNext
			// 
			this.btnNext.Location = new System.Drawing.Point(304, 8);
			this.btnNext.Name = "btnNext";
			this.btnNext.Size = new System.Drawing.Size(75, 23);
			this.btnNext.TabIndex = 6;
			this.btnNext.Text = "btnNext";
			this.btnNext.UseVisualStyleBackColor = true;
			this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
			// 
			// btnCount
			// 
			this.btnCount.Location = new System.Drawing.Point(304, 40);
			this.btnCount.Name = "btnCount";
			this.btnCount.Size = new System.Drawing.Size(75, 23);
			this.btnCount.TabIndex = 7;
			this.btnCount.Text = "btnCount";
			this.btnCount.UseVisualStyleBackColor = true;
			this.btnCount.Click += new System.EventHandler(this.btnCount_Click);
			// 
			// btnReplace
			// 
			this.btnReplace.Location = new System.Drawing.Point(304, 72);
			this.btnReplace.Name = "btnReplace";
			this.btnReplace.Size = new System.Drawing.Size(75, 23);
			this.btnReplace.TabIndex = 8;
			this.btnReplace.Text = "btnReplace";
			this.btnReplace.UseVisualStyleBackColor = true;
			this.btnReplace.Click += new System.EventHandler(this.btnReplace_Click);
			// 
			// btnRepAll
			// 
			this.btnRepAll.Location = new System.Drawing.Point(304, 104);
			this.btnRepAll.Name = "btnRepAll";
			this.btnRepAll.Size = new System.Drawing.Size(75, 23);
			this.btnRepAll.TabIndex = 9;
			this.btnRepAll.Text = "btnRepAll";
			this.btnRepAll.UseVisualStyleBackColor = true;
			this.btnRepAll.Click += new System.EventHandler(this.btnRepAll_Click);
			// 
			// FormSearchReplace
			// 
			this.AcceptButton = this.btnClose;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(384, 161);
			this.Controls.Add(this.btnRepAll);
			this.Controls.Add(this.btnReplace);
			this.Controls.Add(this.btnCount);
			this.Controls.Add(this.btnNext);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.tboxNew);
			this.Controls.Add(this.tboxOld);
			this.Controls.Add(this.labelNew);
			this.Controls.Add(this.labelOld);
			this.Controls.Add(this.labelDesc);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormSearchReplace";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "FormSearchReplace";
			this.Load += new System.EventHandler(this.FormSearchReplace_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label labelDesc;
		private System.Windows.Forms.Label labelOld;
		private System.Windows.Forms.Label labelNew;
		private System.Windows.Forms.TextBox tboxOld;
		private System.Windows.Forms.TextBox tboxNew;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnNext;
		private System.Windows.Forms.Button btnCount;
		private System.Windows.Forms.Button btnReplace;
		private System.Windows.Forms.Button btnRepAll;
	}
}