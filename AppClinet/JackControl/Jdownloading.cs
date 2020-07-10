using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace CefiBrowser
{

    public partial class Jdownloading : UserControl
    {
        [DllImport("shell32.dll", ExactSpelling = true)]
        private static extern void ILFree(IntPtr pidlList);

        [DllImport("shell32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        private static extern IntPtr ILCreateFromPathW(string pszPath);

        [DllImport("shell32.dll", ExactSpelling = true)]
        private static extern int SHOpenFolderAndSelectItems(IntPtr pidlList, uint cild, IntPtr children, uint dwFlags);

        public Jdownloading()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 下载的Index Number，这里是为计算多个下任务同时下载时的区分
        /// </summary>
        public string DownloadIndex { get;set;}

        /// <summary>
        /// 是否下载完成
        /// </summary>
        public bool IsDownloaded { get; set; }

        /// <summary>
        /// 下载文件完整的存储路径，包括文件名
        /// </summary>
        public string FilePaths { get; set; }
        /// <summary>
        /// 正在下载的文件名
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 取消下载
        /// </summary>
        public bool CancelDowning { get; set; }

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
        /// Image的 Base64str码，用来做图标转换和还原
        /// </summary>
        public string ImageBase64str { set; get; }

        /// <summary>
        /// 下载日期
        /// </summary>
        public string DownLoadTime { set; get; }
        /// <summary>
        /// 开始下载的时间，这个是一个主键，也是关键的主键值 
        /// </summary>
        public string Startime { set; get; }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Rectangle borderRc = this.ClientRectangle;
            e.Graphics.DrawLine(SystemPens.ControlDark, new PointF(borderRc.Right-1 , borderRc.Top + 9),
                                new PointF(borderRc.Right-1 ,borderRc.Height-3));
        }

        private void Openfolderfile_Click(object sender, EventArgs e)
        {
            if (!File.Exists(this.FilePaths) && !Directory.Exists(FilePaths))
                return;

            if (Directory.Exists(FilePaths))
                Process.Start(@"explorer.exe", "/select,\"" + FilePaths + "\"");
            else
            {
                IntPtr pidlList = ILCreateFromPathW(FilePaths);
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

        //打开这个程序
        private void Opendownfile_Click(object sender, EventArgs e)
        {
            try
            {
                Process process = new Process();
                //process.StartInfo.UseShellExecute = false;
                process.StartInfo.FileName = FilePaths ;
                //process.StartInfo.CreateNoWindow = true;
                process.Start();
            }
            catch
            { }
        }

        private void CancelDown_Click(object sender, EventArgs e)
        {
            CancelDowning = true;
        }

        public void buttonXP1_ButtonClic(object sender, EventArgs e)
        {
            if (IsDownloaded)
            {
               // buttonXP1.ImageDefault = Resources.up1;
                this.CancelDown.Enabled = false;
            }
            else
            {
               // buttonXP1.ImageDefault = Resources.Down1;
                Opendownfile.Enabled = false;
            }
            DowncontextMenu.Show(PointToScreen( new Point(buttonXP1.Location.X-6,buttonXP1.Location.Y-DowncontextMenu.Height)));
        }
    }

   

}
