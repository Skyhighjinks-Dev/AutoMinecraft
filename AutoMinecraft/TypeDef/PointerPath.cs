using AutoMinecraft.Wrappers;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace AutoMinecraft.TypeDef;

public class PointerPath
{
  public IntPtr BaseAddress { get; private set; }
  public IntPtr EndAddress { get; private set; }
  public int[] Offsets { get; private set; }

  public DateTime LastRecalculation { get; private set; }

  private int Itteration = 0;
  private long Value;

  public PointerPath(IntPtr nBaseAddress, int[] nOffsets)
  {
    this.BaseAddress = nBaseAddress;
    this.Offsets = nOffsets;
  }


  public byte[] GetValue<T>(IntPtr nMinecraftProcess, Process nProcess)
  {
    EndAddress = GetFinalPointer(nProcess);

    int typeSize = Marshal.SizeOf(typeof(T));
    byte[] buffer = new byte[typeSize];


    WindowsWrapper.ReadProcessMemory(nMinecraftProcess, EndAddress, buffer, buffer.Length, out _);
    return buffer;
  }


  private IntPtr GetFinalPointer(Process nProcess)
  {
    IntPtr targetAddr = this.BaseAddress;
    int x = -1;

    do
    {
      IntPtr memAddr = targetAddr + (x < 0 ? 0 : this.Offsets[x]);

      if (x == Offsets.Length - 1)
      {
        LastRecalculation = DateTime.Now;
        return memAddr;
      }

      byte[] buffer = new byte[sizeof(ulong)];
      int bytesRead;

      if (!WindowsWrapper.ReadProcessMemory(nProcess.Handle, memAddr, buffer, buffer.Length, out int _))
      {
        Console.WriteLine($"Error - Unable to read memory at: 0x{memAddr:X}");
        return (IntPtr)0x0;
      }

      if (buffer[0] == 0x0 && buffer[7] != 0x0)
        Array.Reverse(buffer);

      targetAddr = new IntPtr(BitConverter.ToInt64(buffer, 0));

      x++;
    } while (x < Offsets.Length);

    return targetAddr;
  }
}
