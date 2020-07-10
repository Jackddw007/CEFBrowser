using Xilium.CefGlue;

namespace CefiBrowser
{
    internal sealed class CefWebcefCompletionCallback : CefCompletionCallback
    {
        public CefWebcefCompletionCallback()
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

        protected override void OnComplete()
        {
            
        }
    }
}
