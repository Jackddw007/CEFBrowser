using CefiBrowser.WindowsForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using Xilium.CefGlue;

namespace CefiBrowser
{
    sealed class CefWebRequestHandler : CefRequestHandler
    {
        public CefRequestHandler _requestHeandler;
        private readonly CefWebBrowser _core;
       // public string _directory = "Temp/";
      //  private Dictionary<UInt64, MemoryStreamResponseFilter> responseDictionary = new Dictionary<UInt64, MemoryStreamResponseFilter>();
        public CefWebRequestHandler(CefWebBrowser core) :base()
        {
            _core = core;
            _requestHeandler = this;
        }

        // protected override bool CanGetCookies(CefBrowser browser, CefFrame frame, CefRequest request)
        //{
        //    return base.CanGetCookies(browser, frame, request);
        //}

        //protected override bool CanSetCookie(CefBrowser browser, CefFrame frame, CefRequest request, CefCookie cookie)
        //{
        //    return base.CanSetCookie(browser, frame, request, cookie);
        //}

        //protected override bool GetAuthCredentials(CefBrowser browser, CefFrame frame, bool isProxy, string host, int port, string realm, string scheme, CefAuthCallback callback)
        //{
        //    //isProxy = false;
        //    return base.GetAuthCredentials(browser, frame, isProxy, host, port, realm, scheme, callback);
        //}

        //protected override CefResourceHandler GetResourceHandler(CefBrowser browser, CefFrame frame, CefRequest request)
        //{

        //    //CefWebReadHandler cefWebRead = new CefWebReadHandler();


        //    return base.GetResourceHandler(browser, frame, request);
        //}
        //protected override bool OnBeforeBrowse(CefBrowser browser, CefFrame frame, CefRequest request, bool userGesture, bool isRedirect)
        //{
        //    return base.OnBeforeBrowse(browser, frame, request, userGesture, isRedirect);
        //}

        //protected override CefReturnValue OnBeforeResourceLoad(CefBrowser browser, CefFrame frame, CefRequest request, CefRequestCallback callback)
        //{
        //    Uri url;
        //    if (Uri.TryCreate(request.Url, UriKind.Absolute, out url) == false)
        //    {
        //        //If we're unable to parse the Uri then cancel the request
        //        // avoid throwing any exceptions here as we're being called by unmanaged code
        //        return CefReturnValue.Cancel;
        //    }

        //    if (_requestHeandler != null)
        //    {
        //        return base.OnBeforeResourceLoad(browser, frame, request, callback);
        //        //return base.OnBeforeResourceLoad(browserControl, browser, frame, request, callback);
        //    }

        //    return CefReturnValue.Continue;
        //    //return base.OnBeforeResourceLoad(browser, frame, request, callback);

        //}

        protected override bool OnCertificateError(CefBrowser browser, CefErrorCode certError, string requestUrl, CefSslInfo sslInfo, CefRequestCallback callback)
        {
            
            callback.Continue(true);
            return base.OnCertificateError(browser, certError, requestUrl, sslInfo, callback);
        }

        protected override bool OnOpenUrlFromTab(CefBrowser browser, CefFrame frame, string targetUrl, CefWindowOpenDisposition targetDisposition, bool userGesture)
        {
            return base.OnOpenUrlFromTab(browser, frame, targetUrl, targetDisposition, userGesture);
        }

        protected override void OnPluginCrashed(CefBrowser browser, string pluginPath)
        {
            _core.InvokeIfRequired(() => _core.OnPluginCrashed(new PluginCrashedEventArgs(pluginPath)));
        }

        //protected override void OnProtocolExecution(CefBrowser browser, string url, out bool allowOSExecution)
        //{
        //    base.OnProtocolExecution(browser, url, out allowOSExecution);
        //}

        protected override bool OnQuotaRequest(CefBrowser browser, string originUrl, long newSize, CefRequestCallback callback)
        {
            return base.OnQuotaRequest(browser, originUrl, newSize, callback);
        }

        protected override void OnRenderProcessTerminated(CefBrowser browser, CefTerminationStatus status)
        {
            _core.InvokeIfRequired(() => _core.OnRenderProcessTerminated(new RenderProcessTerminatedEventArgs(status)));
        }

