using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using CefiBrowser.Design;
using System.Drawing.Drawing2D;
using CefiBrowser.Properties;
using System.Reflection;
using Xilium.CefGlue;

namespace CefiBrowser
{

    [Designer(typeof(FATabStripItemDesigner))]
    [ToolboxItem(false)]
    [DefaultProperty("Title")]
    [DefaultEvent("Changed")]
    public class FATabStripItem : UserControl
    {
        #region Events

        public event EventHandler Changed;

        #endregion

        #region Fields
        private RectangleF stripRect = Rectangle.Empty;
        private RectangleF addNewRect = Rectangle.Empty;
        private RectangleF iconBounds = Rectangle.Empty;
        private Image image = null;
        private Image itemIcon = null;
        private bool canClose = true;
        private bool selected = false;
        private bool visible = true;
        private bool isDrawn = false;
        private string title = string.Empty;
        private string devToolsName = string.Empty;
        private bool muserSupport = false;

        private StringFormat sf = null;
        private static Font defaultFont = new Font("Tahoma", 8.25f, FontStyle.Regular);
        private bool browserIsLoading = false; //浏览器加载状态
        /// </summary>
        //private FavoriteTools itemFaTools = null;
        //private ChromeTools itemChTools = null;
        private bool itemCloseDown = false;

        private bool isMouseOver = false;
        private bool itemisMouseOver = false;
        private string rul = string.Empty; //存放RUL
        private Color MoveSelectedColor = Color.FromArgb(231, 231, 231); //标签未被选中时鼠标移到该按钮的颜色
        private Rectangle btRect = Rectangle.Empty;
        private Rectangle crossRect = Rectangle.Empty;
        private Color offSelectedColor = Color.FromArgb(208, 208, 208); //标签未选中时的颜色
        public static Color onSelectedColor = Color.FromArgb(242, 242, 242); //标签被选中时的颜色
        private Pen FormLineColor = new Pen(Color.FromArgb(169, 169, 169));// new Pen(SystemColors.ControlDarkDark); //Form边框边线效果颜色
        private Image image_G;
        private CefBrowser itemBrowser;
        public SplitContainer splic = new SplitContainer();
        public bool devToolsOpen = false;

        #endregion

        #region Props


        //  public event System.Windows.Forms.KeyEventHandler KeyDown;

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Size Size
        {
            get { return base.Size; }
            set { base.Size = value; }
        }

        [DefaultValue(true)]
        public new bool Visible
        {
            get { return visible; }
            set
            {
                if (visible == value)
                    return;

                visible = value;
                OnChanged();
            }
        }

        /// <summary>
        /// 内存节省模式
        /// </summary>
        public bool MemCostLower { set; get; }

        /// <summary>
        /// 线程开起时间
        /// </summary>
        public DateTime StrartTime { get; set; }

        public CefBrowser ItemBrowser
        {
            get { return itemBrowser; }
            set { itemBrowser = value; }
        }

        public bool DevToolsOpen
        {
            get { return devToolsOpen; }
            set { devToolsOpen = value; }
        }

        public RectangleF StripRect
        {
            get { return stripRect; }
            set { stripRect = value; }
        }

        [DefaultValue(null)]
        public Image ItemIcon
        {
            get { return itemIcon; }
            set { itemIcon = value; }
        }

        /// <summary>
        /// 父窗口是否最大化
        /// </summary>
        public bool ParentFormIsMax { set; get; }
        public RectangleF IconBounds
        {
            get { return iconBounds; }
            set { iconBounds = value; }
        }

        [DefaultValue(false)]
        public  bool IsPoPWindow { set ; get; }

        public string DevToolsName
        {
            get { return devToolsName; }
            set { devToolsName = value; }
        }



        public String URL
        {
            get { return rul; }
            set { rul = value; }
        }

        [Browsable(false)]
        [DefaultValue(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool IsDrawn
        {
            get { return isDrawn; }
            set
            {
                if (isDrawn == value)
                    return;

                isDrawn = value;
            }
        }
        /// <summary>
        /// 用来存放二维码识别的文件名
        /// </summary>
        public string OcrCodeFileName { set; get; }
        /// <summary>
        /// 存放验证码的URL，用到用CacheVie Tools导出这个文件名
        /// </summary>
        public string OcrCodeUrl { set; get; }
        /// <summary>
        /// Image of <see cref="FATabStripItem"/> which will be displayed
        /// on menu items.
        /// </summary>
        //[DefaultValue(null)]
        //public Image Image
        //{
        //    get { return image; }
        //    set { image = value; }
        //}

        [DefaultValue(true)]
        public bool CanClose
        {
            get { return canClose; }
            set { canClose = value; }
        }

        [DefaultValue("Name")]
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                if (title == value)
                    return;

                title = value;
                OnChanged();
            }
        }

