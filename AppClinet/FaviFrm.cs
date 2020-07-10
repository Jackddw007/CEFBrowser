using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;

namespace CefiBrowser
{
    public partial class FaviFrm : Form

    {
        //This gives us the ability to drag the borderless form to a new location
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private Point p; //鼠标在窗口上的位置
        public ImageList iconImageList = new ImageList(); //存放icon图标
        public string URL;
        public string Title;
        public bool addTag = false;
        public Image iconImage; //icon

        private List<FavireBT> favireBTs;
        public FaviFrm()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            InitializeComponent();
        }

        #region 无边框拖动效果
        //[DllImport("user32.dll")]//拖动无窗体的控件
        //public static extern bool ReleaseCapture();
        //[DllImport("user32.dll")]
        //public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;

        private void Start_MouseDown(object sender, MouseEventArgs e)
        {
            //拖动窗体
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }


//C# 改变无边框窗体的尺寸大小
//以下代码为修改窗体尺寸的代码：

        const int HTLEFT = 10;
        const int HTRIGHT = 11;
        const int HTTOP = 12;
        const int HTTOPLEFT = 13;
        const int HTTOPRIGHT = 14;
        const int HTBOTTOM = 15;
        const int HTBOTTOMLEFT = 0x10;
        const int HTBOTTOMRIGHT = 17;

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x0084:
                    base.WndProc(ref m);
                    Point vPoint = new Point((int)m.LParam & 0xFFFF,
                        (int)m.LParam >> 16 & 0xFFFF);
                    vPoint = PointToClient(vPoint);
                    if (vPoint.X <= 5)
                        if (vPoint.Y <= 5)
                            m.Result = (IntPtr)HTTOPLEFT;
                        else if (vPoint.Y >= ClientSize.Height - 5)
                            m.Result = (IntPtr)HTBOTTOMLEFT;
                        else m.Result = (IntPtr)HTLEFT;
                    else if (vPoint.X >= ClientSize.Width - 5)
                        if (vPoint.Y <= 5)
                            m.Result = (IntPtr)HTTOPRIGHT;
                        else if (vPoint.Y >= ClientSize.Height - 5)
                            m.Result = (IntPtr)HTBOTTOMRIGHT;
                        else m.Result = (IntPtr)HTRIGHT;
                    else if (vPoint.Y <= 5)
                        m.Result = (IntPtr)HTTOP;
                    else if (vPoint.Y >= ClientSize.Height - 5)
                        m.Result = (IntPtr)HTBOTTOM;
                    break;
                case 0x0201://鼠标左键按下的消息 
                            //m.Msg = 0x00A1;//更改消息为非客户区按下鼠标 
                            //m.LParam = IntPtr.Zero;//默认值 
                            //m.WParam = new IntPtr(2);//鼠标放在标题栏内 
                            //以下做了一些修正，确保放大缩小按钮区域可以正常使用

