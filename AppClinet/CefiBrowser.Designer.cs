
using System.Windows.Forms;

namespace CefiBrowser
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && (components != null))
                {
                    components.Dispose();
                }
                base.Dispose(disposing);
            }
            catch
            { }
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            CefiBrowser.TTextBoxBorderRenderStyle tTextBoxBorderRenderStyle1 = new CefiBrowser.TTextBoxBorderRenderStyle();
            this.BtnMUserSupport = new CefiBrowser.ButtonXP();
            this.addrPanel1 = new CefiBrowser.AddrPanel();
            this.BtnSeach = new CefiBrowser.ButtonXP();
            this.textBoxXP1 = new System.Windows.Forms.TextBox();
            this.BtFav = new CefiBrowser.ButtonXP();
            this.BtBack = new CefiBrowser.ButtonXP();
            this.BtForward = new CefiBrowser.ButtonXP();
            this.BtReflash = new CefiBrowser.ButtonXP();
            this.BtHome = new CefiBrowser.ButtonXP();
            this.BtnSettings = new CefiBrowser.ButtonXP();
            this.FaTabMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.NewTabItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.CloseTabItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CloseAtherTabItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.CloseRightTabItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CloseLeftabItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Favri_BarMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TwoOpenNewTableitem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenRULInNewTab = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.CopyFavriBtURL = new System.Windows.Forms.ToolStripMenuItem();
            this.Favi_Modif_Item = new System.Windows.Forms.ToolStripMenuItem();
            this.DelCurrentFavriBT = new System.Windows.Forms.ToolStripMenuItem();
            this.Favi_3_Line = new System.Windows.Forms.ToolStripSeparator();
            this.AddFavri_Folder = new System.Windows.Forms.ToolStripMenuItem();
            this.Edit_FavriBT = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.显示书签栏SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Settings_Menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.OpneNFataItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenNCefiBrowser = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.MemSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.EditFaviFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.DownloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.CefiBPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.PrinttoPDF = new System.Windows.Forms.ToolStripMenuItem();
            this.sBtnSear = new System.Windows.Forms.ToolStripMenuItem();
            this.F12Tools = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.KuaYu_Support = new System.Windows.Forms.ToolStripMenuItem();
            this.MulitiUsers_Support = new System.Windows.Forms.ToolStripMenuItem();
            this.Clear_Cache = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
            this.DownloadPanel3 = new CefiBrowser.AddrPanel();
            this.viewDowload = new CefiBrowser.ButtonXP();
            this.buttonXP2 = new CefiBrowser.ButtonXP();
            this.PanelSearch = new CefiBrowser.AddrPanel();
            this.TxtSearch = new System.Windows.Forms.TextBox();
            this.BtnNextSearch = new System.Windows.Forms.Button();
            this.BtnPrevSearch = new System.Windows.Forms.Button();
            this.BtnCloseSearch = new System.Windows.Forms.Button();
            this.textShow = new CefiBrowser.TxtBoxXP();
            this.addrPanel3 = new CefiBrowser.AddrPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.faTabStrip1 = new CefiBrowser.FATabStrip();
            this.fMenuStrip1 = new CefiBrowser.FMenuStrip();
            this.addrPanel2 = new CefiBrowser.AddrPanel();
            this.FavPanel = new CefiBrowser.AddrPanel();
            this.ToolsPanel = new CefiBrowser.AddrPanel();
            this.formMinButton1 = new CefiBrowser.ButtonXP();
            this.formMaxNormalButton1 = new CefiBrowser.ButtonXP();
            this.formCloseButton1 = new CefiBrowser.ButtonXP();
            this.addrPanel1.SuspendLayout();
            this.FaTabMenu.SuspendLayout();
            this.Favri_BarMenu.SuspendLayout();
            this.Settings_Menu.SuspendLayout();
            this.DownloadPanel3.SuspendLayout();
            this.PanelSearch.SuspendLayout();
            this.addrPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.faTabStrip1)).BeginInit();
            this.ToolsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnMUserSupport
            // 
            this.BtnMUserSupport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnMUserSupport.AutoSize = true;
            this.BtnMUserSupport.BackColor = System.Drawing.Color.Transparent;
            this.BtnMUserSupport.BackColorDown = System.Drawing.Color.DarkGray;
            this.BtnMUserSupport.BackColorEX = System.Drawing.Color.Transparent;
            this.BtnMUserSupport.BackColorLeave = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.BtnMUserSupport.BackColorMove = System.Drawing.Color.LightGray;
            this.BtnMUserSupport.FontM = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtnMUserSupport.ImageDefault = global::CefiBrowser.Properties.Resources.Multiusers;
            this.BtnMUserSupport.ImageDown = null;
            this.BtnMUserSupport.ImageLeave = null;
            this.BtnMUserSupport.ImageMove = null;
            this.BtnMUserSupport.Location = new System.Drawing.Point(784, 6);
            this.BtnMUserSupport.Name = "BtnMUserSupport";
            this.BtnMUserSupport.Size = new System.Drawing.Size(20, 20);
            this.BtnMUserSupport.TabIndex = 15;
            this.BtnMUserSupport.TextColor = System.Drawing.Color.Black;
            this.BtnMUserSupport.TextEX = "";
            this.BtnMUserSupport.ButtonClick += new System.EventHandler(this.BtnMUserSupport_ButtonClick);
            // 
            // addrPanel1
            // 
            this.addrPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.addrPanel1.BackColor = System.Drawing.Color.White;
            this.addrPanel1.Controls.Add(this.BtnSeach);
            this.addrPanel1.Controls.Add(this.textBoxXP1);
            this.addrPanel1.Controls.Add(this.BtFav);
            this.addrPanel1.DrBottom = true;
            this.addrPanel1.DrLeft = true;
            this.addrPanel1.DrRight = true;
            this.addrPanel1.DrTop = true;
            this.addrPanel1.Location = new System.Drawing.Point(107, 4);
            this.addrPanel1.mStrText = null;
            this.addrPanel1.Name = "addrPanel1";
            this.addrPanel1.Size = new System.Drawing.Size(671, 26);
            this.addrPanel1.TabIndex = 14;
            // 
            // BtnSeach
            // 
            this.BtnSeach.AutoSize = true;
            this.BtnSeach.BackColor = System.Drawing.Color.Transparent;
            this.BtnSeach.BackColorDown = System.Drawing.Color.Transparent;
            this.BtnSeach.BackColorEX = System.Drawing.Color.Transparent;
            this.BtnSeach.BackColorLeave = System.Drawing.Color.Transparent;
            this.BtnSeach.BackColorMove = System.Drawing.Color.Transparent;
            this.BtnSeach.FontM = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtnSeach.ImageDefault = global::CefiBrowser.Properties.Resources.shousuo;
            this.BtnSeach.ImageDown = null;
            this.BtnSeach.ImageLeave = null;
            this.BtnSeach.ImageMove = null;
            this.BtnSeach.Location = new System.Drawing.Point(3, 3);
            this.BtnSeach.Name = "BtnSeach";
            this.BtnSeach.Size = new System.Drawing.Size(21, 20);
            this.BtnSeach.TabIndex = 3;
            this.BtnSeach.TextColor = System.Drawing.Color.Black;
            this.BtnSeach.TextEX = "";
            // 
            // textBoxXP1
            // 
            this.textBoxXP1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxXP1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxXP1.Font = new System.Drawing.Font("Verdana", 10.6F);
            this.textBoxXP1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBoxXP1.Location = new System.Drawing.Point(24, 4);
            this.textBoxXP1.Name = "textBoxXP1";
            this.textBoxXP1.Size = new System.Drawing.Size(618, 18);
            this.textBoxXP1.TabIndex = 2;
            this.textBoxXP1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TextBoxXP1_MouseClick);
            this.textBoxXP1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TextBoxXP1_KeyUp);
            this.textBoxXP1.LostFocus += new System.EventHandler(this.TextBoxXP1_LostFocus);
            // 
            // BtFav
            // 
            this.BtFav.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtFav.AutoSize = true;
            this.BtFav.BackColor = System.Drawing.Color.Transparent;
            this.BtFav.BackColorDown = System.Drawing.Color.DarkGray;
            this.BtFav.BackColorEX = System.Drawing.Color.White;
            this.BtFav.BackColorLeave = System.Drawing.Color.White;
            this.BtFav.BackColorMove = System.Drawing.Color.LightGray;
            this.BtFav.FontM = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtFav.ImageDefault = global::CefiBrowser.Properties.Resources.favrite;
            this.BtFav.ImageDown = null;
            this.BtFav.ImageLeave = null;
            this.BtFav.ImageMove = null;
            this.BtFav.Location = new System.Drawing.Point(648, 2);
            this.BtFav.Name = "BtFav";
            this.BtFav.Size = new System.Drawing.Size(21, 21);
            this.BtFav.TabIndex = 0;
            this.BtFav.TextColor = System.Drawing.Color.Black;
            this.BtFav.TextEX = "";
            this.BtFav.ButtonClick += new System.EventHandler(this.BtFav_ButtonClick);
            // 
            // BtBack
            // 
            this.BtBack.AutoSize = true;
            this.BtBack.BackColor = System.Drawing.Color.Transparent;
            this.BtBack.BackColorDown = System.Drawing.Color.DarkGray;
            this.BtBack.BackColorEX = System.Drawing.Color.Transparent;
            this.BtBack.BackColorLeave = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.BtBack.BackColorMove = System.Drawing.Color.LightGray;
            this.BtBack.Enabled = false;
            this.BtBack.FontM = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtBack.ImageDefault = global::CefiBrowser.Properties.Resources.back;
            this.BtBack.ImageDown = null;
            this.BtBack.ImageLeave = null;
            this.BtBack.ImageMove = null;
            this.BtBack.Location = new System.Drawing.Point(5, 7);
            this.BtBack.Name = "BtBack";
            this.BtBack.Size = new System.Drawing.Size(20, 20);
            this.BtBack.TabIndex = 13;
            this.BtBack.TextColor = System.Drawing.Color.Black;
            this.BtBack.TextEX = "";
            this.BtBack.ButtonClick += new System.EventHandler(this.BtBack_Click);
            // 
            // BtForward
            // 
            this.BtForward.AutoSize = true;
            this.BtForward.BackColor = System.Drawing.Color.Transparent;
            this.BtForward.BackColorDown = System.Drawing.Color.DarkGray;
            this.BtForward.BackColorEX = System.Drawing.Color.Transparent;
            this.BtForward.BackColorLeave = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.BtForward.BackColorMove = System.Drawing.Color.LightGray;
            this.BtForward.Enabled = false;
            this.BtForward.FontM = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtForward.ImageDefault = global::CefiBrowser.Properties.Resources.forward;
            this.BtForward.ImageDown = null;
            this.BtForward.ImageLeave = null;
            this.BtForward.ImageMove = null;
            this.BtForward.Location = new System.Drawing.Point(28, 7);
            this.BtForward.Name = "BtForward";
            this.BtForward.Size = new System.Drawing.Size(20, 20);
            this.BtForward.TabIndex = 12;
            this.BtForward.TextColor = System.Drawing.Color.Black;
            this.BtForward.TextEX = "";
            this.BtForward.ButtonClick += new System.EventHandler(this.BtForward_ButtonClink);
            // 
            // BtReflash
            // 
            this.BtReflash.AutoSize = true;
            this.BtReflash.BackColor = System.Drawing.Color.Transparent;
            this.BtReflash.BackColorDown = System.Drawing.Color.DarkGray;
            this.BtReflash.BackColorEX = System.Drawing.Color.Transparent;
            this.BtReflash.BackColorLeave = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.BtReflash.BackColorMove = System.Drawing.Color.LightGray;
            this.BtReflash.Enabled = false;
            this.BtReflash.FontM = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtReflash.ImageDefault = global::CefiBrowser.Properties.Resources.reflesh;
            this.BtReflash.ImageDown = null;
            this.BtReflash.ImageLeave = null;
            this.BtReflash.ImageMove = null;
            this.BtReflash.Location = new System.Drawing.Point(55, 7);
            this.BtReflash.Name = "BtReflash";
            this.BtReflash.Size = new System.Drawing.Size(20, 20);
            this.BtReflash.TabIndex = 11;
            this.BtReflash.TextColor = System.Drawing.Color.Black;
            this.BtReflash.TextEX = "";
            this.BtReflash.ButtonClick += new System.EventHandler(this.BtReflash_Click);
            // 
            // BtHome
            // 
            this.BtHome.AutoSize = true;
            this.BtHome.BackColor = System.Drawing.Color.Transparent;
            this.BtHome.BackColorDown = System.Drawing.Color.DarkGray;
            this.BtHome.BackColorEX = System.Drawing.Color.Transparent;
            this.BtHome.BackColorLeave = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.BtHome.BackColorMove = System.Drawing.Color.LightGray;
            this.BtHome.FontM = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtHome.ImageDefault = global::CefiBrowser.Properties.Resources.home;
            this.BtHome.ImageDown = null;
            this.BtHome.ImageLeave = null;
            this.BtHome.ImageMove = null;
            this.BtHome.Location = new System.Drawing.Point(81, 7);
            this.BtHome.Name = "BtHome";
            this.BtHome.Size = new System.Drawing.Size(20, 20);
            this.BtHome.TabIndex = 10;
            this.BtHome.TextColor = System.Drawing.Color.Black;
            this.BtHome.TextEX = "";
            this.BtHome.ButtonClick += new System.EventHandler(this.BtHome_Clinck);
            // 
            // BtnSettings
            // 
            this.BtnSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSettings.AutoSize = true;
            this.BtnSettings.BackColor = System.Drawing.Color.Transparent;
            this.BtnSettings.BackColorDown = System.Drawing.Color.DarkGray;
            this.BtnSettings.BackColorEX = System.Drawing.Color.Transparent;
            this.BtnSettings.BackColorLeave = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.BtnSettings.BackColorMove = System.Drawing.Color.LightGray;
            this.BtnSettings.FontM = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtnSettings.ImageDefault = global::CefiBrowser.Properties.Resources.menu;
            this.BtnSettings.ImageDown = null;
            this.BtnSettings.ImageLeave = null;
            this.BtnSettings.ImageMove = null;
            this.BtnSettings.Location = new System.Drawing.Point(808, 6);
            this.BtnSettings.Name = "BtnSettings";
            this.BtnSettings.Size = new System.Drawing.Size(19, 20);
            this.BtnSettings.TabIndex = 9;
            this.BtnSettings.TextColor = System.Drawing.Color.Black;
            this.BtnSettings.TextEX = "";
            this.BtnSettings.ButtonClick += new System.EventHandler(this.BtnSettings_ButtonClick);
            // 
            // FaTabMenu
            // 
            this.FaTabMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.FaTabMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewTabItem,
            this.toolStripSeparator1,
            this.CloseTabItem,
            this.CloseAtherTabItem,
            this.toolStripSeparator2,
            this.CloseRightTabItem,
            this.CloseLeftabItem});
            this.FaTabMenu.Name = "contextMenuStrip1";
            this.FaTabMenu.Size = new System.Drawing.Size(230, 126);
            this.FaTabMenu.LostFocus += new System.EventHandler(this.FaTabMenu_LostFocus);
            // 
            // NewTabItem
            // 
            this.NewTabItem.Name = "NewTabItem";
            this.NewTabItem.ShortcutKeyDisplayString = " ";
            this.NewTabItem.Size = new System.Drawing.Size(229, 22);
            this.NewTabItem.Text = "打开新的标签页";
            this.NewTabItem.Click += new System.EventHandler(this.NewTabItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(226, 6);
            // 
            // CloseTabItem
            // 
            this.CloseTabItem.Name = "CloseTabItem";
            this.CloseTabItem.ShortcutKeyDisplayString = "            Ctrl+C";
            this.CloseTabItem.Size = new System.Drawing.Size(229, 22);
            this.CloseTabItem.Text = "关闭标签页";
            this.CloseTabItem.Click += new System.EventHandler(this.CloseTabItem_Click);
            // 
            // CloseAtherTabItem
            // 
            this.CloseAtherTabItem.Name = "CloseAtherTabItem";
            this.CloseAtherTabItem.Size = new System.Drawing.Size(229, 22);
            this.CloseAtherTabItem.Text = "关闭其他标签页";
            this.CloseAtherTabItem.Click += new System.EventHandler(this.CloseAtherTabItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(226, 6);
            // 
            // CloseRightTabItem
            // 
            this.CloseRightTabItem.Name = "CloseRightTabItem";
            this.CloseRightTabItem.Size = new System.Drawing.Size(229, 22);
            this.CloseRightTabItem.Text = "关闭右侧标签页";
            this.CloseRightTabItem.Click += new System.EventHandler(this.CloseRightTabItem_Click);
            // 
            // CloseLeftabItem
            // 
            this.CloseLeftabItem.Name = "CloseLeftabItem";
            this.CloseLeftabItem.Size = new System.Drawing.Size(229, 22);
            this.CloseLeftabItem.Text = "关闭左侧标签页";
            this.CloseLeftabItem.Click += new System.EventHandler(this.CloseLeftabItem_Click);
            // 
            // Favri_BarMenu
            // 
            this.Favri_BarMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.Favri_BarMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TwoOpenNewTableitem,
            this.OpenRULInNewTab,
            this.toolStripSeparator3,
            this.CopyFavriBtURL,
            this.Favi_Modif_Item,
            this.DelCurrentFavriBT,
            this.Favi_3_Line,
            this.AddFavri_Folder,
            this.Edit_FavriBT,
            this.toolStripSeparator5,
            this.显示书签栏SToolStripMenuItem});
            this.Favri_BarMenu.Name = "Favri_BarMenu";
            this.Favri_BarMenu.Size = new System.Drawing.Size(191, 198);
            this.Favri_BarMenu.LostFocus += new System.EventHandler(this.Favri_BarMenu_LostFocus);
            // 
            // TwoOpenNewTableitem
            // 
            this.TwoOpenNewTableitem.Name = "TwoOpenNewTableitem";
            this.TwoOpenNewTableitem.Size = new System.Drawing.Size(190, 22);
            this.TwoOpenNewTableitem.Text = "打开新的标签页";
            this.TwoOpenNewTableitem.Click += new System.EventHandler(this.TwoOpenNewTableitem_Click);
            // 
            // OpenRULInNewTab
            // 
            this.OpenRULInNewTab.Name = "OpenRULInNewTab";
            this.OpenRULInNewTab.Size = new System.Drawing.Size(190, 22);
            this.OpenRULInNewTab.Text = "在新标签页中打开(O)";
            this.OpenRULInNewTab.Click += new System.EventHandler(this.OpenRULInNewTab_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(187, 6);
            // 
            // CopyFavriBtURL
            // 
            this.CopyFavriBtURL.Name = "CopyFavriBtURL";
            this.CopyFavriBtURL.Size = new System.Drawing.Size(190, 22);
            this.CopyFavriBtURL.Text = "复制URL";
            this.CopyFavriBtURL.Click += new System.EventHandler(this.CopyFavriBtURL_Click);
            // 
            // Favi_Modif_Item
            // 
            this.Favi_Modif_Item.Name = "Favi_Modif_Item";
            this.Favi_Modif_Item.Size = new System.Drawing.Size(190, 22);
            this.Favi_Modif_Item.Text = "修改(T)";
            // 
            // DelCurrentFavriBT
            // 
            this.DelCurrentFavriBT.Name = "DelCurrentFavriBT";
            this.DelCurrentFavriBT.Size = new System.Drawing.Size(190, 22);
            this.DelCurrentFavriBT.Text = "删除(D)";
            this.DelCurrentFavriBT.Click += new System.EventHandler(this.DelCurrentFavriBT_Click);
            // 
            // Favi_3_Line
            // 
            this.Favi_3_Line.Name = "Favi_3_Line";
            this.Favi_3_Line.Size = new System.Drawing.Size(187, 6);
            // 
            // AddFavri_Folder
            // 
            this.AddFavri_Folder.Name = "AddFavri_Folder";
            this.AddFavri_Folder.Size = new System.Drawing.Size(190, 22);
            this.AddFavri_Folder.Text = "添加文件夹(F)";
            this.AddFavri_Folder.Click += new System.EventHandler(this.AddFavri_Folder_Click);
            // 
            // Edit_FavriBT
            // 
            this.Edit_FavriBT.Name = "Edit_FavriBT";
            this.Edit_FavriBT.Size = new System.Drawing.Size(190, 22);
            this.Edit_FavriBT.Text = "整理书签";
            this.Edit_FavriBT.Click += new System.EventHandler(this.Edit_FavriBT_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(187, 6);
            // 
            // 显示书签栏SToolStripMenuItem
            // 
            this.显示书签栏SToolStripMenuItem.Checked = true;
            this.显示书签栏SToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.显示书签栏SToolStripMenuItem.Name = "显示书签栏SToolStripMenuItem";
            this.显示书签栏SToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.显示书签栏SToolStripMenuItem.Text = "显示书签栏(S)";
            // 
            // Settings_Menu
            // 
            this.Settings_Menu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.Settings_Menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpneNFataItem,
            this.OpenNCefiBrowser,
            this.toolStripSeparator9,
            this.toolStripMenuItem4,
            this.toolStripSeparator10,
            this.toolStripMenuItem1,
            this.MemSetting,
            this.toolStripMenuItem5,
            this.EditFaviFolder,
            this.DownloadToolStripMenuItem,
            this.toolStripSeparator7,
            this.CefiBPrint,
            this.PrinttoPDF,
            this.sBtnSear,
            this.F12Tools,
            this.toolStripSeparator4,
            this.KuaYu_Support,
            this.MulitiUsers_Support,
            this.Clear_Cache,
            this.toolStripSeparator6,
            this.toolStripMenuItem8});
            this.Settings_Menu.Name = "Settings_Menu";
            this.Settings_Menu.Size = new System.Drawing.Size(185, 386);
            // 
            // OpneNFataItem
            // 
            this.OpneNFataItem.Name = "OpneNFataItem";
            this.OpneNFataItem.Size = new System.Drawing.Size(184, 22);
            this.OpneNFataItem.Text = "新建标签页";
            this.OpneNFataItem.Click += new System.EventHandler(this.OpneNFataItem_Click);
            // 
            // OpenNCefiBrowser
            // 
            this.OpenNCefiBrowser.Name = "OpenNCefiBrowser";
            this.OpenNCefiBrowser.Size = new System.Drawing.Size(184, 22);
            this.OpenNCefiBrowser.Text = "打开新的窗口";
            this.OpenNCefiBrowser.Click += new System.EventHandler(this.OpenNCefiBrowser_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(181, 6);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(184, 22);
            this.toolStripMenuItem4.Text = "缩放";
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(181, 6);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(184, 22);
            this.toolStripMenuItem1.Text = "设置主页";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // MemSetting
            // 
            this.MemSetting.Name = "MemSetting";
            this.MemSetting.Size = new System.Drawing.Size(184, 22);
            this.MemSetting.Text = "内存节省模式";
            this.MemSetting.Click += new System.EventHandler(this.CefGSetting_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Checked = true;
            this.toolStripMenuItem5.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(184, 22);
            this.toolStripMenuItem5.Text = "显示收藏栏";
            // 
            // EditFaviFolder
            // 
            this.EditFaviFolder.Name = "EditFaviFolder";
            this.EditFaviFolder.Size = new System.Drawing.Size(184, 22);
            this.EditFaviFolder.Text = "收藏夹";
            this.EditFaviFolder.Click += new System.EventHandler(this.EditFaviFolder_Click);
            // 
            // DownloadToolStripMenuItem
            // 
            this.DownloadToolStripMenuItem.Name = "DownloadToolStripMenuItem";
            this.DownloadToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.DownloadToolStripMenuItem.Text = "下载管理";
            this.DownloadToolStripMenuItem.Click += new System.EventHandler(this.DownloadToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(181, 6);
            // 
            // CefiBPrint
            // 
            this.CefiBPrint.Name = "CefiBPrint";
            this.CefiBPrint.Size = new System.Drawing.Size(184, 22);
            this.CefiBPrint.Text = "打印";
            this.CefiBPrint.Click += new System.EventHandler(this.CefiBPrint_Click);
            // 
            // PrinttoPDF
            // 
            this.PrinttoPDF.Name = "PrinttoPDF";
            this.PrinttoPDF.Size = new System.Drawing.Size(184, 22);
            this.PrinttoPDF.Text = "页面另存为PDF";
            this.PrinttoPDF.Click += new System.EventHandler(this.PrinttoPDF_Click);
            // 
            // sBtnSear
            // 
            this.sBtnSear.Name = "sBtnSear";
            this.sBtnSear.Size = new System.Drawing.Size(184, 22);
            this.sBtnSear.Text = "在当前页查找";
            this.sBtnSear.Click += new System.EventHandler(this.sBtnSear_Click);
            // 
            // F12Tools
            // 
            this.F12Tools.Name = "F12Tools";
            this.F12Tools.Size = new System.Drawing.Size(184, 22);
            this.F12Tools.Text = "F12开发者工具";
            this.F12Tools.Click += new System.EventHandler(this.F12Tools_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(181, 6);
            // 
            // KuaYu_Support
            // 
            this.KuaYu_Support.Checked = true;
            this.KuaYu_Support.CheckState = System.Windows.Forms.CheckState.Checked;
            this.KuaYu_Support.Name = "KuaYu_Support";
            this.KuaYu_Support.Size = new System.Drawing.Size(184, 22);
            this.KuaYu_Support.Text = "启用跨域支持";
            // 
            // MulitiUsers_Support
            // 
            this.MulitiUsers_Support.Checked = true;
            this.MulitiUsers_Support.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MulitiUsers_Support.Name = "MulitiUsers_Support";
            this.MulitiUsers_Support.Size = new System.Drawing.Size(184, 22);
            this.MulitiUsers_Support.Text = "启用多用户支持";
            this.MulitiUsers_Support.Click += new System.EventHandler(this.MulitiUsers_Support_Click);
            // 
            // Clear_Cache
            // 
            this.Clear_Cache.Name = "Clear_Cache";
            this.Clear_Cache.Size = new System.Drawing.Size(184, 22);
            this.Clear_Cache.Text = "清除浏览器历史记录";
            this.Clear_Cache.Click += new System.EventHandler(this.Clear_Cache_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(181, 6);
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(184, 22);
            this.toolStripMenuItem8.Text = "版本信息";
            // 
            // DownloadPanel3
            // 
            this.DownloadPanel3.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.DownloadPanel3.BackColor = System.Drawing.Color.White;
            this.DownloadPanel3.Controls.Add(this.viewDowload);
            this.DownloadPanel3.Controls.Add(this.buttonXP2);
            this.DownloadPanel3.DrBottom = false;
            this.DownloadPanel3.DrLeft = false;
            this.DownloadPanel3.DrRight = false;
            this.DownloadPanel3.DrTop = true;
            this.DownloadPanel3.Location = new System.Drawing.Point(396, 266);
            this.DownloadPanel3.mStrText = null;
            this.DownloadPanel3.Name = "DownloadPanel3";
            this.DownloadPanel3.Size = new System.Drawing.Size(316, 40);
            this.DownloadPanel3.TabIndex = 11;
            this.DownloadPanel3.Visible = false;
            // 
            // viewDowload
            // 
            this.viewDowload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.viewDowload.AutoSize = true;
            this.viewDowload.BackColor = System.Drawing.Color.Transparent;
            this.viewDowload.BackColorDown = System.Drawing.Color.Silver;
            this.viewDowload.BackColorEX = System.Drawing.Color.White;
            this.viewDowload.BackColorLeave = System.Drawing.Color.White;
            this.viewDowload.BackColorMove = System.Drawing.Color.Gainsboro;
            this.viewDowload.FontM = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.viewDowload.ImageDefault = ((System.Drawing.Image)(resources.GetObject("viewDowload.ImageDefault")));
            this.viewDowload.ImageDown = null;
            this.viewDowload.ImageLeave = null;
            this.viewDowload.ImageMove = null;
            this.viewDowload.Location = new System.Drawing.Point(253, 7);
            this.viewDowload.Name = "viewDowload";
            this.viewDowload.Size = new System.Drawing.Size(29, 27);
            this.viewDowload.TabIndex = 2;
            this.viewDowload.TextColor = System.Drawing.Color.Black;
            this.viewDowload.TextEX = "";
            this.viewDowload.ButtonClick += new System.EventHandler(this.viewDowload_ButtonClick);
            // 
            // buttonXP2
            // 
            this.buttonXP2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonXP2.AutoSize = true;
            this.buttonXP2.BackColor = System.Drawing.Color.Transparent;
            this.buttonXP2.BackColorDown = System.Drawing.Color.Silver;
            this.buttonXP2.BackColorEX = System.Drawing.Color.White;
            this.buttonXP2.BackColorLeave = System.Drawing.Color.White;
            this.buttonXP2.BackColorMove = System.Drawing.Color.Gainsboro;
            this.buttonXP2.FontM = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonXP2.ImageDefault = ((System.Drawing.Image)(resources.GetObject("buttonXP2.ImageDefault")));
            this.buttonXP2.ImageDown = null;
            this.buttonXP2.ImageLeave = null;
            this.buttonXP2.ImageMove = null;
            this.buttonXP2.Location = new System.Drawing.Point(283, 7);
            this.buttonXP2.Name = "buttonXP2";
            this.buttonXP2.Size = new System.Drawing.Size(29, 27);
            this.buttonXP2.TabIndex = 1;
            this.buttonXP2.TextColor = System.Drawing.Color.Black;
            this.buttonXP2.TextEX = "";
            this.buttonXP2.ButtonClick += new System.EventHandler(this.buttonXP2_ButtonClick);
            // 
            // PanelSearch
            // 
            this.PanelSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelSearch.BackColor = System.Drawing.Color.White;
            this.PanelSearch.Controls.Add(this.TxtSearch);
            this.PanelSearch.Controls.Add(this.BtnNextSearch);
            this.PanelSearch.Controls.Add(this.BtnPrevSearch);
            this.PanelSearch.Controls.Add(this.BtnCloseSearch);
            this.PanelSearch.DrBottom = false;
            this.PanelSearch.DrLeft = false;
            this.PanelSearch.DrRight = false;
            this.PanelSearch.DrTop = false;
            this.PanelSearch.Location = new System.Drawing.Point(393, 225);
            this.PanelSearch.mStrText = null;
            this.PanelSearch.Name = "PanelSearch";
            this.PanelSearch.Size = new System.Drawing.Size(231, 31);
            this.PanelSearch.TabIndex = 1;
            this.PanelSearch.Visible = false;
            // 
            // TxtSearch
            // 
            this.TxtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtSearch.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSearch.Location = new System.Drawing.Point(4, 8);
            this.TxtSearch.Name = "TxtSearch";
            this.TxtSearch.Size = new System.Drawing.Size(141, 17);
            this.TxtSearch.TabIndex = 10;
            this.TxtSearch.TextChanged += new System.EventHandler(this.TxtSearch_TextChanged);
            // 
            // BtnNextSearch
            // 
            this.BtnNextSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnNextSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnNextSearch.ForeColor = System.Drawing.Color.White;
            this.BtnNextSearch.Image = ((System.Drawing.Image)(resources.GetObject("BtnNextSearch.Image")));
            this.BtnNextSearch.Location = new System.Drawing.Point(176, 2);
            this.BtnNextSearch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BtnNextSearch.Name = "BtnNextSearch";
            this.BtnNextSearch.Size = new System.Drawing.Size(25, 26);
            this.BtnNextSearch.TabIndex = 9;
            this.BtnNextSearch.Tag = "Find next (Enter)";
            this.BtnNextSearch.UseVisualStyleBackColor = true;
            this.BtnNextSearch.Click += new System.EventHandler(this.BtnNextSearch_Click);
            // 
            // BtnPrevSearch
            // 
            this.BtnPrevSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnPrevSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnPrevSearch.ForeColor = System.Drawing.Color.White;
            this.BtnPrevSearch.Image = ((System.Drawing.Image)(resources.GetObject("BtnPrevSearch.Image")));
            this.BtnPrevSearch.Location = new System.Drawing.Point(151, 2);
            this.BtnPrevSearch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BtnPrevSearch.Name = "BtnPrevSearch";
            this.BtnPrevSearch.Size = new System.Drawing.Size(25, 26);
            this.BtnPrevSearch.TabIndex = 8;
            this.BtnPrevSearch.Tag = "Find previous (Shift+Enter)";
            this.BtnPrevSearch.UseVisualStyleBackColor = true;
            this.BtnPrevSearch.Click += new System.EventHandler(this.BtnPrevSearch_Click);
            // 
            // BtnCloseSearch
            // 
            this.BtnCloseSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCloseSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnCloseSearch.ForeColor = System.Drawing.Color.White;
            this.BtnCloseSearch.Image = ((System.Drawing.Image)(resources.GetObject("BtnCloseSearch.Image")));
            this.BtnCloseSearch.Location = new System.Drawing.Point(202, 2);
            this.BtnCloseSearch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BtnCloseSearch.Name = "BtnCloseSearch";
            this.BtnCloseSearch.Size = new System.Drawing.Size(25, 26);
            this.BtnCloseSearch.TabIndex = 7;
            this.BtnCloseSearch.Tag = "Close (Esc)";
            this.BtnCloseSearch.UseVisualStyleBackColor = true;
            this.BtnCloseSearch.Click += new System.EventHandler(this.BtnCloseSearch_Click);
            // 
            // textShow
            // 
            this.textShow.AllowReturn = false;
            tTextBoxBorderRenderStyle1.LineColor = System.Drawing.Color.LightGray;
            tTextBoxBorderRenderStyle1.LineWidth = 1F;
            this.textShow.BorderRenderStyle = tTextBoxBorderRenderStyle1;
            this.textShow.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textShow.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textShow.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.textShow.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textShow.Location = new System.Drawing.Point(204, 88);
            this.textShow.Margin = new System.Windows.Forms.Padding(0);
            this.textShow.Multiline = true;
            this.textShow.Name = "textShow";
            this.textShow.Size = new System.Drawing.Size(125, 38);
            this.textShow.TabIndex = 9;
            this.textShow.TextMargin = new System.Windows.Forms.Padding(1);
            this.textShow.Visible = false;
            this.textShow.WordWrap = false;
            // 
            // addrPanel3
            // 
            this.addrPanel3.BackColor = System.Drawing.Color.White;
            this.addrPanel3.Controls.Add(this.label2);
            this.addrPanel3.Controls.Add(this.textBox2);
            this.addrPanel3.DrBottom = false;
            this.addrPanel3.DrLeft = false;
            this.addrPanel3.DrRight = false;
            this.addrPanel3.DrTop = false;
            this.addrPanel3.Location = new System.Drawing.Point(349, 107);
            this.addrPanel3.mStrText = null;
            this.addrPanel3.Name = "addrPanel3";
            this.addrPanel3.Size = new System.Drawing.Size(125, 28);
            this.addrPanel3.TabIndex = 2;
            this.addrPanel3.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(3, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 20);
            this.label2.TabIndex = 8;
            this.label2.Text = "缩放比例:";
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.BackColor = System.Drawing.Color.White;
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Font = new System.Drawing.Font("微软雅黑", 11.5F);
            this.textBox2.Location = new System.Drawing.Point(74, 4);
            this.textBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(48, 21);
            this.textBox2.TabIndex = 6;
            // 
            // faTabStrip1
            // 
            this.faTabStrip1.AllowDrop = true;
            this.faTabStrip1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.faTabStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.faTabStrip1.FlashFullSC = false;
            this.faTabStrip1.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.faTabStrip1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.faTabStrip1.InControlHeight = 0;
            this.faTabStrip1.Location = new System.Drawing.Point(1, 1);
            this.faTabStrip1.MemCostLower = false;
            this.faTabStrip1.Name = "faTabStrip1";
            this.faTabStrip1.RemoveALLItem = false;
            this.faTabStrip1.Size = new System.Drawing.Size(835, 471);
            this.faTabStrip1.TabIndex = 6;
            this.faTabStrip1.Text = "faTabStrip1";
            this.faTabStrip1.TabStripItemClosed += new System.EventHandler(this.FaTabStrip1_TabStripItemClosed);
            this.faTabStrip1.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.faTabStrip1.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.FaTabStrip1_ControlAdded);
            this.faTabStrip1.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.FaTabStrip1_ControlRemoved);
            this.faTabStrip1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FaTabStrip1_KeyUp);
            this.faTabStrip1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            // 
            // fMenuStrip1
            // 
            this.fMenuStrip1.AutoSize = false;
            this.fMenuStrip1.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.fMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.fMenuStrip1.Name = "fMenuStrip1";
            this.fMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // addrPanel2
            // 
            this.addrPanel2.BackColor = System.Drawing.SystemColors.Control;
            this.addrPanel2.DrBottom = false;
            this.addrPanel2.DrLeft = false;
            this.addrPanel2.DrRight = false;
            this.addrPanel2.DrTop = false;
            this.addrPanel2.Location = new System.Drawing.Point(0, 105);
            this.addrPanel2.mStrText = null;
            this.addrPanel2.Name = "addrPanel2";
            this.addrPanel2.Size = new System.Drawing.Size(786, 26);
            this.addrPanel2.TabIndex = 0;
            // 
            // FavPanel
            // 
            this.FavPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FavPanel.BackColor = System.Drawing.SystemColors.Control;
            this.FavPanel.DrBottom = true;
            this.FavPanel.DrLeft = false;
            this.FavPanel.DrRight = false;
            this.FavPanel.DrTop = false;
            this.FavPanel.Location = new System.Drawing.Point(1, 76);
            this.FavPanel.mStrText = null;
            this.FavPanel.Name = "FavPanel";
            this.FavPanel.Size = new System.Drawing.Size(835, 28);
            this.FavPanel.TabIndex = 1;
            this.FavPanel.LostFocus += new System.EventHandler(this.FMenuStrip1_LostFocus);
            this.FavPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FavPanel_MouseUp);
            // 
            // ToolsPanel
            // 
            this.ToolsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ToolsPanel.BackColor = System.Drawing.SystemColors.Control;
            this.ToolsPanel.Controls.Add(this.BtnSettings);
            this.ToolsPanel.Controls.Add(this.BtnMUserSupport);
            this.ToolsPanel.Controls.Add(this.addrPanel1);
            this.ToolsPanel.Controls.Add(this.BtBack);
            this.ToolsPanel.Controls.Add(this.BtHome);
            this.ToolsPanel.Controls.Add(this.BtForward);
            this.ToolsPanel.Controls.Add(this.BtReflash);
            this.ToolsPanel.DrBottom = false;
            this.ToolsPanel.DrLeft = false;
            this.ToolsPanel.DrRight = false;
            this.ToolsPanel.DrTop = false;
            this.ToolsPanel.Location = new System.Drawing.Point(1, 43);
            this.ToolsPanel.mStrText = null;
            this.ToolsPanel.Name = "ToolsPanel";
            this.ToolsPanel.Size = new System.Drawing.Size(835, 32);
            this.ToolsPanel.TabIndex = 9;
            // 
            // formMinButton1
            // 
            this.formMinButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.formMinButton1.AutoSize = true;
            this.formMinButton1.BackColor = System.Drawing.Color.Transparent;
            this.formMinButton1.BackColorDown = System.Drawing.Color.OldLace;
            this.formMinButton1.BackColorEX = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.formMinButton1.BackColorLeave = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.formMinButton1.BackColorMove = System.Drawing.Color.WhiteSmoke;
            this.formMinButton1.FontM = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.formMinButton1.ImageDefault = global::CefiBrowser.Properties.Resources.win10min;
            this.formMinButton1.ImageDown = null;
            this.formMinButton1.ImageLeave = null;
            this.formMinButton1.ImageMove = null;
            this.formMinButton1.Location = new System.Drawing.Point(749, 1);
            this.formMinButton1.Name = "formMinButton1";
            this.formMinButton1.Size = new System.Drawing.Size(28, 23);
            this.formMinButton1.TabIndex = 12;
            this.formMinButton1.TextColor = System.Drawing.Color.Black;
            this.formMinButton1.TextEX = "";
            this.formMinButton1.ButtonClick += new System.EventHandler(this.formMinButton1_Click);
            // 
            // formMaxNormalButton1
            // 
            this.formMaxNormalButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.formMaxNormalButton1.AutoSize = true;
            this.formMaxNormalButton1.BackColor = System.Drawing.Color.Transparent;
            this.formMaxNormalButton1.BackColorDown = System.Drawing.Color.OldLace;
            this.formMaxNormalButton1.BackColorEX = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.formMaxNormalButton1.BackColorLeave = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.formMaxNormalButton1.BackColorMove = System.Drawing.Color.WhiteSmoke;
            this.formMaxNormalButton1.FontM = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.formMaxNormalButton1.ImageDefault = ((System.Drawing.Image)(resources.GetObject("formMaxNormalButton1.ImageDefault")));
            this.formMaxNormalButton1.ImageDown = null;
            this.formMaxNormalButton1.ImageLeave = null;
            this.formMaxNormalButton1.ImageMove = null;
            this.formMaxNormalButton1.Location = new System.Drawing.Point(777, 1);
            this.formMaxNormalButton1.Name = "formMaxNormalButton1";
            this.formMaxNormalButton1.Size = new System.Drawing.Size(28, 23);
            this.formMaxNormalButton1.TabIndex = 13;
            this.formMaxNormalButton1.TextColor = System.Drawing.Color.Black;
            this.formMaxNormalButton1.TextEX = "";
            this.formMaxNormalButton1.ButtonClick += new System.EventHandler(this.formMaxNormalButton1_Click);
            // 
            // formCloseButton1
            // 
            this.formCloseButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.formCloseButton1.AutoSize = true;
            this.formCloseButton1.BackColor = System.Drawing.Color.Transparent;
            this.formCloseButton1.BackColorDown = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.formCloseButton1.BackColorEX = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.formCloseButton1.BackColorLeave = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.formCloseButton1.BackColorMove = System.Drawing.Color.Red;
            this.formCloseButton1.FontM = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.formCloseButton1.ImageDefault = global::CefiBrowser.Properties.Resources.win10close;
            this.formCloseButton1.ImageDown = global::CefiBrowser.Properties.Resources.win10close;
            this.formCloseButton1.ImageLeave = global::CefiBrowser.Properties.Resources.win10close;
            this.formCloseButton1.ImageMove = global::CefiBrowser.Properties.Resources.win10closeIn;
            this.formCloseButton1.Location = new System.Drawing.Point(805, 1);
            this.formCloseButton1.Name = "formCloseButton1";
            this.formCloseButton1.Size = new System.Drawing.Size(31, 23);
            this.formCloseButton1.TabIndex = 15;
            this.formCloseButton1.TextColor = System.Drawing.Color.Black;
            this.formCloseButton1.TextEX = "";
            this.formCloseButton1.ButtonClick += new System.EventHandler(this.formCloseButton1_Click);
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(228)))), ((int)(((byte)(228)))));
            this.ClientSize = new System.Drawing.Size(837, 473);
            this.Controls.Add(this.DownloadPanel3);
            this.Controls.Add(this.PanelSearch);
            this.Controls.Add(this.formCloseButton1);
            this.Controls.Add(this.formMaxNormalButton1);
            this.Controls.Add(this.formMinButton1);
            this.Controls.Add(this.textShow);
            this.Controls.Add(this.ToolsPanel);
            this.Controls.Add(this.FavPanel);
            this.Controls.Add(this.addrPanel3);
            this.Controls.Add(this.faTabStrip1);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Activated += new System.EventHandler(this.Form1_Activeated);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.addrPanel1.ResumeLayout(false);
            this.addrPanel1.PerformLayout();
            this.FaTabMenu.ResumeLayout(false);
            this.Favri_BarMenu.ResumeLayout(false);
            this.Settings_Menu.ResumeLayout(false);
            this.DownloadPanel3.ResumeLayout(false);
            this.DownloadPanel3.PerformLayout();
            this.PanelSearch.ResumeLayout(false);
            this.PanelSearch.PerformLayout();
            this.addrPanel3.ResumeLayout(false);
            this.addrPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.faTabStrip1)).EndInit();
            this.ToolsPanel.ResumeLayout(false);
            this.ToolsPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion
        private ButtonXP BtnSettings;
        private ButtonXP BtHome;
        private ButtonXP BtBack;
        private ButtonXP BtForward;
        private ButtonXP BtReflash;
        public FATabStrip faTabStrip1;
        private System.Windows.Forms.ContextMenuStrip FaTabMenu;
        private System.Windows.Forms.ToolStripMenuItem NewTabItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem CloseTabItem;
        private System.Windows.Forms.ToolStripMenuItem CloseAtherTabItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem CloseRightTabItem;
        private System.Windows.Forms.ToolStripMenuItem CloseLeftabItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem 显示书签栏SToolStripMenuItem;
        public System.Windows.Forms.ContextMenuStrip Favri_BarMenu;
        private System.Windows.Forms.ToolStripMenuItem Edit_FavriBT;
        public System.Windows.Forms.ToolStripMenuItem TwoOpenNewTableitem;
        public System.Windows.Forms.ToolStripMenuItem CopyFavriBtURL;
        public System.Windows.Forms.ToolStripMenuItem Favi_Modif_Item;
        public System.Windows.Forms.ToolStripMenuItem DelCurrentFavriBT;
        public System.Windows.Forms.ToolStripMenuItem OpenRULInNewTab;
        public System.Windows.Forms.ToolStripSeparator Favi_3_Line;
        public System.Windows.Forms.ToolStripMenuItem AddFavri_Folder;
        public FMenuStrip fMenuStrip1;
         public ToolStripMenuItem KuaYu_Support;
        public ToolStripMenuItem MulitiUsers_Support;
        public ToolStripMenuItem Clear_Cache;
        private ToolStripSeparator toolStripSeparator4;
        public ContextMenuStrip Settings_Menu;
        private ButtonXP BtnMUserSupport;
        private Button BtnNextSearch;
        private Button BtnPrevSearch;
        private Button BtnCloseSearch;
        private ToolStripMenuItem sBtnSear;
        public AddrPanel PanelSearch;

        public AddrPanel addrPanel3;
        private Label label2;
        private ToolStripSeparator toolStripSeparator7;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripSeparator toolStripSeparator6;
        public TxtBoxXP textShow;
        public TextBox TxtSearch;
        private ButtonXP buttonXP2;
        public AddrPanel DownloadPanel3;
        private ButtonXP viewDowload;
        public ToolStripMenuItem DownloadToolStripMenuItem;
        public TextBox textBox2;
        public ToolStripMenuItem EditFaviFolder;
        private ToolStripMenuItem OpneNFataItem;
        private ToolStripMenuItem OpenNCefiBrowser;
        private ToolStripSeparator toolStripSeparator9;
        private ToolStripMenuItem toolStripMenuItem4;
        private ToolStripSeparator toolStripSeparator10;
        private ToolStripMenuItem toolStripMenuItem5;
        private ToolStripMenuItem CefiBPrint;
        private ToolStripMenuItem PrinttoPDF;
        private ToolStripMenuItem toolStripMenuItem8;
        private ToolStripMenuItem F12Tools;
        public ToolStripMenuItem MemSetting;
        private AddrPanel addrPanel1;
        public TextBox textBoxXP1;
        private ButtonXP BtFav;
        public ButtonXP BtnSeach;
        private AddrPanel addrPanel2;
        public AddrPanel FavPanel;
        public AddrPanel ToolsPanel;
        public ButtonXP formMinButton1;
        public ButtonXP formMaxNormalButton1;
        public ButtonXP formCloseButton1;
    }
}

