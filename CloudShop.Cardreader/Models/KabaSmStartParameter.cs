using System;

namespace CloudShop.Cardreader.Models
{
  public class RfidCardReaderSmStartParameter
  {
    private readonly int _crcAddr;
    private readonly int _cycleTime;
    private readonly int _dataAddr;
    private readonly int _dataLen;
    private readonly int _mode;
    private readonly byte[] _searchString;
    private readonly int _segmentNum;

		/// <summary>
		/// Konstruktor
		/// </summary>
		/// <param name="dataAddr">Datenaddresse</param>
		/// <param name="dataLen">Datenlänge</param>
		/// <param name="cycleTime">Zykluszeit</param>
		/// <param name="crcAddr">Crc-Addresse</param>
		/// <param name="mode">Modus</param>
		/// <param name="segmentNum">Segmentnummer</param>
		/// <param name="searchString">Suchtext</param>
    public RfidCardReaderSmStartParameter(int dataAddr, int dataLen, int cycleTime, int crcAddr, int mode,
                                          int segmentNum, byte[] searchString)
    {
      if ((searchString.Length > 7))
      {
        throw new ArgumentException("Search string length");
      }

      _dataAddr = dataAddr;
      _dataLen = dataLen;
      _cycleTime = cycleTime;
      _crcAddr = crcAddr;
      _mode = mode;
      _segmentNum = segmentNum;
      _searchString = searchString;
    }

		/// <summary>
		/// Datenaddresse
		/// </summary>
    public int DataAddr
    {
      get { return _dataAddr; }
    }

		/// <summary>
		/// Datenlänge
		/// </summary>
    public int DataLen
    {
      get { return _dataLen; }
    }

		/// <summary>
		/// Zykluszeit
		/// </summary>
    public int CycleTime
    {
      get { return _cycleTime; }
    }

		/// <summary>
		/// Crc-Addresse
		/// </summary>
    public int CrcAddr
    {
      get { return _crcAddr; }
    }

		/// <summary>
		/// Modus
		/// </summary>
    public int Mode
    {
      get { return _mode; }
    }

		/// <summary>
		/// Segmentnummer
		/// </summary>
    public int SegmentNum
    {
      get { return _segmentNum; }
    }

		/// <summary>
		/// Suchtext
		/// </summary>
    public byte[] SearchString
    {
      get { return _searchString; }
    }
  }
}