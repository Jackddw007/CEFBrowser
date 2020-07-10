namespace CefiBrowser
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Xilium.CefGlue;

    internal sealed class DemoApp : CefApp
    {
        protected override CefBrowserProcessHandler GetBrowserProcessHandler()
        {
            return base.GetBrowserProcessHandler();
        }

        protected override CefRenderProcessHandler GetRenderProcessHandler()
        {
            return base.GetRenderProcessHandler();
        }

        protected override CefResourceBundleHandler GetResourceBundleHandler()
        {
            return base.GetResourceBundleHandler();
        }

        protected override void OnBeforeCommandLineProcessing(string processType, CefCommandLine commandLine)
        {

            //commandLine.AppendSwitch("--ppapi-flash-path", Environment.CurrentDirectory + "\\plugins\\pepflashplayer.dll");
            //commandLine.AppendSwitch("--ppapi-flash-version", "20.0.0.267");
            //commandLine.AppendSwitch("no-referrers");
            //commandLine.AppendSwitch("--no-proxy-server"); //关闭代理，不关闭则自动使用IE代理
           
            //commandLine.AppendSwitch("proxy-server", "http://127.0.0.1:9090");
            //commandLine.AppendSwitch("--flag-switches-begin");
            //commandLine.AppendSwitch("--flag-switches-end");
            //commandLine.AppendSwitch("--enable-audio-service-sandbox");
            //commandLine.AppendSwitch("--disable-web-security");//关闭同源策略
            //commandLine.AppendSwitch("--enable-ephemeral-flash-permission");
            //commandLine.AppendSwitch("--enable-system-flash");//使用系统flash
            //commandLine.AppendSwitch("--disable-gpu");
            //commandLine.AppendSwitch("disable-ppapi", "1");
            //commandLine.AppendSwitch("--enable-system-flash", "1");
            //NOTE: The following function will set all three params
            //settings.SetOffScreenRenderingBestPerformanceArgs();
            //commandLine.AppendSwitch("disable-gpu", "1");
            //commandLine.AppendSwitch("disable-gpu-compositing", "1");
            //commandLine.AppendSwitch("enable-begin-frame-scheduling", "1");
            //commandLine.AppendSwitch("--disable-application-cache");
            //commandLine.AppendSwitch("--disable-local-storage");
            //commandLine.AppendSwitch("disable-gpu-vsync", "1"); //Disable Vsync
            //Disables the DirectWrite font rendering system on windows.
            //Possibly useful when experiencing blury fonts.
            //commandLine.AppendSwitch("disable-direct-write", "1");
            //commandLine.AppendSwitch("--enable-automatic-password-saving");
            //commandLine.AppendSwitch("--enable-password-save-in-page-navigation");
            //  base.OnBeforeCommandLineProcessing(processType, commandLine);
        }

        protected override void OnRegisterCustomSchemes(CefSchemeRegistrar registrar)
        {
            //registrar.AddCustomScheme("CefBrowser", true, true, true, true, true,true);
            registrar.AddCustomScheme(CefConstHelper.Branding, true, CefSchemeOptions.Standard);
            base.OnRegisterCustomSchemes(registrar);
        }
    }
}
