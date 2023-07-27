using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMinecraft.Managers;
internal class ServiceManager
{
  public static IServiceProvider ServiceProvider { get; private set; }

  public static void SetProvider(ServiceCollection nCollection) => ServiceProvider = nCollection.BuildServiceProvider();

  public static T GetService<T>() where T : new() => ServiceProvider.GetRequiredService<T>();
}