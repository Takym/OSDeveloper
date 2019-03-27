// 事前コンパイル済みヘッダ
#pragma once
#ifndef STDAFX_H
#define STDAFX_H

// 無駄な警告を抑制
#define _CRT_SECURE_NO_WARNINGS
#pragma warning(disable:4255)

// 標準Cライブラリ
#include <stdio.h>
#include <stdlib.h>
#include <tchar.h>

// SDKライブラリ
#define WIN32_LEAN_AND_MEAN
#include <SDKDDKVer.h>
#include <windows.h>
#ifndef KEYEVENTF_KEYDOWN
#define KEYEVENTF_KEYDOWN 0x0000
#endif//KEYEVENTF_KEYDOWN

// 自作ヘッダファイル
#include "libosdev.h"
#include "assets.h"

#endif//STDAFX_H
