using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DimmingContol
{
    public class Master
    {
        private const byte fctReadCoil = 1;
        private const byte fctReadDiscreteInputs = 2;
        private const byte fctReadHoldingRegister = 3;
        private const byte fctReadInputRegister = 4;
        private const byte fctWriteSingleCoil = 5;
        private const byte fctWriteSingleRegister = 6;
        private const byte fctWriteMultipleCoils = 15;
        private const byte fctWriteMultipleRegister = 16;
        private const byte fctReadWriteMultipleRegister = 23;

        public const byte excIllegalFunction = 1;
        public const byte excIllegalDataAdr = 2;
        public const byte excIllegalDataVal = 3;
        public const byte excSlaveDeviceFailure = 4;
        public const byte excAck = 5;
        public const byte excSlaveIsBusy = 6;
        public const byte excGatePathUnavailable = 10;
        public const byte excExceptionNotConnected = 253;
        public const byte excExceptionConnectionLost = 254;
        public const byte excExceptionTimeout = 255;
        private const byte excExceptionOffset = 128;
        private const byte excSendFailt = 100;

#if false
        private static ushort _timeout = 500;
        private static ushort _refresh = 10;
        private static bool _connected = false;
#else
        private ushort _timeout = 500;
        private ushort _refresh = 10;
        private bool _connected = false;
#endif

        private Socket tcpAsyCl;
        private byte[] tcpAsyClBuffer = new byte[2048];


        public delegate void ResponseData(ushort id, byte unit, byte function, byte[] data);
        public event ResponseData OnResponseData;
        public delegate void ExceptionData(ushort id, byte unit, byte function, byte exception);
        public event ExceptionData OnException;

        public ushort Timeout
        {
            get { return _timeout; }
            set
            {
                _timeout = value;
            }
        }

        public ushort Refresh
        {
            get { return _refresh; }
            set { _refresh = value; }
        }

        public bool Connected
        {
            get { return _connected; }
        }

        public Master()
        {
        }

        public Master(string ip, ushort port)
        {
            Connect(ip, port);
        }

        public void Connect(string ip, ushort port)
        {
            try
            {
                if (IPAddress.TryParse(ip, out IPAddress _ip) == false)
                {
                    IPHostEntry hst = Dns.GetHostEntry(ip);
                    ip = hst.AddressList[0].ToString();
                }

                // ----------------------------------------------------------------
                // Connect asynchronous client
                tcpAsyCl = new Socket(IPAddress.Parse(ip).AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                tcpAsyCl.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, _timeout);
                tcpAsyCl.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, _timeout);
                tcpAsyCl.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.NoDelay, 1);


                IAsyncResult result = tcpAsyCl.BeginConnect(IPAddress.Parse(ip), port, null, null);

                bool success = result.AsyncWaitHandle.WaitOne(2000, true);

                if (tcpAsyCl.Connected)
                {
                    //tcpAsyCl.EndConnect(result);
                }
                else
                {
                    // NOTE, MUST CLOSE THE SOCKET

                    tcpAsyCl.Close();

                    throw new SocketException(10060);
                }

                _connected = true;
            }
            catch (System.IO.IOException error)
            {
                Debug.WriteLine($"Connect: {error}");

                _connected = false;
                throw error;
            }
        }

        public void Disconnect()
        {
            Dispose();

            // Add bccho
            _connected = false;
        }

        ~Master()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (tcpAsyCl != null)
            {
                if (tcpAsyCl.Connected)
                {
                    try { tcpAsyCl.Shutdown(SocketShutdown.Both); }
                    catch { }
                    tcpAsyCl.Close();
                }
                tcpAsyCl = null;
            }
            //if (tcpSynCl != null)
            //{
            //    if (tcpSynCl.Connected)
            //    {
            //        try { tcpSynCl.Shutdown(SocketShutdown.Both); }
            //        catch { }
            //        tcpSynCl.Close();
            //    }
            //    tcpSynCl = null;
            //}
        }

        internal void CallException(ushort id, byte unit, byte function, byte exception)
        {
            if ((tcpAsyCl == null) /*|| (tcpSynCl == null)*/) return;
            if (exception == excExceptionConnectionLost)
            {
                //tcpSynCl = null;
                tcpAsyCl = null;
            }
            OnException?.Invoke(id, unit, function, exception);
        }

        internal static UInt16 SwapUInt16(UInt16 inValue)
        {
            return (UInt16)(((inValue & 0xff00) >> 8) |
                     ((inValue & 0x00ff) << 8));
        }

        public void ReadCoils(ushort id, byte unit, ushort startAddress, ushort numInputs)
        {
            WriteAsyncData(CreateReadHeader(id, unit, startAddress, numInputs, fctReadCoil), id);
        }

        public void ReadDiscreteInputs(ushort id, byte unit, ushort startAddress, ushort numInputs)
        {
            WriteAsyncData(CreateReadHeader(id, unit, startAddress, numInputs, fctReadDiscreteInputs), id);
        }

        public void ReadHoldingRegister(ushort id, byte unit, ushort startAddress, ushort numInputs)
        {
            //App.RxValues.TxFrmCnt++;
            //Debug.Write(String.Format("0x{0:X4}:TX\r\n", startAddress));
            //Debug.Write("TX");
            //System.Threading.Thread.Sleep(5000);
            WriteAsyncData(CreateReadHeader(id, unit, startAddress, numInputs, fctReadHoldingRegister), id);
        }

        public void ReadInputRegister(ushort id, byte unit, ushort startAddress, ushort numInputs)
        {
            WriteAsyncData(CreateReadHeader(id, unit, startAddress, numInputs, fctReadInputRegister), id);
        }

        public void WriteSingleCoils(ushort id, byte unit, ushort startAddress, bool OnOff)
        {
            byte[] data;
            data = CreateWriteHeader(id, unit, startAddress, 1, 1, fctWriteSingleCoil);
            if (OnOff == true) data[10] = 255;
            else data[10] = 0;
            WriteAsyncData(data, id);
        }

        public void WriteMultipleCoils(ushort id, byte unit, ushort startAddress, ushort numBits, byte[] values)
        {
            byte numBytes = Convert.ToByte(values.Length);
            byte[] data;
            data = CreateWriteHeader(id, unit, startAddress, numBits, (byte)(numBytes + 2), fctWriteMultipleCoils);
            Array.Copy(values, 0, data, 13, numBytes);
            WriteAsyncData(data, id);
        }

        public void WriteSingleRegister(ushort id, byte unit, ushort startAddress, byte[] values)
        {
            byte[] data;
            data = CreateWriteHeader(id, unit, startAddress, 1, 1, fctWriteSingleRegister);
            data[10] = values[0];
            data[11] = values[1];
            WriteAsyncData(data, id);
        }

        public void WriteMultipleRegister(ushort id, byte unit, ushort startAddress, byte[] values)
        {
            ushort numBytes = Convert.ToUInt16(values.Length);
            if (numBytes % 2 > 0) numBytes++;
            byte[] data;

            data = CreateWriteHeader(id, unit, startAddress, Convert.ToUInt16(numBytes / 2), Convert.ToUInt16(numBytes + 2), fctWriteMultipleRegister);
            Array.Copy(values, 0, data, 13, values.Length);
            WriteAsyncData(data, id);
        }

        public void ReadWriteMultipleRegister(ushort id, byte unit, ushort startReadAddress, ushort numInputs, ushort startWriteAddress, byte[] values)
        {
            ushort numBytes = Convert.ToUInt16(values.Length);
            if (numBytes % 2 > 0) numBytes++;
            byte[] data;

            data = CreateReadWriteHeader(id, unit, startReadAddress, numInputs, startWriteAddress, Convert.ToUInt16(numBytes / 2));
            Array.Copy(values, 0, data, 17, values.Length);
            WriteAsyncData(data, id);
        }

        private byte[] CreateReadHeader(ushort id, byte unit, ushort startAddress, ushort length, byte function)
        {
            byte[] data = new byte[12];

            byte[] _id = BitConverter.GetBytes((short)id);
            data[0] = _id[1];			    // Slave id high byte
            data[1] = _id[0];				// Slave id low byte
            data[5] = 6;					// Message size
            data[6] = unit;					// Slave address
            data[7] = function;				// Function code
            byte[] _adr = BitConverter.GetBytes((short)IPAddress.HostToNetworkOrder((short)startAddress));
            data[8] = _adr[0];				// Start address
            data[9] = _adr[1];				// Start address
            byte[] _length = BitConverter.GetBytes((short)IPAddress.HostToNetworkOrder((short)length));
            data[10] = _length[0];			// Number of data to read
            data[11] = _length[1];			// Number of data to read
            return data;
        }

        private byte[] CreateWriteHeader(ushort id, byte unit, ushort startAddress, ushort numData, ushort numBytes, byte function)
        {
            byte[] data = new byte[numBytes + 11];

            byte[] _id = BitConverter.GetBytes((short)id);
            data[0] = _id[1];				// Slave id high byte
            data[1] = _id[0];				// Slave id low byte
            byte[] _size = BitConverter.GetBytes((short)IPAddress.HostToNetworkOrder((short)(5 + numBytes)));
            data[4] = _size[0];				// Complete message size in bytes
            data[5] = _size[1];				// Complete message size in bytes
            data[6] = unit;					// Slave address
            data[7] = function;				// Function code
            byte[] _adr = BitConverter.GetBytes((short)IPAddress.HostToNetworkOrder((short)startAddress));
            data[8] = _adr[0];				// Start address
            data[9] = _adr[1];				// Start address
            if (function >= fctWriteMultipleCoils)
            {
                byte[] _cnt = BitConverter.GetBytes((short)IPAddress.HostToNetworkOrder((short)numData));
                data[10] = _cnt[0];			// Number of bytes
                data[11] = _cnt[1];			// Number of bytes
                data[12] = (byte)(numBytes - 2);
            }
            return data;
        }

        private byte[] CreateReadWriteHeader(ushort id, byte unit, ushort startReadAddress, ushort numRead, ushort startWriteAddress, ushort numWrite)
        {
            byte[] data = new byte[numWrite * 2 + 17];

            byte[] _id = BitConverter.GetBytes((short)id);
            data[0] = _id[1];						// Slave id high byte
            data[1] = _id[0];						// Slave id low byte
            byte[] _size = BitConverter.GetBytes((short)IPAddress.HostToNetworkOrder((short)(11 + numWrite * 2)));
            data[4] = _size[0];						// Complete message size in bytes
            data[5] = _size[1];						// Complete message size in bytes
            data[6] = unit;							// Slave address
            data[7] = fctReadWriteMultipleRegister;	// Function code
            byte[] _adr_read = BitConverter.GetBytes((short)IPAddress.HostToNetworkOrder((short)startReadAddress));
            data[8] = _adr_read[0];					// Start read address
            data[9] = _adr_read[1];					// Start read address
            byte[] _cnt_read = BitConverter.GetBytes((short)IPAddress.HostToNetworkOrder((short)numRead));
            data[10] = _cnt_read[0];				// Number of bytes to read
            data[11] = _cnt_read[1];				// Number of bytes to read
            byte[] _adr_write = BitConverter.GetBytes((short)IPAddress.HostToNetworkOrder((short)startWriteAddress));
            data[12] = _adr_write[0];				// Start write address
            data[13] = _adr_write[1];				// Start write address
            byte[] _cnt_write = BitConverter.GetBytes((short)IPAddress.HostToNetworkOrder((short)numWrite));
            data[14] = _cnt_write[0];				// Number of bytes to write
            data[15] = _cnt_write[1];				// Number of bytes to write
            data[16] = (byte)(numWrite * 2);

            return data;
        }

        private void WriteAsyncData(byte[] write_data, ushort id)
        {
            if ((tcpAsyCl != null) && (tcpAsyCl.Connected))
            {
                try
                {
                    //System.Threading.Thread.Sleep(3000);

                    tcpAsyCl.BeginSend(write_data, 0, write_data.Length, SocketFlags.None, new AsyncCallback(OnSend), null);
                    tcpAsyCl.BeginReceive(tcpAsyClBuffer, 0, tcpAsyClBuffer.Length, SocketFlags.None, new AsyncCallback(OnReceive), tcpAsyCl);
                }
                catch (SystemException)
                {
                    CallException(id, write_data[6], write_data[7], excExceptionConnectionLost);
                }
            }
            else CallException(id, write_data[6], write_data[7], excExceptionConnectionLost);
        }

        private void OnSend(System.IAsyncResult result)
        {
            if (result.IsCompleted == false) CallException(0xFFFF, 0xFF, 0xFF, excSendFailt);
        }

        private void OnReceive(System.IAsyncResult result)
        {
            if (!result.IsCompleted)
            {
                CallException(0xFF, 0xFF, 0xFF, excExceptionConnectionLost);
            }

            ushort id = SwapUInt16(BitConverter.ToUInt16(tcpAsyClBuffer, 0));
            byte unit = tcpAsyClBuffer[6];
            byte function = tcpAsyClBuffer[7];
            byte[] data;

            // ------------------------------------------------------------
            // Write response data
            if ((function >= fctWriteSingleCoil) && (function != fctReadWriteMultipleRegister))
            {
                data = new byte[2];
                Array.Copy(tcpAsyClBuffer, 10, data, 0, 2);
            }
            // ------------------------------------------------------------
            // Read response data
            else
            {
                data = new byte[tcpAsyClBuffer[8]];
                Array.Copy(tcpAsyClBuffer, 9, data, 0, tcpAsyClBuffer[8]);
            }
            // ------------------------------------------------------------
            // Response data is slave exception
            if (function > excExceptionOffset)
            {
                function -= excExceptionOffset;
                CallException(id, unit, function, tcpAsyClBuffer[8]);
            }
            // ------------------------------------------------------------
            // Response data is regular data
            else OnResponseData?.Invoke(id, unit, function, data);

            //App.RxValues.RxFrmCnt++;
        }

    }
}