        /// <summary>
        /// Gets and sets a value indicating if the page is selected.
        /// </summary>
        [DefaultValue(false)]
        [Browsable(false)]
        public bool Selected
        {
            get { return selected; }
            set
            {
                if (selected == value)
                    return;

                selected = value;
            }
        }

        [Browsable(false)]
        public string Caption
        {
            get { return Title; }
        }


        [DefaultValue(false)]
        public bool MuserSupport
        {
            get { return muserSupport; }
            set { muserSupport = value; }
        }

        //浏览器是否在加载状态
        public bool BrowserIsLoading
        {
            get { return browserIsLoading; }
            set { browserIsLoading = value; }
        }
        public bool IsMouseOver
        {
            get { return isMouseOver; }
            set { isMouseOver = value; }
        }

        //存放TabButton的Rect
        public Rectangle TabBounds
        {
            get { return btRect; }
            set { btRect = value; }
        }

        //存放Item关闭按钮的Rect
        public Rectangle ItemBounds
        {
            get { return crossRect; }
            set { crossRect = value; }
        }

        //记录鼠标是否移动到Item关闭按钮区域
        public bool ItemIsMouseOver
        {
            get { return itemisMouseOver; }
            set { itemisMouseOver = value; }
        }

        public Image Image_G
        {
            get { return image_G; }
            set { image_G = value; }
        }

        public bool ItemCloseDown
        {
            get { return itemCloseDown; }
            set { itemCloseDown = value; }
        }

        #endregion

        #region Ctor

        //int oldX, oldY;
        //bool SplitMouseDown = false;

        public FATabStripItem() : this(string.Empty, null)
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.ContainerControl, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            AutoScaleMode = AutoScaleMode.Font;
            sf = new StringFormat();
            sf.Trimming = StringTrimming.EllipsisCharacter; //超长时最后三个字符以...代替
            sf.FormatFlags |= StringFormatFlags.NoWrap;
            sf.FormatFlags &= StringFormatFlags.DirectionRightToLeft;
            this.AutoSize = true;
            splic.Panel2.SizeChanged += Panel2_SizeChanged;
            splic.Dock = DockStyle.Fill;
            splic.FixedPanel = FixedPanel.None;
            splic.Panel1MinSize = this.Width / 5 * 2;
            splic.SplitterWidth = 2;
            splic.SplitterIncrement = 1;
            splic.IsSplitterFixed = false;
            splic.Panel2Collapsed = true;
            splic.Parent = this;
            
            //this.SetTopLevel(true);
                
           // splic.Panel1.BackColor = Color.FromArgb(191, 195, 198);// 248, 249, 250);
            Controls.Add(splic);
        }

        private void Panel2_SizeChanged(object sender, EventArgs e)
        {
            if (splic.Panel1.Controls.Count > 0 && ItemBrowser != null)
            {
                IntPtr ptr = PublicClass.FindWindowEx(splic.Panel2.Handle, IntPtr.Zero, "CefBrowserWindow", DevToolsName);
               PublicClass.SetWindowPos(ptr, IntPtr.Zero,
                    0, 0, splic.Panel2.Width, splic.Panel2.Height,
                    SetWindowPosFlags.ShowWindow);
            }
        }

        public FATabStripItem(Control displayControl) : this(string.Empty, displayControl)
        {

        }

        public FATabStripItem(string caption, Control displayControl)
        {
            sf = new StringFormat();
            sf.Trimming = StringTrimming.EllipsisCharacter; //超长时最后三个字符以...代替
            sf.FormatFlags |= StringFormatFlags.NoWrap;
            sf.FormatFlags &= StringFormatFlags.DirectionRightToLeft;

            this.AutoSize = true;
            //SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            // SetStyle(ControlStyles.ResizeRedraw, true);
            //SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.ContainerControl, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            //this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            selected = false;
            Visible = true;
            //AutoSize = true;
            UpdateText(caption, displayControl);
            //this.SetTopLevel(true);
            //Add to controls
            if (displayControl != null)
                Controls.Add(displayControl);
        }


        #endregion

        #region IDisposable

