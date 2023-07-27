using AutoMinecraft.Helpers;
using AutoMinecraft.Managers;
using AutoMinecraft.TypeDef;
using AutoMinecraft.Wrappers;

namespace AutoMinecraft.Client.Minecraft;
public struct ClientOrientation
{
  private static MinecraftProcess MCProcess = ServiceManager.GetService<MinecraftProcess>();

  private static PointerPath XOrientationPP = MCProcess.GetPtrPathConfig(MinecraftDataEnum.CLIENT_X_ORIENTATION);
  private static PointerPath YOrientationPP = MCProcess.GetPtrPathConfig(MinecraftDataEnum.CLIENT_Y_ORIENTATION);

  public float X
  { 
    get => ByteHelper.Converters.ConvertByteArrToFloat(XOrientationPP.GetValue<float>(MCProcess.Pointer, MCProcess.Process));
  }
  public float Y
  {
    get => ByteHelper.Converters.ConvertByteArrToFloat(YOrientationPP.GetValue<float>(MCProcess.Pointer, MCProcess.Process));
  }

  public enum Orientation
  { 
    NORTH,
    SOUTH
  }
}
