using AutoMinecraft.TypeDef;
using System.Runtime.InteropServices;

namespace AutoMinecraft.Wrappers;
public static class AutoFarmerLLRWrapper
{
  [DllImport("C:\\Dev\\Personal\\AutoMinecraft\\AutoMinecraft\\Lib\\AutoFarmerLLR\\AutoFarmerLLR.dll")]
  public static extern IntPtr GetAllThreadStartAddresses(uint pID, out int count);
}
