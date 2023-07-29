using vDesk.Model;

namespace vDesk.Service;

public interface IDeskService
{
    /// <summary>
    /// 获取系统已经安装的应用程序
    /// </summary>
    /// <returns></returns>
    ResponseMessage<object> GetInstallApplication(string callParams);

    /// <summary>
    /// 启动指定程序
    /// </summary>
    /// <returns></returns>
    ResponseMessage<object> RunExe(string callParams);
}
