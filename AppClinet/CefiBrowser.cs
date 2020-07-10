using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using Xilium.CefGlue;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using System.Drawing.Imaging;

namespace CefiBrowser
{
    public partial class MainForm : Form
    {
        private Color offSelectedColor = Color.FromArgb(160, 160, 160); //标签未选中时的颜色
        private Color onSelectedColor = Color.FromArgb(242, 242, 242); //标签被选中时的颜色
        private Color MoveSelectedColor = Color.FromArgb(231, 231, 231);
        private Color SbackColor = Color.FromArgb(208, 208, 208); //背景色
        public bool WebSecurity = true;
        public bool CrossDomainSecurity = true;
        public bool WebGL = true;
        private bool RunAPP = false; //决定APP是否运行
        public static string Branding = "CefiBrowser";
        private static int brIndex;
        public bool TitleChangedisTrue = false;
        public string _directory = "Temp/";
        public bool RunQueueScreen = false; //是否运行外屏模式

        public HostHandler host;
        public static MainForm Instance;
        public static string Date_Added = string.Empty; //存放FavriID，方便删除
        StringFormat ssf;
        public bool cefimousDown = false; //防止在拉申和缩小窗口的时候判别是否启用防控件闪烁功能
        public bool cefCtrlDown = false; //判断Ctrl键是否按下
        public bool cefCtrlC = false;
        private int X=0, Y=0; //用来存放拖拽最大化之前的Location 位置
                              //public CefBrowser cefBrowser; //存放_core browser

       public  List<FavireBT> favireBTs;
       public  List<FavirteButton> mFavList = new List<FavirteButton>();
        public string[] RUL;
        public static int scrWidth;
        public static int scrHight;
        public static Size fsize; //保存初始的Size
        Timer _Timer,_Timer2;
        private string _startUrl;
        public MainForm(string starturl)
        {
            PublicClass.SetIconCloseRectWH();

            ////进程间传递参数
            //if (starturl != null || starturl != "")
            //{
            //    Process[] allProcess = Process.GetProcesses();
            //    foreach (Process p in allProcess)
            //    {

            //        if (p.ProcessName.ToLower() + ".exe" == "cefibrowser.exe".ToLower())
            //        {
            //            IntPtr intPtrCef = p.Handle;

            //            break;
            //        }
            //    }
            //}
            _startUrl = starturl;
            //MessageBox.Show(_startUrl);
            Instance = this;
         
            InitializeComponent();

            _Timer = new Timer() { Interval = 266 };
            _Timer.Tick += new EventHandler(Timer_Tick);
            base.Opacity = 0;
            _Timer.Start();

            ssf = new StringFormat();
            ssf.Trimming = StringTrimming.EllipsisCharacter;
            ssf.FormatFlags |= StringFormatFlags.NoWrap;
            ssf.FormatFlags &= StringFormatFlags.NoFontFallback;

            this.MaximizedBounds = Screen.PrimaryScreen.WorkingArea;
            this.MinimumSize = new Size(366, 266);
            this.ControlAdded += Window_ControlAdded;
            fsize = this.ClientSize;
        }

        //防止控件闪烁
        private void Timer_Tick3(object sender, EventArgs e)
        {
            if (this.Opacity >= 1)
            {
                _Timer.Stop();
            }
            else
            {
                base.Opacity += 0.4;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (this.Opacity >= 1)
            {
                _Timer.Stop();
                _Timer2 = new Timer();
                if (_startUrl != null && _startUrl != "")
                {
                    _Timer2.Interval = 300;
                }
                else
                {
                    _Timer2.Interval = 16600;
                }
                _Timer2.Tick += new EventHandler(Timer_Tick1);
                _Timer2.Start();

            }
            else
            {
                base.Opacity += 0.3;
            }
        }
        private void Timer_Tick1(object sender, EventArgs e)
        {
            if (_startUrl != null && _startUrl != "")
            {
              //  MessageBox.Show(_startUrl);
                faTabStrip1.SelectedItem.splic.Panel1.Controls.Add(NewChromiumWeb(_startUrl, faTabStrip1.SelectedItem.TabIndex));
                _Timer2.Stop();
                _Timer2.Enabled = false;
            }
            else 
            {
                /// <summary>
                /// 开启外屏模式
                /// </summary>
                try
                {
                    RUL = File.ReadAllLines(PublicClass.currDirectiory + @"\Temp\gstBrowserTXT.txt");
                    if (RUL[0].Length > 30)
                    {
                        RunQueueScreen = true; //激活外屏模式
                        faTabStrip1.SelectedItem.splic.Panel1.Controls.Add(NewChromiumWeb(RUL[0].ToString(), faTabStrip1.SelectedItem.TabIndex));
                        PublicClass.ScreenFuction("");//,true);
                        _Timer2.Stop();
                        _Timer2.Enabled = false;
                    }
                }
                catch
                {

                }
                RUL = null;
                GC.Collect();
            }

            _Timer2.Stop();
            _Timer2.Enabled = false;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            MouseHook.MouseAction += new MouseEventHandler(Event);
            MouseHook.Start();


            Process[] allProcess = Process.GetProcesses();
            foreach (Process p in allProcess)
            {

                if (p.ProcessName.ToLower() + ".exe" == "CefMain.exe".ToLower())
                {
                    for (int i = 0; i < p.Threads.Count; i++)
                        p.Threads[i].Dispose();
                    p.Kill();
                    RunAPP = true;
                    //break;
                }
            }

            //if (!RunAPP)
            //{
            //    this.Close();
            //    Application.ExitThread();
            //    Application.Exit();
            //}
            //this.ControlBox = false;
            this.Text = "CefiBrowser";
            //默认启用多用户支持
            //MulitiUsers_Support.Checked = false;
            //MulitiUsers_Support.Visible = false;
            scrHight = this.Height;
            scrWidth = this.Width;
            this.KeyPreview = true;
            ToolsPanel.BackColor = FavPanel.BackColor = onSelectedColor;
            this.BackColor= faTabStrip1.BackColor = formCloseButton1.BackColor = formMaxNormalButton1.BackColor = formMinButton1.BackColor = SbackColor;
            faTabStrip1.ForeColor = ToolsPanel.ForeColor = FavPanel.ForeColor = label2.ForeColor = Color.Black;
            this.ForeColor = Color.Blue;
            //Control.CheckForIllegalCrossThreadCalls = false;
            LoadFav_Bar_Info();
            this.ActiveControl = faTabStrip1;
            faTabStrip1.Parent = this;
           


            //清理缓存，每次打开都清
            try
            {
                //if (Directory.Exists(GetAppDir("Cache")))
                //    Directory.Delete(GetAppDir("Cache"), true);
            }
            catch
            { }


          
            textBoxXP1.Focus();
            //this.Width = PublicClass.DESKTOP.Width;
            //this.Height = PublicClass.DESKTOP.Height;
            this.WindowState = FormWindowState.Maximized;
            this.AutoScaleMode = AutoScaleMode.Dpi; //让界面布局适应各种大小不同的屏幕
            this.AutoSize = false;//.置窗体不根据内容超出而调整窗体自身大小，以免窗体超出屏幕。
                                  //AutoScroll = true;

            //this.BackColor = SystemColors.ControlDark;
            this.DownloadPanel3.Dock = DockStyle.Bottom;
            this.StartPosition = FormStartPosition.CenterScreen;
           PublicClass.RestoreRect = this.RestoreBounds;


            //检查看看有没有temp目录，没及建立他
            //if (!Directory.Exists(_directory))
            //{
            //    Directory.CreateDirectory(_directory);
            //}
            //PublicClass.CreateDefaultBrowserForWindows(); //register cefibrowser
            //PublicClass.CreateReg(); //注册默认PDF阅读器 .pdf
            //PublicClass.RegisterBrowser(); //注册默认浏览器

            //this.BackColor = Color.YellowGreen;
            //this.NewShadowEnable = true;
            //this.NewShadowColor = Color.FromArgb(255, 30, 30, 30);
            //this.NewShadowWidth = 4;
        }


        private static string GetAppDir(string name)
        {
            string winXPDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData); //@"C:\Documents and Settings\All Users\Application Data\";
            if (Directory.Exists(winXPDir))
            {
                return winXPDir + @"\" + Branding + @"\" ;
            }
            return @"C:\ProgramData\" + Branding + @"\" ;

        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            this.cefCtrlDown = false;
            addrPanel3.Visible = false;
            // cefCtrlC = false;


            if (e.KeyValue == 27)
            {
                PanelSearch.Visible = false;
                searchOpen = false;
                if (faTabStrip1.SelectedItem.splic.Panel1.Controls.Count > 0 && faTabStrip1.SelectedItem.Title != CefConstHelper.CefDownloadTitle)
                    ((CefWebBrowser)faTabStrip1.SelectedItem.splic.Panel1.Controls[0]).Browser.GetHost().StopFinding(true);

            }

            if ((faTabStrip1.SelectedItem != null))
            {
                if (faTabStrip1.SelectedItem.splic.Panel1.Controls.Count > 0 && !textBoxXP1.Focused && !searchOpen && faTabStrip1.SelectedItem.Title != CefConstHelper.CefDownloadTitle)
                    ((CefWebBrowser)faTabStrip1.SelectedItem.splic.Panel1.Controls[0]).Browser.GetHost().SetFocus(true);
            }
            if (e.KeyValue == 122)
            {
                //            bool fullForm = false;
                //            if (MainForm.Instance.WindowState == System.Windows.Forms.FormWindowState.Maximized
                //&& MainForm.Instance.ToolsPanel.Visible)
                //                fullForm = true;
                PublicClass.ScreenFuction("");//,fullForm);
            }
            base.OnKeyUp(e);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            //MouseHook.stop();
            base.OnFormClosed(e);
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (CefConstHelper.IsDownloading)
            {
                if (MessageBox.Show(this, "下载中，是否退出？，如此时强制退出会导致下载任务失败", "CefiBrowser", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    CefConstHelper.AbortDownloading = true;
                    e.Cancel = false;
                }
            }

            if (e.CloseReason == CloseReason.TaskManagerClosing)
            {
                if (faTabStrip1.Items.Count <= 1 && faTabStrip1.SelectedItem.ItemBrowser == null)
                {
                    if (DownloadPanel3.Visible == true)
                        e.Cancel = true;
                    else
                        e.Cancel = false;
                }
                else
                {


                    e.Cancel = true;
                    int itemIndex = faTabStrip1.SelectedItem.TabIndex;
                    faTabStrip1.Items.Remove(faTabStrip1.SelectedItem);
                    faTabStrip1.SelectedItem = faTabStrip1.Items[itemIndex - 1];
                    faTabStrip1.Invalidate();
                    this.Update();
                }


            }


            base.OnFormClosing(e);
        }
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            //Point ms = Control.MousePosition; //获取鼠标在屏幕的位置
            //if (ms.X <= 0)
            //    MessageBox.Show("Hited!!");
        }

        private void formCloseButton1_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void Form1_Activeated(object sender, EventArgs e)
        {
           //PublicClass. RestoreRect = this.RestoreBounds;
        }
        public void NMFuction()
        {

            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                if (this.RestoreBounds != PublicClass.RestoreRect )
                    this.DesktopBounds = PublicClass.RestoreRect;
                this.WindowState = FormWindowState.Normal;
            }

        }
        private void formMaxNormalButton1_Click(object sender, EventArgs e)
        {
            cefimousDown = false;
            NMFuction();
        }
        private void formMinButton1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        /// <summary>
        /// 重写该方法解决窗体每次还原都会变大的问题
        /// </summary>        
        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            if (base.WindowState == FormWindowState.Normal)
            {
                if (this.Size == this.ClientSize)
                {
                    //if (width == (this.Size.Width + SystemInformation.FrameBorderSize.Width * 2))
                    if (width == (this.Size.Width + 4 * 2) || width == (this.Size.Width + 8 * 2))
                    {
                        width = this.Size.Width;
                        height = this.Size.Height;
                    }
                }
            }

