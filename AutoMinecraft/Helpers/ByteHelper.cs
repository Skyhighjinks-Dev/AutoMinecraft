using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMinecraft.Helpers;
public static class ByteHelper
{
  public static class Converters
  { 
    public static double ConvertByteArrToDouble(byte[] nBytes) => ConvertByteArrToDouble(nBytes, 0);
    public static double ConvertByteArrToDouble(byte[] nBytes, int nStartIndex) => BitConverter.ToDouble(nBytes, nStartIndex);

    public static float ConvertByteArrToFloat(byte[] nBytes) => ConvertByteArrToFloat(nBytes, 0);
    public static float ConvertByteArrToFloat(byte[] nBytes, int nStartIndex) => BitConverter.ToSingle(nBytes, nStartIndex);
  }
}
