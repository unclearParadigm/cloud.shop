using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using CloudShop.Cardreader.Implementation;
using CloudShop.Cardreader.Structures;

namespace CloudShop.Cardreader
{
    public static class RfidCardReaderFactory
    {
        private static readonly IDictionary<RfidCardReaderType, RfidCardReader> SupportedCardreaders =
            new Dictionary<RfidCardReaderType, RfidCardReader>
            {
                {RfidCardReaderType.Kaba, new RfidCardReaderKabaBedanet9107()},
                {RfidCardReaderType.Twn4, new RfidCardReaderElaTecTwn4Legic()}
            };

        public static RfidCardReader OpenCardReaderConnection(string serialPort, RfidCardReaderType cardReaderType)
        {
            if (!SupportedCardreaders.Keys.Contains(cardReaderType))
                throw new NotSupportedException(
                    $"The specified Cardreader of type {cardReaderType} is not (yet) supported");

            var concreteImplementation = SupportedCardreaders[cardReaderType];

            if (concreteImplementation.OpenConnection(serialPort))
            {
                return concreteImplementation;
            }

            concreteImplementation.Dispose();
            throw new IOException($"Could not open a connection to the specified Cardreader on {serialPort}");
        }
    }
}