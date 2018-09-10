 /**
 * Native Library for and Resource OSDeveloper
 * Copyright (C) 2018 Takym.
 * API�񋟃w�b�_�[ (stdafx.h ���ǂݍ��܂�Ă��鎖���O��)
 */

#pragma once
#ifndef LIBOSDEV_H
#define LIBOSDEV_H

// API�񋟗p
#if LIBOSDEV_EXPORTS
#define LIBOSDEV_API(TRet) TRet __declspec(dllexport) __stdcall
#else
#define LIBOSDEV_API(TRet) TRet __declspec(dllimport) __stdcall
#endif // LIBOSDEV_EXPORTS

/* �^��` */
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

/* �\���� */
typedef struct __LIB_STATE__ {
	UINT32 value;
} LIB_STATE;

/* �萔 */
#define LIB_STATE_NORMAL		((struct __LIB_STATE__){ 0x00000000 }) // ���C�u����������Ɏ��s���Ă��鎖��\���܂��B
#define LIB_STATE_NOT_INIT		((struct __LIB_STATE__){ 0x00000001 }) // ���C�u�������܂�����������Ă��Ȃ�����\���܂��B
#define LIB_STATE_NOT_FOUND		((struct __LIB_STATE__){ 0x00000002 }) // ���C�u�������t�@�C���V�X�e���ɑ��݂��Ȃ�����\���܂��B
#define LIB_STATE_UNKNOWN		((struct __LIB_STATE__){ 0xFFFFFFFF }) // �s���ȏ�Ԃ�\���܂��B

#define ICON_Main						 0 // �A�v���P�[�V�����̃A�C�R���ł��B
#define ICON_FormMain_v0_0_0_0			 1 // ���C���E�B���h�E�̃A�C�R���ł��B       ���̃A�C�R���� 0.0.0.0 �Œǉ����ꂽ���ł��B
#define ICON_Unknown_v0_0_0_0			 2 // �s���ł��鎖��\���A�C�R���ł��B       ���̃A�C�R���� 0.0.0.0 �Œǉ����ꂽ���ł��B
#define ICON_File_v0_0_0_0				 3 // �t�@�C����\���A�C�R���ł��B           ���̃A�C�R���� 0.0.0.0 �Œǉ����ꂽ���ł��B
#define ICON_BinaryFile_v0_0_0_0		 4 // �o�C�i���t�@�C����\���A�C�R���ł��B   ���̃A�C�R���� 0.0.0.0 �Œǉ����ꂽ���ł��B
#define ICON_TextFile_v0_0_0_0			 5 // �e�L�X�g�t�@�C����\���A�C�R���ł��B   ���̃A�C�R���� 0.0.0.0 �Œǉ����ꂽ���ł��B
#define ICON_Folder_v0_0_0_0			 6 // �t�H���_��\���A�C�R���ł��B           ���̃A�C�R���� 0.0.0.0 �Œǉ����ꂽ���ł��B
#define ICON_FolderClose_v0_0_0_0		 7 // ���Ă���t�H���_��\���A�C�R���ł��B ���̃A�C�R���� 0.0.0.0 �Œǉ����ꂽ���ł��B
#define ICON_FolderOpen_v0_0_0_0		 8 // �J���Ă���t�H���_��\���A�C�R���ł��B ���̃A�C�R���� 0.0.0.0 �Œǉ����ꂽ���ł��B
#define ICON_Refresh_v0_0_0_0			 9 // �u�X�V�v��\���A�C�R���ł��B           ���̃A�C�R���� 0.0.0.0 �Œǉ����ꂽ���ł��B
#define ICON_Expand_v0_0_0_0			10 // �u�S�ĕ\���v��\���A�C�R���ł��B       ���̃A�C�R���� 0.0.0.0 �Œǉ����ꂽ���ł��B
#define ICON_Collapse_v0_0_0_0			11 // �u�S�ĉB���v��\���A�C�R���ł��B       ���̃A�C�R���� 0.0.0.0 �Œǉ����ꂽ���ł��B

/* �֐� */
#if __cplusplus
extern "C" {
#endif // __cplusplus

	// ���C�u�����̏�Ԃ��m�F���܂��B
	LIBOSDEV_API(LIB_STATE) osdev_checkStatus(void);

	// �w�肳�ꂽID�̃A�C�R�����擾���܂��B
	LIBOSDEV_API(HICON) osdev_getIcon(
		DWORD  dwIconID,    // �擾����A�C�R���̃��\�[�XID����1000���������l�ł��B
		HWND   hWnd,        // �G���[�_�C�A���O��\������ꍇ�́A�e�E�B���h�E�̃n���h���A�\�����Ȃ��ꍇ�ANULL���w�肵�܂��B
		PDWORD pdwHResult); // �G���[�����������ꍇ�A���̃G���[�̔ԍ����������܂��B

#if __cplusplus
}
#endif // __cplusplus

#endif // !LIBOSDEV_H
