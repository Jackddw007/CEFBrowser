using System;
using Xilium.CefGlue;

namespace CefiBrowser
{
    internal sealed class CefWebRequestContextHandler : CefRequestContextHandler
    {
      ////  public readonly CefWebBrowser _core;
        public CefWebRequestContextHandler()
        {
           
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        //protected override CefCookieManager GetCookieManager()
        //{
        //    CefWebcefCompletionCallback cefWebcef = new CefWebcefCompletionCallback();
        //    return CefCookieManager.GetGlobal(cefWebcef);
             
        //    //return CefCookieManager.Create(@"d:\CefiBrowser\Cache" + DateTime.Now.ToString("yyyyMMdd") + _core.TabIndex, false, null);
        //}

        protected override CefResourceRequestHandler GetResourceRequestHandler(CefBrowser browser, CefFrame frame, CefRequest request, bool isNavigation, bool isDownload, string requestInitiator, ref bool disableDefaultHandling)
        {
            //throw new NotImplementedException();
            return null;
        }

        protected override bool OnBeforePluginLoad(string mimeType, string pluginUrl, bool isMainFrame, string topOriginUrl, CefWebPluginInfo pluginInfo, ref CefPluginPolicy pluginPolicy)
        {
            return base.OnBeforePluginLoad(mimeType, pluginUrl, isMainFrame, topOriginUrl, pluginInfo, ref pluginPolicy);
        }

        protected override void OnRequestContextInitialized(CefRequestContext requestContext)
        {
            base.OnRequestContextInitialized(requestContext);
        }
    }
}
