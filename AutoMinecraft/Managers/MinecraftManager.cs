using AutoMinecraft.Client.Minecraft;

namespace AutoMinecraft.Managers;
public class MinecraftManager
{
  private MinecraftClient Client { get; set; }
  private MinecraftProcess Process { get; set; }

  public MinecraftManager()
  { 
    Client = new MinecraftClient();
    Process = new MinecraftProcess(); 
  }
}
