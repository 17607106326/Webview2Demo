using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;

namespace vDesk.Infrastructure.WPF.Installer;

public class InstallApplication
{
    /// <summary>
    /// 本机所有安转软件
    /// </ summary>
    /// <returns>软件列表:软件名称，安转路径</returns>
    public static List<InstallModel> GetInstallApplication()
    {
        List<InstallModel> gInstalledSoftware = new List<InstallModel>();

        using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall", false))
        {
            foreach (string keyName in key.GetSubKeyNames())
            {
                InstallModel installModel = new InstallModel();
                RegistryKey subkey = key.OpenSubKey(keyName);
                installModel.DisplayName = subkey.GetValue("DisplayName") as string;
                installModel.InstallLocation = subkey.GetValue("InstallLocation") as string;
                installModel.DisplayVersion = subkey.GetValue("DisplayVersion") as string;
                installModel.HelpLink = subkey.GetValue("HelpLink") as string;
                if (string.IsNullOrEmpty(installModel.DisplayName))
                    continue;

                gInstalledSoftware.Add(installModel);
            }
        }

        using (var localMachine = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
        {
            var key = localMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall", false);
            foreach (string keyName in key.GetSubKeyNames())
            {
                InstallModel installModel = new InstallModel();
                RegistryKey subkey = key.OpenSubKey(keyName);
                installModel.DisplayName = subkey.GetValue("DisplayName") as string;
                installModel.InstallLocation = subkey.GetValue("InstallLocation") as string;
                installModel.DisplayVersion = subkey.GetValue("DisplayVersion") as string;
                installModel.HelpLink = subkey.GetValue("HelpLink") as string;
                if (string.IsNullOrEmpty(installModel.DisplayName))
                    continue;

                gInstalledSoftware.Add(installModel);
            }
        }

        using (var localMachine = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32))
        {
            var key = localMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall", false);
            foreach (string keyName in key.GetSubKeyNames())
            {
                InstallModel installModel = new InstallModel();
                RegistryKey subkey = key.OpenSubKey(keyName);
                installModel.DisplayName = subkey.GetValue("DisplayName") as string;
                installModel.InstallLocation = subkey.GetValue("InstallLocation") as string;
                installModel.DisplayVersion = subkey.GetValue("DisplayVersion") as string;
                installModel.HelpLink = subkey.GetValue("HelpLink") as string;
                if (string.IsNullOrEmpty(installModel.DisplayName))
                    continue;

                gInstalledSoftware.Add(installModel);
            }
        }

        using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall", false))
        {
            foreach (string keyName in key.GetSubKeyNames())
            {
                InstallModel installModel = new InstallModel();
                RegistryKey subkey = key.OpenSubKey(keyName);
                installModel.DisplayName = subkey.GetValue("DisplayName") as string;
                installModel.InstallLocation = subkey.GetValue("InstallLocation") as string;
                installModel.DisplayVersion = subkey.GetValue("DisplayVersion") as string;
                installModel.HelpLink = subkey.GetValue("HelpLink") as string;
                if (string.IsNullOrEmpty(installModel.DisplayName))
                    continue;

                gInstalledSoftware.Add(installModel);
            }
        }
        return gInstalledSoftware.Where(d => !string.IsNullOrEmpty(d.InstallLocation)).ToList();
    }
}
