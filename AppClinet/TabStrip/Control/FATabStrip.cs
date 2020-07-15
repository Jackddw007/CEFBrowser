using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using CefiBrowser.BaseClasses;
using CefiBrowser.Design;
using CefiBrowser.Properties;

namespace CefiBrowser
{
    [Designer(typeof(FATabStripDesigner))]
    [DefaultEvent("TabStripItemSelectionChanged")]
    [DefaultProperty("Items")]
    [ToolboxItem(true)]
    [ToolboxBitmap("FATabStrip.bmp")]
    public class FATabStrip : BaseStyledPanel, ISupportInitialize, IDisposable
    {
        #region Static Fields

        internal static int PreferredWidth = 350;
        internal static int PreferredHeight = 200;

        #endregion

        #region Constants
        public float AddNbtCurrent_LocationY;// 实时存放新增按钮的right坐标
        private Color offSelectedColor = Color.FromArgb(208, 228, 228); //标签未选中时的颜色
        private Color onSelectedColor =  Color.FromArgb(242, 242, 242); //标签被选中时的颜色
        //private Color MoveSelectedColor = Color.FromArgb(231, 231, 231); //标签未被选中时鼠标移到该按钮的颜色
        private Color tabBackColor = Color.FromArgb(204, 204, 204); //标签后面背景色
        private Color UderLineColor = Color.FromArgb(169, 169, 169); //画UnderLine的颜色
        private Color AddBTonMouserDown = Color.FromArgb(189, 190, 189); //标签被选中时的颜色
        public bool FButtonIsDown = false; //表明已经按下关闭，最大化，最小按钮

        private Color colorMouseOn = Color.Silver;
        public static bool FormMaximum = true; //设置此变量决定主窗口最大化和非最大化时的Tab按钮离主界面离上方边界的距离
        public static bool onMouseDoubleClink = false; //控制当如果在标签上双击时不会进行Form最大化和最小化操作
        public bool ParentMouseMrafting; //标志鼠标是可以拖拽
        public static bool isAddNewButton = false;
        
        private SolidBrush brushFont = new SolidBrush(Color.Black);
        private Color backcolor = System.Drawing.SystemColors.Control;

        private Rectangle rectClose;
        private RectangleF rectIcon;
        public bool mouse_move_down; //拖拽的状态

        // 滑动
        //TabHeader thMouseDown = null;
      //public  Point pMouseDown = new Point();
      public  Point mMovePiont = new Point();

        #endregion

        #region Events

        public event TabStripItemClosingHandler TabStripItemClosing;
        public event TabStripItemChangedHandler TabStripItemSelectionChanged;
        public event HandledEventHandler MenuItemsLoading;
        public event EventHandler MenuItemsLoaded;
        public event EventHandler TabStripItemClosed;

        #endregion

        #region Fields
        /// <summary>
        /// 工具栏和标题栏高度
        /// </summary>
        public  int InControlHeight { set; get; }


        private Rectangle stripButtonRect = Rectangle.Empty;
        public static FATabStripItem selectedItem = null;

        //private ContextMenuStrip menu = null;
        //private FATabStripMenuGlyph menuGlyph = null;

        public NewAddButton NewAddRect;
        public static FATabStripItemCollection items;

        private StringFormat sf = null;
        private static Font defaultFont = new Font("Tahoma", 8.25f, FontStyle.Regular);


        //  private bool alwaysShowClose = true;
        private bool isIniting = false;
        //private bool alwaysShowMenuGlyph = true;
        //private bool menuOpen = false;

        /// <summary>
        /// 内存节省模式
        /// </summary>
        public bool MemCostLower { set; get; }
        #endregion

        #region Methods

        #region Public

        /// <summary>
        /// Returns hit test results
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        public HitTestResult HitTest(Point pt)
        {
            //if (appCloseButton.Bounds.Contains(pt))
            //    return HitTestResult.FormCloseButton;

            //if (appMinButton.Bounds.Contains(pt))
            //    return HitTestResult.FormMinButton;

            //if (appMaxNormalButton.Bounds.Contains(pt))
            //    return HitTestResult.FormMinNormalButton;

            if (NewAddRect.Bounds.Contains(pt))
                return HitTestResult.AddNewButton;
            //if(menuGlyph.Bounds.Contains(pt))
            //    return HitTestResult.MenuGlyph;

            if (GetTabItemByPoint(pt) != null)
                return HitTestResult.TabItem;

            //No other result is available.
            return HitTestResult.None;
        }

        /// <summary>
        /// Add a <see cref="FATabStripItem"/> to this control without selecting it.
        /// </summary>
        /// <param name="tabItem"></param>
        public void AddTab(FATabStripItem tabItem)
        {
            AddTab(tabItem, false);
        }
 
