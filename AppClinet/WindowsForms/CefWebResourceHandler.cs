using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Controls.Primitives;
using Xilium.CefGlue;

namespace CefiBrowser
{
    internal sealed class CefWebResourceHandler : CefResourceHandler
    {
        public CefWebResourceHandler()
        {
        }

        protected override void Cancel()
        {
            //throw new NotImplementedException();
        }


        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        protected override void GetResponseHeaders(CefResponse response, out long responseLength, out string redirectUrl)
        {
            responseLength = -1;
            response.MimeType = "text/html";
            response.Status = 200;
            redirectUrl = null;
        }

        protected override bool Open(CefRequest request, out bool handleRequest, CefCallback callback)
        {
            //if(handleRequest)
              handleRequest = true;
            callback.Continue();
            return true;
            //throw new NotImplementedException();
        }

        protected override bool ProcessRequest(CefRequest request, CefCallback callback)
        {
            callback.Continue();
            return true;
        }

        protected override bool Read(IntPtr dataOut, int bytesToRead, out int bytesRead, CefResourceReadCallback callback)
        {
            if (bytesToRead > 0)
            {
                bytesRead = bytesToRead;
                callback.Continue(bytesRead);
                return true;
            }
            bytesRead = 0;
            return false;
       }

        protected override bool ReadResponse(Stream response, int bytesToRead, out int bytesRead, CefCallback callback)
        {
            if (bytesToRead > 0)
            {
                bytesRead = bytesToRead;
                callback.Continue();
                return true;
            }
            else
            {
                callback.Cancel();
            }
            bytesRead = 0;
            return false;
            //if (bytesToRead == 0)
            //{
            //    byte[] data = Encoding.UTF8.GetBytes("<html><body><h1>Hello CEF</h1></body></html>");
            //    bytesRead = data.Length;
            //    Marshal.Copy(data, 0,IntPtr.Zero, data.Length);
            //    return true;

                //}
                //else
                //{
                //    bytesRead = 0;
                //    return false;
                //}
        }

        //protected override bool ReadResponse(Stream response, int bytesToRead, out int bytesRead, CefCallback callback)
        //{
        //    return base.ReadResponse(response, bytesToRead, out bytesRead, callback);
        //}

        //protected override bool ReadResponse(Stream response, int bytesToRead, out int bytesRead, CefCallback callback)
        //{
        //    if (bytesToRead == 0)
        //    {
        //        byte[] data = Encoding.UTF8.GetBytes("<html><body><h1>Hello CEF</h1></body></html>");
        //        bytesRead = data.Length;
        //        Marshal.Copy(data, 0,,  data.Length);
        //        return true;

        //    }
        //    else
        //    {
        //        bytesRead = 0;
        //        return false;
        //    }

        //    // throw new NotImplementedException();
        //}

        protected override bool Skip(long bytesToSkip, out long bytesSkipped, CefResourceSkipCallback callback)
        {
            bytesSkipped = 0;
            return true;
           // throw new NotImplementedException();
        }
    }
}
