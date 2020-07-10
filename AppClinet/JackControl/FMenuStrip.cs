using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CefiBrowser
{
    [ToolboxItem(true)]
    public class FMenuStrip : ContextMenuStrip
    {
        //#region Fields

        //private Rectangle glyphRect = Rectangle.Empty;
        //private bool isMouseOver = false;
        //private Color MoveSelectedColor = Color.FromArgb(231, 231, 231); //��ǩδ��ѡ��ʱ����Ƶ��ð�ť����ɫ
        //private Rectangle btRect = Rectangle.Empty;
        //private Color offSelectedColor = Color.FromArgb(208, 208, 208); //��ǩδѡ��ʱ����ɫ
        //private Color onSelectedColor = Color.FromArgb(242, 242, 242); //��ǩ��ѡ��ʱ����ɫ
        //private Pen FormLineColor = new Pen(Color.FromArgb(169, 169, 169));// new Pen(SystemColors.ControlDarkDark); //Form�߿����Ч����ɫ
        //private Image itemIcon = null;//Logo icon

        //private string uRL = string.Empty; //�水ť��URL
        //private string title = string.Empty; //����
        private StringFormat sf = null;
        //private static Font defaultFont = new Font("Tahoma", 8.25f, FontStyle.Regular);
        //private string date_added = string.Empty; //����ʱ��
        //private string last_visited = string.Empty; //������ʱ��
        //private string type = string.Empty; //����
        //private string fatherID = string.Empty; //��ID
        //private string layer = string.Empty; //��ǲ�
        //private string iconBase64str = string.Empty;
        //private bool isSelect = false;

        //#endregion

        //#region Props

        //public bool IsMouseOver
        //{
        //    get { return isMouseOver; }
        //    set { isMouseOver = value; }
        //}

        ////public Rectangle Bounds
        ////{
        ////    get { return glyphRect; }
        ////    set { glyphRect = value; }
        ////}
        //public bool IsSelect
        //{
        //    get { return isSelect; }
        //    set { isSelect = value; }
        //}
        //public String URL
        //{
        //    get { return uRL; }
        //    set { uRL = value; }
        //}

        //[DefaultValue(null)]
        //public Image ItemIcon
        //{
        //    get { return itemIcon; }
        //    set { itemIcon = value; }
        //}

        //public string IconBase64str
        //{
        //    get { return iconBase64str; }
        //    set { iconBase64str = value; }

        //}

        //public string Title
        //{
        //    get { return title; }
        //    set { title = value; }

        //}

        //#endregion

        #region Ctor

        public FMenuStrip()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            sf = new StringFormat();
            sf.Trimming = StringTrimming.EllipsisCharacter; //����ʱ��������ַ���...����
            sf.FormatFlags |= StringFormatFlags.NoWrap;
            sf.FormatFlags &= StringFormatFlags.DirectionRightToLeft;
            this.AutoSize = false;
            this.Font = new Font("Tahoma", 8.25f, FontStyle.Regular);
            //  this.BackColor = Color.Transparent;
            
        }

        #endregion

        #region Methods

        
        //public void DrawGlyph(Graphics g)
        //{
        //    if (isMouseOver)
        //    {
        //        Color fill = Color.FromArgb(35, SystemColors.Highlight);
        //        g.FillRectangle(new SolidBrush(fill), glyphRect);
        //        Rectangle borderRect = glyphRect;

        //        borderRect.Width--;
        //        borderRect.Height--;

        //        g.DrawRectangle(SystemPens.Highlight, borderRect);
        //    }

        //    SmoothingMode bak = g.SmoothingMode;

        //    g.SmoothingMode = SmoothingMode.Default;

        //    using (Pen pen = new Pen(Color.Black))
        //    {
        //        pen.Width = 2;

        //        g.DrawLine(pen, new Point(glyphRect.Left + (glyphRect.Width / 3) - 2, glyphRect.Height / 2 - 1),
        //            new Point(glyphRect.Right - (glyphRect.Width / 3), glyphRect.Height / 2 - 1));
        //    }

        //    g.FillPolygon(Brushes.Black, new Point[]{
        //        new Point(glyphRect.Left + (glyphRect.Width / 3)-2, glyphRect.Height / 2+2),
        //        new Point(glyphRect.Right - (glyphRect.Width / 3), glyphRect.Height / 2+2),
        //        new Point(glyphRect.Left + glyphRect.Width / 2-1,glyphRect.Bottom-4)});

        //    g.SmoothingMode = bak;
        //}

        #endregion
    }
}
