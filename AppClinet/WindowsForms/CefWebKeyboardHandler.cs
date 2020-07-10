using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Xilium.CefGlue;

namespace CefiBrowser
{
    internal class CefWebKeyboardHandler :CefKeyboardHandler
    {
        private readonly CefWebBrowser _core;

        public CefWebKeyboardHandler(CefWebBrowser core)
        {
            _core = core;
        }
           
        public override string ToString()
        {
            return base.ToString();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        protected override bool OnPreKeyEvent(CefBrowser browser, CefKeyEvent keyEvent, IntPtr os_event, out bool isKeyboardShortcut)
        {
            if (keyEvent.WindowsKeyCode == 17 )
                MainForm.Instance.Invoke(new Action(() =>
                    { 
                        MainForm.Instance.cefCtrlDown = true; //Ctrl按下
                        MainForm.Instance.textBox2.ForeColor = Color.Black;
                    }
                    ));

             if(keyEvent.EventType == CefKeyEventType.KeyUp)
                MainForm.Instance.Invoke(new Action(() =>
                {
                    MainForm.Instance.cefCtrlDown = false; //Ctrl按下
                }
                 ));
            return base.OnPreKeyEvent(browser, keyEvent, os_event, out isKeyboardShortcut);
        }

        protected override bool OnKeyEvent(CefBrowser browser, CefKeyEvent keyEvent, IntPtr osEvent)
        {
            //对按键的处理
            switch (keyEvent.WindowsKeyCode)
            {
                case 27:
                    MainForm.Instance.Invoke(new Action(() =>
                    {
                        MainForm.Instance.searchOpen = false;
                        MainForm.Instance.CloseSearch();
                        MainForm.Instance.PanelSearch.Visible = false;
                    }
                   ));
                    break;
                case 68:
                    if (keyEvent.IsSystemKey)
                    {
                        MainForm.Instance.Invoke(new Action(() =>
                        {
                            MainForm.Instance.textBoxXP1.Focus();
                            MainForm.Instance.textBoxXP1.SelectAll();
                        }
                        ));
                    }
                    break;
                case 123://功能键 F12 的KeyCode
                    if (keyEvent.EventType.Equals(CefKeyEventType.RawKeyDown))
                    {
                        PublicClass.DevTools(_core, browser);

                    }
                    break;
                case 116: //F5刷新功能
                    if (keyEvent.EventType.Equals(CefKeyEventType.RawKeyDown))
                    {
                        PublicClass.ReflashBrowser(browser, _core);
                    }

                    break;
                case 122: //F11全屏和非全屏功能
                    if (keyEvent.EventType.Equals(CefKeyEventType.KeyUp))
                    {
                        //bool fullForm = false;
                        //if (MainForm.Instance.WindowState == System.Windows.Forms.FormWindowState.Maximized 
                        //    && MainForm.Instance.ToolsPanel.Visible)
                        //    fullForm = true;
                        PublicClass.ScreenFuction("");//,fullForm);

                    }
                    break;
            }
            GC.Collect();
            return base.OnKeyEvent(browser, keyEvent, osEvent);
        }

    }



}
