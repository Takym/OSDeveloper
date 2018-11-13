using JavaScriptEngineSwitcher.V8;
using MarkdownDI = Markdig.Markdown;
using MarkdownDE = MarkdownDeep.Markdown;
using MarkdownS = MarkdownSharp.Markdown;
using System.Reflection;
using System.Text;

namespace OSDeveloper.OLR
{
	/// <summary>
	///  <see langword="Markdown"/>を<see langword="HTML"/>に変換します。
	///  このクラスは継承できません。
	/// </summary>
	public sealed class Markdown
	{
		private const string _rsrc_markedjs = "OSDeveloper.Scripts.marked.min.js";

		private MarkdownDE _mdde;
		private MarkdownS _mds;
		private V8JsEngine _js_engine;

		/// <summary>
		///  内部変数を全て初期化して、
		///  型'<see cref="OSDeveloper.OLR.Markdown"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		public Markdown()
		{
			_mdde = new MarkdownDE();
			_mds = new MarkdownS();
			_js_engine = new V8JsEngine();
			_js_engine.ExecuteResource(_rsrc_markedjs, Assembly.GetExecutingAssembly());
		}

		/// <summary>
		///  限定をカスタマイズして、
		///  指定された<see langword="Markdown"/>文字列を<see langword="HTML"/>に変換します。
		/// </summary>
		/// <param name="text">変換する<see langword="Markdown"/>文字列です。</param>
		/// <param name="title">生成される<see langword="HTML"/>の題名です。</param>
		/// <param name="enc">
		///  生成される<see langword="HTML"/>のヘッダー情報に埋め込まれる文字コードです。
		///  実際のエンコーディングは変更されません。
		/// </param>
		/// <param name="parser"><see langword="Markdown"/>を変換するライブラリの種類です。</param>
		/// <returns>変換結果の<see langword="HTML"/>文字列です。</returns>
		public string Transform(string text, string title, Encoding enc, MarkdownConverterType parser)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append($"<?xml version=\"1.0\" encoding=\"{enc.WebName}\" ?>");
			sb.Append($"<!DOCTYPE html><html><head><meta charset=\"{enc.WebName}\">");
			sb.Append($"<title>{title}</title></head><body>");
			switch (parser) {
				case MarkdownConverterType.MarkedJS:
					sb.Append("<!-- marked.js -->");
					sb.Append(_js_engine.CallFunction<string>("marked", text));
					break;
				case MarkdownConverterType.Markdig:
					sb.Append("<!-- Markdig -->");
					sb.Append(MarkdownDI.ToHtml(text));
					break;
				case MarkdownConverterType.MarkdownDeep:
					sb.Append("<!-- MarkdownDeep -->");
					sb.Append(_mdde.Transform(text));
					break;
				case MarkdownConverterType.MarkdownSharp:
					sb.Append("<!-- MarkdownSharp -->");
					sb.Append(_mds.Transform(text));
					break;
				default:
					sb.Append(text);
					break;
			}
			sb.Append("</body></html>");
			return sb.ToString();
		}

		/// <summary>
		///  限定の設定で、
		///  指定された<see langword="Markdown"/>文字列を<see langword="HTML"/>に変換します。
		/// </summary>
		/// <param name="text">変換する<see langword="Markdown"/>文字列です。</param>
		/// <returns>変換結果の<see langword="HTML"/>文字列です。</returns>
		public string Transform(string text)
		{
			return this.Transform(text, "untitled", Encoding.UTF8, MarkdownConverterType.MarkedJS);
		}

		/// <summary>
		///  指定された<see langword="Markdown"/>文字列を<see langword="HTML"/>に変換します。
		/// </summary>
		/// <param name="text">変換する<see langword="Markdown"/>文字列です。</param>
		/// <param name="title">生成される<see langword="HTML"/>の題名です。</param>
		/// <returns>変換結果の<see langword="HTML"/>文字列です。</returns>
		public static string Transform(string text, string title)
		{
			return _inst.Transform(text, title, Encoding.UTF8, MarkdownConverterType.MarkedJS);
		}
		private readonly static Markdown _inst = new Markdown();
	}

	/// <summary>
	///  <see langword="Markdown"/>の解析機の種類を表します。
	/// </summary>
	public enum MarkdownConverterType
	{
		/// <summary>
		///  <see langword="Markdown"/>の解析に<see langword="marked.js"/>を利用します。
		/// </summary>
		MarkedJS,

		/// <summary>
		///  <see langword="Markdown"/>の解析に<see langword="Markdig"/>を利用します。
		/// </summary>
		Markdig,

		/// <summary>
		///  <see langword="Markdown"/>の解析に<see langword="Markdown Deep"/>を利用します。
		/// </summary>
		MarkdownDeep,

		/// <summary>
		///  <see langword="Markdown"/>の解析に<see langword="Markdown Sharp"/>を利用します。
		/// </summary>
		MarkdownSharp,

		/// <summary>
		///  <see langword="Markdown"/>をそのまま表示します。
		/// </summary>
		RawText
	}
}
