//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using CefSharp;
//using System.IO;

//namespace WFASpider
//{
//    public class RequestHandler_new : CefSharp.Handler.DefaultRequestHandler //CefSharp.Example.Handlers
//    {
//        public string _directory = "DownloadFile/";

//        private Dictionary<UInt64, MemoryStreamResponseFilter> responseDictionary = new Dictionary<UInt64, MemoryStreamResponseFilter>();


//        public IRequestHandler _requestHeandler;

//        public RequestHandler_new(IRequestHandler rh) : base()
//        {
//            _requestHeandler = rh;
//        }

//        public override CefReturnValue OnBeforeResourceLoad(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, IRequestCallback callback)
//        {

//            Uri url;
//            if (Uri.TryCreate(request.Url, UriKind.Absolute, out url) == false)
//            {
//                //If we're unable to parse the Uri then cancel the request
//                // avoid throwing any exceptions here as we're being called by unmanaged code
//                return CefReturnValue.Cancel;
//            }

//            //System.Diagnostics.Debug.WriteLine(request.ResourceType.ToString());
//            //System.Diagnostics.Debug.WriteLine(url);

//            var extension = url.ToString().ToLower();
//            if (request.ResourceType == ResourceType.Image || extension.EndsWith(".jpg") || extension.EndsWith(".png") || extension.EndsWith(".gif") || extension.EndsWith(".jpeg"))
//            {

//                System.Diagnostics.Debug.WriteLine(url);//打印

//            }


//            //下面是一大波官方 示例
//            //Uri url;
//            //if (Uri.TryCreate(request.Url, UriKind.Absolute, out url) == false)
//            //{
//            //    //If we're unable to parse the Uri then cancel the request
//            //    // avoid throwing any exceptions here as we're being called by unmanaged code
//            //    return CefReturnValue.Cancel;
//            //}

//            ////Example of how to set Referer
//            //// Same should work when setting any header

//            //// For this example only set Referer when using our custom scheme
//            //if (url.Scheme == CefSharpSchemeHandlerFactory.SchemeName)
//            //{
//            //    //Referrer is now set using it's own method (was previously set in headers before)
//            //    request.SetReferrer("http://google.com", ReferrerPolicy.Default);
//            //}

//            ////Example of setting User-Agent in every request.
//            ////var headers = request.Headers;

//            ////var userAgent = headers["User-Agent"];
//            ////headers["User-Agent"] = userAgent + " CefSharp";

//            ////request.Headers = headers;

//            ////NOTE: If you do not wish to implement this method returning false is the default behaviour
//            //// We also suggest you explicitly Dispose of the callback as it wraps an unmanaged resource.
//            ////callback.Dispose();
//            ////return false;

//            ////NOTE: When executing the callback in an async fashion need to check to see if it's disposed
//            //if (!callback.IsDisposed)
//            //{
//            //    using (callback)
//            //    {
//            //        if (request.Method == "POST")
//            //        {
//            //            using (var postData = request.PostData)
//            //            {
//            //                if (postData != null)
//            //                {
//            //                    var elements = postData.Elements;

//            //                    var charSet = request.GetCharSet();

//            //                    foreach (var element in elements)
//            //                    {
//            //                        if (element.Type == PostDataElementType.Bytes)
//            //                        {
//            //                            var body = element.GetBody(charSet);
//            //                        }
//            //                    }
//            //                }
//            //            }
//            //        }

//            //        //Note to Redirect simply set the request Url
//            //        //if (request.Url.StartsWith("https://www.google.com", StringComparison.OrdinalIgnoreCase))
//            //        //{
//            //        //    request.Url = "https://github.com/";
//            //        //}

//            //        //Callback in async fashion
//            //        //callback.Continue(true);
//            //        //return CefReturnValue.ContinueAsync;
//            //    }
//            //}

//            //return CefReturnValue.Continue;

//            if (_requestHeandler != null)
//            {
//                return _requestHeandler.OnBeforeResourceLoad(browserControl, browser, frame, request, callback);
//                //return base.OnBeforeResourceLoad(browserControl, browser, frame, request, callback);
//            }

//            return CefReturnValue.Continue;
//        }

//        public override IResponseFilter GetResourceResponseFilter(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, IResponse response)
//        {

//            var url = new Uri(request.Url);
//            var extension = url.ToString().ToLower();
//            if (request.ResourceType == ResourceType.Image || extension.EndsWith(".jpg") || extension.EndsWith(".png") || extension.EndsWith(".gif") || extension.EndsWith(".jpeg"))
//            {

//                //Only called for our customScheme
//                var dataFilter = new MemoryStreamResponseFilter();//新建成数据 处理器
//                responseDictionary.Add(request.Identifier, dataFilter);
//                return dataFilter;

//            }

//            if (_requestHeandler != null)
//            {
//                return _requestHeandler.GetResourceResponseFilter(browserControl, browser, frame, request, response);
//                //return base.GetResourceResponseFilter(browserControl, browser, frame, request, response);
//            }
//            return null;
//        }

//        Random _rand = new Random();

//        public override void OnResourceLoadComplete(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, IResponse response, UrlRequestStatus status, long receivedContentLength)
//        {

//            var url = new Uri(request.Url);
//            var extension = url.ToString().ToLower();
//            if (request.ResourceType == ResourceType.Image || extension.EndsWith(".jpg") || extension.EndsWith(".png") || extension.EndsWith(".gif") || extension.EndsWith(".jpeg"))
//            {
//                MemoryStreamResponseFilter filter;
//                if (responseDictionary.TryGetValue(request.Identifier, out filter))
//                {
//                    if (!Directory.Exists(_directory))
//                    {
//                        Directory.CreateDirectory(_directory);
//                    }


//                    System.Diagnostics.Debug.WriteLine("responseDictionary.Count:" + responseDictionary.Count);

//                    //TODO: Do something with the data here
//                    var data = filter.Data;
//                    var dataLength = filter.Data.Length;
//                    //NOTE: You may need to use a different encoding depending on the request
//                    //var dataAsUtf8String = Encoding.UTF8.GetString(data);

//                    if (dataLength > 0)
//                    {
//                        string fileName = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-fff-") + _rand.Next(99999, 999999) + ".png";

//                        string path = _directory + fileName;

//                        try
//                        {
//                            fileName = Path.GetFileName(url.ToString());

//                            File.WriteAllBytes(path, data);
//                            return;
//                        }
//                        catch (Exception e)
//                        {
//                            //throw;
//                        }

//                        fileName = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-fff-") + _rand.Next(99999, 999999) + ".png";
//                        path = _directory + fileName;
//                        File.WriteAllBytes(path, data);//保存数据
//                    }
//                }

//                return;
//            }

//            if (_requestHeandler != null)
//            {
//                _requestHeandler.OnResourceLoadComplete(browserControl, browser, frame, request, response, status, receivedContentLength);
//            }

//        }
//    }
//}
