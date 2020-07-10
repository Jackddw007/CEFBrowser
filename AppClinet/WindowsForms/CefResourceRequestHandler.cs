using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xilium.CefGlue;

namespace CefiBrowser.WindowsForms
{
    internal sealed class CefWebResourceRequestHandler : CefResourceRequestHandler
    {
        private readonly CefWebBrowser _core;
        public CefWebResourceRequestHandler(CefWebBrowser core)
        {
            _core = core;
        }


        protected override CefCookieAccessFilter GetCookieAccessFilter(CefBrowser browser, CefFrame frame, CefRequest request)
        {

            //return new CefWebCefCookieAccessFilter();
            return null;
        }

        protected override CefResourceHandler GetResourceHandler(CefBrowser browser, CefFrame frame, CefRequest request)
        {
            request.SetReferrer(request.ReferrerURL, CefReferrerPolicy.Origin);
            //return new CefWebResourceHandler();
            return null;
        }

        protected override CefResponseFilter GetResourceResponseFilter(CefBrowser browser, CefFrame frame, CefRequest request, CefResponse response)
        {
            return base.GetResourceResponseFilter(browser, frame, request, response);
        }

        protected override CefReturnValue OnBeforeResourceLoad(CefBrowser browser, CefFrame frame, CefRequest request, CefRequestCallback callback)
        {
            request.SetReferrer(request.ReferrerURL, CefReferrerPolicy.Origin);
            return CefReturnValue.Continue;

        }

        protected override void OnProtocolExecution(CefBrowser browser, CefFrame frame, CefRequest request, ref bool allowOSExecution)
        {
            base.OnProtocolExecution(browser, frame, request, ref allowOSExecution);
        }

        protected override void OnResourceLoadComplete(CefBrowser browser, CefFrame frame, CefRequest request, CefResponse response, CefUrlRequestStatus status, long receivedContentLength)
        {
          
            base.OnResourceLoadComplete(browser, frame, request, response, status, receivedContentLength);
        }

        protected override void OnResourceRedirect(CefBrowser browser, CefFrame frame, CefRequest request, CefResponse response, ref string newUrl)
        {
            base.OnResourceRedirect(browser, frame, request, response, ref newUrl);
        }

        protected override bool OnResourceResponse(CefBrowser browser, CefFrame frame, CefRequest request, CefResponse response)
        {
            //if (response.MimeType.Contains("htm"))
            //    response.MimeType = "text/javascript";
            return base.OnResourceResponse(browser, frame, request, response);
        }
    }
}
