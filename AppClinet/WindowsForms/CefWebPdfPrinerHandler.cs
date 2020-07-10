using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Xilium.CefGlue;

namespace CefiBrowser
{
    internal sealed class CefWebPdfPrinerHandler : CefPdfPrintCallback
    {
        public CefWebPdfPrinerHandler()
        {
        }
        protected override void OnPdfPrintFinished(string path, bool ok)
        {
            if(ok)
             MessageBox.Show("PDF另存为完成！");
        }
    }
}
