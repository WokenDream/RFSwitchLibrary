using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;
using FT_HANDLE = System.UInt32;

namespace RFSwitchLibrary
{
    public static class RFSwitchController
    {
        public const UInt32 FT_BAUD_300 = 300;
        public const UInt32 FT_BAUD_600 = 600;
        public const UInt32 FT_BAUD_1200 = 1200;
        public const UInt32 FT_BAUD_2400 = 2400;
        public const UInt32 FT_BAUD_4800 = 4800;
        public const UInt32 FT_BAUD_9600 = 9600;
        public const UInt32 FT_BAUD_14400 = 14400;
        public const UInt32 FT_BAUD_19200 = 19200;
        public const UInt32 FT_BAUD_38400 = 38400;
        public const UInt32 FT_BAUD_57600 = 57600;
        public const UInt32 FT_BAUD_115200 = 115200;
        public const UInt32 FT_BAUD_230400 = 230400;
        public const UInt32 FT_BAUD_460800 = 460800;
        public const UInt32 FT_BAUD_921600 = 921600;

        public const UInt32 FT_LIST_NUMBER_ONLY = 0x80000000;
        public const UInt32 FT_LIST_BY_INDEX = 0x40000000;
        public const UInt32 FT_LIST_ALL = 0x20000000;
        public const UInt32 FT_OPEN_BY_SERIAL_NUMBER = 1;
        public const UInt32 FT_OPEN_BY_DESCRIPTION = 2;

        // Word Lengths
        public const byte FT_BITS_8 = 8;
        public const byte FT_BITS_7 = 7;
        public const byte FT_BITS_6 = 6;
        public const byte FT_BITS_5 = 5;

        // Stop Bits
        public const byte FT_STOP_BITS_1 = 0;
        public const byte FT_STOP_BITS_1_5 = 1;
        public const byte FT_STOP_BITS_2 = 2;

        // Parity
        public const byte FT_PARITY_NONE = 0;
        public const byte FT_PARITY_ODD = 1;
        public const byte FT_PARITY_EVEN = 2;
        public const byte FT_PARITY_MARK = 3;
        public const byte FT_PARITY_SPACE = 4;

        // Flow Control
        public const UInt16 FT_FLOW_NONE = 0;
        public const UInt16 FT_FLOW_RTS_CTS = 0x0100;
        public const UInt16 FT_FLOW_DTR_DSR = 0x0200;
        public const UInt16 FT_FLOW_XON_XOFF = 0x0400;

        // Purge rx and tx buffers
        public const byte FT_PURGE_RX = 1;
        public const byte FT_PURGE_TX = 2;

        // Events
        public const UInt32 FT_EVENT_RXCHAR = 1;
        public const UInt32 FT_EVENT_MODEM_STATUS = 2;

        public static unsafe bool SwitchToPathFor(String EvalBoard, int numOfAttempts)
        {
            const string ChannelA = "DLP232M A";
            //const string ChannelB = "DLP232M B";
            const byte Enable = 0x01;
            const byte Mask = 0xff; // all 8 pins set to output

            UInt32 NumDevs;
            FT_HANDLE ftHandle = 0;

            byte[] DataSet = { 0 };
            switch (EvalBoard)
            {
                case "B":
                    DataSet[0] = 1; // (00000001) for Barium
                    break;
                case "X":
                    DataSet[0] = 2; // (00000010) for Xenon
                    break;
                default:
                    throw new Exception(" \"B\" stands for Barium, and \"X\" stands for Xenon");
            }
            if (numOfAttempts < 2)
            {
                Console.WriteLine("numOfAttempts: {0} has to be greater than 1", numOfAttempts);
                numOfAttempts = 2;
            }
            byte[] DataRead = { 0 };

            try
            {
                FT.ListDevices(&NumDevs, null, FT_LIST_NUMBER_ONLY); // get number of devices connected
                for (UInt32 i = 0; i < NumDevs; ++i) //find the correct channel for the chip
                {
                    byte[] DevName = new byte[64];

                    fixed (byte* temp_name = DevName, data_set = DataSet, data_read = DataRead)
                    {
                        FT.ListDevices(i, temp_name, FT_LIST_BY_INDEX | FT_OPEN_BY_DESCRIPTION); //get the description of current device

                        System.Text.ASCIIEncoding Enc = new System.Text.ASCIIEncoding();
                        string DevInfo = Enc.GetString(DevName, 0, DevName.Length).Trim('\0');
                        if (!DevInfo.Equals(ChannelA))
                        {
                            continue; // we only use channel A
                        }

                        int attempt = 0;
                        do
                        {
                            Console.WriteLine("attempt: {0}", attempt);
                            FT.OpenEx(temp_name, FT_OPEN_BY_DESCRIPTION, ref ftHandle); //open device by description
                            FT.SetBitMode(ftHandle, Mask, Enable); //enter Bit Bang mode, and set all the 8 pins to output
                            System.Threading.Thread.Sleep(50);

                            FT_HANDLE NumByteWritten = 2, NumByteRead = 2;
                            FT.Write(ftHandle, data_set, (UInt32)DataSet.Length, ref NumByteWritten); //set voltages on the 8 pins
                            System.Threading.Thread.Sleep(50);

                            FT.Read(ftHandle, data_read, (FT_HANDLE)DataRead.Length, ref NumByteRead); //read volts back from pins

                            FT.Close(ftHandle);
                            ++attempt;
                        } while (DataRead[0] != DataSet[0] && attempt < numOfAttempts);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

            return DataRead[0] == DataSet[0];
        }
    }
}
