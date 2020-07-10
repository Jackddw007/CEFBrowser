using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xilium.CefGlue;

namespace CefiBrowser.WindowsForms
{
    class CefWebCefCookieAccessFilter : CefCookieAccessFilter
    {
        public CefWebCefCookieAccessFilter()
        {
        }

        protected override bool CanSaveCookie(CefBrowser browser, CefFrame frame, CefRequest request, CefResponse response, CefCookie cookie)
        {
            //   throw new NotImplementedException();
            return true;
        }

        protected override bool CanSendCookie(CefBrowser browser, CefFrame frame, CefRequest request, CefCookie cookie)
        {
            return true;
            //throw new NotImplementedException();
        }
    }
}
