using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xilium.CefGlue;

namespace CefiBrowser
{
    internal class CefWebReadHandler : CefReadHandler
    {
        public CefWebReadHandler()
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

        protected override bool Eof()
        {
            throw new NotImplementedException();
        }

        protected override bool MayBlock()
        {
            throw new NotImplementedException();
        }

        protected override long Read(Stream stream, long length)
        {
            throw new NotImplementedException();
        }

        protected override bool Seek(long offset, SeekOrigin whence)
        {
            throw new NotImplementedException();
        }

        protected override long Tell()
        {
            throw new NotImplementedException();
        }
    }
}
