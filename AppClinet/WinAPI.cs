using System;
using System.Runtime.InteropServices;
using System.Text;

namespace CefiBrowser
{
    internal static class WinAPI
    {
        #region const

        public static readonly IntPtr TRUE = new IntPtr(1);
        public static readonly IntPtr FALSE = IntPtr.Zero;

        #endregion

        #region WindowMessages

        public enum WindowMessages
        {            
            WM_MOVE = 0x0003,
            WM_SIZE = 0x0005,
            WM_ACTIVATE = 0x0006,

            WM_ACTIVATEAPP = 0x001C,

            WM_SETCURSOR = 0x0020,
            WM_MOUSEACTIVATE = 0x0021,
            WM_GETMINMAXINFO = 0x24,
            WM_WINDOWPOSCHANGING = 0x0046,
            WM_WINDOWPOSCHANGED = 0x0047,

            // non client area
            WM_NCCREATE = 0x0081,
            WM_NCDESTROY = 0x0082,
            WM_NCCALCSIZE = 0x0083,
            WM_NCHITTEST = 0x84,
            WM_NCPAINT = 0x0085,
            WM_NCACTIVATE = 0x0086,

            // non client mouse
            WM_NCMOUSEMOVE = 0x00A0,
            WM_NCLBUTTONDOWN = 0x00A1,
            WM_NCLBUTTONUP = 0x00A2,
            WM_NCLBUTTONDBLCLK = 0x00A3,
            WM_NCRBUTTONDOWN = 0x00A4,
            WM_NCRBUTTONUP = 0x00A5,
            WM_NCRBUTTONDBLCLK = 0x00A6,
            WM_NCMBUTTONDOWN = 0x00A7,
            WM_NCMBUTTONUP = 0x00A8,
            WM_NCMBUTTONDBLCLK = 0x00A9,

            WM_SYSCOMMAND = 0x0112,
            WM_PARENTNOTIFY = 0x0210,

            WM_MDINEXT = 0x224,
        }

        #endregion        

        #region WindowStyle

      
       

        #endregion

       

        #region Scrollbar



        #endregion

        #region NCHITTEST
        /// <summary>
        /// Location of cursor hot spot returnet in WM_NCHITTEST.
        /// </summary>
        public enum NCHITTEST
        {
            /// <summary>
            /// On the screen background or on a dividing line between windows 
            /// (same as HTNOWHERE, except that the DefWindowProc function produces a system beep to indicate an error).
            /// </summary>
            HTERROR = (-2),
            /// <summary>
            /// In a window currently covered by another window in the same thread 
            /// (the message will be sent to underlying windows in the same thread until one of them returns a code that is not HTTRANSPARENT).
            /// </summary>
            HTTRANSPARENT = (-1),
            /// <summary>
            /// On the screen background or on a dividing line between windows.
            /// </summary>
            HTNOWHERE = 0,
            /// <summary>In a client area.</summary>
            HTCLIENT = 1,
            /// <summary>In a title bar.</summary>
            HTCAPTION = 2,
            /// <summary>In a window menu or in a Close button in a child window.</summary>
            HTSYSMENU = 3,
            /// <summary>In a size box (same as HTSIZE).</summary>
            HTGROWBOX = 4,
            /// <summary>In a menu.</summary>
            HTMENU = 5,
            /// <summary>In a horizontal scroll bar.</summary>
            HTHSCROLL = 6,
            /// <summary>In the vertical scroll bar.</summary>
            HTVSCROLL = 7,
            /// <summary>In a Minimize button.</summary>
            HTMINBUTTON = 8,
            /// <summary>In a Maximize button.</summary>
            HTMAXBUTTON = 9,
            /// <summary>In the left border of a resizable window 
            /// (the user can click the mouse to resize the window horizontally).</summary>
            HTLEFT = 10,
            /// <summary>
            /// In the right border of a resizable window 
            /// (the user can click the mouse to resize the window horizontally).
            /// </summary>
            HTRIGHT = 11,
            /// <summary>In the upper-horizontal border of a window.</summary>
            HTTOP = 12,
            /// <summary>In the upper-left corner of a window border.</summary>
            HTTOPLEFT = 13,
            /// <summary>In the upper-right corner of a window border.</summary>
            HTTOPRIGHT = 14,
            /// <summary>	In the lower-horizontal border of a resizable window 
            /// (the user can click the mouse to resize the window vertically).</summary>
            HTBOTTOM = 15,
            /// <summary>In the lower-left corner of a border of a resizable window 
            /// (the user can click the mouse to resize the window diagonally).</summary>
            HTBOTTOMLEFT = 16,
            /// <summary>	In the lower-right corner of a border of a resizable window 
            /// (the user can click the mouse to resize the window diagonally).</summary>
            HTBOTTOMRIGHT = 17,
            /// <summary>In the border of a window that does not have a sizing border.</summary>
            HTBORDER = 18,

