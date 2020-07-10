using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CefiBrowser
{

    public partial class ButtonXP : UserControl
    {

        private bool isMouseDown=false;

        /// <summary>
        /// 鼠标单击事件
        /// </summary>
        public event EventHandler ButtonClick;

        /// <summary>
        /// 控件的默认图片
        /// </summary>
        private Image _imageDefault = null;
        private Image _DefaultImage = null;

        [Description("控件的默认图片")]
        public Image ImageDefault
        {
            get { return _imageDefault; }
            set
            {
                _imageDefault = value;
                label.Image = _DefaultImage= _imageDefault;
            }
        }
        /// <summary>
        /// 光标移动到控件上方显示的图片
        /// </summary>
        private Image _imageMove = null;
        [Description("光标移动到控件上方显示的图片")]
        public Image ImageMove
        {
            get { return _imageMove; }
            set { _imageMove = value; }
        }
        /// <summary>
        /// 光标离开控件显示的图片
        /// </summary>
        private Image _imageLeave = null;
        [Description("光标离开控件显示的图片")]
        public Image ImageLeave
        {
            get { return _imageLeave; }
            set { _imageLeave = value; }
        }

        /// <summary>
        /// 鼠标按下时控件显示的图片
        /// </summary>
        private Image _imageDown = null;
        [Description("鼠标按下时控件显示的图片")]
        public Image ImageDown
        {
            get { return _imageDown; }
            set { _imageDown = value; }
        }

        /// <summary>
        /// 控件的背景色
        /// </summary>
        private Color _backColorEX = Color.Transparent;

        [Description("控件的背景色")]
        public Color BackColorEX
        {
            get { return _backColorEX; }
            set
            {
                if (isMouseDown)
                {
                    _backColorEX = value;
                    label.BackColor = backColorDown;
                }
                else
                {
                    _backColorEX = value;
                    label.BackColor = _backColorEX;
                }
            }
        }

        /// <summary>
        /// 鼠标移动到控件上方显示的颜色
        /// </summary>
        private Color backColorMove = Color.Transparent;
        [Description("鼠标移动到控件上方显示的颜色")]
        public Color BackColorMove
        {
            get { return backColorMove; }
            set { backColorMove = value; }
        }
        /// <summary>
        /// 鼠标离开控件显示的背景色
        /// </summary>
        private Color backColorLeave = Color.Transparent;
        [Description("鼠标离开控件显示的背景色")]
        public Color BackColorLeave
        {
            get { return backColorLeave; }
            set { backColorLeave = value; }
        }
        /// <summary>
        /// 鼠标在控件上按下显示的背景色
        /// </summary>
        private Color backColorDown = Color.Transparent;
        [Description("鼠标在控件上按下显示的背景色")]
        public Color BackColorDown
        {
            get { return backColorDown; }
            set { backColorDown = value; }
        }

        /// <summary>
        /// 控件的文字显示
        /// </summary>
        private string textEX = "";
        [Description("显示的文字")]
        public string TextEX
        {
            get { return textEX; }
            set
            {
                textEX = value;
                label.Text = textEX;
            }
        }
        /// <summary>
        /// 文字的颜色
        /// </summary>
        private Color textColor = Color.Black;
        [Description("文字的颜色")]
        public Color TextColor
        {
            get { return textColor; }
            set
            {
                textColor = value;
                label.ForeColor = textColor;
            }
        }
        /// <summary>
        /// 用于显示文本的字体
        /// </summary>
        private Font fontM = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
        [Description("用于显示文本的字体")]
        public Font FontM
        {
            get { return fontM; }
            set
            {
                fontM = value;
                label.Font = fontM;
            }
        }


        public ButtonXP()
        {
            //AutoSize = true;
            //AutoScaleMode = AutoScaleMode.Dpi;
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            //this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            //this.BackColor = Color.Transparent;
            InitializeComponent();
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style |= (int)WinAPI.WindowStyles.WS_CLIPCHILDREN;
                return cp;
            }
        }
        /// <summary>
        /// 鼠标单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label_Click(object sender, EventArgs e)
        {
            if (ButtonClick != null)
            {
                ButtonClick(sender, e);
            }
        }

        /// <summary>
        /// 鼠标移动到控件上显示的背景色和背景图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label_MouseMove(object sender, MouseEventArgs e)
        {
            if (backColorMove != Color.Transparent)
            {
                BackColorEX = backColorMove;
            }
            if (_imageMove != null && !isMouseDown)
            {
                ImageDefault = _imageMove;
            }
            this.Invalidate();
        }

        
        /// <summary>
        /// 鼠标在控件上按下时显示的背景色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label_MouseDown(object sender, MouseEventArgs e)
        {
            if (backColorDown != Color.Transparent)
            {
                BackColorEX = backColorDown;
            }
            isMouseDown = true;

             
            if (_imageDown != null)
            {
                ImageDefault = _imageDown;
            }
            this.Invalidate();
        }

        private void label_LostFocus(object sender, EventArgs e)
        {
            //if (backColorLeave != Color.Transparent)
            //{
                BackColorEX = Color.Transparent;
            //}
        }
        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            BackColorEX = Color.Transparent;
        }
        /// <summary>
        /// 鼠标离开控件后显示的背景色和背景图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label_MouseLeave(object sender, EventArgs e)
        {
            isMouseDown = false;
            if (backColorLeave != Color.Transparent)
            {
                BackColorEX = backColorLeave;
            }

            if (_imageLeave != null)
            {
                ImageDefault = _imageLeave;
            }
            this.Invalidate();
        }

    }
}
