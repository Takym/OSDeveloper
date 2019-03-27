// DllMain’è‹`
#include "stdafx.h"

HMODULE hMod;
int     nStatus;

#pragma warning(disable:4100)

//BOOL WINAPI   DllMain(_In_ HINSTANCE hinstDLL,  _In_ DWORD fdwReason,           _In_ LPVOID lpvReserved)
BOOL   APIENTRY DllMain(_In_ HINSTANCE hInstance, _In_ DWORD  ul_reason_for_call, _In_ LPVOID lpReserved)
{
	switch (ul_reason_for_call) {
		case DLL_PROCESS_ATTACH:
			hMod         = ((HMODULE)(hInstance));
			nStatus      = LIB_STATE_LOADED;
			nSendKeyMode = SENDKEY_MODE_INPUT;
			break;
		case DLL_PROCESS_DETACH:
			nStatus = LIB_STATE_DISPOSED;
			break;
		case DLL_THREAD_ATTACH:
		case DLL_THREAD_DETACH:
			break;
	}
	return TRUE;
}

#pragma warning(default:4100)

LIBOSDEV_API(int) osdev_checkStatus()
{
	return nStatus;
}
