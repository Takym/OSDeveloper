using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OSDeveloper.Core.GraphicalUIs
{
	/// <summary>
	///  ターミナルタブを表します。
	///  ターミナルタブとは画面下部に表示される複数のコンソール出力、エラー一覧、タスク一覧等を
	///  管理しタブ形式で表示するコントロールです。
	/// </summary>
	public partial class TerminalTab : TabControl
	{
		/// <summary>
		///  型'<see cref="OSDeveloper.Core.GraphicalUIs.TerminalTab"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		public TerminalTab()
		{
			this.InitializeComponent();
		}

		/// <summary>
		///  <see cref="System.Windows.Forms.TabControl.DrawItem"/>を発生させます。
		/// </summary>
		/// <param name="e">
		///  イベントデータを格納している<see cref="System.Windows.Forms.DrawItemEventArgs"/>オブジェクトです。
		/// </param>
		protected override void OnDrawItem(DrawItemEventArgs e)
		{
			this.SuspendLayout();
			base.OnDrawItem(e);

			string caption = this.TabPages[e.Index].Text;
			e.Graphics.DrawString(
				caption,
				e.Font,
				SystemBrushes.WindowText,
				e.Bounds.Left + 4,
				e.Bounds.Top + 4);

			if (e.State == DrawItemState.Selected) {
				var closebtn = new Rectangle(e.Bounds.Right - 16, e.Bounds.Top + 4, 12, 12);
				e.Graphics.FillRectangle(SystemBrushes.ButtonFace, closebtn);
				e.Graphics.DrawRectangle(SystemPens.ButtonShadow, closebtn);
				e.Graphics.DrawLine(
					SystemPens.WindowText,
					closebtn.Left + 4, closebtn.Top + 4, closebtn.Right - 4, closebtn.Bottom - 4);
				e.Graphics.DrawLine(
					SystemPens.WindowText,
					closebtn.Right - 4, closebtn.Top + 4, closebtn.Left + 4, closebtn.Bottom - 4);
			}

			e.DrawFocusRectangle();

			this.ResumeLayout();
		}
	}
}
