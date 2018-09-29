﻿namespace OSDeveloper.Core.GraphicalUIs
{
	/// <summary>
	///  メインウィンドウのステータスバーで表示される、ソフトウェアの状態を表す文字列の一覧です。
	///  このクラスは継承できません。
	/// </summary>
	public static class MainWindowStatusMessage
	{
		/// <summary>
		///  準備中である事を表わします。
		/// </summary>
		/// <returns>翻訳済みのメッセージです。</returns>
		public static string Preparing()
		{
			return MainWindowStatusMessageAsset.Preparing;
		}

		/// <summary>
		///  準備が完了した事を表します。
		/// </summary>
		/// <returns>翻訳済みのメッセージです。</returns>
		public static string Ready()
		{
			return MainWindowStatusMessageAsset.Ready;
		}

		/// <summary>
		///  処理が未実装である事を表します。
		/// </summary>
		/// <param name="proc">未実装の処理の名前です。</param>
		/// <returns>翻訳済みのメッセージです。</returns>
		public static string Unimplemented(string proc)
		{
			return string.Format(MainWindowStatusMessageAsset.Unimplemented, proc);
		}
	}
}
