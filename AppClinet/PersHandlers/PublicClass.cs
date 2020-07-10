namespace CefiBrowser
{
    using CefiBrowser.Properties;
    using Microsoft.Win32;
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
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

        //隐藏鼠标指针
        [DllImport("user32.dll", EntryPoint = "ShowCursor", CharSet = CharSet.Auto)]
        public extern static void ShowCursor(int status);


        #region DllImport

        //1.声明自适应类实例  
        //***********************************************************
        //This gives us the ability to drag the borderless form to a new location
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize")]
        public static extern int SetProcessWorkingSetSize(IntPtr process, int minSize, int maxSize);

        #endregion


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
                if (browser.IsLoading)
                    browser.StopLoad();
                browser.Reload();
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

                    MainForm.Instance.faTabStrip1.Invalidate();
                    
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
        public static void BrowserLoadingInfo(FATabStripItem tabStrip)
        {
            tabStrip.ItemIcon = null;
            tabStrip.BrowserIsLoading = true; //播放动画效果
            tabStrip.Title = "加载中...";
        }


        #region 图标和Base64码转换
        /// <summary>
        /// 将图片数据转换为Base64字符串
        public static string IamgeToBase64(Image faImage)
        {
            byte[] bytes = null ;
            //MainForm.Instance.Invoke(new Action(() =>
            //{
            BinaryFormatter binFormatter = new BinaryFormatter();
            MemoryStream memStream = new MemoryStream();
            binFormatter.Serialize(memStream, faImage);
            bytes = memStream.GetBuffer();
           
            memStream.Close();
            memStream.Dispose();
            binFormatter = null;
            memStream = null;

            //}));
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

        /// <summary>
        /// Convert Byte[] to Image
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static Image BytesToImage(byte[] buffer)
        {
            MemoryStream ms = new MemoryStream(buffer);
            Image image = System.Drawing.Image.FromStream(ms);
            return image;
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


        #region  OCR图片识别，目前支持四位字符，包括字母和数字
        /// <summary>
        /// OCR图片识别，目前支持四位字符，包括字母和数字
        /// </summary>
        /// <param name="bm"></param>
        /// <returns></returns>
        public static string getPicnum(Bitmap bm,bool reCall)
        {
            MVerifyCode verifyCode = new MVerifyCode(bm);
            if (reCall)
            {
                verifyCode.reOCR = true;
            }
            else
            {
                verifyCode.reOCR = false;
            }
            verifyCode.GrayByPixels(); //灰度处理
            // 这里的166,160..代表背景杂质的颜色值，166是一个灰度值
            for (int y=140; y<170; y++)
            {
                verifyCode.ClearNoise(y, 1);
            }
            verifyCode.GetPicValidByValue(128, 4); //得到有效空间
            verifyCode.GetPicValidByValue(128); //得到有效图形
            Bitmap[] pics = verifyCode.GetSplitPics(9); //智能分割，按传入的初字符宽度
            pics[0] = verifyCode.GetCPicValidByValue(128, 1, pics[0]);
            pics[1] = verifyCode.GetCPicValidByValue(128, 1, pics[1]);
            pics[2] = verifyCode.GetCPicValidByValue(128, 1, pics[2]);
            pics[3] = verifyCode.GetCPicValidByValue(128, 1, pics[3]);

            string result = "";
            char singleChar = ' ';
            {
                for (int i = 0; i < 4; i++)
                {
                    string code = verifyCode.GetSingleBmpCode(pics[i], 128);   //得到代码串
                    int yy = code.Length;

                    for (int arrayIndex = 0; arrayIndex < CefConstHelper.CodeArray.Length; arrayIndex++)
                    {
                        int yy1 = CefConstHelper.CodeArray[arrayIndex].Length;
                        if (CefConstHelper.CodeArray[arrayIndex].Equals(code))  //相等
                        {
                            if (arrayIndex < 10)   // 0..9
                                singleChar = (char)(48 + arrayIndex);
                            else if (arrayIndex < 36) //A..Z
                                singleChar = (char)(65 + arrayIndex - 10);
                            else
                                singleChar = (char)(97 + arrayIndex - 36);
                            result = result + singleChar;
                        }
                        else
                        {
                            if (yy1 == yy)
                            {
                                int number_same = 0;
                                for (int k = 0; k < yy1; k++)
                                {
                                    if (code.Substring(k, 1) == CefConstHelper.CodeArray[arrayIndex].Substring(k, 1))
                                        number_same++;
                                }
                                if (yy1 - number_same < 7) //相似度超过97%的时候，诊断为相同
                                {
                                    if (arrayIndex < 10)   // 0..9
                                        singleChar = (char)(48 + arrayIndex);
                                    else if (arrayIndex < 36) //A..Z
                                        singleChar = (char)(65 + arrayIndex - 10);
                                    else
                                        singleChar = (char)(97 + arrayIndex - 36);
                                    result = result + singleChar;
                                }
                                else
                                {
                                    
                                }
                            }
                        }
                    }
                }
            }
            return result;

        }

        #endregion

        #region dosCommand Dos命令语句  

        public static string Execute(string dosCommand)
        {
            return Execute(dosCommand, 10);
        }
        /// <summary>  
        /// 执行DOS命令，返回DOS命令的输出  
        /// </summary>  
        /// <param name="dosCommand">dos命令</param>  
        /// <param name="milliseconds">等待命令执行的时间（单位：毫秒），  
        /// 如果设定为0，则无限等待</param>  
        /// <returns>返回DOS命令的输出</returns>  
        public static string Execute(string command, int seconds)
        {
            string output = ""; //输出字符串  
            if (command != null && !command.Equals(""))
            {
                Process process = new Process();//创建进程对象  
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = "cmd.exe";//设定需要执行的命令  
                startInfo.Arguments = "/C " + command;//“/C”表示执行完命令后马上退出  
                startInfo.UseShellExecute = false;//不使用系统外壳程序启动  
                startInfo.RedirectStandardInput = false;//不重定向输入  
                startInfo.RedirectStandardOutput = true; //重定向输出  
                startInfo.CreateNoWindow = true;//不创建窗口  
                process.StartInfo = startInfo;
                try
                {
                    if (process.Start())//开始进程  
                    {
                        if (seconds == 0)
                        {
                            process.WaitForExit();//这里无限等待进程结束  
                        }
                        else
                        {
                            process.WaitForExit(seconds); //等待进程结束，等待时间为指定的毫秒  
                        }
                        output = process.StandardOutput.ReadToEnd();//读取进程的输出  
                    }
                }
                catch
                {
                }
                finally
                {
                    if (process != null)
                        process.Close();
                }
            }
            return output;
        }

        #endregion

        #region 全屏和还原功能
        //下面是保存全屏之前的窗口尽寸
        public static int _oldLeft { get; set; }
        public static int _oldTop { get; set; }
        public static int _oldWidth { get; set; }
        public static int _oldHeight { get; set; }

        public static bool FormMaxSC { get; set; }

        public static FormBorderStyle fBorderStyle { get; set; }
        /// <summary>
        /// 保存RestoreClientRect
        /// </summary>
        public static Rectangle RestoreRect { get; set; }
        /// <summary>
        /// 全屏和还原全屏尺寸
        /// </summary>
        public static void ScreenFuction(string flashfullscreen)//,bool formMaxSC)
        {
            MainForm.Instance.Invoke(new Action(() =>
            {
                if (MainForm.Instance.ToolsPanel.Visible)
                {
                    int iActulaWidth = Screen.PrimaryScreen.Bounds.Width;
                    int iActulaHeight = Screen.PrimaryScreen.Bounds.Height;
                    //int x = MainForm.Instance.DesktopBounds.X;
                    fBorderStyle = MainForm.Instance.FormBorderStyle;
                    WinAPI.RECT rECT = new WinAPI.RECT();
                    rECT.Left = MainForm.Instance.Left;
                    rECT.Top = MainForm.Instance.Top;
                    rECT.Right = MainForm.Instance.Right;
                    rECT.Bottom = MainForm.Instance.Bottom;

                    if (MainForm.Instance.IsAboutToMaximize(rECT))
                    {
                        FormMaxSC = true;
                    }

                    _oldLeft = MainForm.Instance.Left;
                    _oldTop = MainForm.Instance.Top;
                    _oldWidth = MainForm.Instance.Width;
                    _oldHeight = MainForm.Instance.Height;
                    MainForm.Instance.faTabStrip1.FlashFullSC = true;

                    //MainForm.Instance.FormBorderStyle = FormBorderStyle.None;
                    MainForm.Instance.TopMost = true;
                    MainForm.Instance.formCloseButton1.Visible = false;
                    MainForm.Instance.formMaxNormalButton1.Visible = false;
                    MainForm.Instance.formMinButton1.Visible = false;
                    MainForm.Instance.ToolsPanel.Visible = false;
                    MainForm.Instance.FavPanel.Visible = false;
                    MainForm.Instance.faTabStrip1.Location = new Point(0, 0);
                    MainForm.Instance.faTabStrip1.Width = MainForm.Instance.faTabStrip1.Width + 2;
                    MainForm.Instance.faTabStrip1.Height = MainForm.Instance.faTabStrip1.Height + 2;

                    IntPtr m_OldWndParent = IntPtr.Zero;
                    NativeMethods.SetWindowPos(NativeMethods.GetForegroundWindow(), m_OldWndParent,
                                0, 0, iActulaWidth, iActulaHeight, SetWindowPosFlags.ShowWindow);

                }
                else
                {

                    ShowCursor(1);
                    MainForm.Instance.faTabStrip1.FlashFullSC = false;

                    //MainForm.Instance.FormBorderStyle = fBorderStyle;
                    //MainForm.Instance.ResumeLayout(false);
                    MainForm.Instance.TopMost = false;
                    MainForm.Instance.ToolsPanel.Visible = true;
                    MainForm.Instance.FavPanel.Visible = true;

                    MainForm.Instance.formCloseButton1.Visible = true;
                    MainForm.Instance.formMaxNormalButton1.Visible = true;
                    MainForm.Instance.formMinButton1.Visible = true;
                    IntPtr m_OldWndParent = IntPtr.Zero;
                    MainForm.Instance.faTabStrip1.Location = new Point(1, 1);
                    MainForm.Instance.faTabStrip1.Width = MainForm.Instance.faTabStrip1.Width - 2;
                    MainForm.Instance.faTabStrip1.Height = MainForm.Instance.faTabStrip1.Height - 2;

                    NativeMethods.SetWindowPos(NativeMethods.GetForegroundWindow(), m_OldWndParent,
                  _oldLeft, _oldTop, _oldWidth, _oldHeight, SetWindowPosFlags.ShowWindow);
                    if (FormMaxSC)
                    {
                        MainForm.Instance.WindowState = FormWindowState.Maximized;
                        MainForm.Instance.faTabStrip1.FlashFullSC = false;
                    }
                    FormMaxSC = false;
                    MainForm.Instance.Invalidate();
                }
              //  MainForm.Instance.Invalidate();
              //  MainForm.Instance.faTabStrip1.SelectedItem.splic.Panel1.Invalidate();
            }));

        }
        #endregion

        #region 注册默认浏览器
        //  HKEY_CLASSES_ROOT\\.扩展名\\shell\\open\\command：节点设置值  D:\open.EXE %1 注意后面的“%1”
        //  HKEY_CLASSES_ROOT\\.扩展名\\DefaultIcon: 赋值为：D:\open.ico,0 注意后面的“,0”
        //例如：
        //  HKEY_CLASSES_ROOT\\.mp4\\shell\\open\\command：节点设置值  D:\open.EXE %1 注意后面的“%1”
        //  HKEY_CLASSES_ROOT\\.mp4\\DefaultIcon: 赋值为：D:\OpenVideo.ico,0 注意后面的“,0”
        //函数如下：


        const string extentionStr = ".pdf";
        const string autoextentionStr = ".pdf_auto_file";
        const string commStr = @"shell\open\command";
      public static void CreateReg()
        {
            try
            {
                RegistryKey mvKey = Registry.ClassesRoot.OpenSubKey(extentionStr);
                RegistryKey mvKey1 = Registry.ClassesRoot.OpenSubKey(autoextentionStr);
                if (mvKey == null)
                {
                    mvKey = Registry.ClassesRoot.CreateSubKey(extentionStr); //创建.video节点 
                }
                if (mvKey1 == null)
                {
                    mvKey1 = Registry.ClassesRoot.CreateSubKey(autoextentionStr); //创建.video节点 
                }

                //===========先创建command
                bool createCommandKey = false;
                RegistryKey key = mvKey.OpenSubKey(commStr, true);
                RegistryKey key1 = mvKey.OpenSubKey(commStr, true);
                if (key == null)
                {
                    try
                    {
                        mvKey.CreateSubKey(commStr);
                        mvKey1.CreateSubKey(commStr);
                        createCommandKey = true;
                    }
                    catch
                    {
                    }
                }
                //如果创建command失败,则删除现有的.video，重新创建.video
                if (!createCommandKey)
                {
                    try
                    {
                        //如果现有的.mv无法创建，则删除.mv，重新创建
                        Registry.ClassesRoot.DeleteSubKeyTree(extentionStr);
                        mvKey = Registry.ClassesRoot.CreateSubKey(extentionStr); //创建.video节点 
                        Registry.ClassesRoot.DeleteSubKeyTree(autoextentionStr);
                        mvKey1 = Registry.ClassesRoot.CreateSubKey(autoextentionStr); //创建.video节点 

                    }
                    catch
                    {
                        return;//重新创建.mv失败，退出
                    }
                }

                //MVkey再次读取和创建commandkey
                RegistryKey commandKey = mvKey.OpenSubKey(commStr, true);
                RegistryKey commandKey1 = mvKey1.OpenSubKey(commStr, true);

                if (commandKey == null || commandKey1==null)
                {
                    try
                    {
                        commandKey = mvKey.CreateSubKey(commStr);
                        commandKey1 = mvKey1.CreateSubKey(commStr);

                    }
                    catch
                    {
                        return;
                    }
                }


                //mvKey创建icoKey 
                RegistryKey icoKey = mvKey.OpenSubKey("DefaultIcon", true);
                RegistryKey icoKey1 = mvKey1.OpenSubKey("DefaultIcon", true);

                if (icoKey == null || icoKey1==null)
                {
                    try
                    {
                        icoKey = mvKey.CreateSubKey("DefaultIcon");
                        icoKey1 = mvKey1.CreateSubKey("DefaultIcon");
                    }
                    catch
                    {
                        return;
                    }
                }

                //给command节点赋值,修改默认可执行程序 

                string runExe = System.Reflection.Assembly.GetExecutingAssembly().Location + " \"%1\"";
                //%1前后一定要有双引号，不然接收WINDOWS的有空格的目录，会被分成多段，导致路径识别不到。
                object getOldValue = commandKey.GetValue("");
                object getOldValue1 = commandKey1.GetValue("");
                if (getOldValue == null || getOldValue.ToString() != runExe)
                {
                    commandKey.SetValue("", runExe);
                    commandKey1.SetValue("", runExe);
                }
                //图标文件,图片文件要与EXE文件同名但不同扩展名，修改默认图片
                string icoPath = Path.ChangeExtension(System.Reflection.Assembly.GetExecutingAssembly().Location, ".ico");
                if (File.Exists(icoPath))
                {
                    string newFile = Application.CommonAppDataPath + "\\" + Path.GetFileName(icoPath);
                    if (!File.Exists(newFile))
                        File.Copy(icoPath, newFile);

                    object getIco = icoKey.GetValue("");
                    if (getIco == null || getIco.ToString() != newFile)
                        icoKey.SetValue("", newFile + ",0");

                    object getIco1 = icoKey1.GetValue("");
                    if (getIco1 == null || getIco1.ToString() != newFile)
                        icoKey1.SetValue("", newFile + ",0");

                }
            }
            catch
            {
            }
        }

      public static void CreateDefaultBrowserForWindows()
        {
            string currentPath = System.AppDomain.CurrentDomain.BaseDirectory;
            const string DIY_KEY_NAME = "htmlfile";
            RegistryKey regCR = Registry.ClassesRoot;
            RegistryKey regCommand = null;
            //if (!IsExistSubKey(regCR, DIY_KEY_NAME))
            //{
                //创建自定义的注册表项
                regCommand = regCR.CreateSubKey(DIY_KEY_NAME, RegistryKeyPermissionCheck.ReadWriteSubTree)
                .CreateSubKey("Shell", RegistryKeyPermissionCheck.ReadWriteSubTree)
                .CreateSubKey("open", RegistryKeyPermissionCheck.ReadWriteSubTree)
                .CreateSubKey("command", RegistryKeyPermissionCheck.ReadWriteSubTree);
            //}
            //else
            //{
            //    //打开已存在的自定义注册表项
            //    regCommand = regCR.OpenSubKey(DIY_KEY_NAME, true)
            //        .OpenSubKey("Shell", true)
            //        .OpenSubKey("open", true)
            //    .OpenSubKey("command", true);
            //}
            regCommand.SetValue("", "\"" + PublicClass.currDirectiory + "\\CefiBrowser.exe\"  \"%1\"");

            RegistryKey regCU = Registry.CurrentUser;
            RegistryKey regUrlAssoc = regCU.OpenSubKey("Software", true).OpenSubKey("Microsoft", true).OpenSubKey("Windows", true)
                .OpenSubKey("Shell", true)
                .OpenSubKey("Associations", true)
                .OpenSubKey("UrlAssociations", true);
            RegistryKey regHttpChoice = regUrlAssoc.OpenSubKey("http", true).OpenSubKey("UserChoice", true);
            regHttpChoice.SetValue("Progid", DIY_KEY_NAME);
            RegistryKey regHttpsChoice = regUrlAssoc.OpenSubKey("https", true).OpenSubKey("UserChoice", true);
            regHttpsChoice.SetValue("Progid", DIY_KEY_NAME);
        }


        const string AppID = "CefiBrowser";
        const string AppName = "CefiBrowser.exe";
        const string AppDescription = "CEF安全浏览器";
        static string AppPath =  System.Reflection.Assembly.GetExecutingAssembly().Location;
        static string AppIcon = AppPath + ",0";
        static string AppOpenUrlCommand = AppPath + " %1";
        static string AppReinstallCommand = AppPath + " --register";
       public static void RegisterBrowser()
        {
            // Register application.
            var appReg = Registry.LocalMachine.CreateSubKey(string.Format("SOFTWARE\\Clients\\StartMenuInternet\\{0}", AppID));
            appReg.SetValue("", AppName);
            appReg.CreateSubKey("DefaultIcon").SetValue("", AppIcon);
            appReg.CreateSubKey("shell\\open\\command").SetValue("", AppOpenUrlCommand);

            // Install info.
            var appInstallInfo = appReg.CreateSubKey("InstallInfo");
            appInstallInfo.SetValue("IconsVisible", 1);
            appInstallInfo.SetValue("ShowIconsCommand", AppPath); // TOOD: Do I need to support this?
            appInstallInfo.SetValue("HideIconsCommand", AppPath); // TOOD: Do I need to support this?
            appInstallInfo.SetValue("ReinstallCommand", AppReinstallCommand);

            // Register capabilities.
            var capabilityReg = appReg.CreateSubKey("Capabilities");
            capabilityReg.SetValue("ApplicationName", AppName);
            capabilityReg.SetValue("ApplicationIcon", AppIcon);
            capabilityReg.SetValue("ApplicationDescription", AppDescription);

            // Set up protocols we want to handle.
            var urlAssoc = capabilityReg.CreateSubKey("URLAssociations");
            urlAssoc.SetValue("http", AppID);
            urlAssoc.SetValue("https", AppID);
            urlAssoc.SetValue("ftp", AppID);
        }

       public static void UnregisterBrowser()
        {
            Registry.LocalMachine.DeleteSubKeyTree(string.Format("SOFTWARE\\Clients\\StartMenuInternet\\{0}", AppID), false);
        }

        #endregion

        #region 全局变量
        //启动时传递的URL地址
        public static string StartUrl;
        public static string currDirectiory;

        //浏览器弹窗大小
        public static int _browser_Width;
        public static int _browser_Heith;
        public static int _browser_Y;
        public static int _browser_X;

        #endregion

        #region 获取系统DPI和分辨率等
        #region Win32 API  
        [DllImport("user32.dll")]
            static extern IntPtr GetDC(IntPtr ptr);
            [DllImport("gdi32.dll")]
            static extern int GetDeviceCaps(
            IntPtr hdc, // handle to DC  
            int nIndex // index of capability  
            );
            [DllImport("user32.dll", EntryPoint = "ReleaseDC")]
            static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDc);
            #endregion

        #region DeviceCaps常量  
            const int HORZRES = 8;
            const int VERTRES = 10;
            const int LOGPIXELSX = 88;
            const int LOGPIXELSY = 90;
            const int DESKTOPVERTRES = 117;
            const int DESKTOPHORZRES = 118;
            #endregion

        #region 属性  
            /// <summary>  
            /// 获取屏幕分辨率当前物理大小  
            /// </summary>  
            public static Size WorkingArea
            {
                get
                {
                    IntPtr hdc = GetDC(IntPtr.Zero);
                    Size size = new Size();
                    size.Width = GetDeviceCaps(hdc, HORZRES);
                    size.Height = GetDeviceCaps(hdc, VERTRES);
                    ReleaseDC(IntPtr.Zero, hdc);
                    return size;
                }
            }
            /// <summary>  
            /// 当前系统DPI_X 大小 一般为96  
            /// </summary>  
            public static int DpiX
            {
                get
                {
                    IntPtr hdc = GetDC(IntPtr.Zero);
                    int DpiX = GetDeviceCaps(hdc, LOGPIXELSX);
                    ReleaseDC(IntPtr.Zero, hdc);
                    return DpiX;
                }
            }
            /// <summary>  
            /// 当前系统DPI_Y 大小 一般为96  
            /// </summary>  
            public static int DpiY
            {
                get
                {
                    IntPtr hdc = GetDC(IntPtr.Zero);
                    int DpiX = GetDeviceCaps(hdc, LOGPIXELSY);
                    ReleaseDC(IntPtr.Zero, hdc);
                    return DpiX;
                }
            }
            /// <summary>  
            /// 获取真实设置的桌面分辨率大小  
            /// </summary>  
            public static Size DESKTOP
            {
                get
                {
                    IntPtr hdc = GetDC(IntPtr.Zero);
                    Size size = new Size();
                    size.Width = GetDeviceCaps(hdc, DESKTOPHORZRES);
                    size.Height = GetDeviceCaps(hdc, DESKTOPVERTRES);
                    ReleaseDC(IntPtr.Zero, hdc);
                    return size;
                }
            }

            /// <summary>  
            /// 获取宽度缩放百分比  
            /// </summary>  
            public static float ScaleX
            {
                get
                {
                    IntPtr hdc = GetDC(IntPtr.Zero);
                    int t = GetDeviceCaps(hdc, DESKTOPHORZRES);
                    int d = GetDeviceCaps(hdc, HORZRES);
                    float ScaleX = (float)GetDeviceCaps(hdc, DESKTOPHORZRES) / (float)GetDeviceCaps(hdc, HORZRES);
                    ReleaseDC(IntPtr.Zero, hdc);
                    return ScaleX;
                }
            }
            /// <summary>  
            /// 获取高度缩放百分比  
            /// </summary>  
            public static float ScaleY
            {
                get
                {
                    IntPtr hdc = GetDC(IntPtr.Zero);
                    float ScaleY = (float)(float)GetDeviceCaps(hdc, DESKTOPVERTRES) / (float)GetDeviceCaps(hdc, VERTRES);
                    ReleaseDC(IntPtr.Zero, hdc);
                    return ScaleY;
                }
        }

        /// <summary>
        /// 动态设置标签小图标和关闭按钮大小及标签高度
        /// </summary>
        public static void SetIconCloseRectWH()
        {
            if (PublicClass.DpiX >= 140)
            {
                CefConstHelper.DEF_HEADER_HEIGHT = CefConstHelper.Def_TabButton_Hight = 38;
                CefConstHelper.closeRectH = CefConstHelper.closeRectW = 19;
                CefConstHelper.rectIconSizeH = CefConstHelper.rectIconSizeW = 19;
                CefConstHelper.TextSizeH = 19;
                CefConstHelper.AddNbt_FormRight = 166;

            }
            else if (PublicClass.DpiX >= 120 && PublicClass.DpiX <= 140)
            {
                CefConstHelper.DEF_HEADER_HEIGHT = CefConstHelper.Def_TabButton_Hight = 34;
                CefConstHelper.closeRectH = CefConstHelper.closeRectW = 17;
                CefConstHelper.rectIconSizeH = CefConstHelper.rectIconSizeW = 17;
                CefConstHelper.TextSizeH = 16;
                CefConstHelper.AddNbt_FormRight = 150;
            }
        }
#endregion



#endregion

        #region GetPicThumbnail
/// <summary>
/// 无损压缩图片
/// </summary>
/// <param name="sFile">原图片</param>
/// <param name="dFile">压缩后保存位置</param>
/// <param name="dHeight">高度</param>
/// <param name="dWidth">宽度</param>
/// <param name="flag">压缩质量 1-100</param>
/// <returns></returns>

public static Image GetPicThumbnail(Image itemImage, int dHeight, int dWidth)//, int flag)
        {
            System.Drawing.Image iSource = itemImage;// System.Drawing.Image.FromFile(sFile);
            ImageFormat tFormat = iSource.RawFormat;
            int sW = 0, sH = 0;
            //按比例缩放
            Size tem_size = new Size(iSource.Width, iSource.Height);
            if (tem_size.Width > dHeight || tem_size.Width > dWidth) //将**改成c#中的或者操作符号
            {
                if ((tem_size.Width * dHeight) > (tem_size.Height * dWidth))
                {
                    sW = dWidth;
                    sH = (dWidth * tem_size.Height) / tem_size.Width;
                }
                else
                {
                    sH = dHeight;
                    sW = (tem_size.Width * dHeight) / tem_size.Height;
                }
            }
            else
            {
                sW = tem_size.Width;
                sH = tem_size.Height;
            }

            Bitmap ob = new Bitmap(dWidth, dHeight);
            Graphics g = Graphics.FromImage(ob);
            g.Clear(Color.WhiteSmoke);
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(iSource, new Rectangle((dWidth - sW) / 2, (dHeight - sH) / 2, sW, sH), 0, 0, iSource.Width, iSource.Height, GraphicsUnit.Pixel);
            g.Dispose();
            return ob;
            //以下代码为保存图片时，设置压缩质量
            //EncoderParameters ep = new EncoderParameters();
            //long[] qy = new long[1];
            //qy[0] = flag;//设置压缩的比例1-100
            //EncoderParameter eParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qy);
            //ep.Param[0] = eParam;
            //try
            //{
            //    ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();

            //    ImageCodecInfo jpegICIinfo = null;

            //    for (int x = 0; x < arrayICI.Length; x++)
            //    {
            //        if (arrayICI[x].FormatDescription.Equals("JPEG"))
            //        {
            //            jpegICIinfo = arrayICI[x];
            //            break;
            //        }
            //    }
            //    if (jpegICIinfo != null)
            //    {
            //        ob.Save(dFile, jpegICIinfo, ep);//dFile是压缩后的新路径
            //    }
            //    else
            //    {
            //        ob.Save(dFile, tFormat);
            //    }
            //    return true;
            //}
            //catch
            //{
            //    return false;
            //}
            //finally
            //{
            //    iSource.Dispose();
            //    ob.Dispose();

            //}
        }
        #endregion

        #region IP地址转换
        /// <summary>
                /// 将IPv4格式的字符串转换为int型表示
                /// </summary>
                /// <param name="strIPAddress">IPv4格式的字符</param>
                /// <returns></returns>
        public static int IPToNumber(string strIPAddress)
        {
            //将目标IP地址字符串strIPAddress转换为数字    
            string[] arrayIP = strIPAddress.Split('.');
            int sip1 = Int32.Parse(arrayIP[0]);
            int sip2 = Int32.Parse(arrayIP[1]);
            int sip3 = Int32.Parse(arrayIP[2]);
            int sip4 = Int32.Parse(arrayIP[3]);
            int tmpIpNumber;
            tmpIpNumber = (sip1 << 24) + (sip2 << 16) + (sip3 << 8) + sip4;
            return tmpIpNumber;
        }


        /// <summary>
        /// 将int型表示的IP还原成正常IPv4格式。
        /// </summary>/// <param name="intIPAddress">
        /// int型表示的IP
        ///</param>
        /// <returns></returns>
        public static string NumberToIP(int intIPAddress)
        {
            byte[] bs = BitConverter.GetBytes(intIPAddress);
            return string.Format("{0}.{1}.{2}.{3}", bs[3], bs[2], bs[1], bs[0]);
        }

        public static string NumberToIP(uint intIPAddress)
        {
            byte[] bs = BitConverter.GetBytes(intIPAddress);
            return string.Format("{0}.{1}.{2}.{3}", bs[3], bs[2], bs[1], bs[0]);
        }

        #endregion

        #region 保存窗口每次变大时的尽寸
        /// <summary>
        /// 窗体变化前X坐标
        /// </summary>
        public static int ReDesktopBoudX;
        /// <summary>
        /// 窗体变化前Y坐标
        /// </summary>
        public static int ReDesktopBoudY;
        /// <summary>
        /// 窗体变化前高度
        /// </summary>
        public static int ReDesktopBoudHeight;
        /// <summary>
        /// 窗体变化前宽度
        /// </summary>
        public static int ReDesktopBoudWidth;

        #endregion

    }


}
