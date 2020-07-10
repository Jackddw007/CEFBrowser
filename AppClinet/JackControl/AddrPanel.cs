using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CefiBrowser
{
    [ToolboxItem(true)]
  public  class AddrPanel : Panel
    {
        private StringFormat sf = null;
        private static Font defaultFont = new Font("Tahoma", 12.66f, FontStyle.Regular);


        public AddrPanel()
        {
            //首先开启双缓冲，防止闪烁
            //双缓冲的一大堆设置 具体参数含义参照msdn的ControlStyles枚举值
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.ContainerControl, true);


            sf = new StringFormat();
            sf.Trimming = StringTrimming.EllipsisCharacter; //超长时最后三个字符以...代替
            sf.FormatFlags |= StringFormatFlags.NoWrap;
            sf.FormatFlags &= StringFormatFlags.DirectionRightToLeft;
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= (int)WinAPI.WindowStyles.WS_CLIPCHILDREN;
                return cp;
            }
        }
        /// <summary>
        /// 显示文件
        /// </summary>
        public string mStrText { set; get; }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            //画左边边线
            if (DrLeft)
                e.Graphics.DrawLine(SystemPens.ControlDark, new PointF(0, 0), new PointF(0, ClientRectangle.Height));
            //画上边边线
            if (DrTop)
                e.Graphics.DrawLine(SystemPens.ControlDark, new PointF(0, 0), new PointF(ClientRectangle.Width, 0));
            //画右边边线
            if (DrRight)
                e.Graphics.DrawLine(SystemPens.ControlDark, new PointF(ClientRectangle.Width - 1, 0), new PointF(ClientRectangle.Width - 1, ClientRectangle.Height));
            //画底边边线
            if (DrBottom)
                e.Graphics.DrawLine(SystemPens.ControlDark, new PointF(0, ClientRectangle.Height - 1), new PointF(ClientRectangle.Width, ClientRectangle.Height - 1));

            if (mStrText != null && mStrText != "")
            {
                Rectangle textRect = ClientRectangle;
              //  ForeColor = Color.Blue ;
                e.Graphics.DrawString(mStrText, defaultFont, new SolidBrush(ForeColor), textRect, sf);
                if(ForeColor == Color.Red)
                    e.Graphics.DrawLine(new Pen(Color.Red,1.66f), new PointF(0, ClientRectangle.Height/2), new PointF(ClientRectangle.Width-10, ClientRectangle.Height/2));

            }
            
        }


        public bool DrTop { set; get; }
        public bool DrLeft { set; get; }
        public bool DrRight { set; get; }
        public bool DrBottom { set; get; }


    }
}