            /// <summary>
            /// Handles proper disposition of the tab page control.
            /// </summary>
            /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            try
            {
                base.Dispose(disposing);

                if (disposing)
                {
                    if (image != null)
                        image.Dispose();
                }
            }
            catch
            { }
        }

        #endregion

        #region ShouldSerialize

        public bool ShouldSerializeIsDrawn()
        {
            return false;
        }

        public bool ShouldSerializeDock()
        {
            return false;
        }

        public bool ShouldSerializeControls()
        {
            return Controls != null && Controls.Count > 0;
        }

        public bool ShouldSerializeVisible()
        {
            return true;
        }

        #endregion

        #region Methods


        private void UpdateText(string caption, Control displayControl)
        {
            if (displayControl != null && displayControl is ICaptionSupport)
            {
                ICaptionSupport capControl = displayControl as ICaptionSupport;
                Title = capControl.Caption;
            }
            else if (caption.Length <= 0 && displayControl != null)
            {
                Title = displayControl.Text;
            }
            else if (caption != null)
            {
                Title = caption;
            }
            else
            {
                Title = string.Empty;
            }
        }

        public void Assign(FATabStripItem item)
        {
            Visible = item.Visible;
            Text = item.Text;
            CanClose = item.CanClose;
            Tag = item.Tag;
        }
        /// <summary>
        /// 当时Button左边到Form起始点的距离
        /// </summary>
        public float ButtontoForm_LeftDistance { set; get; }
        protected internal virtual void OnChanged()
        {
            if (Changed != null)
                Changed(this, EventArgs.Empty);
        }

        /// <summary>
        /// Return a string representation of page.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Caption;
        }

        //TabButton样式方法
        public void DrawCross(Graphics g, FATabStripItem currentItem, FATabStripItem SelectedItem)
        {
            RectangleF buttonRect = StripRect;
            if (buttonRect.Width < 1 && buttonRect.Height < 1)
                return;

            GraphicsPath path = new GraphicsPath();
            LinearGradientBrush brush;

            #region ButtonRect的样式

            //  float buttonHeight = buttonRect.Bottom ;
            if (!ParentFormIsMax)
            {
                path.AddLine(buttonRect.X, CefConstHelper.buttonHeight + CefConstHelper.DEF_Header_TopDis, buttonRect.Left + CefConstHelper.buttonPar, buttonRect.Y);
                path.AddLine(buttonRect.Left + CefConstHelper.buttonPar, buttonRect.Y, buttonRect.Right - CefConstHelper.buttonPar, buttonRect.Y);
                path.AddLine(buttonRect.Right - CefConstHelper.buttonPar, buttonRect.Y, buttonRect.Right, CefConstHelper.buttonHeight + CefConstHelper.DEF_Header_TopDis);
                path.CloseFigure();
            }
            else
            {
                path.AddLine(buttonRect.X, CefConstHelper.buttonHeight, buttonRect.Left + CefConstHelper.buttonPar, buttonRect.Y);
                path.AddLine(buttonRect.Left + CefConstHelper.buttonPar, buttonRect.Y, buttonRect.Right - CefConstHelper.buttonPar, buttonRect.Y);
                path.AddLine(buttonRect.Right - CefConstHelper.buttonPar, buttonRect.Y, buttonRect.Right, CefConstHelper.buttonHeight);
                path.CloseFigure();

            }

            ////using (GraphicsPath path = new GraphicsPath())
            ////{
            ////path.AddBezier(
            ////    new Point(buttonRect.X, buttonRect.Bottom + 2),
            ////    new Point(buttonRect.X + 3, buttonRect.Bottom - 2),
            ////    new Point(buttonRect.X + 3, buttonRect.Bottom - 2),
            ////    new Point(buttonRect.X, buttonRect.Bottom + 2));
            //path.AddLine(rect.X + 4, rect.Bottom - 4, rect.Left + 15 - 4, rect.Y + 4);
            ////path.AddBezier(
            ////    new Point(buttonRect.Left + 15 - 4, buttonRect.Y + 4),
            ////    new Point(buttonRect.Left + 15 - 3, buttonRect.Y + 2),
            ////    new Point(buttonRect.Left + 15 - 3, buttonRect.Y + 2),
            ////    new Point(buttonRect.Left + 15, buttonRect.Y));
            //path.AddLine(rect.Left + 15, rect.Y, rect.Right - 15, rect.Y);
            ////path.AddBezier(
            ////    new Point(buttonRect.Right - 15, buttonRect.Y),
            ////    new Point(buttonRect.Right - 15 + 3, buttonRect.Y + 2),
            ////    new Point(buttonRect.Right - 15 + 3, buttonRect.Y + 2),
            ////    new Point(buttonRect.Right - 15 + 4, buttonRect.Y + 4));
            //path.AddLine(rect.Right - 15 + 4, rect.Y + 4, rect.Right - 4, rect.Bottom - 4);
            ////path.AddBezier(
            ////    new Point(buttonRect.Right, buttonRect.Bottom),
            ////    new Point(buttonRect.Right - 3, buttonRect.Bottom - 3),
            ////    new Point(buttonRect.Right - 3, buttonRect.Bottom - 3),
            ////    new Point(buttonRect.Right + 1, buttonRect.Bottom + 1));
            //path.CloseFigure();

            //Region region = new System.Drawing.Region(path);

            //g.DrawPath(new Pen(Color.Black), path);

            #endregion
            brush = new LinearGradientBrush(buttonRect, offSelectedColor, offSelectedColor, LinearGradientMode.Horizontal);

            if (isMouseOver)
            {
                brush = new LinearGradientBrush(buttonRect, MoveSelectedColor, MoveSelectedColor, LinearGradientMode.Horizontal);
            }

            if (currentItem == SelectedItem)
            {
                brush = new LinearGradientBrush(buttonRect, onSelectedColor, onSelectedColor, LinearGradientMode.Horizontal);
            }

            g.FillPath(brush, path);

            if (!ParentFormIsMax)
            {
                g.DrawPath(SystemPens.Control, path);
                g.DrawLine(SystemPens.ControlDark, new PointF(buttonRect.X, CefConstHelper.buttonHeight + CefConstHelper.DEF_Header_TopDis), new PointF(buttonRect.Left + CefConstHelper.buttonPar, buttonRect.Y));
                g.DrawLine(SystemPens.ControlDark, new PointF(buttonRect.Left + CefConstHelper.buttonPar, buttonRect.Y), new PointF(buttonRect.Right - CefConstHelper.buttonPar, buttonRect.Y));
                g.DrawLine(SystemPens.ControlDark, new PointF(buttonRect.Right - CefConstHelper.buttonPar, buttonRect.Y), new PointF(buttonRect.Right, CefConstHelper.buttonHeight + CefConstHelper.DEF_Header_TopDis));
            }
            else
            {
                g.DrawPath(SystemPens.Control, path);
                g.DrawLine(SystemPens.ControlDark, new PointF(buttonRect.X, CefConstHelper.buttonHeight), new PointF(buttonRect.Left + CefConstHelper.buttonPar, buttonRect.Y));
                g.DrawLine(SystemPens.ControlDark, new PointF(buttonRect.Left + CefConstHelper.buttonPar, buttonRect.Y), new PointF(buttonRect.Right - CefConstHelper.buttonPar, buttonRect.Y));
                g.DrawLine(SystemPens.ControlDark, new PointF(buttonRect.Right - CefConstHelper.buttonPar, buttonRect.Y), new PointF(buttonRect.Right, CefConstHelper.buttonHeight));

            }

        }

        //Item的关闭按钮
        public void ItemCloseDraw(Graphics g)
        {
            if (itemisMouseOver)
            {
                Color fill = Color.FromArgb(196, 225, 255); //当鼠标移动到关闭按钮时的颜色Color.FromArgb(253, 244, 191);
                g.FillRectangle(new SolidBrush(fill), crossRect);
                Rectangle borderRect = crossRect;

                borderRect.Width--;
                borderRect.Height--;
                //边框颜色
                g.DrawRectangle(new Pen(Color.FromArgb(51, 153, 255)), borderRect);
            }

            if(itemCloseDown)
            {
                Color fill = Color.FromArgb(220, 72, 72);// 255, 232, 166);
                g.FillRectangle(new SolidBrush(fill), crossRect);
                Rectangle borderRect = crossRect;

                borderRect.Width--;
                borderRect.Height--;
                //边框颜色
                g.DrawRectangle(new Pen(Color.FromArgb(220, 72, 72)), borderRect);//new Pen(Color.FromArgb(229, 195, 101))

            }
            Pen pen;

            if (itemCloseDown)
                pen = new Pen(Color.White, 1.56f);

            else
                pen = new Pen(Color.Black, 1.56f);

            using (pen)
            {
                g.DrawLine(pen, crossRect.Left + 3, crossRect.Top + 3,
                    crossRect.Right - 5, crossRect.Bottom - 4);

                g.DrawLine(pen, crossRect.Right - 5, crossRect.Top + 3,
                    crossRect.Left + 3, crossRect.Bottom - 4);
            }
        }


        //DrawItem Image
        public void DrawItemImage(Graphics g)
        {
            if (itemIcon != null)
            {
                browserIsLoading = false;
                g.DrawImage(itemIcon, iconBounds);

            }
            else
            {
                if (browserIsLoading)
                {

                   g.DrawImage(image_G,iconBounds);// (Image)Resources.ResourceManager.GetObject("Marty_000" + (gpImageNumber).ToString("00")), iconBounds);
                }
            }
        }

      //draw Itemtext
        public void DrawItemText(Graphics g, RectangleF textRect)
        {
            textRect.Height = CefConstHelper.TextSizeH;

            if (browserIsLoading)
                textRect.X = iconBounds.Right + 3;

            if (MuserSupport) //如果是缓存隔离模式标签警示
                g.DrawString(Title, defaultFont, new SolidBrush(Color.Blue), textRect, sf);
            else
                g.DrawString(Title, defaultFont, new SolidBrush(ForeColor), textRect, sf);
        }
     
        #endregion
    }
}
