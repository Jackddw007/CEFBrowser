using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xilium.CefGlue;

namespace CefiBrowser
{
    internal sealed class SourceVisitor : CefStringVisitor
    {
        private readonly Action<string> _callback;

        public SourceVisitor(Action<string> callback)
        {
            _callback = callback;
        }

        protected override void Visit(string value)
        {
            _callback(value);
        }
    }
}
