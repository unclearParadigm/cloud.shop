using System;
using System.IO.Ports;

namespace CloudShop.Cardreader.Implementation {
	public class RfidCardReaderElaTecTwn4Legic : RfidCardReader {
		private SerialPort _device;

		/// <summary>
		/// Category for trace-messages.
		/// </summary>
		private string _traceCategory = "RfidCardReader ElatecTWN4LEGIC";
		
		public override bool OpenConnection(string serialPort)
		{
			_device = new SerialPort(serialPort) {
				BaudRate = 9600,
				DataBits = 8,
				StopBits = StopBits.One,
				Parity = Parity.None,
				ReadTimeout = 2000,
				WriteTimeout = 2000,
				NewLine = "\r"
			};
			
			try {
				_device.Open();
				_device.DataReceived += DeviceDataReceived;
				return true;
			}
			catch (Exception) {
				return false;
			}
		}

		/// <summary>
		/// Schließt die Verbindung zum Kartenleser
		/// </summary>
		public override void CloseConnection()
		{
			if (!(_device?.IsOpen ?? false)) 
				return;

			try
			{
				_device.Close();
				_device.Dispose();
			} catch{ /*ignored*/ }
		}
		
		public override bool IsConnected() {
			return _device?.IsOpen ?? false;
		}

		public override void EnableOneReading() {
		}

		/// <summary>
		/// Data-Reveiced-Event der seriellen Schnittstelle
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">Eventargumente</param>
		private void DeviceDataReceived(object sender, SerialDataReceivedEventArgs e) {
			GenerateCardNumber(_device.ReadLine());
		}

		/// <summary>
		/// Berechnet aus einer Meldung die Kartennummer.
		/// </summary>
		/// <param name="actualMessage">Meldung zum analysieren.</param>
		private void GenerateCardNumber(string data)
		{
			FireOnCardDateReceivedEvent(data);
			FireOnShortCardDateReceivedEvent(Convert.ToInt32(data.Substring(6, 8)).ToString());
		}
	}
}