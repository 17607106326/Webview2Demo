using Serilog;
using System;
using System.IO;

namespace vDesk.Infrastructure.WPF;

public class RunExeHandler
{
    public static void InvokeExe(string path)
    {
        try
        {
            if (File.Exists(path) == true)
            {
                //创建启动对象
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                //设置运行文件
                startInfo.FileName = path;
                //设置启动参数
                //startInfo.Arguments ="123123123";
                //设置启动动作,确保以管理员身份运行
                //startInfo.Verb = "runas";
                //如果不是管理员，则启动UAC
                System.Diagnostics.Process.Start(startInfo);
            }
        }
        catch (Exception e)
        {
            Log.Error($"启动 程序时失败：{e.Message + e.InnerException}");
        }
    }

}
