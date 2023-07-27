using AutoMinecraft.TypeDef;
using AutoMinecraft.Wrappers;
using System.Runtime.InteropServices;
using static AutoMinecraft.Client.Minecraft.ClientOrientation;

namespace AutoMinecraft.Client.Minecraft;
public class MinecraftClient
{
  public ClientOrientation Orientation { get; private set; }
  public ClientPosition Position { get; private set; }

  private static double StartPosX = -45.7;
  private static double StartPosZ = -50.7;

  private static float StartOriX = 167.6f;
  private static float StartOriY = 14.7f;

  private static float MovementPerTick = 0.045196533f;
  private static int Movement = 1;


  public async Task PositionCursor(Orientation nOrientation)
  {
    

    double diff = Math.Abs(Math.Abs((Orientation.X % 360)) - StartOriX);
    int loops = CalculateMovement(diff);

    Console.WriteLine(diff);
    Console.WriteLine(loops);
    for (int x = 0; x < loops; x++)
    {
      WindowWrapper.mouse_event(WindowWrapper.MOUSEEVENTF_MOVE, Movement, 0, 0, 0);
    }

    double diffY = Math.Abs(Math.Abs(Orientation.Y % 360) - StartOriY);
    int loopsY = CalculateMovement(diffY);

    Console.WriteLine(diffY);
    Console.WriteLine(loopsY);
    for (int x = 0; x < loopsY; x++)
    {
      WindowWrapper.mouse_event(WindowWrapper.MOUSEEVENTF_MOVE, 0, Movement, 0, 0);
    }
  }


  private int CalculateMovement(double nDifference) => (int)Math.Ceiling(nDifference / MovementPerTick);
}
