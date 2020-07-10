using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CefiBrowser.JackControl
{
    [ToolboxItem(true)]
    public class FMenuItem : ToolStripMenuItem
    {
        #region Fields

        //private Rectangle glyphRect = Rectangle.Empty;
        private bool isMouseOver = false;
        //private Color MoveSelectedColor = Color.FromArgb(231, 231, 231); //标签未被选中时鼠标移到该按钮的颜色
        //private Rectangle btRect = Rectangle.Empty;
        //private Color offSelectedColor = Color.FromArgb(208, 208, 208); //标签未选中时的颜色
        //private Color onSelectedColor = Color.FromArgb(242, 242, 242); //标签被选中时的颜色
        //private Pen FormLineColor = new Pen(Color.FromArgb(169, 169, 169));// new Pen(SystemColors.ControlDarkDark); //Form边框边线效果颜色
        private Image itemIcon = null;//Logo icon

        private string uRL = string.Empty; //存按钮的URL
        private string title = string.Empty; //名称
        private StringFormat sf = null;
        private static Font defaultFont = new Font("Tahoma", 8.25f, FontStyle.Regular);
        private string date_added = string.Empty; //建立时间
        private string last_visited = string.Empty; //最后访问时间
        private string type = string.Empty; //类型
        private string fatherID = string.Empty; //父ID
        private string layer = string.Empty; //标记层
        private string iconBase64str = string.Empty;
        private bool isSelect = false;

        #endregion

        #region Props

        public bool IsMouseOver
        {
            get { return isMouseOver; }
            set { isMouseOver = value; }
        }

        //public Rectangle Bounds
        //{
        //    get { return glyphRect; }
        //    set { glyphRect = value; }
        //}
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

        #endregion

        #region Ctor

        public FMenuItem()
        {
            //首先开启双缓冲，防止闪烁
            //双缓冲的一大堆设置 具体参数含义参照msdn的ControlStyles枚举值
            //this.SetStyle(ControlStyles.UserPaint, true);
            //this.SetStyle(ControlStyles.ResizeRedraw, true);
            //this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            //this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            //this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            
            sf = new StringFormat();
            sf.Trimming = StringTrimming.EllipsisCharacter; //超长时最后三个字符以...代替
            sf.FormatFlags |= StringFormatFlags.NoWrap;
            sf.FormatFlags &= StringFormatFlags.DirectionRightToLeft;
            this.Font = new Font("Tahoma", 8.25f, FontStyle.Regular);
            this.BackColor = Color.Transparent;
            this.AutoSize = false;
           // this.Height = 18;
        }

        #endregion

        #region Methods
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (e.Button == MouseButtons.Left)
            {
                //当鼠标松开时，标示变量为按下并松开了控件
                IsSelect = false;
                //刷新面板触发OnPaint重绘
                MainForm.Instance.Invoke(new Action(() =>
                {
                   

                    if (MainForm.Instance.faTabStrip1.SelectedItem.splic.Panel1.Controls.Count > 0)
                        if (MainForm.Instance.faTabStrip1.SelectedItem.ItemBrowser != null)
                        {
                            MainForm.Instance.faTabStrip1.SelectedItem.ItemBrowser.GetMainFrame().LoadUrl(this.URL);
                        }
                        else
                            ((CefWebBrowser)MainForm.Instance.faTabStrip1.SelectedItem.splic.Panel1.Controls[0]).Browser.GetMainFrame().LoadUrl(this.URL);
                    else
                        MainForm.Instance.faTabStrip1.SelectedItem.splic.Panel1.Controls.Add(MainForm.Instance.NewChromiumWeb(this.URL, MainForm.Instance.faTabStrip1.SelectedItem.TabIndex));
                  
                    if (MainForm.Instance.faTabStrip1.SelectedItem != null)
                        MainForm.Instance.textBoxXP1.Text = MainForm.Instance.faTabStrip1.SelectedItem.URL = this.uRL;

                    PublicClass.BrowserLoadingInfo(MainForm.Instance.faTabStrip1.SelectedItem);
                }

                ));
            }
            GC.Collect();
        }

        //重载绘画事件
        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            //因为上面调用了base会绘制原生控件 重刷一下背景清掉原生绘制 不然自己绘制的是重叠在原生绘制上
            //base.OnPaintBackground(pevent);
            //得到绘画句柄
            Graphics g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias; //描边时消除锯齿SetSmoothingMode, AntiAlias为指定消除锯齿的呈现
            g.CompositingQuality = CompositingQuality.HighQuality;



            Rectangle borderRect = new Rectangle(new Point(0, 0), new Size(266, 20));


            //测量用指定的 System.Drawing.Font 绘制并用指定的 System.Drawing.StringFormat 格式化的指定字符串
            //如果要固定标签长度不需检测Title的宽度
            SizeF textSize = new SizeF(266, 13);
            if (PublicClass.DpiX >= 120)
                textSize = new SizeF(266, 17);
            PointF textLoc = new PointF(borderRect.Left + 26, borderRect.Top + (borderRect.Height / 2) - (textSize.Height / 2));
           if(PublicClass.DpiX>=120)
            {
                textLoc = new PointF(borderRect.Left + 26, borderRect.Top + (borderRect.Height / 2) - (textSize.Height / 2) +2);

            }
            RectangleF textRect = new RectangleF(textLoc, textSize);
            textRect.Width = borderRect.Width - 26;

            if (borderRect.Width < 1 && borderRect.Height < 1)
                return;

            //判断使用什么资源图
            //if (m_bMouseHover)
            //{
            //Color fill = Color.FromArgb(255, 252, 244);// renderer.ColorTable.ButtonSelectedHighlight; //当鼠标移动到关闭按钮时的颜色Color.FromArgb(253, 244, 191);//
            //g.FillRectangle(new SolidBrush(fill), borderRect);
            //borderRect.Width--;
            //borderRect.Height--;
            //g.DrawRectangle(new Pen(Color.FromArgb(229, 195, 101)), borderRect);//new Pen(Color.FromArgb(229, 195, 101))
            //                                                                    //}

            //if (m_bMouseDown)
            //{
            //    Color fill = Color.FromArgb(255, 232, 166);// renderer.ColorTable.ButtonSelectedHighlight; //当鼠标移动到关闭按钮时的颜色Color.FromArgb(253, 244, 191);//

            //g.FillRectangle(new SolidBrush(fill), borderRect);

            //borderRect.Width--;
            //borderRect.Height--;

            //g.DrawRectangle(new Pen(Color.FromArgb(229, 195, 101)), borderRect);//new Pen(Color.FromArgb(229, 195, 101))
            //}
            g.DrawString(Title, defaultFont, new SolidBrush(ForeColor), textRect, sf);

            if (itemIcon != null)
                g.DrawImage(itemIcon, new Rectangle(borderRect.X + 5, borderRect.Height / 2 - 7, 16, 16));

        }

        #endregion

    }
    
}