        /// <summary>
        /// 插入Tab功能
        /// </summary>
        /// <param name="tabItem"></param>
        /// <param name="autoSelect"></param>
        /// <param name="tabIndex"></param>
        public void InsetTab(FATabStripItem tabItem, bool autoSelect,int tabIndex)
        {
            if (CefConstHelper.Def_TabButton_White < 40) //如果标签宽度太小就不增加了，因为没实际意义
                return;
            CalcNewRectXY("AddItem");

            tabItem.Dock = DockStyle.Fill;
            if (tabItem.Title == "")
                tabItem.Title = "新标签页";


            tabItem.BackColor = SystemColors.Window;//onSelectedColor;// Color.FromArgb(204, 204, 204);
            tabItem.IsDrawn = true;
            tabItem.CanClose = true;
            tabItem.Height = CefConstHelper.Def_TabButton_Hight;

            Items.Insert(tabIndex, tabItem);

            if ((autoSelect && tabItem.Visible) || (tabItem.Visible && Items.DrawnCount < 1))
            {
                SelectedItem = tabItem;
                SelectItem(tabItem);
            }

        }
        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams cp = base.CreateParams;
        //        cp.ExStyle |= 0x02000000 | (int)WinAPI.WindowStyles.WS_CLIPCHILDREN;
        //        return cp;
        //    }
        //}
        /// <summary>
        /// Add a <see cref="FATabStripItem"/> to this control.
        /// User can make the currently selected item or not.
        /// </summary>
        /// <param name="tabItem"></param>
        public void AddTab(FATabStripItem tabItem, bool autoSelect)
        {
            if (CefConstHelper.Def_TabButton_White < 40) //如果标签宽度太小就不增加了，因为没实际意义
                return;
            CalcNewRectXY("AddItem");


            tabItem.Dock = DockStyle.Fill;
            tabItem.Title = "新标签页";
            tabItem.BackColor = SystemColors.Window;//onSelectedColor;// Color.FromArgb(204, 204, 204);
            tabItem.IsDrawn = true;
            tabItem.CanClose = true;
            //  tabItem.ItemIcon = Properties.Resources.icon_normal;//for test
           // tabItem.Height = CefConstHelper.Def_TabButton_Hight;

            Items.Add(tabItem);

            if ((autoSelect && tabItem.Visible) || (tabItem.Visible && Items.DrawnCount < 1))
            {
                SelectedItem = tabItem;
                SelectItem(tabItem);
                // SelectedItem.BackColor = offSelectedColor;// SystemColors.Window;
                //}
                this.Invalidate();
            }
        }
        /// <summary>
        /// 最大化和复原的时候计算离窗口上面的高度
        /// </summary>
        /// <param name="FuncName"></param>
        public void ControlSizeChanged()//string FuncName, MouseEventArgs e)
        {
            float nAddBt_LocationY = CefConstHelper.Def_TabtoLeft + ((float)CefConstHelper.Def_TabButton_White - CefConstHelper.TabButtons_DefOffset) * items.Count + (float)CefConstHelper.Def_TabButton_White;
            float newBTto_FormRight = ClientRectangle.Width - CefConstHelper.AddNbt_FormRight - 10; //10为新增按钮左边到左边最近一个标签按钮之间的宽度

            if (nAddBt_LocationY < newBTto_FormRight)
            {
                AddNbtCurrent_LocationY = newBTto_FormRight;
                CefConstHelper.Def_TabButton_White = AddNbtCurrent_LocationY / (items.Count);

                float uu = CefConstHelper.Def_TabtoLeft + ((float)CefConstHelper.Def_TabButton_White - CefConstHelper.TabButtons_DefOffset) * (items.Count - 1) + (float)CefConstHelper.Def_TabButton_White;// - AddNbtCurrent_LocationY) / (items.Count + 1);
                float mm = (AddNbtCurrent_LocationY - uu) / (items.Count + 1);

                CefConstHelper.Def_TabButton_White = CefConstHelper.Def_TabButton_White + mm;
                float uu2u = CefConstHelper.Def_TabtoLeft + ((float)CefConstHelper.Def_TabButton_White - CefConstHelper.TabButtons_DefOffset) * (items.Count - 1) + (float)CefConstHelper.Def_TabButton_White;
                if (CefConstHelper.Def_TabButton_White > CefConstHelper.Current_TabButton_White)
                    CefConstHelper.Def_TabButton_White = CefConstHelper.Current_TabButton_White;
            }
            UpdateLayout();
        }

        /// <summary>
        /// 计算新增按钮的公位置，这里要运行到浮点运算
        /// </summary>
        public void CalcNewRectXY(string DAction)
        {
            if (items.Count < 2)
                return;

            if (DAction == "AddItem")
            {
                float nAddBt_LocationY = CefConstHelper.Def_TabtoLeft + ((float)CefConstHelper.Def_TabButton_White - CefConstHelper.TabButtons_DefOffset) * items.Count + (float)CefConstHelper.Def_TabButton_White;

                float newBTto_FormRight = ClientRectangle.Width - CefConstHelper.AddNbt_FormRight - 10; //10为新增按钮左边到左边最近一个标签按钮之间的宽度

                if (nAddBt_LocationY > newBTto_FormRight)
                {
                    AddNbtCurrent_LocationY = newBTto_FormRight;
                    CefConstHelper.Def_TabButton_White = AddNbtCurrent_LocationY / (items.Count + 1);

                    float uu = CefConstHelper.Def_TabtoLeft + ((float)CefConstHelper.Def_TabButton_White - CefConstHelper.TabButtons_DefOffset) * items.Count + (float)CefConstHelper.Def_TabButton_White;// - AddNbtCurrent_LocationY) / (items.Count + 1);
                    float mm = (AddNbtCurrent_LocationY - uu) / (items.Count + 1);

                    CefConstHelper.Def_TabButton_White = CefConstHelper.Def_TabButton_White + mm;
                    float uu2u = CefConstHelper.Def_TabtoLeft + ((float)CefConstHelper.Def_TabButton_White - CefConstHelper.TabButtons_DefOffset) * items.Count + (float)CefConstHelper.Def_TabButton_White;

                }
            }

            if (DAction == "RemoveItem")
            {
                float nAddBt_LocationY = CefConstHelper.Def_TabtoLeft + ((float)CefConstHelper.Def_TabButton_White - CefConstHelper.TabButtons_DefOffset) * (items.Count - 1) + (float)CefConstHelper.Def_TabButton_White;

                float newBTto_FormRight = ClientRectangle.Width - CefConstHelper.AddNbt_FormRight - 10;

                if (nAddBt_LocationY == newBTto_FormRight)
                {
                    AddNbtCurrent_LocationY = newBTto_FormRight;
                    CefConstHelper.Def_TabButton_White = AddNbtCurrent_LocationY / (items.Count - 1);

                    float uu = CefConstHelper.Def_TabtoLeft + ((float)CefConstHelper.Def_TabButton_White - CefConstHelper.TabButtons_DefOffset) * (items.Count - 2) + (float)CefConstHelper.Def_TabButton_White;// - AddNbtCurrent_LocationY) / (items.Count + 1);
                    float mm = (AddNbtCurrent_LocationY - uu) / (items.Count - 1);

                    CefConstHelper.Def_TabButton_White = CefConstHelper.Def_TabButton_White + mm;
                    float uu2u = CefConstHelper.Def_TabtoLeft + ((float)CefConstHelper.Def_TabButton_White - CefConstHelper.TabButtons_DefOffset) * (items.Count - 2) + (float)CefConstHelper.Def_TabButton_White;
                    if (CefConstHelper.Def_TabButton_White > CefConstHelper.Current_TabButton_White)
                        CefConstHelper.Def_TabButton_White = CefConstHelper.Current_TabButton_White;

                }
                else
                {
                    AddNbtCurrent_LocationY = newBTto_FormRight;
                    CefConstHelper.Def_TabButton_White = AddNbtCurrent_LocationY / (items.Count - 1);

                    float uu = CefConstHelper.Def_TabtoLeft + ((float)CefConstHelper.Def_TabButton_White - CefConstHelper.TabButtons_DefOffset) * (items.Count - 2) + (float)CefConstHelper.Def_TabButton_White;// - AddNbtCurrent_LocationY) / (items.Count + 1);
                    float mm = (AddNbtCurrent_LocationY - uu) / (items.Count - 1);

                    CefConstHelper.Def_TabButton_White = CefConstHelper.Def_TabButton_White + mm;
                    float uu2u = CefConstHelper.Def_TabtoLeft + ((float)CefConstHelper.Def_TabButton_White - CefConstHelper.TabButtons_DefOffset) * (items.Count - 2) + (float)CefConstHelper.Def_TabButton_White;
                    if (CefConstHelper.Def_TabButton_White > CefConstHelper.Current_TabButton_White)
                        CefConstHelper.Def_TabButton_White = CefConstHelper.Current_TabButton_White;

                }
            }
            //this.Invalidate();
            //UpdateLayout();
        }
        /// <summary>
        /// Remove a <see cref="FATabStripItem"/> from this control.
        /// </summary>
        /// <param name="tabItem"></param>
        public void RemoveTab(FATabStripItem tabItem)
        {
            if (items.Count == 1)
                Application.Exit();

            CalcNewRectXY("RemoveItem");

            int tabIndex = Items.IndexOf(tabItem);

            if (tabIndex >= 0)
            {
                UnSelectItem(tabItem);
                Items.Remove(tabItem);
            }

            if (Items.Count > 0)
            {
                if (RightToLeft == RightToLeft.No)
                {
                    if (Items[tabIndex - 1] != null)
                    {
                        SelectedItem = Items[tabIndex - 1];
                    }
                    else
                    {
                        SelectedItem = Items.FirstVisible;
                    }
                }
                //else
                //{
                //    if (Items[tabIndex + 1] != null)
                //    {
                //        SelectedItem = Items[tabIndex + 1];
                //    }
                //    else
                //    {
                //        SelectedItem = Items.LastVisible;
                //    }
                //}
            }
            CefConstHelper.DEF_START_POS -= (CefConstHelper.Def_TabButton_White - CefConstHelper.TabButtons_DefOffset);
         //   this.Invalidate();

        }

