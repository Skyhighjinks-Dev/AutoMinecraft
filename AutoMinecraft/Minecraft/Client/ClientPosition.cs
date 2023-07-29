using AutoMinecraft.Helpers;
using AutoMinecraft.TypeDef;

namespace AutoMinecraft.Minecraft.Client;
internal class ClientPosition : IPosition
{
  private MinecraftProcess Process;

  public double X => GetX();

  public double Y => GetY();

  public double Z => GetZ();

  public ClientPosition(MinecraftProcess nProcess)
  { 
    this.Process = nProcess;
  }

  private double GetX() => ByteHelper.Converters.ConvertByteArrToDouble(Process.GetPtrPathConfig(MinecraftClientData.CLIENT_X_POSITION).GetValue<double>(Process.Pointer, Process.Process));
  private double GetY() => ByteHelper.Converters.ConvertByteArrToDouble(Process.GetPtrPathConfig(MinecraftClientData.CLIENT_Y_POSITION).GetValue<double>(Process.Pointer, Process.Process));
  private double GetZ() => ByteHelper.Converters.ConvertByteArrToDouble(Process.GetPtrPathConfig(MinecraftClientData.CLIENT_Z_POSITION).GetValue<double>(Process.Pointer, Process.Process));
}
