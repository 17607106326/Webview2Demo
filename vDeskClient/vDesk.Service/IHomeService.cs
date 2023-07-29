using vDesk.Model;

namespace vDesk.Service;

public interface IHomeService
{
    /// <summary>
    /// 获取主页 hello 提示语
    /// </summary>
    /// <returns></returns>
    ResponseMessage<string> GetHelloText(string callParams);
}
