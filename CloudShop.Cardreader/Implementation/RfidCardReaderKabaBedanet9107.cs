using System;
using System.IO.Ports;
using CloudShop.Cardreader.Models;

namespace CloudShop.Cardreader.Implementation
{
    public class RfidCardReaderKabaBedanet9107 : RfidCardReader
    {
        private SerialPort _device;
        private int[] _receiveBuffer = new int[100];
        private int _writeCounter;

        /// <summary>
        /// Category for trace-messages.
        /// </summary>
        private string _traceCategory = "RfidCardReader KabaBedanet9107";

        public override bool OpenConnection(string address)
        {
            _device = new SerialPort(address, 38400, Parity.None, 8, StopBits.One);

            try
            {
                _device.Open();
                _device.DataReceived += DeviceDataReceived;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public override void CloseConnection()
        {
            if (!(_device?.IsOpen ?? false))
                return;

            try
            {
                _device.Close();
                _device.Dispose();
            }
            catch
            {
                /*ignored*/
            }
        }

        public override bool IsConnected()
        {
            return _device?.IsOpen ?? false;
        }

        /// <summary>
        /// Sendet die notwendigen Kommandos, um das auslesen einer Karte zu aktivieren.
        /// </summary>
        public override void EnableOneReading()
        {
            SendMessage1();
        }

        /// <summary>
        /// Data-Reveiced-Event der seriellen Schnittstelle
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventargumente</param>
        private void DeviceDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int bytes2Read = _device.BytesToRead;

            if (bytes2Read == 0) // wenn nichts mehr abgeholt werden muss --> Meldung überprüfen und Ende.
            {
                CheckMessages();
                return;
            }

            for (int i = 0; i < bytes2Read; i++)
            {
                lock (_receiveBuffer)
                {
                    int actualByte = _device.ReadByte();
                    _receiveBuffer[_writeCounter] = actualByte;
                    _writeCounter++;
                }
            }

            DeviceDataReceived(sender, e); // Aufruf damit wirklich alle Datenbytes abgeholt werden.
        }

        /// <summary>
        /// Diese Methode kümmert sich um das Messagehandling.
        /// </summary>
        private void CheckMessages()
        {
            int[] actualMessage = GetNextMessage();

            if (actualMessage == null)
            {
                return;
            }

            switch (actualMessage[1])
            {
                case 20: // message 1
                    //TraceWriteLine("Got Message 1 Answer", _traceCategory, TraceLevel.DebugMessage);
                    if (actualMessage[0] == 3 && actualMessage[1] == 20 && actualMessage[2] == 1 &&
                        actualMessage[3] == 22)
                    {
                        // wenn gleiche message wie gesendet:
                        SendMessage2();
                    }
                    else
                    {
                        // retry
                        SendMessage1();
                    }

                    break;
                case 48: // message 2
                    //	TraceWriteLine("Got Message 2 Answer", _traceCategory, TraceLevel.DebugMessage);
                    // Send SmStartParameter
                    SendSmStartParameter(new RfidCardReaderSmStartParameter(14, 11, 4, 25, 21, 1, new byte[] {2}));
                    break;
                case 128: // CardNumberReceived;
                    //	TraceWriteLine("Got CardData Answer", _traceCategory, TraceLevel.DebugMessage);
                    GenerateCardNumber(actualMessage);
                    break;
                default:
                    // something wrong --> reset
                    lock (_receiveBuffer)
                    {
                        _receiveBuffer = new int[100];
                        _writeCounter = 0;
                    }

                    break;
            }
        }

        /// <summary>
        /// Extrahiert aus dem Buffer die nächste Meldung.
        /// </summary>
        /// <returns>Die extrahierte Meldung (byteweise).</returns>
        private int[] GetNextMessage()
        {
            lock (_receiveBuffer)
            {
                int lengthOfMessage = _receiveBuffer[0];

                if (lengthOfMessage == 0)
                {
                    return null;
                }

                if (_writeCounter < (lengthOfMessage + 1)) // +1 wegen crc
                {
                    // Message noch nicht fertig im Buffer
                    return null;
                }

                int[] result = new int[lengthOfMessage + 1];

                // Message rauskopieren

                Array.Copy(_receiveBuffer, 0, result, 0, lengthOfMessage + 1);

                if (_writeCounter - lengthOfMessage - 1 < 0)
                {
                    //	TraceWriteLine("Error", _traceCategory, TraceLevel.ErrorMessage);
                }

                // Message aus _receiveBuffer löschen
                Array.Copy(_receiveBuffer, lengthOfMessage + 1, _receiveBuffer, 0,
                    _receiveBuffer.Length - lengthOfMessage - 1);

                // _writecounter richtig stellen
                _writeCounter -= lengthOfMessage + 1;

                return result;
            }
        }

        /// <summary>
        /// Berechnet aus einer Meldung die Kartennummer.
        /// </summary>
        /// <param name="actualMessage">Meldung zum analysieren.</param>
        private void GenerateCardNumber(int[] actualMessage)
        {
            byte[] buffer = new byte[actualMessage[9]];

            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = (byte) actualMessage[10 + i];
            }

            string cardnumber = "";

            foreach (byte actualbyte in buffer)
            {
                cardnumber += actualbyte.ToString("X2");
            }

            //	TraceWriteLine("RfidCardReaderBedanet9107::Cardnumber received: " + cardnumber, _traceCategory, TraceLevel.DebugMessage);

            FireOnCardDateReceivedEvent(cardnumber);

            FireOnShortCardDateReceivedEvent(cardnumber.Substring(16));
        }

