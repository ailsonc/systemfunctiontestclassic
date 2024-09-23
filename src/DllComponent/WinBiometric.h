#pragma once

#include <windows.h>
#include <stdio.h>
#include <conio.h>
#include <winbio.h>

#pragma comment(lib, "WinBio.lib")
//------------------------------------------------------------------------
// Forward declarations.
//
SIZE_T WinBioEnum();
WINBIO_SESSION_HANDLE WinBioOpen();
HRESULT WinBioLocate(WINBIO_SESSION_HANDLE sessionHandle, WINBIO_BIOMETRIC_SUBTYPE subFactor);

HRESULT TestWinBiometric(WINBIO_SESSION_HANDLE sessionHandle);
HRESULT DiscardEnrollment(WINBIO_SESSION_HANDLE sessionHandle);
