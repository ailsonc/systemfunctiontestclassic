//*********************************************************
//
// Copyright (c) Microsoft. All rights reserved.
// THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
// IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
// PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//*********************************************************
//
// Supporting radio management:
//   https://msdn.microsoft.com/en-us/library/windows/hardware/jj200337(v=vs.85).aspx
//
// Windows Radio Management Sample:
//   https://github.com/Microsoft/Windows-driver-samples/tree/master/network/radio/RadioManagerSample
//
// Note: For x64 platform, must be built by "x64" configuration.  
//       Build x86 version and execute on x64 platform, will cause CoCreateInstance() error.


#include <initguid.h>
#include <stdio.h>
#include <Windows.h>
#include <RadioMgr.h>

// Find *'s CLSID by using regedit.exe
// Found BthRadioMedia.dll: HKEY_CLASSES_ROOT\CLSID\{afd198ac-5f30-4e89-a789-5ddf60a69366}
DEFINE_GUID(CLSID_SampleRadioMedia, 0xafd198ac, 0x5f30, 0x4e89, 0xa7, 0x89, 0x5d, 0xdf, 0x60, 0xa6, 0x93, 0x66);

#define SAFE_RELEASE(punk) if ((punk) != NULL)   { (punk)->Release(); (punk) = NULL; }

#define EXIT_ON_ERROR(exp) if (hr != S_OK) {  printf("%s Error, hr = 0x%X \n", exp, hr);  goto Exit; }


void DisbleBTRadio() {
	IMediaRadioManager* pMediaRadioManager = NULL;
	IRadioInstanceCollection* pRadioInstanceCollection = NULL;
	IRadioInstance *pRadioInstance = NULL;

	HRESULT hrInit = NULL, hr = NULL;

	wprintf(L"DisbleRadioMgr() Begin! \n");

	hrInit = hr = CoInitializeEx(NULL, COINIT_MULTITHREADED);
	EXIT_ON_ERROR("CoInitializeEx()");

	hr = CoCreateInstance(CLSID_SampleRadioMedia, NULL, CLSCTX_ALL, IID_IMediaRadioManager, (void**)&pMediaRadioManager);
	EXIT_ON_ERROR("CoCreateInstance(IMediaRadioManager)");

	hr = pMediaRadioManager->GetRadioInstances(&pRadioInstanceCollection);
	EXIT_ON_ERROR("QueryInterface(IRadioInstanceCollection)");

	UINT32 count = 0;
	hr = pRadioInstanceCollection->GetCount(&count);
	EXIT_ON_ERROR("pRadioInstanceCollection->GetCount(&count)");

	wprintf(L"pRadioInstanceCollection count = %u\n", count);

	for (UINT32 i = 0; i < count; i++)
	{
		hr = pRadioInstanceCollection->GetAt(i, &pRadioInstance);
		EXIT_ON_ERROR("pRadioInstanceCollection->GetAt()");

		BSTR bstrName;
		hr = pRadioInstance->GetFriendlyName(1033, &bstrName);
		EXIT_ON_ERROR("pRadioInstance->GetFriendlyName()");
		wprintf(L"pRadioInstance name = [%s]\n", bstrName);

		WCHAR str[20];
		wsprintf(str, L"%s\0", bstrName);
		if (lstrcmp(str, L"Bluetooth") != 0)
		{
			continue;
		}

		DEVICE_RADIO_STATE state;
		hr = pRadioInstance->GetRadioState(&state);
		EXIT_ON_ERROR("pRadioInstance->GetRadioState()");

		wprintf(L"Current state = %s\n", (state == DRS_RADIO_ON) ?
			L"DRS_RADIO_ON" :
			L"DRS_RADIO_OFF");

		//Disable radio
		hr = pRadioInstance->SetRadioState(DRS_SW_RADIO_OFF, 3);
		EXIT_ON_ERROR("pRadioInstance->SetRadioState()");
		wprintf(L"SetRadioState: DRS_SW_RADIO_OFF\n");
		

		SAFE_RELEASE(pRadioInstance);
	}

Exit:

	SAFE_RELEASE(pMediaRadioManager);
	SAFE_RELEASE(pRadioInstanceCollection);
	SAFE_RELEASE(pRadioInstance);

	if (hrInit != NULL) CoUninitialize();

	wprintf(L"DisableBTRadio() End! \n");

	return;
}


