using AutoMinecraft.Helpers;
using AutoMinecraft.TypeDef;

namespace AutoMinecraft.Minecraft.Client;
internal class ClientOrientation : IOrientation
{
  private MinecraftProcess Process;

  public float Pitch => GetPitch();
  public float Yaw => GetYaw();

  public ClientOrientation(MinecraftProcess nProcess)
  { 
    this.Process = nProcess;
  }


  private float GetPitch() => ByteHelper.Converters.ConvertByteArrToFloat(Process.GetPtrPathConfig(MinecraftClientData.CLIENT_X_ORIENTATION).GetValue<double>(Process.Pointer, Process.Process));
  private float GetYaw() => ByteHelper.Converters.ConvertByteArrToFloat(Process.GetPtrPathConfig(MinecraftClientData.CLIENT_Y_ORIENTATION).GetValue<double>(Process.Pointer, Process.Process));
}