        /// <summary>
        /// Sendet die Meldung 1.
        /// </summary>
        private void SendMessage1()
        {
            if (_device != null && _device.IsOpen)
            {
                //	TraceWriteLine("SendMessage1", _traceCategory, TraceLevel.DebugMessage);
                // länge, daten, daten, crc
                // crc = d0 ^ d1 ^ d2;
                _device.Write(new byte[] {3, 20, 1, 22}, 0, 4);
            }
        }

        /// <summary>
        ///  Sendet die Meldung 2.
        /// </summary>
        private void SendMessage2()
        {
            if (_device != null && _device.IsOpen)
            {
                //TraceWriteLine("SendMessage2", _traceCategory, TraceLevel.DebugMessage);
                // länge, daten, daten, crc
                // crc = d0 ^ d1 ^ d2;
                _device.Write(new byte[] {3, 48, 0, 51}, 0, 4);
            }
        }

        /// <summary>
        /// Senden einen SmStartParameter.
        /// </summary>
        /// <param name="parameter">Der zu versendende Parameter.</param>
        private void SendSmStartParameter(RfidCardReaderSmStartParameter parameter)
        {
            if (_device != null && _device.IsOpen)
            {
                //	TraceWriteLine("SendSmStartParameter", _traceCategory, TraceLevel.DebugMessage);

                int smStartParameterLength = 10 + parameter.SearchString.Length;

                byte[] data = new byte[smStartParameterLength + 1];

                data[0] = (byte) smStartParameterLength;
                data[1] = 0x80; // = -128
                data[2] = (byte) (parameter.DataAddr >> 8);
                data[3] = (byte) (parameter.DataAddr);
                data[4] = (byte) (parameter.DataLen);
                data[5] = (byte) (parameter.CycleTime);
                data[6] = (byte) (parameter.CrcAddr >> 8);
                data[7] = (byte) (parameter.CrcAddr);
                data[8] = (byte) (parameter.Mode);
                data[9] = (byte) (parameter.SegmentNum);

                Array.Copy(parameter.SearchString, 0, data, 10, parameter.SearchString.Length);

                // CheckSumme

                int checksum = 0;
                for (int i = 0; i < smStartParameterLength; i++)
                {
                    checksum ^= data[i];
                }

                data[data.Length - 1] = (byte) checksum;

                _device.Write(data, 0, data.Length);
            }
        }
    }
}