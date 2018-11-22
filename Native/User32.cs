using System.Drawing;
using System.Windows.Forms;

namespace OSDeveloper.Native
{
	/// <summary>
	///  <see cref="OSDeveloper.Native.WinapiWrapper.User32"/>内の関数を利用し易い形にラップします。
	/// </summary>
	public static class User32
	{
		/// <summary>
		///  指定されたコントロールをキャプチャして画像を生成します。
		/// </summary>
		/// <param name="c">キャプチャ対象のコントロールです。</param>
		/// <returns>取得した画像オブジェクトです。</returns>
		public static Image CaptureControl(Control c)
		{
			Bitmap img = new Bitmap(c.Width, c.Height);
			using (Graphics g = Graphics.FromImage(img)) {
				var dc = g.GetHdc();
				WinapiWrapper.User32.PrintWindow(c.Handle, dc, 0);
				g.ReleaseHdc(dc);
			}
			return img;
		}
	}
}
