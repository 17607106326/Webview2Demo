using System.Reflection;

namespace vDesk.Infrastructure;

public class SystemInfo
{
    /// <summary>
    /// 获取程序项目名称
    /// </summary>
    /// <returns></returns>
    public static string ProjectName() => Assembly.GetEntryAssembly()?.GetName().Name!;
}