bool EnableBTRadio()
{
	IMediaRadioManager* pMediaRadioManager = NULL;
	IRadioInstanceCollection* pRadioInstanceCollection = NULL;
	IRadioInstance *pRadioInstance = NULL;

	HRESULT hrInit = NULL, hr = NULL;
	bool result = false;

	wprintf(L"EnableRadioMgr() Begin! \n");

	hrInit = hr = CoInitializeEx(NULL, COINIT_MULTITHREADED);
	EXIT_ON_ERROR("CoInitializeEx()");

	hr = CoCreateInstance(CLSID_SampleRadioMedia, NULL, CLSCTX_ALL, IID_IMediaRadioManager, (void**)&pMediaRadioManager);
	EXIT_ON_ERROR("CoCreateInstance(IMediaRadioManager)");

	hr = pMediaRadioManager->GetRadioInstances(&pRadioInstanceCollection);
	EXIT_ON_ERROR("QueryInterface(IRadioInstanceCollection)");

	UINT32 count = 0;
	hr = pRadioInstanceCollection->GetCount(&count);
	EXIT_ON_ERROR("pRadioInstanceCollection->GetCount(&count)");

	wprintf(L"pRadioInstanceCollection count = %u\n", count);

	for (UINT32 i = 0; i < count; i++)
	{
		hr = pRadioInstanceCollection->GetAt(i, &pRadioInstance);
		EXIT_ON_ERROR("pRadioInstanceCollection->GetAt()");

		BSTR bstrName;
		hr = pRadioInstance->GetFriendlyName(1033, &bstrName);
		EXIT_ON_ERROR("pRadioInstance->GetFriendlyName()");
		wprintf(L"pRadioInstance name = [%s]\n", bstrName);

		WCHAR str[20];
		wsprintf(str, L"%s\0", bstrName);
		if (lstrcmp(str, L"Bluetooth") != 0)
		{
			continue;
		}

		DEVICE_RADIO_STATE state;
		hr = pRadioInstance->GetRadioState(&state);
		EXIT_ON_ERROR("pRadioInstance->GetRadioState()");

		wprintf(L"Current state = %s\n", (state == DRS_RADIO_ON) ?
			L"DRS_RADIO_ON" :
			L"DRS_SW_RADIO_OFF");


		if (state != DRS_RADIO_ON)
		{
			hr = pRadioInstance->SetRadioState(DRS_RADIO_ON, 3);
			EXIT_ON_ERROR("pRadioInstance->SetRadioState()");
			wprintf(L"SetRadioState: ON");
			//enabled radio
			result = true;
		}
		else {
			//radio already ON, do nothing
		}

		SAFE_RELEASE(pRadioInstance);
		return result;
	}

Exit:

	SAFE_RELEASE(pMediaRadioManager);
	SAFE_RELEASE(pRadioInstanceCollection);
	SAFE_RELEASE(pRadioInstance);

	if (hrInit != NULL) CoUninitialize();

	wprintf(L"EnableBTRadio() End! \n");

	return result;
}

int __cdecl main(int argc, char* argv[])
{
	char* temp = NULL;
	int result = 0;
	if (argc > 1) {
		if (strcmp(argv[1], "on") == 0) {
			//EnableBTRadio will only return true if radio is turned on by program
			//if radio was already turn on initially, EnableBTRadio will return false
			if (EnableBTRadio()) result = 1;
		}
		else if (strcmp(argv[1], "off") == 0) {
			DisbleBTRadio();
		}
	}

	return result;
}