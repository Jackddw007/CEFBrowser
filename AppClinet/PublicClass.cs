namespace CefiBrowser
{
    using CefiBrowser.Properties;
    using System;
    using System.Drawing;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Windows.Forms;
    using Xilium.CefGlue;
    public static class PublicClass
    {
        #region DLL申明
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", EntryPoint = "ShowWindow", CharSet = CharSet.Auto)]
        public static extern int ShowWindow(IntPtr hwnd, int nCmdShow);

        [DllImport("user32.dll", SetLastError = true)] //查询窗口句柄，有时候用Findwindow查不出来就用它来查
        public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, string windowTitle);


        [DllImport("user32.dll")]
        public static extern IntPtr BeginDeferWindowPos(int nNumWindows);

        //[DllImport("user32.dll", SetLastError = true)]
        //public static extern IntPtr DeferWindowPos(HDWP hWinPoslnfo, IntPtr hWnd, IntPtr hWndlnsertAffer, int x, int y, int cx, int Cy, uint uFags);
        [StructLayout(LayoutKind.Sequential)]
        public struct HDWP
        {
            public IntPtr hWnd;
            public IntPtr hWnd1;

        }
        [DllImport("user32.dll")]
        public static extern IntPtr DeferWindowPos(IntPtr hWinPosInfo, IntPtr hWnd,
               IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

        [DllImport("user32.dll")]
        public static extern bool EndDeferWindowPos(IntPtr hWinPosInfo);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, SetWindowPosFlags uFlags);

        [DllImport("user32.dll")]
        public static extern IntPtr GetFocus();

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern System.IntPtr GetForegroundWindow();
        //该函数返回桌面窗口的句柄。桌面窗口覆盖整个屏幕。桌面窗口是一个要在其上绘制所有的图标和其他窗口的区域
        [DllImport("User32.dll")]
        public static extern IntPtr GetDesktopWindow();

        //函数名。该函数返回指定窗口的显示状态以及被恢复的、最大化的和最小化的窗口位置
        [DllImport("User32.dll")]
        public static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);
       public struct WINDOWPLACEMENT
        {
            public uint length;
            public uint flags;
            public uint showCmd;
            public Point ptMinPosition;
            public Point ptMaxPosition;
            public Rectangle rcNormalPosition;
        };

        #endregion

        private static Color offSelectedColor = Color.FromArgb(160, 160, 160); //标签未选中时的颜色

        /// <summary>
        /// 刷新当前浏览器
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="_core"></param>
        public static void ReflashBrowser(CefBrowser browser, CefWebBrowser _core)
        {
            MainForm.Instance.Invoke(new Action(() =>
            {
                MainForm.Instance.faTabStrip1.SelectedItem.BrowserIsLoading = true;
                MainForm.Instance.faTabStrip1.SelectedItem.Title = "加载中...";
                if (_core.Browser.IsLoading)
                    _core.Browser.StopLoad();
                browser.GetMainFrame().LoadUrl(MainForm.Instance.faTabStrip1.SelectedItem.URL.Trim());
            }
         ));
        }

        /// <summary>
        /// 打开或者关闭DevTools
        /// </summary>
        /// <param name="_core"></param>
        /// <param name="browser"></param>
        public static void DevTools(CefWebBrowser _core,CefBrowser browser)
        {
            
            MainForm.Instance.Invoke(new Action(() =>
            {
                if (!_core.DevToolsOpen)
                {
                    if (MainForm.Instance.faTabStrip1.SelectedItem.DevToolsName == null || MainForm.Instance.faTabStrip1.SelectedItem.DevToolsName == string.Empty)
                    {
                        CefWindowInfo CefWindowInfo1;
                        int devWidth = MainForm.Instance.faTabStrip1.SelectedItem.Width;
                        MainForm.Instance.faTabStrip1.SelectedItem.splic.Panel1.AutoScroll = true;
                        MainForm.Instance.faTabStrip1.SelectedItem.splic.Panel2.AutoScroll = true;
                        MainForm.Instance.faTabStrip1.SelectedItem.splic.Panel2Collapsed = false;
                        MainForm.Instance.faTabStrip1.SelectedItem.Tag = _core;
                        CefWindowInfo1 = CefWindowInfo.Create();
                        CefWindowInfo1.X = devWidth / 2;
                        CefWindowInfo1.Y = 0;
                        CefRectangle rectangle = new CefRectangle(0, 0, devWidth / 2, _core.Height);
                        //此举是为了多个Devtools打开的情况，要让后面的FindwindowEX函数能找到这个窗口的句柄
                        MainForm.Instance.faTabStrip1.SelectedItem.DevToolsName = "CefDevTools" + DateTime.Now.ToString("yyyyMMddHHmmss");
                        CefWindowInfo1.SetAsWindow(MainForm.Instance.faTabStrip1.SelectedItem.splic.Panel2.Handle, rectangle, MainForm.Instance.faTabStrip1.SelectedItem.DevToolsName);
                        browser.GetHost().ShowDevTools(CefWindowInfo1, new DevFormClient(), new CefBrowserSettings(), new CefPoint(0, 0));
                        browser.GetHost().SetFocus(true);
                        _core.DevToolsOpen = MainForm.Instance.faTabStrip1.SelectedItem.DevToolsOpen = true;
                        MainForm.Instance.faTabStrip1.SelectedItem.ItemBrowser = browser;
                        MainForm.Instance.faTabStrip1.SelectedItem.splic.BackColor = SystemColors.ControlDark;
                    }
                    else
                    {
                        MainForm.Instance.faTabStrip1.SelectedItem.splic.Panel2Collapsed = false;
                        _core.DevToolsOpen = MainForm.Instance.faTabStrip1.SelectedItem.DevToolsOpen = true;
                        MainForm.Instance.faTabStrip1.SelectedItem.ItemBrowser = browser;
                    }
                }
                else
                {
                    MainForm.Instance.faTabStrip1.SelectedItem.splic.Panel2Collapsed = true;
                    _core.DevToolsOpen = MainForm.Instance.faTabStrip1.SelectedItem.DevToolsOpen = false;
                    //_core = null;
                    MainForm.Instance.faTabStrip1.SelectedItem.ItemBrowser = null;
                    // MainForm.Instance.faTabStrip1.SelectedItem.DevToolsName = null;
                    ResizeWindow(browser.GetHost().GetWindowHandle(), MainForm.Instance.faTabStrip1.SelectedItem.Width, MainForm.Instance.faTabStrip1.SelectedItem.Height);
                    GC.Collect();
                }
            }));
        }

        private class DevFormClient : CefClient
        {

        }

        public static void ResizeWindow(IntPtr handle, int width, int height)
        {
            if (handle != IntPtr.Zero)
            {
                NativeMethods.SetWindowPos(handle, IntPtr.Zero,
                    0, 0, width, height,
                    SetWindowPosFlags.ShowWindow 
                    );
            }
        }

        //初始化浏览器加载及动画播放效果
        public static void BrowserCSH(FATabStripItem tabStrip)
        {
            tabStrip.BrowserIsLoading = true; //播放动画效果
            tabStrip.ItemIcon = null;
            tabStrip.Title = "加载中...";
        }


        #region 图标和Base64码转换
        /// <summary>
        /// 将图片数据转换为Base64字符串
        public static string IamgeToBase64(Image faImage)
        {
            byte[] bytes = null ;
            MainForm.Instance.Invoke(new Action(() =>
            {
            BinaryFormatter binFormatter = new BinaryFormatter();
            MemoryStream memStream = new MemoryStream();
            binFormatter.Serialize(memStream, faImage);
            bytes = memStream.GetBuffer();
           
            memStream.Close();
            memStream.Dispose();
            binFormatter = null;
            memStream = null;

            }));
            if (bytes != null)
                return Convert.ToBase64String(bytes);
            else
            return null;
        }
        /// <summary>
        /// 将Base64字符串转换为图片
        public static Image Base64ToImage(string txtImage)
        {
            byte[] bytes = Convert.FromBase64String(txtImage);
            MemoryStream memStream = new MemoryStream(bytes);
            BinaryFormatter binFormatter = new BinaryFormatter();
            bytes = null;

            return (Image)binFormatter.Deserialize(memStream);
        }

        #endregion

        //检查文件是否存在
        public static bool CheckFiles(string fileName)
        {
            string currDirectiory = Environment.CurrentDirectory.ToString();
            if (!Directory.Exists(currDirectiory + @"\UserData\"))
                Directory.CreateDirectory(currDirectiory + @"\UserData\");


            if (!File.Exists(currDirectiory + @"\UserData\" + fileName))
            {
                File.Create(currDirectiory + @"\UserData\" + fileName).Close();
                File.Create(currDirectiory + @"\UserData\" + fileName + ".bak").Close();
            }
            else
            {
                //有就备份
                File.Copy(currDirectiory + @"\UserData\" + fileName, currDirectiory + @"\UserData\" + fileName + ".bak", true);
            }
            currDirectiory = null;
            GC.Collect();
            return true;
        }

        #region 公用值
        /// <summary>
        /// 下载TabItem的Title， "下载内容"
        /// </summary>
        public static string CefDownloadTitle = "下载内容";
        /// <summary>
        /// 内部浏览器下载页面地址 "CefiBrowser://Downloads"
        /// </summary>
        public static string CefiBrowserInternalDownloadUrl = "CefiBrowser://Downloads";
        /// <summary>
        /// 下载文件中包括@这种一般是病毒，不给下载
        /// </summary>
        public static string CefDownLoadWarling = "这个文件是病毒,已放弃下载！";

        /// <summary>
        /// 是否在下载中， false是没有下载，True是有下载
        /// </summary>
        public static bool IsDownloading = false;

        /// <summary>
        /// 强制中断下载
        /// </summary>
        public static bool AbortDownloading = false;

        /// <summary>
        /// 主程序名称
        /// </summary>
        public static string CefiBrowserName =  "CefiBrowser.exe";
        #endregion
    }


}
