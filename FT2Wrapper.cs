using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;
using FT_HANDLE = System.UInt32;

namespace RFSwitchLibrary
{
    enum FT_STATUS//:Uint32
    {
        FT_OK = 0,
        FT_INVALID_HANDLE,
        FT_DEVICE_NOT_FOUND,
        FT_DEVICE_NOT_OPENED,
        FT_IO_ERROR,
        FT_INSUFFICIENT_RESOURCES,
        FT_INVALID_PARAMETER,
        FT_INVALID_BAUD_RATE,
        FT_DEVICE_NOT_OPENED_FOR_ERASE,
        FT_DEVICE_NOT_OPENED_FOR_WRITE,
        FT_FAILED_TO_WRITE_DEVICE,
        FT_EEPROM_READ_FAILED,
        FT_EEPROM_WRITE_FAILED,
        FT_EEPROM_ERASE_FAILED,
        FT_EEPROM_NOT_PRESENT,
        FT_EEPROM_NOT_PROGRAMMED,
        FT_INVALID_ARGS,
        FT_OTHER_ERROR
    };

    public static class FT
    {
        [DllImport("FTD2XX.dll")]
        static extern unsafe FT_STATUS FT_ListDevices(void* pvArg1, void* pvArg2, UInt32 dwFlags);	// FT_ListDevices by number only
        public static unsafe void ListDevices(void* pvArg1, void* pvArg2, UInt32 dwFlags)
        {
            FT_STATUS ftStatus = FT_ListDevices(pvArg1, pvArg2, dwFlags);
            if (ftStatus != FT_STATUS.FT_OK)
            {
                throw new Exception("FT_ListDevices failed");
            }
        }

        [DllImport("FTD2XX.dll")]
        static extern unsafe FT_STATUS FT_ListDevices(UInt32 pvArg1, void* pvArg2, UInt32 dwFlags);	// FT_ListDevcies by serial number or description by index only
        public static unsafe void ListDevices(UInt32 pvArg1, void* pvArg2, UInt32 dwFlags)
        {
            FT_STATUS ftStatus = FT_ListDevices(pvArg1, pvArg2, dwFlags);
            if (ftStatus != FT_STATUS.FT_OK)
            {
                throw new Exception("FT_ListDevices failed");
            }
        }

        [DllImport("FTD2XX.dll")]
        static extern FT_STATUS FT_Open(UInt32 uiPort, ref FT_HANDLE ftHandle);
        public static unsafe void Open(UInt32 uiPort, ref FT_HANDLE ftHandle)
        {
            FT_STATUS ftStatus = FT_Open(uiPort, ref ftHandle);
            if (ftStatus != FT_STATUS.FT_OK)
            {
                throw new Exception("FT_Open failed");
            }
        }

        [DllImport("FTD2XX.dll")]
        static extern unsafe FT_STATUS FT_OpenEx(void* pvArg1, UInt32 dwFlags, ref FT_HANDLE ftHandle);
        public static unsafe void OpenEx(void* pvArg1, UInt32 dwFlags, ref FT_HANDLE ftHandle)
        {
            FT_STATUS ftStatus = FT_OpenEx(pvArg1, dwFlags, ref ftHandle);
            if (ftStatus != FT_STATUS.FT_OK)
            {
                throw new Exception("FT_OpenEx failed");
            }
        }

        [DllImport("FTD2XX.dll")]
        static extern FT_STATUS FT_Close(FT_HANDLE ftHandle);
        public static unsafe void Close(FT_HANDLE ftHandle)
        {
            FT_STATUS ftStatus = FT_Close(ftHandle);
            if (ftStatus != FT_STATUS.FT_OK)
            {
                throw new Exception("FT_Close failed");
            }
        }

        [DllImport("FTD2XX.dll")]
        static extern unsafe FT_STATUS FT_Read(FT_HANDLE ftHandle, void* lpBuffer, UInt32 dwBytesToRead, ref UInt32 lpdwBytesReturned);
        public static unsafe void Read(FT_HANDLE ftHandle, void* lpBuffer, UInt32 dwBytesToRead, ref UInt32 lpdwBytesReturned)
        {
            FT_STATUS ftStatus = FT_Read(ftHandle, lpBuffer, dwBytesToRead, ref lpdwBytesReturned);
            if (ftStatus != FT_STATUS.FT_OK)
            {
                throw new Exception("FT_Read failed");
            }
        }

