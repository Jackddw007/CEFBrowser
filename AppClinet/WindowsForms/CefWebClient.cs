namespace CefiBrowser
{
    using System;
    using System.Collections.Generic;
    using Xilium.CefGlue;

    public class CefWebClient : CefClient
    {
        private readonly CefWebBrowser _core;
        private readonly CefWebLifeSpanHandler _lifeSpanHandler;
        private readonly CefWebDisplayHandler _displayHandler;
        private readonly CefWebLoadHandler _loadHandler;
        private readonly CefWebRequestHandler _requestHandler;
        private readonly CefWebKeyboardHandler _keyboardHandler;
        private readonly CefWebDownloadHandler _downloadHandler;
        private readonly CefWebContextMenuHandler _contextMenuHandler;
        private readonly CefWebResourceHandler _resourceHandler;
        //private readonly DownloadHandler _downloadHandler;

        public CefWebClient(CefWebBrowser core)
        {
            _core = core;
            _lifeSpanHandler = new CefWebLifeSpanHandler(_core);
            _displayHandler = new CefWebDisplayHandler(_core);
            _loadHandler = new CefWebLoadHandler(_core);
            _requestHandler = new CefWebRequestHandler(_core);
            _keyboardHandler = new CefWebKeyboardHandler(_core);
            _downloadHandler = new CefWebDownloadHandler(_core);
            _contextMenuHandler = new CefWebContextMenuHandler(_core);
            _resourceHandler = new CefWebResourceHandler();
        }



        protected CefWebBrowser Core { get { return _core; } }

        protected override CefLifeSpanHandler GetLifeSpanHandler()
        {
            return _lifeSpanHandler;
        }

        protected override CefDisplayHandler GetDisplayHandler()
        {
            return _displayHandler;
        }

        
        protected override CefLoadHandler GetLoadHandler()
        {
            return _loadHandler;
        }

        protected override CefRequestHandler GetRequestHandler()
        {
            CefWebReadHandler cefWebReadHandler = new CefWebReadHandler();
         
            return _requestHandler;
        }

        
        protected override CefKeyboardHandler GetKeyboardHandler()
        {
            return _keyboardHandler;
        }

        protected override CefDownloadHandler GetDownloadHandler()
        {
            return _downloadHandler;
        }

        protected override CefContextMenuHandler GetContextMenuHandler()
        {
            return _contextMenuHandler;
        }


    }
}