            base.SetBoundsCore(x, y, width, height, specified);
        }

        /// <summary>
        /// 重写该方法解决在VS设计器中，每次保存一个新的尺寸，再打开尺寸会变大的问题
        /// </summary>        
        protected override void SetClientSizeCore(int x, int y)
        {
            //MessageBox.Show("before SetClientSizeCore,size:" +
            //    base.Size.ToString() + ", clisize:" + base.ClientSize.ToString()
            //    + ", x:" + x.ToString() + ", y:" + y.ToString());

            //if (base.WindowState == FormWindowState.Normal)
            //{
            //    if (base.Size != base.ClientSize)
            //    {
            //        int diffx = Size.Width - ClientSize.Width;
            //        int diffy = Size.Height - ClientSize.Height;
            //        if (diffx == 4 * 2 || diffx == 8 * 2 || DesignMode)
            //        {
            //            x -= diffx;
            //            y -= diffy;
            //        }
            //    }
            //}
            base.SetClientSizeCore(x, y);

            //MessageBox.Show(base.SizeFromClientSize(new Size(x,y)).ToString());

            //MessageBox.Show("after SetClientSizeCore,size:" +
            //    base.Size.ToString() + ", clisize:" + base.ClientSize.ToString()
            //    + ", x:" + x.ToString() + ", y:" + y.ToString());

        }

     
        protected override void OnMove(EventArgs e)
        {
            base.OnMove(e);
            if (this.WindowState == FormWindowState.Normal)
            {
                if (PublicClass.RestoreRect == this.RestoreBounds)
                {
                    PublicClass.SetIconCloseRectWH();
                    faTabStrip1.InControlHeight = ToolsPanel.Height + FavPanel.Height;
                    this.ToolsPanel.Location = new Point(faTabStrip1.Location.X - 0, CefConstHelper.DEF_HEADER_HEIGHT + CefConstHelper.DEF_Header_TopDis);
                    this.FavPanel.Location = new Point(faTabStrip1.Location.X - 0, ToolsPanel.Bottom);
                    this.FavPanel.Width = ToolsPanel.Width = faTabStrip1.Width;
                }
            }

        }

        //protected override void OnResize(EventArgs e)
        //{
        //    base.OnResize(e);
        //    if (this.WindowState == FormWindowState.Normal)
        //    {
        //        PublicClass.SetIconCloseRectWH();
        //        faTabStrip1.InControlHeight = ToolsPanel.Height + FavPanel.Height;
        //        this.ToolsPanel.Location = new Point(faTabStrip1.Location.X - 0, CefConstHelper.DEF_HEADER_HEIGHT + CefConstHelper.DEF_Header_TopDis);
        //        this.FavPanel.Location = new Point(faTabStrip1.Location.X - 0, ToolsPanel.Bottom);
        //        this.FavPanel.Width = ToolsPanel.Width = faTabStrip1.Width;
        //    }
        //}
        //下面其实不是Form1的大小改变，而是Fatab大小改变,当鼠标拖拽的时候下面的代码会生效
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            PublicClass.SetIconCloseRectWH();
            //try
            //{
            //    if (RunQueueScreen != true && faTabStrip1 != null && faTabStrip1.Items.Count > 0 && DownloadPanel3.Visible != true)
            //        if (cefimousDown != true)
            //        {
            //            _Timer = new Timer() { Interval = 1 };
            //            _Timer.Tick += new EventHandler(Timer_Tick3);
            //            base.Opacity = 0;
            //            _Timer.Start(); //防止控件闪烁
            //        }
            //}
            //catch
            //{ }

            if (faTabStrip1.Visible == false)
            {
                faTabStrip1.Visible = true;
                faTabStrip1.Invalidate();
            }
            if (this.WindowState == FormWindowState.Normal)
            {
                if (PublicClass.RestoreRect == this.RestoreBounds)
                {
                    faTabStrip1.InControlHeight = ToolsPanel.Height + FavPanel.Height;
                    this.ToolsPanel.Location = new Point(faTabStrip1.Location.X - 0, CefConstHelper.DEF_HEADER_HEIGHT + CefConstHelper.DEF_Header_TopDis);
                    this.FavPanel.Location = new Point(faTabStrip1.Location.X - 0, ToolsPanel.Bottom);
                    this.FavPanel.Width = ToolsPanel.Width = faTabStrip1.Width;
                }
                else
                {
                    //if (PublicClass.RestoreRect == this.RestoreBounds)
                    //{
                    PublicClass.SetIconCloseRectWH();
                    faTabStrip1.InControlHeight = ToolsPanel.Height + FavPanel.Height;
                    this.ToolsPanel.Location = new Point(faTabStrip1.Location.X - 0, CefConstHelper.DEF_HEADER_HEIGHT + CefConstHelper.DEF_Header_TopDis);
                    this.FavPanel.Location = new Point(faTabStrip1.Location.X - 0, ToolsPanel.Bottom);
                    this.FavPanel.Width = ToolsPanel.Width = faTabStrip1.Width;
                    //}
                }
        }
            WinAPI.RECT rECT = new WinAPI.RECT();
            rECT.Left = this.Left;
            rECT.Top = this.Top;
            rECT.Right = this.Right;
            rECT.Bottom = this.Bottom;

            if (IsAboutToMaximize(rECT))
            {
                faTabStrip1.InControlHeight = ToolsPanel.Height + FavPanel.Height;
                if (PublicClass.DpiX < 100)
                {
                    this.ToolsPanel.Location = new Point(faTabStrip1.Location.X, CefConstHelper.DEF_HEADER_HEIGHT -1);
                }
                else if (PublicClass.DpiX >= 120)
                {
                    this.ToolsPanel.Location = new Point(faTabStrip1.Location.X, CefConstHelper.DEF_HEADER_HEIGHT);
                }

                this.FavPanel.Location = new Point(faTabStrip1.Location.X, ToolsPanel.Bottom);// 61);
                this.FavPanel.Width = ToolsPanel.Width = this.faTabStrip1.Width;

                if (PanelSearch.Visible)
                    PanelSearch.Location = new Point(this.Width - PanelSearch.Width - PanelSearch.Width / 4 - 5, FavPanel.Top + 10);
            }
            if (faTabStrip1.SelectedItem != null)
            {
                if (faTabStrip1.SelectedItem.Title == CefConstHelper.CefDownloadTitle && faTabStrip1.SelectedItem.splic.Panel1.Controls.Count > 1)
                {
                    for (int i = 0; i < faTabStrip1.SelectedItem.splic.Panel1.Controls.Count; i++)
                    {
                        if (i == 0)
                        {
                            ((mDownloadTopItemcs)faTabStrip1.SelectedItem.splic.Panel1.Controls[i]).SearchPanel1.Width = faTabStrip1.SelectedItem.Width / 3;
                            if (((mDownloadTopItemcs)faTabStrip1.SelectedItem.splic.Panel1.Controls[i]).SearchPanel1.Width < ((Jdownload)faTabStrip1.SelectedItem.splic.Panel1.Controls[i + 1]).Width / 2)
                                ((mDownloadTopItemcs)faTabStrip1.SelectedItem.splic.Panel1.Controls[i]).SearchPanel1.Width = 360;
                            ((mDownloadTopItemcs)faTabStrip1.SelectedItem.splic.Panel1.Controls[i]).SearchPanel1.Location =
                            new Point((faTabStrip1.SelectedItem.Width - ((mDownloadTopItemcs)faTabStrip1.SelectedItem.splic.Panel1.Controls[i]).SearchPanel1.Width) / 2, 6);
                        }
                        else
                        {
                            ((Jdownload)faTabStrip1.SelectedItem.splic.Panel1.Controls[i]).Location =
                            new Point((faTabStrip1.SelectedItem.Width - ((Jdownload)faTabStrip1.SelectedItem.splic.Panel1.Controls[i]).Width) / 2,
                            (((Jdownload)faTabStrip1.SelectedItem.splic.Panel1.Controls[i]).Location.Y));
                        }
                    }
                }
            }

            if (WindowState == FormWindowState.Maximized)
                formMaxNormalButton1.ImageDefault = Properties.Resources.win10maxnor;
            else if (WindowState == FormWindowState.Normal)
            {
                formMaxNormalButton1.ImageDefault = Properties.Resources.win10max;
                ReDWLine(this.CreateGraphics());
            }
        }

  
        private void Window_ControlAdded(object sender, ControlEventArgs e)
        {
            Control c = e.Control;
            c.MouseDown += Form1_MouseDown;
        }

        private int TextBox1_MouseClick_count = 0;
        public void textXP_TextSelect()
        {
            textBoxXP1.SelectAll();
        }
        private void TextBoxXP1_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            TextBox1_MouseClick_count++;
            if (TextBox1_MouseClick_count == 1)
            {
                textXP_TextSelect();
            }
        }
        private void TextBoxXP1_LostFocus(object sender, System.EventArgs e)
        {
            TextBox1_MouseClick_count = 0;
        }

        //搜索的时候特殊字符在百度不能搜索的解决方法
        public static string ReplaceStr(string str)
        {
            str = str.Replace("#", "%23");
            str = str.Replace("^", "%5E");
            str = str.Replace("&", "%26");
            str = str.Replace(":", "%3A");
            str = str.Replace("+", "%2B");
            return str;
        }
        //判断URL是否非法,URL正测表达式
        public static bool IsUrl(string str)
        {
            try
            {
                string Url = @"(([a-zA-Z0-9\._-]+\.[a-zA-Z]{2,6})|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(:[0-9]{1,4})*(/[a-zA-Z0-9\&%_\./-~-]*)?";
                return Regex.IsMatch(str, Url);
            }
            catch
            {
                return false;
            }
        }

        //不要在文框中按回车和ESC键的时候发出咚的一声
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter || keyData == Keys.Escape)
            {
                return true;
            }

            return base.ProcessDialogKey(keyData);
        }

        private void TextBoxXP1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                if (textBoxXP1.Text.Contains(@"chrome://") || textBoxXP1.Text.Contains(@"file:///") || textBoxXP1.Text.Contains(@"chrome-devtools://"))
                {
                    faTabStrip1.SelectedItem.ItemIcon = null;

                    if (faTabStrip1.SelectedItem.splic.Panel1.Controls.Count < 1)
                    {
                        faTabStrip1.SelectedItem.splic.Panel1.Controls.Add(NewChromiumWeb(textBoxXP1.Text, faTabStrip1.SelectedItem.TabIndex));
                    }
                    else
                    {
                        if (faTabStrip1.SelectedItem.ItemBrowser != null)
                        {
                            faTabStrip1.SelectedItem.ItemBrowser.GetMainFrame().LoadUrl(textBoxXP1.Text);
                        }
                        else
                        {
                            ((CefWebBrowser)faTabStrip1.SelectedItem.splic.Panel1.Controls[0]).Browser.GetMainFrame().LoadUrl(textBoxXP1.Text);
                        }
                    }
                    faTabStrip1.SelectedItem.BrowserIsLoading = false;
                }
                else
                {
                    faTabStrip1.SelectedItem.ItemIcon = null;
                    faTabStrip1.SelectedItem.BrowserIsLoading = true;

                    if (!IsUrl(textBoxXP1.Text))
                    {
                        PubSeach(faTabStrip1.SelectedItem, textBoxXP1.Text);
                    }
                    else
                    {
                        faTabStrip1.SelectedItem.BrowserIsLoading = true;
                        if (faTabStrip1.SelectedItem.splic.Panel1.Controls.Count > 0)
                        {
                            if (faTabStrip1.SelectedItem.ItemBrowser != null)
                            {
                                faTabStrip1.SelectedItem.ItemBrowser.GetMainFrame().LoadUrl(textBoxXP1.Text);
                            }
                            else
                            {
                                ((CefWebBrowser)faTabStrip1.SelectedItem.splic.Panel1.Controls[0]).Browser.GetMainFrame().LoadUrl(textBoxXP1.Text);
                            }
                        }
                        else
                        {
                            ChromLoad(textBoxXP1.Text.Trim());
                        }
                    }
                }
            }
            else
            {
                keyFuction(sender, e);
            }
        }

        //公共搜索
        public void PubSeach(FATabStripItem fATab, string seachStr)
        {
            faTabStrip1.SelectedItem.BrowserIsLoading = true;
            faTabStrip1.SelectedItem.Title = "加载中...";
            if (fATab.splic.Panel1.Controls.Count < 1)
            {
                fATab.splic.Panel1.Controls.Add(NewChromiumWeb("www.baidu.com/s?ie=UTF-8&wd=" + ReplaceStr(seachStr), fATab.TabIndex));
            }
            else
            {
                if (fATab.ItemBrowser != null)
                {
                    faTabStrip1.SelectedItem.ItemBrowser.GetMainFrame().LoadUrl("www.baidu.com/s?ie=UTF-8&wd=" + ReplaceStr(seachStr));
                }
                else
                    ((CefWebBrowser)fATab.splic.Panel1.Controls[0]).Browser.GetMainFrame().LoadUrl("www.baidu.com/s?ie=UTF-8&wd=" + ReplaceStr(seachStr));
            }
            //fATab = null;
            //seachStr = null;
            GC.Collect();
        }
        //#endregion

        #region Tab控件事件
        private void FaTabStrip1_ControlAdded(object sender, System.Windows.Forms.ControlEventArgs e)
        {
            textBoxXP1.Text = "";
            textBoxXP1.Focus();
            BtBack.Enabled = false;
            BtForward.Enabled = false;
            BtReflash.Enabled = false;
            BtnSeach.ImageDefault = Properties.Resources.shousuo;
        }
        //这里实际是引用TabPages控件的MouseDown
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            HitTestResult result = faTabStrip1.HitTest(e.Location);
            FATabStripItem item = faTabStrip1.GetTabItemByPoint(e.Location);
            if (e.Button != MouseButtons.Left)
            {
                if (result == HitTestResult.TabItem)
                {
                    if (item != null)
                    {
                        faTabStrip1.SelectedItem = item;

                    }
                    if (e.Button == MouseButtons.Right)
                    {
                        Point p = new Point();
                        p.X = e.Location.X + this.Location.X + 2;
                        p.Y = e.Location.Y + this.Location.Y + 2;
                        FaTabMenu.Show(p);

                        item = null;
                        GC.Collect();
                        return;
                    }
                }
            }
            else
            {
                if (result == HitTestResult.TabItem)
                {
                    if (item != null)
                    {
                        faTabStrip1.SelectedItem = item;
                    }

                    if (faTabStrip1.SelectedItem.splic.Panel1.Controls.Count == 0)
                    {
                        InvokeIfNeeded(() => textBoxXP1.Text = "");
                        EnableBackButton(false);
                        EnableForwardButton(false);
                        EnableReflashButton(false);
                        BtnSeach.ImageDefault = Properties.Resources.shousuo;

                    }
                    else
                    {
                        if (faTabStrip1.SelectedItem.URL == "" || faTabStrip1.SelectedItem.URL == null || faTabStrip1.SelectedItem.Title == "新标签页" || faTabStrip1.SelectedItem.Title == CefConstHelper.CefDownloadTitle)
                        {
                            textBoxXP1.Text = "";
                            EnableBackButton(false);
                            EnableForwardButton(false);
                            EnableReflashButton(false);
                            BtnSeach.ImageDefault = Properties.Resources.shousuo;

                        }
                        else
                        {
                            textBoxXP1.Text = faTabStrip1.SelectedItem.URL;// ((CefWebBrowser)faTabStrip1.SelectedItem.splic.Panel1.Controls[0]).Address;
                            EnableBackButton(((CefWebBrowser)faTabStrip1.SelectedItem.splic.Panel1.Controls[0]).Browser.CanGoBack);
                            EnableForwardButton(((CefWebBrowser)faTabStrip1.SelectedItem.splic.Panel1.Controls[0]).Browser.CanGoForward);
                            EnableReflashButton(true);
                            ((CefWebBrowser)faTabStrip1.SelectedItem.splic.Panel1.Controls[0]).Browser.GetHost().SetFocus(true);
                            if (faTabStrip1.SelectedItem.URL.Contains("https"))
                            {
                                BtnSeach.ImageDefault = Properties.Resources.sslkey1;
                            }
                            else if (faTabStrip1.SelectedItem.URL.Contains("http"))
                            {
                                BtnSeach.ImageDefault = Properties.Resources.info1;
                            }

                        }
                        if (faTabStrip1.SelectedItem.Title == CefConstHelper.CefDownloadTitle)
                        {
                            textBoxXP1.Text = CefConstHelper.CefiBrowserInternalDownloadUrl;
                            BtnSeach.ImageDefault = Properties.Resources.info1;
                        }
                    }
                }
            }
            if (e.Clicks == 1)
            {
                ToolsPanel.Visible = true;
                FavPanel.Visible = true;
                FATabStrip.ParentMouseMrafting = false;
                //执行这个是判断是否能拖拽
                this.faTabStrip1.FATabMouseDown(e, "1");

                //如果鼠标区域不在Tab标签的Rect范围就可以拖拽，否则不行
                if (FATabStrip.ParentMouseMrafting)
                {
                    cefimousDown = false;
                    //X = this.Location.X;
                    //Y = this.Location.Y;
                    // faTabStrip1.SelectedItem.BrowserIsLoading = false;
                    faTabStrip1.Visible = true;
                    PublicClass.ReleaseCapture();
                   PublicClass.SendMessage(Handle, PublicClass.WM_NCLBUTTONDOWN, PublicClass.HT_CAPTION, 0);

                }
            }
            if (e.Clicks == 2)
            {
                ToolsPanel.Visible = true;
                FavPanel.Visible = true;
                FATabStrip.ParentMouseMrafting = false;
                //执行这个是判断是否能拖拽
                this.faTabStrip1.FATabMouseDown(e, "1");

                //如果鼠标区域不在Tab标签的Rect范围就可以拖拽，否则不行
                if (FATabStrip.ParentMouseMrafting)
                {
                    cefimousDown = false;
                    faTabStrip1.Visible = true;
                    //X = this.Location.X;
                    //Y = this.Location.Y;
                   // faTabStrip1.SelectedItem.BrowserIsLoading = false;
                    PublicClass.ReleaseCapture();
                    PublicClass.SendMessage(Handle, PublicClass.WM_NCLBUTTONDOWN, PublicClass.HT_CAPTION, 0);
                }
                FATabStrip.ParentMouseMrafting = false;
                if (faTabStrip1.newAddBT.Bounds.Contains(e.Location) != true && faTabStrip1.SelectedItem.ItemBounds.Contains(e.Location) != true)
                {
                    NMFuction();
                }
            }
            item = null;
            GC.Collect();
        }

        private void FaTabStrip1_TabStripItemClosed(object sender, System.EventArgs e)
        {
            textBoxXP1.Text = faTabStrip1.SelectedItem.URL;

        }

        private void FaTabStrip1_ControlRemoved(object sender, System.Windows.Forms.ControlEventArgs e)
        {
            faTabStrip1.CalcNewRectXY("RemoveItem");

        }


        public void keyFuction(object sender, KeyEventArgs e)
        {
            //对按键的处理
            switch (e.KeyValue)
            {

                case 68: //alt+D 时选中地址栏中的内容
                    if (e.Alt)
                    {
                        textBoxXP1.Focus();
                        textBoxXP1.SelectAll();
                    }
                    break;
                case 123://功能键 F12 的KeyCode
                    if (faTabStrip1.SelectedItem.splic.Panel1.Controls.Count > 0 && faTabStrip1.SelectedItem.Title != CefConstHelper.CefDownloadTitle)
                    {
                        CefKeyEvent cefKey = new CefKeyEvent();
                        cefKey.WindowsKeyCode = e.KeyValue;
                        if (faTabStrip1.SelectedItem.ItemBrowser != null)
                            faTabStrip1.SelectedItem.ItemBrowser.GetHost().SendKeyEvent(cefKey);
                        else
                            ((CefWebBrowser)faTabStrip1.SelectedItem.splic.Panel1.Controls[0]).Browser.GetHost().SendKeyEvent(cefKey);
                        cefKey = null;
                    }
                    break;

                case 27:
                    searchOpen = false;
                    CloseSearch();
                    PanelSearch.Visible = false;
                    break;
                case 116: //F5刷新功能

                    if (faTabStrip1.SelectedItem.splic.Panel1.Controls.Count > 0)
                    {
                        if (((CefWebBrowser)(faTabStrip1.SelectedItem.splic.Panel1.Controls[0])).Browser.IsLoading)
                            ((CefWebBrowser)(faTabStrip1.SelectedItem.splic.Panel1.Controls[0])).Browser.StopLoad();

                        ((CefWebBrowser)(faTabStrip1.SelectedItem.splic.Panel1.Controls[0])).Browser.Reload();
                    }

                    break;
            }
        }
        private void FaTabStrip1_KeyUp(object sender, KeyEventArgs e)
        {
            keyFuction(sender, e);
        }


        #endregion

        private void BtHome_Clinck(object sender, EventArgs e)
        {
            ChromLoad(CefConstHelper.HomeUrl);
        }

        //后退功能
        private void BtBack_Click(object sender, EventArgs e)
        {
            if (faTabStrip1.SelectedItem.splic.Panel1.Controls.Count > 0)
            {

                faTabStrip1.SelectedItem.BrowserIsLoading = true;
                if (faTabStrip1.SelectedItem.ItemBrowser != null)
                    faTabStrip1.SelectedItem.ItemBrowser.GoBack();
                else
                {
                    try
                    {
                        ((CefWebBrowser)faTabStrip1.SelectedItem.splic.Panel1.Controls[0]).Browser.GoBack();
                    }
                    catch
                    {
                        BtBack.Enabled = false;
                    }
                }

            }
        }

        private void BtForward_ButtonClink(object sender, EventArgs e)
        {
            if (faTabStrip1.SelectedItem.splic.Panel1.Controls.Count > 0)
            {

                faTabStrip1.SelectedItem.BrowserIsLoading = true;
                if (faTabStrip1.SelectedItem.ItemBrowser != null)
                    faTabStrip1.SelectedItem.ItemBrowser.GoForward();
                else
                {
                    try
                    {
                        ((CefWebBrowser)faTabStrip1.SelectedItem.splic.Panel1.Controls[0]).Browser.GoForward();
                    }
                    catch
                    {
                        BtForward.Enabled = false;
                    }
                }
            }
        }
        //刷新功能
        private void BtReflash_Click(object sender, EventArgs e)
        {
            RefreshActiveTab();
        }

        private void FaTabMenu_LostFocus(object sender, System.EventArgs e)
        {
            this.FaTabMenu.Visible = false;
        }
        private void Favri_BarMenu_LostFocus(object sender, System.EventArgs e)
        {
            this.Favri_BarMenu.Visible = false;
        }

        //多用户缓存隔离功能
        private void BtnMUserSupport_ButtonClick(object sender, System.EventArgs e)
        {
            FATabStripItem fATab = new FATabStripItem();
            fATab.MuserSupport = true;
            faTabStrip1.InsetTab(fATab, true, faTabStrip1.SelectedItem.TabIndex + 1);
            faTabStrip1.SelectedItem = fATab;
            EnableBackButton(false);
            EnableForwardButton(false);
            EnableReflashButton(false); //新建一个空的标签页
            fATab = null;
            GC.Collect();
        }


        #region Fatab右键菜单新增新标签页功能
        private void NewTabItem_Click(object sender, EventArgs e)
        {
            FATabStripItem fATab = new FATabStripItem();
            faTabStrip1.InsetTab(fATab, true, faTabStrip1.SelectedItem.TabIndex + 1);
            faTabStrip1.SelectedItem = fATab;
            EnableBackButton(false);
            EnableForwardButton(false);
            EnableReflashButton(false); //新建一个空的标签页
            fATab = null;
            GC.Collect();
        }

        private void CloseTabItem_Click(object sender, EventArgs e)
        {
            // faTabStrip1.RemoveALLItem = true;
            if (faTabStrip1.Items.Count == 1)
            {
                Application.Exit();
            }
            if (faTabStrip1.Items.Count > 1)
            {
                int currentindex = faTabStrip1.SelectedItem.TabIndex;
                int itemindex = currentindex;
                faTabStrip1.Items.Remove(faTabStrip1.Items[currentindex]); //关闭当前
                itemindex--;
                if (itemindex < 1)
                {
                    itemindex = 0;
                    faTabStrip1.SelectedItem = faTabStrip1.Items[itemindex];
                }
                else
                {
                    faTabStrip1.SelectedItem = faTabStrip1.Items[itemindex--];
                }
            }
            // faTabStrip1.RemoveALLItem = false;
        }
        //关闭其他TabItem
        private void CloseAtherTabItem_Click(object sender, EventArgs e)
        {
            //先关闭右边的
            faTabStrip1.RemoveALLItem = true;
            for (int i = faTabStrip1.Items.Count - 1; i > faTabStrip1.SelectedItem.TabIndex; i--)
            {
                if (faTabStrip1.Items[i].TabIndex != faTabStrip1.SelectedItem.TabIndex)
                    faTabStrip1.Items.Remove(faTabStrip1.Items[i]);
            }
            //再关闭左边的
            for (int i = 0; i < faTabStrip1.SelectedItem.TabIndex; i++)
            {
                faTabStrip1.Items.Remove(faTabStrip1.Items[0]);
            }

            faTabStrip1.RemoveALLItem = false;
            faTabStrip1.Invalidate();
        }

        private void Event(object sender, MouseEventArgs e)
        {
            //判断是否已安装带滚轮的鼠标  
            //SystemInformation.MouseWheelPresent.ToString();  
            //获取鼠标滚轮在滚动时所获得的行数  
            //SystemInformation.MouseWheelScrollLines.ToString();  
            //判断该操作系统是否支持滚轮鼠标  
            //SystemInformation.NativeMouseWheelSupport.ToString();   
            // textBoxXP1.Text = PointToClient(e.Location).ToString() + MousePosition.ToString();
            try
            {
                if (PointToClient(e.Location).X < 0 || PointToClient(e.Location).Y < 0 ||
                    PointToClient(e.Location).X > this.Width || PointToClient(e.Location).Y > this.Height) //如果鼠标不在当前窗口的时候发生
                {
                    cefCtrlDown = false;
                    addrPanel3.Visible = false;

                }

                if (cefCtrlDown && e.Button == MouseButtons.Middle)
                {
                    // textBoxXP1.Text = PointToClient(e.Location).ToString();
                    textBox2.ForeColor = Color.Black;
                    addrPanel3.Visible = true;
                    addrPanel3.Location = new Point(this.Width / 2 - addrPanel3.Width / 2, this.faTabStrip1.SelectedItem.Top);
                    addrPanel3.Focus();
                    CefConstHelper.mouseNumber += e.Delta / 100;
                    textBox2.Text = (100 + CefConstHelper.mouseNumber * 10 * 2 + CefConstHelper.mouseNumber * 10).ToString() + @"%";
                    // faTabStrip1.SelectedItem.Title = e.Delta.ToString();
                    // faTabStrip1.SelectedItem.Title = mouseNumber.ToString();
                    //变通的方法实现放大和缩小浏览器显示
                    //首先检查当前是否打开了浏览器
                    if (faTabStrip1.SelectedItem.splic.Panel1.Controls.Count > 0 && addrPanel3.Visible == true)
                    {
                        // faTabStrip1.SelectedItem.Title = mouseNumber.ToString();
                        ((CefWebBrowser)faTabStrip1.SelectedItem.splic.Panel1.Controls[0]).Browser.GetHost().SetZoomLevel(CefConstHelper.mouseNumber);// 0.063f)
                    }
                }
                //else if (e.Button == MouseButtons.Right)
                //{
                //    if (faTabStrip1.SelectedItem.splic.Panel1.Controls.Count > 0 )
                //    {
                //        faTabStrip1.SelectedItem.splic.Panel1.Invalidate();
                //        faTabStrip1.SelectedItem.Title = "Good";
                //    }
                //}
            }
            catch
            { }

        }

        private void CloseRightTabItem_Click(object sender, EventArgs e)
        {
            //关闭右边的
            faTabStrip1.RemoveALLItem = true;
            for (int i = faTabStrip1.Items.Count - 1; i > faTabStrip1.SelectedItem.TabIndex; i--)
            {
                if (faTabStrip1.Items[i].TabIndex != faTabStrip1.SelectedItem.TabIndex)
                {
                    faTabStrip1.Items.Remove(faTabStrip1.Items[i]);
                }
            }
            faTabStrip1.RemoveALLItem = false;
            faTabStrip1.Invalidate();
        }

        private void CloseLeftabItem_Click(object sender, EventArgs e)
        {
            faTabStrip1.RemoveALLItem = true;
            //关闭左边的
            for (int i = 0; i < faTabStrip1.SelectedItem.TabIndex; i++)
            {
                faTabStrip1.Items.Remove(faTabStrip1.Items[0]);
            }

            faTabStrip1.RemoveALLItem = false;
             faTabStrip1.Invalidate();
        }

        #endregion

        #region 收藏夹菜单功能

        //收藏夹功能
        private void BtFav_ButtonClick(object sender, EventArgs e)
        {
            if (faTabStrip1.SelectedItem.splic.Panel1.Controls.Count < 1 || faTabStrip1.SelectedItem.ItemIcon == null)
            {
                MessageBox.Show(this, "对不起，无法添加收藏", "CefiBrowser", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            FaviFrm faviFrm = new FaviFrm();
            faviFrm.URL = ((CefWebBrowser)faTabStrip1.SelectedItem.splic.Panel1.Controls[0]).Address;
            faviFrm.Title = ((CefWebBrowser)faTabStrip1.SelectedItem.splic.Panel1.Controls[0]).Title;
            faviFrm.iconImage = faTabStrip1.SelectedItem.ItemIcon;
            faviFrm.Show();
        }
        private void FMenuStrip1_LostFocus(object sender, System.EventArgs e)
        {
            fMenuStrip1.Visible = false;
        }
        private void FavPanel_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point p = Control.MousePosition;
                CopyFavriBtURL.Visible = false;
                DelCurrentFavriBT.Visible = false;
                OpenRULInNewTab.Visible = false;
                Favi_3_Line.Visible = false;
                Favi_Modif_Item.Visible = false;
                Favri_BarMenu.Show(p);
            }
        }

        private void Edit_FavriBT_Click(object sender, EventArgs e)
        {
            FaviFrm faviFrm = new FaviFrm();
            faviFrm.Show();
        }

        public void RefreshActiveTab()
        {
            InvokeIfNeeded(() => faTabStrip1.SelectedItem.BrowserIsLoading = true);
            InvokeIfNeeded(() => faTabStrip1.SelectedItem.Title = "加载中...");
            if (faTabStrip1.SelectedItem.ItemBrowser != null)
                faTabStrip1.SelectedItem.ItemBrowser.Reload();
            else
            {
                InvokeIfNeeded(() => ((CefWebBrowser)faTabStrip1.SelectedItem.splic.Panel1.Controls[0]).Browser.StopLoad());
                InvokeIfNeeded(() => ((CefWebBrowser)faTabStrip1.SelectedItem.splic.Panel1.Controls[0]).Browser.GetMainFrame().LoadUrl(faTabStrip1.SelectedItem.URL.Trim()));
                textBoxXP1.Text = faTabStrip1.SelectedItem.URL.Trim();
            }
            faTabStrip1.Invalidate();
            GC.Collect();

        }
        /// <summary>
        /// 打开收藏夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditFaviFolder_Click(object sender, EventArgs e)
        {
            FaviFrm faviFrm = new FaviFrm();
            faviFrm.Show();
        }

        //快捷收藏Item在新TabItem中打开
        private void OpenRULInNewTab_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < FavPanel.Controls.Count; i++)
            {
                if (((FavirteButton)FavPanel.Controls[i]).IsSelect == true)
                {
                    AddNewBrowserTab(((FavirteButton)FavPanel.Controls[i]).URL);
                    ((FavirteButton)FavPanel.Controls[i]).IsSelect = false;
                    return;
                }
            }
        }
        //复制所在快捷Item的URL到系统剪贴板
        private void CopyFavriBtURL_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < FavPanel.Controls.Count; i++)
            {
                if (((FavirteButton)FavPanel.Controls[i]).IsSelect == true)
                {
                    Clipboard.SetDataObject(((FavirteButton)FavPanel.Controls[i]).URL);
                    ((FavirteButton)FavPanel.Controls[i]).IsSelect = false;
                    return;
                }
            }
        }
        //删除收藏的快捷Item
        private void DelCurrentFavriBT_Click(object sender, EventArgs e)
        {
            Bookmarks bookmarks = new Bookmarks();
            for (int i = 0; i < FavPanel.Controls.Count; i++)
            {
                if (((FavirteButton)FavPanel.Controls[i]).Date_Added == Date_Added)
                {
                    //这里是删除功能
                    if (bookmarks.DelBookmarks((FavirteButton)FavPanel.Controls[i]))
                    {
                        Date_Added = "";
                        FavPanel.Controls.Clear();
                    }
                    break;
                }
            }
            LoadFav_Bar_Info(); //删除后重新加载，哈哈，有点傻傻的
        }

        //添加快捷链接文件夹
        private void AddFavri_Folder_Click(object sender, EventArgs e)
        {
            FavirteButton favirte = new FavirteButton();
            //if (FavPanel.Controls.Count > 0)
            //{
            //    for (int i = 0; i < FavPanel.Controls.Count; i++)
            //    {
            //        if (((FavirteButton)FavPanel.Controls[i]).URL == favirte.URL ||
            //         ((FavirteButton)FavPanel.Controls[i]).Title == favirte.Title)
            //        {
            //            favirte = null;
            //            GC.Collect();
            //            return;
            //        }
            //    }
            //}

            favirte.Height = 23;
            favirte.Title = "快捷收藏夹";
            // favirte.URL = faTabStrip1.SelectedItem.URL;
            favirte.ItemIcon = CefiBrowser.Properties.Resources.FileFolderIcon;

            Font sf = new Font("Tahoma", 8.25f, FontStyle.Regular);
            Graphics g = this.CreateGraphics();
            //单位为mm
            g.PageUnit = GraphicsUnit.Millimeter;
            //测量字符串长度
            Size sif = TextRenderer.MeasureText(g, favirte.Title, sf, new Size(0, 0), TextFormatFlags.NoPadding);

            favirte.TitleWidth = sif.Width;
            if (favirte.Height - sif.Height < 6)
                favirte.Height = sif.Height + 8;

            favirte.Width = 26 + favirte.TitleWidth;

            if (favirte.Width > 140)
                favirte.Width = 140;

            favirte.Left = FavPanel.Left + 6;
            if (FavPanel.Controls.Count > 0)
            {
                int nFavPoint = 0;
                nFavPoint = FavPanel.Left + 6;
                for (int i = 0; i < FavPanel.Controls.Count; i++)
                {
                    //if (((FavirteButton)FavPanel.Controls[i]).URL == "" ||
                    // ((FavirteButton)FavPanel.Controls[i]).Title == favirte.Title)
                    //    return;

                    nFavPoint = FavPanel.Controls[i].Width + 2 + nFavPoint;

                }
                favirte.Left = nFavPoint;

            }

            FavPanel.Controls.Add(favirte);
            Bookmarks bookmarks = new Bookmarks();
            favirte.Date_Added = DateTime.Now.ToString("yyyyMMddHHmmssffff");
            favirte.Last_Visited = DateTime.Now.ToString("yyyyMMddHHmmssffff");
            favirte.Layer = "0";
            favirte.ID = DateTime.Now.ToString("yyyyMMddHHmmssffff");
            favirte.FatherID = "0";
            favirte.Type = "FaviFolder";
            favirte.URL = "";
            favirte.IconBase64str = PublicClass.IamgeToBase64(CefiBrowser.Properties.Resources.FileFolderIcon);

            bookmarks.WriteBookmarks(favirte);

            favirte = null;
            GC.Collect();

        }
        private void TwoOpenNewTableitem_Click(object sender, EventArgs e)
        {
            FATabStripItem fATab = new FATabStripItem();
            faTabStrip1.InsetTab(fATab, true, faTabStrip1.SelectedItem.TabIndex + 1);
            faTabStrip1.SelectedItem = fATab;
            EnableBackButton(false);
            EnableForwardButton(false);
            EnableReflashButton(false); //新建一个空的标签页
            fATab = null;
            GC.Collect();
        }
        #endregion

        #region  公共功能
        //程序启动时加载收藏夹上的信息
        public void LoadFav_Bar_Info()
        {

            
            string jsonStr = File.ReadAllText(PublicClass.currDirectiory + @"\UserData\Bookmarks");
            Font sf = new Font("Tahoma", 8.25f, FontStyle.Regular);
            Graphics g = this.CreateGraphics();
            FavirteButton favirte;// = new FavirteButton();
          
            try
            {
                var jobInfoList = JsonConvert.DeserializeObject<List<FavireBT>>(jsonStr);
                if (jobInfoList.Count > 0)
                {
                    favireBTs = jobInfoList;
                    foreach (FavireBT jobInfo in favireBTs)
                    {
                        if (jobInfo.Layer == "0")
                        {
                            favirte = new FavirteButton();
                            favirte.Height = 23;
                            favirte.Title = jobInfo.Title;
                            favirte.URL = jobInfo.URL;
                            favirte.ItemIcon = PublicClass.Base64ToImage(jobInfo.IconBase64str);
                            favirte.Date_Added = jobInfo.Date_Added;
                            favirte.Last_Visited = jobInfo.Last_Visited;
                            favirte.Layer = jobInfo.Layer;
                            favirte.Type = jobInfo.Type;
                            favirte.ID = jobInfo.ID;
                            favirte.FatherID = jobInfo.FatherID;

                            //单位为mm
                            g.PageUnit = GraphicsUnit.Millimeter;
                            //测量字符串长度
                            Size sif = TextRenderer.MeasureText(g, favirte.Title, sf, new Size(0, 0), TextFormatFlags.NoPadding);

                            favirte.TitleWidth = sif.Width;
                            if (PublicClass.DpiX >= 120)
                            {
                                favirte.Width = 32 + favirte.TitleWidth;
                                favirte.Height = favirte.Height + CefConstHelper.TextSizeH / 4;
                            }
                            else
                                favirte.Width = 26 + favirte.TitleWidth;

                            //if(sif.Height>favirte.Height)
                            

                            if (favirte.Width > 140)
                                favirte.Width = 140;

                            favirte.Left = FavPanel.Left + 6;
                            if (FavPanel.Controls.Count > 0)
                            {
                                int nFavPoint = 0;
                                nFavPoint = FavPanel.Left + 6;
                                for (int i = 0; i < FavPanel.Controls.Count; i++)
                                {
                                    nFavPoint = FavPanel.Controls[i].Width + 2 + nFavPoint;
                                }
                                favirte.Left = nFavPoint;

                            }
                            favirte.Location = new Point(favirte.Location.X, (FavPanel.Height - favirte.Height) / 2-1);
                            FavPanel.Controls.Add(favirte);
                            mFavList.Add(favirte);
                            // favirte = null;
                            GC.Collect();
                        }
                    }
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            //favirte = null;
            //currDirectiory = null;
            jsonStr = null;
            sf = null;
            g = null;
            favireBTs = null;
            GC.Collect();
        }

        #endregion

        #region CefWebBrowser 各种功能

        public void EnableBackButton(bool canGoBack)
        {
            this.Invoke(new Action(() =>
            {
                BtBack.Enabled = canGoBack;
            }
              ));

        }
        public void EnableForwardButton(bool canGoForward)
        {
            this.Invoke(new Action(() =>
            {
                BtForward.Enabled = canGoForward;
            }
            ));
        }

        public void EnableReflashButton(bool canReflash)
        {
            this.Invoke(new Action(() =>
            {
                BtReflash.Enabled = canReflash;
            }
            ));
        }

        public void InvokeIfNeeded(Action action)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(action);
            }
            else
            {
                action.Invoke();
            }
        }

        public void ChromLoad(string cUrl)
        {
            if (faTabStrip1.SelectedItem.splic.Panel1.Controls.Count > 0)
            {
                if (faTabStrip1.SelectedItem.ItemBrowser != null)
                    faTabStrip1.SelectedItem.ItemBrowser.GetMainFrame().LoadUrl(cUrl);
                else
                {
                    if (faTabStrip1.SelectedItem.Title == CefConstHelper.CefDownloadTitle)
                    {
                        faTabStrip1.SelectedItem.splic.Panel1.Controls.Clear();
                        faTabStrip1.SelectedItem.splic.Panel1.Controls.Add(NewChromiumWeb(cUrl, faTabStrip1.SelectedItem.TabIndex));
                    }
                    else
                    ((CefWebBrowser)faTabStrip1.SelectedItem.splic.Panel1.Controls[0]).Browser.GetMainFrame().LoadUrl(cUrl);
                }
            }
            else
            {
                faTabStrip1.SelectedItem.splic.Panel1.Controls.Add(NewChromiumWeb(cUrl, faTabStrip1.SelectedItem.TabIndex));

                ((CefWebBrowser)faTabStrip1.SelectedItem.splic.Panel1.Controls[0]).TabIndex = faTabStrip1.SelectedItem.TabIndex;
            }
            PublicClass.BrowserLoadingInfo(faTabStrip1.SelectedItem);
            GC.Collect();
        }
        //搜索按钮功能
        private void BtnSeach_ButtonClick(object sender, System.EventArgs e)
        {
            if (!IsUrl(textBoxXP1.Text))
            {
                if (faTabStrip1.SelectedItem.splic.Panel1.Controls.Count < 1)
                {
                    MainForm.Instance.faTabStrip1.SelectedItem.splic.Panel1.Controls.Add(NewChromiumWeb("www.baidu.com/#ie=" + "{" + "inputEncoding}&wd=" + ReplaceStr(textBoxXP1.Text), faTabStrip1.SelectedItem.TabIndex));
                }
                else
                {
                    ((CefWebBrowser)faTabStrip1.SelectedItem.splic.Panel1.Controls[0]).Browser.GetMainFrame().LoadUrl("www.baidu.com/#ie=" + "{" + "inputEncoding}&wd=" + ReplaceStr(textBoxXP1.Text));
                }
            }
        }

        #region  CefWebBrowser功能
        //增加新的TabItem
        public void AddNewBrowserTab(string url)
        {
            //if (url != @"about:blank")
            if (!IsUrl(url))
                return;

            FATabStripItem tabStrip = new FATabStripItem();
            //if (url == "CefiBrowser://storage/downloads.html")
            //{
            //    tabStrip.Title = "下载标签";
            //}
            PublicClass.BrowserLoadingInfo(tabStrip);
            faTabStrip1.InsetTab(tabStrip, true, faTabStrip1.SelectedItem.TabIndex + 1);
            textBoxXP1.Text= tabStrip.URL = url;

            tabStrip.splic.Panel1.Controls.Add(NewChromiumWeb(url, faTabStrip1.SelectedItem.TabIndex));
            tabStrip.splic.Panel1.Controls[0].TabIndex = tabStrip.TabIndex = faTabStrip1.SelectedItem.TabIndex ;

          

            tabStrip = null;
            GC.Collect();
        }

        public CefWebBrowser NewChromiumWeb(string url, int tabIndex)
        {
            TextBox1_MouseClick_count = 0; //重置地址栏选择状态为初始状态
            brIndex++;
            EnableReflashButton(false);
            CefWebBrowser chromiumWeb = new CefWebBrowser();
            chromiumWeb.TabIndex = tabIndex;
            chromiumWeb.StartUrl = url;
            chromiumWeb.Dock = DockStyle.Fill;
            faTabStrip1.Items[tabIndex].URL= this.textBoxXP1.Text = url;
            if (this.faTabStrip1.SelectedItem.MuserSupport == true)
                chromiumWeb.multiUsersMode = true; //启用多用户支持
            else
                chromiumWeb.multiUsersMode = false;
           
            chromiumWeb.BrowserCreated += ChromiumWeb_BrowserCreated;
            chromiumWeb.AddressChanged += ChromiumWeb_AddressChanged;
            chromiumWeb.TitleChanged += ChromiumWeb_TitleChanged;
            chromiumWeb.StatusMessage += ChromiumWeb_StatusMessage;
            chromiumWeb.BeforePopup += ChromiumWeb_BeforePopup;
            chromiumWeb.LoadEnd += ChromiumWeb_LoadEnd;
            chromiumWeb.LoadStarted += ChromiumWeb_LoadStarted;
            chromiumWeb.LoadError += ChromiumWeb_LoadError;
           
            return chromiumWeb;
        }

        private void ChromiumWeb_LoadError(object sender, LoadErrorEventArgs e)
        {

            // MessageBox.Show(e.ErrorText);
            //throw new NotImplementedException();
            //for (int i = 0; i < faTabStrip1.Items.Count; i++)
            //{
            //    if (faTabStrip1.Items[i].TabIndex == ((CefWebBrowser)sender).TabIndex)
            //    {
            //        faTabStrip1.Items[i].Title = e.ErrorText;
            //        //faTabStrip1.Items[i].ItemIcon = Resources.blank;
            //        //((CefWebBrowser)faTabStrip1.Items[i].Controls[0]).Browser.StopLoad();
            //        break;
            //    }
            //}
        }
        private void ChromiumWeb_BrowserCreated(object sender, EventArgs e)
        {
            
            for (int i = 0; i < faTabStrip1.Items.Count; i++)
            {
                if (((CefWebBrowser)sender).TabIndex == faTabStrip1.Items[i].TabIndex)
                {
                    faTabStrip1.Items[i].ItemBrowser = ((CefWebBrowser)sender)._browser;
                    faTabStrip1.Items[i].StrartTime = DateTime.Now;
                }
            }

        }
        //加载开始
        private void ChromiumWeb_LoadStarted(object sender, LoadStartEventArgs e)
        {
            
            //var global = e.Frame.V8Context.GetGlobal();
            //var extent = CefV8Value.CreateObject(null);
            //global.SetValue("V8", extent, CefV8PropertyAttribute.None);

        }

        //加载完成
        private void ChromiumWeb_LoadEnd(object sender, LoadEndEventArgs e)
        {
            //加载完成，如果节省内存模式开启，那就将进程大部内存占用移动虚拟内存中，少占用物理内存
            for (int i = 0; i < faTabStrip1.Items.Count; i++)
            {
                if (((CefWebBrowser)sender).TabIndex == faTabStrip1.Items[i].TabIndex )
                {
                    if (!faTabStrip1.Items[i].IsPoPWindow)
                      faTabStrip1.Items[i].URL = ((CefWebBrowser)sender).Address;

                    if(faTabStrip1.Items[i] == faTabStrip1.SelectedItem)
                    {
                        EnableBackButton(e.Frame.Browser.CanGoBack);
                        EnableForwardButton(e.Frame.Browser.CanGoForward);
                        EnableReflashButton(true);
                    }

                    if (MemSetting.Checked&& faTabStrip1.Items[i].MemCostLower!=true)
                    {
                        Process[] allProcess = Process.GetProcesses();
                        foreach (Process p in allProcess)
                        {
                            if (p.ProcessName.ToLower() + ".exe" == CefConstHelper.CefiBrowserName.ToLower())
                            {
                                if (p.StartTime.ToString().ToLower() == faTabStrip1.Items[i].StrartTime.ToString().ToLower())
                                {
                                    IntPtr intPtrCef = p.Handle;
                                    PublicClass.SetProcessWorkingSetSize(intPtrCef, -1, -1);
                                    faTabStrip1.Items[i].MemCostLower = true;
                                    // break;
                                }
                            }
                        }
                       // break;
                    }
                }
            }

        }
        private void ChromiumWeb_BeforePopup(object sender, BeforePopupEventArgs e)
        {
            //也不知道对不对，这样暂时可以应对一些网站个别弹窗的问题
            if (e.TargetFrameName != null && e.TargetUrl == "about:blank") 
            {
                e.Handled = false;
                AddNewBrowserTab(e.TargetUrl);
            }
            else
            {

                if (e.WindowInfo.Height > 0 && e.WindowInfo.Width > 0 )
                {
                    e.Handled = false;
                    faTabStrip1.SelectedItem.IsPoPWindow = true;
                    e.WindowInfo.SetAsPopup(e.WindowInfo.Handle,e.TargetUrl);
                }
                else
                {
                    e.Handled = true;
                    AddNewBrowserTab(e.TargetUrl);
                }
            }

        }
     
        private void ChromiumWeb_StatusMessage(object sender, StatusMessageEventArgs e)
        {
            
            // int l = 0;
            // throw new NotImplementedException();
            //MessageBox.Show("good");
        }
        private void ChromiumWeb_AddressChanged(object sender, AddressChangedEventArgs e)
        {

            for (int i = 0; i < faTabStrip1.Items.Count; i++)
            {
                if (e.CefWeb.TabIndex == faTabStrip1.Items[i].TabIndex)
                {
                   
                    if (faTabStrip1.Items[i] == faTabStrip1.SelectedItem )//&& !faTabStrip1.Items[i].IsPoPWindow)
                    {
                        
                        if (e.Address.Contains(@"chrome-devtools://devtools") != true)
                        {
                            if (!faTabStrip1.Items[i].IsPoPWindow)
                                faTabStrip1.Items[i].URL = textBoxXP1.Text = e.Address;

                            if (e.Address.Contains("https"))
                            {
                                BtnSeach.ImageDefault = Properties.Resources.sslkey1;
                            }
                            else if(e.Address.Contains("http"))
                            {
                                BtnSeach.ImageDefault = Properties.Resources.info1;
                            }
                            else
                            {
                                BtnSeach.ImageDefault = Properties.Resources.shousuo;
                            }
                        }
                    }
                }
            }
        }
        //获取网站Titile
        private void ChromiumWeb_TitleChanged(object sender, TitleChangedEventArgs e)
        {
            if (e.Title != null)
            {
                for (int i = 0; i < faTabStrip1.Items.Count; i++)
                {
                    if (e.CefWeb.TabIndex == faTabStrip1.Items[i].TabIndex) 
                    {
                        if (e.Title.Contains(@"chrome-devtools://devtools") != true && e.Title != CefConstHelper.CefDownloadTitle
                           )// &&  !faTabStrip1.Items[i].IsPoPWindow)
                        {
                            faTabStrip1.Items[i].Title = e.Title;
                            EnableBackButton(e.CefWeb.Browser.CanGoBack);
                            EnableForwardButton(e.CefWeb.Browser.CanGoForward);
                            EnableReflashButton(true);
                        }
                    }
                    if (faTabStrip1.Items[i].splic.Panel1.Controls.Count > 0)
                    {
                        if (faTabStrip1.Items[i].Title != CefConstHelper.CefDownloadTitle && e.Title.Contains(@"chrome-devtools://devtools") != true && !faTabStrip1.Items[i].IsPoPWindow)
                        {
                            faTabStrip1.Items[i].TabIndex = faTabStrip1.Items[i].splic.Panel1.Controls[0].TabIndex = i;
                        }
                        else
                        {
                            faTabStrip1.Items[i].TabIndex = i;
                        }
                    }
                }
            }
        }

        #endregion


        #endregion

        #region Win-Message handler方法
        protected override void WndProc(ref Message m)
        {
            bool alreadyHandled = false;

            switch (m.Msg)
            {
                case 528: //解决CefWebBrowser不能获取鼠标事件
                    cefimousDown = false; //在Form内区域鼠标按下
                    fMenuStrip1.Visible = false;
                    Favri_BarMenu.Visible = false;
                    Settings_Menu.Visible = false;
                    break;

                case (int)WinAPI.WindowMessages.WM_NCCALCSIZE:
                    cefimousDown = true;//在Form边框，准备绽放Form大小时鼠标按下
                    alreadyHandled = WmNcCalcSize(ref m);
                    break;

                case (int)WinAPI.WindowMessages.WM_NCHITTEST:
                    cefimousDown = true;//在Form边框，准备绽放Form大小时鼠标按下
                    alreadyHandled = WmNcHitTest(ref m);
                    break;

                //case (int)WinAPI.WindowMessages.WM_NCACTIVATE:
                //    alreadyHandled = WmNcActivate(ref m);
                //    break;

                //case (int)WinAPI.WindowMessages.WM_NCPAINT:
                //    alreadyHandled = true;
                //    break;

                default:
                    break;
            }

            if (alreadyHandled != true)
                base.WndProc(ref m);
        }

        int SideResizeWidth = 10;
        internal Rectangle TopLeftRect
        {
            get
            {
                return new Rectangle(0, 0, SideResizeWidth, SideResizeWidth);
            }
        }
        internal Rectangle TopRect
        {
            get
            {
                return new Rectangle(
                    SideResizeWidth,
                    0,
                    this.Size.Width - SideResizeWidth * 2,
                    SideResizeWidth);
            }
        }
        internal Rectangle TopRightRect
        {
            get
            {
                return new Rectangle(
                    this.Size.Width - SideResizeWidth,
                    0,
                    SideResizeWidth,
                    SideResizeWidth);
            }
        }
        internal Rectangle LeftRect
        {
            get
            {
                return new Rectangle(
                    0,
                    SideResizeWidth,
                    SideResizeWidth,
                    this.Size.Height - SideResizeWidth * 2);
            }
        }
        internal Rectangle RightRect
        {
            get
            {
                return new Rectangle(
                    this.Size.Width - SideResizeWidth,
                    SideResizeWidth,
                    SideResizeWidth,
                    this.Size.Height - SideResizeWidth * 2);
            }
        }
        internal Rectangle BottomLeftRect
        {
            get
            {
                return new Rectangle(
                    0,
                    this.Size.Height - SideResizeWidth,
                    SideResizeWidth,
                    SideResizeWidth);
            }
        }
        internal Rectangle BottomRect
        {
            get
            {
                return new Rectangle(
                    SideResizeWidth,
                    this.Size.Height - SideResizeWidth,
                    this.Size.Width - SideResizeWidth * 2,
                    SideResizeWidth);
            }
        }
        internal Rectangle BottomRightRect
        {
            get
            {
                return new Rectangle(
                    this.Size.Width - SideResizeWidth,
                    this.Size.Height - SideResizeWidth,
                    SideResizeWidth,
                    SideResizeWidth);
            }
        }
        private bool WmNcHitTest(ref Message m)
        {
            int para = m.LParam.ToInt32();
            int x0 = WinAPI.LOWORD(para);
            int y0 = WinAPI.HIWORD(para);
            Point p = PointToClient(new Point(x0, y0));

            if (TopLeftRect.Contains(p))
            {
                m.Result = new IntPtr((int)WinAPI.NCHITTEST.HTTOPLEFT);
                return true;
            }

            if (TopRect.Contains(p))
            {
                m.Result = new IntPtr((int)WinAPI.NCHITTEST.HTTOP);
                return true;
            }

            if (TopRightRect.Contains(p))
            {
                m.Result = new IntPtr((int)WinAPI.NCHITTEST.HTTOPRIGHT);
                return true;
            }

            if (LeftRect.Contains(p))
            {
                m.Result = new IntPtr((int)WinAPI.NCHITTEST.HTLEFT);
                return true;
            }

            if (RightRect.Contains(p))
            {
                m.Result = new IntPtr((int)WinAPI.NCHITTEST.HTRIGHT);
                return true;
            }

            if (BottomLeftRect.Contains(p))
            {
                m.Result = new IntPtr((int)WinAPI.NCHITTEST.HTBOTTOMLEFT);
                return true;
            }

            if (BottomRect.Contains(p))
            {
                m.Result = new IntPtr((int)WinAPI.NCHITTEST.HTBOTTOM);
                return true;
            }

            if (BottomRightRect.Contains(p))
            {
                m.Result = new IntPtr((int)WinAPI.NCHITTEST.HTBOTTOMRIGHT);
                return true;
            }

            m.Result = new IntPtr((int)WinAPI.NCHITTEST.HTCLIENT);
            return true;
        }
        public bool WmNcActivate(ref Message m)
        {
            // something here
            m.Result = WinAPI.TRUE;
            return true;
        }


        /// <summary>
        /// 判断所接收到的 wm_nc-calc-size 消息是否指示窗体即将最大化
        /// </summary>        
        public bool IsAboutToMaximize(WinAPI.RECT rect)
        {
            /*
             * 判断的方法是，只要窗体的左右、上下都延伸到了屏幕工作区之外，
             * 并且左和右、上和下都延伸相同的量，就认为窗体是要进行最大化
             */

            int left = rect.Left;
            int top = rect.Top;
            int width = rect.Right - rect.Left;
            int height = rect.Bottom - rect.Top;

            if (left <= 0 && top <= 0)
            {
                if (top < 0 && this.WindowState != FormWindowState.Maximized) //判断当拖拽到最上方的时候发生
                    this.faTabStrip1.Visible = false;
                else
                    faTabStrip1.Visible = true;

                Rectangle workingArea = Screen.GetWorkingArea(this);
                if (width == (workingArea.Width + (-left) * 2)
                    && height == (workingArea.Height + (-top) * 2))
                    return true;
            }
            return false;
        }
        public bool WmNcCalcSize(ref Message m)
        {
            if (m.WParam == new IntPtr(1))
            {
                WinAPI.NCCALCSIZE_PARAMS info = (WinAPI.NCCALCSIZE_PARAMS)Marshal.PtrToStructure(m.LParam, typeof(WinAPI.NCCALCSIZE_PARAMS));
                if (IsAboutToMaximize(info.rectNewForm))
                {
                    Rectangle workingRect = Screen.GetWorkingArea(this);
                    info.rectNewForm.Left = workingRect.Left;// - SideResizeWidth;
                    info.rectNewForm.Top = workingRect.Top;
                    info.rectNewForm.Right = workingRect.Right;// + SideResizeWidth;
                    info.rectNewForm.Bottom = workingRect.Bottom;// + SideResizeWidth;
                    Marshal.StructureToPtr(info, m.LParam, true);
                }
            }
            return true;
        }
        #endregion

        #region 设置功能
        /// <summary>
        /// 清除浏览器记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Clear_Cache_Click(object sender, EventArgs e)
        {
            try
            {
                if (Directory.Exists(GetAppDir("Cache")))
                    Directory.Delete(GetAppDir("Cache"), true);
            }
            catch
            {

            }
        }

        private void BtnSettings_ButtonClick(object sender, System.EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                Settings_Menu.Show(MainForm.Instance.ToolsPanel.PointToScreen(new Point(BtnSettings.Location.X - BtnSettings.Width-146, BtnSettings.Location.Y + BtnSettings.Bottom -9)));
            }
            else if (this.WindowState == FormWindowState.Normal)
            {
                Settings_Menu.Show(MainForm.Instance.ToolsPanel.PointToScreen(new Point(BtnSettings.Location.X-146, BtnSettings.Location.Y + BtnSettings.Bottom -9 )));
            }
        }

        private void MulitiUsers_Support_Click(object sender, EventArgs e)
        {
            //if (MulitiUsers_Support.Checked)
            //    MulitiUsers_Support.Checked = false;
            //else
            //    MulitiUsers_Support.Checked = true;
        }
        #endregion

        #region HotKey
        /// <summary>
        /// 如果函数执行成功，返回值不为0。
        /// 如果函数执行失败，返回值为0。要得到扩展错误信息，调用GetLastError。
        /// </summary>
        /// <param name="hWnd">要定义热键的窗口的句柄</param>
        /// <param name="id">定义热键ID（不能与其它ID重复）</param>
        /// <param name="fsModifiers">标识热键是否在按Alt、Ctrl、Shift、Windows等键时才会生效</param>
        /// <param name="vk">定义热键的内容</param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, KeyModifiers fsModifiers, Keys vk);

        /// <summary>
        /// 注销热键
        /// </summary>
        /// <param name="hWnd">要取消热键的窗口的句柄</param>
        /// <param name="id">要取消热键的ID</param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        /// <summary>
        /// 辅助键名称。
        /// Alt, Ctrl, Shift, WindowsKey
        /// </summary>
        [Flags()]
        public enum KeyModifiers { None = 0, Alt = 1, Ctrl = 2, Shift = 4, WindowsKey = 8 }

        /// <summary>
        /// 注册热键
        /// </summary>
        /// <param name="hwnd">窗口句柄</param>
        /// <param name="hotKey_id">热键ID</param>
        /// <param name="keyModifiers">组合键</param>
        /// <param name="key">热键</param>
        public static void RegHotKey(IntPtr hwnd, int hotKeyId, KeyModifiers keyModifiers, Keys key)
        {
            if (!RegisterHotKey(hwnd, hotKeyId, keyModifiers, key))
            {
                int errorCode = Marshal.GetLastWin32Error();
                if (errorCode == 1409)
                {
                    MessageBox.Show("热键被占用 ！");
                }
                else
                {
                    MessageBox.Show("注册热键失败！错误代码：" + errorCode);
                }
            }
        }

        /// <summary>
        /// 注销热键
        /// </summary>
        /// <param name="hwnd">窗口句柄</param>
        /// <param name="hotKey_id">热键ID</param>
        public static void UnRegHotKey(IntPtr hwnd, int hotKeyId)
        {
            //注销指定的热键
            UnregisterHotKey(hwnd, hotKeyId);
        }



        #endregion

        #region 页面查找功能

       public bool searchOpen = false;
        string lastSearch = "";
        private void sBtnSear_Click(object sender, EventArgs e)
        {
            if (faTabStrip1.SelectedItem.splic.Panel1.Controls.Count > 0 && faTabStrip1.SelectedItem.Title != CefConstHelper.CefDownloadTitle)
            {
                OpenSearch();
            }
        }
        public void OpenSearch()
        {
            if (!searchOpen)
            {
                searchOpen = true;
                PanelSearch.Location = new Point(this.Width - PanelSearch.Width-PanelSearch.Width/4-5 , FavPanel.Top + 10);
                InvokeIfNeeded(delegate () {
                    PanelSearch.Visible = true;
                    TxtSearch.Text = lastSearch;
                    TxtSearch.Focus();
                    TxtSearch.SelectAll();
                });
            }
            else
            {
                InvokeIfNeeded(delegate () {
                    TxtSearch.Focus();
                    TxtSearch.SelectAll();
                });
            }
        }
        public void CloseSearch()
        {
            if (searchOpen)
            {
                searchOpen = false;
                TxtSearch.Text = "";
                InvokeIfNeeded(delegate () {
                    PanelSearch.Visible = false;
                    if(faTabStrip1.SelectedItem.splic.Panel1.Controls.Count>0)
                      ((CefWebBrowser)faTabStrip1.SelectedItem.splic.Panel1.Controls[0]).Browser.GetHost().StopFinding(true);
                });
            }
        }
        private void BtnCloseSearch_Click(object sender, EventArgs e)
        {
            CloseSearch();
            if (faTabStrip1.SelectedItem.splic.Panel1.Controls.Count > 0)
                ((CefWebBrowser)faTabStrip1.SelectedItem.splic.Panel1.Controls[0]).Browser.GetHost().SetFocus(true);
        }

        private void BtnPrevSearch_Click(object sender, EventArgs e)
        {
            FindTextOnPage(false);
        }
        private void BtnNextSearch_Click(object sender, EventArgs e)
        {
            FindTextOnPage(true);
        }

        private void FindTextOnPage(bool next = true)
        {
            if (faTabStrip1.SelectedItem.splic.Panel1.Controls.Count > 0)
            {
                bool first = lastSearch != TxtSearch.Text;
                lastSearch = TxtSearch.Text;
                if (lastSearch.CheckIfValid())
                {
                    ((CefWebBrowser)faTabStrip1.SelectedItem.splic.Panel1.Controls[0]).Browser.GetHost().Find(0, lastSearch, true, false, !first);
                }
                else
                {
                    ((CefWebBrowser)faTabStrip1.SelectedItem.splic.Panel1.Controls[0]).Browser.GetHost().StopFinding(true);
                }
               
            }
            TxtSearch.Focus();
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            FindTextOnPage(true);
        }

        private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {

        }






        #endregion

        #region Mouse Hook
        public static class MouseHook
        {
            public static event MouseEventHandler MouseAction = delegate { };

            public static void Start()
            {
                _hookID = SetHook(_proc);


            }
            public static void stop()
            {
                UnhookWindowsHookEx(_hookID);
            }

            private static LowLevelMouseProc _proc = HookCallback;
            private static IntPtr _hookID = IntPtr.Zero;

            private static IntPtr SetHook(LowLevelMouseProc proc)
            {
                using (Process curProcess = Process.GetCurrentProcess())
                using (ProcessModule curModule = curProcess.MainModule)
                {
                    return SetWindowsHookEx(WH_MOUSE_LL, proc,
                      GetModuleHandle(curModule.ModuleName), 0);
                }
            }

            private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);

            private static IntPtr HookCallback(
              int nCode, IntPtr wParam, IntPtr lParam)
            {
                

                if (nCode >= 0 && MouseMessages.WM_MOUSEWHEEL == (MouseMessages)wParam)
                {
                    MSLLHOOKSTRUCT hookStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));
                    //int delta = (int)hookStruct.mouseData / 65536; //这里是判断鼠标滚轮的滚动
                    
                   MouseAction(null, new MouseEventArgs(MouseButtons.Middle, 1, MousePosition.X, MousePosition.Y, (int)hookStruct.mouseData / 65536));
                }

                if ((nCode >= 0 && MouseMessages.WM_MOUSEMOVE == (MouseMessages)wParam)) //这里是当鼠标在移动时发生
                {
                    MSLLHOOKSTRUCT hookStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));
                    MouseAction(null, new MouseEventArgs(MouseButtons.None, 0, MousePosition.X, MousePosition.Y, 0));
                }

                return CallNextHookEx(_hookID, nCode, wParam, lParam);
            }


            private const int WH_MOUSE_LL = 14;

            private enum MouseMessages
            {
                WM_LBUTTONDOWN = 0x0201,
                WM_LBUTTONUP = 0x0202,
                WM_MOUSEMOVE = 0x0200,
                WM_MOUSEWHEEL = 0x020A,
                WM_RBUTTONDOWN = 0x0204,
                WM_RBUTTONUP = 0x0205
            }

            [StructLayout(LayoutKind.Sequential)]
            private struct POINT
            {
                public int x;
                public int y;
            }

            [StructLayout(LayoutKind.Sequential)]
            private struct MSLLHOOKSTRUCT
            {
                public POINT pt;
                public uint mouseData;
                public uint flags;
                public uint time;
                public IntPtr dwExtraInfo;
            }

            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            private static extern IntPtr SetWindowsHookEx(int idHook,
              LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);

            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            private static extern bool UnhookWindowsHookEx(IntPtr hhk);

            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
              IntPtr wParam, IntPtr lParam);

            [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            private static extern IntPtr GetModuleHandle(string lpModuleName);


        }



        #endregion

        #region 下载相关功能
        //显示下载按钮
        private void viewDowload_ButtonClick(object sender, EventArgs e)
        {
            int itemNumber = -1;
            bool downloadForm_isOpen = false;
            for(int i=0;i < faTabStrip1.Items.Count;i++)
            {
                if (faTabStrip1.Items[i].Title == CefConstHelper.CefDownloadTitle)
                {
                    downloadForm_isOpen = true;
                    itemNumber = i;
                    break;
                }
            }
            if (downloadForm_isOpen)
            {
                faTabStrip1.SelectedItem = faTabStrip1.Items[itemNumber];
                faTabStrip1.SelectItem(faTabStrip1.Items[itemNumber]);
            }
            else
            {
                //打开所有下载界面
                FATabStripItem fATab = new FATabStripItem();
                fATab.ItemIcon = Properties.Resources.Download;
                fATab.Title = CefConstHelper.CefDownloadTitle;// "下载内容";
                fATab.BackColor = Color.FromArgb(248, 249, 250);
                faTabStrip1.InsetTab(fATab, true, faTabStrip1.SelectedItem.TabIndex + 1);
                fATab = null;
                GC.Collect();
                LoadDownList_Info();
            }

            //关闭DownloadPanel
            for (int i = 0; i < DownloadPanel3.Controls.Count - 2; i++)
            {
                DownloadPanel3.Controls.Remove(DownloadPanel3.Controls[2]);
            }
            DownloadPanel3.Visible = false;
            faTabStrip1.Height = faTabStrip1.Height + 16 + DownloadPanel3.Height;

        }

        /// <summary>
        /// 加载下载信息
        /// </summary>
        public void LoadDownList_Info()
        {
            string currDirectiory = Environment.CurrentDirectory.ToString();
            if (!PublicClass.CheckFiles("mDownloadRecodes"))
            {
                MessageBox.Show("加载下载列表失败！");
                return;
            }

            mDownloadTopItemcs topItemcs = new mDownloadTopItemcs();
            int itemIndex = 0;
            topItemcs.Dock = DockStyle.Top;
            //topItemcs.SearchPanel1.Width = faTabStrip1.SelectedItem.Width / 3;
            //topItemcs.SearchPanel1.Location = new Point((faTabStrip1.SelectedItem.Width - topItemcs.SearchPanel1.Width) / 2, 6);

            for (int i = 0; i < faTabStrip1.Items.Count; i++)
            {
                if (faTabStrip1.Items[i].Title == CefConstHelper.CefDownloadTitle)
                {
                    faTabStrip1.Items[i].splic.Panel1.Controls.Clear();
                    itemIndex = i;
                    faTabStrip1.Items[i].splic.Panel1.Controls.Add(topItemcs);
                    break;
                }
            }
            //topItemcs.SearchPanel1.Width = faTabStrip1.SelectedItem.Width / 3;
            //topItemcs.SearchPanel1.Location = new Point((faTabStrip1.SelectedItem.Width - topItemcs.SearchPanel1.Width) / 2, 6);

            string jsonStr = File.ReadAllText(currDirectiory + @"\UserData\mDownloadRecodes");
            Font sf = new Font("Tahoma", 8.25f, FontStyle.Regular);
            Graphics g = CreateGraphics();
            Jdownload downitem;// = new FavirteButton();
            List<Jdownload> downitemBTs;
            int iK = 0;
            try
            {
                var jobInfoList = JsonConvert.DeserializeObject<List<Jdownload>>(jsonStr);
                if (jobInfoList.Count > 0)
                {

                    downitemBTs = jobInfoList;
                    foreach (Jdownload jobInfo in downitemBTs)
                    {
                        downitem = new Jdownload();
                        downitem.FileName = jobInfo.FileName;
                        downitem.mDownloadTextBTN1.URL = downitem.DownloadUrl = jobInfo.DownloadUrl;
                        downitem.FullFilePaths = jobInfo.FullFilePaths;
                        downitem.DownloadUrl = jobInfo.DownloadUrl;
                        downitem.FileAlreadyDele = jobInfo.FileAlreadyDele;
                        downitem.IsDownloading = jobInfo.IsDownloading;
                        //downitem.Startime = jobInfo.Startime;
                        downitem.DownloadID = jobInfo.DownloadID;
                        downitem.pictureBox1.Image = PublicClass.Base64ToImage(jobInfo.ImageBase64str);
                        downitem.ImageBase64str = jobInfo.ImageBase64str;
                        downitem.Width = this.Width / 3;
                        downitem.Location = new Point((faTabStrip1.Items[itemIndex].Width - downitem.Width) / 2,
                                            faTabStrip1.Items[itemIndex].splic.Panel1.Controls[iK].Location.Y +
                                            faTabStrip1.Items[itemIndex].splic.Panel1.Controls[iK].Height + 20);

                        faTabStrip1.Items[itemIndex].splic.Panel1.Controls.Add(downitem);

                        faTabStrip1.Items[itemIndex].splic.Panel1.AutoScroll = true;
                        faTabStrip1.Items[itemIndex].splic.Panel1.AutoScrollMinSize = new Size(Screen.GetWorkingArea(this).Width-20, Screen.GetWorkingArea(this).Height - 76);

                        //GC.Collect();
                        iK++;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            if (faTabStrip1.SelectedItem.Title== CefConstHelper.CefDownloadTitle)
              textBoxXP1.Text = CefConstHelper.CefiBrowserInternalDownloadUrl;// "CefiBrowser://Downloads";
            if (iK > 20)
                MessageBox.Show(this, "下载管理中太多下载内容，请即时清理，以免影响系统运行！", "CefiBrowser", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            faTabStrip1.Items[itemIndex].splic.Panel1.BackColor = Color.FromArgb(248, 249, 250);
            topItemcs.SearchPanel1.Width = faTabStrip1.SelectedItem.Width / 3;
            topItemcs.SearchPanel1.Location = new Point((faTabStrip1.SelectedItem.Width - topItemcs.SearchPanel1.Width) / 2, 6);

            currDirectiory = null;
            jsonStr = null;
            sf = null;
            g = null;
            downitem = null;
            topItemcs = null;

            GC.Collect();
        }

        //关闭DownloadPanel
        private void buttonXP2_ButtonClick(object sender, EventArgs e)
        {
            //关闭DownloadPanel
            for(int i=DownloadPanel3.Controls.Count; i >2;i--)
            {
                DownloadPanel3.Controls.Remove(DownloadPanel3.Controls[i-1]);
            }
            DownloadPanel3.Visible = false;
            faTabStrip1.Height = faTabStrip1.Height + 16 + DownloadPanel3.Height;
        }
        //打开所有下载界面
        private void DownloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //打开所有下载界面
            int itemNumber = -1;
            bool downloadForm_isOpen = false;
            for (int i = 0; i < faTabStrip1.Items.Count; i++)
            {
                if (faTabStrip1.Items[i].Title == CefConstHelper.CefDownloadTitle) // "下载内容")
                {
                    downloadForm_isOpen = true;
                    itemNumber = i;
                    break;
                }
            }
            if (downloadForm_isOpen)
            {
                faTabStrip1.SelectedItem = faTabStrip1.Items[itemNumber];
                faTabStrip1.SelectItem(faTabStrip1.Items[itemNumber]);
            }
            else
            {
                //打开所有下载界面
                FATabStripItem fATab = new FATabStripItem();
                fATab.ItemIcon = Properties.Resources.Download;
                fATab.Title = CefConstHelper.CefDownloadTitle;// "下载内容";

                faTabStrip1.InsetTab(fATab, true, faTabStrip1.SelectedItem.TabIndex + 1);
                fATab = null;
                GC.Collect();
                LoadDownList_Info();
            }
            //this.Invalidate();
        }


        #endregion

        #region 打印功能
        private void CefiBPrint_Click(object sender, EventArgs e)
        {
            if (faTabStrip1.SelectedItem.splic.Panel1.Controls.Count > 0 && faTabStrip1.SelectedItem.Title != CefConstHelper.CefDownloadTitle)
            {
                ((CefWebBrowser)faTabStrip1.SelectedItem.splic.Panel1.Controls[0]).Browser.GetHost().Print(); //print web page
            }
        }

        private void PrinttoPDF_Click(object sender, EventArgs e)
        {
            if (faTabStrip1.SelectedItem.splic.Panel1.Controls.Count > 0 && faTabStrip1.SelectedItem.Title != CefConstHelper.CefDownloadTitle)
            {
                SaveFileDialog pdfsave = new SaveFileDialog();
                pdfsave.Filter = "PDF文件|*.pdf";
                pdfsave.FilterIndex = 0;
                pdfsave.RestoreDirectory = true;//保存对话框是否记忆上次打开的目录
                pdfsave.CheckPathExists = true;//检查目录
                pdfsave.FileName = faTabStrip1.SelectedItem.Title;// System.DateTime.Now.ToString("yyyyMMddHHmmss") + "-"; ;//设置默认文件名

                if (pdfsave.ShowDialog() == DialogResult.OK)
                {
                    ((CefWebBrowser)faTabStrip1.SelectedItem.splic.Panel1.Controls[0]).Browser.GetHost().PrintToPdf(pdfsave.FileName, new CefPdfPrintSettings(), new CefWebPdfPrinerHandler());
                }
                pdfsave = null;
            }
        }
        #endregion

        private void OpneNFataItem_Click(object sender, EventArgs e)
        {
            FATabStripItem fATab = new FATabStripItem();
            faTabStrip1.InsetTab(fATab, true, faTabStrip1.SelectedItem.TabIndex + 1);
            faTabStrip1.SelectedItem = fATab;
            EnableBackButton(false);
            EnableForwardButton(false);
            EnableReflashButton(false); //新建一个空的标签页
            fATab = null;
            GC.Collect();
        }

        //开发者工具
        private void F12Tools_Click(object sender, EventArgs e)
        {
            if (faTabStrip1.SelectedItem.splic.Panel1.Controls.Count > 0 && faTabStrip1.SelectedItem.Title != CefConstHelper.CefDownloadTitle)
            {
                PublicClass.DevTools(((CefWebBrowser)faTabStrip1.SelectedItem.splic.Panel1.Controls[0]), ((CefWebBrowser)faTabStrip1.SelectedItem.splic.Panel1.Controls[0]).Browser);
            }
            
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //ScriptingTelnet mtelnet = new ScriptingTelnet("172.30.14.253",23,10);
            //if (mtelnet.Connect())
            //{
            //    mtelnet.SendAndWait("admin", "");
            //    mtelnet.SendAndWait("Hkit.##^","");
            //    mtelnet.SendAndWait("sys","");
            //    mtelnet.SendAndWait("ipsec policy ipsec2311123542 10 isakmp","");
            //    mtelnet.SendAndWait("undo policy enable","");
            //    mtelnet.Disconnect();
            //}
        }

        //打开新的主程序
        private void OpenNCefiBrowser_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            process.StartInfo.FileName = Directory.GetCurrentDirectory() + @"\" + CefConstHelper.CefiBrowserName;
            process.StartInfo.Arguments = "";
            process.Start();
         //   this.Hide();
        }

        /// <summary>
        /// 自动登录for OCR
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CefGSetting_Click(object sender, EventArgs e)
        {
            if (MemSetting.Checked)
            {
                MemSetting.Checked = false;
                //faTabStrip1.MemCostLower = false;

            }
            else
            {
                //faTabStrip1.MemCostLower = true;
                MemSetting.Checked = true;
            }
        }

      
        /// <summary>
        /// 设置窗口样式，Form 自定义为None，需要的样式在这里调整
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams ret = base.CreateParams;
                //ret.Style = (int)WinAPI.WindowStyles.WS_OVERLAPPED | (int)WinAPI.WindowStyles.WS_TABSTOP
                //| (int)WinAPI.WindowStyles.WS_THICKFRAME | (int)WinAPI.WindowStyles. |
                //(int)WinAPI.WindowStyles.WS_DLGFRAME | (int)WinAPI.WindowStyles.WS_CLIPCHILDREN;
                // | (int)WinAPI.WindowStyles.WS_BORDER;//
                ret.Style = (int)WinAPI.WindowStyles.WS_ForJack;
                ret.ExStyle |= (int)WinAPI.WindowStyles.WS_EX_ACCEPTFILES | (int)WinAPI.WindowStyles.WS_EX_TOOLWINDOW;
                return ret;
            }
        }

        public void ReDWLine(Graphics e)
        {
            Rectangle borderRc = this.ClientRectangle;

            if (this.WindowState == FormWindowState.Normal)
            {
                borderRc.Width--;
                borderRc.Height--;
                e.DrawRectangle(new Pen(SystemColors.ControlDark, 1), borderRc);

            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            ReDWLine(e.Graphics);

        }


        
    }
}


