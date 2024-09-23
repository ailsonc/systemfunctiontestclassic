//*********************************************************
//
// Copyright (c) Microsoft. All rights reserved.
// THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
// IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
// PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//*********************************************************

#include "CoreComponent.h"

using namespace DllComponent;

#define MAX_SIZE 4096 //2048
#define BUFFER_SIZE 64
/// <summary>
/// Enum biometric.
/// </summary>
SIZE_T CoreComponent::BioEnum()
{
	SIZE_T unitCount = 0;
	unitCount = WinBioEnum();
	return unitCount;
}

/// <summary>
/// Test biometric.
/// </summary>
WINBIO_SESSION_HANDLE  CoreComponent::BioOpen()
{
	WINBIO_SESSION_HANDLE sessionHandle = NULL;
	sessionHandle = WinBioOpen();
	return sessionHandle;
}

HRESULT  CoreComponent::BioLocate(WINBIO_SESSION_HANDLE sessionHandle)
{
	HRESULT hr = S_OK;
	hr = WinBioLocate(sessionHandle, WINBIO_ANSI_381_POS_RH_INDEX_FINGER);
	return hr;
}

HRESULT CoreComponent::DiscardEnroll(WINBIO_SESSION_HANDLE sessionHandle)
{
	HRESULT hr = S_OK;

	hr = DiscardEnrollment(sessionHandle);

	return hr;
}

