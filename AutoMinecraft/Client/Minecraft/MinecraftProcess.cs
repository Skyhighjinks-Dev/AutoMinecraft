using AutoMinecraft.TypeDef;
using AutoMinecraft.Wrappers;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace AutoMinecraft.Client.Minecraft;
public class MinecraftProcess
{
  private static string JavaProcessName = "javaw";

  public Process Process { get; private set; }
  public IntPtr Pointer { get; private set; }
  public List<ThreadData> ThreadData { get; private set; }

  private Dictionary<MinecraftDataEnum, PointerPath> PointerPathConfig { get; set; }

  public MinecraftProcess()
  {
    Process = WindowWrapper.GetProcessByName(JavaProcessName);
    Pointer = WindowWrapper.OpenProcess(WindowWrapper.PROCESS_WM_READ, false, Process.Id);

    IntPtr temp = AutoFarmerLLRWrapper.GetAllThreadStartAddresses((uint)Process.Id, out int count);
    ThreadData = ExtractThreadData(temp, count);

    SetupPointerPathConfig();
  }

  public PointerPath GetPtrPathConfig(MinecraftDataEnum nData)
  { 
    if(PointerPathConfig.ContainsKey(nData))
      return PointerPathConfig[nData];

    throw new NullReferenceException($"Path hasn't been setup for '{Enum.GetName(typeof(MinecraftDataEnum), nData)}'!");
  }

  private List<ThreadData> ExtractThreadData(IntPtr nPointer, int nCount)
  {
    List<ThreadData> toReturn = new List<ThreadData>();

    for (int x = 0; x < nCount; x++)
    {
      int offset = x * Marshal.SizeOf<ThreadData>();
      IntPtr ptr = IntPtr.Add(nPointer, offset);

      ThreadData td = Marshal.PtrToStructure<ThreadData>(ptr);
      toReturn.Add(td);
    }

    return toReturn;
  }

  private void SetupPointerPathConfig()
  {
    PointerPathConfig = new Dictionary<MinecraftDataEnum, PointerPath>()
    {
      { MinecraftDataEnum.CLIENT_X_POSITION, new PointerPath(GetThreadDataIntPtr(1) - 0x958, new int[] { 0x15C, 0x360, 0x16C, 0x10 }) },
      { MinecraftDataEnum.CLIENT_Y_POSITION, new PointerPath(GetThreadDataIntPtr(1) - 0x958, new int[] { 0x16C, 0x248 }) },
      { MinecraftDataEnum.CLIENT_Z_POSITION, new PointerPath(GetThreadDataIntPtr(1) - 0x958, new int[] { 0x15C, 0x20 }) },
      { MinecraftDataEnum.CLIENT_X_ORIENTATION, new PointerPath((IntPtr)0x8E5DE820, new int[0]) },
      { MinecraftDataEnum.CLIENT_Y_ORIENTATION, new PointerPath((IntPtr)0x8E5DE81C, new int[0]) }
    };
  }


  private IntPtr GetThreadDataIntPtr(int nPosition) => (IntPtr)ThreadData.Where(x => x.Position == nPosition).First().Address;
}
