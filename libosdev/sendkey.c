// sendkeyä÷êîé¿ëï
#include "stdafx.h"

LIBOSDEV_API(void) osdev_sendKey(_In_ HWND hCtrl, _In_ BYTE nKeyCode, _In_ BYTE nModifier)
{
	//SendMessage(hCtrl, WM_KEYDOWN, nModifier, 0);
	//SendMessage(hCtrl, WM_KEYDOWN, nKeyCode,  0);
	//Sleep(50);
	//SendMessage(hCtrl, WM_KEYUP,   nKeyCode,  0);
	//SendMessage(hCtrl, WM_KEYUP,   nModifier, 0);

	HWND hWnd = GetFocus();
	SetFocus(hCtrl);
	keybd_event(nModifier, ((BYTE)(0)),               0, 0);
	keybd_event(nKeyCode,  ((BYTE)(0)),               0, 0);
	keybd_event(nKeyCode,  ((BYTE)(0)), KEYEVENTF_KEYUP, 0);
	keybd_event(nModifier, ((BYTE)(0)), KEYEVENTF_KEYUP, 0);
	SetFocus(hWnd);

	return;
}