HRESULT CoreComponent::TestBiometric(WINBIO_SESSION_HANDLE sessionHandle)
{
	HRESULT hr = S_OK;

	hr = TestWinBiometric(sessionHandle);

	return hr;
}
/// <summary>
/// Test screen brightness.
/// </summary>
/// <returns>Return execution result.</returns>
HRESULT  CoreComponent::TestBrightness()
{
	//HRESULT  status = S_OK;
	DWORD status = ERROR_SUCCESS;
	LPGUID pScheme;
	static ULONG ulType = REG_DWORD;
	DWORD autoAcBrightness = UNKNOWN_STATE, autoDcBrightness = UNKNOWN_STATE;
	DWORD curAcBrightness = UNKNOWN_PERCENTAGE, curDcBrightness = UNKNOWN_PERCENTAGE;
	DWORD dwSize = sizeof(DWORD);

	status = PowerGetActiveScheme(NULL, &pScheme);
	if (status != ERROR_SUCCESS)
	{
		//wprintf(L"PowerGetActiveScheme() Error = %u\n", status);
		goto Exit;
	}

	// Get the current auto brightness state and brightness percentage
	status = PowerReadACValue(NULL, pScheme, &GUID_VIDEO_SUBGROUP, &GUID_VIDEO_ADAPTIVE_DISPLAY_BRIGHTNESS, &ulType, (LPBYTE)(&autoAcBrightness), &dwSize);
	if (status != ERROR_SUCCESS)
	{
		//wprintf(L"PowerReadACValue() Error = %u\n", status);
		goto Exit;
	}
	status = PowerReadACValue(NULL, pScheme, &GUID_VIDEO_SUBGROUP, &GUID_DEVICE_POWER_POLICY_VIDEO_BRIGHTNESS, &ulType, (LPBYTE)(&curAcBrightness), &dwSize);
	if (status != ERROR_SUCCESS)
	{
		//wprintf(L"PowerReadACValue() Error = %u\n", status);
		goto Exit;
	}

	status = PowerReadDCValue(NULL, pScheme, &GUID_VIDEO_SUBGROUP, &GUID_VIDEO_ADAPTIVE_DISPLAY_BRIGHTNESS, &ulType, (LPBYTE)(&autoDcBrightness), &dwSize);
	if (status != ERROR_SUCCESS)
	{
		//wprintf(L"PowerReadDCValue() Error = %u\n", status);
		goto Exit;
	}

	status = PowerReadDCValue(NULL, pScheme, &GUID_VIDEO_SUBGROUP, &GUID_DEVICE_POWER_POLICY_VIDEO_BRIGHTNESS, &ulType, (LPBYTE)(&curDcBrightness), &dwSize);
	if (status != ERROR_SUCCESS)
	{
		//wprintf(L"PowerReadDCValue() Error = %u\n", status);
		goto Exit;
	}

	/*wprintf(L"AutoAC = %u, AutoDC = %u, curACBrightness = %u, curDCBrightness = %u\n",
		autoAcBrightness, autoDcBrightness, curAcBrightness, curDcBrightness);*/

	// Disable adjust screen brightness automatically
	status = PowerWriteACValueIndex(NULL, pScheme, &GUID_VIDEO_SUBGROUP, &GUID_VIDEO_ADAPTIVE_DISPLAY_BRIGHTNESS, 0);
	if (status != ERROR_SUCCESS)
	{
		//wprintf(L"PowerWriteACValueIndex() Error = %u\n", status);
		goto Exit;
	}
	status = PowerWriteDCValueIndex(NULL, pScheme, &GUID_VIDEO_SUBGROUP, &GUID_VIDEO_ADAPTIVE_DISPLAY_BRIGHTNESS, 0);
	if (status != ERROR_SUCCESS)
	{
		//wprintf(L"PowerWriteDCValueIndex() Error = %u\n", status);
		goto Exit;
	}

	status = PowerSetActiveScheme(NULL, pScheme);
	if (status != ERROR_SUCCESS)
	{
		//wprintf(L"PowerWriteDCValueIndex() Error = %u\n", status);
		goto Exit;
	}

	for (DWORD i = 0; i <= 100; i += 20)
	{
		status = PowerWriteACValueIndex(NULL, pScheme, &GUID_VIDEO_SUBGROUP, &GUID_DEVICE_POWER_POLICY_VIDEO_BRIGHTNESS, i);
		if (status != ERROR_SUCCESS)
		{
			//wprintf(L"PowerWriteACValueIndex() Error = %u\n", status);
			goto Exit;
		}
		status = PowerWriteDCValueIndex(NULL, pScheme, &GUID_VIDEO_SUBGROUP, &GUID_DEVICE_POWER_POLICY_VIDEO_BRIGHTNESS, i);
		if (status != ERROR_SUCCESS)
		{
			//wprintf(L"PowerWriteDCValueIndex() Error = %u\n", status);
			goto Exit;
		}

		//wprintf(L"Brightness = %u\n", i);
		status = PowerSetActiveScheme(NULL, pScheme);
		if (status != ERROR_SUCCESS) goto Exit;

		Sleep(MAX_WAIT_TIME);
	}

Exit:

	// Restore back to the current auto brightness state and brightness percentage
	if (autoAcBrightness != UNKNOWN_STATE)
	{
		status = PowerWriteACValueIndex(NULL, pScheme, &GUID_VIDEO_SUBGROUP, &GUID_VIDEO_ADAPTIVE_DISPLAY_BRIGHTNESS, autoAcBrightness);
		if (status != ERROR_SUCCESS)
		{
			//wprintf(L"PowerWriteACValueIndex() Error = %u\n", status);
			goto Exit;
		}
	}

	if (autoDcBrightness != UNKNOWN_STATE)
	{
		status = PowerWriteDCValueIndex(NULL, pScheme, &GUID_VIDEO_SUBGROUP, &GUID_VIDEO_ADAPTIVE_DISPLAY_BRIGHTNESS, autoDcBrightness);
		if (status != ERROR_SUCCESS)
		{
			//wprintf(L"PowerWriteDCValueIndex() Error = %u\n", status);
			goto Exit;
		}
	}

	if (curAcBrightness != UNKNOWN_PERCENTAGE)
	{
		status = PowerWriteACValueIndex(NULL, pScheme, &GUID_VIDEO_SUBGROUP, &GUID_DEVICE_POWER_POLICY_VIDEO_BRIGHTNESS, curAcBrightness);
		if (status != ERROR_SUCCESS)
		{
			//wprintf(L"PowerWriteACValueIndex() Error = %u\n", status);
			goto Exit;
		}
	}

	if (curDcBrightness != UNKNOWN_PERCENTAGE)
	{
		status = PowerWriteDCValueIndex(NULL, pScheme, &GUID_VIDEO_SUBGROUP, &GUID_DEVICE_POWER_POLICY_VIDEO_BRIGHTNESS, curDcBrightness);
		if (status != ERROR_SUCCESS)
		{
			//wprintf(L"PowerWriteACValueIndex() Error = %u\n", status);
			goto Exit;
		}
	}

	status = PowerSetActiveScheme(NULL, pScheme);
	if (status != ERROR_SUCCESS)
	{
		//wprintf(L"PowerWriteDCValueIndex() Error = %u\n", status);
		goto Exit;
	}

	// Get the current auto brightness state and brightness percentage
	status = PowerReadACValue(NULL, pScheme, &GUID_VIDEO_SUBGROUP, &GUID_VIDEO_ADAPTIVE_DISPLAY_BRIGHTNESS, &ulType, (LPBYTE)(&autoAcBrightness), &dwSize);
	if (status != ERROR_SUCCESS)
	{
		//wprintf(L"PowerReadACValue() Error = %u\n", status);
		goto Exit;
	}

	status = PowerReadDCValue(NULL, pScheme, &GUID_VIDEO_SUBGROUP, &GUID_VIDEO_ADAPTIVE_DISPLAY_BRIGHTNESS, &ulType, (LPBYTE)(&autoDcBrightness), &dwSize);
	if (status != ERROR_SUCCESS)
	{
		//wprintf(L"PowerReadDCValue() Error = %u\n", status);
		goto Exit;
	}

	status = PowerReadACValue(NULL, pScheme, &GUID_VIDEO_SUBGROUP, &GUID_DEVICE_POWER_POLICY_VIDEO_BRIGHTNESS, &ulType, (LPBYTE)(&curAcBrightness), &dwSize);
	if (status != ERROR_SUCCESS)
	{
		//wprintf(L"PowerReadACValue() Error = %u\n", status);
		goto Exit;
	}

	status = PowerReadDCValue(NULL, pScheme, &GUID_VIDEO_SUBGROUP, &GUID_DEVICE_POWER_POLICY_VIDEO_BRIGHTNESS, &ulType, (LPBYTE)(&curDcBrightness), &dwSize);
	if (status != ERROR_SUCCESS)
	{
		//wprintf(L"PowerReadDCValue() Error = %u\n", status);
		goto Exit;
	}

	/*wprintf(L"AutoAC = %u, AutoDC = %u, curACBrightness = %u, curDCBrightness = %u\n",
		autoAcBrightness, autoDcBrightness, curAcBrightness, curDcBrightness);*/

	return status;
}

