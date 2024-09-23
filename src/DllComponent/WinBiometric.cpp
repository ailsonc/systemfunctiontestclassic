#include "WinBiometric.h"

SIZE_T WinBioEnum()
{
	HRESULT hr = S_OK;
	WINBIO_UNIT_ID unitId = 0;
	PWINBIO_UNIT_SCHEMA unitSchema = NULL;
	SIZE_T unitCount = 0;
	hr = WinBioEnumBiometricUnits(
		WINBIO_TYPE_FINGERPRINT,        // Type of biometric unit
		&unitSchema,                    // Array of unit schemas
		&unitCount);                   // Count of unit schemas

	if (FAILED(hr))
		unitCount = 0;
	return unitCount;
}

WINBIO_SESSION_HANDLE WinBioOpen()
{
	HRESULT hr = S_OK;
	WINBIO_SESSION_HANDLE sessionHandle = NULL;
	WINBIO_UNIT_ID unitId = 0;

	// Connect to the system pool. 
	hr = WinBioOpenSession(
		WINBIO_TYPE_FINGERPRINT,    // Service provider
		WINBIO_POOL_SYSTEM,         // Pool type
		WINBIO_FLAG_DEFAULT,        // Configuration and access
		NULL,                       // Array of biometric unit IDs
		0,                          // Count of biometric unit IDs
		NULL,                       // Database ID
		&sessionHandle              // [out] Session handle
	);

	if (FAILED(hr))
	{
		sessionHandle = NULL;
		return sessionHandle;
	}

	return sessionHandle;
}

HRESULT WinBioLocate(WINBIO_SESSION_HANDLE sessionHandle, WINBIO_BIOMETRIC_SUBTYPE subFactor)
{

	HRESULT hr = S_OK;
	WINBIO_UNIT_ID unitId = 0;
	// Locate a sensor.
	//wprintf_s(L"\n Swipe your finger on the sensor...\n");
	hr = WinBioLocateSensor(sessionHandle, &unitId);
	if (FAILED(hr))
	{
		return hr;
	}

	// Begin the enrollment sequence. 
	hr = WinBioEnrollBegin(
		sessionHandle,      // Handle to open biometric session
		subFactor,          // Finger to create template for
		unitId              // Biometric unit ID
	);


	return hr;
}

HRESULT DiscardEnrollment(WINBIO_SESSION_HANDLE sessionHandle)
{
	HRESULT hr = S_OK;
	/*
	FILE *stream;
	char msg[4096] = "";
	wchar_t strTemp[4096] = L"";
	errno_t err;

	// open file
	err = fopen_s(&stream, "BioMsg.txt", "a+");
	fwrite("DiscardEnrollment\n", strlen("DiscardEnrollment\n"), 1, stream);
	// close file
	int numclosed = _fcloseall();
	*/

	// Discard the enrollment if the appropriate flag is set.
	// Commit the enrollment if it is not discarded.
	hr = WinBioCloseSession(sessionHandle);

	//hr = WinBioEnrollDiscard(sessionHandle);

	return hr;
}

/*
#define WINBIO_FP_TOO_HIGH          ((WINBIO_REJECT_DETAIL)1)
#define WINBIO_FP_TOO_LOW           ((WINBIO_REJECT_DETAIL)2)
#define WINBIO_FP_TOO_LEFT          ((WINBIO_REJECT_DETAIL)3)
#define WINBIO_FP_TOO_RIGHT         ((WINBIO_REJECT_DETAIL)4)
#define WINBIO_FP_TOO_FAST          ((WINBIO_REJECT_DETAIL)5)
#define WINBIO_FP_TOO_SLOW          ((WINBIO_REJECT_DETAIL)6)
#define WINBIO_FP_POOR_QUALITY      ((WINBIO_REJECT_DETAIL)7)
#define WINBIO_FP_TOO_SKEWED        ((WINBIO_REJECT_DETAIL)8)
#define WINBIO_FP_TOO_SHORT         ((WINBIO_REJECT_DETAIL)9)
#define WINBIO_FP_MERGE_FAILURE     ((WINBIO_REJECT_DETAIL)10)
*/
HRESULT TestWinBiometric(WINBIO_SESSION_HANDLE sessionHandle)
{
	HRESULT hr = S_OK;
	WINBIO_REJECT_DETAIL rejectDetail = 0;

	// Capture enrollment information by swiping the sensor with
	// the finger identified by the subFactor argument in the 
	// WinBioEnrollBegin function.


	hr = WinBioEnrollCapture(
		sessionHandle,  // Handle to open biometric session
		&rejectDetail   // [out] Failure information
	);


	if (hr == WINBIO_I_MORE_DATA)
	{
		;// wprintf_s(L"\n    More data required.\n");
	}
	if (FAILED(hr))
	{
		if (hr == WINBIO_E_BAD_CAPTURE)
		{
			hr = rejectDetail;
			//wprintf_s(L"\n  Error: Bad capture; reason: %d",rejectDetail); // if rejectDetail = 10 , fail
		}
		else
		{
			;// wprintf_s(L"\n WinBioEnrollCapture failed. hr = 0x%x", hr);
		}
	}
	else
	{
		;// wprintf_s(L"\n    Template completed.\n");
	}

	return hr;
}