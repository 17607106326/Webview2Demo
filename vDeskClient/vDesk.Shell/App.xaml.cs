using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Win32;
using System.Diagnostics;
using System.Management;
using System.Windows;
using System.Windows.Controls;
using vDesk.Core;
using vDesk.Infrastructure.WPF;

namespace vDeskShell;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public static IHost AppHost { get; private set; }
    public IConfiguration Configuration { get; private set; }
    public App()
    {
        List<Type> forms = new List<Type>
        {
            typeof(MainWindow)
        };
        // 创建di容器
        AppHost = ServiceBuilder.CreateContainer(forms, "vDesk.Service");
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        // 判断进程是否存在
        Process instance = ClientCheck.RunningInstance();
        if (instance is not null)
        {
            // 存在就激活
            ClientCheck.HandleRunningInstance(instance);
            return;
        }

        await AppHost!.StartAsync();

        var startUp = AppHost.Services.GetRequiredService<MainWindow>();
        startUp.Show();
        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await AppHost!.StopAsync();
        base.OnExit(e);
    }
}