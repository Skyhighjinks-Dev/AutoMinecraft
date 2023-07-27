#include <iostream>
#include <string>
#include <vector>
#include <sstream>
#include <inttypes.h>
#include <windows.h>

// list all PIDs and TIDs
#include <tlhelp32.h>
#include <Psapi.h>
#include <vector>
#include "ntinfo.h"


#pragma pack(push, 1)
typedef struct
{
	DWORD64 Address;
	int Position;
} ThreadData;
#pragma pack(pop)


extern "C" __declspec(dllexport) ThreadData* GetAllThreadStartAddresses(DWORD pID, int* count);
std::vector<uint64_t> threadList(uint64_t pid);
uint64_t GetThreadStartAddress(HANDLE processHandle, HANDLE hThread);


extern "C" ThreadData * GetAllThreadStartAddresses(DWORD pID, int* count)
{
	HANDLE hProcHandle = NULL;
	hProcHandle = OpenProcess(PROCESS_ALL_ACCESS, FALSE, pID);
	if (hProcHandle == NULL)
	{
		// Failed to open the process, handle the error appropriately.
		// For example, throw an exception or return NULL with *count = 0.
		throw std::runtime_error("Failed to open process.");
	}

	std::vector<uint64_t> threadId = threadList(pID);
	std::vector<ThreadData> addresses;
	int stackNum = 0;

	for (auto it = threadId.begin(); it != threadId.end(); ++it)
	{
		HANDLE threadHandle = OpenThread(THREAD_GET_CONTEXT | THREAD_QUERY_INFORMATION, FALSE, *it);
		uint64_t threadStartAddress = GetThreadStartAddress(hProcHandle, threadHandle);

		ThreadData curr;
		curr.Address = threadStartAddress;
		curr.Position = stackNum;

		addresses.push_back(curr);

		stackNum++;
	}

	CloseHandle(hProcHandle);

	// Instead of using 'new', use 'malloc' to allocate memory for the array.
	ThreadData* resultArray = (ThreadData*)malloc(addresses.size() * sizeof(ThreadData));
	if (resultArray)
	{
		std::copy(addresses.begin(), addresses.end(), resultArray);
		*count = static_cast<int>(addresses.size());
	}
	else
	{
		*count = 0;
	}

	return resultArray;
}




std::vector<uint64_t> threadList(uint64_t pid) {
	/* solution from http://stackoverflow.com/questions/1206878/enumerating-threads-in-windows */
	std::vector<uint64_t> vect = std::vector<uint64_t>();
	HANDLE h = CreateToolhelp32Snapshot(TH32CS_SNAPTHREAD, 0);
	if (h == INVALID_HANDLE_VALUE)
		return vect;

	THREADENTRY32 te;
	te.dwSize = sizeof(te);
	if (Thread32First(h, &te)) {
		do {
			if (te.dwSize >= FIELD_OFFSET(THREADENTRY32, th32OwnerProcessID) +
				sizeof(te.th32OwnerProcessID)) {

				if (te.th32OwnerProcessID == pid) {
					vect.push_back(te.th32ThreadID);
				}
			}
			te.dwSize = sizeof(te);
		} while (Thread32Next(h, &te));
	}

	return vect;
}

uint64_t GetThreadStartAddress(HANDLE processHandle, HANDLE hThread) {
	/* rewritten from https://github.com/cheat-engine/cheat-engine/blob/master/Cheat%20Engine/CEFuncProc.pas#L3080 */
	uint64_t used = 0, ret = 0;
	uint64_t stacktop = 0, result = 0;

	MODULEINFO mi;

	GetModuleInformation(processHandle, GetModuleHandle("kernel32.dll"), &mi, sizeof(mi));
	stacktop = (uint64_t)GetThreadStackTopAddress_x86(processHandle, hThread);

	CloseHandle(hThread);

	if (stacktop) {
		//find the stack entry pointing to the function that calls "ExitXXXXXThread"
		//Fun thing to note: It's the first entry that points to a address in kernel32

		uint64_t* buf32 = new uint64_t[8192];

		if (ReadProcessMemory(processHandle, (LPCVOID)(stacktop - 8192), buf32, 8192, NULL)) {
			for (int i = 8192 / 8 - 1; i >= 0; --i) {
				if (buf32[i] >= (uint64_t)mi.lpBaseOfDll && buf32[i] <= (uint64_t)mi.lpBaseOfDll + mi.SizeOfImage) {
					result = stacktop - 8192 + i * 8;
					break;
				}
			}
		}

		delete buf32;
	}

	return result;
}