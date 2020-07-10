using CefiBrowser.JackControl;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using Xilium.CefGlue;

namespace CefiBrowser
{
    [ToolboxItem(true)]
    public class FavirteButton : Button
    {
        private Color MoveSelectedColor = Color.FromArgb(231, 231, 231); //标签未被选中时鼠标移到该按钮的颜色
        private Rectangle btRect = Rectangle.Empty;
        private Color offSelectedColor = Color.FromArgb(208, 208, 208); //标签未选中时的颜色
        private Color onSelectedColor = Color.FromArgb(242, 242, 242); //标签被选中时的颜色
        private Pen FormLineColor = new Pen(Color.FromArgb(169, 169, 169));// new Pen(SystemColors.ControlDarkDark); //Form边框边线效果颜色
        private Image itemIcon = null;//Logo icon

        private string uRL = string.Empty; //存按钮的URL
        private string title = string.Empty; //名称
        private StringFormat sf = null;
        private static Font defaultFont = new Font("Tahoma", 8.25f, FontStyle.Regular);
        private string date_added = string.Empty; //建立时间
        private string id;
        private string last_visited = string.Empty; //最后访问时间
        private string type = string.Empty; //类型
        private string fatherID = string.Empty; //父ID
        private string layer = string.Empty; //标记层
        private int titleWidth;
        private string iconBase64str = string.Empty;
        private bool isSelect = false;
        private List<FavireBT> favireBTs;
        public ImageList iconImageList = new ImageList(); //存放icon图标
        public Timer t_imer = new Timer();