        /// <summary>
        /// Get a <see cref="FATabStripItem"/> at provided point.
        /// If no item was found, returns null value.
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        public FATabStripItem GetTabItemByPoint(Point pt)
        {
            FATabStripItem item = null;
            bool found = false;

            for (int i = 0; i < Items.Count; i++)
            {
                FATabStripItem current = Items[i];

                if (current.StripRect.Contains(pt) && current.Visible && current.IsDrawn)
                {
                    item = current;
                    found = true;
                }

                if (found)
                    break;
            }

            return item;
        }

        /// <summary>
        /// 重右边开始计算，这样是为了照顾拖拽当前标签向右移时要正确的命中其他Item
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        public FATabStripItem GetTabItemByPointFromRigt(Point pt)
        {
            FATabStripItem item = null;
            bool found = false;

            for (int i = Items.Count -1; i >=0; i--)
            {
                FATabStripItem current = Items[i];

                if (current.StripRect.Contains(pt) && current.Visible && current.IsDrawn)
                {
                    item = current;
                    found = true;
                }

                if (found)
                    break;
            }

            return item;
        }

        //取消Form按钮的选中状态
        public void CancelFromButton()
        {
            if (FButtonIsDown == false)
            {
                NewAddRect.IsMouseOver = false;
                NewAddRect.IsMouseDown = false;

                if (SelectedItem != null)
                {
                    SelectedItem.ItemCloseDown = false;
                    SelectedItem.ItemIsMouseOver = false;
                }
                Invalidate();
            }
           

        }
        #endregion

        #region Internal

        internal void UnDrawAll()
        {
            for (int i = 0; i < Items.Count; i++)
            {
                Items[i].IsDrawn = false;
            }
        }

        internal void SelectItem(FATabStripItem tabItem)
        {
            tabItem.Dock = DockStyle.Fill;
            tabItem.Visible = true;
            tabItem.Selected = true;
        }

        internal void UnSelectItem(FATabStripItem tabItem)
        {
            //tabItem.Visible = false;
            tabItem.Selected = false;
        }

        #endregion

        #region Protected

        /// <summary>
        /// Fires <see cref="TabStripItemClosing"/> event.
        /// </summary>
        /// <param name="e"></param>
        protected internal virtual void OnTabStripItemClosing(TabStripItemClosingEventArgs e)
        {
            if (TabStripItemClosing != null)
                TabStripItemClosing(e);
        }

        /// <summary>
        /// Fires <see cref="TabStripItemClosed"/> event.
        /// </summary>
        /// <param name="e"></param>
        protected internal virtual void OnTabStripItemClosed(EventArgs e)
        {
            if (TabStripItemClosed != null)
                TabStripItemClosed(this, e);
        }

        /// <summary>
        /// Fires <see cref="MenuItemsLoading"/> event.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnMenuItemsLoading(HandledEventArgs e)
        {
            if (MenuItemsLoading != null)
                MenuItemsLoading(this, e);
        }
        /// <summary>
        /// Fires <see cref="MenuItemsLoaded"/> event.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnMenuItemsLoaded(EventArgs e)
        {
            if (MenuItemsLoaded != null)
                MenuItemsLoaded(this, e);
        }

        /// <summary>
        /// Fires <see cref="TabStripItemSelectionChanged"/> event.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnTabStripItemChanged(TabStripItemChangedEventArgs e)
        {
            if (TabStripItemSelectionChanged != null)
                TabStripItemSelectionChanged(e);
        }

        /// <summary>
        /// Loads menu items based on <see cref="FATabStripItem"/>s currently added
        /// to this control.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnMenuItemsLoad(EventArgs e)
        {
            //menu.RightToLeft = RightToLeft;
            //menu.Items.Clear();

            //for (int i = 0; i < Items.Count; i++)
            //{
            //    FATabStripItem item = Items[i];
            //    if (!item.Visible)
            //        continue;

            //    ToolStripMenuItem tItem = new ToolStripMenuItem(item.Title);
            //    tItem.Tag = item;
            //    tItem.Image = item.Image;
            //    menu.Items.Add(tItem);
            //}

            //OnMenuItemsLoaded(EventArgs.Empty);
        }

