using AutoMinecraft.Helpers;
using AutoMinecraft.TypeDef;

namespace AutoMinecraft.Minecraft.Client;

internal class MinecraftClient : MinecraftProcess
{
  private ClientPosition ClientPosition { get; init; }
  private ClientOrientation ClientOrientation { get; init; }

  private static double StartPosX = -45.7;
  private static double StartPosZ = -50.7;

  private static float StartOriX = 167.6f;
  private static float StartOriY = 14.7f;

  private static int Movement = 25;
  private float AmountPerItteration = 1.1299907f;

  public MinecraftClient() : base()
  {
    ClientPosition = new ClientPosition(this);
    ClientOrientation = new ClientOrientation(this);
  }
}