/// <summary>
/// Test WiFi for searching nearby AP.
/// </summary>
/// <returns>Return searched AP list with String.</returns>
String^ CoreComponent::TestWiFi(String^ TargetSSID, int iIfConnection)
{
	int dwRetVal = 0;
	WIFIINFO strWIFIINFO[32];
	int iOut = 0;

	wchar_t str[MAX_SIZE] = L"";
	wchar_t strTemp[MAX_SIZE] = L"";
	char cstr[BUFFER_SIZE] = "";

	const wchar_t* wchars = (const wchar_t*)(Marshal::StringToHGlobalUni(TargetSSID)).ToPointer();
	dwRetVal = WiFiQuery(strWIFIINFO, 32, &iOut, (WCHAR*)wchars, iIfConnection);
	for (int i = 0; i < iOut; i++)
	{
		if (wcslen(strWIFIINFO[i].wsSSID) == 0)
		{
			swprintf_s(strTemp, MAX_STRING_LENGTH, L"NULL ,%d, %s\n", strWIFIINFO[i].iSignaldegree, strWIFIINFO[i].wsPhyTypes);  // strWIFIINFO[i].iIfSecurity for security
		}
		else
		{
			//force to assign value from wchar to char 
			for (int k = 0; k < BUFFER_SIZE; k++)
				cstr[k] = (char)strWIFIINFO[i].wsSSID[k];

			wchar_t* wszString = new wchar_t[BUFFER_SIZE];
			//convert utf-8 to unicode
			MultiByteToWideChar(CP_UTF8, NULL, cstr, BUFFER_SIZE, wszString, BUFFER_SIZE);

			swprintf_s(strTemp, MAX_STRING_LENGTH, L"%s ,%d, %s\n", wszString, strWIFIINFO[i].iSignaldegree, strWIFIINFO[i].wsPhyTypes); // strWIFIINFO[i].iIfSecurity for security
		}
		wcscat(str, strTemp);
	}

	if (dwRetVal == WiFi_Conn_FAIL)
		wcscat(str, L"WiFi_Connect_Fail ,0\n");

		

	return gcnew String(str);
}
/// <summary>
/// Test WiFi for searching nearby AP.
/// </summary>
/// <param name="pWIFIINFO">the struct of SSID,Signaldegree and Security status.< / param>
/// <param name="infoSize">the maximum size of pWIFIINFO.< / param>
/// <param name="outInfoLen">the number of pWIFIINFO.< / param>
/// <returns>Return execution result.</returns>
int CoreComponent::WiFiQuery(WIFIINFO *pWIFIINFO, int infoSize, int *outInfoLen, WCHAR *TargetSSID, int iIfConnection)
{
	// variables used for WlanEnumInterfaces
	PWLAN_INTERFACE_INFO_LIST pIfList = NULL;
	PWLAN_INTERFACE_INFO pIfInfo = NULL;
	PWLAN_AVAILABLE_NETWORK_LIST pBssList = NULL;
	PWLAN_AVAILABLE_NETWORK pBssEntry = NULL;

	HRESULT hr = S_OK;
	HANDLE hClient = NULL;
	DWORD dwMaxClient = WLAN_API_VERSION_2_0;
	DWORD dwCurVersion = 0;
	DWORD dwResult = 0;
	DWORD dwRetVal = 0;
	DWORD dwDataSize;

	int iRet = 0;
	unsigned int i, j, k;
	int m = 0;

	int selectedIndex = -1;
	PWLAN_AVAILABLE_NETWORK selectedBssEntry = NULL;
	PWLAN_AVAILABLE_NETWORK curConnectedBssEntry = NULL;
	WCHAR selectedSSID[64] = { 0 };
	WCHAR curConnectedSSID[64] = { 0 };
	LPWSTR pProfileXml = NULL;

	WCHAR ssidName[WLAN_MAX_NAME_LENGTH + 1];
	WCHAR phyTypes[WLAN_MAX_NAME_LENGTH + 1];
	WCHAR str[MAX_STRING_LENGTH] = { 0 };


	dwResult = WlanOpenHandle(dwMaxClient, NULL, &dwCurVersion, &hClient);
	if (dwResult != ERROR_SUCCESS) {
		hr = HRESULT_FROM_WIN32(dwResult);
		dwRetVal = 1;
		goto Exit;
	}

	dwResult = WlanEnumInterfaces(hClient, NULL, &pIfList);
	if (dwResult != ERROR_SUCCESS) {
		hr = HRESULT_FROM_WIN32(dwResult);
		dwRetVal = 1;
		goto Exit;
	}


	PVOID pData;
	WLAN_RADIO_STATE wlanRadioState;
	DOT11_RADIO_STATE initialState = dot11_radio_state_on; //set default state to ON
	//Query interface to get initial radio state
	dwResult = WlanQueryInterface(
		hClient,
		&pIfList->InterfaceInfo[0].InterfaceGuid,
		wlan_intf_opcode_radio_state,
		NULL,                       // reserved   
		&dwDataSize,
		&pData,
		NULL);
	if (dwResult != ERROR_SUCCESS) {
		hr = HRESULT_FROM_WIN32(dwResult);
		dwRetVal = 1;
		goto Exit;
	}
	wlanRadioState = *((PWLAN_RADIO_STATE)pData);
	for (i = 0; i < wlanRadioState.dwNumberOfPhys; i++)
	{
		initialState = wlanRadioState.PhyRadioState[i].dot11SoftwareRadioState;
	}

	WLAN_PHY_RADIO_STATE state;
	state.dwPhyIndex = 0;
	state.dot11SoftwareRadioState = dot11_radio_state_on;
	pData = &state;
	//Wifi radio state -> ON
	dwResult = WlanSetInterface(hClient, &pIfList->InterfaceInfo[0].InterfaceGuid,
		wlan_intf_opcode_radio_state, sizeof(WLAN_PHY_RADIO_STATE), pData, NULL);
	if (dwResult != ERROR_SUCCESS) {
		hr = HRESULT_FROM_WIN32(dwResult);
		dwRetVal = 1;
		goto Exit;
	}

	//Find all wifi networks
	for (i = 0; i < (int)pIfList->dwNumberOfItems; i++)
	{
		pIfInfo = (WLAN_INTERFACE_INFO *)&pIfList->InterfaceInfo[i];

		dwResult = WlanGetAvailableNetworkList(hClient,
			&pIfInfo->InterfaceGuid,
			WLAN_AVAILABLE_NETWORK_INCLUDE_ALL_MANUAL_HIDDEN_PROFILES,
			NULL,
			&pBssList);
		if (dwResult != ERROR_SUCCESS) {
			hr = HRESULT_FROM_WIN32(dwResult);
			dwRetVal = 1;
			goto Exit;
		}

		for (j = 0; j < pBssList->dwNumberOfItems; j++)
		{
			pBssEntry = (WLAN_AVAILABLE_NETWORK *)&pBssList->Network[j];

			// Remove duplicated network name (the SSID with profile)
			if (pBssEntry->dwFlags == 0x2) continue;

			// Skip null string or hidden SSID
			if (pBssEntry->dot11Ssid.uSSIDLength != 0)
			{
				ssidName[0] = L'\0';
				for (k = 0; k < pBssEntry->dot11Ssid.uSSIDLength; k++)
				{
					swprintf_s(ssidName, WLAN_MAX_NAME_LENGTH, L"%s%c", ssidName, pBssEntry->dot11Ssid.ucSSID[k]);
				}
				ssidName[WLAN_MAX_NAME_LENGTH] = L'\0';

				phyTypes[0] = L'\0';
				for (DWORD l = 0; l < sizeof(pBssEntry->dot11PhyTypes) / sizeof(pBssEntry->dot11PhyTypes[0]); l++)
				{
					swprintf_s(phyTypes, WLAN_MAX_NAME_LENGTH, L"%s%d", phyTypes, pBssEntry->dot11PhyTypes[l]);
				}
				phyTypes[WLAN_MAX_NAME_LENGTH] = L'\0';


				swprintf_s(pWIFIINFO[m].wsSSID, 64, L"%s", ssidName);
				swprintf_s(pWIFIINFO[m].wsPhyTypes, 64, L"%s", phyTypes);

				pWIFIINFO[m].iSignaldegree = (int)pBssEntry->wlanSignalQuality;
				pWIFIINFO[m].iIfSecurity = (int)pBssEntry->bSecurityEnabled;
				//pWIFIINFO[m].wsPhyTypes = pBssEntry->dot11PhyTypes;

				if (m >= infoSize) goto Exit;
				m++;

				if (pBssEntry->dwFlags) {
					if (pBssEntry->dwFlags & WLAN_AVAILABLE_NETWORK_CONNECTED)
					{
						//Currently connected
						curConnectedBssEntry = pBssEntry;
						swprintf(&curConnectedSSID[0], 64, L"%s", &ssidName[0]);
					}
				}
				if (TargetSSID != NULL)
				{
					if (selectedIndex == -1 && wcscmp(TargetSSID, ssidName) == 0)
					{
						selectedIndex = j;
						selectedBssEntry = pBssEntry;
						swprintf(&selectedSSID[0], 64, L"%s", &ssidName[0]);
					}
				}

			}

		}// end of WlanGetAvailableNetworkList

		if (iIfConnection == 1)
		{
			if (TargetSSID != NULL)
			{
				if (selectedIndex < 0) // SSID not found!
				{
					dwRetVal = WiFi_Conn_FAIL;
					goto Exit;
				}
				else
				{
					// disconnect current connection first
					if (curConnectedBssEntry != NULL)
					{
						dwResult = WlanDisconnect(hClient,
							&pIfInfo->InterfaceGuid,
							0);

						if (dwResult != ERROR_SUCCESS) { //WlanDisconnect failed with error
							dwRetVal = WiFi_Conn_FAIL;
							goto Exit;
						}
					}
					// go connect
					wprintf(L"Go connect (%s)...\n", selectedSSID);

					WLAN_CONNECTION_PARAMETERS connectionData;
					connectionData.wlanConnectionMode = wlan_connection_mode_discovery_unsecure;
					connectionData.strProfile = NULL;
					connectionData.pDot11Ssid = &(selectedBssEntry->dot11Ssid);
					connectionData.pDesiredBssidList = NULL;
					connectionData.dot11BssType = selectedBssEntry->dot11BssType;
					connectionData.dwFlags = 0;

					if (selectedBssEntry->dwFlags & WLAN_AVAILABLE_NETWORK_HAS_PROFILE)
					{
						DWORD dwFlags = 0;
						DWORD dwGrantedAccess = 0;

						dwResult = WlanGetProfile(hClient,
							&pIfInfo->InterfaceGuid,
							selectedBssEntry->strProfileName,
							NULL,
							&pProfileXml,
							&dwFlags,
							&dwGrantedAccess);

						if (dwResult != ERROR_SUCCESS) {
							wprintf(L"WlanGetProfile failed with error: %u\n", dwResult);
							dwRetVal = WiFi_Conn_FAIL;
							goto Exit;
						}
						else {

							connectionData.strProfile = pProfileXml;
						}
					}
					dwResult = WlanConnect(hClient,
						&pIfInfo->InterfaceGuid,
						&connectionData,
						0);

					if (dwResult != ERROR_SUCCESS) {
						wprintf(L"WlanConnect (%s) failed with error: 0x%08x\n", selectedSSID, dwResult);
						dwRetVal = WiFi_Conn_FAIL;
					}

				}

			}
		}

		//Wifi radio state -> Initial state
		WLAN_PHY_RADIO_STATE state2;
		state2.dwPhyIndex = 0;
		state2.dot11SoftwareRadioState = initialState;
		pData = &state2;

		dwResult = WlanSetInterface(hClient, &pIfList->InterfaceInfo[0].InterfaceGuid,
			wlan_intf_opcode_radio_state, sizeof(WLAN_PHY_RADIO_STATE), pData, NULL);
		if (dwResult != ERROR_SUCCESS) {
			hr = HRESULT_FROM_WIN32(dwResult);
			dwRetVal = 1;
			goto Exit;
		}
	}

Exit:
	if (hClient != INVALID_HANDLE_VALUE)
	{
		WlanCloseHandle(hClient, 0);
		hClient = INVALID_HANDLE_VALUE;
	}
	if (pBssList != NULL) {
		WlanFreeMemory(pBssList);
		pBssList = NULL;
	}

	if (pIfList != NULL) {
		WlanFreeMemory(pIfList);
		pIfList = NULL;
	}
	if (pProfileXml != NULL) {
		WlanFreeMemory(pProfileXml);
		pProfileXml = NULL;
	}
	*outInfoLen = m;

	return dwRetVal;
}
/// <summary>
/// Test BT for searching nearby BT devices.
/// </summary>
/// <param name="iSec">the BT scan delay time and it can be configure on the SFTConfig.xml.< / param>
String^ CoreComponent::TestBT(int iSec)
{
	UINT8 nRetry;
	HRESULT  hr = S_OK;
	static int iTimeoutMultiplier = (int)ceil(iSec / 1.28);
	if (iTimeoutMultiplier > 48)
		iTimeoutMultiplier = 5;
	
	BLUETOOTH_DEVICE_SEARCH_PARAMS parameters = { sizeof(BLUETOOTH_DEVICE_SEARCH_PARAMS),
		1, 0, 1, 1, 1, iTimeoutMultiplier, NULL };
	BLUETOOTH_DEVICE_INFO deviceInfo = { 0 };
	HBLUETOOTH_DEVICE_FIND hFind = NULL;
	HRESULT hrInit = E_FAIL;
	WCHAR str[MAX_STRING_LENGTH];
	bool bBTDiscover = false;
	str[0] = L'\0';

	// Grab a handle to the first device
	deviceInfo.dwSize = sizeof(BLUETOOTH_DEVICE_INFO);
	for (nRetry = 0; nRetry < 2 && hFind == NULL; nRetry++)
	{
		hFind = BluetoothFindFirstDevice(&parameters, &deviceInfo);
	}

	if (NULL == hFind)
	{
		//hr = HRESULT_FROM_WIN32(GetLastError());
		goto Exit;
	}

	int i = 1;
	if (deviceInfo.szName[0] == L'\0')
	{
		swprintf_s(str, MAX_STRING_LENGTH, L"%2d. %s\n", i++, L"(Unknown)");
		wprintf(L"\n(Unknown)");
	}

	else
	{
		swprintf_s(str, MAX_STRING_LENGTH, L"%2d. %s\n", i++, deviceInfo.szName);
		wprintf(L"\n");
		wprintf(deviceInfo.szName);
		bBTDiscover = true;

	}


	while (BluetoothFindNextDevice(hFind, &deviceInfo))
	{
		if (deviceInfo.szName[0] == L'\0')
		{
			swprintf_s(str, MAX_STRING_LENGTH, L"%s%2d. %s\n", str, i++, L"(Unknown)");
			wprintf(L"\n(Unknown)");
		}
		else
		{
			swprintf_s(str, MAX_STRING_LENGTH, L"%s%2d. %s\n", str, i++, deviceInfo.szName);
			wprintf(L"\n");
			wprintf(deviceInfo.szName);
			bBTDiscover = true;

		}

	}

	DWORD dwError = GetLastError();
	if (dwError != ERROR_NO_MORE_ITEMS && dwError != ERROR_SUCCESS)
	{
		hr = HRESULT_FROM_WIN32(dwError);
		goto Exit;
	}

	//swprintf_s(lpBluetoothList, MAX_STRING_LENGTH, L"%s\0", str);

Exit:

	// Close deviceHandle so nothing is leaked
	if (NULL != hFind)  BluetoothFindDeviceClose(hFind);

	if (hrInit == S_OK)  CoUninitialize();

	return gcnew String(str);

}

