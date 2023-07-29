using Newtonsoft.Json;
using System.Reflection;
using vDesk.Core;

namespace vDesk.Shell;

public class MainCall
{
    /// <summary>
    /// 入口方法，所有的调用都通过这个方法进来，根据参数，分发对应的逻辑调用
    /// </summary>
    /// <param name="callName">分发的方法名称</param>
    /// <param name="callParams">分发的参数</param>
    /// <returns></returns>
    public string Call(string callName, object callParams)
    {
        var nameSplit = callName.Split("/");
        // 服务名
        var iName = $"I{nameSplit.First()}Service";
        // 方法名
        var iAction = nameSplit.Last();

        var target = ServiceBuilder.AllServices.Find(d => d.Name.Equals(iName));

        MethodInfo mt = target.GetMethod(iAction);

        var diService = GetDIService(iName, target);

        var result = mt.Invoke(diService, new object[] { callParams });

        return JsonConvert.SerializeObject(result);
    }

    /// <summary>
    /// 记录注入的服务
    /// </summary>
    private static HashSet<object> _DIServices = new HashSet<object>();

    /// <summary>
    /// 查找注入的服务，已注入直接拿，未注入的注入
    /// </summary>
    /// <param name="iName">接口名</param>
    /// <param name="target">对象类型</param>
    /// <returns></returns>
    public object GetDIService(string iName, Type target)
    {
        object obj = null;
        bool notExists = false;
        foreach (var item in _DIServices)
        {
            Type t = item.GetType();
            if (iName.Equals(t.Name))
            {
                obj = item;
                notExists = true;
                break;
            }
        }

        if (!notExists)
        {
            var addObj = ServiceBuilder.GetInstance(target);
            obj = addObj;
            _DIServices.Add(addObj);
        }
        return obj;
    }
}