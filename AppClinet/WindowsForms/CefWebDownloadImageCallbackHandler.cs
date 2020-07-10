using CefiBrowser.Properties;
using System;
using System.Drawing;
using System.IO;
using Xilium.CefGlue;


namespace CefiBrowser
{
    internal sealed  class CefWebDownloadImageCallbackHandler : CefDownloadImageCallback
    {

        int itemNumber;
        string filename;
        LoadEndEventArgs _loadEndEventArgs;
        string _Url = string.Empty;
        public CefWebDownloadImageCallbackHandler(string _filename,int fItemNumber,LoadEndEventArgs e,string Url)
        {
            itemNumber = fItemNumber;
            filename = _filename;
            _loadEndEventArgs = e;
            _Url = Url;
        }

        protected override void OnDownloadImageFinished(string imageUrl, int httpStatusCode, CefImage image)
        {
            int imageW, imageH;
            if (itemNumber >= 0)
            {
                if (image == null || image.Width == 0)
                {
                    MainForm.Instance.faTabStrip1.Items[itemNumber].ItemIcon = Resources.blank;
                    MainForm.Instance.faTabStrip1.Items[itemNumber].BrowserIsLoading = false;
                    MainForm.Instance.faTabStrip1.Invalidate();
                }
                else
                {
                    try
                    {
                        if (((CefWebBrowser)(MainForm.Instance.faTabStrip1.Items[itemNumber].splic.Panel1.Controls[0])).Browser != null)//.Title.Contains("加载中...")!=true)
                        {
                            
                            MainForm.Instance.faTabStrip1.Items[itemNumber].ItemIcon = BytesToImage(image.GetAsPng(1f, true, out imageW, out imageH).ToArray());
                            MainForm.Instance.faTabStrip1.Items[itemNumber].BrowserIsLoading = false;
                            MainForm.Instance.faTabStrip1.Invalidate();
                        }
                        //}
                    }
                    catch
                    {
                        MainForm.Instance.faTabStrip1.Items[itemNumber].ItemIcon = Resources.blank;
                        MainForm.Instance.faTabStrip1.Items[itemNumber].BrowserIsLoading = false;
                        MainForm.Instance.faTabStrip1.Invalidate();
                    }

                }
            }
            else if (itemNumber == -1) // 的时候，则为下载图片
            {
                WriteBytesToFile(filename, image.GetAsPng(1f, true, out imageW, out imageH).ToArray());
            }
            else if (itemNumber == -2)  //下载验证码
            {
                if (image == null)
                    return;
            }
        }
        /// <summary>
        /// Convert Byte[] to Image
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static Image BytesToImage(byte[] buffer)
        {
            MemoryStream ms = new MemoryStream(buffer);
            Image image = System.Drawing.Image.FromStream(ms);
            return image;
        }

        /// <summary>
        /// 将Btyes二进制流图片写入文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="content"></param>
        public void WriteBytesToFile(string fileName, byte[] content)
        {
            FileStream fs = new FileStream(fileName, FileMode.Create);
            BinaryWriter w = new BinaryWriter(fs);
            try
            {
                w.Write(content);
            }
            finally
            {
                fs.Close();
                w.Close();
                GC.Collect();
            }

            fs = null;
            w = null;
            GC.Collect();
        }
    }
}
