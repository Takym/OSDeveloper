using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TakymLib.IO.Tests
{
	[TestClass()]
	public class PathStringTests
	{
		[TestMethod()]
		public void GetRelativePathTest()
		{
			{
				// tp: T:\aaa\bbb\ccc
				// bp: T:\aaa\bbb\ccc\ddd\eee\fff
				// rp: ..\..\..
				var tp = new PathString("T:\\aaa\\bbb\\ccc");
				var bp = new PathString("T:\\aaa\\bbb\\ccc\\ddd\\eee\\fff");
				var rp = tp.GetRelativePath(bp);
				Assert.AreEqual(rp, "..\\..\\..");
			}
			{
				// tp: aaa\bbb\ccc\ddd\eee\fff
				// bp: aaa\bbb\ccc
				// rp: ddd\eee\fff
				var tp = new PathString("T:\\aaa\\bbb\\ccc\\ddd\\eee\\fff");
				var bp = new PathString("T:\\aaa\\bbb\\ccc");
				var rp = tp.GetRelativePath(bp);
				Assert.AreEqual(rp, "ddd\\eee\\fff");
			}
			{
				// tp: aaa\bbb\ccc\xxx
				// bp: aaa\bbb\ccc\ddd\eee\fff
				// rp: ..\..\..\xxx
				var tp = new PathString("T:\\aaa\\bbb\\ccc\\xxx");
				var bp = new PathString("T:\\aaa\\bbb\\ccc\\ddd\\eee\\fff");
				var rp = tp.GetRelativePath(bp);
				Assert.AreEqual(rp, "..\\..\\..\\xxx");
			}
			{
				// tp: aaa\xxx\yyy\ddd\eee\fff
				// bp: aaa\bbb\ccc
				// rp: ..\..\xxx\yyy\ddd\eee\fff
				var tp = new PathString("T:\\aaa\\xxx\\yyy\\ddd\\eee\\fff");
				var bp = new PathString("T:\\aaa\\bbb\\ccc");
				var rp = tp.GetRelativePath(bp);
				Assert.AreEqual(rp, "..\\..\\xxx\\yyy\\ddd\\eee\\fff");
			}
		}
	}
}
