using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xilium.CefGlue;

namespace CefiBrowser
{
    public class BeforeDownloadEventArgs : EventArgs
    {
        public BeforeDownloadEventArgs(CefDownloadItem downloadItem, string suggestedName, CefBeforeDownloadCallback callback)
        {
            this.downloadItem = downloadItem;
            this.suggestedName = suggestedName;
            this.callback = callback;
        }

        public CefDownloadItem downloadItem;
        public string suggestedName;
        public CefBeforeDownloadCallback callback;

    }

    public class DownloadUpdatedEventArgs : EventArgs
    {
        public DownloadUpdatedEventArgs(CefBrowser browser, CefDownloadItem downloadItem, CefDownloadItemCallback callback)
        {

            this.DownloadItem = downloadItem;
            this._browser = browser;
            this.Callback = callback;
        }

        public CefDownloadItem DownloadItem;
        public CefBrowser _browser;
        public CefDownloadItemCallback Callback;
    }



}