        #endregion

        #region Overrides

        protected override void OnRightToLeftChanged(EventArgs e)
        {
            base.OnRightToLeftChanged(e);
            UpdateLayout();
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (selectedItem == null)
            {
                FATabStripItem newitem = new FATabStripItem();
                AddTab(newitem, true);
                newitem = null;
                ControlSizeChanged();
            }
            //画Tabstrip边框
            // SetDefaultSelected();
            //Rectangle borderRc = ClientRectangle;
            //borderRc.Width--;
            //borderRc.Height--;
            //e.Graphics.DrawRectangle(SystemPens.ControlDark, borderRc);
            CefConstHelper.DEF_START_POS = 10;
            if (PublicClass.DpiX > 150)
            {
                CefConstHelper.DEF_START_POS = 16;
            }
            else if (PublicClass.DpiX >= 120 && PublicClass.DpiX <= 150)
            {
                CefConstHelper.DEF_START_POS = 13;
            }

            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;

            #region Draw Pages
            FATabStripItem currentItem = new FATabStripItem();
            for (int i = 0; i < Items.Count; i++)
            {
                currentItem = Items[i];
                if (!currentItem.Visible && !DesignMode)
                    continue;
                OnCalcTabPage(e.Graphics, currentItem);
                if (!AllowDraw(currentItem))
                    continue;
                if (RemoveALLItem == false)
                {
                    currentItem.TabIndex = i;//这个设置有助于新Tab的插入会在要插入的Tab的右边
                    if (currentItem.Controls.Count > 0)
                        currentItem.Controls[0].TabIndex = i;
                }
                OnDrawTabPage(e.Graphics, currentItem);

            }
            currentItem = null;
            #endregion


            //置前显示当前按钮，以免被其他按钮当住
            OnDrawTabPage(e.Graphics, SelectedItem);

            if (mouse_move_down != true)
            {
                if (NewAddRect.Bounds.Width > 1 || NewAddRect.Bounds.Height > 1)
                    //新增按钮
                    NewAddRect.DrawCross(e.Graphics);

            }
            #region Draw UnderPage Line
            //  float UderLineCost = 0;

            if (SelectedItem != null && SelectedItem.IsDrawn)
            {
                if (ParentForm.WindowState == FormWindowState.Maximized)
                {
                    PointF end = new PointF(SelectedItem.StripRect.Left + 1f, CefConstHelper.buttonHeight - CefConstHelper.UderLineCost);
                    e.Graphics.DrawLine(SystemPens.ControlDark, new PointF(0, CefConstHelper.buttonHeight - CefConstHelper.UderLineCost), new PointF(ClientRectangle.Width, CefConstHelper.buttonHeight - CefConstHelper.UderLineCost));
                    e.Graphics.DrawLine(new Pen(onSelectedColor, 1f), end, new PointF(SelectedItem.StripRect.Right - 1f, CefConstHelper.buttonHeight - CefConstHelper.UderLineCost));
                }
                else
                {
                    PointF end = new PointF(SelectedItem.StripRect.Left + 1f, CefConstHelper.buttonHeight + CefConstHelper.DEF_Header_TopDis - CefConstHelper.UderLineCost);
                    e.Graphics.DrawLine(SystemPens.ControlDark, new PointF(0, CefConstHelper.buttonHeight + CefConstHelper.DEF_Header_TopDis - CefConstHelper.UderLineCost), new PointF(ClientRectangle.Width, CefConstHelper.buttonHeight + CefConstHelper.DEF_Header_TopDis - CefConstHelper.UderLineCost));
                    e.Graphics.DrawLine(new Pen(onSelectedColor, 1f), end, new PointF(SelectedItem.StripRect.Right - 1f, CefConstHelper.buttonHeight + CefConstHelper.DEF_Header_TopDis - CefConstHelper.UderLineCost));

                }
            }
            //Rectangle borderRc = ParentForm.ClientRectangle; ;
            //borderRc.Width--;
            //borderRc.Height--;
            //e.Graphics.DrawRectangle(new Pen(SystemColors.ControlDark, 1), borderRc);

            #endregion
        }

        #region 鼠标事件
        //public void FATabMouseDown(MouseEventArgs e, string FuncName)
        //{
        //    if (e.Button == MouseButtons.Right)
        //        return;

        //    HitTestResult result = HitTest(e.Location);
        //    FATabStripItem item = GetTabItemByPoint(e.Location);

        //    if (result == HitTestResult.TabItem)
        //    {
        //        if (item != null)
        //        {
        //            SelectedItem = item;
        //            if (item.splic.Panel1.Controls.Count > 0 && MemCostLower)
        //            {
        //                Process[] allProcess = Process.GetProcesses();
        //                foreach (Process p in allProcess)
        //                {
        //                    if (p.ProcessName.ToLower() + ".exe" == CefConstHelper.CefiBrowserName.ToLower())
        //                    {
        //                        if (p.StartTime.ToString().ToLower() == item.StrartTime.ToString().ToLower())
        //                        {
        //                            IntPtr intPtrCef = p.Handle;
        //                            PublicClass.SetProcessWorkingSetSize(intPtrCef, -1, -1);
        //                            // break;
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        //这里Item按钮鼠标按下事件要改成只高亮显示，不关闭 ？？后继完成
        //        if (item.ItemBounds.Contains(e.Location))
        //        {
        //            FButtonIsDown = true;
        //            item.ItemCloseDown = true;
        //            Invalidate(item.ItemBounds);
        //        }
        //    }
        //    else if (result == HitTestResult.AddNewButton)
        //    {
        //        NewAddRect.IsMouseDown = true;
        //        Invalidate();// NewAddRect.DBouds);
        //        FButtonIsDown = true;
        //    }
        //    else
        //    {
        //        CancelFromButton();
        //    }

        //    //ControlSizeChanged(FuncName, e);

        //    if (NewAddRect.Bounds.Contains(e.Location))
        //    {
        //        ParentMouseMrafting = false; //为False不能拖拽
        //    }
        //    else if (item != null)
        //    {
        //        ParentMouseMrafting = false; //为False不能拖拽
        //    }
        //    else
        //    {
        //        if (FuncName == "1")
        //        {
        //            if (item != null)
        //            {
        //                ParentMouseMrafting = false; //为False不能拖拽
        //            }
        //        }
        //        if (e.Y <= NewAddRect.Bounds.Bottom + 5)
        //        {
        //            ParentMouseMrafting = true; //为False不能拖拽

