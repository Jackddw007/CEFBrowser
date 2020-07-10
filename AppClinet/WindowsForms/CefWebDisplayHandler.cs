namespace CefiBrowser
{
    using CefiBrowser.Properties;
    using System;
    using Xilium.CefGlue;

    internal sealed class
        CefWebDisplayHandler : CefDisplayHandler
    {
        private readonly CefWebBrowser _core;

        public CefWebDisplayHandler(CefWebBrowser core)
        {
            _core = core;
        }

        protected override void OnTitleChange(CefBrowser browser, string title)
        {
            _core.InvokeIfRequired(() => _core.OnTitleChanged(new TitleChangedEventArgs(title, _core)));

        }

        protected override void OnAddressChange(CefBrowser browser, CefFrame frame, string url)
        {

            //  CefCookieManager.FlushStore(CefCompletionCallback as null);

            if (frame.IsMain)
            {
                _core.InvokeIfRequired(() => _core.OnAddressChanged(new AddressChangedEventArgs(frame, url, _core)));
            }
        }

        protected override void OnStatusMessage(CefBrowser browser, string value)
        {
            _core.InvokeIfRequired(() => _core.OnStatusMessage(new StatusMessageEventArgs(value)));

        }


        protected override bool OnTooltip(CefBrowser browser, string text)
        {
            var e = new TooltipEventArgs(text);
            _core.InvokeIfRequired(() => _core.OnTooltip(e));
            return e.Handled;
        }

        protected override void OnFaviconUrlChange(CefBrowser browser, string[] iconUrls)
        {
            string iconstr = "";
            int t = iconUrls.Length;

            if (t < 2)
                iconstr = iconUrls[t - 1].ToString();
            else
            {
                for (int y = 0; y < t; y++)
                {
                    if (iconUrls[y].ToString().Contains(".ico") == true || iconUrls[y].ToString().Contains(".png") == true || iconUrls[y].ToString().Contains(".jpg") == true || iconUrls[y].ToString().Contains(".gif"))
                    {
                        iconstr = iconUrls[y].ToString();
                        if (iconUrls[y].ToString().Contains("file:///"))
                            return;
                        break;
                    }
                }
            }

            for (int i = 0; i < MainForm.Instance.faTabStrip1.Items.Count; i++)
            {
                if (MainForm.Instance.faTabStrip1.Items[i].splic.Panel1.Controls.Count > 0)
                {
                    if (MainForm.Instance.faTabStrip1.Items[i].Title != CefConstHelper.CefDownloadTitle)
                    {
                        if (_core == (CefWebBrowser)MainForm.Instance.faTabStrip1.Items[i].splic.Panel1.Controls[0])
                        {
                            if (iconstr != "" && iconstr != null)
                            {

                                _core.Browser.GetHost().DownloadImage(iconstr, true, 0, true, new CefWebDownloadImageCallbackHandler("", i, null, ""));
                            }
                            else
                            {
                                MainForm.Instance.faTabStrip1.Items[i].ItemIcon = Resources.blank;
                                break;
                            }
                        }
                    }
                }
            }

            iconstr = null;
            GC.Collect();
            base.OnFaviconUrlChange(browser, iconUrls);
        }

        protected override void OnFullscreenModeChange(CefBrowser browser, bool fullscreen)
        {
            //bool fullForm = false;
     //       if (MainForm.Instance.WindowState == System.Windows.Forms.FormWindowState.Maximized
     //&& MainForm.Instance.ToolsPanel.Visible)
     //           fullForm = true;
            if (fullscreen)
            {

                PublicClass.ScreenFuction("fullscreen");//, fullForm);
            }
            else
            {
                PublicClass.ScreenFuction("");//, fullForm);
            }
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        protected override bool OnConsoleMessage(CefBrowser browser, CefLogSeverity level, string message, string source, int line)
        {
            var e = new ConsoleMessageEventArgs(message, source, line);
            _core.InvokeIfRequired(() => _core.OnConsoleMessage(e));

            return e.Handled;
            //  return base.OnConsoleMessage(browser, level, message, source, line);
        }

        protected override bool OnAutoResize(CefBrowser browser, ref CefSize newSize)
        {
            return base.OnAutoResize(browser, ref newSize);
        }

        protected override void OnLoadingProgressChange(CefBrowser browser, double progress)
        {
            base.OnLoadingProgressChange(browser, progress);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }

}
