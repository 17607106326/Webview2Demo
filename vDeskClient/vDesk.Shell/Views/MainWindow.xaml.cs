using Microsoft.Extensions.Configuration;
using Microsoft.Web.WebView2.Core;
using System.Windows;
using vDesk.Core;
using vDesk.Infrastructure.WPF;
using vDesk.Shell;
using WinInterop = System.Windows.Interop;

namespace vDeskShell;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly IConfiguration _configuration;

    #region 最大化显示任务栏

    void win_SourceInitialized(object sender, EventArgs e)
    {
        System.IntPtr handle = (new WinInterop.WindowInteropHelper(this)).Handle;
        WinInterop.HwndSource.FromHwnd(handle).AddHook(new WinInterop.HwndSourceHook(WindowMaxShowtaskBar.WindowProc));
    }

    #endregion
    public MainWindow()
    {
        InitializeComponent();

        _configuration = ServiceBuilder.GetInstance<IConfiguration>();

        // 最大化显示任务栏
        this.SourceInitialized += new EventHandler(win_SourceInitialized);

        // 设置默认最大化
        this.WindowState = WindowState.Maximized;
        // 允许透明
        this.AllowsTransparency = true;
        // 取消标题栏
        this.WindowStyle = WindowStyle.None;

        this.ResizeMode = ResizeMode.NoResize;

        InitializeAsync();
    }

    public async void InitializeAsync()
    {


        // 显示初始化CoreWebView2
        await MainWebView.EnsureCoreWebView2Async();
        MainWebView.CoreWebView2InitializationCompleted += delegate (object sender, CoreWebView2InitializationCompletedEventArgs e)
        {
            Console.WriteLine("初始化完成");
            //if (e.IsSuccess)
            //{
            //    var webview2 = MainWebView.CoreWebView2;
            //    var filePath = Path.Combine(Environment.CurrentDirectory + "/dist", "index.html");
            //    var uri = new Uri(filePath).ToString();
            //    webview2.SetVirtualHostNameToFolderMapping("localhost", "./dist", CoreWebView2HostResourceAccessKind.Allow);
            //    webview2.Navigate("https://localhost/index.html");
            //}
        };

        //打开开发工具
        //MainWebView.CoreWebView2.OpenDevToolsWindow();

        var runAddress = _configuration["RunAddress"];
        MainWebView.Source = new Uri(runAddress);
        //MainWebView.CoreWebView2.Navigate("http://192.168.31.245:8000/");

        this.MainWebView.WebMessageReceived += delegate (object sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            var uri = e.TryGetWebMessageAsString();
            MainWebView.CoreWebView2.PostWebMessageAsString("wpf发送：" + uri);
        };

        if (MainWebView != null && MainWebView.CoreWebView2 != null)
        {
            // 向前端window注册WV2对象
            await MainWebView.CoreWebView2.ExecuteScriptAsync("window.WV2 = window.chrome.webview.hostObjects;");

            var mainCall = new MainCall();
            MainWebView.CoreWebView2.AddHostObjectToScript("mainCall", mainCall);
        }

        MainWebView.CoreWebView2.DOMContentLoaded += CoreWebView2_DOMContentLoaded;
    }

    private void CoreWebView2_DOMContentLoaded(object sender, CoreWebView2DOMContentLoadedEventArgs e)
    {
        // 文档重新加载。重新暴露对象，刷新会导致对象丢失
        if (MainWebView != null && MainWebView.CoreWebView2 != null)
        {
            // 向前端window注册WV2对象
            MainWebView.CoreWebView2.ExecuteScriptAsync("window.WV2 = window.chrome.webview.hostObjects;");

            var mainCall = new MainCall();
            MainWebView.CoreWebView2.AddHostObjectToScript("mainCall", mainCall);
        }
    }
}