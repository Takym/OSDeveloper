// ライブラリ専用ヘッダ (stdafx.h が読み込まれている事が前提)

#pragma once
#ifndef LIBOSDEV_H
#define LIBOSDEV_H

#ifdef LIBOSDEV_DLL
#define LIBOSDEV_API(TRet) __declspec(dllexport) TRet WINAPI
#else
#define LIBOSDEV_API(TRet) __declspec(dllimport) TRet WINAPI
#endif//LIBOSDEV_DLL

#ifdef __cplusplus
extern "C" {
#endif//__cplusplus

/* dllmain.c */
#define LIB_STATE_NOT_INIT  0
#define LIB_STATE_NOT_FOUND 1
#define LIB_STATE_LOADED    2
#define LIB_STATE_DISPOSED  3
extern HMODULE hMod;
extern int     nStatus;

BOOL APIENTRY DllMain(
	_In_ HINSTANCE hInstance,
	_In_ DWORD     ul_reason_for_call,
	_In_ LPVOID    lpReserved
);

LIBOSDEV_API(int) osdev_checkStatus(void);

/* assets.c */
LIBOSDEV_API(HICON) osdev_loadIcon(
	_In_  DWORD  dwIconID,
	_In_  int    nSize,
	_In_  HWND   hWnd,
	_Out_ PDWORD pdwHResult
);

/* sendkey.c */
LIBOSDEV_API(void) osdev_sendKey(
	_In_ HWND hCtrl,
	_In_ BYTE nKeyCode,
	_In_ BYTE nModifier
);

#ifdef __cplusplus
}
#endif//__cplusplus

#endif//LIBOSDEV_H
