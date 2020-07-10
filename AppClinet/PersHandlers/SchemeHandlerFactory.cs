using Xilium.CefGlue;

namespace CefiBrowser
{
    internal sealed class SchemeHandlerFactory : CefSchemeHandlerFactory
    {
        public SchemeHandlerFactory()
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

        protected override CefResourceHandler Create(CefBrowser browser, CefFrame frame, string schemeName, CefRequest request)
        {
            schemeName = CefConstHelper.Branding;
            return new CefWebResourceHandler();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}