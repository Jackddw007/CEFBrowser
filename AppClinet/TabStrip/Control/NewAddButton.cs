using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CefiBrowser
{
    public class NewAddButton
    {
        #region Fields
        private RectangleF glyphRect = RectangleF.Empty;
        private Rectangle dglyphRect = Rectangle.Empty;
        private bool isMouseOver = false;
        private bool visible;
        private ToolStripProfessionalRenderer renderer;
        private Color addNewButtonColor = Color.FromArgb(95, 99, 104);
        //private Color onSelectedColor = Color.FromArgb(234, 240, 255);//242, 242, 242); //标签被选中时的颜色
        private Color AddBTonMouserDown = Color.FromArgb(189, 190, 189); //标签被选中时的颜色
        private bool isMouseDown = false;
        private Color UderLineColor = Color.FromArgb(147, 147, 147); //画UnderLine的颜色
        private Color MoveSelectedColor = Color.FromArgb(231, 231, 231); //标签未被选中时鼠标移到该按钮的颜色
        private Color offSelectedColor = Color.FromArgb(208, 208, 208); //标签未选中时的颜色

        #endregion

        #region Props

        public bool IsMouseOver
        {
            get { return isMouseOver; }
            set { isMouseOver = value; }
        }

        public bool IsMouseDown
        {
            get { return isMouseDown; }
            set { isMouseDown = value; }
        }

        public RectangleF Bounds
        {
            get { return glyphRect; }
            set { glyphRect = value; }
        }

        public Rectangle DBouds
        {
            get { return dglyphRect; }
            set { dglyphRect = value; }
        }

        public bool Visible
        {
            get { return visible; }
            set { visible = value; }
        }

        #endregion

        #region Ctor

        internal NewAddButton(ToolStripProfessionalRenderer renderer) //CloseButton关闭按钮
        {
            this.renderer = renderer;
        }

        #endregion

        #region Methods

        public void DrawCross(Graphics g)
        {
            RectangleF AddNbt = glyphRect;
            GraphicsPath path = new GraphicsPath();
            LinearGradientBrush brush;

            #region 画新增按钮的方法
            path.AddLine(AddNbt.Left + 3, AddNbt.Y, AddNbt.Right, AddNbt.Y);
            path.AddLine(AddNbt.Right, AddNbt.Y, AddNbt.Right + AddNbt.Width / 3 - 3, AddNbt.Y + AddNbt.Height);
            path.AddLine(AddNbt.Right + AddNbt.Width / 3 - 3, AddNbt.Y + AddNbt.Height, AddNbt.Left + AddNbt.Width / 3, AddNbt.Y + AddNbt.Height);
            path.AddLine(AddNbt.Left + AddNbt.Width / 3, AddNbt.Y + AddNbt.Height, AddNbt.Left + 3, AddNbt.Y);
            #endregion

            brush = new LinearGradientBrush(AddNbt, offSelectedColor, offSelectedColor, LinearGradientMode.Vertical);

            if (isMouseOver)
            {
                brush = new LinearGradientBrush(AddNbt, MoveSelectedColor, MoveSelectedColor, LinearGradientMode.Vertical);
            }

            if (isMouseDown)
            {
                brush = new LinearGradientBrush(AddNbt, AddBTonMouserDown, AddBTonMouserDown, LinearGradientMode.Vertical);
            }



            g.FillPath(brush, path); //填充这个梯形框 //目前样式二是画的一个梯形框
            g.DrawPath(new Pen(brush, 1), path); ////绘制这个梯形框
            Region region = new System.Drawing.Region(path);
            g.DrawPath(new Pen(UderLineColor, 1.44f), path);
        }
        #endregion

    }
}