                    Point point = Control.MousePosition;
                    point = PointToClient(point);
                    if (point.X < this.Width - 100 && point.Y < 30)
                    {
                        m.Msg = 0x00A1;//更改消息为非客户区按下鼠标 
                        m.LParam = IntPtr.Zero;//默认值 
                        m.WParam = new IntPtr(2);//鼠标放在标题栏内 
                    }
                 //   this.Invalidate();
                    base.WndProc(ref m);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }
        #endregion

        private void formCloseButton1_Click(object sender, EventArgs e)
        {
            if (itemTagChanged != true)
            {
                if (MessageBox.Show(this, "有未保存的标签，是否退出？", "CefiBrowser", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    this.Close();
                    GC.Collect();
                }
            }
            else
            {
                this.Close();
                GC.Collect();
            }
        }

        //private void FaviFrm_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        //{
        //   p = Control.MousePosition;
        //    if (p.Y <= 230)
        //    {
        //        ReleaseCapture();
        //        SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        //    }
        //}

        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
            //e.Graphics.SmoothingMode = SmoothingMode.AntiAlias; //描边时消除锯齿SetSmoothingMode, AntiAlias为指定消除锯齿的呈现
            //e.Graphics.CompositingQuality = CompositingQuality.HighQuality;

            //if (this.WindowState == FormWindowState.Normal)
            //{   //  画TabButton的底线
            //    this.Padding = new Padding(1);
            //    e.Graphics.DrawRectangle(new Pen(Color.FromArgb(130, 135, 144)), ClientRectangle);
            //    e.Graphics.DrawLine(new Pen(Color.FromArgb(130, 135, 144)), new Point((int)ClientRectangle.Right - 1, ClientRectangle.Y), new Point(ClientRectangle.Right - 1, ClientRectangle.Height));
            //    e.Graphics.DrawLine(new Pen(Color.FromArgb(130, 135, 144)), new Point((int)ClientRectangle.Left, ClientRectangle.Height - 1), new Point(ClientRectangle.Right - 1, ClientRectangle.Height - 1));
            //}
            base.OnPaint(e);
            Rectangle borderRc = this.ClientRectangle; ;
            borderRc.Width--;
            borderRc.Height--;
            e.Graphics.DrawRectangle(SystemPens.ControlDark, borderRc);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (itemTagChanged ==true)
            {
               if(MessageBox.Show(this,"有未保存的标签，是否退出？","CefiBrowser", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    this.Close();
                    GC.Collect();
                }
            }
            else
            {
                this.Close();
                GC.Collect();
            }

        }

        private void FaviFrm_Load(object sender, EventArgs e)
        {
            treeView1.ImageList = iconImageList;
            LoadFav_Bar_Info();
            this.ItemTitle.Text = Title;
            this.ItemURL.Text = URL;
            addURLTag(); //检查是否决定增加URL标签
            this.treeView1.ExpandAll();

          //  DelLabBT.Enabled = false;
        }

        public void addURLTag()
        {
            bool checkURL_AlreadyHave = false; //检查这个标签是否存在

            for (int i = 0; i < treeView1.Nodes.Count; i++)
            {
                if (treeView1.Nodes[i].Text == ItemTitle.Text)
                {
                    checkURL_AlreadyHave = true;
                    break;
                }
                if (((nTreeNode)treeView1.Nodes[i]).Type == "FaviFolder")
                {
                    for (int l = 0; l < ((nTreeNode)treeView1.Nodes[i]).Nodes.Count; l++)
                    {
                        if (((nTreeNode)treeView1.Nodes[i]).Nodes[l].Text == ItemTitle.Text 
                            ||((nTreeNode)(((nTreeNode)treeView1.Nodes[i]).Nodes[l])).URL == ItemURL.Text)
                        {
                            checkURL_AlreadyHave = true;
                            break;
                        }
                    }
                }
            }

            if (checkURL_AlreadyHave == false) //如果没有重复就增加URL标签
            {
                if (ItemTitle.Text.Length > 1 && ItemURL.Text.Length > 4)
                {
                    nTreeNode treeNode = new nTreeNode();
                    treeNode.Text = treeNode.Name = treeNode.Title = ItemTitle.Text;
                    treeNode.URL = ItemURL.Text;
                    treeNode.Date_Added = DateTime.Now.ToString("yyyyMMddHHmmssffff");
                    iconImageList.Images.Add(treeNode.Date_Added, iconImage);
                    treeNode.Last_Visited = DateTime.Now.ToString("yyyyMMddHHmmssffff");
                    treeNode.Layer = "0";
                    treeNode.ID = DateTime.Now.ToString("yyyyMMddHHmmssffff");
                    treeNode.FatherID = "0";
                    treeNode.Type = "URL";
                    treeNode.SelectedImageIndex  = treeNode.ImageIndex = iconImageList.Images.IndexOfKey(treeNode.Date_Added);

                    treeNode.IconBase64str = IamgeToBase64(iconImage);
                    treeView1.Nodes.Add(treeNode);
                    treeView1.SelectedNode = treeNode;
                }
            }
            else
            {
                MessageBox.Show(this,"标签重复，不能添加！","CefiBrowser", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                this.Close();
                GC.Collect();
            }
        }
        private void TreeView1_NodeMouseClick(object sender, System.Windows.Forms.TreeNodeMouseClickEventArgs e)
        {
            if (ItemTitle != null)
            {
                ItemTitle.Text = ((nTreeNode)e.Node).Title;
                ItemURL.Text = ((nTreeNode)e.Node).URL;
            }
           // DelLabBT.Enabled = true;
        }

        private void TreeView1_LostFocus(object sender, System.EventArgs e)
        {
          //  DelLabBT.Enabled = false;

        }

        #region  公共功能
        //程序启动时加载收藏夹上的信息
        public void LoadFav_Bar_Info()
        {
          //  string currDirectiory = Environment.CurrentDirectory.ToString();
            string jsonStr = File.ReadAllText(PublicClass.currDirectiory + @"\UserData\Bookmarks");
            Font sf = new Font("Tahoma", 8.25f, FontStyle.Regular);
            Graphics g = this.CreateGraphics();
            try
            {
                var jobInfoList = JsonConvert.DeserializeObject<List<FavireBT>>(jsonStr);
                if (jobInfoList.Count > 0)
                {
                    favireBTs = jobInfoList;
                    foreach (FavireBT jobInfo in favireBTs)
                    {
                        iconImageList.Images.Add(jobInfo.Date_Added, Base64ToImage(jobInfo.IconBase64str));
                        nTreeNode tNode = new nTreeNode();
                        tNode.Name = jobInfo.Title;
                        tNode.URL = jobInfo.URL;
                        tNode.Title = jobInfo.Title;
                        tNode.Date_Added = jobInfo.Date_Added;
                        tNode.Last_Visited = jobInfo.Last_Visited;
                        tNode.Layer = jobInfo.Layer;
                        tNode.Type = jobInfo.Type;
                        tNode.ID = jobInfo.ID;
                        tNode.FatherID = jobInfo.FatherID;
                        tNode.Text = jobInfo.Title;
                        tNode.ToolTipText = jobInfo.Title;
                        tNode.Type = jobInfo.Type;
                        tNode.IconBase64str = jobInfo.IconBase64str;
                        tNode.SelectedImageIndex =  tNode.ImageIndex=iconImageList.Images.IndexOfKey(jobInfo.Date_Added);
                        if (jobInfo.Layer == "0")
                        {
                            treeView1.Nodes.Add(tNode);
                            tNode = null;
                            GC.Collect();
                        }
                        else
                        {
                            for(int i=0;i<treeView1.Nodes.Count;i++)
                            {
                                if(((nTreeNode)treeView1.Nodes[i]).ID == tNode.FatherID)
                                {
                                    ((nTreeNode)treeView1.Nodes[i]).Nodes.Add(tNode);
                                }
                            }
                            tNode = null;
                            GC.Collect();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
           // currDirectiory = null;
            jsonStr = null;
            sf = null;
            g = null;
            favireBTs = null;
            GC.Collect();
        }

        /// <summary>
        /// 保存整理后的收藏夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Favi_Save_Click(object sender, EventArgs e)
        {

           // string currDirectiory = Environment.CurrentDirectory.ToString();
            nBookmarks.nCheckFiles(@"Bookmarks"); //检查
            string jsonStr="";
            try
            {
                for (int i = 0; i < treeView1.Nodes.Count; i++)
                {
                    JObject jsonObject1 = new JObject();
                    nTreeNode tTreeNode = new nTreeNode();
                    tTreeNode = (nTreeNode)treeView1.Nodes[i];
                    jsonObject1.Add("Date_Added", tTreeNode.Date_Added);
                    jsonObject1.Add("Last_Visited", tTreeNode.Last_Visited);
                    jsonObject1.Add("Title", tTreeNode.Text);
                    jsonObject1.Add("URL", tTreeNode.URL);
                    jsonObject1.Add("Layer", tTreeNode.Layer);
                    jsonObject1.Add("ID", tTreeNode.ID);
                    jsonObject1.Add("FatherID", tTreeNode.FatherID);
                    jsonObject1.Add("Type", tTreeNode.Type);
                    jsonObject1.Add("IconBase64str", tTreeNode.IconBase64str);
                    jsonStr = jsonStr.Replace("[", "");
                    jsonStr = jsonStr.Replace("]", "");
                    jsonStr = jsonStr + ",";
                    jsonStr = "[" + jsonStr + jsonObject1.ToString() + "]";
                    jsonStr = jsonStr.Replace(",,", ",");

                    if (tTreeNode.Type == "FaviFolder")
                    {
                        for (int k = 0; k < tTreeNode.Nodes.Count; k++)
                        {
                            JObject jsonObject = new JObject();
                            nTreeNode yTreeNode = new nTreeNode();
                            yTreeNode = (nTreeNode)tTreeNode.Nodes[k];
                            jsonObject.Add("Date_Added", yTreeNode.Date_Added);
                            jsonObject.Add("Last_Visited", yTreeNode.Last_Visited);
                            jsonObject.Add("Title", yTreeNode.Text);
                            jsonObject.Add("URL", yTreeNode.URL);
                            jsonObject.Add("Layer", yTreeNode.Layer);
                            jsonObject.Add("ID", yTreeNode.ID);
                            jsonObject.Add("FatherID", yTreeNode.FatherID);
                            jsonObject.Add("Type", yTreeNode.Type);
                            jsonObject.Add("IconBase64str", yTreeNode.IconBase64str);
                            jsonStr = jsonStr.Replace("[", "");
                            jsonStr = jsonStr.Replace("]", "");
                            jsonStr = jsonStr + ",";
                            jsonStr = "[" + jsonStr + jsonObject.ToString() + "]";
                            jsonStr = jsonStr.Replace(",,", ",");
                        }
                    }
                }
                jsonStr = "[" + jsonStr.Substring(2, jsonStr.Length - 2);
                File.WriteAllText(PublicClass.currDirectiory + @"\UserData\Bookmarks", jsonStr);

            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }

            // nCheckFiles("Bookmarks"); //备份
            MainForm.Instance.Invoke(new Action(() =>
            {
                MainForm.Instance.FavPanel.Controls.Clear();
                MainForm.Instance.LoadFav_Bar_Info(); //删除后重新加载，哈哈，有点傻傻的
              
            }
           ));
            itemTagChanged = false;// 当保存改动后标记为False
            ItemTitle = null;
            ItemURL = null;
            iconImage = null;

            jsonStr = null;
           // currDirectiory = null;
            GC.Collect();
            //MainForm.Instance.RefreshActiveTab();
        }
        public bool itemTagChanged = false; //所有标签是否有变动
        private void NewFavi_Folder_Click(object sender, EventArgs e)
        {
            FavirteButton favirte = new FavirteButton();

            favirte.Height = 23;
            favirte.Title = "快捷收藏夹";
            favirte.ItemIcon = CefiBrowser.Properties.Resources.FileFolderIcon;

            Font sf = new Font("Tahoma", 8.25f, FontStyle.Regular);
            Graphics g = this.CreateGraphics();
            //单位为mm
            g.PageUnit = GraphicsUnit.Millimeter;
            //测量字符串长度
            Size sif = TextRenderer.MeasureText(g, favirte.Title, sf, new Size(0, 0), TextFormatFlags.NoPadding);

            favirte.TitleWidth = sif.Width;
            favirte.Width = 26 + favirte.TitleWidth;

            if (favirte.Width > 140)
                favirte.Width = 140;

            favirte.Left = MainForm.Instance.FavPanel.Left + 6;
            if (MainForm.Instance.FavPanel.Controls.Count > 0)
            {
                int nFavPoint = 0;
                nFavPoint = MainForm.Instance.FavPanel.Left + 6;
                for (int i = 0; i < MainForm.Instance.FavPanel.Controls.Count; i++)
                {
                    //if (((FavirteButton)FavPanel.Controls[i]).URL == "" ||
                    // ((FavirteButton)FavPanel.Controls[i]).Title == favirte.Title)
                    //    return;

                    nFavPoint = MainForm.Instance.FavPanel.Controls[i].Width + 2 + nFavPoint;

                }
                favirte.Left = nFavPoint;

            }
            MainForm.Instance.Invoke(new Action(() =>
            {
                MainForm.Instance.FavPanel.Controls.Add(favirte);
            }

            ));

            nTreeNode yTreeNode = new nTreeNode();
            
            yTreeNode.Date_Added = DateTime.Now.ToString("yyyyMMddHHmmssffff");
            iconImageList.Images.Add(yTreeNode.Date_Added, favirte.ItemIcon);
            yTreeNode.Last_Visited = DateTime.Now.ToString("yyyyMMddHHmmssffff");
            yTreeNode.Layer = "0";
            yTreeNode.ID = DateTime.Now.ToString("yyyyMMddHHmmssffff");
            yTreeNode.FatherID = "0";
            yTreeNode.Type = "FaviFolder";
            yTreeNode.URL = "";
            yTreeNode.Text = favirte.Title;
            yTreeNode.Title = yTreeNode.Name= favirte.Title;
            yTreeNode.SelectedImageIndex = yTreeNode.StateImageIndex = yTreeNode.ImageIndex = iconImageList.Images.IndexOfKey(yTreeNode.Date_Added);

            yTreeNode.IconBase64str = IamgeToBase64(CefiBrowser.Properties.Resources.FileFolderIcon);
            treeView1.Nodes.Add(yTreeNode);
            itemTagChanged = true;

            sf = null;
            favirte = null;
            yTreeNode = null;
            GC.Collect();
        }

        private Point Position = new Point(0, 0);

        private void treeView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void treeView1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(nTreeNode)))
                e.Effect = DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;
        }

        private void treeView1_DragDrop(object sender, DragEventArgs e)
        {
            nTreeNode myNode = null;
            if (e.Data.GetDataPresent(typeof(nTreeNode)))
            {
                myNode = (nTreeNode)(e.Data.GetData(typeof(nTreeNode)));
            }
            else
            {
                MessageBox.Show("error");
            }
            Position.X = e.X;
            Position.Y = e.Y;
            Position = treeView1.PointToClient(Position);
            nTreeNode DropNode = (nTreeNode)this.treeView1.GetNodeAt(Position);
  
            // 1.目标节点不是空。2.目标节点不是被拖拽接点的字节点。3.目标节点不是被拖拽节点本身
            if (DropNode != null && DropNode.Parent != myNode && DropNode != myNode && DropNode.Type == "FaviFolder")
            {
                nTreeNode DragNode = myNode;
                // 将被拖拽节点从原来位置删除。
                myNode.Remove();
                // 在目标节点下增加被拖拽节点
                DragNode.Layer = "1";
                DragNode.FatherID = DropNode.ID;
                DropNode.Nodes.Add(DragNode);
                itemTagChanged = true; //标记有变动
            }

            // 如果目标节点不存在，即拖拽的位置不存在节点，那么就将被拖拽节点放在根节点之下
          else  if (myNode.Layer == "1")
            {
                myNode.FatherID = "";
                myNode.Layer = "0";
                nTreeNode DragNode = myNode;
                myNode.Remove();
                treeView1.Nodes.Add(DragNode);
                itemTagChanged = true; //标记有变动
            }

            else if(myNode.Type == "FaviFolder")
            {

                nTreeNode DragNode = myNode;
                myNode.Remove();
                treeView1.Nodes.Add(DragNode);
                itemTagChanged = true; //标记有变动

            }


        }
        /// <summary>
        /// 将图片数据转换为Base64字符串
        public string IamgeToBase64(Image faImage)
        {
            BinaryFormatter binFormatter = new BinaryFormatter();
            MemoryStream memStream = new MemoryStream();
            binFormatter.Serialize(memStream, faImage);
            byte[] bytes = memStream.GetBuffer();
            return Convert.ToBase64String(bytes);
        }
        /// <summary>
        /// 将Base64字符串转换为图片
        public Image Base64ToImage(string txtImage)
        {
            byte[] bytes = Convert.FromBase64String(txtImage);
            MemoryStream memStream = new MemoryStream(bytes);
            BinaryFormatter binFormatter = new BinaryFormatter();
            return (Image)binFormatter.Deserialize(memStream);
        }







        #endregion

        private void DelLabBT_Click(object sender, EventArgs e)
        {

            Bookmarks bookmarks = new Bookmarks();
            for (int i = 0; i < MainForm.Instance.FavPanel.Controls.Count; i++)
            {
                if (((FavirteButton)MainForm.Instance.FavPanel.Controls[i]).Date_Added == ((nTreeNode)treeView1.SelectedNode).Date_Added)
                {
                    //这里是删除功能
                    if (bookmarks.DelBookmarks((FavirteButton)MainForm.Instance.FavPanel.Controls[i]))
                    {
                        treeView1.Nodes.Remove(((nTreeNode)treeView1.SelectedNode));
                        itemTagChanged = true; //标注此时有修改
                    }
                    break;
                }
            }
            if (itemTagChanged == true) //如果没有任何改变不要执行删除动作
            {
                MainForm.Instance.Invoke(new Action(() =>
            {
                MainForm.Instance.FavPanel.Controls.Clear();
                MainForm.Instance.LoadFav_Bar_Info(); //删除后重新加载，哈哈，有点傻傻的
            }
            ));
            }
        }
        
    }

    [ToolboxItem(true)]
    public class nTreeNode : TreeNode
    {
        private Image itemIcon = null;//Logo icon

        private string uRL = string.Empty; //存按钮的URL
        private string title = string.Empty; //名称
#pragma warning disable CS0414 // 字段“nTreeNode.sf”已被赋值，但从未使用过它的值
        private StringFormat sf = null;
#pragma warning restore CS0414 // 字段“nTreeNode.sf”已被赋值，但从未使用过它的值
        private static Font defaultFont = new Font("Tahoma", 8.25f, FontStyle.Regular);
        private string date_added = string.Empty; //建立时间
        private string id;
        private string last_visited = string.Empty; //最后访问时间
        private string type = string.Empty; //类型
        private string fatherID = string.Empty; //父ID
        private string layer = string.Empty; //标记层
        private int titleWidth;
        private string iconBase64str = string.Empty;
        private bool isSelect = false;
        public nTreeNode()
        {

        }

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

        public int TitleWidth
        {
            get { return titleWidth; }
            set { titleWidth = value; }
        }
        //建立时间
        public string Date_Added
        {
            get { return date_added; }
            set { date_added = value; }

        }

        //ID
        public string ID
        {
            get { return id; }
            set { id = value; }

        }

        public string Last_Visited
        {
            get { return last_visited; }
            set { last_visited = value; }

        }

        //类型
        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        //父ID
        public string FatherID
        {
            get { return fatherID; }
            set { fatherID = value; }
        }

        //
        public string Layer
        {
            get { return layer; }
            set { layer = value; }
        }


    }

    public class nBookmarks
    {
        public void nWriteBookmarks(FavirteButton favirteButton)
        {
            string currDirectiory = Environment.CurrentDirectory.ToString();
            nCheckFiles("Bookmarks"); //检查
            JObject jsonObject = new JObject();
            string jsonStr;
            try
            {
                jsonStr = File.ReadAllText(currDirectiory + @"\UserData\Bookmarks");
                if (jsonStr == "")
                {
                    jsonObject.Add("Date_Added", favirteButton.Date_Added);
                    jsonObject.Add("Last_Visited", favirteButton.Last_Visited);
                    jsonObject.Add("Title", favirteButton.Title);
                    jsonObject.Add("URL", favirteButton.URL);
                    jsonObject.Add("Layer", favirteButton.Layer);
                    jsonObject.Add("ID", favirteButton.ID);
                    jsonObject.Add("FatherID", favirteButton.FatherID);
                    jsonObject.Add("Type", favirteButton.Type);
                    jsonObject.Add("IconBase64str", favirteButton.IconBase64str);
                    File.WriteAllText(currDirectiory + @"\UserData\Bookmarks", "[" + jsonObject.ToString() + "]");
                    // GC.Collect();
                }
                else
                {
                    jsonObject = new JObject();
                    jsonObject.Add("Date_Added", favirteButton.Date_Added);
                    jsonObject.Add("Last_Visited", favirteButton.Last_Visited);
                    jsonObject.Add("Title", favirteButton.Title);
                    jsonObject.Add("URL", favirteButton.URL);
                    jsonObject.Add("Layer", favirteButton.Layer);
                    jsonObject.Add("ID", favirteButton.ID);
                    jsonObject.Add("FatherID", favirteButton.FatherID);
                    jsonObject.Add("Type", favirteButton.Type);
                    jsonObject.Add("IconBase64str", favirteButton.IconBase64str);
                    jsonStr = jsonStr.Replace("[", "");
                    jsonStr = jsonStr.Replace("]", "");
                    jsonStr = jsonStr + ",";
                    jsonStr = "[" + jsonStr + jsonObject.ToString() + "]";
                    jsonStr = jsonStr.Replace(",,", ",");
                    File.WriteAllText(currDirectiory + @"\UserData\Bookmarks", jsonStr);
                    // GC.Collect();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            nCheckFiles("Bookmarks"); //备份

            jsonObject = null;
            jsonStr = null;
            currDirectiory = null;
            GC.Collect();
        }
        public bool nDelBookmarks(FavirteButton favirteButton)
        {
            string currDirectiory = Environment.CurrentDirectory.ToString();
            nCheckFiles("Bookmarks"); //检查
            JObject jsonObject = new JObject();
            string jsonStr;
            try
            {
                jsonStr = File.ReadAllText(currDirectiory + @"\UserData\Bookmarks");
                if (jsonStr != "")
                {
                    List<FavireBT> jobInfoList = JsonConvert.DeserializeObject<List<FavireBT>>(jsonStr);
                    jsonStr = "";
                    foreach (FavireBT favBT in jobInfoList)
                    {
                        if (favBT.Date_Added != favirteButton.Date_Added)
                        {
                            jsonObject = new JObject();
                            jsonObject.Add("Date_Added", favBT.Date_Added);
                            jsonObject.Add("Last_Visited", favBT.Last_Visited);
                            jsonObject.Add("Title", favBT.Title);
                            jsonObject.Add("URL", favBT.URL);
                            jsonObject.Add("Layer", favBT.Layer);
                            jsonObject.Add("ID", favBT.ID);
                            jsonObject.Add("FatherID", favBT.FatherID);
                            jsonObject.Add("Type", favBT.Type);
                            jsonObject.Add("IconBase64str", favBT.IconBase64str);
                            jsonStr = jsonStr.Replace("[", "");
                            jsonStr = jsonStr.Replace("]", "");
                            jsonStr += jsonObject.ToString() + ",";

                        }
                    }
                }

                File.WriteAllText(currDirectiory + @"\UserData\Bookmarks", "[" + jsonStr.Replace(",,", ",") + "]");
                return true;
            }
            catch
            {
                return false;
            }
        }

        //public void ClearFileText()
        //{
        //    string currDirectiory = Environment.CurrentDirectory.ToString();
        //    FileStream stream2 = File.Open(currDirectiory + @"\UserData\Bookmarks", FileMode.OpenOrCreate, FileAccess.Write);
        //    stream2.Seek(0, SeekOrigin.Begin);
        //    stream2.SetLength(0); //清空txt文件
        //    stream2.Close();
        //    File.WriteAllLines(PathBase + inforpath, content1);
        //}


        //更新
        public  void nUpdate()
        {
            string jsonfile = Environment.CurrentDirectory.ToString() + @"\UserData\Bookmarks";
            if (File.Exists(jsonfile))
            {
                string jsonString = File.ReadAllText(jsonfile);//读取文件

                List<FavirteButton> jobInfoList = JsonConvert.DeserializeObject<List<FavirteButton>>(jsonString);

                foreach (FavirteButton jobInfo in jobInfoList)
                {
                    Console.WriteLine("UserName:" + jobInfo.Title);
                }

                JObject jobject = JObject.Parse(jsonString);//解析成json
                jobject["Devices"]["name"] = "555555";//替换需要的文件
                string convertString = Convert.ToString(jobject);//将json装换为string
                File.WriteAllText(jsonfile, convertString);//将内容写进jon文件中
            }
            else
            {

            }
        }

        //检查文件是否存在
        public static bool nCheckFiles(string fileName)
        {
            string currDirectiory = Environment.CurrentDirectory.ToString();
            if (!Directory.Exists(currDirectiory + @"\UserData\"))
                Directory.CreateDirectory(currDirectiory + @"\UserData\");


            if (!File.Exists(currDirectiory + @"\UserData\" + fileName))
            {
                File.Create(currDirectiory + @"\UserData\" + fileName).Close();
                File.Create(currDirectiory + @"\UserData\" + fileName + ".bak").Close();
            }
            else
            {
                //有就备份
                File.Copy(currDirectiory + @"\UserData\" + fileName, currDirectiory + @"\UserData\" + fileName + ".bak", true);
            }
            currDirectiory = null;
            GC.Collect();
            return true;
        }




    }

    public class nFavireBT
    {
        public string Date_Added { set; get; }
        public string Last_Visited { set; get; }
        public string Title { set; get; }
        public string URL { set; get; }
        public string Layer { set; get; }
        public string ID { set; get; }
        public string FatherID { set; get; }
        public string Type { set; get; }
        public string IconBase64str { set; get; }
    }

}
