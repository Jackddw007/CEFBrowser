using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xilium.CefGlue;

namespace CefiBrowser
{
    public class CefWebRenderProcessHandler : CefRenderProcessHandler
    {
        private readonly CefWebBrowser _core;

        public CefWebRenderProcessHandler(CefWebBrowser core)
        {
            _core = core;
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

        protected override CefLoadHandler GetLoadHandler()
        {
            return base.GetLoadHandler();
        }

        protected override void OnBrowserCreated(CefBrowser browser, CefDictionaryValue extraInfo)
        {
            base.OnBrowserCreated(browser, extraInfo);
        }

        protected override void OnBrowserDestroyed(CefBrowser browser)
        {
            base.OnBrowserDestroyed(browser);
        }

        protected override void OnContextCreated(CefBrowser browser, CefFrame frame, CefV8Context context)
        {

            var global = context.GetGlobal();

            var extent = CefV8Value.CreateObject(null);

            global.SetValue("V8", extent, CefV8PropertyAttribute.None);
           
            base.OnContextCreated(browser, frame, context);
        }

        protected override void OnContextReleased(CefBrowser browser, CefFrame frame, CefV8Context context)
        {
            base.OnContextReleased(browser, frame, context);
        }

        protected override void OnFocusedNodeChanged(CefBrowser browser, CefFrame frame, CefDomNode node)
        {
            base.OnFocusedNodeChanged(browser, frame, node);
        }


        protected override bool OnProcessMessageReceived(CefBrowser browser, CefFrame frame, CefProcessId sourceProcess, CefProcessMessage message)
        {
           // return base.OnProcessMessageReceived(browser, frame, sourceProcess, message);
            string[] items = message.Name.Split(new char[] { '|' });
            if (items.Length == 0)
                return false;
            return true;
            //switch (items[0])
            //{
            //    case "EvaluateScript":
            //        {
            //            CefV8Value value = CefV8Value.CreateString("t");
            //            CefV8Exception exp;
            //            browser.GetMainFrame().V8Context.TryEval(null,items[1],0, out value, out exp);
            //            //CommonObj.JsEvaResult = null;
            //            if (value == null)
            //            {
            //                //CommonObj.flag = true;
            //                return true;
            //            }
            //            else
            //            if (value.IsArray)
            //            {

            //            }
            //            else
            //            if (value.IsString)
            //            {
            //                CommonObj.JsEvaResult = value.GetStringValue();
            //            }
            //            else
            //            if (value.IsInt)
            //            {
            //                CommonObj.JsEvaResult = value.GetIntValue();
            //            }
            //            else
            //            if (value.IsDouble)
            //            {
            //                CommonObj.JsEvaResult = value.GetDoubleValue();
            //            }
            //            else
            //            if (value.IsBool)
            //            {
            //                CommonObj.JsEvaResult = value.GetBoolValue();
            //            }
            //            else
            //            if (value.IsDate)
            //            {
            //                CommonObj.JsEvaResult = value.GetDateValue();
            //            }
            //            CommonObj.flag = true;
            //            return true;
            //        }
            //}
        }

        //protected override bool OnProcessMessageReceived(CefBrowser browser, CefProcessId sourceProcess, CefProcessMessage message)
        //{
        //    string[] items = message.Name.Split(new char[] { '|' });
        //    if (items.Length == 0) return false;

        //    switch (items[0])
        //    {
        //        case "EvaluateScript":
        //            {
        //                CefV8Value value = CefV8Value.CreateString("t");
        //                CefV8Exception exp;
        //                browser.GetMainFrame().V8Context.TryEval(items[1], out value, out exp);
        //                CommonObj.JsEvaResult = null;
        //                if (value == null)
        //                {
        //                    CommonObj.flag = true;
        //                    return true;
        //                }
        //                else
        //                if (value.IsArray)
        //                {

        //                }
        //                else
        //                if (value.IsString)
        //                {
        //                    CommonObj.JsEvaResult = value.GetStringValue();
        //                }
        //                else
        //                if (value.IsInt)
        //                {
        //                    CommonObj.JsEvaResult = value.GetIntValue();
        //                }
        //                else
        //                if (value.IsDouble)
        //                {
        //                    CommonObj.JsEvaResult = value.GetDoubleValue();
        //                }
        //                else
        //                if (value.IsBool)
        //                {
        //                    CommonObj.JsEvaResult = value.GetBoolValue();
        //                }
        //                else
        //                if (value.IsDate)
        //                {
        //                    CommonObj.JsEvaResult = value.GetDateValue();
        //                }
        //                CommonObj.flag = true;
        //                return true;
        //            }
        //    }
        //}

        protected override void OnRenderThreadCreated(CefListValue extraInfo)
        {
            base.OnRenderThreadCreated(extraInfo);
        }

        protected override void OnUncaughtException(CefBrowser browser, CefFrame frame, CefV8Context context, CefV8Exception exception, CefV8StackTrace stackTrace)
        {
            base.OnUncaughtException(browser, frame, context, exception, stackTrace);
        }

        protected override void OnWebKitInitialized()
        {
            base.OnWebKitInitialized();
        }
    }
}
