using CefiBrowser.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CefiBrowser
{
    [ToolboxItem(true)]
    public  class FormCloseButton : Button
    {
        public FormCloseButton()
        {
            //首先开启双缓冲，防止闪烁
            //双缓冲的一大堆设置 具体参数含义参照msdn的ControlStyles枚举值
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;
        }
        //用来标示是否鼠标正在悬浮在按钮上  true:悬浮在按钮上 false:鼠标离开了按钮
        private bool m_bMouseHover;
        //用来标示是否鼠标点击了按钮  true：按下了按钮 false：松开了按钮
        private bool m_bMouseDown;

        //重载鼠标悬浮的事件
        protected override void OnMouseEnter(EventArgs e)
        {
            //当鼠标进入控件时，标示变量为进入了控件
            m_bMouseHover = true;
            //刷新面板触发OnPaint重绘
            this.Invalidate();
            base.OnMouseEnter(e);
        }

        //重载鼠标离开的事件
        protected override void OnMouseLeave(EventArgs e)
        {
            //当鼠标离开控件时，标示变量为离开了控件
            m_bMouseHover = false;
            //刷新面板触发OnPaint重绘
            this.Invalidate();
            base.OnMouseLeave(e);
        }

        //重载鼠标按下的事件
        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            //当鼠标按下控件时，标示变量为按下了控件
            m_bMouseDown = true;
            //刷新面板触发OnPaint重绘
            this.Invalidate();
            base.OnMouseDown(mevent);
        }

        //重载鼠标松开的事件
        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            //当鼠标松开时，标示变量为按下并松开了控件
            m_bMouseDown = false;
            //刷新面板触发OnPaint重绘
            this.Invalidate();
            base.OnMouseUp(mevent);
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

            //判断使用什么资源图
            if(m_bMouseHover)
            {
                Color fill = Color.FromArgb(244, 84, 84);// 255, 252, 244);
                g.FillRectangle(new SolidBrush(fill), this.ClientRectangle);
                Rectangle borderRect = ClientRectangle;
                borderRect.Width--;
                borderRect.Height--;
                g.DrawRectangle(new Pen(Color.FromArgb(244, 84, 84)), borderRect);//new Pen(Color.FromArgb(229, 195, 101))
            }

            if (m_bMouseDown)
            {
                Color fill = Color.FromArgb(220, 72, 72);// 255, 232, 166);
                g.FillRectangle(new SolidBrush(fill), ClientRectangle);
                Rectangle borderRect = ClientRectangle;
                borderRect.Width--;
                borderRect.Height--;
                g.DrawRectangle(new Pen(Color.FromArgb(220, 72, 72)), borderRect);//new Pen(Color.FromArgb(229, 195, 101))
            }
            //Pen pen;

            //if (m_bMouseDown)
            //    pen = new Pen(Color.White, 1.96f);

            //else
            //    pen = new Pen(Color.Black, 1.96f);

            //using (pen)
            //{
            //    g.DrawLine(pen, ClientRectangle.Left + 13, ClientRectangle.Top + 8,
            //        ClientRectangle.Right - 13, ClientRectangle.Bottom - 8);

            //    g.DrawLine(pen, ClientRectangle.Right - 13, ClientRectangle.Top + 8,
            //        ClientRectangle.Left + 13, ClientRectangle.Bottom - 8);
            //}
            Rectangle iamgeRec = new Rectangle(new Point(ClientRectangle.Left + 6, ClientRectangle.Top + 3), new Size(22, 20));
            g.DrawImage(Resources.Close, iamgeRec);
        }
    }
}