            HTOBJECT = 19,
            /// <summary>In a Close button.</summary>
            HTCLOSE = 20,
            /// <summary>In a Help button.</summary>
            HTHELP = 21,
        }

        #endregion

        #region struct

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;

            public RECT(int left, int top, int right, int bottom)
            {
                this.Left = left;
                this.Top = top;
                this.Right = right;
                this.Bottom = bottom;
            }

            public override string ToString()
            {
                return "{ Left:" + this.Left + ", Top:" + this.Top
                    + ", Width:" + (this.Right - this.Left).ToString()
                    + ", Height:" + (this.Bottom - this.Top).ToString() + "}";
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct WINDOWPOS
        {
            public IntPtr hwnd;
            public IntPtr hWndInsertAfter;
            public int x;
            public int y;
            public int cx;
            public int cy;
            public uint flags;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NCCALCSIZE_PARAMS
        {
            /// <summary>
            /// Contains the new coordinates of a window that has been moved or resized, that is, it is the proposed new window coordinates.
            /// </summary>
            public RECT rectNewForm;
            /// <summary>
            /// Contains the coordinates of the window before it was moved or resized.
            /// </summary>
            public RECT rectOldForm;
            /// <summary>
            /// Contains the coordinates of the window's client area before the window was moved or resized.
            /// </summary>
            public RECT rectOldClient;
            /// <summary>
            /// Pointer to a WINDOWPOS structure that contains the size and position values specified in the operation that moved or resized the window.
            /// </summary>
            public WINDOWPOS lpPos;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SIZE
        {
            public Int32 cx;
            public Int32 cy;

            public SIZE(Int32 x, Int32 y)
            {
                cx = x;
                cy = y;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct BLENDFUNCTION
        {
            byte BlendOp;
            byte BlendFlags;
            byte SourceConstantAlpha;
            byte AlphaFormat;

            public BLENDFUNCTION(byte op, byte flags, byte alpha, byte format)
            {
                BlendOp = op;
                BlendFlags = flags;
                SourceConstantAlpha = alpha;
                AlphaFormat = format;
            }
        }

        //public enum BlendOp : byte
        //{
        //    AC_SRC_OVER = 0x00,
        //    AC_SRC_ALPHA = 0x01,
        //}        

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public Int32 x;
            public Int32 y;

            public POINT(Int32 x, Int32 y)
            {
                this.x = x;
                this.y = y;
            }
        }

        #endregion


        #region non-dll method

        public static int LOWORD(int value)
        {
            return value & 0xFFFF;
        }

        public static int HIWORD(int value)
        {
            return value >> 16;
        }

        #endregion

        //#region dll-import method

        //[DllImport("user32.dll")]
        //public static extern int ShowScrollBar(IntPtr hWnd, int wBar, int bShow);

        //[DllImport("user32.dll", CharSet = CharSet.Auto)]
        //public static extern int GetWindowLong(IntPtr hWnd, int Index);

        //[DllImport("user32.dll", CharSet = CharSet.Auto)]
        //public static extern int SetWindowLong(IntPtr hWnd, int Index, int Value);

        //[DllImport("user32.dll", ExactSpelling = true)]
        //public static extern int SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        //[DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        //public static extern IntPtr GetDC(IntPtr hWnd);

        //[DllImport("gdi32.dll", ExactSpelling = true, PreserveSig = true, SetLastError = true)]
        //public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

        //[DllImport("gdi32.dll")]
        //public static extern bool DeleteDC(IntPtr hdc);

        //[DllImport("user32.dll", ExactSpelling = true)]
        //public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        //[DllImport("gdi32.dll")]
        //public static extern bool DeleteObject(IntPtr hObject);

        //[DllImport("gdi32.dll", SetLastError = true)]
        //public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        //[DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        //public static extern bool UpdateLayeredWindow(IntPtr hwnd
        //    , IntPtr hdcDst
        //    , ref POINT pptDst
        //    , ref SIZE psize
        //    , IntPtr hdcSrc
        //    , ref POINT pptSrc
        //    , uint crKey
        //    , [In] ref BLENDFUNCTION pblend
        //    , uint dwFlags
        //    );

        //[System.Security.SuppressUnmanagedCodeSecurity]
        //[DllImport("user32.dll", CharSet = CharSet.Auto)]
        //public static extern IntPtr SendMessage(IntPtr hWnd, int msg,
        //                            IntPtr wParam, IntPtr lParam);

        //#endregion
    }
}