/// <summary>
/// Returns friendly name of available camera devices.
/// </summary>
/// <returns>Return execution result.</returns>
String^  CoreComponent::GetCameraDevice()
{
	WCHAR str[MAX_STRING_LENGTH] = { 0 };

	IMFActivate **ppDevices = NULL;
	UINT32      count = 0;

	IMFAttributes *pAttributes = NULL;

	HRESULT hr = MFCreateAttributes(&pAttributes, 1);
	if (FAILED(hr))
	{
		wprintf(L" MFCreateAttributes() Error = 0x%X \n", hr);
		return "";
	}
	
	// Ask for source type = video capture devices
	hr = pAttributes->SetGUID(MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE,
		MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_VIDCAP_GUID);
	if (FAILED(hr))
	{
		wprintf(L" SetGUID() Error = 0x%X \n", hr);
		return "";
	}

	// Enumerate devices.
	hr = MFEnumDeviceSources(pAttributes, &ppDevices, &count);
	if (FAILED(hr))
	{
		wprintf(L" MFEnumDeviceSources() Error = 0x%X \n", hr);
		return "";
	}

	String ^outStr = gcnew String("");

	for (DWORD k = 0; k < count; k++)
	{

		wchar_t *szFriendlyName = NULL;
		UINT32 cchName;
		HRESULT hr = S_OK;

		hr = ppDevices[k]->GetAllocatedString(MF_DEVSOURCE_ATTRIBUTE_FRIENDLY_NAME,
			&szFriendlyName, &cchName);
		OutputDebugStringW(szFriendlyName);
		int size = sizeof(szFriendlyName[0]);

		if (FAILED(hr))
		{
			break;
		}

		String ^tmpStr = gcnew String(szFriendlyName);
		if (outStr->Length > 0) {
			outStr = outStr + "," + tmpStr;
		}
		else {
			outStr = outStr + tmpStr;
		}
		
		CoTaskMemFree(szFriendlyName);
	}
	return outStr;
}