        protected override void OnRenderViewReady(CefBrowser browser)
        {
            base.OnRenderViewReady(browser);
        }
        //protected override void OnResourceLoadComplete(CefBrowser browser, CefFrame frame, CefRequest request, CefResponse response, CefUrlRequestStatus status, long receivedContentLength)
        //{
        //    if (request.ReferrerURL != null)
        //    {
        //        #region for12306
        //        if (request.ReferrerURL.Contains("kyfw.12306.cn/otn/leftTicket/queryZ?leftTicketDTO.train_date="))
        //        {
        //                //if (request.Url.Length == 63)
        //                //{
        //                MemoryStreamResponseFilter filter;
        //            if (responseDictionary.TryGetValue(request.Identifier, out filter))
        //            {
        //                //if (!Directory.Exists(_directory))
        //                //{
        //                //    Directory.CreateDirectory(_directory);
        //                //}
        //                //System.Diagnostics.Debug.WriteLine("responseDictionary.Count:" + responseDictionary.Count);

        //                //TODO: Do something with the data here
        //                var data = filter.Data;
        //                var dataLength = filter.Data.Length;
        //                //data to image
        //                //Image imageFile = PublicClass.BytesToImage(data);

        //                ////data to base64
        //                //string ucodePic = Convert.ToBase64String(data);
        //                ////NOTE: You may need to use a different encoding depending on the request
        //                ////var dataAsUtf8String = Encoding.UTF8.GetString(data);

        //                //Bitmap uPics = new Bitmap(imageFile);
        //                ////for test below
        //                ////imageFile.Save("d:\\123.png",System.Drawing.Imaging.ImageFormat.Png);
        //                ////imageFile.Dispose();
        //                ////imageFile = null;
        //                //string uucode = PublicClass.getPicnum(uPics, false);
        //                //if (uucode.Length < 4)
        //                //{
        //                //    uucode = PublicClass.getPicnum(uPics, true);
        //                //}
        //                //string Kcode = "'data:image/jpg;base64," + ucodePic + "'";
        //                //frame.ExecuteJavaScript("document.getElementById('saveCode').value='" + uucode + "';", request.ReferrerURL, 1);
        //                //frame.ExecuteJavaScript("document.getElementById('identity').src = " + Kcode, request.ReferrerURL, 1);

        //                //自动登录
        //                //if (MainForm.Instance.CefGSetting.Checked)
        //                //    frame.ExecuteJavaScript("index_iframe.window.checkCode(1)", request.ReferrerURL, 1);// document.getElementById('$0').onclick();", request.ReferrerURL, 1);

        //                //Kcode = null;
        //                //ucodePic = null;
        //                //uucode = null;
        //                //imageFile = null;
        //                //uPics = null;
        //                MainForm.Instance.faTabStrip1.SelectedItem.Title = DateTime.Now.ToString();
        //                data = null;
        //                GC.Collect();

        //            }
        //        }
        //        #endregion

        //        #region for OCR code 验证码识别
        //        //if (request.ReferrerURL.Contains("www.sh.msa.gov.cn/zwzx/index1?vts=&nav=&a=&b=&c=&type="))
        //        //{
        //        //    if (request.Url != null)// && request.Url.Contains("views/image.jsp"))
        //        //    {
        //        //        if (request.Url.Length == 63)
        //        //        {
        //        //            //if (request.Url.Length == 63)
        //        //            //{
        //        //            //MainForm.Instance.faTabStrip1.SelectedItem.OcrCodeUrl = request.Url;
        //        //            //MainForm.Instance.faTabStrip1.SelectedItem.OcrCodeFileName = request.Url.Substring(request.Url.LastIndexOf("=") + 1, 13) + ".jpg";

        //        //            //for get transfor data by image
        //        //            //var url = new Uri(request.Url);
        //        //            //var extension = url.ToString().ToLower();
        //        //            if (request.ResourceType == CefResourceType.Image)// || extension.EndsWith(".jpg") || extension.EndsWith(".png") || extension.EndsWith(".gif") || extension.EndsWith(".jpeg"))
        //        //            {
        //        //                MemoryStreamResponseFilter filter;
        //        //                if (responseDictionary.TryGetValue(request.Identifier, out filter))
        //        //                {
        //        //                    //if (!Directory.Exists(_directory))
        //        //                    //{
        //        //                    //    Directory.CreateDirectory(_directory);
        //        //                    //}
        //        //                    //System.Diagnostics.Debug.WriteLine("responseDictionary.Count:" + responseDictionary.Count);