        [DllImport("FTD2XX.dll")]
        static extern unsafe FT_STATUS FT_Write(FT_HANDLE ftHandle, void* lpBuffer, UInt32 dwBytesToWrite, ref UInt32 lpdwBytesWritten);
        public static unsafe void Write(FT_HANDLE ftHandle, void* lpBuffer, UInt32 dwBytesToWrite, ref UInt32 lpdwBytesWritten)
        {
            FT_STATUS ftStatus = FT_Write(ftHandle, lpBuffer, dwBytesToWrite, ref lpdwBytesWritten);
            if (ftStatus != FT_STATUS.FT_OK)
            {
                throw new Exception("FT_Write failed");
            }
        }

        [DllImport("FTD2XX.dll")]
        static extern unsafe FT_STATUS FT_SetBaudRate(FT_HANDLE ftHandle, UInt32 dwBaudRate);
        public static unsafe void SetBaudRate(FT_HANDLE ftHandle, UInt32 dwBaudRate)
        {
            FT_STATUS ftStatus = FT_SetBaudRate(ftHandle, dwBaudRate);
            if (ftStatus != FT_STATUS.FT_OK)
            {
                throw new Exception("FT_SetBaudRate failed");
            }
        }

        [DllImport("FTD2XX.dll")]
        static extern unsafe FT_STATUS FT_SetDataCharacteristics(FT_HANDLE ftHandle, byte uWordLength, byte uStopBits, byte uParity);
        public static unsafe void SetDataCharacteristics(FT_HANDLE ftHandle, byte uWordLength, byte uStopBits, byte uParity)
        {
            FT_STATUS ftStatus = FT_SetDataCharacteristics(ftHandle, uWordLength, uStopBits, uParity);
            if (ftStatus != FT_STATUS.FT_OK)
            {
                throw new Exception("FT_SetDataCharacteristics failed");
            }
        }

        [DllImport("FTD2XX.dll")]
        static extern unsafe FT_STATUS FT_SetFlowControl(FT_HANDLE ftHandle, char usFlowControl, byte uXon, byte uXoff);
        public static unsafe void SetFlowControl(FT_HANDLE ftHandle, char usFlowControl, byte uXon, byte uXoff)
        {
            FT_STATUS ftStatus = FT_SetFlowControl(ftHandle, usFlowControl, uXon, uXoff);
            if (ftStatus != FT_STATUS.FT_OK)
            {
                throw new Exception("FT_SetFlowControl failed");
            }
        }

        [DllImport("FTD2XX.dll")]
        static extern unsafe FT_STATUS FT_SetDtr(FT_HANDLE ftHandle);
        public static unsafe void SetDtr(FT_HANDLE ftHandle)
        {
            FT_STATUS ftStatus = FT_SetDtr(ftHandle);
            if (ftStatus != FT_STATUS.FT_OK)
            {
                throw new Exception("FT_SetDtr failed");
            }
        }

        [DllImport("FTD2XX.dll")]
        static extern unsafe FT_STATUS FT_ClrDtr(FT_HANDLE ftHandle);
        public static unsafe void ClrDtr(FT_HANDLE ftHandle)
        {
            FT_STATUS ftStatus = FT_ClrDtr(ftHandle);
            if (ftStatus != FT_STATUS.FT_OK)
            {
                throw new Exception("FT_ClrDtr failed");
            }
        }

        [DllImport("FTD2XX.dll")]
        static extern unsafe FT_STATUS FT_SetRts(FT_HANDLE ftHandle);
        public static unsafe void SetRts(FT_HANDLE ftHandle)
        {
            FT_STATUS ftStatus = FT_SetRts(ftHandle);
            if (ftStatus != FT_STATUS.FT_OK)
            {
                throw new Exception("FT_SetRts failed");
            }
        }

        [DllImport("FTD2XX.dll")]
        static extern unsafe FT_STATUS FT_ClrRts(FT_HANDLE ftHandle);
        public static unsafe void ClrRts(FT_HANDLE ftHandle)
        {
            FT_STATUS ftStatus = FT_ClrRts(ftHandle);
            if (ftStatus != FT_STATUS.FT_OK)
            {
                throw new Exception("FT_ClrRts failed");
            }
        }

