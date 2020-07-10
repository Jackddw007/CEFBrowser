

namespace CefiBrowser
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Net;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using Xilium.CefGlue;

    internal  class CefWebContextMenuHandler : CefContextMenuHandler
    {
        private readonly CefWebBrowser _core;
        public static string SearStr; //要收索的内容
        private string linkrul;
        private static string Url = @"((http|ftp|https)://)(([a-zA-Z0-9\._-]+\.[a-zA-Z]{2,6})|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(:[0-9]{1,4})*(/[a-zA-Z0-9\&%_\./-~-]*)?";

        public CefWebContextMenuHandler(CefWebBrowser core)
        {
            _core = core;
        }

        protected override void OnBeforeContextMenu(CefBrowser browser, CefFrame frame, CefContextMenuParams state, CefMenuModel model)
        {
            if (model.Count > 0)
            {
                model.Clear();
            }

            //bool removed = model.Remove(CefMenuCommand.ViewSource); // Remove "View Source" option
            if (state.LinkUrl != null || IsUrl(state.SelectionText))
            {
                
                model.AddItem(133, "新窗口中打开链接");
                model.AddItem(134, "复制链接");
                model.AddSeparator();
            }
            model.AddItem(135, "搜索此内容");

            if(state.MediaType == CefContextMenuMediaType.Image)
            {
                SearStr = state.SelectionText;
                model.AddItem(136, "图片另存为");
            }
            model.AddSeparator();

            model.AddItem(100, "复制");
            model.AddItem(114, "粘贴");
            model.AddSeparator();
            model.AddItem(200, "返回");
            model.AddItem(300, "前进");
            model.AddItem(350, "刷新");
            model.AddItem(113, "停止");
            model.AddSeparator();

            if(_core.Browser.GetHost() != null)
            {
              //  model.AddSeparator();
                model.AddItem(402, "打印此页");
                model.AddItem(401, "页面另存为PDF");
                model.AddSeparator();
            }
            if (!_core.DevToolsOpen)
                model.AddItem(400, "打开调试工具");
            else
                model.AddItem(400, "关闭调试工具");
            if (state.SelectionText != null)
            {
                model.SetEnabled(100, true);
            }
            else
            {
                model.SetEnabled(100, false);
            }
            if (Clipboard.GetText() == "")
            {
                model.SetEnabled(114, false);
                
            }
            else
            {
                model.SetEnabled(114, true); 
            }


            if (state.SelectionText != "" && state.SelectionText!=null)
            {
                SearStr = state.SelectionText;
                model.SetEnabled(135, true);

            }
            else
                model.SetEnabled(135, false);

            if (browser.IsLoading)
                model.SetEnabled(113, true);
            else
                model.SetEnabled(113, false);


            if (browser.CanGoBack)
            {
                model.SetEnabled(200, true);
            }
            else
            {
                model.SetEnabled(200, false);
            }
            if (browser.CanGoForward)
            {
                model.SetEnabled(300, true);
            }
            else
            {
                model.SetEnabled(300, false);
            }

        }

        protected override bool OnContextMenuCommand(CefBrowser browser, CefFrame frame, CefContextMenuParams state, int commandId, CefEventFlags eventFlags)
        {

            switch (commandId)
            {
                case 134:
                    Clipboard.SetText(state.LinkUrl);
                    return true;
                case 133:
                    //在新窗口中打开这个链接
                    if (state.LinkUrl != null)
                        linkrul = state.LinkUrl;
                    else if(IsUrl(state.SelectionText))
                        linkrul = state.SelectionText;

                    MainForm.Instance.Invoke(new Action(() =>
                    {
                        MainForm.Instance.AddNewBrowserTab(linkrul);
                    }));
                    linkrul = "";
                    GC.Collect();
                    return true;

                case 135: //在新窗口中打开链接并搜索选中的内容
                    MainForm.Instance.Invoke(new Action(() =>
                    {
                        FATabStripItem fATab = new FATabStripItem();
                        fATab.TabIndex = MainForm.Instance.faTabStrip1.SelectedItem.TabIndex + 1;
                        MainForm.Instance.faTabStrip1.InsetTab(fATab, true, fATab.TabIndex);
                        MainForm.Instance.PubSeach(MainForm.Instance.faTabStrip1.SelectedItem, SearStr);
                        fATab = null;
                        GC.Collect();
                    }));
                    return true;
                case 136: //图片另存为
                    SaveFileDialog savedialog = new SaveFileDialog();
                    savedialog.Filter = "Jpg 图片|*.jpg|Bmp 图片|*.bmp|Gif 图片|*.gif|Png 图片|*.png|Wmf  图片|*.wmf";
                    savedialog.FilterIndex = 0;
                    savedialog.RestoreDirectory = true;//保存对话框是否记忆上次打开的目录
                    savedialog.CheckPathExists = true;//检查目录
                    savedialog.FileName = System.DateTime.Now.ToString("yyyyMMddHHmmss") + "-"; //设置默认文件名
                    if (savedialog.ShowDialog() == DialogResult.OK)
                    {
                        browser.GetHost().DownloadImage(state.SourceUrl, false, 0, false, new CefWebDownloadImageCallbackHandler(savedialog.FileName, -1,null,""));
                        //WriteBytesToFile(savedialog.FileName, GetBytesFromUrl(state.SourceUrl));
                        //Image.Save(, System.Drawing.Imaging.ImageFormat.Jpeg);// image为要保存的图片
                        MessageBox.Show("图片保存成功！", "信息提示");
                    }
                    savedialog = null;
                    GC.Collect();
                    return true;
                case 100:
                    Clipboard.SetText(state.SelectionText);
                    return true;
                case 114:
                    frame.Paste();
                    return true;
                case 113:
                    browser.StopLoad();
                    return true;
                case 200:
                    browser.GoBack();
                    return true;
                case 300:
                    browser.GoForward();
                    return true;
                case 350: //刷新
                      PublicClass.ReflashBrowser(browser, _core);
                    return true;
                case 400:
                    //browser;
                    //var host = browser.GetHost();
                    //var wi = CefWindowInfo.Create();
                    //wi.SetAsPopup(IntPtr.Zero, "DevTools");
                    //host.ShowDevTools(wi, new DevToolsWebClient(), new CefBrowserSettings(), new CefPoint(0, 0));
                    PublicClass.DevTools(_core, browser);
                    return true;

                case 401:
                    SaveFileDialog pdfsave = new SaveFileDialog();
                    pdfsave.Filter = "PDF文件|*.pdf";
                    pdfsave.FilterIndex = 0;
                    pdfsave.RestoreDirectory = true;//保存对话框是否记忆上次打开的目录
                    pdfsave.CheckPathExists = true;//检查目录
                    pdfsave.FileName = _core.Title;// System.DateTime.Now.ToString("yyyyMMddHHmmss") + "-"; ;//设置默认文件名

                    if (pdfsave.ShowDialog() == DialogResult.OK)
                    {
                        _core.Browser.GetHost().PrintToPdf(pdfsave.FileName, new CefPdfPrintSettings(), new CefWebPdfPrinerHandler());
                    }
                    pdfsave = null;

                    return true;
                case 402:
                    _core.Browser.GetHost().Print(); //print web page
                    return true;
                default:  // Allow default handling, if any.
                    return false;
            }
            
        }



        //判断URL是否非法,URL正测表达式
        public  bool IsUrl(string str)
        {
            try
            {
                return Regex.IsMatch(str, Url);
            }
            catch
            {
                return false;
            }
        }


    }


}
