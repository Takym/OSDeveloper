// ���O�R���p�C���ς݃w�b�_
#pragma once
#ifndef STDAFX_H
#define STDAFX_H

// ���ʂȌx����}��
#define _CRT_SECURE_NO_WARNINGS
#pragma warning(disable:4255)

// �W��C���C�u����
#include <stdio.h>
#include <stdlib.h>
#include <tchar.h>

// SDK���C�u����
#define WIN32_LEAN_AND_MEAN
#include <SDKDDKVer.h>
#include <windows.h>
#ifndef KEYEVENTF_KEYDOWN
#define KEYEVENTF_KEYDOWN 0x0000
#endif//KEYEVENTF_KEYDOWN

// ����w�b�_�t�@�C��
#include "libosdev.h"
#include "assets.h"

#endif//STDAFX_H
