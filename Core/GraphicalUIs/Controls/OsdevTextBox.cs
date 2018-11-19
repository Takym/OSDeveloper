#pragma warning disable CS0809 // 旧形式のメンバーが、旧形式でないメンバーをオーバーライドします
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using OSDeveloper.Assets;
using OSDeveloper.Core.MiscUtils;

namespace OSDeveloper.Core.GraphicalUIs.Controls
{
	/// <summary>
	///  <see langword="OSDeveloper"/>のテキストエディタで利用される専用のテキストボックスです。
	/// </summary>
	[Docking(DockingBehavior.Ask)]
	[DefaultProperty(nameof(Text))]
	[DefaultEvent(nameof(TextChanged))]
	public partial class OsdevTextBox : Control
	{
		/// <summary>
		///  このテキストボックスに格納されている文字列を取得または設定します。
		/// </summary>
		public override string Text
		{
			get
			{
				return base.Text;
			}

			set
			{
				_lines = value.CRtoLF().Split('\n');
				base.Text = value;
			}
		}

		/// <summary>
		///  このテキストボックスに格納されているテキスト行を取得または設定します。
		///  配列の値には改行コード(LFやCR等)を含む場合は無視されます。
		/// </summary>
		public string[] Lines
		{
			get
			{
				return _lines;
			}

			set
			{
				_lines = value;
				base.Text = string.Join("\r\n", value);
			}
		}
		private string[] _lines;

		/// <summary>
		///  型'<see cref="OSDeveloper.Core.GraphicalUIs.Controls.OsdevTextBox"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		public OsdevTextBox()
		{
			InitializeComponent();
			this.SetStyle(
				ControlStyles.UserPaint |
				ControlStyles.Opaque |
				ControlStyles.ResizeRedraw |
				ControlStyles.Selectable |
				ControlStyles.UserMouse |
				ControlStyles.AllPaintingInWmPaint |
				ControlStyles.OptimizedDoubleBuffer,
				true);
		}

		/// <summary>
		///  <see cref="System.Windows.Forms.Control.Paint"/>イベントを発生させます。
		/// </summary>
		/// <param name="e">
		///  イベントデータを格納している<see cref="System.Windows.Forms.PaintEventArgs"/>オブジェクトです。
		/// </param>
		protected override void OnPaint(PaintEventArgs e)
		{
			this.SuspendLayout();
			base.OnPaint(e);

			e.Graphics.Clear(Color.Black);
			using (Font f = FontResources.CreateGothic()) {
				string[] lines = this.Text.CRtoLF().Split('\n');
				{
					int x = f.Height * 3;
					e.Graphics.DrawLine(Pens.Salmon, x, 0, x, this.Height);
					for (int i = 0; i < this.Width; ++i) {
						x = i * f.Height / 2;
						if (i % 2 == 0) {
							e.Graphics.DrawLine(Pens.LightSalmon, x, 0, x, f.Height);
						} else {
							e.Graphics.DrawLine(Pens.DarkSalmon, x, 0, x, f.Height / 2);
						}
					}
				}
				for (int i = 0; i < lines.Length; ++i) {
					int y = (i + 1) * f.Height;
					e.Graphics.DrawString($"{i + 1:D5}", f, Brushes.Salmon, new Point(0, y));
					e.Graphics.DrawString(lines[i], f, Brushes.White, new Point(f.Height * 3, y));
				}
			}

			this.ResumeLayout(false);
		}

		/// <summary>
		///  <see cref="System.Windows.Forms.Control.KeyPress"/>イベントを発生させます。
		/// </summary>
		/// <param name="e">
		///  イベントデータを格納している<see cref="System.Windows.Forms.KeyPressEventArgs"/>オブジェクトです。
		/// </param>
		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			base.OnKeyPress(e);

			this.Text += e.KeyChar;
			e.Handled = true;
		}

		/// <summary>
		///  <see cref="System.Windows.Forms.Control.TextChanged"/>イベントを発生させます。
		/// </summary>
		/// <param name="e">
		///  イベントデータを格納している<see cref="System.EventArgs"/>オブジェクトです。
		/// </param>
		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged(e);
			this.Invalidate();
		}

		/// <summary>
		///  背景描画イベントを無効化します。
		/// </summary>
		/// <param name="e">利用されていません。互換性の為に存在しています。</param>
		[Obsolete("このクラスでは背景描画イベントは利用されていません。")]
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			// 処理速度向上のため背景描画停止。
		}
	}
}
