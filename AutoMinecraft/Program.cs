using AutoMinecraft.Client.Minecraft;
using AutoMinecraft.Managers;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

public class Program
{
  private static void Main(string[] args)
  {
    MinecraftClient client = new MinecraftClient();
    MinecraftProcess process = new MinecraftProcess();

    var collection = new ServiceCollection();
    collection
      .AddSingleton(client)
      .AddSingleton(process);

    ServiceManager.SetProvider(collection);

    Thread.Sleep(3000);


    float _x = client.Orientation.X;
    float _y = client.Orientation.Y;
    Console.WriteLine($"X_Ori: {_x} | Y_Ori: {client.Orientation.Y}");

    client.PositionCursor(0);

    Thread.Sleep(200);
    float __x = client.Orientation.X;
    float __y = client.Orientation.Y;
    Console.WriteLine($"X_Ori: {__x} | Y_Ori: {client.Orientation.Y}");
    Console.WriteLine($"X_OriDiff: {__x - _x}");
    Console.WriteLine($"Y_OriDiff: {__y -_y}");
  }
}