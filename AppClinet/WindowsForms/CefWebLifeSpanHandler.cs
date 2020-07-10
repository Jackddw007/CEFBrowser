namespace CefiBrowser
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;
    using System.Web.UI.Design;
    using Xilium.CefGlue;

    internal sealed class CefWebLifeSpanHandler : CefLifeSpanHandler
    {
        private readonly CefWebBrowser _core;
        private CefWindowInfo _WindowsInfo;
        public CefWebLifeSpanHandler(CefWebBrowser core)
        {
            _core = core;
        }

        protected override void OnAfterCreated(CefBrowser browser)
        {
            //CefMouseEvent cefMouseEvent = new CefMouseEvent();
            //  cefMouseEvent.Modifiers= CefEventFlags.None ;
            ////mouseEvent.X = mouseX;
            ////mouseEvent.Y = mouseY;
            //browser.GetHost().SendMouseMoveEvent(cefMouseEvent, false);
          //  base.OnAfterCreated(browser);
        	_core.InvokeIfRequired(() => _core.OnBrowserAfterCreated(browser));
        }

        protected override bool DoClose(CefBrowser browser)
        {
            // TODO: ... dispose core
            return false;
        }

		protected override void OnBeforeClose(CefBrowser browser)
		{
            try
            {
                if (_core.InvokeRequired)
                    _core.BeginInvoke((Action)_core.OnBeforeClose);
                else
                    _core.OnBeforeClose();
            }
            catch
            { }
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

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        protected override bool OnBeforePopup(CefBrowser browser, CefFrame frame, string targetUrl, string targetFrameName, CefWindowOpenDisposition targetDisposition, bool userGesture, CefPopupFeatures popupFeatures, CefWindowInfo windowInfo, ref CefClient client, CefBrowserSettings settings, ref CefDictionaryValue extraInfo, ref bool noJavascriptAccess)
        {
            //  return base.OnBeforePopup(browser, frame, targetUrl, targetFrameName, targetDisposition, userGesture, popupFeatures, windowInfo, ref client, settings, ref extraInfo, ref noJavascriptAccess);

            var e = new BeforePopupEventArgs(frame, targetUrl, targetFrameName, popupFeatures, windowInfo, client, settings,noJavascriptAccess);

            client = e.Client;
            noJavascriptAccess =  e.NoJavascriptAccess;
            _core.x = windowInfo.X;
            _core.y = windowInfo.Y;
            _core._width = windowInfo.Width;
            _core._height = windowInfo.Height;
            _core.InvokeIfRequired(() => _core.OnBeforePopup(e));
            return e.Handled;
        }
    }
}
