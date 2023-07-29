using Microsoft.Extensions.Configuration;
using Microsoft.Win32;
using SqlSugar;
using vDesk.Model;

namespace vDesk.Service.Services;

public class HomeService : IHomeService
{
    private readonly ISqlSugarClient _db;
    private readonly IConfiguration _Configuration;

    public HomeService(ISqlSugarClient db, IConfiguration Configuration)
    {
        _db = db;
        _Configuration = Configuration;
    }

    public ResponseMessage<string> GetHelloText(string callParams)
    {

        try
        {
            var a = _db.Queryable<Weather>().ToList();
        }
        catch (Exception)
        {
            throw;
        }

        var hour = DateTime.Now.Hour;
        var hourTxt = "早上";
        if (hour >= 6 && hour < 9)
        {
            hourTxt = "早上";
        }
        else if (hour >= 9 && hour < 11)
        {
            hourTxt = "上午";
        }
        else if (hour >= 11 && hour < 13)
        {
            hourTxt = "中午";
        }
        else if (hour >= 13 && hour < 17)
        {
            hourTxt = "下午";
        }
        else
        {
            hourTxt = "晚上";
        }
        return new ResponseMessage<string>() { Code = ResponseMessageCode.成功, Data = $"{hourTxt}好啊,GetH,{callParams}" };
    }
}
