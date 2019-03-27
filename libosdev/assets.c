// リソース関連
#include "stdafx.h"

LIBOSDEV_API(HICON) osdev_loadIcon
(_In_ const DWORD dwIconID, _In_ const int nSize, _In_ HWND hWnd, _Out_ PDWORD pdwHResult)
{
	//HICON r = LoadIcon(hMod, MAKEINTRESOURCE(dwIconID));
	//HICON r = ((HICON)(LoadImage(hMod, MAKEINTRESOURCE(dwIconID), IMAGE_ICON, 0, 0, LR_DEFAULTCOLOR)));
	//HRSRC hRsrc = FindResource(hMod, MAKEINTRESOURCE(dwIconID), RT_ICON);
	//HICON r = ((HICON)(LoadResource(hMod, hRsrc)));

	HICON r = ((HICON)(LoadImage(hMod, MAKEINTRESOURCE(dwIconID), IMAGE_ICON, nSize, nSize, LR_DEFAULTCOLOR)));

	if (r == NULL) {
		*pdwHResult = GetLastError();
		if (hWnd != NULL) {
			LPVOID lpMsgBuf;
			FormatMessage(
				FORMAT_MESSAGE_ALLOCATE_BUFFER | FORMAT_MESSAGE_FROM_SYSTEM | FORMAT_MESSAGE_IGNORE_INSERTS,
				NULL, *pdwHResult, MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT), ((LPTSTR)(&lpMsgBuf)), 0, NULL);
			MessageBox(hWnd, ((LPCTSTR)(lpMsgBuf)), _TEXT("OSDeveloper - Icon Loader"), MB_OK | MB_ICONERROR);
			LocalFree(lpMsgBuf);
		}
		return NULL;
	} else {
		*pdwHResult = NO_ERROR;
		return r;
	}
}
