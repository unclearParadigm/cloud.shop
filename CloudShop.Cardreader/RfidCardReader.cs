﻿using System;

 namespace CloudShop.Cardreader
{
	public abstract class RfidCardReader : IDisposable {
		public abstract bool OpenConnection(string serialPort);
		public abstract void CloseConnection();
		public abstract bool IsConnected();

		/// <summary>
		/// Sendet die notwendigen Kommandos, um das auslesen einer Karte zu aktivieren.
		/// </summary>
		public abstract void EnableOneReading();

		public delegate void OnCardDataReceivedDelegate(string cardnumber);

		/// <summary>
		/// Dieses Event wird ausgelöst, wenn die Kartendaten empfangen wurden und gibt die langen Daten zurück.
		/// </summary>
		public event OnCardDataReceivedDelegate OnCardDataReceived;

		/// <summary>
		/// Dieses Event wird ausgelöst, wenn die Kartendaten empfangen wurden und gibt die kurzen Daten (die auf der Karten stehen) zurück.
		/// </summary>
		public event OnCardDataReceivedDelegate OnShortCardDataReceived;

		/// <summary>
		/// Wird ausgelöst, wenn eine Karte eingelesen wurde.
		/// </summary>
		/// <param name="cardnumber">Die Kartennummer (langes Format).</param>
		protected void FireOnCardDateReceivedEvent(string cardnumber)
		{
			OnCardDataReceived?.Invoke(cardnumber);
		}

		/// <summary>
		/// Wird ausgelöst, wenn eine Karte eingelesen wurde.
		/// </summary>
		/// <param name="cardnumber">Die Kartennummer (kurzes Format).</param>
		protected void FireOnShortCardDateReceivedEvent(string cardnumber)
		{
			OnShortCardDataReceived?.Invoke(cardnumber);
		}

		public void Dispose() {
			CloseConnection();
		}
	}
}