        //        }
        //        else
        //            ParentMouseMrafting = false;

        //        //if (FuncName == "2")
        //        //{
        //        //    ParentMouseMrafting = false;
        //        //    if (NewAddRect.Bounds.Contains(e.Location) != true && SelectedItem.ItemBounds.Contains(e.Location) != true)
        //        //    {
        //        //        FormMaxNormal("2");
        //        //    }
        //        //}
        //    }
        //    if (e.Button == System.Windows.Forms.MouseButtons.Left)
        //    {
        //        pMouseDown = e.Location;
        //        FButtonIsDown = true;
        //    }
        //    mouse_move_down = false; 
        //}

        //protected override void OnMouseMove(MouseEventArgs e)
        //{
        //    base.OnMouseMove(e);
        //    HitTestResult result = HitTest(e.Location);
        //    FATabStripItem item = GetTabItemByPoint(e.Location);

        //    #region 新增按钮

        //    if (FButtonIsDown == false)
        //        if (result == HitTestResult.AddNewButton)
        //        {
        //            if (NewAddRect.Bounds.Contains(e.Location))
        //            {
        //                NewAddRect.IsMouseOver = true;
        //                Invalidate();
        //            }
        //            else
        //            {
        //                NewAddRect.IsMouseOver = false;
        //                Invalidate();
        //            }

        //        }
        //        else
        //        {
        //            CancelFromButton();
        //        }

        //    #endregion

        //    #region 当鼠标移动到tab按钮和关闭按钮区域的时候标签按钮颜色的改变
        //    //这里先把其他的ItemMouseOver关掉
        //    for (int i = 0; i < items.Count; i++)
        //    {
        //        items[i].IsMouseOver = false;
        //        Invalidate(items[i].TabBounds);

        //        items[i].ItemIsMouseOver = false;
        //        Invalidate(items[i].ItemBounds);

        //    }

        //    if (item != null && FButtonIsDown == false)
        //    {
        //        if (selectedItem != item)
        //        {
        //            item.IsMouseOver = true;
        //            Invalidate(item.TabBounds);
        //        }
        //        else
        //        {
        //            item.IsMouseOver = false;
        //            Invalidate(item.TabBounds);
        //        }

        //        if (item.ItemBounds.Contains(e.Location))
        //        {
        //            item.ItemIsMouseOver = true;
        //            Invalidate(item.ItemBounds);
        //        }
        //        else
        //        {
        //            item.ItemIsMouseOver = false;
        //            Invalidate(item.ItemBounds);
        //        }
        //    }
        //    #endregion
        //    if (e.Button == MouseButtons.Left)
        //    {
        //        //判断鼠标按下左键的同时是而且在移动，即拖拽行为发生
        //        if (e.X - pMouseDown.X > 6 || pMouseDown.X - e.X > 6) //向右或是向左移动
        //        {
        //            mouse_move_down = true;
        //            if (item != null && Items.Count > 1)
        //            {
        //                if (item != selectedItem)
        //                    Items.SwichItem(selectedItem, GetTabItemByPoint(e.Location));
        //                else
        //                    Items.SwichItem(selectedItem, GetTabItemByPointFromRigt(e.Location));
        //            }
        //        }
        //    }

        //    mMovePiont = e.Location;
        //    //this.Invalidate();
        //}


        //protected override void OnMouseUp(MouseEventArgs e)
        //{
        //    base.OnMouseUp(e);


        //    if (HitTest(e.Location) == HitTestResult.AddNewButton && FButtonIsDown && e.Button != MouseButtons.Right && mouse_move_down != true)
        //    {
        //        NewAddRect.IsMouseOver = false;
        //        NewAddRect.IsMouseDown = false;
        //        FATabStripItem newitem = new FATabStripItem();
        //        AddTab(newitem, true);
        //    }
        //    if (SelectedItem.ItemBounds.Contains(e.Location) && FButtonIsDown)
        //    {
        //        if (SelectedItem != null)
        //        {
        //            TabStripItemClosingEventArgs args = new TabStripItemClosingEventArgs(SelectedItem);
        //            OnTabStripItemClosing(args);
        //            if (!args.Cancel && SelectedItem.CanClose)
        //            {
        //                RemoveTab(SelectedItem);
        //                OnTabStripItemClosed(EventArgs.Empty);
        //            }
        //        }
        //    }

        //    FButtonIsDown = false;
        //    mouse_move_down = false;

        //}


        //protected override void OnMouseLeave(EventArgs e)
        //{
        //    base.OnMouseLeave(e);

        //    FButtonIsDown = false; //鼠标离开的时候重置这个标志

        //    CancelFromButton(); //取消Form按钮的选中状态


        //    //这里先把其他的ItemMouseOver及itemIsMouseOver关掉
        //    for (int i = 0; i < items.Count; i++)
        //    {
        //        items[i].IsMouseOver = false;
        //        Invalidate(items[i].TabBounds);

        //        items[i].ItemIsMouseOver = false;
        //        Invalidate(items[i].ItemBounds);
        //    }
        //    //刷新控件
        //    Invalidate();
        //}