        [DllImport("FTD2XX.dll")]
        static extern unsafe FT_STATUS FT_GetModemStatus(FT_HANDLE ftHandle, ref UInt32 lpdwModemStatus);
        public static unsafe void GetModemStatus(FT_HANDLE ftHandle, ref UInt32 lpdwModemStatus)
        {
            FT_STATUS ftStatus = FT_GetModemStatus(ftHandle, ref lpdwModemStatus);
            if (ftStatus != FT_STATUS.FT_OK)
            {
                throw new Exception("FT_GetModemStatus failed");
            }
        }

        [DllImport("FTD2XX.dll")]
        static extern unsafe FT_STATUS FT_SetChars(FT_HANDLE ftHandle, byte uEventCh, byte uEventChEn, byte uErrorCh, byte uErrorChEn);
        public static unsafe void SetChars(FT_HANDLE ftHandle, byte uEventCh, byte uEventChEn, byte uErrorCh, byte uErrorChEn)
        {
            FT_STATUS ftStatus = FT_SetChars(ftHandle, uEventCh, uEventChEn, uErrorCh, uErrorChEn);
            if (ftStatus != FT_STATUS.FT_OK)
            {
                throw new Exception("FT_SetChars failed");
            }
        }

        [DllImport("FTD2XX.dll")]
        static extern unsafe FT_STATUS FT_Purge(FT_HANDLE ftHandle, UInt32 dwMask);
        public static unsafe void Purge(FT_HANDLE ftHandle, UInt32 dwMask)
        {
            FT_STATUS ftStatus = FT_Purge(ftHandle, dwMask);
            if (ftStatus != FT_STATUS.FT_OK)
            {
                throw new Exception("FT_Purge failed");
            }
        }

        [DllImport("FTD2XX.dll")]
        static extern unsafe FT_STATUS FT_SetTimeouts(FT_HANDLE ftHandle, UInt32 dwReadTimeout, UInt32 dwWriteTimeout);
        public static unsafe void SetTimeouts(FT_HANDLE ftHandle, UInt32 dwReadTimeout, UInt32 dwWriteTimeout)
        {
            FT_STATUS ftStatus = FT_SetTimeouts(ftHandle, dwReadTimeout, dwWriteTimeout);
            if (ftStatus != FT_STATUS.FT_OK)
            {
                throw new Exception("FT_SetTimeouts failed");
            }
        }

        [DllImport("FTD2XX.dll")]
        static extern unsafe FT_STATUS FT_GetQueueStatus(FT_HANDLE ftHandle, ref UInt32 lpdwAmountInRxQueue);
        public static unsafe void GetQueueStatus(FT_HANDLE ftHandle, ref UInt32 lpdwAmountInRxQueue)
        {
            FT_STATUS ftStatus = FT_GetQueueStatus(ftHandle, ref lpdwAmountInRxQueue);
            if (ftStatus != FT_STATUS.FT_OK)
            {
                throw new Exception("FT_GetQueueStatus failed");
            }
        }

        [DllImport("FTD2XX.dll")]
        static extern unsafe FT_STATUS FT_SetBreakOn(FT_HANDLE ftHandle);
        public static unsafe void SetBreakOn(FT_HANDLE ftHandle)
        {
            FT_STATUS ftStatus = FT_SetBreakOn(ftHandle);
            if (ftStatus != FT_STATUS.FT_OK)
            {
                throw new Exception("FT_SetBreakOn failed");
            }
        }

        [DllImport("FTD2XX.dll")]
        static extern unsafe FT_STATUS FT_SetBreakOff(FT_HANDLE ftHandle);
        public static unsafe void SetBreakOff(FT_HANDLE ftHandle)
        {
            FT_STATUS ftStatus = FT_SetBreakOff(ftHandle);
            if (ftStatus != FT_STATUS.FT_OK)
            {
                throw new Exception("FT_SetBreakOff failed");
            }
        }

        [DllImport("FTD2XX.dll")]
        static extern unsafe FT_STATUS FT_GetStatus(FT_HANDLE ftHandle, ref UInt32 lpdwAmountInRxQueue, ref UInt32 lpdwAmountInTxQueue, ref UInt32 lpdwEventStatus);
        public static unsafe void GetStatus(FT_HANDLE ftHandle, ref UInt32 lpdwAmountInRxQueue, ref UInt32 lpdwAmountInTxQueue, ref UInt32 lpdwEventStatus)
        {
            FT_STATUS ftStatus = FT_GetStatus(ftHandle, ref lpdwAmountInRxQueue, ref lpdwAmountInTxQueue, ref lpdwEventStatus);
            if (ftStatus != FT_STATUS.FT_OK)
            {
                throw new Exception("FT_GetStatus failed");
            }
        }

