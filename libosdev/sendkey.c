// sendkeyä÷êîé¿ëï
#include "stdafx.h"

int nSendKeyMode;

LIBOSDEV_API(void) osdev_sendKey(_In_ const HWND hCtrl, _In_ const BYTE nKeyCode, _In_ const BYTE nModifier)
{
	INPUT ks[2];
	HWND hWnd = GetFocus();
	SetFocus(hCtrl);

	switch (nSendKeyMode) {
		case SENDKEY_MODE_MSG:
			SendMessage(hCtrl, WM_KEYDOWN, nModifier, 0);
			SendMessage(hCtrl, WM_KEYDOWN, nKeyCode,  0);
			SendMessage(hCtrl, WM_KEYUP,   nKeyCode,  0);
			SendMessage(hCtrl, WM_KEYUP,   nModifier, 0);
			break;
		case SENDKEY_MODE_EVENT:
			keybd_event(nModifier, ((BYTE)(0)), KEYEVENTF_KEYDOWN, 0);
			keybd_event(nKeyCode,  ((BYTE)(0)), KEYEVENTF_KEYDOWN, 0);
			keybd_event(nKeyCode,  ((BYTE)(0)), KEYEVENTF_KEYUP,   0);
			keybd_event(nModifier, ((BYTE)(0)), KEYEVENTF_KEYUP,   0);
			break;
		case SENDKEY_MODE_INPUT:
			ks[0].type           = INPUT_KEYBOARD;
			ks[0].ki.wVk         = nModifier;
			ks[0].ki.time        = 0;
			ks[0].ki.wScan       = 0;
			ks[0].ki.dwExtraInfo = 0;
			ks[0].ki.dwFlags     = KEYEVENTF_KEYDOWN;
			ks[1].type           = INPUT_KEYBOARD;
			ks[1].ki.wVk         = nKeyCode;
			ks[1].ki.time        = 0;
			ks[1].ki.wScan       = 0;
			ks[1].ki.dwExtraInfo = 0;
			ks[0].ki.dwFlags     = KEYEVENTF_KEYDOWN;
			SendInput(2, ks, sizeof(INPUT));
			ks[0].ki.wVk         = nKeyCode;
			ks[0].ki.dwFlags     = KEYEVENTF_KEYUP;
			ks[1].ki.wVk         = nModifier;
			ks[1].ki.dwFlags     = KEYEVENTF_KEYUP;
			SendInput(2, ks, sizeof(INPUT));
			break;
	}

	SetFocus(hWnd);
	return;
}

LIBOSDEV_API(int) osdev_sendKey_getMode()
{
	return nSendKeyMode;
}

LIBOSDEV_API(BOOL) osdev_sendKey_setMode(_In_ const int nMode)
{
	if (0 <= nMode && nMode < 3) {
		nSendKeyMode = nMode;
		return TRUE;
	}
	return FALSE;
}