        //        //                    //TODO: Do something with the data here
        //        //                    var data = filter.Data;
        //        //                    //var dataLength = filter.Data.Length;
        //        //                    //data to image
        //        //                    Image imageFile = PublicClass.BytesToImage(data);

        //        //                    //data to base64
        //        //                    string ucodePic = Convert.ToBase64String(data);
        //        //                    //NOTE: You may need to use a different encoding depending on the request
        //        //                    //var dataAsUtf8String = Encoding.UTF8.GetString(data);

        //        //                    Bitmap uPics = new Bitmap(imageFile);
        //        //                    //for test below
        //        //                    //imageFile.Save("d:\\123.png",System.Drawing.Imaging.ImageFormat.Png);
        //        //                    //imageFile.Dispose();
        //        //                    //imageFile = null;
        //        //                    string uucode = PublicClass.getPicnum(uPics, false);
        //        //                    if (uucode.Length < 4)
        //        //                    {
        //        //                        uucode = PublicClass.getPicnum(uPics, true);
        //        //                    }
        //        //                    string Kcode = "'data:image/jpg;base64," + ucodePic + "'";
        //        //                    frame.ExecuteJavaScript("document.getElementById('saveCode').value='" + uucode + "';", request.ReferrerURL, 1);
        //        //                    frame.ExecuteJavaScript("document.getElementById('identity').src = " + Kcode, request.ReferrerURL, 1);

        //        //                    //自动登录
        //        //                    if (MainForm.Instance.CefGSetting.Checked)
        //        //                        frame.ExecuteJavaScript("index_iframe.window.checkCode(1)", request.ReferrerURL, 1);// document.getElementById('$0').onclick();", request.ReferrerURL, 1);

        //        //                    //Kcode = null;
        //        //                    ucodePic = null;
        //        //                    uucode = null;
        //        //                    imageFile = null;
        //        //                    uPics = null;
        //        //                    data = null;
        //        //                    GC.Collect();
        //        //                }
        //        //            }
        //        //        }
        //        //    }
        //        //}
        //        #endregion

        //        #region for EHR Ucode
        //        //if (request.ReferrerURL.Contains(":8081/RedseaPlatform"))
        //        //{
        //        //    if (request.Url != null && request.Url.Contains("checkCode.jsp"))// && request.Url.Contains("views /image.jsp"))
        //        //    {
        //        //        if (request.ResourceType == CefResourceType.Image)// || extension.EndsWith(".jpg") || extension.EndsWith(".png") || extension.EndsWith(".gif") || extension.EndsWith(".jpeg"))
        //        //        {
        //        //            MemoryStreamResponseFilter filter;
        //        //            if (responseDictionary.TryGetValue(request.Identifier, out filter))
        //        //            {
        //        //                var data = filter.Data;
        //        //                //var dataLength = filter.Data.Length;
        //        //                //data to image
        //        //                Image imageFile = PublicClass.BytesToImage(data);

        //        //                //data to base64
        //        //                string ucodePic = Convert.ToBase64String(data);
        //        //                Bitmap uPics = new Bitmap(imageFile);
        //        //                string uucode = PublicClass.getPicnum(uPics, false);
        //        //                //if (uucode.Length < 4)
        //        //                //{
        //        //                //    uucode = PublicClass.getPicnum(uPics, true);
        //        //                //}
        //        //                string Kcode = "'data:image/jpg;base64," + ucodePic + "'";
        //        //                frame.ExecuteJavaScript("document.getElementById('randCode').value='" + uucode + "';", request.ReferrerURL, 1);
        //        //                frame.ExecuteJavaScript("document.getElementById('randCodeImg').src = " + Kcode, request.ReferrerURL, 1);

        //        //                Kcode = null;
        //        //                ucodePic = null;
        //        //                uucode = null;
        //        //                imageFile = null;
        //        //                uPics = null;
        //        //                data = null;
        //        //                GC.Collect();
        //        //            }
        //        //        }
        //        //    }
        //        //}

        //        #endregion
        //    }
        //    base.OnResourceLoadComplete(browser, frame, request, response, status, receivedContentLength);
        //}

