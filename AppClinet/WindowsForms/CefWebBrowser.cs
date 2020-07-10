namespace CefiBrowser
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;
    using Xilium.CefGlue;

    [ToolboxBitmap(typeof(CefWebBrowser))]
    public class CefWebBrowser : Control
    {
        public  bool _handleCreated;
      //  public static string Branding = "CefiBrowser";
        public bool popupurl;
        public CefBrowser _browser;
        public IntPtr _browserWindowHandle;
        public DateTime StartTimme { get; set; }
        public CefWebBrowser()
        {
               SetStyle(
                ControlStyles.ContainerControl
                | ControlStyles.ResizeRedraw
                | ControlStyles.FixedWidth
                | ControlStyles.FixedHeight
                | ControlStyles.StandardClick
                | ControlStyles.UserMouse
                | ControlStyles.SupportsTransparentBackColor
                | ControlStyles.StandardDoubleClick
                | ControlStyles.OptimizedDoubleBuffer
                | ControlStyles.CacheText
                | ControlStyles.EnableNotifyMessage
                | ControlStyles.DoubleBuffer
                | ControlStyles.OptimizedDoubleBuffer
                | ControlStyles.UseTextForAccessibility
                | ControlStyles.Opaque,
                false);

            SetStyle(
                ControlStyles.UserPaint
                | ControlStyles.AllPaintingInWmPaint
                | ControlStyles.Selectable,
                true);

            StartUrl = "about:blank";

        }


        [DefaultValue("")]//about:blank")]
        public string StartUrl { get; set; }
        public bool DevToolsOpen { get; set; }

        public static int tabIndexNumber;

        /// <summary>
        /// 是否启用多用户隔离模式
        /// </summary>
        public bool multiUsersMode { get; set; } 

        [Browsable(false)]
        public CefBrowserSettings BrowserSettings { get; set; }

		internal void InvokeIfRequired(Action a)
		{
            try
            {
                if (InvokeRequired)
                    Invoke(a);
                else
                    a();
            }
            catch
            { };
		}


        protected virtual CefWebClient CreateWebClient()
        {
            return new CefWebClient(this);
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            if (DesignMode)
            {
                if (!_handleCreated) Paint += PaintInDesignMode;
            }
            else
            {
                var windowInfo = CefWindowInfo.Create();
                windowInfo.SetAsChild(Handle, new CefRectangle { X = 0, Y = 0, Width = Width, Height = Height });

                var client = CreateWebClient();
                var settings = BrowserSettings;
                if (settings == null)
                {
                    settings = new CefBrowserSettings();
                    settings.AcceptLanguageList = "zh-CN"; //设置默认为;
                    settings.FileAccessFromFileUrls = CefState.Enabled;// CefCommandLine.Create("allow-access-from.file");
                    settings.UniversalAccessFromFileUrls = CefState.Enabled;
                    settings.ApplicationCache = CefState.Enabled;
                    settings.JavaScript = CefState.Enabled;
                    settings.JavaScriptAccessClipboard = CefState.Enabled;
                    settings.JavaScriptDomPaste = CefState.Enabled;
                    settings.LocalStorage = CefState.Enabled;
                    settings.WebGL = CefState.Disabled;
                    settings.Databases = CefState.Enabled;
                    settings.ImageLoading = CefState.Enabled;
                    settings.WebSecurity = CefState.Enabled;
                    settings.JavaScriptCloseWindows = CefState.Enabled;
                    settings.DefaultEncoding = "UTF8";
                    settings.ImageLoading = CefState.Enabled;
                    settings.ImageShrinkStandaloneToFit = CefState.Enabled;

                }
                

                //判断隔离独立页面的Cookie
                if (multiUsersMode)
                {
                    tabIndexNumber++;
                    CefRequestContextSettings contextSettings = new CefRequestContextSettings();
                    contextSettings.PersistSessionCookies = false;
                    contextSettings.PersistUserPreferences = true;
                    contextSettings.AcceptLanguageList = "zh-CN";
                    contextSettings.IgnoreCertificateErrors = true;
                    CefWebRequestContextHandler requestContextHandler = new CefWebRequestContextHandler();
                    CefRequestContext requestContext = CefRequestContext.CreateContext(contextSettings, requestContextHandler);
                    CefBrowserHost.CreateBrowser(windowInfo, client, settings, StartUrl,null, requestContext); //隔离Cookie
                    contextSettings = null;
                    requestContext = null;
                   
                    GC.Collect();
                }
                else
                {
                    //CefRequestContextSettings contextSettings = new CefRequestContextSettings();
                    //contextSettings.PersistSessionCookies = true;
                    //contextSettings.PersistUserPreferences = true;
                    //contextSettings.CachePath = GetAppDir("Cache");
                    //contextSettings.AcceptLanguageList = "zh-CN";
                    //// contextSettings.EnableNetSecurityExpiration = false;
                    //contextSettings.IgnoreCertificateErrors = true;
                    //CefWebRequestContextHandler requestContextHandler = new CefWebRequestContextHandler();

                    //CefRequestContext requestContext = CefRequestContext.CreateContext(contextSettings, requestContextHandler);
                    //CefBrowserHost.CreateBrowser(windowInfo, client, settings, StartUrl, null, requestContext);
                    //contextSettings = null;
                    //requestContext = null;

                    //GC.Collect();
                    CefBrowserHost.CreateBrowser(windowInfo, client, settings, StartUrl);
                }
                windowInfo = null;
                client = null;
                settings = null;
                GC.Collect();
            }

            _handleCreated = true;
            base.OnHandleCreated(e);
        }

        protected override void Dispose(bool disposing)
        {
            if (_browser != null && disposing) // TODO: ugly hack to avoid crashes when CefWebBrowser are Finalized and underlying objects already finalized
            {
                var host = _browser.GetHost();
                if (host != null)
                {
                    host.CloseBrowser();
                    host.Dispose();
                }
                _browser.Dispose();
                _browser = null;
                _browserWindowHandle = IntPtr.Zero;
            }
            GC.Collect();
            base.Dispose(disposing);
        }

    	public event EventHandler BrowserCreated;

        public int x,y,_width,_height;
        internal protected virtual void OnBrowserAfterCreated(CefBrowser browser)
        {
            _browser = browser;

            _browserWindowHandle = _browser.GetHost().GetWindowHandle();

            if (_width > 0 && _height > 0)
                ResizeWindow(_browserWindowHandle, x, y, _width, _height);
            else
                ResizeWindow(_browserWindowHandle, x, y, Width, Height);


            if (BrowserCreated != null)
                BrowserCreated(this, EventArgs.Empty);

        }

        internal protected virtual void OnTitleChanged(TitleChangedEventArgs e)
        {
            Title = e.Title;

            var handler = TitleChanged;
            if (handler != null) handler(this, e);
        }

        public string Title { get; private set; }

        public event EventHandler<TitleChangedEventArgs> TitleChanged;

        internal protected virtual void OnAddressChanged(AddressChangedEventArgs e)
        {
        	Address = e.Address;

            var handler = AddressChanged;
            if (handler != null) handler(this, e);
        }

        public string Address { get; private set; }

        public event EventHandler<AddressChangedEventArgs> AddressChanged;

		internal protected virtual void OnStatusMessage(StatusMessageEventArgs e)
        {
			var handler = StatusMessage;
            if (handler != null) handler(this, e);
        }

        public event EventHandler<StatusMessageEventArgs> StatusMessage;

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (_browserWindowHandle != IntPtr.Zero)
            {
                //Ignore size changes when form are minimized.
                var form = TopLevelControl as Form;
                if (form != null && form.WindowState == FormWindowState.Minimized)
                {
                    return;
                }

                if (_width > 0 && _height > 0)
                    ResizeWindow(_browserWindowHandle, x, y, _width, _height);
                else
                    ResizeWindow(_browserWindowHandle, x, y, Width, Height);

            }
        }

        private void PaintInDesignMode(object sender, PaintEventArgs e)
        {
            var width = this.Width;
            var height = this.Height;
            if (width > 1 && height > 1)
            {
                var brush = new SolidBrush(this.ForeColor);
                var pen = new Pen(this.ForeColor);
                pen.DashStyle = DashStyle.Dash;

                e.Graphics.DrawRectangle(pen, 0, 0, width - 1, height - 1);

                var fontHeight = (int)(this.Font.GetHeight(e.Graphics) * 1.25);

                var x = 3;
                var y = 3;

                e.Graphics.DrawString(CefConstHelper.Branding, Font, brush, x, y + (0 * fontHeight));
                e.Graphics.DrawString(string.Format("StartUrl: {0}", StartUrl), Font, brush, x, y + (1 * fontHeight));

                brush.Dispose();
                pen.Dispose();
            }
        }

        public void InvalidateSize()
		{
            if (_width > 0 && _height > 0)
                ResizeWindow(_browserWindowHandle, x, y, _width, _height);
            else
                ResizeWindow(_browserWindowHandle, x, y, Width, Height);

        }

        public static void ResizeWindow(IntPtr handle,int x, int y, int width, int height)
        {

            if (handle != IntPtr.Zero)
            {
                NativeMethods.SetWindowPos(handle, IntPtr.Zero,
                  x, y, width, height,
                  SetWindowPosFlags.NoMove | SetWindowPosFlags.NoZOrder
                  );
            }

        }

        public CefBrowser Browser { get { return _browser; } }

    	public event EventHandler<ConsoleMessageEventArgs> ConsoleMessage;

		internal protected virtual void OnConsoleMessage(ConsoleMessageEventArgs e)
    	{
			if (ConsoleMessage != null)
				ConsoleMessage(this, e);
			else
				e.Handled = false;
    	}

    	public event EventHandler<LoadingStateChangeEventArgs> LoadingStateChange;

		internal protected virtual void OnLoadingStateChange(LoadingStateChangeEventArgs e)
		{
			if (LoadingStateChange != null)
				LoadingStateChange(this, e);
		}

    	public event EventHandler<TooltipEventArgs> Tooltip;

		internal protected virtual void OnTooltip(TooltipEventArgs e)
		{
			if (Tooltip != null)
				Tooltip(this, e);
			else
				e.Handled = false;
		}

    	public event EventHandler BeforeClose;

		internal protected virtual void OnBeforeClose()
		{
			_browserWindowHandle = IntPtr.Zero;
			if (BeforeClose != null)
				BeforeClose(this, EventArgs.Empty);
		}

    	public event EventHandler<BeforePopupEventArgs> BeforePopup;

		internal protected virtual void OnBeforePopup(BeforePopupEventArgs e)
        {
            if (e.TargetUrl != "about:blank")
            {
                popupurl = false;
            }

            if (BeforePopup != null)
            {
               // e.Handled = true;
                BeforePopup(this, e);
            }
            else
                e.Handled = false;
        }

    	public event EventHandler<LoadEndEventArgs> LoadEnd;

		internal protected virtual void OnLoadEnd(LoadEndEventArgs e)
		{
			if (LoadEnd != null)
				LoadEnd(this, e);
		}

    	public event EventHandler<LoadErrorEventArgs> LoadError;

		internal protected virtual void OnLoadError(LoadErrorEventArgs e)
		{
			if (LoadError != null)
				LoadError(this, e);
		}

    	public event EventHandler<LoadStartEventArgs> LoadStarted;

		internal protected virtual void OnLoadStart(LoadStartEventArgs e)
		{
			if (LoadStarted != null)
				LoadStarted(this, e);
		}

    	public event EventHandler<PluginCrashedEventArgs> PluginCrashed;

		internal protected virtual void OnPluginCrashed(PluginCrashedEventArgs e)
    	{
			if (PluginCrashed != null)
				PluginCrashed(this, e);
    	}

    	public event EventHandler<RenderProcessTerminatedEventArgs> RenderProcessTerminated;

		internal protected virtual void OnRenderProcessTerminated(RenderProcessTerminatedEventArgs e)
		{
			if (RenderProcessTerminated != null)
				RenderProcessTerminated(this, e);
		}

        //public event EventHandler<BeforeDownloadEventArgs> BeforeDownload;

        //internal protected virtual void OnBeforeDownload(BeforeDownloadEventArgs e)
        //{
        //    if (BeforeDownload != null)
        //        BeforeDownload(this, e);
        //}

        //public event EventHandler<DownloadUpdatedEventArgs> DownloadUpdate;

        //internal protected virtual void OnDownloadUpdate(DownloadUpdatedEventArgs e)
        //{
        //    if (DownloadUpdate != null)
        //        DownloadUpdate(this, e);
        //}

    }
}
