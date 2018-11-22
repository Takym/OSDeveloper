/**
 * Native Library and Resource for OSDeveloper
 * Copyright (C) 2018 Takym.
 * プログラム本体
 */

#include "stdafx.h"

HMODULE hMod = NULL;

BOOL APIENTRY DllMain(HMODULE hModule, DWORD  ul_reason_for_call, LPVOID lpReserved)
{
	switch (ul_reason_for_call)
	{
		case DLL_PROCESS_ATTACH:
			hMod = hModule;
			break;
		case DLL_THREAD_ATTACH:
		case DLL_THREAD_DETACH:
		case DLL_PROCESS_DETACH:
			break;
	}
	return TRUE;
}

LIBOSDEV_API(LIB_STATE) osdev_checkStatus()
{
	if (hMod == NULL) {
		return LIB_STATE_NOT_INIT;
	}
	return LIB_STATE_NORMAL;
}

LIBOSDEV_API(HICON) osdev_getIcon(DWORD dwIconID, HWND hWnd, PDWORD pdwHResult)
{
	//HICON r = LoadIcon(hMod, MAKEINTRESOURCE(dwIconID));
	//HICON r = ((HICON)(LoadImage(hMod, MAKEINTRESOURCE(dwIconID), IMAGE_ICON, 0, 0, LR_DEFAULTCOLOR)));
	//HRSRC hRsrc = FindResource(hMod, MAKEINTRESOURCE(dwIconID), RT_ICON);
	//HICON r = ((HICON)(LoadResource(hMod, hRsrc)));

	HICON r = ((HICON)(LoadImage(hMod, MAKEINTRESOURCE(dwIconID + 1000), IMAGE_ICON, 16, 16, LR_DEFAULTCOLOR)));

	if (r == NULL && hWnd != NULL) {
		*pdwHResult = GetLastError();
		LPVOID lpMsgBuf;
		FormatMessage(
			FORMAT_MESSAGE_ALLOCATE_BUFFER | FORMAT_MESSAGE_FROM_SYSTEM | FORMAT_MESSAGE_IGNORE_INSERTS,
			NULL, *pdwHResult, MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT), ((LPTSTR)(&lpMsgBuf)), 0, NULL);
		MessageBox(hWnd, ((LPCTSTR)(lpMsgBuf)), _TEXT("OSDeveloper - Icon Reader"), MB_OK | MB_ICONERROR);
		LocalFree(lpMsgBuf);
		return NULL;
	}
	*pdwHResult = NO_ERROR;
	return r;
}

LIBOSDEV_API(void) osdev_sendKey(HWND hCtrl, unsigned int nKeyCode, unsigned int nModifier)
{
	//SendMessage(hCtrl, WM_KEYDOWN, nModifier, 0);
	//SendMessage(hCtrl, WM_KEYDOWN, nKeyCode,  0);
	//Sleep(50);
	//SendMessage(hCtrl, WM_KEYUP,   nKeyCode,  0);
	//SendMessage(hCtrl, WM_KEYUP,   nModifier, 0);

	HWND hWnd = GetFocus();
	SetFocus(hCtrl);
	keybd_event(nModifier, 0,               0, 0);
	keybd_event(nKeyCode,  0,               0, 0);
	keybd_event(nKeyCode,  0, KEYEVENTF_KEYUP, 0);
	keybd_event(nModifier, 0, KEYEVENTF_KEYUP, 0);
	SetFocus(hWnd);

	return;
}
