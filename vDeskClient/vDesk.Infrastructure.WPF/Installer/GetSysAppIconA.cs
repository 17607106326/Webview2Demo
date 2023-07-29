﻿using System;
using System.Runtime.InteropServices;

namespace vDesk.Infrastructure.WPF.Installer;

/// <summary>
/// 网上最常用的获取系统图标的方法，文件夹及文件图标都可获取，但只有2个尺寸：Size(16,16)、Size(32,32)
/// </summary>
public class GetSysAppIconA
{
    #region 方法4

    [DllImport("Shell32.dll")]
    public static extern IntPtr SHGetFileInfo
    (
        string pszPath, //一个包含要取得信息的文件相对或绝对路径的缓冲。它可以处理长或短文件名。（也就是指定的文件路径）注[1]
        uint dwFileAttributes,//资料上说，这个参数仅用于uFlags中包含SHGFI_USEFILEATTRIBUTES标志的情况(一般不使用)。如此，它应该是文件属性的组合：存档，只读，目录，系统等。
        out SHFILEINFO psfi,
        uint cbfileInfo,//简单地给出上项结构的尺寸。
        SHGFI uFlags//函数的核心变量，通过所有可能的标志，你就能驾驭函数的行为和实际地得到信息。
    );


    [StructLayout(LayoutKind.Sequential)]
    public struct SHFILEINFO
    {
        public IntPtr hIcon;//图标句柄
        public int iIcon;//系统图标列表的索引
        public uint dwAttributes; //文件的属性
        [MarshalAs(UnmanagedType.LPStr, SizeConst = 260)]
        public string szDisplayName;//文件的路径等 文件名最长256（ANSI），加上盘符（X:\）3字节，259字节，再加上结束符1字节，共260
        [MarshalAs(UnmanagedType.LPStr, SizeConst = 80)]
        public string szTypeName;//文件的类型名 固定80字节
    };



    public enum SHGFI
    {
        SmallIcon = 0x00000001,
        LargeIcon = 0x00000000,
        Icon = 0x00000100,
        DisplayName = 0x00000200,//Retrieve the display name for the file, which is the name as it appears in Windows Explorer. The name is copied to the szDisplayName member of the structure specified in psfi. The returned display name uses the long file name, if there is one, rather than the 8.3 form of the file name. Note that the display name can be affected by settings such as whether extensions are shown.
        Typename = 0x00000400,  //Retrieve the string that describes the file's type. The string is copied to the szTypeName member of the structure specified in psfi.
        SysIconIndex = 0x00004000, //Retrieve the index of a system image list icon. If successful, the index is copied to the iIcon member of psfi. The return value is a handle to the system image list. Only those images whose indices are successfully copied to iIcon are valid. Attempting to access other images in the system image list will result in undefined behavior.
        UseFileAttributes = 0x00000010 //Indicates that the function should not attempt to access the file specified by pszPath. Rather, it should act as if the file specified by pszPath exists with the file attributes passed in dwFileAttributes. This flag cannot be combined with the SHGFI_ATTRIBUTES, SHGFI_EXETYPE, or SHGFI_PIDL flags.
    }

    /// <summary>
    /// 根据文件名得到系统图标（经修改参数后文件夹也可以）
    /// </summary>
    /// <param name="fileName">文件名</param>
    /// <param name="largeIcon">图标的大小</param>
    /// <returns></returns>
    public static System.Drawing.Icon GetFileIcon(string fileName, bool largeIcon)
    {
        SHFILEINFO info = new SHFILEINFO();
        int size = Marshal.SizeOf(info);
        SHGFI flags;
        if (largeIcon)
            flags = SHGFI.Icon | SHGFI.LargeIcon;//| SHGFI.UseFileAttributes;网上都有加这项导致只对文件有效，去掉后文件夹也可以。
        else
            flags = SHGFI.Icon | SHGFI.SmallIcon;//| SHGFI.UseFileAttributes;网上都有加这项导致只对文件有效，去掉后文件夹也可以。
        IntPtr iconIntPtr = SHGetFileInfo(fileName, 0, out info, (uint)size, flags);
        if (iconIntPtr.Equals(IntPtr.Zero))
            return null;
        return System.Drawing.Icon.FromHandle(info.hIcon);
    }

    /// <summary>  
    /// 获取文件夹图标（可用GetFileIcon代替）
    /// </summary>  
    /// <returns>图标</returns>  
    public static System.Drawing.Icon GetDirectoryIcon(string path, bool largeIcon)
    {
        SHFILEINFO info = new SHFILEINFO();
        int size = Marshal.SizeOf(info);
        SHGFI flags;
        if (largeIcon)
            flags = SHGFI.Icon | SHGFI.LargeIcon;
        else
            flags = SHGFI.Icon | SHGFI.SmallIcon;

        IntPtr iconIntPtr = SHGetFileInfo(path, 0, out info, (uint)size, flags);
        if (iconIntPtr.Equals(IntPtr.Zero))
            return null;
        return System.Drawing.Icon.FromHandle(info.hIcon);
    }


    #endregion
}
