using AutoMinecraft.TypeDef;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace AutoMinecraft.Wrappers;
public static class WindowWrapper
{
  public static readonly int PROCESS_WM_READ = 0x0010;

  public static readonly int MOUSEEVENTF_MOVE = 0x0001;

  [DllImport("kernel32.dll")]
  internal static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

  [DllImport("kernel32.dll", SetLastError = true)]
  internal static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out] byte[] lpBuffer, int dwSize, out int lpNumberOfBytesRead);

  [DllImport("kernel32.dll")]
  internal static extern bool CloseHandle(IntPtr hObject);

  [DllImport("user32.dll")]
  public static extern bool SetCursorPos(int X, int Y);

  [DllImport("user32.dll")]
  public static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

  public static Process GetProcessByName(string nProcessName)
  {
    Process[] processes = Process.GetProcessesByName(nProcessName);
    return processes.Length > 0 ? processes[0] : null;
  }
}
