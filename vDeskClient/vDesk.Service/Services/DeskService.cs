using System.Windows.Markup;
using vDesk.Infrastructure.WPF;
using vDesk.Infrastructure.WPF.Installer;
using vDesk.Model;

namespace vDesk.Service.Services;

public class DeskService : IDeskService
{
    public ResponseMessage<object> GetInstallApplication(string callParams)
    {
        var data = InstallApplication.GetInstallApplication();
        return new ResponseMessage<object>() { Code = ResponseMessageCode.成功, Data = data };
    }

    public ResponseMessage<object> RunExe(string callParams)
    {
        RunExeHandler.InvokeExe(callParams);
        return new ResponseMessage<object>() { Code = ResponseMessageCode.成功, Data = "" };
    }
}
