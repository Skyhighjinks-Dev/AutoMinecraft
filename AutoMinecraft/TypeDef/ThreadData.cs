using System.Runtime.InteropServices;

namespace AutoMinecraft.TypeDef;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct ThreadData
{
  public ulong Address; // Memeory Address
  public int Position; // ThreadStack position
}
