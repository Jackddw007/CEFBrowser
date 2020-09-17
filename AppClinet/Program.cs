namespace CefiBrowser
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Windows.Forms;
    using Xilium.CefGlue;

    internal static class Program
    {


        [STAThread]
        private static int Main(string[] args)
        {
            PublicClass.currDirectiory =  Environment.CurrentDirectory.ToString();// PublicClass.currDirectiory = System.Windows.Forms.Application.StartupPath;
            new ProcessHook().InitHook();
            try
            {
                CefRuntime.Load();
            }
            catch (DllNotFoundException ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 1;
            }
            catch (CefRuntimeException ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 2;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 3;
            }

            var mainArgs = new CefMainArgs(args);
            var app = new DemoApp();
            
            var exitCode = CefRuntime.ExecuteProcess(mainArgs, app, IntPtr.Zero);
            if (exitCode != -1)
                return exitCode;

            var codeBase = Assembly.GetExecutingAssembly().CodeBase;
            var localFolder = Path.GetDirectoryName(new Uri(codeBase).LocalPath);
            var settings = new CefSettings
            {
                //BrowserSubprocessPath = browserProcessPath,
                //SingleProcess = false,
                //WindowlessRenderingEnabled = true,
                MultiThreadedMessageLoop = true,
                LogSeverity = CefLogSeverity.Disable,
                //LogFile = "CefGlue.log",
                Locale = "zh-CN",
                JavaScriptFlags = "js-flags",
                CommandLineArgsDisabled = false,
                IgnoreCertificateErrors = true,
                CachePath = GetAppDir("Cache"),
                UserAgent = CefConstHelper.UserAgent,
                //NoSandbox = true,

                //AcceptLanguageList = "zh-CN",
                //PersistSessionCookies = true,
            };


            CefRuntime.RegisterSchemeHandlerFactory(CefConstHelper.Branding, CefConstHelper.Branding, new SchemeHandlerFactory());
            CefRuntime.Initialize(mainArgs, settings, app,IntPtr.Zero);
            Application.EnableVisualStyles();
            CefRuntime.EnableHighDpiSupport();
            Application.SetCompatibleTextRenderingDefault(false);

            if (!settings.MultiThreadedMessageLoop)
            {
                Application.Idle += (sender, e) => { CefRuntime.DoMessageLoopWork(); };
            }

            if (args.Length > 0)
            {
                Application.Run(new MainForm(args[0].ToString()));

                //MessageBox.Show(PublicClass.StartUrl);
            }
            else
            {
                Application.Run(new MainForm(""));

            }


            CefRuntime.Shutdown();

            try
            {

                //if (Directory.Exists(GetAppDir("Cache")))
                //    Directory.Delete(GetAppDir("Cache"), true);
            }
            catch
            { }
            return 0;
        }

        //public static string CombinePaths(params string[] paths)
        //{
        //    if (paths == null)
        //    {
        //        throw new ArgumentNullException("paths");
        //    }
        //    return paths.Aggregate(Path.Combine);
        //}

        private static string GetAppDir(string name)
        {
            string winXPDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData); //@"C:\Documents and Settings\All Users\Application Data\";
            if (Directory.Exists(winXPDir))
            {
                return winXPDir + @"\" + CefConstHelper.Branding + @"\";
            }
            return @"C:\ProgramData\" + CefConstHelper.Branding + @"\";

        }
    }
}