HRESULT SetTouchDisableProperty(HWND hwnd, BOOL fDisableTouch)
{
	IPropertyStore* pPropStore;
	HRESULT hrReturnValue = SHGetPropertyStoreForWindow(hwnd, IID_PPV_ARGS(&pPropStore));
	if (SUCCEEDED(hrReturnValue))
	{
		PROPVARIANT var;
		var.vt = VT_BOOL;
		var.boolVal = fDisableTouch ? VARIANT_TRUE : VARIANT_FALSE;
		hrReturnValue = pPropStore->SetValue(PKEY_EdgeGesture_DisableTouchWhenFullscreen, var);
		pPropStore->Release();
	}
	return hrReturnValue;
}

/// <summary>
/// Disable edge UI when in full screen mode
/// </summary>
/// <param name="hWnd">the pointer of a window handle< / param>
int CoreComponent::DisableTouch(IntPtr hWnd) 
{
	HWND nativeHWND = (HWND)hWnd.ToPointer();
	SetTouchDisableProperty(nativeHWND, true);
	return 0;
}


/// <summary>
/// Query battery information.
/// </summary>
/// <returns>Return execution result.</returns>
BatteryInfo CoreComponent::QueryBatteryInfo()
{
	BatteryInfo info = { S_OK, 0, 0, 0};
	HRESULT  hr = S_OK;
	HRESULT  hr_found = S_FALSE;
	SP_DEVICE_INTERFACE_DATA did = { 0 };
	DWORD cbRequired = 0;
	PSP_DEVICE_INTERFACE_DETAIL_DATA pdidd = nullptr;
	HANDLE hBattery = NULL;
	BATTERY_INFORMATION batteryInfo = { 0 };
	BATTERY_QUERY_INFORMATION batteryQueryInfo = { 0 };
	BATTERY_WAIT_STATUS batteryWaitStatus = { 0 };
	BATTERY_STATUS batteryStatus;
	//SYSTEM_BATTERY_STATE batteryState;
	DWORD dwWait = 0;
	DWORD dwOut = 0;

	HDEVINFO hdev =
		SetupDiGetClassDevs(&GUID_DEVCLASS_BATTERY,
		0,
		0,
		DIGCF_PRESENT | DIGCF_DEVICEINTERFACE);
	if (INVALID_HANDLE_VALUE == hdev)
	{
		hr = HRESULT_FROM_WIN32(GetLastError());
		goto Exit;
	}

	// Limit search to 100 batteries max
	for (int idev = 0; idev < 100; idev++)
	{
		did = { 0 };
		did.cbSize = sizeof(did);

		if (!SetupDiEnumDeviceInterfaces(hdev,
			0,
			&GUID_DEVCLASS_BATTERY,
			idev,
			&did))
		{
			hr = HRESULT_FROM_WIN32(GetLastError());
			if (ERROR_NO_MORE_ITEMS == GetLastError())
			{
				break;
			}
			else
			{
				continue;
			}
		}

		cbRequired = 0;

		SetupDiGetDeviceInterfaceDetail(hdev,
			&did,
			0,
			0,
			&cbRequired,
			0);
		if (ERROR_INSUFFICIENT_BUFFER != GetLastError())
		{
			hr = HRESULT_FROM_WIN32(GetLastError());
			continue;
		}

		if (pdidd != nullptr)
		{
			LocalFree(pdidd);
			pdidd = nullptr;
		}

		pdidd = (PSP_DEVICE_INTERFACE_DETAIL_DATA)LocalAlloc(LPTR, cbRequired);

		if (!pdidd)
		{
			continue;
		}

		pdidd->cbSize = sizeof(*pdidd);
		if (!SetupDiGetDeviceInterfaceDetail(hdev,
			&did,
			pdidd,
			cbRequired,
			&cbRequired,
			0))
		{
			hr = HRESULT_FROM_WIN32(GetLastError());
			continue;
		}

		// Create Battery driver interface file
		hBattery =
			CreateFile(pdidd->DevicePath,
			GENERIC_READ,
			FILE_SHARE_READ,
			NULL,
			OPEN_EXISTING,
			FILE_ATTRIBUTE_NORMAL,
			NULL);


		if (hBattery == INVALID_HANDLE_VALUE)
		{
			hr = HRESULT_FROM_WIN32(GetLastError());
			continue;
		}

		if (!DeviceIoControl(hBattery,
			IOCTL_BATTERY_QUERY_TAG,
			&dwWait,
			sizeof(dwWait),
			&batteryQueryInfo.BatteryTag,
			sizeof(batteryQueryInfo.BatteryTag),
			&dwOut,
			NULL)
			|| (batteryQueryInfo.BatteryTag == BATTERY_TAG_INVALID))
		{
			hr = HRESULT_FROM_WIN32(GetLastError());
			continue;
		}

		// With the tag, you can query the battery info.
		batteryQueryInfo.InformationLevel = BatteryInformation;

		if (!DeviceIoControl(hBattery,
			IOCTL_BATTERY_QUERY_INFORMATION,
			&batteryQueryInfo,
			sizeof(batteryQueryInfo),
			&batteryInfo,
			sizeof(batteryInfo),
			&dwOut,
			NULL))
		{
			hr = HRESULT_FROM_WIN32(GetLastError());
			continue;
		}

		// Query the battery status.
		batteryWaitStatus.BatteryTag = batteryQueryInfo.BatteryTag;

		if (!DeviceIoControl(hBattery,
			IOCTL_BATTERY_QUERY_STATUS,
			&batteryWaitStatus,
			sizeof(batteryWaitStatus),
			&batteryStatus,
			sizeof(batteryStatus),
			&dwOut,
			NULL))
		{
			hr = HRESULT_FROM_WIN32(GetLastError());
			continue;
		}
		//An batteru successfully query'd
		hr_found = S_OK;
	}

	info.PowerState = batteryStatus.PowerState;
	info.Capacity = batteryStatus.Capacity;
	info.FullCapcity = batteryInfo.FullChargedCapacity;
	info.Rate = batteryStatus.Rate;

Exit:

	if (hBattery != NULL) CloseHandle(hBattery);
	if (pdidd != nullptr)
	{
		LocalFree(pdidd);
		pdidd = nullptr;
	}
	if (hr_found == S_OK) 
	{
		info.Result = hr_found;
	}
	else 
	{
		info.Result = hr;
	}
	

	return info;
}