        [DllImport("FTD2XX.dll")]
        static extern unsafe FT_STATUS FT_SetEventNotification(FT_HANDLE ftHandle, UInt32 dwEventMask, void* pvArg);
        public static unsafe void SetEventNotification(FT_HANDLE ftHandle, UInt32 dwEventMask, void* pvArg)
        {
            FT_STATUS ftStatus = FT_SetEventNotification(ftHandle, dwEventMask, pvArg);
            if (ftStatus != FT_STATUS.FT_OK)
            {
                throw new Exception("FT_SetEventNotification failed");
            }
        }

        [DllImport("FTD2XX.dll")]
        static extern unsafe FT_STATUS FT_ResetDevice(FT_HANDLE ftHandle);
        public static unsafe void ResetDevice(FT_HANDLE ftHandle)
        {
            FT_STATUS ftStatus = FT_ResetDevice(ftHandle);
            if (ftStatus != FT_STATUS.FT_OK)
            {
                throw new Exception("FT_ResetDevice failed");
            }
        }

        [DllImport("FTD2XX.dll")]
        static extern unsafe FT_STATUS FT_SetDivisor(FT_HANDLE ftHandle, char usDivisor);
        public static unsafe void SetDivisor(FT_HANDLE ftHandle, char usDivisor)
        {
            FT_STATUS ftStatus = FT_SetDivisor(ftHandle, usDivisor);
            if (ftStatus != FT_STATUS.FT_OK)
            {
                throw new Exception("FT_SetDivisor failed");
            }
        }

        [DllImport("FTD2XX.dll")]
        static extern unsafe FT_STATUS FT_GetLatencyTimer(FT_HANDLE ftHandle, ref byte pucTimer);
        public static unsafe void GetLatencyTimer(FT_HANDLE ftHandle, ref byte pucTimer)
        {
            FT_STATUS ftStatus = FT_GetLatencyTimer(ftHandle, ref pucTimer);
            if (ftStatus != FT_STATUS.FT_OK)
            {
                throw new Exception("FT_GetLatencyTimer failed");
            }
        }

        [DllImport("FTD2XX.dll")]
        static extern unsafe FT_STATUS FT_SetLatencyTimer(FT_HANDLE ftHandle, byte ucTimer);
        public static unsafe void SetLatencyTimer(FT_HANDLE ftHandle, byte pucTimer)
        {
            FT_STATUS ftStatus = FT_SetLatencyTimer(ftHandle, pucTimer);
            if (ftStatus != FT_STATUS.FT_OK)
            {
                throw new Exception("FT_SetLatencyTimer failed");
            }
        }

        [DllImport("FTD2XX.dll")]
        static extern unsafe FT_STATUS FT_GetBitMode(FT_HANDLE ftHandle, ref byte pucMode);
        public static unsafe void GetBitMode(FT_HANDLE ftHandle, ref byte pucMode)
        {
            FT_STATUS ftStatus = FT_GetBitMode(ftHandle, ref pucMode);
            if (ftStatus != FT_STATUS.FT_OK)
            {
                throw new Exception("FT_GetBitMode failed");
            }
        }

        [DllImport("FTD2XX.dll")]
        static extern unsafe FT_STATUS FT_SetBitMode(FT_HANDLE ftHandle, byte ucMask, byte ucEnable);
        public static unsafe void SetBitMode(FT_HANDLE ftHandle, byte ucMask, byte ucEnable)
        {
            FT_STATUS ftStatus = FT_SetBitMode(ftHandle, ucMask, ucEnable);
            if (ftStatus != FT_STATUS.FT_OK)
            {
                throw new Exception("FT_SetBitMode failed");
            }
        }

        [DllImport("FTD2XX.dll")]
        static extern unsafe FT_STATUS FT_SetUSBParameters(FT_HANDLE ftHandle, UInt32 dwInTransferSize, UInt32 dwOutTransferSize);
        public static unsafe void SetUSBParameters(FT_HANDLE ftHandle, UInt32 dwInTransferSize, UInt32 dwOutTransferSize)
        {
            FT_STATUS ftStatus = FT_SetUSBParameters(ftHandle, dwInTransferSize, dwOutTransferSize);
            if (ftStatus != FT_STATUS.FT_OK)
            {
                throw new Exception("FT_SetUSBParameters failed");
            }
        }
    }
}
