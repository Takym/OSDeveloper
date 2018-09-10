 /**
 * Native Library for and Resource OSDeveloper
 * Copyright (C) 2018 Takym.
 * API提供ヘッダー (stdafx.h が読み込まれている事が前提)
 */

#pragma once
#ifndef LIBOSDEV_H
#define LIBOSDEV_H

// API提供用
#if LIBOSDEV_EXPORTS
#define LIBOSDEV_API(TRet) TRet __declspec(dllexport) __stdcall
#else
#define LIBOSDEV_API(TRet) TRet __declspec(dllimport) __stdcall
#endif // LIBOSDEV_EXPORTS

/* 型定義 */
typedef unsigned __int8			UINT8;   //, UBYTE,  BYTE;
typedef   signed __int8			SINT8;   //, SBYTE,  ;
typedef unsigned __int16		UINT16;  //, USHORT, ;
typedef   signed __int16		SINT16;  //, SSHORT, SHORT;
typedef unsigned __int32		UINT32;  //, UINT,   ;
typedef   signed __int32		SINT32;  //, SINT,   INT;
typedef unsigned __int64		UINT64;  //, ULONG,  ;
typedef   signed __int64		SINT64;  //, SLONG,  LONG;
#if !!AVAILABLE_128BIT_INTEGER
typedef unsigned __int128		UINT128; //, UVAST,  VAST;
typedef   signed __int128		SINT128; //, SVAST;
#endif // AVAILABLE_128BIT_INTEGER

/* 構造体 */
typedef struct __LIB_STATE__ {
	UINT32 value;
} LIB_STATE;

/* 定数 */
#define LIB_STATE_NORMAL		((struct __LIB_STATE__){ 0x00000000 }) // ライブラリが正常に実行している事を表します。
#define LIB_STATE_NOT_INIT		((struct __LIB_STATE__){ 0x00000001 }) // ライブラリがまだ初期化されていない事を表します。
#define LIB_STATE_NOT_FOUND		((struct __LIB_STATE__){ 0x00000002 }) // ライブラリがファイルシステムに存在しない事を表します。
#define LIB_STATE_UNKNOWN		((struct __LIB_STATE__){ 0xFFFFFFFF }) // 不明な状態を表します。

#define ICON_Main						 0 // アプリケーションのアイコンです。
#define ICON_FormMain_v0_0_0_0			 1 // メインウィンドウのアイコンです。       このアイコンは 0.0.0.0 で追加された物です。
#define ICON_Unknown_v0_0_0_0			 2 // 不明である事を表すアイコンです。       このアイコンは 0.0.0.0 で追加された物です。
#define ICON_File_v0_0_0_0				 3 // ファイルを表すアイコンです。           このアイコンは 0.0.0.0 で追加された物です。
#define ICON_BinaryFile_v0_0_0_0		 4 // バイナリファイルを表すアイコンです。   このアイコンは 0.0.0.0 で追加された物です。
#define ICON_TextFile_v0_0_0_0			 5 // テキストファイルを表すアイコンです。   このアイコンは 0.0.0.0 で追加された物です。
#define ICON_Folder_v0_0_0_0			 6 // フォルダを表すアイコンです。           このアイコンは 0.0.0.0 で追加された物です。
#define ICON_FolderClose_v0_0_0_0		 7 // 閉じているフォルダを表すアイコンです。 このアイコンは 0.0.0.0 で追加された物です。
#define ICON_FolderOpen_v0_0_0_0		 8 // 開いているフォルダを表すアイコンです。 このアイコンは 0.0.0.0 で追加された物です。
#define ICON_Refresh_v0_0_0_0			 9 // 「更新」を表すアイコンです。           このアイコンは 0.0.0.0 で追加された物です。
#define ICON_Expand_v0_0_0_0			10 // 「全て表示」を表すアイコンです。       このアイコンは 0.0.0.0 で追加された物です。
#define ICON_Collapse_v0_0_0_0			11 // 「全て隠す」を表すアイコンです。       このアイコンは 0.0.0.0 で追加された物です。

/* 関数 */
#if __cplusplus
extern "C" {
#endif // __cplusplus

	// ライブラリの状態を確認します。
	LIBOSDEV_API(LIB_STATE) osdev_checkStatus(void);

	// 指定されたIDのアイコンを取得します。
	LIBOSDEV_API(HICON) osdev_getIcon(
		DWORD  dwIconID,    // 取得するアイコンのリソースIDから1000を引いた値です。
		HWND   hWnd,        // エラーダイアログを表示する場合は、親ウィンドウのハンドル、表示しない場合、NULLを指定します。
		PDWORD pdwHResult); // エラーが発生した場合、そのエラーの番号が代入されます。

#if __cplusplus
}
#endif // __cplusplus

#endif // !LIBOSDEV_H