        #endregion
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (isIniting)
                return;
            if (ParentForm != null && items.Count > 0)
            {
                float nAddBt_LocationY = CefConstHelper.Def_TabtoLeft + ((float)CefConstHelper.Def_TabButton_White - CefConstHelper.TabButtons_DefOffset) * items.Count + (float)CefConstHelper.Def_TabButton_White;

                float newBTto_FormRight = ClientRectangle.Width - CefConstHelper.AddNbt_FormRight - 10; //10为新增按钮左边到左边最近一个标签按钮之间的宽度

                if (nAddBt_LocationY > newBTto_FormRight)
                {
                    AddNbtCurrent_LocationY = newBTto_FormRight;
                    CefConstHelper.Def_TabButton_White = AddNbtCurrent_LocationY / (items.Count);

                    float uu = CefConstHelper.Def_TabtoLeft + ((float)CefConstHelper.Def_TabButton_White - CefConstHelper.TabButtons_DefOffset) * (items.Count - 1) + (float)CefConstHelper.Def_TabButton_White;// - AddNbtCurrent_LocationY) / (items.Count + 1);
                    float mm = (AddNbtCurrent_LocationY - uu) / (items.Count);

                    CefConstHelper.Def_TabButton_White = CefConstHelper.Def_TabButton_White + mm;
                    float uu2u = CefConstHelper.Def_TabtoLeft + ((float)CefConstHelper.Def_TabButton_White - CefConstHelper.TabButtons_DefOffset) * (items.Count - 1) + (float)CefConstHelper.Def_TabButton_White;
                    if (CefConstHelper.Def_TabButton_White > CefConstHelper.Current_TabButton_White)
                        CefConstHelper.Def_TabButton_White = CefConstHelper.Current_TabButton_White;
                }
                if (nAddBt_LocationY < newBTto_FormRight) //当左右缩放窗口时发生
                {
                    AddNbtCurrent_LocationY = newBTto_FormRight;
                    CefConstHelper.Def_TabButton_White = AddNbtCurrent_LocationY / (items.Count);
                    float uu = CefConstHelper.Def_TabtoLeft + ((float)CefConstHelper.Def_TabButton_White - CefConstHelper.TabButtons_DefOffset) * (items.Count - 1) + (float)CefConstHelper.Def_TabButton_White;// - AddNbtCurrent_LocationY) / (items.Count + 1);
                    float mm = (AddNbtCurrent_LocationY - uu) / (items.Count);

                    CefConstHelper.Def_TabButton_White = CefConstHelper.Def_TabButton_White + mm;
                    float uu2u = CefConstHelper.Def_TabtoLeft + ((float)CefConstHelper.Def_TabButton_White - CefConstHelper.TabButtons_DefOffset) * (items.Count - 1) + (float)CefConstHelper.Def_TabButton_White;
                    if (CefConstHelper.Def_TabButton_White > CefConstHelper.Current_TabButton_White)
                        CefConstHelper.Def_TabButton_White = CefConstHelper.Current_TabButton_White;

                }

            }
            
            //UpdateLayout();
        }

        #endregion

        #region Private

        private bool AllowDraw(FATabStripItem item)
        {
            bool result = true;

            if (RightToLeft == RightToLeft.No)
            {
                if (item.StripRect.Right >= stripButtonRect.Width)
                    result = false;
            }
            else
            {
                if (item.StripRect.Left <= stripButtonRect.Left)
                    return false;
            }

            return result;
        }

        //public void SetDefaultSelected()
        //{
        //    if (selectedItem == null && Items.Count > 0)
        //        SelectedItem = Items[items.Count - 1];

        //    for (int i = 0; i < Items.Count; i++)
        //    {
        //        FATabStripItem itm = Items[i];
        //        itm.Dock = DockStyle.Fill;
        //    }
        //}


        //public void FormMaxNormal(string par)
        //{
        //    if(par=="2")
        //    {
        //        if(ParentForm.WindowState == FormWindowState.Maximized)
        //        {
        //            ParentForm.WindowState = FormWindowState.Normal;
        //        }
        //        else
        //        {
                    
        //            ParentForm.WindowState = FormWindowState.Maximized;
        //        }

        //    }
        //}
        /// <summary>
        /// 检查父窗体是否最大化
        /// </summary>
        public void CalcParentFormMax()
        {
            if (ParentForm != null)
                if (ParentForm.WindowState == FormWindowState.Maximized)
                    FormMaximum = true;
                else
                    FormMaximum = false;
        }

