using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Reflection;
using vDesk.Infrastructure;
using vDesk.SqlSugar;

namespace vDesk.Core;

public class ServiceBuilder
{
    private static IServiceProvider serviceProvider;
    public static IConfiguration Configuration { get; private set; }

    #region 创建容器
    /// <summary>
    /// 创建容器
    /// </summary>
    /// <param name="forms">需要实例化的页面</param>
    /// <param name="dlls">需要注入的接口dll文件，默认接口和实现在同一个dll</param>
    public static IHost CreateContainer(List<Type> forms, string idlls = "")
    {
        var host = CreateHostBuilder(forms, idlls).Build();
        serviceProvider = host.Services;
        return host;
    }
    #endregion

    static IHostBuilder CreateHostBuilder(List<Type> forms, string idlls)
    {
        var host = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                // 清除已有的所有配置（包括appsettings.json配置）
                //config.Sources.Clear();
                //config
                //.SetBasePath(Directory.GetCurrentDirectory())
                //.AddJsonFile($".appsettings.json",
                //    optional: true, //文件不存在也没关系
                //    reloadOnChange: true);
            })
            .ConfigureServices((hostingContext, services) =>
            {
                services.AddLogging(loggingBuilder =>
                {
                    loggingBuilder.ClearProviders();
                    loggingBuilder.SetMinimumLevel(LogLevel.Information);
                });

                ConfigureServices(hostingContext, services, forms, idlls);
            });
        return host;

    }

    #region 注册各种服务类
    /// <summary>
    /// 注册各种服务类
    /// </summary>
    /// <param name="services"></param>
    private static void ConfigureServices(HostBuilderContext context, IServiceCollection services, List<Type> forms, string idlls)
    {
        IConfigurationBuilder builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        Configuration = builder.Build();

        // 配置 sqlsugar 数据库
        services.AddSqlsugarSetup(Configuration); 
        // 需要注入，否则其他地方注入无法获取
        services.AddSingleton(Configuration);

        #region 添加页面实例
        if (forms is not null)
        {
            foreach (var form in forms)
            {
                services.AddScoped(form);
            }
        }
        #endregion

        #region 自动注入接口,默认蒋根执行程序的接口检索注入
        HashSet<string> dlls = new HashSet<string>();
        if (!string.IsNullOrEmpty(idlls))
        {
            foreach (var item in idlls.Split(","))
            {
                dlls.Add(item);
            }
        }
        // 添加当前程序
        dlls.Add(SystemInfo.ProjectName());
        foreach (var dll in dlls)
        {
            string dllName = $"{Environment.CurrentDirectory}/{dll}.dll";
            var interfaces = Assembly.LoadFrom(dllName).GetTypes().Where(t => t.IsInterface);
            var implements = Assembly.LoadFrom(dllName).GetTypes();
            foreach (var item in interfaces)
            {
                var type = implements.FirstOrDefault(x => item.IsAssignableFrom(x) && x.Name != item.Name);
                if (type != null)
                {
                    // 记录注入的服务
                    AllServices.Add(item);
                    services.AddScoped(item, type);
                }
            }
        }
        #endregion
    }
    #endregion

    #region 获取指定类型的实例,方便winform直接通过属性注入方式获取（使用构造函数注入，其他地方打开窗口没有传参报错）
    public static T GetInstance<T>()
    {
        return serviceProvider!.GetRequiredService<T>();
    }

    /// <summary>
    /// 通过对象类型找到服务
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public static object GetInstance(Type t)
    {
        return serviceProvider!.GetService(t);
    }
    #endregion

    /// <summary>
    /// 所有的服务
    /// </summary>
    public static List<Type> AllServices { get; set; } = new List<Type>();
}