        public FavirteButton()
        {
            //首先开启双缓冲，防止闪烁
            //双缓冲的一大堆设置 具体参数含义参照msdn的ControlStyles枚举值
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.FixedWidth, true);
            this.SetStyle(ControlStyles.FixedHeight, true);
            //this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            sf = new StringFormat();
            sf.Trimming = StringTrimming.EllipsisCharacter; //超长时最后三个字符以...代替
            sf.FormatFlags |= StringFormatFlags.NoWrap;
            sf.FormatFlags &= StringFormatFlags.DirectionRightToLeft;
            
           
            //this.AutoSizeMode = AutoSizeMode.GrowOnly;
            //this.AutoSize = true;
            this.BackColor = Color.Transparent;
            t_imer.Interval = 200;
            t_imer.Tick += new EventHandler(Timer_Tick);
            

        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000 | (int)WinAPI.WindowStyles.WS_CLIPCHILDREN;
                return cp;
            }
        }
        //用来标示是否鼠标正在悬浮在按钮上  true:悬浮在按钮上 false:鼠标离开了按钮
        private bool m_bMouseHover;
        //用来标示是否鼠标点击了按钮  true：按下了按钮 false：松开了按钮
        private bool m_bMouseDown;

        private int time_t = 0;
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (time_t >= 600)
            {
                t_imer.Stop();
                showtext();

            }
            else
            {
                time_t += 100;
            }
        }

        //重载鼠标悬浮的事件
        protected override void OnMouseEnter(EventArgs e)
        {
            //当鼠标进入控件时，标示变量为进入了控件
            m_bMouseHover = true;
            //刷新面板触发OnPaint重绘
            if (t_imer.Enabled == false)
            {
                t_imer.Enabled = true;
                t_imer.Start();
            }
            this.Invalidate();
            base.OnMouseEnter(e);
        }

        private void showtext()
        {

            if (this.Type != "FaviFolder")
                MainForm.Instance.Invoke(new Action(() =>
                {
                    Font sf = new Font("Tahoma", 10.5f, FontStyle.Regular);
                    Graphics g = this.CreateGraphics();
                    //单位为mm
                    g.PageUnit = GraphicsUnit.Millimeter;
                    //测量字符串长度
                    Size sif = TextRenderer.MeasureText(g, this.URL, sf, new Size(0, 0), TextFormatFlags.NoPadding);
                    Size tsif = TextRenderer.MeasureText(g, this.Title, sf, new Size(0, 0), TextFormatFlags.NoPadding);
                    if (sif.Width > tsif.Width)
                        MainForm.Instance.textShow.Width = sif.Width + 6;
                    else
                        MainForm.Instance.textShow.Width = tsif.Width + 6;


                    //如果当前宽度不够那就以Form的宽度为基准
                    if(MainForm.Instance.textShow.Width> MainForm.Instance.FavPanel.Width)
                    {
                        MainForm.Instance.textShow.Width = MainForm.Instance.FavPanel.Width - 3;
                    }
                    int PX = MousePosition.X;
                    MainForm.Instance.textShow.Text = this.Title + "\r\n" + this.URL;
                    //当Laction+text宽度大于Form的宽的时候
                    if (MainForm.Instance.textShow.Width + MainForm.Instance.FavPanel.PointToClient(new Point(PX, MousePosition.Y)).X > MainForm.Instance.FavPanel.Width)
                    {
                        PX = PX - 3- (MainForm.Instance.textShow.Width + MainForm.Instance.FavPanel.PointToClient(new Point(PX, MousePosition.Y)).X - MainForm.Instance.FavPanel.Width);
                    }
                    if (MainForm.Instance.WindowState == FormWindowState.Maximized)
                        MainForm.Instance.textShow.Location = MainForm.Instance.FavPanel.PointToClient(new Point(PX, MousePosition.Y + 82));
                    else if (MainForm.Instance.WindowState == FormWindowState.Normal)
                        MainForm.Instance.textShow.Location =   MainForm.Instance.FavPanel.PointToClient(new Point(PX, MousePosition.Y + 97));

                    MainForm.Instance.textShow.Visible = true;
                }
                ));
            time_t = 0;
            t_imer.Enabled = false;
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
        }
        //重载鼠标离开的事件
        protected override void OnMouseLeave(EventArgs e)
        {
            MainForm.Instance.Invoke(new Action(() =>
            {
                MainForm.Instance.textShow.Visible = false;
            }
));
            time_t = 0;
            t_imer.Enabled = false;

            //当鼠标离开控件时，标示变量为离开了控件
            m_bMouseHover = false;
            //刷新面板触发OnPaint重绘

            this.Invalidate();
            base.OnMouseLeave(e);
        }

        int xPos, yPos;
        Point mDownPiont;
        //重载鼠标按下的事件
        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            xPos = mevent.X;//当前x坐标.
            yPos = mevent.Y;//当前y坐标.
            mDownPiont = PointToScreen( new Point( mevent.X,mevent.Y));
            //当鼠标按下控件时，标示变量为按下了控件
            m_bMouseDown = true;
            //刷新面板触发OnPaint重绘
            this.Invalidate();
            base.OnMouseDown(mevent);
        }

        FavirteButton favirte_Button;
        protected override void OnMouseMove(MouseEventArgs mevent)
        {
            base.OnMouseMove(mevent);
            if (m_bMouseDown)
            {
                this.BringToFront();
                this.Left += Convert.ToInt16(mevent.X - xPos);//设置x坐标.
                                                              //  this.Top += Convert.ToInt16(mevent.Y - yPos);//设置y坐标.
                for (int i = 0; i < MainForm.Instance.mFavList.Count; i++)
                    if (this.Title == MainForm.Instance.mFavList[i].Title)
                    {
                        if (i > 0)
                            if (this.Left > MainForm.Instance.Left && this.Left < MainForm.Instance.mFavList[i - 1].Right - MainForm.Instance.mFavList[i - 1].Width / 2)
                            {
                                
                                favirte_Button= MainForm.Instance.mFavList[i - 1];
                                MainForm.Instance.mFavList[i - 1] = this ;
                                MainForm.Instance.mFavList[i] = favirte_Button;
                                MainForm.Instance.FavPanel.Invalidate();
                            }
                    }
            }
        }


        //重载鼠标松开的事件
        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            if (mDownPiont != PointToScreen(new Point(mevent.X, mevent.Y)))
            {
                m_bMouseDown = false;
                //this.Invalidate();
                return;
            }

            MainForm.Date_Added = this.Date_Added;
            Point p = new Point(this.Location.X, this.Location.Y + this.Bottom);
            if (mevent.Button != MouseButtons.Left)
            {

            
                //增加右键菜单功能
                IsSelect = true;
                //判断是否弹出子菜单
                if (this.Type == "FaviFolder")
                {
                    MainForm.Instance.Invoke(new Action(() =>
                    {
                        MainForm.Instance.CopyFavriBtURL.Visible = false;
                        MainForm.Instance.OpenRULInNewTab.Visible = false;
                        MainForm.Instance.DelCurrentFavriBT.Visible = true;
                        MainForm.Instance.Favi_3_Line.Visible = true;
                        MainForm.Instance.Favi_Modif_Item.Visible = true;
                        MainForm.Instance.Favri_BarMenu.Show(MousePosition);
                    }
                    ));
                }
                else
                {
                    MainForm.Instance.Invoke(new Action(() =>
                    {
                        MainForm.Instance.CopyFavriBtURL.Visible = true;
                        MainForm.Instance.DelCurrentFavriBT.Visible = true;
                        MainForm.Instance.OpenRULInNewTab.Visible = true;
                        MainForm.Instance.Favi_3_Line.Visible = true;
                        MainForm.Instance.Favi_Modif_Item.Visible = true;
                        MainForm.Instance.Favri_BarMenu.Show(MousePosition);
                    }
                    ));
                }
            }
            else
            {
                //判断是否弹出子菜单
                if (this.Type == "FaviFolder")
                {
                    IsSelect = false;
                    MainForm.Instance.CopyFavriBtURL.Visible = false;
                    MainForm.Instance.DelCurrentFavriBT.Visible = false;
                    MainForm.Instance.OpenRULInNewTab.Visible = false;
                    MainForm.Instance.Favi_3_Line.Visible = false;
                    MainForm.Instance.Favi_Modif_Item.Visible = false;
                    MainForm.Instance.Invoke(new Action(() =>
                    {
                        MainForm.Instance.fMenuStrip1.Items.Clear();
                        //MainForm.Instance.fMenuStrip1.Width = 266;
                        string jsonStr = File.ReadAllText(PublicClass.currDirectiory + @"\UserData\Bookmarks");
                        try
                        {
                            var jobInfoList = JsonConvert.DeserializeObject<List<FavireBT>>(jsonStr);
                            if (jobInfoList.Count > 0)
                            {
                                favireBTs = jobInfoList;
                                int countfMenuItem = 0;
                                int fWidth = 0;
                                foreach (FavireBT jobInfo in favireBTs)
                                {

                                    if (jobInfo.FatherID == this.ID)
                                    {
                                        countfMenuItem++;
                                        FMenuItem fMenuItem = new FMenuItem();
                                        fMenuItem.Width = 266;
                                        fMenuItem.Height = 22;
                                        fMenuItem.Title = jobInfo.Title;
                                        if (GetPixelByStr(fMenuItem.Title) < fMenuItem.Width)
                                        {
                                            fMenuItem.Width = GetPixelByStr(fMenuItem.Title) + 58;
                                            if (fMenuItem.Width > fWidth)
                                                fWidth = fMenuItem.Width;
                                        }
                                        if (fMenuItem.Width > 260)
                                            fMenuItem.Width = fWidth = 266 + 26;

                                        fMenuItem.URL = jobInfo.URL;
                                        fMenuItem.ItemIcon = PublicClass.Base64ToImage(jobInfo.IconBase64str);
                                        // fMenuItem.Image = fMenuItem.ItemIcon; //这里如果给值就导致图标变大成24*24的
                                        //fMenuItem.Text = fMenuItem.Title;
                                        fMenuItem.ToolTipText = jobInfo.Title;
                                        fMenuItem.Invalidate();
                                        MainForm.Instance.fMenuStrip1.Items.Add(fMenuItem);
                                    }
                                }
                                MainForm.Instance.fMenuStrip1.Width = fWidth;
                                MainForm.Instance.fMenuStrip1.Height = countfMenuItem * 22 + 4;//自动调整个快捷菜单的高度
                                for (int i = 0; i < MainForm.Instance.fMenuStrip1.Items.Count; i++)
                                {
                                    MainForm.Instance.fMenuStrip1.Items[i].Width = fWidth;
                                }

                            }
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message);
                        }
                        MainForm.Instance.fMenuStrip1.Show(MainForm.Instance.FavPanel.PointToScreen(p));
                    }
                    ));
                }
                else if (MainForm.Instance.faTabStrip1.SelectedItem.splic.Panel1.Controls.Count > 0)
                {
                    MainForm.Instance.Invoke(new Action(() =>
                    {
                        PublicClass.BrowserLoadingInfo(MainForm.Instance.faTabStrip1.SelectedItem);
                        if (MainForm.Instance.faTabStrip1.SelectedItem.Title == CefConstHelper.CefDownloadTitle)
                        {
                            MainForm.Instance.faTabStrip1.SelectedItem.splic.Panel1.Controls.Clear();
                           // PublicClass.BrowserCSH(MainForm.Instance.faTabStrip1.SelectedItem);
                            MainForm.Instance.faTabStrip1.SelectedItem.splic.Panel1.Controls.Add(MainForm.Instance.NewChromiumWeb(this.URL, MainForm.Instance.faTabStrip1.SelectedItem.TabIndex));

                        }
                        else
                        {
                            if (MainForm.Instance.faTabStrip1.SelectedItem.ItemBrowser != null)
                                if (MainForm.Instance.faTabStrip1.SelectedItem.ItemBrowser.IsLoading)
                                {
                                    MainForm.Instance.faTabStrip1.SelectedItem.ItemBrowser.StopLoad();
                                }

                            //当鼠标松开时，标示变量为按下并松开了控件
                            IsSelect = false;
                            //刷新面板触发OnPaint重绘

                           
                            if (MainForm.Instance.faTabStrip1.SelectedItem.IsPoPWindow)
                            {
                                
                                MainForm.Instance.faTabStrip1.SelectedItem.splic.Panel1.Controls.Clear();
                                MainForm.Instance.faTabStrip1.SelectedItem.splic.Panel1.Controls.Add(MainForm.Instance.NewChromiumWeb(this.URL, MainForm.Instance.faTabStrip1.SelectedItem.TabIndex));
                            }
                            else
                            {
                                MainForm.Instance.faTabStrip1.SelectedItem.ItemBrowser.GetMainFrame().LoadUrl(this.URL);
                            }
                            GC.Collect();

                        }
                    }
                    ));
                }
                else
                {
                    MainForm.Instance.Invoke(new Action(() =>
                    {
                        PublicClass.BrowserLoadingInfo(MainForm.Instance.faTabStrip1.SelectedItem);
                        if (MainForm.Instance.faTabStrip1.SelectedItem.ItemBrowser != null)
                            if (MainForm.Instance.faTabStrip1.SelectedItem.ItemBrowser.IsLoading)
                            {
                                MainForm.Instance.faTabStrip1.SelectedItem.ItemBrowser.StopLoad();
                            }
                    //当鼠标松开时，标示变量为按下并松开了控件
                    IsSelect = false;
                    //刷新面板触发OnPaint重绘

                        MainForm.Instance.faTabStrip1.SelectedItem.splic.Panel1.Controls.Add(MainForm.Instance.NewChromiumWeb(this.URL, MainForm.Instance.faTabStrip1.SelectedItem.TabIndex));
                    }
                    ));
                }
            }
            m_bMouseDown = false;
            if (MainForm.Instance.faTabStrip1.SelectedItem != null && this.Type != "FaviFolder" && mevent.Button!= MouseButtons.Right)
                MainForm.Instance.textBoxXP1.Text = MainForm.Instance.faTabStrip1.SelectedItem.URL = this.uRL;

            base.OnMouseUp(mevent);
        }



        private void MenuItem_Click(object sender, System.EventArgs e)
        {
            MessageBox.Show("Good Job!");
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

        public bool IsSelect
        {
            get { return isSelect; }
            set { isSelect = value; }
        }
        public String URL
        {
            get { return uRL; }
            set { uRL = value; }
        }

        [DefaultValue(null)]
        public Image ItemIcon
        {
            get { return itemIcon; }
            set { itemIcon = value; }
        }

        public string IconBase64str
        {
            get { return iconBase64str; }
            set { iconBase64str = value; }

        }


        public string Title
        {
            get { return title; }
            set { title = value; }

        }

        public int TitleWidth
        {
            get { return titleWidth; }
            set { titleWidth = value; }
        }
        //建立时间
        public string Date_Added
        {
            get { return date_added; }
            set { date_added = value; }

        }

        //ID
        public string ID
        {
            get { return id; }
            set { id = value; }

        }

        public string Last_Visited
        {
            get { return last_visited; }
            set { last_visited = value; }

        }

        //类型
        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        //父ID
        public string FatherID
        {
            get { return fatherID; }
            set { fatherID = value; }
        }

        //
        public string Layer
        {
            get { return layer; }
            set { layer = value; }
        }
        /// <summary>
        /// 测量字符长度以像素为单位
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public  int GetPixelByStr(string str)
        {
            Font sf = new Font("Tahoma", 8.25f, FontStyle.Regular);
            Graphics g = this.CreateGraphics();
            //单位为mm
            g.PageUnit = GraphicsUnit.Millimeter;
            //测量字符串长度
            Size sif = TextRenderer.MeasureText(g, str, sf, new Size(0, 0), TextFormatFlags.NoPadding);


            return sif.Width;
        }
        //重载绘画事件
        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            //因为上面调用了base会绘制原生控件 重刷一下背景清掉原生绘制 不然自己绘制的是重叠在原生绘制上
            base.OnPaintBackground(pevent);
            //得到绘画句柄
            Graphics g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias; //描边时消除锯齿SetSmoothingMode, AntiAlias为指定消除锯齿的呈现
            g.CompositingQuality = CompositingQuality.HighQuality;

            Rectangle borderRect = ClientRectangle;

            //测量用指定的 System.Drawing.Font 绘制并用指定的 System.Drawing.StringFormat 格式化的指定字符串
            //如果要固定标签长度不需检测Title的宽度
            SizeF textSize = new SizeF(titleWidth, CefConstHelper.TextSizeH);
   
            PointF textLoc = new PointF(borderRect.Left + 20, borderRect.Top + (borderRect.Height / 2) - (textSize.Height / 2));
            RectangleF textRect = new RectangleF(textLoc, textSize);
            textRect.Width = borderRect.Width - 19;

            if (borderRect.Width < 1 && borderRect.Height < 1)
                return;

            //判断使用什么资源图
            if (m_bMouseHover)
            {
                Color fill = Color.FromArgb(255, 252, 244);// renderer.ColorTable.ButtonSelectedHighlight; //当鼠标移动到关闭按钮时的颜色Color.FromArgb(253, 244, 191);//
                g.FillRectangle(new SolidBrush(fill), borderRect);
                borderRect.Width--;
                borderRect.Height--;
                g.DrawRectangle(new Pen(Color.FromArgb(229, 195, 101)), borderRect);//new Pen(Color.FromArgb(229, 195, 101))
            }

            if (m_bMouseDown)
            {
                Color fill = Color.FromArgb(255, 232, 166);// renderer.ColorTable.ButtonSelectedHighlight; //当鼠标移动到关闭按钮时的颜色Color.FromArgb(253, 244, 191);//

                g.FillRectangle(new SolidBrush(fill), borderRect);

                borderRect.Width--;
                borderRect.Height--;

                g.DrawRectangle(new Pen(Color.FromArgb(229, 195, 101)), borderRect);//new Pen(Color.FromArgb(229, 195, 101))
            }

            g.DrawString(Title, defaultFont, new SolidBrush(ForeColor), textRect, sf);

            if (itemIcon != null)
                g.DrawImage(itemIcon, new Rectangle(ClientRectangle.X + 2, ClientRectangle.Height / 2 - CefConstHelper.TextSizeH/2, CefConstHelper.rectIconSizeW, CefConstHelper.rectIconSizeH));

        }

    }
}
