using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace vDesk.Infrastructure.WPF;

/// <summary>
/// 客户端检测
/// </summary>
public class ClientCheck
{
    #region 同一个电脑只允许一个客户端存在
    public static Process RunningInstance()
    {
        Process current = Process.GetCurrentProcess();
        Process[] processes = Process.GetProcessesByName(current.ProcessName);
        foreach (Process process in processes)
        {
            if (process.Id != current.Id)
            {
                if (process.ProcessName == current.ProcessName)
                {
                    return process;
                }
            }
        }
        return null;
    }

    public static void HandleRunningInstance(Process instance)
    {
        //Make   sure   the   window   is   not   minimized   or   maximized   
        ShowWindowAsync(instance.MainWindowHandle, SW_MAXIMIZE);
        //Set   the   real   intance   to   foreground   window
        SetForegroundWindow(instance.MainWindowHandle);
    }

    [DllImport("User32.dll")]
    private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);
    [DllImport("User32.dll")]
    private static extern bool SetForegroundWindow(IntPtr hWnd);

    private const int SW_HIDE = 0;
    private const int SW_NORMAL = 1; //正常弹出窗体 
    private const int SW_MAXIMIZE = 3; //最大化弹出窗体 
    private const int SW_SHOWNOACTIVATE = 4;
    private const int SW_SHOW = 5;
    private const int SW_MINIMIZE = 6;
    private const int SW_RESTORE = 9;
    private const int SW_SHOWDEFAULT = 10;
    #endregion
}
