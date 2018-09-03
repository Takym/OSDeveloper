/**
 * Native Library and Resource for OSDeveloper
 * Copyright (C) 2018 Takym.
 * 共通ヘッダー
 */

#pragma once
#ifndef STDAFX_H
#define STDAFX_H
#define WIN32_LEAN_AND_MEAN
#define _CRT_SECURE_NO_WARNINGS

#include <SDKDDKVer.h>
#include <windows.h>
#include <stdlib.h>
#include <stdio.h>
#include <stddef.h>
#include <tchar.h>

extern HMODULE hMod;

#include "resource.h"
#include "libosdev.h"
#endif // !STDAFX_H
