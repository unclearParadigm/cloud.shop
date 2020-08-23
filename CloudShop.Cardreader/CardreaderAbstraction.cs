using System.Threading.Tasks;
using CloudShop.Cardreader.Structures;

namespace CloudShop.Cardreader
{
    public class CardreaderAbstraction
    {
        private const string ComPort = "Com16";
        private const RfidCardReaderType CardReaderType = RfidCardReaderType.Kaba;
        public RfidCardReader _myCardReader;

        public CardreaderAbstraction() {
            _myCardReader = RfidCardReaderFactory.OpenCardReaderConnection(ComPort, CardReaderType);
        }

        public void Connect()
        {
            _myCardReader.EnableOneReading();
        }


    }
}