        private void OnCalcTabPage(Graphics g, FATabStripItem currentItem)
        {
            Font currentFont = Font;
            if (currentItem == SelectedItem)
                currentFont = new Font(Font, FontStyle.Bold);

            //测量用指定的 System.Drawing.Font 绘制并用指定的 System.Drawing.StringFormat 格式化的指定字符串
            //如果要固定标签长度不需检测Title的宽度
            // SizeF textSize = g.MeasureString(currentItem.Title, SystemFonts.DefaultFont, new SizeF(CefConstHelper.Def_TabButton_White, 10), sf);
            // SizeF textSize = new SizeF(CefConstHelper.Def_TabButton_White, currentFont.Height); //10p这个文字框的为高度，120为宽度，

            CalcParentFormMax();
            RectangleF buttonRect;

            if (FormMaximum != true)  //当窗口不是最大化的时候Y坐标会改变,具体为TabButton高度的一半
            {
                //buttonRect是定义标签button的边框宽和高及坐标,28为高，X,Y为坐标
                buttonRect = new RectangleF(CefConstHelper.DEF_START_POS, CefConstHelper.DEF_Header_TopDis, CefConstHelper.Def_TabButton_White, CefConstHelper.buttonHeight);

                currentItem.StripRect = buttonRect;
                CefConstHelper.DEF_START_POS += (CefConstHelper.Def_TabButton_White - CefConstHelper.TabButtons_DefOffset);
                NewAddRect.Bounds = new RectangleF(CefConstHelper.DEF_START_POS + 10, (CefConstHelper.buttonHeight + CefConstHelper.DEF_Header_TopDis) / 5 + CefConstHelper.DEF_Header_TopDis, CefConstHelper.AddnewButton_Width, CefConstHelper.buttonHeight / 2);
            }
            else 
            {
                //buttonRect是定义标签button的边框宽和高及坐标,28为高，X,Y为坐标
                //窗口最大化的时候Y坐标为1
                buttonRect = new RectangleF(CefConstHelper.DEF_START_POS, this.Location.Y-2, CefConstHelper.Def_TabButton_White, CefConstHelper.buttonHeight);
                currentItem.StripRect = buttonRect;
                CefConstHelper.DEF_START_POS += (CefConstHelper.Def_TabButton_White - CefConstHelper.TabButtons_DefOffset);
                NewAddRect.Bounds = new RectangleF(CefConstHelper.DEF_START_POS + 10, (CefConstHelper.buttonHeight + CefConstHelper.DEF_Header_TopDis) / 5, CefConstHelper.AddnewButton_Width, CefConstHelper.buttonHeight / 2); //画新增按钮
            }
            PublicClass.DEF_START_POS = CefConstHelper.DEF_START_POS;
            if (mouse_move_down)//拖拽状态
            {
                buttonRect.X = mMovePiont.X - CefConstHelper.Def_TabButton_White /2;
                if (buttonRect.X < 3)
                    buttonRect.X = 3;

                if (buttonRect.X > ClientRectangle.Width - CefConstHelper.AddNbt_FormRight - buttonRect.Width + 30)
                    buttonRect.X = ClientRectangle.Width - CefConstHelper.AddNbt_FormRight - buttonRect.Width + 30;
                selectedItem.StripRect = buttonRect;
            }
            
        }
        private void BeginAnimate()//开始动画方法
        {
            if (m_img != null)
            {
                //当gif动画每隔一定时间后，都会变换一帧那么就会触发一事件，该方法就是将当前image每变换一帧时，都会调用当前这个委托所关联的方法。
                ImageAnimator.Animate(m_img, evtHandler);
            }
        }
        private void OnImageAnimate(Object sender, EventArgs e)//委托所关联的方法
        {
            for (int i = 0; i < items.Count; i++)
                if (items[i].BrowserIsLoading)
                {
                    this.Invalidate();
                    break;
                }
            //该方法中，只是使得当前这个winform重绘，然后去调用该winform的OnPaint（）方法进行重绘)
        }
        private void UpdateImage()
        {//获得当前gif动画的下一步需要渲染的帧，当下一步任何对当前gif动画的操作都是对该帧进行操作
            ImageAnimator.UpdateFrames(m_img);
        }
        private void OnDrawTabPage(Graphics g, FATabStripItem currentItem)
        {
            Font currentFont = Font;
            SizeF textSize = g.MeasureString(currentItem.Title, currentFont, new SizeF(CefConstHelper.Def_TabButton_White, 10), sf);

            #region 画TabButton的样式
            RectangleF buttonRect = currentItem.StripRect;
            // currentItem.TabBounds = new Rectangle((int)buttonRect.X, (int)buttonRect.Y, (int)buttonRect.Width, (int)buttonRect.Height);

            #region Tab style
            if (ParentForm.WindowState == FormWindowState.Maximized)
                currentItem.ParentFormIsMax = selectedItem.ParentFormIsMax = true;
            else
                currentItem.ParentFormIsMax = selectedItem.ParentFormIsMax = false;
            currentItem.DrawCross(g, currentItem, SelectedItem);
            #endregion

            PointF textLoc = new PointF(buttonRect.Left + buttonRect.Height, buttonRect.Top + (buttonRect.Height / 2) - (textSize.Height / 2) - 2);
            RectangleF textRect = buttonRect;
            textRect.Location = textLoc;
           // textRect.Y = buttonRect.Y + buttonRect.Height / 2 + currentFont.Height/2;
            textRect.X = buttonRect.Left + 14;
            textRect.Width = buttonRect.Width - (textRect.Left - buttonRect.Left) - 4;
            textRect.Height = textSize.Height + currentFont.Size ;

            //画标签上的关闭按钮 并判断主窗口最大化和最小化时的Y轴的值
            CalcParentFormMax();
            if (FormMaximum == true)
                rectClose = new Rectangle((int)textRect.Right - CefConstHelper.closeRectW - CefConstHelper.CloseRect_toRight_Parm, (int)(buttonRect.Y+ buttonRect.Height/2 - textRect.Height/2), CefConstHelper.closeRectW, CefConstHelper.closeRectH);
            // rectClose = new Rectangle((int)textRect.Right - CefConstHelper.closeRectW - 10, (int)textRect.Height / 2 - CefConstHelper.DEF_Header_TopDis / 5, CefConstHelper.closeRectW, CefConstHelper.closeRectH);
            else
            {
               // rectClose = new Rectangle((int)textRect.Right - CefConstHelper.closeRectW - 10, (int)textRect.Height + (int)((float)CefConstHelper.DEF_Header_TopDis / 4f), CefConstHelper.closeRectW, CefConstHelper.closeRectH);
               rectClose = new Rectangle((int)textRect.Right - CefConstHelper.closeRectW - CefConstHelper.CloseRect_toRight_Parm, (int)(buttonRect.Y + buttonRect.Height / 2 - textRect.Height / 2), CefConstHelper.closeRectW, CefConstHelper.closeRectH);
            }

            currentItem.ItemBounds = rectClose;
            currentItem.ItemCloseDraw(g);

            //画标签上的Icon图标
            if (CefConstHelper.Def_TabButton_White >= 60)
            {
                if (FormMaximum == true)
                    rectIcon = new RectangleF(textRect.Left + 3, textRect.Height / 2 , CefConstHelper.rectIconSizeW, CefConstHelper.rectIconSizeH);
                else
                    rectIcon = new RectangleF(textRect.Left + 3, textRect.Height + CefConstHelper.DEF_Header_TopDis / 2 , CefConstHelper.rectIconSizeW, CefConstHelper.rectIconSizeH);

                rectIcon.Y = rectIcon.Y + (textRect.Height -rectIcon.Height)/ 3;

                if (currentItem.ItemIcon != null)
                {
                    textRect.Width = textRect.Width - rectIcon.Width - rectClose.Width * 2;
                    textRect.X = rectIcon.Right + 3;
                }
                else
                {
                    textRect.Width = textRect.Width -rectIcon.Width  - rectClose.Width * 2;
                    //textRect.X = rectIcon.Right + 3;
                }

                if (currentItem.BrowserIsLoading)
                {
                    //获得当前gif动画下一步要渲染的帧。
                    UpdateImage();
                    //将获得的当前gif动画需要渲染的帧显示在界面上的某个位置。
                    currentItem.Image_G = m_img;
                }
                rectIcon.Y = rectClose.Y;
                currentItem.IconBounds = rectIcon;
                currentItem.DrawItemImage(g);

            }
            else
            {
                if (FormMaximum == true) //是否提示多开状态
                    rectIcon = new RectangleF(textRect.Left + 3, textRect.Height / 2 - 1.6f, CefConstHelper.rectIconSizeW, CefConstHelper.rectIconSizeH);
                else
                    rectIcon = new RectangleF(textRect.Left + 3, textRect.Height + CefConstHelper.DEF_Header_TopDis / 2 - 1.6f, CefConstHelper.rectIconSizeW, CefConstHelper.rectIconSizeH);

                textRect.Width = textRect.Width - rectClose.Width * 2;
                textRect.X = textRect.X + 3;
            }

            //输出标签名字
            textRect.Y = rectClose.Y;
            currentItem.DrawItemText(g, textRect);


            #endregion
            currentItem.IsDrawn = true;

        }

