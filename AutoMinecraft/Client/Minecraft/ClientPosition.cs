using AutoMinecraft.Helpers;
using AutoMinecraft.Managers;
using AutoMinecraft.TypeDef;

namespace AutoMinecraft.Client.Minecraft;
public struct ClientPosition
{ 
  private static MinecraftProcess MCProcess = ServiceManager.GetService<MinecraftProcess>();

  private static PointerPath XPositionPP = MCProcess.GetPtrPathConfig(MinecraftDataEnum.CLIENT_X_POSITION);
  private static PointerPath YPositionPP = MCProcess.GetPtrPathConfig(MinecraftDataEnum.CLIENT_Y_POSITION);
  private static PointerPath ZPositionPP = MCProcess.GetPtrPathConfig(MinecraftDataEnum.CLIENT_Z_POSITION);


  public double X
  { 
    get => ByteHelper.Converters.ConvertByteArrToDouble(XPositionPP.GetValue<double>(MCProcess.Pointer, MCProcess.Process));    
  }

  public double Y
  { 
    get => ByteHelper.Converters.ConvertByteArrToDouble(YPositionPP.GetValue<double>(MCProcess.Pointer, MCProcess.Process));
  }
  
  
  public double Z
  { 
    get => ByteHelper.Converters.ConvertByteArrToDouble(ZPositionPP.GetValue<double>(MCProcess.Pointer, MCProcess.Process));
  }
}
