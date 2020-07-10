using System;
using System.IO;
using Xilium.CefGlue;

namespace CefiBrowser
{
    //数据处理器，仅将 数据保存到内存。
    public class MemoryStreamResponseFilter : CefResponseFilter
    {
        private MemoryStream memoryStream;

        protected override bool InitFilter()
        {
            //NOTE: We could initialize this earlier, just one possible use of InitFilter
            memoryStream = new MemoryStream();

            return true;
        }

        protected override CefResponseFilterStatus Filter(UnmanagedMemoryStream dataIn, long dataInSize, out long dataInRead, UnmanagedMemoryStream dataOut, long dataOutSize, out long dataOutWritten)
        {
            if (dataIn == null)
            {
                dataInRead = 0;
                dataOutWritten = 0;

                return CefResponseFilterStatus.Done;
            }

            dataInRead = dataIn.Length;
            dataOutWritten = Math.Min(dataInRead, dataOut.Length);

            //Important we copy dataIn to dataOut
            dataIn.CopyTo(dataOut);

            //Copy data to stream
            dataIn.Position = 0;
            dataIn.CopyTo(memoryStream);

            return CefResponseFilterStatus.Done;
        }

        protected override void Dispose(bool disposing)
        {
            if (memoryStream != null)
            {
                memoryStream.Dispose();
                memoryStream = null;
            }
        }

        public byte[] Data
        {
            get {
                return memoryStream.ToArray(); 
            }
        }


    }
}