        public void UpdateLayout()
        {

            sf.Trimming = StringTrimming.EllipsisCharacter;
            sf.FormatFlags |= StringFormatFlags.NoWrap;
            sf.FormatFlags &= StringFormatFlags.DirectionRightToLeft;

            stripButtonRect = new Rectangle(0, 0, ClientSize.Width - CefConstHelper.DEF_GLYPH_WIDTH - 2, CefConstHelper.buttonHeight);

                if (ParentForm != null && ParentForm.WindowState == FormWindowState.Maximized)
                {
                    if (FlashFullSC)
                        DockPadding.Top = 0;
                    else
                        //  DockPadding.Top = DEF_HEADER_HEIGHT + InControlHeight;
                        DockPadding.Top = CefConstHelper.buttonHeight + InControlHeight - 3;

                    DockPadding.Bottom = 0;
                    DockPadding.Right = 0;
                    DockPadding.Left = 0;

                }
                else
                {
                    if (FlashFullSC)
                    {
                        DockPadding.Top = 0;
                        DockPadding.Bottom = 0;
                        DockPadding.Right = 0;
                        DockPadding.Left = 0;
                    }
                    else
                    {
                        DockPadding.Top = CefConstHelper.buttonHeight + InControlHeight + CefConstHelper.DEF_Header_TopDis -1;
                        DockPadding.Bottom = 0;
                        DockPadding.Right = 0;
                        DockPadding.Left = 0;
                    }
                }
            //}
        }

        //下面这个功能如果你要加入工具栏就在这里加
        private void OnCollectionChanged(object sender, CollectionChangeEventArgs e)
        {
            FATabStripItem itm = (FATabStripItem)e.Element;

            if (e.Action == CollectionChangeAction.Add)
            {
                Controls.Add(itm);

                OnTabStripItemChanged(new TabStripItemChangedEventArgs(itm, FATabStripItemChangeTypes.Added));

            }
            else if (e.Action == CollectionChangeAction.Remove)
            {
                Controls.Remove(itm);
                itm.Dispose();

                OnTabStripItemChanged(new TabStripItemChangedEventArgs(itm, FATabStripItemChangeTypes.Removed));
            }
            else
            {
                OnTabStripItemChanged(new TabStripItemChangedEventArgs(itm, FATabStripItemChangeTypes.Changed));
            }

            //UpdateLayout();
            Invalidate();
        }

        #endregion

        #endregion

        #region Ctor

        private EventHandler evtHandler = null;
        private Image m_img;
        public FATabStrip()
        {
            BeginInit();

            SetStyle(ControlStyles.ContainerControl, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.Selectable, true);
           // AutoScaleMode = AutoScaleMode.Dpi;
            
            items = new FATabStripItemCollection();
            items.CollectionChanged += new CollectionChangeEventHandler(OnCollectionChanged);

            NewAddRect = new NewAddButton(ToolStripRenderer);
            
            Font = defaultFont;
            sf = new StringFormat();
            EndInit();
            UpdateLayout();
            m_img = (Image)Resources.ResourceManager.GetObject("Loading1");// Image.FromFile(@"C:\Users\dengwu.deng\Desktop\timg.gif");
            evtHandler = new EventHandler(OnImageAnimate);//为委托关联一个处理方法
            BeginAnimate();//调用开始动画方法
        }


        #endregion

        #region Props

        public NewAddButton newAddBT
        {
            get { return NewAddRect; }
        }

        [DefaultValue(null)]
        [RefreshProperties(RefreshProperties.All)]
        public FATabStripItem SelectedItem
        {
            get { return selectedItem; }
            set
            {
                if (selectedItem == value)
                    return;

                if (value == null && Items.Count > 0)
                {
                    FATabStripItem itm = Items[0];
                    if (itm.Visible)
                    {
                        selectedItem = itm;
                        selectedItem.Selected = true;
                        selectedItem.Dock = DockStyle.Fill;
                    }
                }
                else
                {
                    selectedItem = value;
                }

                foreach (FATabStripItem itm in Items)
                {
                    if (itm == selectedItem)
                    {
                        SelectItem(itm);
                        itm.Dock = DockStyle.Fill;
                        itm.Show();
                    }
                    else
                    {
                        UnSelectItem(itm);
                        itm.Hide();
                    }
                }

                SelectItem(selectedItem);
                Invalidate();

                if (!selectedItem.IsDrawn)
                {
                    Items.MoveTo(0, selectedItem);
                    Invalidate();
                }

                OnTabStripItemChanged(
                    new TabStripItemChangedEventArgs(selectedItem, FATabStripItemChangeTypes.SelectionChanged));
            }
        }

        public bool flashFuuSC;
        public bool FlashFullSC {
            set
            {
                flashFuuSC = value;
                //this.Invalidate();
                UpdateLayout();
            }
            get { return flashFuuSC ; } }
      
        public bool RemoveALLItem { set; get; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public FATabStripItemCollection Items
        {
            get { return items; }
        }

        [DefaultValue(typeof(Size), "350,200")]
        public new Size Size
        {
            get { return base.Size; }
            set
            {
                if (base.Size == value)
                    return;

                base.Size = value;
                UpdateLayout();
            }
        }

        /// <summary>
        /// DesignerSerializationVisibility
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new ControlCollection Controls
        {
            get { return base.Controls; }
        }



        #endregion

        #region ShouldSerialize
        public bool ShouldSerializeFont()
        {
            return Font != null && !Font.Equals(defaultFont);
        }

        public bool ShouldSerializeSelectedItem()
        {
            return true;
        }

        public bool ShouldSerializeItems()
        {
            return items.Count > 0;
        }

        public new void ResetFont()
        {
            Font = defaultFont;
        }

        #endregion

        #region ISupportInitialize Members

        public void BeginInit()
        {
            isIniting = true;
        }

        public void EndInit()
        {
            isIniting = false;
        }

        #endregion

        #region IDisposable

        ///<summary>
        ///Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        ///</summary>
        ///<filterpriority>2</filterpriority>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                items.CollectionChanged -= new CollectionChangeEventHandler(OnCollectionChanged);
                //menu.ItemClicked        -= new ToolStripItemClickedEventHandler(OnMenuItemClicked);
                //menu.VisibleChanged     -= new EventHandler(OnMenuVisibleChanged);

                foreach (FATabStripItem item in items)
                {
                    if (item != null && !item.IsDisposed)
                        item.Dispose();
                }

                //if (menu != null && !menu.IsDisposed)
                //    menu.Dispose();

                if (sf != null)
                    sf.Dispose();
            }

            base.Dispose(disposing);
        }

        #endregion
    }
}