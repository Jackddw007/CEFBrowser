using CefiBrowser.Properties;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Xilium.CefGlue;

namespace CefiBrowser
{
    internal sealed class CefWebDownloadHandler : CefDownloadHandler
    {
        private readonly CefWebBrowser _core;
        private static Font Label1Font = new Font("Tahoma", 11.06f, FontStyle.Regular);
        private static Font Label2Font = new Font("Tahoma", 8.13f, FontStyle.Regular);
        public CefWebDownloadHandler(CefWebBrowser core)
        {
            _core = core;
        }

        public bool CheckFileExists(string FileFullPaths)
        {
            return File.Exists(FileFullPaths);
        }

        protected override void OnBeforeDownload(CefBrowser browser, CefDownloadItem downloadItem, string suggestedName, CefBeforeDownloadCallback callback)
        {
            string DownloadID = downloadItem.Id.ToString();
            //base.OnBeforeDownload(browser, downloadItem, suggestedName, callback);
            if (suggestedName.Contains("@"))
            {
                System.Windows.Forms.MessageBox.Show(CefConstHelper.CefDownLoadWarling, "CefiBrowser", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return;
            }
            string JsuggestedName = suggestedName.Substring(0, suggestedName.LastIndexOf("."));
            string GsuggestedName = JsuggestedName;
            string JsuggestedEndName = suggestedName.Substring(suggestedName.LastIndexOf("."), suggestedName.Length - suggestedName.LastIndexOf("."));
            string Jfilepath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\downloads";
            Jdownloading dwitem = new Jdownloading();
            Jdownload WrDownloadListItem = new Jdownload();

            MainForm.Instance.Invoke(new Action(() =>
            {
                MainForm.Instance.faTabStrip1.SelectedItem.BrowserIsLoading = false; //下载的时候不显示Gif动画
                if (MainForm.Instance.DownloadPanel3.Visible == false)
                {
                    MainForm.Instance.DownloadPanel3.Visible = true;
                    MainForm.Instance.faTabStrip1.Height = MainForm.Instance.Height - MainForm.Instance.DownloadPanel3.Height;//修正下载时进度控件位置错误
                }
                for (int i = 0; i < 1000; i++)
                {
                    if (CheckFileExists(Jfilepath + "\\" + JsuggestedName + JsuggestedEndName) != true)
                    {
                        if (CheckFileExists(Jfilepath + "\\" + JsuggestedName + JsuggestedEndName + ".cfg") != true || CheckFileExists(Jfilepath + "\\" + JsuggestedName + JsuggestedEndName + ".tfg") != true)
                        {
                            break;
                        }
                        else
                        {
                            JsuggestedName = GsuggestedName + "(" + (i + 1) + ")";
                        }
                    }
                    else
                    {
                        JsuggestedName = GsuggestedName + "(" + (i + 1) + ")";
                    }
                }
                dwitem.label1.Font = Label1Font;
                dwitem.label2.Font = Label2Font;
                dwitem.label1.ForeColor = Color.DimGray;
                dwitem.label1.Text = JsuggestedName + JsuggestedEndName;
                dwitem.FileName = JsuggestedName + JsuggestedEndName;
                dwitem.label2.Text = downloadItem.PercentComplete + "%";
                //dwitem.Startime = downloadItem.StartTime.ToString().ToLower().Trim();
                dwitem.Height = 36;
                //dwitem.DownloadIndex = downloadItem.Id;
                dwitem.FilePaths = Jfilepath + "\\" + JsuggestedName + JsuggestedEndName;
                dwitem.DownloadIndex = DownloadID;
                dwitem.IsDownloaded = false;
                dwitem.Location = new Point((MainForm.Instance.DownloadPanel3.Controls.Count - 2) * dwitem.Width + 6, 2);
                MainForm.Instance.DownloadPanel3.Controls.Add(dwitem);
                if (MainForm.Instance.faTabStrip1.SelectedItem.Title == "加载中...")
                {
                    //MainForm.Instance.faTabStrip1.SelectedItem.Title = "下载...";
                    //MainForm.Instance.faTabStrip1.SelectedItem.ItemIcon = Resources.AppIconNormal;
                    MainForm.Instance.faTabStrip1.RemoveTab(MainForm.Instance.faTabStrip1.SelectedItem);
                }
            }));
            callback.Continue(Jfilepath +"\\"+ JsuggestedName + JsuggestedEndName, false); //这里true就是显示弹框，如果为False就不用弹
            //PublicClass.mDownload( downloadItem.Url, Jfilepath , JsuggestedName + JsuggestedEndName,(int)dwitem.DownloadIndex);
            //callback.Dispose();
            ////写入下载记录表中

            WrDownloadListItem.DownLoadTime = DateTime.Now.TimeOfDay.ToString();
            WrDownloadListItem.FileName = JsuggestedName + JsuggestedEndName;
            WrDownloadListItem.FullFilePaths = dwitem.FilePaths;
            WrDownloadListItem.DownloadUrl = downloadItem.Url; 
            WrDownloadListItem.DownloadID = DownloadID;
            //WrDownloadListItem.Startime = downloadItem.StartTime.ToString().ToLower().Trim();
            WrDownloadListItem.IsDownloading = true;
            WrDownloadListItem.FileAlreadyDele = false;
            WrDownloadListItem.ImageBase64str = PublicClass.IamgeToBase64(Resources.AppIconNormal); //要修改这个默认的APP图标
            WrDownloadListItem.Width = MainForm.Instance.faTabStrip1.SelectedItem.Width / 3;
            mDownloadRecode.WritemDownloadRecode(WrDownloadListItem);

            //判断下载Tab是否打开
            bool DownloadTabIsOpen = false;
            FATabStripItem fATab = new FATabStripItem();
            MainForm.Instance.Invoke(new Action(() =>
            {
                for (int i = 0; i < MainForm.Instance.faTabStrip1.Items.Count; i++)
                {
                    if (MainForm.Instance.faTabStrip1.Items[i].Title == CefConstHelper.CefDownloadTitle)
                    {
                        fATab = MainForm.Instance.faTabStrip1.Items[i];
                        DownloadTabIsOpen = true;
                        break;
                    }
                }
                if (DownloadTabIsOpen)
                {
                    MainForm.Instance.LoadDownList_Info();
                }
            }));
            Jfilepath = null;
            JsuggestedEndName = null;
            JsuggestedName = null;
            //dwitem = null;
            GsuggestedName = null;
            WrDownloadListItem = null;
            GC.Collect();
        }

        protected override void OnDownloadUpdated(CefBrowser browser, CefDownloadItem downloadItem, CefDownloadItemCallback callback)
        {
            ////base.OnDownloadUpdated(browser, downloadItem, callback);
            //PublicClass.DownloadProgress(browser, callback, downloadItem);// downloadItem.Url.Substring(downloadItem.Url.LastIndexOf("/") + 1, downloadItem.Url.Length - downloadItem.Url.LastIndexOf("/")));

            ////if (downloadItem.IsComplete)
            ////{
            ////    //  MessageBox.Show("下载成功！");
            ////    //if (browser.IsPopup && !browser.HasDocument)
            ////    //{
            ////    if (downloadItem.IsComplete)
            ////        browser.GetHost().CloseBrowser();
            ////    //}
            ////}
            CefDownloadItem cefDownloadItem = downloadItem;
            //long reciv = downloadItem.ReceivedBytes;
            try
            {
                uint ID = downloadItem.Id;
                int dPercentComplete = downloadItem.PercentComplete;
        
                if (cefDownloadItem.IsInProgress)
                {
                    CefConstHelper.IsDownloading = true;
                    if (MainForm.Instance.DownloadPanel3.Controls.Count > 2)
                    {
                        MainForm.Instance.Invoke(new Action(() =>
                        {
                            for (int i = 0; i < MainForm.Instance.faTabStrip1.Items.Count; i++)
                            {
                                if (MainForm.Instance.faTabStrip1.Items[i].Title == CefConstHelper.CefDownloadTitle)
                                {
                                    for (int k = 1; i < MainForm.Instance.faTabStrip1.Items[i].splic.Panel1.Controls.Count; k++)
                                    {
                                        if (k == MainForm.Instance.faTabStrip1.Items[i].splic.Panel1.Controls.Count)
                                            break;
                                        Jdownload mjdownload = ((Jdownload)MainForm.Instance.faTabStrip1.Items[i].splic.Panel1.Controls[k]);
                                        if (mjdownload.IsDownloading && ID.ToString() == mjdownload.DownloadID)
                                        {
                                            mjdownload.pictureBox1.Image = null;
                                            mjdownload.label1.Text = dPercentComplete.ToString() + "%";//  sender.FinishedRate.ToString() + "%";
                                                                                                       //  break;
                                        }
                                    }
                                    break;
                                }
                            }

                            for (int y = 0; y < MainForm.Instance.DownloadPanel3.Controls.Count - 2; y++)
                            {
                                Jdownloading yDown = ((Jdownloading)(MainForm.Instance.DownloadPanel3.Controls[y + 2]));
                                if (yDown.DownloadIndex == ID.ToString())
                                {
                                    ((Jdownloading)(MainForm.Instance.DownloadPanel3.Controls[y + 2])).label2.Text = dPercentComplete.ToString() + "%"; // sender.FinishedRate.ToString() + "%";
                                    if(dPercentComplete<0)
                                    {
                                        ((Jdownloading)(MainForm.Instance.DownloadPanel3.Controls[y + 2])).label2.Text = (cefDownloadItem.ReceivedBytes / 1024000f).ToString("0.00") + "MB"; // sender.FinishedRate.ToString() + "%";
                                    }
                                    if(cefDownloadItem.PercentComplete<=0 && cefDownloadItem.ReceivedBytes==0 && cefDownloadItem.TotalBytes>0)
                                    {
                                        ((Jdownloading)(MainForm.Instance.DownloadPanel3.Controls[y + 2])).label2.Text = "100%";
                                    }
                                    if (((Jdownloading)(MainForm.Instance.DownloadPanel3.Controls[y + 2])).CancelDowning)
                                    {
                                        callback.Cancel();//取消下载
                                    ((Jdownloading)(MainForm.Instance.DownloadPanel3.Controls[y + 2])).label2.ForeColor = Color.Red;
                                        ((Jdownloading)(MainForm.Instance.DownloadPanel3.Controls[y + 2])).label2.Text = "停止";
                                        ((Jdownloading)(MainForm.Instance.DownloadPanel3.Controls[y + 2])).CancelDown.Enabled = false;
                                    }
                                }
                            }

                        }));
                    }
                }
                //当下载完成时
                if (cefDownloadItem.IsComplete)
                {
                    MainForm.Instance.Invoke(new Action(() =>
                    {
                        bool downloadTabOpened = false;
                        for (int i = 0; i < MainForm.Instance.faTabStrip1.Items.Count; i++)
                        {
                            if (MainForm.Instance.faTabStrip1.Items[i].Title == CefConstHelper.CefDownloadTitle)
                            {
                                for (int k = 1; i < MainForm.Instance.faTabStrip1.Items[i].splic.Panel1.Controls.Count - 1; k++)
                                {
                                    if (k == MainForm.Instance.faTabStrip1.Items[i].splic.Panel1.Controls.Count)
                                        break;
                                    Jdownload jdownloadR = (Jdownload)(MainForm.Instance.faTabStrip1.Items[i].splic.Panel1.Controls[k]);
                                    if (jdownloadR.DownloadID == ID.ToString() && jdownloadR.IsDownloading)
                                    {
                                        jdownloadR.label1.Text = "";
                                        jdownloadR.label1.Hide();
                                        jdownloadR.ImageBase64str = PublicClass.IamgeToBase64(System.Drawing.Icon.ExtractAssociatedIcon(jdownloadR.FullFilePaths).ToBitmap());
                                        jdownloadR.FileAlreadyDele = false;
                                        jdownloadR.IsDownloading = false;
                                        jdownloadR.pictureBox1.Image = System.Drawing.Icon.ExtractAssociatedIcon(jdownloadR.FullFilePaths).ToBitmap();
                                        jdownloadR.Invalidate();
                                        mDownloadRecode.Update(jdownloadR);
                                        downloadTabOpened = true;
                                    }
                                }
                                break;
                            }
                        }



                        if (MainForm.Instance.DownloadPanel3.Visible)
                        {
                            for (int h = 0; h < MainForm.Instance.DownloadPanel3.Controls.Count - 2; h++)
                            {
                                if (((Jdownloading)(MainForm.Instance.DownloadPanel3.Controls[h + 2])).DownloadIndex == ID.ToString())
                                {
                                    ((Jdownloading)(MainForm.Instance.DownloadPanel3.Controls[h + 2])).buttonXP1.ImageDefault = Resources.up1;
                                    ((Jdownloading)(MainForm.Instance.DownloadPanel3.Controls[h + 2])).label2.Visible = false;
                                    ((Jdownloading)(MainForm.Instance.DownloadPanel3.Controls[h + 2])).pictureBox1.Image =
                                    Icon.ExtractAssociatedIcon(((Jdownloading)(MainForm.Instance.DownloadPanel3.Controls[h + 2])).FilePaths).ToBitmap(); ;
                                    ((Jdownloading)(MainForm.Instance.DownloadPanel3.Controls[h + 2])).IsDownloaded = true;
                                    ((Jdownloading)(MainForm.Instance.DownloadPanel3.Controls[h + 2])).Opendownfile.Enabled = true;
                                    MainForm.Instance.DownloadPanel3.Invalidate();
                                    if (!downloadTabOpened) //当没有打开下载页面的时候也要更新一部分内容
                                    {
                                        string jsonStr = File.ReadAllText(PublicClass.currDirectiory + @"\UserData\mDownloadRecodes");
                                        Jdownload downitem = new Jdownload();// = new FavirteButton();
                                        List<Jdownload> downitemBTs;
                                        try
                                        {
                                            var jobInfoList = JsonConvert.DeserializeObject<List<Jdownload>>(jsonStr);
                                            if (jobInfoList.Count > 0)
                                            {
                                                downitemBTs = jobInfoList;
                                                foreach (Jdownload jobInfo in downitemBTs)
                                                {
                                                    if (jobInfo.DownloadID == ID.ToString() && jobInfo.IsDownloading)
                                                    {
                                                        jobInfo.ImageBase64str = PublicClass.IamgeToBase64(((Jdownloading)(MainForm.Instance.DownloadPanel3.Controls[h + 2])).pictureBox1.Image);
                                                        downitem = jobInfo;
                                                        break;
                                                    }
                                                }
                                                mDownloadRecode.Update(downitem);
                                            }
                                        }
                                        catch (Exception e)
                                        {
                                            MessageBox.Show(e.Message);
                                        }
                                    }
                                    break;
                                }
                            }
                        }
                    }));


                    if (downloadItem.IsComplete)
                        CefConstHelper.IsDownloading = false;
                }
            }
            catch(Exception e)
            { }
        }
    }
}