        //protected override CefResponseFilter GetResourceResponseFilter(CefBrowser browser, CefFrame frame, CefRequest request, CefResponse response)
        //{
        //    #region for 12306 
        //    if (request.Url != null)
        //    {
        //        if (request.Url.Contains("kyfw.12306.cn/otn/leftTicket/queryZ?leftTicketDTO.train_date="))
        //        {
        //            if (request.Url != null)// && request.Url.Contains("views/image.jsp"))
        //            {
        //                //Only called for our customScheme
        //                MemoryStreamResponseFilter dataFilter = new MemoryStreamResponseFilter();//新建成数据 处理器
        //                responseDictionary.Add(request.Identifier, dataFilter);
        //                return dataFilter;
        //            }
        //        }
        //    }
        //    #endregion

        //    #region for XiaoQing 
        //    //if (request.ReferrerURL != null)
        //    //{
        //    //    if (request.ReferrerURL.Contains("www.sh.msa.gov.cn/zwzx/index1?vts=&nav=&a=&b=&c=&type="))
        //    //    {
        //    //        if (request.Url != null)// && request.Url.Contains("views/image.jsp"))
        //    //        {
        //    //            if (request.Url.Length == 63)
        //    //            {
        //    //                //var url = new Uri(request.Url);
        //    //                //var extension = url.ToString().ToLower();
        //    //                if (request.ResourceType == CefResourceType.Image) // || extension.EndsWith(".jpg") || extension.EndsWith(".png") || extension.EndsWith(".gif") || extension.EndsWith(".jpeg"))
        //    //                {
        //    //                    //Only called for our customScheme
        //    //                    MemoryStreamResponseFilter dataFilter = new MemoryStreamResponseFilter();//新建成数据 处理器
        //    //                    responseDictionary.Add(request.Identifier, dataFilter);
        //    //                    return dataFilter;
        //    //                }
        //    //            }
        //    //        }
        //    //    }
        //    //}
        //    #endregion

        //    #region for EHR system
        //    //if (request.ReferrerURL != null)
        //    //{
        //    //    if (request.ReferrerURL.Contains(":8081/RedseaPlatform"))
        //    //    {
        //    //        if (request.Url != null && request.Url.Contains("checkCode.jsp"))
        //    //        {
        //    //            //var url = new Uri(request.Url);
        //    //            //var extension = url.ToString().ToLower();
        //    //            if (request.ResourceType == CefResourceType.Image) // || extension.EndsWith(".jpg") || extension.EndsWith(".png") || extension.EndsWith(".gif") || extension.EndsWith(".jpeg"))
        //    //            {
        //    //                //Only called for our customScheme
        //    //                MemoryStreamResponseFilter dataFilter = new MemoryStreamResponseFilter();//新建成数据 处理器
        //    //                responseDictionary.Add(request.Identifier, dataFilter);
        //    //                return dataFilter;
        //    //            }
        //    //        }
        //    //    }
        //    //}
        //    #endregion
        //    return base.GetResourceResponseFilter(browser, frame, request, response);
        //}

        //protected override void OnResourceRedirect(CefBrowser browser, CefFrame frame, CefRequest request, CefResponse response, ref string newUrl)
        //{
        //    base.OnResourceRedirect(browser, frame, request, response, ref newUrl);
        //}

        //protected override bool OnResourceResponse(CefBrowser browser, CefFrame frame, CefRequest request, CefResponse response)
        //{
        //    return base.OnResourceResponse(browser, frame, request, response);
        //}

        protected override bool OnSelectClientCertificate(CefBrowser browser, bool isProxy, string host, int port, CefX509Certificate[] certificates, CefSelectClientCertificateCallback callback)
        {
            //isProxy = false;
            return base.OnSelectClientCertificate(browser, isProxy, host, port, certificates, callback);
        }

        protected override CefResourceRequestHandler GetResourceRequestHandler(CefBrowser browser, CefFrame frame, CefRequest request, bool isNavigation, bool isDownload, string requestInitiator, ref bool disableDefaultHandling)
        {
            disableDefaultHandling = false;
            return null;
            //如果需要执行自定义拦截就需要执行下面的Handler
            //return new CefWebResourceRequestHandler(_core);
        }

       protected override bool GetAuthCredentials(CefBrowser browser, string originUrl, bool isProxy, string host, int port, string realm, string scheme, CefAuthCallback callback)
        {
            return base.GetAuthCredentials(browser, originUrl, isProxy, host, port, realm, scheme, callback);
        }

        protected override bool OnBeforeBrowse(CefBrowser browser, CefFrame frame, CefRequest request, bool userGesture, bool isRedirect)
        {
            return base.OnBeforeBrowse(browser, frame, request, userGesture, isRedirect);
        }
    }
}
