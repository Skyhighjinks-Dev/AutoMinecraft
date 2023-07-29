using AutoMinecraft.Minecraft.Client;

namespace AutoMinecraft.Managers;

public class MinecraftManager
{
  private MinecraftClient Client { get; set; }

  public MinecraftManager()
  {
    Client = new MinecraftClient();
  }
}
