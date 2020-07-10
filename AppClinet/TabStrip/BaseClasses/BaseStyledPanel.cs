using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace CefiBrowser.BaseClasses
{
    [ToolboxItem(false)]
    public class BaseStyledPanel : ContainerControl
    {
        #region Fields

        private static ToolStripProfessionalRenderer renderer;

        #endregion

        #region Events

        public event EventHandler ThemeChanged;

        #endregion

        #region Ctor

        static BaseStyledPanel()
        {
           
            renderer = new ToolStripProfessionalRenderer();

        }



        public BaseStyledPanel()
        {

            // Set painting style for better performance.
            SetStyle(ControlStyles.ContainerControl, true);
            SetStyle(ControlStyles.UserPaint, false);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.Selectable, true);
            SetStyle(ControlStyles.StandardClick, true);
            //this.BackColor = Color.Transparent;
            //base.AllowDrop = true;


        }

        #endregion

        #region Methods

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Space || keyData == Keys.Up
              || keyData == Keys.Down || keyData == Keys.Left
              || keyData == Keys.Right)
            {
                MessageBox.Show(keyData.ToString(), "提示");
                //SendKeys.Send(keyData.ToString());
                return false;
            }
            return base.ProcessDialogKey(keyData);
        }
        protected override bool IsInputKey(Keys keyData)
        {
            if ((keyData & Keys.Alt) == Keys.Alt)
            {
                return false;
            }
            switch ((keyData & Keys.KeyCode))
            {
                case Keys.Up:
                case Keys.Down:
                    return true;
            }
            return base.IsInputKey(keyData);
        }
        protected override void OnSystemColorsChanged(EventArgs e)
        {
            base.OnSystemColorsChanged(e);
            UpdateRenderer();
            Invalidate();
        }

        protected virtual void OnThemeChanged(EventArgs e)
        {
            if (ThemeChanged != null)
                ThemeChanged(this, e);
        }

        private void UpdateRenderer()
        {
            if (!UseThemes)
            {
                renderer.ColorTable.UseSystemColors = true;
            }
            else
            {
                renderer.ColorTable.UseSystemColors = false;
            }
        }



        #endregion

        #region Props

        [Browsable(false)]
        public ToolStripProfessionalRenderer ToolStripRenderer
        {
            get { return renderer; }
        }

        [DefaultValue(true)]
        [Browsable(false)]
        public bool UseThemes
        {
            get
            {
                return VisualStyleRenderer.IsSupported && VisualStyleInformation.IsSupportedByOS && Application.RenderWithVisualStyles;
            }
        }

        #endregion
    }


}
