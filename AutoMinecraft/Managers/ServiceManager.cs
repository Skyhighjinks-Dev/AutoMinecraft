using Microsoft.Extensions.DependencyInjection;

namespace AutoMinecraft.Managers;

internal class ServiceManager
{
  public static IServiceProvider ServiceProvider { get; private set; }

  public static void SetProvider(ServiceCollection nCollection) => ServiceProvider = nCollection.BuildServiceProvider();

  public static T GetService<T>() where T : new() => ServiceProvider.GetRequiredService<T>();
}