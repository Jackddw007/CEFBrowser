//using CefSharp;
//using CefSharp.Structs;
//using System;
//using System.Collections.Generic;

//namespace SharpBrowser
//{
//    internal class DisplayHandler : IDisplayHandler
//    {

//        MainForm myForm;

//        public DisplayHandler(MainForm form)
//        {
//            myForm = form;
//        }

//        public void OnAddressChanged(IWebBrowser chromiumWebBrowser, AddressChangedEventArgs addressChangedArgs)
//        {

//        }

//        public bool OnAutoResize(IWebBrowser chromiumWebBrowser, IBrowser browser, Size newSize)
//        {
//            return false;
//        }

//        public bool OnConsoleMessage(IWebBrowser chromiumWebBrowser, ConsoleMessageEventArgs consoleMessageArgs)
//        {
//            return false;
//        }

//        public void OnFaviconUrlChange(IWebBrowser chromiumWebBrowser, IBrowser browser, IList<string> urls)
//        {
//          //  var url = urls[0];
//            myForm.LoadWebIco(new Uri(urls[0]));
//        }

//        public void OnFullscreenModeChange(IWebBrowser chromiumWebBrowser, IBrowser browser, bool fullscreen)
//        {
           
//        }

//        public void OnStatusMessage(IWebBrowser chromiumWebBrowser, StatusMessageEventArgs statusMessageArgs)
//        {
            
//        }

//        public void OnTitleChanged(IWebBrowser chromiumWebBrowser, TitleChangedEventArgs titleChangedArgs)
//        {
            
//        }

//        public bool OnTooltipChanged(IWebBrowser chromiumWebBrowser, ref string text)
//        {
//            return false;
//        }
//    }

//}
