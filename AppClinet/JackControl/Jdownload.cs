using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CefiBrowser
{
    public partial class Jdownload : UserControl
    {
        [DllImport("shell32.dll", ExactSpelling = true)]
        private static extern void ILFree(IntPtr pidlList);

        [DllImport("shell32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        private static extern IntPtr ILCreateFromPathW(string pszPath);

        [DllImport("shell32.dll", ExactSpelling = true)]
        private static extern int SHOpenFolderAndSelectItems(IntPtr pidlList, uint cild, IntPtr children, uint dwFlags);
        private static Font defaultFont = new Font("Tahoma", 8.25f, FontStyle.Regular);
        private StringFormat sf = null;
        public Jdownload()
        {
            //首先开启双缓冲，防止闪烁
            //双缓冲的一大堆设置 具体参数含义参照msdn的ControlStyles枚举值
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            sf = new StringFormat();
            sf.Trimming = StringTrimming.EllipsisCharacter; //超长时最后三个字符以...代替
            sf.FormatFlags |= StringFormatFlags.NoWrap;
            sf.FormatFlags &= StringFormatFlags.DirectionRightToLeft;

            InitializeComponent();


        }
        /// <summary>
        /// 下载文件的URL链接
        /// </summary>
        public string DownloadUrl { get; set; }

        /// <summary>
        /// 文件存储完整路径
        /// </summary>
        public string FullFilePaths { get; set; }

        /// <summary>
        /// 下载文件的图标
        /// </summary>
        public Image Vimage { get; set; }

        /// <summary>
        /// 下载的文件名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Image的 Base64str码，用来做图标转换和还原
        /// </summary>
        public string ImageBase64str { set; get; }

        /// <summary>
        /// 下载日期
        /// </summary>
        public string DownLoadTime { set; get; }

        /// <summary>
        /// 已下载的程序是否删除
        /// </summary>
        public bool FileAlreadyDele { set; get; }

        /// <summary>
        /// 程序是否下载完成
        /// </summary>
        public bool IsDownloading { set; get; }
        /// <summary>
        /// 下载的进程ID
        /// </summary>
        public string DownloadID { set; get; }
        /// <summary>
        /// 开始下载的时间，这个是一个主键，也是关键的主键值 
        /// </summary>
        public string Startime { set; get; }
        private void Jdownload_Load(object sender, EventArgs e)
        {
            label1.Font = new Font("Tahoma", 8.33f, FontStyle.Bold);
            addrPanel2.Font = new Font("Tahoma", 10.33f, FontStyle.Bold);
            mDownloadTextBTN1.Font = new Font("Tahoma", 10.66f, FontStyle.Regular);
            OpenFileBtn.FontM = new Font("Tahoma", 9.13f, FontStyle.Regular);
            label1.ForeColor = OpenFileBtn.TextColor = Color.FromArgb(26, 115, 232);
            mDownloadTextBTN1.ForeColor = Color.DimGray;
            mDownloadTextBTN1.Cursor = OpenFileBtn.Cursor = addrPanel2.Cursor= Cursors.Hand;
            this.buttonXP1.Location = new Point(this.Width - buttonXP1.Width - 1, buttonXP1.Location.Y-2);
            if (!File.Exists(this.FullFilePaths))
            {
                addrPanel2.ForeColor = Color.Red;
                addrPanel2.Font = new Font("Tahoma", 12.66f, FontStyle.Regular);
                addrPanel2.Cursor = Cursors.Default;
            }
            else
            {
                addrPanel2.ForeColor = Color.Blue;
            }

            this.addrPanel2.mStrText = FileName;

            Graphics g = this.CreateGraphics();
            Font sf = new Font("Tahoma", 12.66f, FontStyle.Regular);
            //单位为mm
            g.PageUnit = GraphicsUnit.Millimeter;
            //测量字符串长度
            Size sif = TextRenderer.MeasureText(g, this.addrPanel2.mStrText, sf, new Size(0, 0), TextFormatFlags.NoPadding);

            addrPanel2.Width = sif.Width;
            if (PublicClass.DpiX >= 120)
                addrPanel2.Width =  addrPanel2.Width +23;
            else
                addrPanel2.Width = addrPanel2.Width+13;

            if (addrPanel2.Width > this.Width - addrPanel1.Width - buttonXP1.Width - 30)
                addrPanel2.Width = this.Width - addrPanel1.Width - buttonXP1.Width - 24;

            if (mDownloadTextBTN1.Width > this.Width- addrPanel1.Width -buttonXP1.Width-20)
                mDownloadTextBTN1.Width = this.Width - addrPanel1.Width - buttonXP1.Width - 14;


        }
        private void ButtonXP1_ButtonClick(object sender, System.EventArgs e)
        {
            //这里就是当点删除按钮的时候就删除当前Item，同时要删除所在在List中的记录
            Panel mListPanel = new Panel();
            FATabStripItem fATabStripItem = new FATabStripItem();
            fATabStripItem.Title = "";
            mDownloadRecode.DelBookmarks(this);//删除自己所在List中的数据
            MainForm.Instance.Invoke(new Action(() =>
            {
                for(int i=0; i < MainForm.Instance.faTabStrip1.Items.Count;i++)
                {
                    if (MainForm.Instance.faTabStrip1.Items[i].Title == CefConstHelper.CefDownloadTitle)
                    {
                        fATabStripItem = MainForm.Instance.faTabStrip1.Items[i];
                        break;
                    }
                }
                if(fATabStripItem.Title == CefConstHelper.CefDownloadTitle)
                {
                    for (int k = 1; k < fATabStripItem.splic.Panel1.Controls.Count;k++)
                    {
                        if(((Jdownload)fATabStripItem.splic.Panel1.Controls[k]).FullFilePaths == this.FullFilePaths)
                        {
                            fATabStripItem.splic.Panel1.Controls.Remove(fATabStripItem.splic.Panel1.Controls[k]);
                            for (int y= k; y< fATabStripItem.splic.Panel1.Controls.Count ;y++ )
                            {
                                fATabStripItem.splic.Panel1.Controls[y].Location = new Point(this.Location.X, 
                                    fATabStripItem.splic.Panel1.Controls[y].Location.Y- fATabStripItem.splic.Panel1.Controls[y].Height-20);
                            }
                          
                            break;
                        }
                    }
                 }
             }
            ));
         }

        private void MDownloadTextBTN1_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            MainForm.Instance.Invoke(new Action(() =>
            {
                MainForm.Instance.AddNewBrowserTab(this.DownloadUrl);
            }
           ));
        }

        private void JSizeChanged(object sender, EventArgs e)
        {
            buttonXP1.Location = new Point(this.Width - buttonXP1.Width - 94, this.Location.Y + 3);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Rectangle borderRc = this.ClientRectangle; ;
            borderRc.Width--;
            borderRc.Height--;
            e.Graphics.DrawRectangle(SystemPens.ControlDark, borderRc);
        }

        private void OpenFileBtn_Load(object sender, EventArgs e)
        {
            if (!File.Exists(this.FullFilePaths) && !Directory.Exists(FullFilePaths))
                return;

            if (Directory.Exists(FullFilePaths))
                Process.Start(@"explorer.exe", "/select,\"" + FullFilePaths + "\"");
            else
            {
                IntPtr pidlList = ILCreateFromPathW(FullFilePaths);
                if (pidlList != IntPtr.Zero)
                {
                    try
                    {
                        Marshal.ThrowExceptionForHR(SHOpenFolderAndSelectItems(pidlList, 0, IntPtr.Zero, 0));
                    }
                    finally
                    {
                        ILFree(pidlList);
                    }
                }
            }
        }


        //当单击文件名时检查文件名是否存在，如果不存在要显示删除状态
        private void AddrPanel2_MouseUp(object sender, MouseEventArgs e)
        {
            if (File.Exists(this.FullFilePaths))
            {
                //addrPanel2.mStrText = addrPanel2.mStrText + "  此文件已被删除";
                //addrPanel2.ForeColor = Color.Red;
                //addrPanel2.Font = new Font("Tahoma", 10.33f, FontStyle.Regular);
                //addrPanel2.Invalidate();
                try
                {
                    Process process = new Process();
                    //process.StartInfo.UseShellExecute = false;
                    process.StartInfo.FileName = this.FullFilePaths;
                    //process.StartInfo.CreateNoWindow = true;
                    process.Start();
                }
                catch { }
            }
        }
    }
}
