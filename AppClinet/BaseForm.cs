using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CefiBrowser
{
    public class BaseForm : Form
    {
        public BaseForm()
        {
            Initialize();
        }
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!_shadowEnabled) return;
            _reg = new Region(this.ClientRectangle);
            _reg.Exclude(new Rectangle(this.Width - _shadowWidth, 0, _shadowWidth, _shadowWidth + 3));
            _reg.Exclude(new Rectangle(0, this.Height - _shadowWidth, _shadowWidth + 3, _shadowWidth));
            this.Region = _reg;
            _reg.Dispose(); _reg = null;
        }
        /// <summary>
        /// 阴影宽度
        /// </summary>
        public int NewShadowWidth
        {
            get { return _shadowWidth; }
            set { _shadowWidth = value; this.Invalidate(); }
        }
        /// <summary>
        /// 阴影颜色
        /// </summary>
        public Color NewShadowColor
        {
            get { return _shadowColor; }
            set { _shadowColor = value; this.Invalidate(); }
        }
        /// <summary>
        /// 是否显示阴影
        /// </summary>
        [Browsable(false)]
        public bool NewShadowEnable
        {
            get { return _shadowEnabled; }
            set { _shadowEnabled = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            _startPoint = Control.MousePosition;
            _oldLocation = this.Location;
            _IsMouseDown = true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (!_IsMouseDown) return;
            Point end = Control.MousePosition;
            this.Location = new Point(_oldLocation.X + end.X - _startPoint.X, _oldLocation.Y + end.Y - _startPoint.Y);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            _IsMouseDown = false;
        }
        /// <summary>
        /// 窗体重绘
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.FillRectangle(new SolidBrush(_shadowColor), new Rectangle(this.Width - _shadowWidth, 0, _shadowWidth, this.Height));
            e.Graphics.FillRectangle(new SolidBrush(_shadowColor), new Rectangle(0, this.Height - _shadowWidth, this.Width, _shadowWidth));
        }
        /// <summary>
        /// 窗体尺寸变化
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (!_shadowEnabled) return;
            _reg = new Region(this.ClientRectangle);
            _reg.Exclude(new Rectangle(this.Width - _shadowWidth, 0, _shadowWidth, _shadowWidth + 3));
            _reg.Exclude(new Rectangle(0, this.Height - _shadowWidth, _shadowWidth + 3, _shadowWidth));
            this.Region = _reg;
            _reg.Dispose(); _reg = null;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Initialize()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            _shadowWidth = 4;
            _shadowColor = Color.DarkGray;
            _shadowEnabled = false;
            _reg = null;
        }

        private int _shadowWidth;
        private Color _shadowColor;
        private Region _reg;
        private bool _shadowEnabled;
        private bool _IsMouseDown;
        private Point _oldLocation;
        private Point _startPoint;

    }
}
