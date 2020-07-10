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
    public class mDownloadTextBTN : Button
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
        private static Font defaultFont = new Font("Tahoma", 12.33f, FontStyle.Regular);
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

        public mDownloadTextBTN()
        {
            //首先开启双缓冲，防止闪烁
            //双缓冲的一大堆设置 具体参数含义参照msdn的ControlStyles枚举值
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            //this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            sf = new StringFormat();
            sf.Trimming = StringTrimming.EllipsisCharacter; //超长时最后三个字符以...代替
            sf.FormatFlags |= StringFormatFlags.NoWrap;
            sf.FormatFlags &= StringFormatFlags.DirectionRightToLeft;

            this.BackColor = Color.Transparent;
            //t_imer.Interval = 200;
            //t_imer.Tick += new EventHandler(Timer_Tick);

        }
        //用来标示是否鼠标正在悬浮在按钮上  true:悬浮在按钮上 false:鼠标离开了按钮
        private bool m_bMouseHover;
        //用来标示是否鼠标点击了按钮  true：按下了按钮 false：松开了按钮
        private bool m_bMouseDown;

        //private int time_t = 0;
        //private void Timer_Tick(object sender, EventArgs e)
        //{
        //    if (time_t >= 600)
        //    {
        //        t_imer.Stop();
        //        showtext();

        //    }
        //    else
        //    {
        //        time_t += 100;
        //    }
        //}

        ////重载鼠标悬浮的事件
        //protected override void OnMouseEnter(EventArgs e)
        //{
        //    //当鼠标进入控件时，标示变量为进入了控件
        //    m_bMouseHover = true;
        //    //刷新面板触发OnPaint重绘
        //    if (t_imer.Enabled == false)
        //    {
        //        t_imer.Enabled = true;
        //        t_imer.Start();
        //    }
        //    this.Invalidate();
        //    base.OnMouseEnter(e);
        //}

        private void showtext()
        {

            //MainForm.Instance.Invoke(new Action(() =>
            //{
                Font sf = new Font("Tahoma", 10.5f, FontStyle.Regular);
                Graphics g = this.CreateGraphics();
                //单位为mm
                g.PageUnit = GraphicsUnit.Millimeter;
                //测量字符串长度
                Size sif = TextRenderer.MeasureText(g, this.URL, sf, new Size(0, 0), TextFormatFlags.NoPadding);
                // Size tsif = TextRenderer.MeasureText(g, this.Title, sf, new Size(0, 0), TextFormatFlags.NoPadding);
                //if (sif.Width )//> tsif.Width)
                //    MainForm.Instance.textShow.Width = sif.Width + 6;
                //else
                //    MainForm.Instance.textShow.Width = tsif.Width + 6;
                this.Text = URL;

            //}
            //));
            //time_t = 0;
            //t_imer.Enabled = false;
        }

        protected override void OnLostFocus(EventArgs e)
        {

            base.OnLostFocus(e);
        }
        //重载鼠标离开的事件
//        protected override void OnMouseLeave(EventArgs e)
//        {
//            MainForm.Instance.Invoke(new Action(() =>
//            {
//                MainForm.Instance.textShow.Visible = false;
//            }
//));
//            time_t = 0;
//            t_imer.Enabled = false;

//            //当鼠标离开控件时，标示变量为离开了控件
//            m_bMouseHover = false;
//            //刷新面板触发OnPaint重绘

//            this.Invalidate();
//            base.OnMouseLeave(e);
//        }

        //重载鼠标按下的事件
        //protected override void OnMouseDown(MouseEventArgs mevent)
        //{

        //    //当鼠标按下控件时，标示变量为按下了控件
        //    m_bMouseDown = true;


        //    //刷新面板触发OnPaint重绘
        //    this.Invalidate();
        //    base.OnMouseDown(mevent);
        //}

        //重载鼠标松开的事件
        protected override void OnMouseUp(MouseEventArgs mevent)
        {


            base.OnMouseUp(mevent);
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

        public String URL
        {
            get { return uRL; }
            set { uRL = value; }
        }

        /// <summary>
        /// 下载后存储在下载目录下的文件名
        /// </summary>
        public String FileName { get; set; }

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
            SizeF textSize = new SizeF(titleWidth, this.Height-3);

            PointF textLoc = new PointF(borderRect.Left, borderRect.Top + (borderRect.Height / 2) - (textSize.Height / 2));
            RectangleF textRect = new RectangleF(textLoc, textSize);
            textRect.Width = borderRect.Width ;

            //if (borderRect.Width < 1 && borderRect.Height < 1)
            //    return;

            //判断使用什么资源图
            //if (m_bMouseHover)
            //{
            //    Color fill = Color.FromArgb(255, 252, 244);// renderer.ColorTable.ButtonSelectedHighlight; //当鼠标移动到关闭按钮时的颜色Color.FromArgb(253, 244, 191);//
            //    g.FillRectangle(new SolidBrush(fill), borderRect);
            //    borderRect.Width--;
            //    borderRect.Height--;
            //    g.DrawRectangle(new Pen(Color.FromArgb(229, 195, 101)), borderRect);//new Pen(Color.FromArgb(229, 195, 101))
            //}

            //if (m_bMouseDown)
            //{
            //    Color fill = Color.FromArgb(255, 232, 166);// renderer.ColorTable.ButtonSelectedHighlight; //当鼠标移动到关闭按钮时的颜色Color.FromArgb(253, 244, 191);//

            //    g.FillRectangle(new SolidBrush(fill), borderRect);

            //    borderRect.Width--;
            //    borderRect.Height--;

            //    g.DrawRectangle(new Pen(Color.FromArgb(229, 195, 101)), borderRect);//new Pen(Color.FromArgb(229, 195, 101))
            //}
            g.DrawString(URL, defaultFont, new SolidBrush(ForeColor), textRect, sf);

            //if (itemIcon != null)
            //    g.DrawImage(itemIcon, new Rectangle(ClientRectangle.X + 2, ClientRectangle.Height / 2 - 7, 16, 16));

        }

    }
}
