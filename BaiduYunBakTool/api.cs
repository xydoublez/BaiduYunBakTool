﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using static BaiduYunBakTool.win32;

namespace BaiduYunBakTool
{



    public sealed class API
    {

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct TBBUTTONINFO
        {
            public int cbSize;
            public int dwMask;
            public int idCommand;
            public int iImage;
            public byte fsState;
            public byte fsStyle;
            public short cx;
            public IntPtr lParam;
            public IntPtr pszText;
            public int cchText;
        }
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetActiveWindow(IntPtr hWnd);
        [DllImport("USER32.DLL", EntryPoint = "PostMessage")]
        public static extern bool PostMessage(IntPtr hwnd, uint msg,
                IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);


        [DllImport("gdi32.dll")]
        public static extern int SetBkColor(
            IntPtr hdc,           // handle to DC
            int crColor   // background color value
            );
        [DllImport("gdi32.dll")]
        public static extern int GetBkColor(IntPtr hdc);

        [DllImport("user32.dll", EntryPoint = "GetDC")]
        public static extern IntPtr GetDC(IntPtr ptr);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetDesktopWindow();

        [DllImport("user32")]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter,
            int x, int y, int cx, int cy, int flags);
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        internal static extern int MapWindowPoints(IntPtr hWndFrom, IntPtr hWndTo, ref Point pt, int cPoints);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetWindowDC(IntPtr hwnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, ref TBBUTTONINFO buttonInfo);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int RegisterWindowMessage(string msg);
        /// <summary>
        /// 设置目标窗体大小，位置
        /// </summary>
        /// <param name="hWnd">目标句柄</param>
        /// <param name="x">目标窗体新位置X轴坐标</param>
        /// <param name="y">目标窗体新位置Y轴坐标</param>
        /// <param name="nWidth">目标窗体新宽度</param>
        /// <param name="nHeight">目标窗体新高度</param>
        /// <param name="BRePaint">是否刷新窗体</param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, bool BRePaint);
        [DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool BitBlt(IntPtr hDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);
        //该函数获得一个指定子窗口的父窗口句柄
        [DllImport("user32.dll")]
        public static extern int GetParent(IntPtr hwnd);
        [DllImport("user32.dll")]
        public static extern IntPtr SetFocus(IntPtr hWnd);
        //该函数获得给定窗口的可视状态。
        [DllImport("user32.dll")]
        public static extern bool IsWindowVisible(IntPtr hwnd);
        public delegate bool CallBack(IntPtr hwnd, int y);
        //该函数枚举所有屏幕上的顶层窗口，并将窗口句柄传送给应用程序定义的回调函数。
        //回调函数返回FALSE将停止枚举，否则EnumWindows函数继续到所有顶层窗口枚举完为止。
        [DllImport("user32.dll")]
        public static extern int EnumWindows(CallBack x, int y);    
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr DefWindowProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);


        [DllImport("ole32.dll", PreserveSig = false)]
        public static extern IntPtr CreateILockBytesOnHGlobal(IntPtr hGlobal, bool fDeleteOnRelease);
        //    public static extern ILockBytes CreateILockBytesOnHGlobal(IntPtr hGlobal, bool fDeleteOnRelease);


        //[DllImport("ole32.dll", PreserveSig = false)]
        //public static extern IntPtr StgCreateDocfileOnILockBytes(IntPtr iLockBytes, STGM grfMode, int reserved);
        //    public static extern IStorage StgCreateDocfileOnILockBytes(ILockBytes iLockBytes, STGM grfMode, int reserved);

        public const int PROCESS_ALL_ACCESS = 2035711;

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);
        [Flags]
        public enum AllocationType
        {
            Commit = 0x1000,
            Reserve = 0x2000,
            Decommit = 0x4000,
            Release = 0x8000,
            Reset = 0x80000,
            Physical = 0x400000,
            TopDown = 0x100000,
            WriteWatch = 0x200000,
            LargePages = 0x20000000
        }

        [Flags]
        public enum MemoryProtection
        {
            Execute = 0x10,
            ExecuteRead = 0x20,
            ExecuteReadWrite = 0x40,
            ExecuteWriteCopy = 0x80,
            NoAccess = 0x01,
            ReadOnly = 0x02,
            ReadWrite = 0x04,
            WriteCopy = 0x08,
            GuardModifierflag = 0x100,
            NoCacheModifierflag = 0x200,
            WriteCombineModifierflag = 0x400
        }
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        public static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress,uint dwSize, AllocationType flAllocationType, MemoryProtection flProtect);
        [DllImport("kernel32.dll ")]
        public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, out byte[] lpBuffer, int nSize, out int lpNumberOfBytesRead);
        [Flags]
        public enum FreeType
        {
            Decommit = 0x4000,
            Release = 0x8000,
        }
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        public static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress,int dwSize, FreeType dwFreeType);
        [DllImport("kernel32.dll ")]
        public static extern bool CloseHandle(IntPtr hProcess);
        [DllImport("kernel32.dll")]
        public static extern bool WriteProcessMemory(
                   IntPtr hProcess,
                   IntPtr lpBaseAddress,
                   IntPtr lpBuffer,
                   int nSize,
                   out int lpNumberOfBytesWritten
               );
        [DllImport("kernel32.dll ")]
        static extern bool ReadProcessMemory(int hProcess, int lpBaseAddress, out int lpBuffer, int nSize, out int lpNumberOfBytesRead);
        //二维数组
        [DllImport("kernel32.dll ")]
        static extern bool ReadProcessMemory(int hProcess, int lpBaseAddress, byte[,] lpBuffer, int nSize, out int lpNumberOfBytesRead);
        //一维数组
        [DllImport("kernel32.dll ")]
        static extern bool ReadProcessMemory(int hProcess, int lpBaseAddress, byte[] lpBuffer, int nSize, out int lpNumberOfBytesRead);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hWnd, int message, IntPtr wParam, IntPtr lParam);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool GetWindowRect(IntPtr hWnd, ref Rectangle lpRect);

        [StructLayout(LayoutKind.Sequential)]
        public struct MouseInput
        {
            public Int32 dx;
            public Int32 dy;
            public Int32 Mousedata;
            public Int32 dwFlag;
            public Int32 time;
            public IntPtr dwExtraInfo;
        }
        public struct DROPFILES
        {
            public int pFiles;
            public int x;
            public int y;
            public int fNC;
            public int fWide;
        };
        [StructLayout(LayoutKind.Sequential)]
        public struct tagKEYBDINPUT
        {
            Int16 wVk;
            Int16 wScan;
            Int32 dwFlags;
            Int32 time;
            IntPtr dwExtraInfo;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct tagHARDWAREINPUT
        {
            Int32 uMsg;
            Int16 wParamL;
            Int16 wParamH;
        }
        [StructLayout(LayoutKind.Explicit)]
        public struct Input
        {
            [FieldOffset(0)] public Int32 type;
            [FieldOffset(4)] public MouseInput mi;
            [FieldOffset(4)] public tagKEYBDINPUT ki;
            [FieldOffset(4)] public tagHARDWAREINPUT hi;
        }
        /// <summary>
        /// Synthesizes keystrokes, mouse motions, and button clicks.
        /// </summary>
        [DllImport("user32.dll")]
        public static extern uint SendInput(uint nInputs, [MarshalAs(UnmanagedType.LPArray), In] Input[] pInputs, int cbSize);

        [DllImport("user32.dll")]
        public static extern IntPtr GetSystemMenu(IntPtr hwnd, bool bRevert);


        [DllImport("USER32.DLL")]
        public static extern int SendMessage(
            IntPtr hwnd,
            int wMsg,
            int wParam,
            IntPtr lParam
            );
        [DllImport("USER32.DLL")]
        public static extern int SendMessage(
            IntPtr hwnd,
            int wMsg,
            int wParam,
            StringBuilder lParam
            );
        [DllImport("USER32.DLL")]
        public static extern int SendMessage(
            IntPtr hwnd,
            int wMsg,
            int wParam,
            string lParam
            );
        [DllImport("USER32.DLL")]
        public static extern int SendMessage(
            IntPtr hwnd,
            int wMsg,
            bool wParam,
            string lParam
            );
        [DllImport("USER32.DLL")]
        public static extern int SendMessage(
            IntPtr hwnd,
            int wMsg,
            bool wParam,
            int lParam
            );

        [DllImport("USER32.DLL")]
        public static extern int SendMessage(
               IntPtr hwnd,
               int wMsg,
               int wParam,
               ref Rectangle lParam
               );
        #region

        public const int SRCCOPY = 0x00CC0020; /* dest = source                   */
        public const int SRCPAINT = 0x00EE0086; /* dest = source OR dest           */
        public const int SRCAND = 0x008800C6; /* dest = source AND dest          */
        public const int SRCINVERT = 0x00660046; /* dest = source XOR dest          */
        public const int SRCERASE = 0x00440328; /* dest = source AND (NOT dest )   */
        public const int NOTSRCCOPY = 0x00330008; /* dest = (NOT source)             */
        public const int NOTSRCERASE = 0x001100A6; /* dest = (NOT src) AND (NOT dest) */
        public const int MERGECOPY = 0x00C000CA; /* dest = (source AND pattern)     */
        public const int MERGEPAINT = 0x00BB0226; /* dest = (NOT source) OR dest     */
        public const int PATCOPY = 0x00F00021; /* dest = pattern                  */
        public const int PATPAINT = 0x00FB0A09; /* dest = DPSnoo                   */
        public const int PATINVERT = 0x005A0049; /* dest = pattern XOR dest         */
        public const int DSTINVERT = 0x00550009; /* dest = (NOT dest)               */
        public const int BLACKNESS = 0x00000042; /* dest = BLACK                    */
        public const int WHITENESS = 0x00FF0062; /* dest = WHITE   */
        static int wm_mouseenter = -1;
        public static int WM_MOUSEENTER
        {
            get
            {
                if (wm_mouseenter == -1)
                {
                    wm_mouseenter = RegisterWindowMessage("WinFormsMouseEnter");
                }
                return wm_mouseenter;
            }
        }

        public const int EM_GETOLEINTERFACE = WM_USER + 60;
        public const int SC_MOVE = 0xF010;
        /*
         * WM_NCHITTEST and MOUSEHOOKSTRUCT Mouse Position Codes
         */
        public const int HTERROR = (-2);
        public const int HTTRANSPARENT = (-1);
        //    public const int HTNOWHERE          = 0;
        //    public const int HTCLIENT            =1;
        public const int HTCAPTION = 2;
        public const int HTSYSMENU = 3;
        public const int HTGROWBOX = 4;
        public const int HTSIZE = HTGROWBOX;
        public const int HTMENU = 5;
        public const int HTHSCROLL = 6;
        public const int HTVSCROLL = 7;
        public const int HTMINBUTTON = 8;
        public const int HTMAXBUTTON = 9;
        public const int HTLEFT = 10;
        public const int HTRIGHT = 11;
        public const int HTTOP = 12;
        public const int HTTOPLEFT = 13;
        public const int HTTOPRIGHT = 14;
        public const int HTBOTTOM = 15;
        public const int HTBOTTOMLEFT = 16;
        public const int HTBOTTOMRIGHT = 17;
        public const int HTBORDER = 18;
        public const int HTCLOSE = 20;
        public const int HTHELP = 21;


        /*
         * WM_NCHITTEST and MOUSEHOOKSTRUCT Mouse Position Codes
         */
        /*
#define HTERROR             (-2)
#define HTTRANSPARENT       (-1)
#define HTNOWHERE           0
#define HTCLIENT            1
#define HTCAPTION           2
#define HTSYSMENU           3
#define HTGROWBOX           4
#define HTSIZE              HTGROWBOX
#define HTMENU              5
#define HTHSCROLL           6
#define HTVSCROLL           7
#define HTMINBUTTON         8
#define HTMAXBUTTON         9
#define HTLEFT              10
#define HTRIGHT             11
#define HTTOP               12
#define HTTOPLEFT           13
#define HTTOPRIGHT          14
#define HTBOTTOM            15
#define HTBOTTOMLEFT        16
#define HTBOTTOMRIGHT       17
#define HTBORDER            18
#define HTREDUCE            HTMINBUTTON
#define HTZOOM              HTMAXBUTTON
#define HTSIZEFIRST         HTLEFT
#define HTSIZELAST          HTBOTTOMRIGHT
#if(WINVER >= 0x0400)
#define HTOBJECT            19
#define HTCLOSE             20
#define HTHELP              21
#endif /* WINVER >= 0x0400 */


        public const int ACM_OPENA = 0x464;
        public const int ACM_OPENW = 0x467;
        public const int ADVF_ONLYONCE = 2;
        public const int ADVF_PRIMEFIRST = 4;
        public const int ARW_BOTTOMLEFT = 0;
        public const int ARW_BOTTOMRIGHT = 1;
        public const int ARW_DOWN = 4;
        public const int ARW_HIDE = 8;
        public const int ARW_LEFT = 0;
        public const int ARW_RIGHT = 0;
        public const int ARW_TOPLEFT = 2;
        public const int ARW_TOPRIGHT = 3;
        public const int ARW_UP = 4;
        public const int BDR_RAISED = 5;
        public const int BDR_RAISEDINNER = 4;
        public const int BDR_RAISEDOUTER = 1;
        public const int BDR_SUNKEN = 10;
        public const int BDR_SUNKENINNER = 8;
        public const int BDR_SUNKENOUTER = 2;
        public const int BF_ADJUST = 0x2000;
        public const int BF_BOTTOM = 8;
        public const int BF_FLAT = 0x4000;
        public const int BF_LEFT = 1;
        public const int BF_MIDDLE = 0x800;
        public const int BF_RIGHT = 4;
        public const int BF_TOP = 2;
        public const int BFFM_ENABLEOK = 0x465;
        public const int BFFM_INITIALIZED = 1;
        public const int BFFM_SELCHANGED = 2;
        public const int BFFM_SETSELECTION = 0x467;
        public const int BI_BITFIELDS = 3;
        public const int BI_RGB = 0;
        public const int BITMAPINFO_MAX_COLORSIZE = 0x100;
        public const int BITSPIXEL = 12;
        public const int BM_SETCHECK = 0xf1;
        public const int BM_SETSTATE = 0xf3;
        public const int BN_CLICKED = 0;
        public const int BS_3STATE = 5;
        public const int BS_BOTTOM = 0x800;
        public const int BS_CENTER = 0x300;
        public const int BS_DEFPUSHBUTTON = 1;
        public const int BS_GROUPBOX = 7;
        public const int BS_LEFT = 0x100;
        public const int BS_MULTILINE = 0x2000;
        public const int BS_OWNERDRAW = 11;
        public const int BS_PATTERN = 3;
        public const int BS_PUSHBUTTON = 0;
        public const int BS_PUSHLIKE = 0x1000;
        public const int BS_RADIOBUTTON = 4;
        public const int BS_RIGHT = 0x200;
        public const int BS_RIGHTBUTTON = 0x20;
        public const int BS_TOP = 0x400;
        public const int BS_VCENTER = 0xc00;
        public const int CB_ADDSTRING = 0x143;
        public const int CB_DELETESTRING = 0x144;
        public const int CB_ERR = -1;
        public const int CB_FINDSTRING = 0x14c;
        public const int CB_FINDSTRINGEXACT = 0x158;
        public const int CB_GETCURSEL = 0x147;
        public const int CB_GETDROPPEDSTATE = 0x157;
        public const int CB_GETEDITSEL = 320;
        public const int CB_GETITEMDATA = 0x150;
        public const int CB_GETITEMHEIGHT = 340;
        public const int CB_INSERTSTRING = 330;
        public const int CB_LIMITTEXT = 0x141;
        public const int CB_RESETCONTENT = 0x14b;
        public const int CB_SETCURSEL = 0x14e;
        public const int CB_SETDROPPEDWIDTH = 0x160;
        public const int CB_SETEDITSEL = 0x142;
        public const int CB_SETITEMHEIGHT = 0x153;
        public const int CB_SHOWDROPDOWN = 0x14f;
        public const int CBEM_GETITEMA = 0x404;
        public const int CBEM_GETITEMW = 0x40d;
        public const int CBEM_INSERTITEMA = 0x401;
        public const int CBEM_INSERTITEMW = 0x40b;
        public const int CBEM_SETITEMA = 0x405;
        public const int CBEM_SETITEMW = 0x40c;
        public const int CBEN_ENDEDITA = -805;
        public const int CBEN_ENDEDITW = -806;
        public const int CBN_DBLCLK = 2;
        public const int CBN_DROPDOWN = 7;
        public const int CBN_EDITCHANGE = 5;
        public const int CBN_SELCHANGE = 1;
        public const int CBN_SELENDOK = 9;
        public const int CBS_AUTOHSCROLL = 0x40;
        public const int CBS_DROPDOWN = 2;
        public const int CBS_DROPDOWNLIST = 3;
        public const int CBS_HASSTRINGS = 0x200;
        public const int CBS_NOINTEGRALHEIGHT = 0x400;
        public const int CBS_OWNERDRAWFIXED = 0x10;
        public const int CBS_OWNERDRAWVARIABLE = 0x20;
        public const int CBS_SIMPLE = 1;
        public const int CC_ANYCOLOR = 0x100;
        public const int CC_ENABLEHOOK = 0x10;
        public const int CC_FULLOPEN = 2;
        public const int CC_PREVENTFULLOPEN = 4;
        public const int CC_RGBINIT = 1;
        public const int CC_SHOWHELP = 8;
        public const int CC_SOLIDCOLOR = 0x80;
        public const int CCS_NODIVIDER = 0x40;
        public const int CCS_NOPARENTALIGN = 8;
        public const int CCS_NORESIZE = 4;
        public const int CDDS_ITEM = 0x10000;
        public const int CDDS_ITEMPREPAINT = 0x10001;
        public const int CDDS_POSTPAINT = 2;
        public const int CDDS_PREPAINT = 1;
        public const int CDDS_SUBITEM = 0x20000;
        public const int CDERR_DIALOGFAILURE = 0xffff;
        public const int CDERR_FINDRESFAILURE = 6;
        public const int CDERR_INITIALIZATION = 2;
        public const int CDERR_LOADRESFAILURE = 7;
        public const int CDERR_LOADSTRFAILURE = 5;
        public const int CDERR_LOCKRESFAILURE = 8;
        public const int CDERR_MEMALLOCFAILURE = 9;
        public const int CDERR_MEMLOCKFAILURE = 10;
        public const int CDERR_NOHINSTANCE = 4;
        public const int CDERR_NOHOOK = 11;
        public const int CDERR_NOTEMPLATE = 3;
        public const int CDERR_REGISTERMSGFAIL = 12;
        public const int CDERR_STRUCTSIZE = 1;
        public const int CDIS_CHECKED = 8;
        public const int CDIS_DEFAULT = 0x20;
        public const int CDIS_DISABLED = 4;
        public const int CDIS_FOCUS = 0x10;
        public const int CDIS_GRAYED = 2;
        public const int CDIS_HOT = 0x40;
        public const int CDIS_INDETERMINATE = 0x100;
        public const int CDIS_MARKED = 0x80;
        public const int CDIS_SELECTED = 1;
        public const int CDRF_DODEFAULT = 0;
        public const int CDRF_NEWFONT = 2;
        public const int CDRF_NOTIFYITEMDRAW = 0x20;
        public const int CDRF_NOTIFYPOSTPAINT = 0x10;
        public const int CDRF_NOTIFYSUBITEMDRAW = 0x20;
        public const int CDRF_SKIPDEFAULT = 4;
        public const int CF_APPLY = 0x200;
        public const int CF_BITMAP = 2;
        public const int CF_DIB = 8;
        public const int CF_DIF = 5;
        public const int CF_EFFECTS = 0x100;
        public const int CF_ENABLEHOOK = 8;
        public const int CF_ENHMETAFILE = 14;
        public const int CF_FIXEDPITCHONLY = 0x4000;
        public const int CF_FORCEFONTEXIST = 0x10000;
        public const int CF_HDROP = 15;
        public const int CF_INITTOLOGFONTSTRUCT = 0x40;
        public const int CF_LIMITSIZE = 0x2000;
        public const int CF_LOCALE = 0x10;
        public const int CF_METAFILEPICT = 3;
        public const int CF_NOSIMULATIONS = 0x1000;
        public const int CF_NOVECTORFONTS = 0x800;
        public const int CF_NOVERTFONTS = 0x1000000;
        public const int CF_OEMTEXT = 7;
        public const int CF_PALETTE = 9;
        public const int CF_PENDATA = 10;
        public const int CF_RIFF = 11;
        public const int CF_SCREENFONTS = 1;
        public const int CF_SCRIPTSONLY = 0x400;
        public const int CF_SELECTSCRIPT = 0x400000;
        public const int CF_SHOWHELP = 4;
        public const int CF_SYLK = 4;
        public const int CF_TEXT = 1;
        public const int CF_TIFF = 6;
        public const int CF_TTONLY = 0x40000;
        public const int CF_UNICODETEXT = 13;
        public const int CF_WAVE = 12;
        public const int CFERR_MAXLESSTHANMIN = 0x2002;
        public const int CFERR_NOFONTS = 0x2001;
        public const int CHILDID_SELF = 0;
        public const int CLR_DEFAULT = -16777216;
        public const int CLR_NONE = -1;
        public const int cmb4 = 0x473;
        public const int COLOR_WINDOW = 5;
        public const int CONNECT_E_CANNOTCONNECT = -2147220990;
        public const int CONNECT_E_NOCONNECTION = -2147220992;
        public const int CP_WINANSI = 0x3ec;
        public const int CS_DBLCLKS = 8;
        public const int CSIDL_APPDATA = 0x1a;
        public const int CSIDL_COMMON_APPDATA = 0x23;
        public const int CSIDL_COOKIES = 0x21;
        public const int CSIDL_DESKTOP = 0;
        public const int CSIDL_DESKTOPDIRECTORY = 0x10;
        public const int CSIDL_FAVORITES = 6;
        public const int CSIDL_HISTORY = 0x22;
        public const int CSIDL_INTERNET = 1;
        public const int CSIDL_INTERNET_CACHE = 0x20;
        public const int CSIDL_LOCAL_APPDATA = 0x1c;
        public const int CSIDL_PERSONAL = 5;
        public const int CSIDL_PROGRAM_FILES = 0x26;
        public const int CSIDL_PROGRAM_FILES_COMMON = 0x2b;
        public const int CSIDL_PROGRAMS = 2;
        public const int CSIDL_RECENT = 8;
        public const int CSIDL_SENDTO = 9;
        public const int CSIDL_STARTMENU = 11;
        public const int CSIDL_STARTUP = 7;
        public const int CSIDL_SYSTEM = 0x25;
        public const int CSIDL_TEMPLATES = 0x15;
        public const int CTRLINFO_EATS_ESCAPE = 2;
        public const int CTRLINFO_EATS_RETURN = 1;
        public const int CW_USEDEFAULT = -2147483648;
        public const int CWP_SKIPINVISIBLE = 1;
        public const int DCX_CACHE = 2;
        public const int DCX_LOCKWINDOWUPDATE = 0x400;
        public const int DCX_WINDOW = 1;
        public const int DEFAULT_GUI_FONT = 0x11;
        public const int DFC_BUTTON = 4;
        public const int DFC_CAPTION = 1;
        public const int DFC_MENU = 2;
        public const int DFC_SCROLL = 3;
        public const int DFCS_BUTTON3STATE = 8;
        public const int DFCS_BUTTONCHECK = 0;
        public const int DFCS_BUTTONPUSH = 0x10;
        public const int DFCS_BUTTONRADIO = 4;
        public const int DFCS_CAPTIONCLOSE = 0;
        public const int DFCS_CAPTIONHELP = 4;
        public const int DFCS_CAPTIONMAX = 2;
        public const int DFCS_CAPTIONMIN = 1;
        public const int DFCS_CAPTIONRESTORE = 3;
        public const int DFCS_CHECKED = 0x400;
        public const int DFCS_FLAT = 0x4000;
        public const int DFCS_INACTIVE = 0x100;
        public const int DFCS_MENUARROW = 0;
        public const int DFCS_MENUBULLET = 2;
        public const int DFCS_MENUCHECK = 1;
        public const int DFCS_PUSHED = 0x200;
        public const int DFCS_SCROLLCOMBOBOX = 5;
        public const int DFCS_SCROLLDOWN = 1;
        public const int DFCS_SCROLLLEFT = 2;
        public const int DFCS_SCROLLRIGHT = 3;
        public const int DFCS_SCROLLUP = 0;
        public const int DI_NORMAL = 3;
        public const int DIB_RGB_COLORS = 0;
        public const int DISP_E_EXCEPTION = -2147352567;
        public const int DISP_E_MEMBERNOTFOUND = -2147352573;
        public const int DISP_E_PARAMNOTFOUND = -2147352572;
        public const int DISPATCH_METHOD = 1;
        public const int DISPATCH_PROPERTYGET = 2;
        public const int DISPATCH_PROPERTYPUT = 4;
        public const int DISPID_PROPERTYPUT = -3;
        public const int DISPID_UNKNOWN = -1;
        public const int DLGC_WANTALLKEYS = 4;
        public const int DLGC_WANTARROWS = 1;
        public const int DLGC_WANTCHARS = 0x80;
        public const int DLGC_WANTTAB = 2;
        public const int DRAGDROP_E_ALREADYREGISTERED = -2147221247;
        public const int DRAGDROP_E_NOTREGISTERED = -2147221248;
        public const int DT_CALCRECT = 0x400;
        public const int DT_EDITCONTROL = 0x2000;
        public const int DT_END_ELLIPSIS = 0x8000;
        public const int DT_EXPANDTABS = 0x40;
        public const int DT_LEFT = 0;
        public const int DT_NOCLIP = 0x100;
        public const int DT_NOPREFIX = 0x800;
        public const int DT_RIGHT = 2;
        public const int DT_RTLREADING = 0x20000;
        public const int DT_SINGLELINE = 0x20;
        public const int DT_VCENTER = 4;
        public static readonly int DTM_SETFORMAT;
        public const int DTM_SETFORMATA = 0x1005;
        public const int DTM_SETFORMATW = 0x1032;
        public const int DTM_SETMCCOLOR = 0x1006;
        public const int DTM_SETMCFONT = 0x1009;
        public const int DTM_SETRANGE = 4100;
        public const int DTM_SETSYSTEMTIME = 0x1002;
        public const int DTN_CLOSEUP = -753;
        public const int DTN_DATETIMECHANGE = -759;
        public const int DTN_DROPDOWN = -754;
        public static readonly int DTN_FORMAT;
        public const int DTN_FORMATA = -756;
        public static readonly int DTN_FORMATQUERY;
        public const int DTN_FORMATQUERYA = -755;
        public const int DTN_FORMATQUERYW = -742;
        public const int DTN_FORMATW = -743;
        public static readonly int DTN_USERSTRING;
        public const int DTN_USERSTRINGA = -758;
        public const int DTN_USERSTRINGW = -745;
        public static readonly int DTN_WMKEYDOWN;
        public const int DTN_WMKEYDOWNA = -757;
        public const int DTN_WMKEYDOWNW = -744;
        public const int DTS_LONGDATEFORMAT = 4;
        public const int DTS_RIGHTALIGN = 0x20;
        public const int DTS_SHOWNONE = 2;
        public const int DTS_TIMEFORMAT = 9;
        public const int DTS_UPDOWN = 1;
        public const int DUPLICATE = 6;
        public const int DUPLICATE_SAME_ACCESS = 2;
        public const int DV_E_DVASPECT = -2147221397;
        public const int DVASPECT_CONTENT = 1;
        public const int DVASPECT_OPAQUE = 0x10;
        public const int DVASPECT_TRANSPARENT = 0x20;
        public const int E_ABORT = -2147467260;
        public const int E_FAIL = -2147467259;
        public const int E_INVALIDARG = -2147024809;
        public const int E_NOINTERFACE = -2147467262;
        public const int E_NOTIMPL = -2147467263;
        public const int E_OUTOFMEMORY = -2147024882;
        public const int E_UNEXPECTED = -2147418113;
        public const int EC_LEFTMARGIN = 1;
        public const int EC_RIGHTMARGIN = 2;
        public const int EDGE_BUMP = 9;
        public const int EDGE_ETCHED = 6;
        public const int EDGE_RAISED = 5;
        public const int EDGE_SUNKEN = 10;
        public const int EM_CANUNDO = 0xc6;
        public const int EM_CHARFROMPOS = 0xd7;
        public const int EM_EMPTYUNDOBUFFER = 0xcd;
        public const int EM_GETLINE = 0xc4;
        public const int EM_GETLINECOUNT = 0xba;
        public const int EM_GETMODIFY = 0xb8;
        public const int EM_GETSEL = 0xb0;
        public const int EM_LIMITTEXT = 0xc5;
        public const int EM_POSFROMCHAR = 0xd6;
        public const int EM_REPLACESEL = 0xc2;
        public const int EM_SCROLL = 0xb5;
        public const int EM_SCROLLCARET = 0xb7;
        public const int EM_SETMARGINS = 0xd3;
        public const int EM_SETMODIFY = 0xb9;
        public const int EM_SETPASSWORDCHAR = 0xcc;
        public const int EM_SETREADONLY = 0xcf;
        public const int EM_SETSEL = 0xb1;
        public const int EM_UNDO = 0xc7;
        public static readonly int EMR_POLYTEXTOUT;
        public const int EMR_POLYTEXTOUTA = 0x60;
        public const int EMR_POLYTEXTOUTW = 0x61;
        public const int EN_ALIGN_LTR_EC = 0x700;
        public const int EN_ALIGN_RTL_EC = 0x701;
        public const int EN_CHANGE = 0x300;
        public const int EN_HSCROLL = 0x601;
        public const int EN_VSCROLL = 0x602;
        public const int ES_AUTOHSCROLL = 0x80;
        public const int ES_AUTOVSCROLL = 0x40;
        public const int ES_CENTER = 1;
        public const int ES_LEFT = 0;
        public const int ES_LOWERCASE = 0x10;
        public const int ES_MULTILINE = 4;
        public const int ES_NOHIDESEL = 0x100;
        public const int ES_READONLY = 0x800;
        public const int ES_RIGHT = 2;
        public const int ES_UPPERCASE = 8;
        public const int ETO_CLIPPED = 4;
        public const int ETO_OPAQUE = 2;
        public const int FADF_BSTR = 0x100;
        public const int FADF_DISPATCH = 0x400;
        public const int FADF_UNKNOWN = 0x200;
        public const int FADF_VARIANT = 0x800;
        public const int FALT = 0x10;
        public const int FNERR_BUFFERTOOSMALL = 0x3003;
        public const int FNERR_INVALIDFILENAME = 12290;
        public const int FNERR_SUBCLASSFAILURE = 0x3001;
        public const int FORMAT_MESSAGE_FROM_SYSTEM = 0x1000;
        public const int FORMAT_MESSAGE_IGNORE_INSERTS = 0x200;
        public const int FRERR_BUFFERLENGTHZERO = 0x4001;
        public const int FSHIFT = 4;
        public const int FVIRTKEY = 1;
        public const int GDI_ERROR = -1;
        public const int GDT_NONE = 1;
        public const int GDT_VALID = 0;
        public const int GDTR_MAX = 2;
        public const int GDTR_MIN = 1;
        public const int GMEM_DDESHARE = 0x2000;
        public const int GMEM_MOVEABLE = 2;
        public const int GMEM_ZEROINIT = 0x40;
        public const int GMR_DAYSTATE = 1;
        public const int GMR_VISIBLE = 0;
        public const int GW_CHILD = 5;
        public const int GW_HWNDNEXT = 2;
        public const int GW_HWNDPREV = 3;
        public const int GWL_EXSTYLE = -20;
        public const int GWL_HWNDPARENT = -8;
        public const int GWL_ID = -12;
        public const int GWL_STYLE = -16;
        public const int GWL_WNDPROC = -4;
        public const int HC_ACTION = 0;
        public const int HC_GETNEXT = 1;
        public const int HC_SKIP = 2;
        public const int HCF_HIGHCONTRASTON = 1;
        public static readonly int HDM_GETITEM;
        public const int HDM_GETITEMA = 0x1203;
        public const int HDM_GETITEMCOUNT = 0x1200;
        public const int HDM_GETITEMW = 0x120b;
        public static readonly int HDM_INSERTITEM;
        public const int HDM_INSERTITEMA = 0x1201;
        public const int HDM_INSERTITEMW = 0x120a;
        public static readonly int HDM_SETITEM;
        public const int HDM_SETITEMA = 0x1204;
        public const int HDM_SETITEMW = 4620;
        public static readonly int HDN_BEGINTRACK;
        public const int HDN_BEGINTRACKA = -306;
        public const int HDN_BEGINTRACKW = -326;
        public static readonly int HDN_DIVIDERDBLCLICK;
        public const int HDN_DIVIDERDBLCLICKA = -305;
        public const int HDN_DIVIDERDBLCLICKW = -325;
        public static readonly int HDN_ENDTRACK;
        public const int HDN_ENDTRACKA = -307;
        public const int HDN_ENDTRACKW = -327;
        public static readonly int HDN_GETDISPINFO;
        public const int HDN_GETDISPINFOA = -309;
        public const int HDN_GETDISPINFOW = -329;
        public static readonly int HDN_ITEMCHANGED;
        public const int HDN_ITEMCHANGEDA = -301;
        public const int HDN_ITEMCHANGEDW = -321;
        public static readonly int HDN_ITEMCHANGING;
        public const int HDN_ITEMCHANGINGA = -300;
        public const int HDN_ITEMCHANGINGW = -320;
        public static readonly int HDN_ITEMCLICK;
        public const int HDN_ITEMCLICKA = -302;
        public const int HDN_ITEMCLICKW = -322;
        public static readonly int HDN_ITEMDBLCLICK;
        public const int HDN_ITEMDBLCLICKA = -303;
        public const int HDN_ITEMDBLCLICKW = -323;
        public static readonly int HDN_TRACK;
        public const int HDN_TRACKA = -308;
        public const int HDN_TRACKW = -328;
        public const int HELPINFO_WINDOW = 1;
        public static readonly int HH_FTS_DEFAULT_PROXIMITY;
        public const int HOLLOW_BRUSH = 5;
        public const int HTCLIENT = 1;
        public const int HTNOWHERE = 0;
        public static HandleRef HWND_BOTTOM;
        public static HandleRef HWND_NOTOPMOST;
        public static HandleRef HWND_TOP;
        public static HandleRef HWND_TOPMOST;
        public const int ICC_BAR_CLASSES = 4;
        public const int ICC_DATE_CLASSES = 0x100;
        public const int ICC_LISTVIEW_CLASSES = 1;
        public const int ICC_PROGRESS_CLASS = 0x20;
        public const int ICC_TAB_CLASSES = 8;
        public const int ICC_TREEVIEW_CLASSES = 2;
        public const int ICON_BIG = 1;
        public const int ICON_SMALL = 0;
        public const int IDC_APPSTARTING = 32650;
        public const int IDC_ARROW = 0x7f00;
        public const int IDC_CROSS = 0x7f03;
        public const int IDC_HELP = 0x7f8b;
        public const int IDC_IBEAM = 0x7f01;
        public const int IDC_NO = 0x7f88;
        public const int IDC_SIZEALL = 0x7f86;
        public const int IDC_SIZENESW = 0x7f83;
        public const int IDC_SIZENS = 0x7f85;
        public const int IDC_SIZENWSE = 0x7f82;
        public const int IDC_SIZEWE = 0x7f84;
        public const int IDC_UPARROW = 0x7f04;
        public const int IDC_WAIT = 0x7f02;
        public const int ILC_COLOR = 0;
        public const int ILC_COLOR16 = 0x10;
        public const int ILC_COLOR24 = 0x18;
        public const int ILC_COLOR32 = 0x20;
        public const int ILC_COLOR4 = 4;
        public const int ILC_COLOR8 = 8;
        public const int ILC_MASK = 1;
        public const int ILD_MASK = 0x10;
        public const int ILD_NORMAL = 0;
        public const int ILD_ROP = 0x40;
        public const int ILD_TRANSPARENT = 1;
        public const int IMAGE_CURSOR = 2;
        public const int IMAGE_ICON = 1;
        public const int IME_CMODE_FULLSHAPE = 8;
        public const int IME_CMODE_KATAKANA = 2;
        public const int IME_CMODE_NATIVE = 1;
        public const int INPLACE_E_NOTOOLSPACE = -2147221087;
        public static IntPtr InvalidIntPtr;
        public const int KEYEVENTF_KEYUP = 2;
        public const int LANG_NEUTRAL = 0;
        public static readonly int LANG_USER_DEFAULT;
        public const int LB_ADDSTRING = 0x180;
        public const int LB_DELETESTRING = 0x182;
        public const int LB_ERR = -1;
        public const int LB_ERRSPACE = -2;
        public const int LB_FINDSTRING = 0x18f;
        public const int LB_FINDSTRINGEXACT = 0x1a2;
        public const int LB_GETCARETINDEX = 0x19f;
        public const int LB_GETCURSEL = 0x188;
        public const int LB_GETITEMHEIGHT = 0x1a1;
        public const int LB_GETITEMRECT = 0x198;
        public const int LB_GETSEL = 0x187;
        public const int LB_GETSELCOUNT = 400;
        public const int LB_GETSELITEMS = 0x191;
        public const int LB_GETTEXT = 0x189;
        public const int LB_GETTEXTLEN = 0x18a;
        public const int LB_GETTOPINDEX = 0x18e;
        public const int LB_INSERTSTRING = 0x181;
        public const int LB_ITEMFROMPOINT = 0x1a9;
        public const int LB_RESETCONTENT = 0x184;
        public const int LB_SETCOLUMNWIDTH = 0x195;
        public const int LB_SETCURSEL = 390;
        public const int LB_SETHORIZONTALEXTENT = 0x194;
        public const int LB_SETITEMHEIGHT = 0x1a0;
        public const int LB_SETLOCALE = 0x1a5;
        public const int LB_SETSEL = 0x185;
        public const int LB_SETTOPINDEX = 0x197;
        public const int LBN_DBLCLK = 2;
        public const int LBN_SELCHANGE = 1;
        public const int LBS_DISABLENOSCROLL = 0x1000;
        public const int LBS_EXTENDEDSEL = 0x800;
        public const int LBS_HASSTRINGS = 0x40;
        public const int LBS_MULTICOLUMN = 0x200;
        public const int LBS_MULTIPLESEL = 8;
        public const int LBS_NOINTEGRALHEIGHT = 0x100;
        public const int LBS_NOSEL = 0x4000;
        public const int LBS_NOTIFY = 1;
        public const int LBS_OWNERDRAWFIXED = 0x10;
        public const int LBS_OWNERDRAWVARIABLE = 0x20;
        public const int LBS_USETABSTOPS = 0x80;
        public const int LBS_WANTKEYBOARDINPUT = 0x400;
        public const int LOCALE_IFIRSTDAYOFWEEK = 0x100c;
        public static readonly int LOCALE_USER_DEFAULT;
        public const int LOCK_EXCLUSIVE = 2;
        public const int LOCK_ONLYONCE = 4;
        public const int LOCK_WRITE = 1;
        public const int LOGPIXELSX = 0x58;
        public const int LOGPIXELSY = 90;
        public const int LVA_ALIGNLEFT = 1;
        public const int LVA_ALIGNTOP = 2;
        public const int LVA_DEFAULT = 0;
        public const int LVA_SNAPTOGRID = 5;
        public const int LVCF_FMT = 1;
        public const int LVCF_IMAGE = 0x10;
        public const int LVCF_ORDER = 0x20;
        public const int LVCF_TEXT = 4;
        public const int LVCF_WIDTH = 2;
        public const int LVFI_PARAM = 1;
        public const int LVHT_ONITEM = 14;
        public const int LVHT_ONITEMSTATEICON = 8;
        public const int LVIF_IMAGE = 2;
        public const int LVIF_PARAM = 4;
        public const int LVIF_STATE = 8;
        public const int LVIF_TEXT = 1;
        public const int LVIR_BOUNDS = 0;
        public const int LVIR_ICON = 1;
        public const int LVIR_LABEL = 2;
        public const int LVIR_SELECTBOUNDS = 3;
        public const int LVIS_CUT = 4;
        public const int LVIS_DROPHILITED = 8;
        public const int LVIS_FOCUSED = 1;
        public const int LVIS_OVERLAYMASK = 3840;
        public const int LVIS_SELECTED = 2;
        public const int LVIS_STATEIMAGEMASK = 61440;
        public const int LVM_ARRANGE = 0x1016;
        public const int LVM_DELETEALLITEMS = 0x1009;
        public const int LVM_DELETECOLUMN = 0x101c;
        public const int LVM_DELETEITEM = 0x1008;
        public static readonly int LVM_EDITLABEL;
        public const int LVM_EDITLABELA = 0x1017;
        public const int LVM_EDITLABELW = 0x1076;
        public const int LVM_ENSUREVISIBLE = 0x1013;
        public static readonly int LVM_FINDITEM;
        public const int LVM_FINDITEMA = 0x100d;
        public const int LVM_FINDITEMW = 0x1053;
        public static readonly int LVM_GETCOLUMN;
        public const int LVM_GETCOLUMNA = 0x1019;
        public const int LVM_GETCOLUMNW = 0x105f;
        public const int LVM_GETCOLUMNWIDTH = 0x101d;
        public const int LVM_GETHEADER = 0x101f;
        public const int LVM_GETHOTITEM = 0x103d;
        public static readonly int LVM_GETISEARCHSTRING;
        public const int LVM_GETISEARCHSTRINGA = 0x1034;
        public const int LVM_GETISEARCHSTRINGW = 0x1075;
        public static readonly int LVM_GETITEM;
        public const int LVM_GETITEMA = 0x1005;
        public const int LVM_GETITEMRECT = 4110;
        public const int LVM_GETITEMSTATE = 4140;
        public static readonly int LVM_GETITEMTEXT;
        public const int LVM_GETITEMTEXTA = 0x102d;
        public const int LVM_GETITEMTEXTW = 0x1073;
        public const int LVM_GETITEMW = 0x104b;
        public const int LVM_GETNEXTITEM = 0x100c;
        public const int LVM_GETSELECTEDCOUNT = 0x1032;
        public static readonly int LVM_GETSTRINGWIDTH;
        public const int LVM_GETSTRINGWIDTHA = 0x1011;
        public const int LVM_GETSTRINGWIDTHW = 0x1057;
        public const int LVM_GETTOPINDEX = 0x1027;
        public const int LVM_HITTEST = 0x1012;
        public static readonly int LVM_INSERTCOLUMN;
        public const int LVM_INSERTCOLUMNA = 0x101b;
        public const int LVM_INSERTCOLUMNW = 0x1061;
        public static readonly int LVM_INSERTITEM;
        public const int LVM_INSERTITEMA = 0x1007;
        public const int LVM_INSERTITEMW = 0x104d;
        public const int LVM_SETBKCOLOR = 0x1001;
        public static readonly int LVM_SETCOLUMN;
        public const int LVM_SETCOLUMNA = 0x101a;
        public const int LVM_SETCOLUMNW = 0x1060;
        public const int LVM_SETCOLUMNWIDTH = 0x101e;
        public const int LVM_SETEXTENDEDLISTVIEWSTYLE = 4150;
        public const int LVM_SETIMAGELIST = 0x1003;
        public static readonly int LVM_SETITEM;
        public const int LVM_SETITEMA = 0x1006;
        public const int LVM_SETITEMCOUNT = 0x102f;
        public const int LVM_SETITEMSTATE = 0x102b;
        public static readonly int LVM_SETITEMTEXT;
        public const int LVM_SETITEMTEXTA = 0x102e;
        public const int LVM_SETITEMTEXTW = 0x1074;
        public const int LVM_SETITEMW = 0x104c;
        public const int LVM_SETTEXTBKCOLOR = 0x1026;
        public const int LVM_SETTEXTCOLOR = 0x1024;
        public const int LVM_SORTITEMS = 0x1030;
        public const int LVN_BEGINDRAG = -109;
        public static readonly int LVN_BEGINLABELEDIT;
        public const int LVN_BEGINLABELEDITA = -105;
        public const int LVN_BEGINLABELEDITW = -175;
        public const int LVN_BEGINRDRAG = -111;
        public const int LVN_COLUMNCLICK = -108;
        public static readonly int LVN_ENDLABELEDIT;
        public const int LVN_ENDLABELEDITA = -106;
        public const int LVN_ENDLABELEDITW = -176;
        public static readonly int LVN_GETDISPINFO;
        public const int LVN_GETDISPINFOA = -150;
        public const int LVN_GETDISPINFOW = -177;
        public const int LVN_ITEMACTIVATE = -114;
        public const int LVN_ITEMCHANGED = -101;
        public const int LVN_ITEMCHANGING = -100;
        public const int LVN_KEYDOWN = -155;
        public static readonly int LVN_ODFINDITEM;
        public const int LVN_ODFINDITEMA = -152;
        public const int LVN_ODFINDITEMW = -179;
        public static readonly int LVN_SETDISPINFO;
        public const int LVN_SETDISPINFOA = -151;
        public const int LVN_SETDISPINFOW = -178;
        public const int LVNI_FOCUSED = 1;
        public const int LVNI_SELECTED = 2;
        public const int LVS_ALIGNLEFT = 0x800;
        public const int LVS_ALIGNTOP = 0;
        public const int LVS_AUTOARRANGE = 0x100;
        public const int LVS_EDITLABELS = 0x200;
        public const int LVS_EX_CHECKBOXES = 4;
        public const int LVS_EX_FULLROWSELECT = 0x20;
        public const int LVS_EX_GRIDLINES = 1;
        public const int LVS_EX_HEADERDRAGDROP = 0x10;
        public const int LVS_EX_ONECLICKACTIVATE = 0x40;
        public const int LVS_EX_TRACKSELECT = 8;
        public const int LVS_EX_TWOCLICKACTIVATE = 0x80;
        public const int LVS_ICON = 0;
        public const int LVS_LIST = 3;
        public const int LVS_NOCOLUMNHEADER = 0x4000;
        public const int LVS_NOLABELWRAP = 0x80;
        public const int LVS_NOSCROLL = 0x2000;
        public const int LVS_NOSORTHEADER = 0x8000;
        public const int LVS_REPORT = 1;
        public const int LVS_SHAREIMAGELISTS = 0x40;
        public const int LVS_SHOWSELALWAYS = 8;
        public const int LVS_SINGLESEL = 4;
        public const int LVS_SMALLICON = 2;
        public const int LVS_SORTASCENDING = 0x10;
        public const int LVS_SORTDESCENDING = 0x20;
        public const int LVSIL_NORMAL = 0;
        public const int LVSIL_SMALL = 1;
        public const int LVSIL_STATE = 2;
        public const int LWA_ALPHA = 2;
        public const int LWA_COLORKEY = 1;
        public const int MAX_PATH = 260;
        public const int MB_OK = 0;
        public const int MCHT_CALENDAR = 0x20000;
        public const int MCHT_CALENDARBK = 0x20000;
        public const int MCHT_CALENDARDATE = 0x20001;
        public const int MCHT_CALENDARDATENEXT = 0x1020001;
        public const int MCHT_CALENDARDATEPREV = 0x2020001;
        public const int MCHT_CALENDARDAY = 0x20002;
        public const int MCHT_CALENDARWEEKNUM = 0x20003;
        public const int MCHT_TITLE = 0x10000;
        public const int MCHT_TITLEBK = 0x10000;
        public const int MCHT_TITLEBTNNEXT = 0x1010003;
        public const int MCHT_TITLEBTNPREV = 0x2010003;
        public const int MCHT_TITLEMONTH = 0x10001;
        public const int MCHT_TITLEYEAR = 0x10002;
        public const int MCHT_TODAYLINK = 0x30000;
        public const int MCM_GETMAXTODAYWIDTH = 0x1015;
        public const int MCM_GETMINREQRECT = 0x1009;
        public const int MCM_GETMONTHRANGE = 0x1007;
        public const int MCM_GETTODAY = 0x100d;
        public const int MCM_HITTEST = 4110;
        public const int MCM_SETCOLOR = 0x100a;
        public const int MCM_SETFIRSTDAYOFWEEK = 0x100f;
        public const int MCM_SETMAXSELCOUNT = 4100;
        public const int MCM_SETMONTHDELTA = 0x1014;
        public const int MCM_SETRANGE = 0x1012;
        public const int MCM_SETSELRANGE = 0x1006;
        public const int MCM_SETTODAY = 0x100c;
        public const int MCN_GETDAYSTATE = -747;
        public const int MCN_SELCHANGE = -749;
        public const int MCN_SELECT = -746;
        public const int MCS_DAYSTATE = 1;
        public const int MCS_MULTISELECT = 2;
        public const int MCS_NOTODAY = 0x10;
        public const int MCS_NOTODAYCIRCLE = 8;
        public const int MCS_WEEKNUMBERS = 4;
        public const int MCSC_MONTHBK = 4;
        public const int MCSC_TEXT = 1;
        public const int MCSC_TITLEBK = 2;
        public const int MCSC_TITLETEXT = 3;
        public const int MCSC_TRAILINGTEXT = 5;
        public const int MDITILE_HORIZONTAL = 1;
        public const int MDITILE_VERTICAL = 0;
        public const int MEMBERID_NIL = -1;
        public const int MF_BYCOMMAND = 0;
        public const int MF_BYPOSITION = 0x400;
        public const int MF_ENABLED = 0;
        public const int MF_GRAYED = 1;
        public const int MF_POPUP = 0x10;
        public const int MF_SYSMENU = 0x2000;
        public const int MFT_MENUBREAK = 0x40;
        public const int MFT_RIGHTJUSTIFY = 0x4000;
        public const int MFT_RIGHTORDER = 0x2000;
        public const int MFT_SEPARATOR = 0x800;
        public const int MIIM_DATA = 0x20;
        public const int MIIM_ID = 2;
        public const int MIIM_STATE = 1;
        public const int MIIM_SUBMENU = 4;
        public const int MIIM_TYPE = 0x10;
        public const int MK_CONTROL = 8;
        public const int MK_LBUTTON = 1;
        public const int MK_MBUTTON = 0x10;
        public const int MK_RBUTTON = 2;
        public const int MK_SHIFT = 4;
        public const int MM_ANISOTROPIC = 8;
        public const int MM_TEXT = 1;
        public const int MNC_EXECUTE = 2;
        public const string MOUSEZ_CLASSNAME = "MouseZ";
        public const string MOUSEZ_TITLE = "Magellan MSWHEEL";
        public const string MSH_MOUSEWHEEL = "MSWHEEL_ROLLMSG";
        public const string MSH_SCROLL_LINES = "MSH_SCROLL_LINES_MSG";
        public const int NFR_ANSI = 1;
        public const int NFR_UNICODE = 2;
        public const int NIF_ICON = 2;
        public const int NIF_MESSAGE = 1;
        public const int NIF_TIP = 4;
        public const int NIM_ADD = 0;
        public const int NIM_DELETE = 2;
        public const int NIM_MODIFY = 1;
        public const int NM_CLICK = -2;
        public const int NM_CUSTOMDRAW = -12;
        public const int NM_DBLCLK = -3;
        public const int NM_RCLICK = -5;
        public const int NM_RDBLCLK = -6;
        public const int NM_RELEASEDCAPTURE = -16;
        public static HandleRef NullHandleRef;
        public const int OBJ_BITMAP = 7;
        public const int OBJ_BRUSH = 2;
        public const int OBJ_DC = 3;
        public const int OBJ_ENHMETADC = 12;
        public const int OBJ_EXTPEN = 11;
        public const int OBJ_FONT = 6;
        public const int OBJ_MEMDC = 10;
        public const int OBJ_METADC = 4;
        public const int OBJ_METAFILE = 9;
        public const int OBJ_PAL = 5;
        public const int OBJ_PEN = 1;
        public const int OBJ_REGION = 8;
        public const int OBJID_CLIENT = -4;
        public const int ODS_CHECKED = 8;
        public const int ODS_COMBOBOXEDIT = 0x1000;
        public const int ODS_DEFAULT = 0x20;
        public const int ODS_DISABLED = 4;
        public const int ODS_FOCUS = 0x10;
        public const int ODS_GRAYED = 2;
        public const int ODS_HOTLIGHT = 0x40;
        public const int ODS_INACTIVE = 0x80;
        public const int ODS_NOACCEL = 0x100;
        public const int ODS_NOFOCUSRECT = 0x200;
        public const int ODS_SELECTED = 1;
        public const int OFN_ALLOWMULTISELECT = 0x200;
        public const int OFN_CREATEPROMPT = 0x2000;
        public const int OFN_ENABLEHOOK = 0x20;
        public const int OFN_ENABLESIZING = 0x800000;
        public const int OFN_EXPLORER = 0x80000;
        public const int OFN_FILEMUSTEXIST = 0x1000;
        public const int OFN_HIDEREADONLY = 4;
        public const int OFN_NOCHANGEDIR = 8;
        public const int OFN_NODEREFERENCELINKS = 0x100000;
        public const int OFN_NOVALIDATE = 0x100;
        public const int OFN_OVERWRITEPROMPT = 2;
        public const int OFN_PATHMUSTEXIST = 0x800;
        public const int OFN_READONLY = 1;
        public const int OFN_SHOWHELP = 0x10;
        public const int OFN_USESHELLITEM = 0x1000000;
        public const int OLE_E_NOCONNECTION = -2147221500;
        public const int OLE_E_PROMPTSAVECANCELLED = -2147221492;
        public const int OLECLOSE_PROMPTSAVE = 2;
        public const int OLECLOSE_SAVEIFDIRTY = 0;
        public const int OLEIVERB_HIDE = -3;
        public const int OLEIVERB_INPLACEACTIVATE = -5;
        public const int OLEIVERB_PRIMARY = 0;
        public const int OLEIVERB_PROPERTIES = -7;
        public const int OLEIVERB_SHOW = -1;
        public const int OLEIVERB_UIACTIVATE = -4;
        public const int OLEMISC_ACTIVATEWHENVISIBLE = 0x100;
        public const int OLEMISC_ACTSLIKEBUTTON = 0x1000;
        public const int OLEMISC_INSIDEOUT = 0x80;
        public const int OLEMISC_RECOMPOSEONRESIZE = 1;
        public const int OLEMISC_SETCLIENTSITEFIRST = 0x20000;
        public const int PBM_SETPOS = 0x402;
        public const int PBM_SETRANGE = 0x401;
        public const int PBM_SETRANGE32 = 1030;
        public const int PBM_SETSTEP = 0x404;
        public const int PD_COLLATE = 0x10;
        public const int PD_DISABLEPRINTTOFILE = 0x80000;
        public const int PD_ENABLEPRINTHOOK = 0x1000;
        public const int PD_NOCURRENTPAGE = 0x800000;
        public const int PD_NONETWORKBUTTON = 0x200000;
        public const int PD_NOPAGENUMS = 8;
        public const int PD_NOSELECTION = 4;
        public const int PD_PRINTTOFILE = 0x20;
        public const int PD_SHOWHELP = 0x800;
        public const int PDERR_CREATEICFAILURE = 0x100a;
        public const int PDERR_DEFAULTDIFFERENT = 0x100c;
        public const int PDERR_DNDMMISMATCH = 0x1009;
        public const int PDERR_GETDEVMODEFAIL = 0x1005;
        public const int PDERR_INITFAILURE = 0x1006;
        public const int PDERR_LOADDRVFAILURE = 4100;
        public const int PDERR_NODEFAULTPRN = 0x1008;
        public const int PDERR_NODEVICES = 0x1007;
        public const int PDERR_PARSEFAILURE = 0x1002;
        public const int PDERR_PRINTERNOTFOUND = 0x100b;
        public const int PDERR_RETDEFFAILURE = 0x1003;
        public const int PDERR_SETUPFAILURE = 0x1001;
        public const int PLANES = 14;
        public const int PM_NOREMOVE = 0;
        public const int PM_NOYIELD = 2;
        public const int PM_REMOVE = 1;
        public const int PRF_CHECKVISIBLE = 1;
        public const int PRF_CHILDREN = 0x10;
        public const int PRF_CLIENT = 4;
        public const int PRF_ERASEBKGND = 8;
        public const int PRF_NONCLIENT = 2;
        public const int PS_DOT = 2;
        public const int PS_SOLID = 0;
        public const int PSD_DISABLEMARGINS = 0x10;
        public const int PSD_DISABLEORIENTATION = 0x100;
        public const int PSD_DISABLEPAPER = 0x200;
        public const int PSD_DISABLEPRINTER = 0x20;
        public const int PSD_ENABLEPAGESETUPHOOK = 0x2000;
        public const int PSD_INHUNDREDTHSOFMILLIMETERS = 8;
        public const int PSD_MARGINS = 2;
        public const int PSD_MINMARGINS = 1;
        public const int PSD_NONETWORKBUTTON = 0x200000;
        public const int PSD_SHOWHELP = 0x800;
        public static readonly int PSM_SETFINISHTEXT;
        public const int PSM_SETFINISHTEXTA = 0x473;
        public const int PSM_SETFINISHTEXTW = 0x479;
        public static readonly int PSM_SETTITLE;
        public const int PSM_SETTITLEA = 0x46f;
        public const int PSM_SETTITLEW = 0x478;
        public const int QS_ALLEVENTS = 0xbf;
        public const int QS_ALLINPUT = 0xff;
        public const int QS_ALLPOSTMESSAGE = 0x100;
        public const int QS_HOTKEY = 0x80;
        public const int QS_INPUT = 7;
        public const int QS_KEY = 1;
        public const int QS_MOUSE = 6;
        public const int QS_MOUSEBUTTON = 4;
        public const int QS_MOUSEMOVE = 2;
        public const int QS_PAINT = 0x20;
        public const int QS_POSTMESSAGE = 8;
        public const int QS_SENDMESSAGE = 0x40;
        public const int QS_TIMER = 0x10;
        public static readonly int RB_INSERTBAND;
        public const int RB_INSERTBANDA = 0x401;
        public const int RB_INSERTBANDW = 0x40a;
        public const int RDW_ALLCHILDREN = 0x80;
        public const int RDW_ERASE = 4;
        public const int RDW_FRAME = 0x400;
        public const int RDW_INVALIDATE = 1;
        public const int RECO_DROP = 1;
        public const int RGN_DIFF = 4;
        public const int RPC_E_CHANGED_MODE = -2147417850;
        public const int S_FALSE = 1;
        public const int S_OK = 0;
        public const int SB_BOTTOM = 7;
        public const int SB_CTL = 2;
        public const int SB_ENDSCROLL = 8;
        public const int SB_GETRECT = 0x40a;
        public static readonly int SB_GETTEXT;
        public const int SB_GETTEXTA = 0x402;
        public static readonly int SB_GETTEXTLENGTH;
        public const int SB_GETTEXTLENGTHA = 0x403;
        public const int SB_GETTEXTLENGTHW = 0x40c;
        public const int SB_GETTEXTW = 0x40d;
        public static readonly int SB_GETTIPTEXT;
        public const int SB_GETTIPTEXTA = 0x412;
        public const int SB_GETTIPTEXTW = 0x413;
        public const int SB_HORZ = 0;
        public const int SB_LEFT = 6;
        public const int SB_LINEDOWN = 1;
        public const int SB_LINELEFT = 0;
        public const int SB_LINERIGHT = 1;
        public const int SB_LINEUP = 0;
        public const int SB_PAGEDOWN = 3;
        public const int SB_PAGELEFT = 2;
        public const int SB_PAGERIGHT = 3;
        public const int SB_PAGEUP = 2;
        public const int SB_RIGHT = 7;
        public const int SB_SETICON = 0x40f;
        public const int SB_SETPARTS = 0x404;
        public static readonly int SB_SETTEXT;
        public const int SB_SETTEXTA = 0x401;
        public const int SB_SETTEXTW = 0x40b;
        public static readonly int SB_SETTIPTEXT;
        public const int SB_SETTIPTEXTA = 1040;
        public const int SB_SETTIPTEXTW = 0x411;
        public const int SB_SIMPLE = 0x409;
        public const int SB_THUMBPOSITION = 4;
        public const int SB_THUMBTRACK = 5;
        public const int SB_TOP = 6;
        public const int SB_VERT = 1;
        public const int SBARS_SIZEGRIP = 0x100;
        public const int SBS_HORZ = 0;
        public const int SBS_VERT = 1;
        public const int SBT_NOBORDERS = 0x100;
        public const int SBT_OWNERDRAW = 0x1000;
        public const int SBT_POPOUT = 0x200;
        public const int SBT_RTLREADING = 0x400;
        public const int SC_CLOSE = 0xf060;
        public const int SC_KEYMENU = 0xf100;
        public const int SC_MAXIMIZE = 0xf030;
        public const int SC_MINIMIZE = 0xf020;
        public const int SC_RESTORE = 0xf120;
        public const int SC_SIZE = 61440;
        public const int SHGFP_TYPE_CURRENT = 0;
        public const int SIF_ALL = 0x17;
        public const int SIF_PAGE = 2;
        public const int SIF_POS = 4;
        public const int SIF_RANGE = 1;
        public const int SIF_TRACKPOS = 0x10;
        public const int SM_ARRANGE = 0x38;
        public const int SM_CLEANBOOT = 0x43;
        public const int SM_CMONITORS = 80;
        public const int SM_CMOUSEBUTTONS = 0x2b;
        public const int SM_CXBORDER = 5;
        public const int SM_CXCURSOR = 13;
        public const int SM_CXDOUBLECLK = 0x24;
        public const int SM_CXDRAG = 0x44;
        public const int SM_CXEDGE = 0x2d;
        public const int SM_CXFIXEDFRAME = 7;
        public const int SM_CXFRAME = 0x20;
        public const int SM_CXHSCROLL = 0x15;
        public const int SM_CXHTHUMB = 10;
        public const int SM_CXICON = 11;
        public const int SM_CXICONSPACING = 0x26;
        public const int SM_CXMAXIMIZED = 0x3d;
        public const int SM_CXMAXTRACK = 0x3b;
        public const int SM_CXMENUCHECK = 0x47;
        public const int SM_CXMENUSIZE = 0x36;
        public const int SM_CXMIN = 0x1c;
        public const int SM_CXMINIMIZED = 0x39;
        public const int SM_CXMINSPACING = 0x2f;
        public const int SM_CXMINTRACK = 0x22;
        public const int SM_CXSCREEN = 0;
        public const int SM_CXSIZE = 30;
        public const int SM_CXSMICON = 0x31;
        public const int SM_CXSMSIZE = 0x34;
        public const int SM_CXVIRTUALSCREEN = 0x4e;
        public const int SM_CXVSCROLL = 2;
        public const int SM_CYBORDER = 6;
        public const int SM_CYCAPTION = 4;
        public const int SM_CYCURSOR = 14;
        public const int SM_CYDOUBLECLK = 0x25;
        public const int SM_CYDRAG = 0x45;
        public const int SM_CYEDGE = 0x2e;
        public const int SM_CYFIXEDFRAME = 8;
        public const int SM_CYFRAME = 0x21;
        public const int SM_CYHSCROLL = 3;
        public const int SM_CYICON = 12;
        public const int SM_CYICONSPACING = 0x27;
        public const int SM_CYKANJIWINDOW = 0x12;
        public const int SM_CYMAXIMIZED = 0x3e;
        public const int SM_CYMAXTRACK = 60;
        public const int SM_CYMENU = 15;
        public const int SM_CYMENUCHECK = 0x48;
        public const int SM_CYMENUSIZE = 0x37;
        public const int SM_CYMIN = 0x1d;
        public const int SM_CYMINIMIZED = 0x3a;
        public const int SM_CYMINSPACING = 0x30;
        public const int SM_CYMINTRACK = 0x23;
        public const int SM_CYSCREEN = 1;
        public const int SM_CYSIZE = 0x1f;
        public const int SM_CYSMCAPTION = 0x33;
        public const int SM_CYSMICON = 50;
        public const int SM_CYSMSIZE = 0x35;
        public const int SM_CYVIRTUALSCREEN = 0x4f;
        public const int SM_CYVSCROLL = 20;
        public const int SM_CYVTHUMB = 9;
        public const int SM_DBCSENABLED = 0x2a;
        public const int SM_DEBUG = 0x16;
        public const int SM_MENUDROPALIGNMENT = 40;
        public const int SM_MIDEASTENABLED = 0x4a;
        public const int SM_MOUSEPRESENT = 0x13;
        public const int SM_MOUSEWHEELPRESENT = 0x4b;
        public const int SM_NETWORK = 0x3f;
        public const int SM_PENWINDOWS = 0x29;
        public const int SM_REMOTESESSION = 0x1000;
        public const int SM_SAMEDISPLAYFORMAT = 0x51;
        public const int SM_SECURE = 0x2c;
        public const int SM_SHOWSOUNDS = 70;
        public const int SM_SWAPBUTTON = 0x17;
        public const int SM_XVIRTUALSCREEN = 0x4c;
        public const int SM_YVIRTUALSCREEN = 0x4d;
        public const int SORT_DEFAULT = 0;
        public const int SPI_GETDEFAULTINPUTLANG = 0x59;
        public const int SPI_GETDRAGFULLWINDOWS = 0x26;
        public const int SPI_GETHIGHCONTRAST = 0x42;
        public const int SPI_GETNONCLIENTMETRICS = 0x29;
        public const int SPI_GETSNAPTODEFBUTTON = 0x5f;
        public const int SPI_GETWHEELSCROLLLINES = 0x68;
        public const int SPI_GETWORKAREA = 0x30;
        public const int SS_CENTER = 1;
        public const int SS_LEFT = 0;
        public const int SS_NOPREFIX = 0x80;
        public const int SS_OWNERDRAW = 13;
        public const int SS_RIGHT = 2;
        public const int SS_SUNKEN = 0x1000;
        public const int STARTF_USESHOWWINDOW = 1;
        public const int STATFLAG_DEFAULT = 0;
        public const int STATFLAG_NONAME = 1;
        public const int STATFLAG_NOOPEN = 2;
        public const int stc4 = 0x443;
        public const int STGC_DANGEROUSLYCOMMITMERELYTODISKCACHE = 4;
        public const int STGC_DEFAULT = 0;
        public const int STGC_ONLYIFCURRENT = 2;
        public const int STGC_OVERWRITE = 1;
        public const int STGM_CREATE = 0x1000;
        public const int STGM_READ = 0;
        public const int STGM_READWRITE = 2;
        public const int STGM_SHARE_EXCLUSIVE = 0x10;
        public const int STGM_WRITE = 1;
        public const int STREAM_SEEK_CUR = 1;
        public const int STREAM_SEEK_END = 2;
        public const int STREAM_SEEK_SET = 0;
        public const int SUBLANG_DEFAULT = 1;
        public const int SW_ERASE = 4;
        public const int SW_HIDE = 0;
        public const int SW_INVALIDATE = 2;
        public const int SW_MAX = 10;
        public const int SW_MAXIMIZE = 3;
        public const int SW_MINIMIZE = 6;
        public const int SW_NORMAL = 1;
        public const int SW_RESTORE = 9;
        public const int SW_SCROLLCHILDREN = 1;
        public const int SW_SHOW = 5;
        public const int SW_SHOWMAXIMIZED = 3;
        public const int SW_SHOWMINIMIZED = 2;
        public const int SW_SHOWMINNOACTIVE = 7;
        public const int SW_SHOWNA = 8;
        public const int SW_SHOWNOACTIVATE = 4;
        public const int SWP_DRAWFRAME = 0x20;
        public const int SWP_HIDEWINDOW = 0x80;
        public const int SWP_NOACTIVATE = 0x10;
        public const int SWP_NOMOVE = 2;
        public const int SWP_NOSIZE = 1;
        public const int SWP_NOZORDER = 4;
        public const int SWP_SHOWWINDOW = 0x40;
        public static readonly int TB_ADDBUTTONS;
        public const int TB_ADDBUTTONSA = 0x414;
        public const int TB_ADDBUTTONSW = 0x444;
        public static readonly int TB_ADDSTRING;
        public const int TB_ADDSTRINGA = 0x41c;
        public const int TB_ADDSTRINGW = 0x44d;
        public const int TB_AUTOSIZE = 0x421;
        public const int TB_BOTTOM = 7;
        public const int TB_BUTTONSTRUCTSIZE = 0x41e;
        public const int TB_DELETEBUTTON = 0x416;
        public const int TB_ENABLEBUTTON = 0x401;
        public const int TB_ENDTRACK = 8;
        public const int TB_GETBUTTON = 0x417;
        public const int TB_BUTTONCOUNT = (WM_USER + 24);
        public static readonly int TB_GETBUTTONINFO;
        public const int TB_GETBUTTONINFOA = 0x441;
        public const int TB_GETBUTTONINFOW = 0x43f;
        public const int TB_GETBUTTONSIZE = 0x43a;
        public static readonly int TB_GETBUTTONTEXT;
        public const int TB_GETBUTTONTEXTA = 0x42d;
        public const int TB_GETBUTTONTEXTW = 0x44b;
        public const int TB_GETRECT = 0x433;
        public const int TB_GETROWS = 0x428;
        public static readonly int TB_INSERTBUTTON;
        public const int TB_INSERTBUTTONA = 0x415;
        public const int TB_INSERTBUTTONW = 0x443;
        public const int TB_ISBUTTONCHECKED = 0x40a;
        public const int TB_ISBUTTONINDETERMINATE = 0x40d;
        public const int TB_LINEDOWN = 1;
        public const int TB_LINEUP = 0;
        public static readonly int TB_MAPACCELERATOR;
        public const int TB_MAPACCELERATORA = 0x44e;
        public const int TB_MAPACCELERATORW = 0x45a;
        public const int TB_PAGEDOWN = 3;
        public const int TB_PAGEUP = 2;
        public static readonly int TB_SAVERESTORE;
        public const int TB_SAVERESTOREA = 1050;
        public const int TB_SAVERESTOREW = 1100;
        public static readonly int TB_SETBUTTONINFO;
        public const int TB_SETBUTTONINFOA = 1090;
        public const int TB_SETBUTTONINFOW = 0x440;
        public const int TB_SETBUTTONSIZE = 0x41f;
        public const int TB_SETEXTENDEDSTYLE = 0x454;
        public const int TB_SETIMAGELIST = 0x430;
        public const int TB_THUMBPOSITION = 4;
        public const int TB_THUMBTRACK = 5;
        public const int TB_TOP = 6;
        public const int TBIF_COMMAND = 0x20;
        public const int TBIF_IMAGE = 1;
        public const int TBIF_SIZE = 0x40;
        public const int TBIF_STATE = 4;
        public const int TBIF_STYLE = 8;
        public const int TBIF_TEXT = 2;
        public const int TBM_GETPOS = 0x400;
        public const int TBM_SETLINESIZE = 0x417;
        public const int TBM_SETPAGESIZE = 0x415;
        public const int TBM_SETPOS = 0x405;
        public const int TBM_SETRANGE = 1030;
        public const int TBM_SETRANGEMAX = 0x408;
        public const int TBM_SETRANGEMIN = 0x407;
        public const int TBM_SETTIC = 0x404;
        public const int TBM_SETTICFREQ = 0x414;
        public const int TBN_DROPDOWN = -710;
        public static readonly int TBN_GETBUTTONINFO;
        public const int TBN_GETBUTTONINFOA = -700;
        public const int TBN_GETBUTTONINFOW = -720;
        public static readonly int TBN_GETDISPINFO;
        public const int TBN_GETDISPINFOA = -716;
        public const int TBN_GETDISPINFOW = -717;
        public static readonly int TBN_GETINFOTIP;
        public const int TBN_GETINFOTIPA = -718;
        public const int TBN_GETINFOTIPW = -719;
        public const int TBN_QUERYINSERT = -706;
        public const int TBS_AUTOTICKS = 1;
        public const int TBS_BOTH = 8;
        public const int TBS_BOTTOM = 0;
        public const int TBS_NOTICKS = 0x10;
        public const int TBS_TOP = 4;
        public const int TBS_VERT = 2;
        public const int TBSTATE_CHECKED = 1;
        public const int TBSTATE_ENABLED = 4;
        public const int TBSTATE_HIDDEN = 8;
        public const int TBSTATE_INDETERMINATE = 0x10;
        public const int TBSTYLE_BUTTON = 0;
        public const int TBSTYLE_CHECK = 2;
        public const int TBSTYLE_DROPDOWN = 8;
        public const int TBSTYLE_EX_DRAWDDARROWS = 1;
        public const int TBSTYLE_FLAT = 0x800;
        public const int TBSTYLE_LIST = 0x1000;
        public const int TBSTYLE_SEP = 1;
        public const int TBSTYLE_TOOLTIPS = 0x100;
        public const int TBSTYLE_WRAPPABLE = 0x200;
        public const int TCIF_IMAGE = 2;
        public const int TCIF_TEXT = 1;
        public const int TCM_ADJUSTRECT = 0x1328;
        public const int TCM_DELETEALLITEMS = 0x1309;
        public const int TCM_DELETEITEM = 0x1308;
        public const int TCM_GETCURSEL = 0x130b;
        public static readonly int TCM_GETITEM;
        public const int TCM_GETITEMA = 0x1305;
        public const int TCM_GETITEMRECT = 0x130a;
        public const int TCM_GETITEMW = 0x133c;
        public const int TCM_GETROWCOUNT = 0x132c;
        public const int TCM_GETTOOLTIPS = 0x132d;
        public static readonly int TCM_INSERTITEM;
        public const int TCM_INSERTITEMA = 0x1307;
        public const int TCM_INSERTITEMW = 0x133e;
        public const int TCM_SETCURSEL = 0x130c;
        public const int TCM_SETIMAGELIST = 0x1303;
        public static readonly int TCM_SETITEM;
        public const int TCM_SETITEMA = 4870;
        public const int TCM_SETITEMSIZE = 0x1329;
        public const int TCM_SETITEMW = 0x133d;
        public const int TCM_SETPADDING = 0x132b;
        public const int TCN_SELCHANGE = -551;
        public const int TCN_SELCHANGING = -552;
        public const int TCS_BOTTOM = 2;
        public const int TCS_BUTTONS = 0x100;
        public const int TCS_FIXEDWIDTH = 0x400;
        public const int TCS_FLATBUTTONS = 8;
        public const int TCS_HOTTRACK = 0x40;
        public const int TCS_MULTILINE = 0x200;
        public const int TCS_OWNERDRAWFIXED = 0x2000;
        public const int TCS_RAGGEDRIGHT = 0x800;
        public const int TCS_RIGHT = 2;
        public const int TCS_RIGHTJUSTIFY = 0;
        public const int TCS_TABS = 0;
        public const int TCS_TOOLTIPS = 0x4000;
        public const int TCS_VERTICAL = 0x80;
        public const int TME_HOVER = 1;
        public const int TME_LEAVE = 2;
        public const string TOOLTIPS_CLASS = "tooltips_class32";
        public const int TPM_LEFTALIGN = 0;
        public const int TPM_LEFTBUTTON = 0;
        public const int TPM_VERTICAL = 0x40;
        public const int TRANSPARENT = 1;
        public const int TTDT_AUTOMATIC = 0;
        public const int TTDT_AUTOPOP = 2;
        public const int TTDT_INITIAL = 3;
        public const int TTDT_RESHOW = 1;
        public const int TTF_IDISHWND = 1;
        public const int TTF_RTLREADING = 4;
        public const int TTF_SUBCLASS = 0x10;
        public const int TTF_TRACK = 0x20;
        public const int TTF_TRANSPARENT = 0x100;
        public const int TTI_WARNING = 2;
        public const int TTM_ACTIVATE = 0x401;
        public static readonly int TTM_ADDTOOL;
        public const int TTM_ADDTOOLA = 0x404;
        public const int TTM_ADDTOOLW = 0x432;
        public const int TTM_ADJUSTRECT = 0x41f;
        public static readonly int TTM_DELTOOL;
        public const int TTM_DELTOOLA = 0x405;
        public const int TTM_DELTOOLW = 0x433;
        public static readonly int TTM_ENUMTOOLS;
        public const int TTM_ENUMTOOLSA = 0x40e;
        public const int TTM_ENUMTOOLSW = 0x43a;
        public static readonly int TTM_GETCURRENTTOOL;
        public const int TTM_GETCURRENTTOOLA = 0x40f;
        public const int TTM_GETCURRENTTOOLW = 0x43b;
        public const int TTM_GETDELAYTIME = 0x415;
        public static readonly int TTM_GETTEXT;
        public const int TTM_GETTEXTA = 0x40b;
        public const int TTM_GETTEXTW = 1080;
        public static readonly int TTM_GETTOOLINFO;
        public const int TTM_GETTOOLINFOA = 0x408;
        public const int TTM_GETTOOLINFOW = 0x435;
        public static readonly int TTM_HITTEST;
        public const int TTM_HITTESTA = 0x40a;
        public const int TTM_HITTESTW = 0x437;
        public static readonly int TTM_NEWTOOLRECT;
        public const int TTM_NEWTOOLRECTA = 1030;
        public const int TTM_NEWTOOLRECTW = 0x434;
        public const int TTM_RELAYEVENT = 0x407;
        public const int TTM_SETDELAYTIME = 0x403;
        public const int TTM_SETMAXTIPWIDTH = 0x418;
        public static readonly int TTM_SETTITLE;
        public const int TTM_SETTITLEA = 0x420;
        public const int TTM_SETTITLEW = 0x421;
        public static readonly int TTM_SETTOOLINFO;
        public const int TTM_SETTOOLINFOA = 0x409;
        public const int TTM_SETTOOLINFOW = 0x436;
        public const int TTM_TRACKACTIVATE = 0x411;
        public const int TTM_TRACKPOSITION = 0x412;
        public const int TTM_UPDATE = 0x41d;
        public static readonly int TTM_UPDATETIPTEXT;
        public const int TTM_UPDATETIPTEXTA = 0x40c;
        public const int TTM_UPDATETIPTEXTW = 0x439;
        public const int TTM_WINDOWFROMPOINT = 1040;
        public static readonly int TTN_GETDISPINFO;
        public const int TTN_GETDISPINFOA = -520;
        public const int TTN_GETDISPINFOW = -530;
        public const int TTM_GETMAXTIPWIDTH = (WM_USER + 25);
        public static readonly int TTN_NEEDTEXT;
        public const int TTN_NEEDTEXTA = -520;
        public const int TTN_NEEDTEXTW = -530;
        public const int TTN_POP = -522;
        public const int TTN_SHOW = -521;
        public const int TTS_ALWAYSTIP = 1;
        public const int TTS_BALLOON = 0x40;
        public const int TV_FIRST = 0x1100;
        public const int TVC_BYKEYBOARD = 2;
        public const int TVC_BYMOUSE = 1;
        public const int TVC_UNKNOWN = 0;
        public const int TVE_COLLAPSE = 1;
        public const int TVE_EXPAND = 2;
        public const int TVGN_CARET = 9;
        public const int TVGN_FIRSTVISIBLE = 5;
        public const int TVGN_NEXT = 1;
        public const int TVGN_NEXTVISIBLE = 6;
        public const int TVGN_PREVIOUS = 2;
        public const int TVGN_PREVIOUSVISIBLE = 7;
        public const int TVHT_ONITEM = 70;
        public const int TVHT_ONITEMSTATEICON = 0x40;
        public const int TVI_FIRST = -65535;
        public const int TVI_ROOT = -65536;
        public const int TVIF_HANDLE = 0x10;
        public const int TVIF_IMAGE = 2;
        public const int TVIF_SELECTEDIMAGE = 0x20;
        public const int TVIF_STATE = 8;
        public const int TVIF_TEXT = 1;
        public const int TVIS_EXPANDED = 0x20;
        public const int TVIS_EXPANDEDONCE = 0x40;
        public const int TVIS_SELECTED = 2;
        public const int TVIS_STATEIMAGEMASK = 61440;
        public const int TVM_DELETEITEM = 0x1101;
        public static readonly int TVM_EDITLABEL;
        public const int TVM_EDITLABELA = 0x110e;
        public const int TVM_EDITLABELW = 0x1141;
        public const int TVM_ENDEDITLABELNOW = 0x1116;
        public const int TVM_ENSUREVISIBLE = 0x1114;
        public const int TVM_EXPAND = 0x1102;
        public const int TVM_GETEDITCONTROL = 0x110f;
        public const int TVM_GETINDENT = 0x1106;
        public static readonly int TVM_GETISEARCHSTRING;
        public const int TVM_GETISEARCHSTRINGA = 0x1117;
        public const int TVM_GETISEARCHSTRINGW = 0x1140;
        public static readonly int TVM_GETITEM;
        public const int TVM_GETITEMA = 0x110c;
        public const int TVM_GETITEMHEIGHT = 4380;
        public const int TVM_GETITEMRECT = 0x1104;
        public const int TVM_GETITEMW = 0x113e;
        public const int TVM_GETNEXTITEM = 0x110a;
        public const int TVM_GETVISIBLECOUNT = 0x1110;
        public const int TVM_HITTEST = 0x1111;
        public static readonly int TVM_INSERTITEM;
        public const int TVM_INSERTITEMA = 0x1100;
        public const int TVM_INSERTITEMW = 0x1132;
        public const int TVM_SELECTITEM = 0x110b;
        public const int TVM_SETBKCOLOR = 0x111d;
        public const int TVM_SETIMAGELIST = 0x1109;
        public const int TVM_SETINDENT = 0x1107;
        public static readonly int TVM_SETITEM;
        public const int TVM_SETITEMA = 0x110d;
        public const int TVM_SETITEMHEIGHT = 0x111b;
        public const int TVM_SETITEMW = 0x113f;
        public const int TVM_SETTEXTCOLOR = 0x111e;
        public static readonly int TVN_BEGINDRAG;
        public const int TVN_BEGINDRAGA = -407;
        public const int TVN_BEGINDRAGW = -456;
        public static readonly int TVN_BEGINLABELEDIT;
        public const int TVN_BEGINLABELEDITA = -410;
        public const int TVN_BEGINLABELEDITW = -459;
        public static readonly int TVN_BEGINRDRAG;
        public const int TVN_BEGINRDRAGA = -408;
        public const int TVN_BEGINRDRAGW = -457;
        public static readonly int TVN_ENDLABELEDIT;
        public const int TVN_ENDLABELEDITA = -411;
        public const int TVN_ENDLABELEDITW = -460;
        public static readonly int TVN_GETDISPINFO;
        public const int TVN_GETDISPINFOA = -403;
        public const int TVN_GETDISPINFOW = -452;
        public static readonly int TVN_ITEMEXPANDED;
        public const int TVN_ITEMEXPANDEDA = -406;
        public const int TVN_ITEMEXPANDEDW = -455;
        public static readonly int TVN_ITEMEXPANDING;
        public const int TVN_ITEMEXPANDINGA = -405;
        public const int TVN_ITEMEXPANDINGW = -454;
        public static readonly int TVN_SELCHANGED;
        public const int TVN_SELCHANGEDA = -402;
        public const int TVN_SELCHANGEDW = -451;
        public static readonly int TVN_SELCHANGING;
        public const int TVN_SELCHANGINGA = -401;
        public const int TVN_SELCHANGINGW = -450;
        public static readonly int TVN_SETDISPINFO;
        public const int TVN_SETDISPINFOA = -404;
        public const int TVN_SETDISPINFOW = -453;
        public const int TVS_CHECKBOXES = 0x100;
        public const int TVS_EDITLABELS = 8;
        public const int TVS_FULLROWSELECT = 0x1000;
        public const int TVS_HASBUTTONS = 1;
        public const int TVS_HASLINES = 2;
        public const int TVS_LINESATROOT = 4;
        public const int TVS_RTLREADING = 0x40;
        public const int TVS_SHOWSELALWAYS = 0x20;
        public const int TVS_TRACKSELECT = 0x200;
        public const int UIS_CLEAR = 2;
        public const int UIS_INITIALIZE = 3;
        public const int UIS_SET = 1;
        public const int UISF_HIDEACCEL = 2;
        public const int UISF_HIDEFOCUS = 1;
        public const int UOI_FLAGS = 1;
        public const int USERCLASSTYPE_FULL = 1;
        public const string uuid_IAccessible = "{618736E0-3C3D-11CF-810C-00AA00389B71}";
        public const string uuid_IEnumVariant = "{00020404-0000-0000-C000-000000000046}";
        public const int VIEW_E_DRAW = -2147221184;
        public const int VK_CONTROL = 0x11;
        public const int VK_MENU = 0x12;
        public const int VK_SHIFT = 0x10;
        public const int VK_TAB = 9;
        public const int WA_ACTIVE = 1;
        public const int WA_CLICKACTIVE = 2;
        public const int WA_INACTIVE = 0;
        public const string WC_DATETIMEPICK = "SysDateTimePick32";
        public const string WC_LISTVIEW = "SysListView32";
        public const string WC_MONTHCAL = "SysMonthCal32";
        public const string WC_PROGRESS = "msctls_progress32";
        public const string WC_STATUSBAR = "msctls_statusbar32";
        public const string WC_TABCONTROL = "SysTabControl32";
        public const string WC_TOOLBAR = "ToolbarWindow32";
        public const string WC_TRACKBAR = "msctls_trackbar32";
        public const string WC_TREEVIEW = "SysTreeView32";
        public const int WH_GETMESSAGE = 3;
        public const int WH_JOURNALPLAYBACK = 1;
        public const int WH_MOUSE = 7;
        public const int WHEEL_DELTA = 120;
        public const int WM_ACTIVATE = 6;
        public const int WM_ACTIVATEAPP = 0x1c;
        public const int WM_AFXFIRST = 0x360;
        public const int WM_AFXLAST = 0x37f;
        public const int WM_APP = 0x8000;
        public const int WM_ASKCBFORMATNAME = 780;
        public const int WM_CANCELJOURNAL = 0x4b;
        public const int WM_CANCELMODE = 0x1f;
        public const int WM_CAPTURECHANGED = 0x215;
        public const int WM_CHANGECBCHAIN = 0x30d;
        public const int WM_CHANGEUISTATE = 0x127;
        public const int WM_CHAR = 0x102;
        public const int WM_CHARTOITEM = 0x2f;
        public const int WM_CHILDACTIVATE = 0x22;
        public const int WM_CHOOSEFONT_GETLOGFONT = 0x401;
        public const int WM_CLEAR = 0x303;
        public const int WM_CLOSE = 0x10;
        public const int WM_COMMAND = 0x111;
        public const int WM_COMMNOTIFY = 0x44;
        public const int WM_COMPACTING = 0x41;
        public const int WM_COMPAREITEM = 0x39;
        public const int WM_CONTEXTMENU = 0x7b;
        public const int WM_COPY = 0x301;
        public const int WM_COPYDATA = 0x4a;
        public const int WM_CREATE = 1;
        public const int WM_CTLCOLOR = 0x19;
        public const int WM_CTLCOLORBTN = 0x135;
        public const int WM_CTLCOLORDLG = 310;
        public const int WM_CTLCOLOREDIT = 0x133;
        public const int WM_CTLCOLORLISTBOX = 0x134;
        public const int WM_CTLCOLORMSGBOX = 0x132;
        public const int WM_CTLCOLORSCROLLBAR = 0x137;
        public const int WM_CTLCOLORSTATIC = 0x138;
        public const int WM_CUT = 0x300;
        public const int WM_DEADCHAR = 0x103;
        public const int WM_DELETEITEM = 0x2d;
        public const int WM_DESTROY = 2;
        public const int WM_DESTROYCLIPBOARD = 0x307;
        public const int WM_DEVICECHANGE = 0x219;
        public const int WM_DEVMODECHANGE = 0x1b;
        public const int WM_DISPLAYCHANGE = 0x7e;
        public const int WM_DRAWCLIPBOARD = 0x308;
        public const int WM_DRAWITEM = 0x2b;
        public const int WM_DROPFILES = 0x233;
        public const int WM_ENABLE = 10;
        public const int WM_ENDSESSION = 0x16;
        public const int WM_ENTERIDLE = 0x121;
        public const int WM_ENTERMENULOOP = 0x211;
        public const int WM_ENTERSIZEMOVE = 0x231;
        public const int WM_ERASEBKGND = 20;
        public const int WM_EXITMENULOOP = 530;
        public const int WM_EXITSIZEMOVE = 0x232;
        public const int WM_FONTCHANGE = 0x1d;
        public const int WM_GETDLGCODE = 0x87;
        public const int WM_GETFONT = 0x31;
        public const int WM_GETHOTKEY = 0x33;
        public const int WM_GETICON = 0x7f;
        public const int WM_GETMINMAXINFO = 0x24;
        public const int WM_GETOBJECT = 0x3d;
        public const int WM_GETTEXT = 13;
        public const int WM_GETTEXTLENGTH = 14;
        public const int WM_HANDHELDFIRST = 0x358;
        public const int WM_HANDHELDLAST = 0x35f;
        public const int WM_HELP = 0x53;
        public const int WM_HOTKEY = 0x312;
        public const int WM_HSCROLL = 0x114;
        public const int WM_HSCROLLCLIPBOARD = 0x30e;
        public const int WM_ICONERASEBKGND = 0x27;
        public const int WM_IME_CHAR = 0x286;
        public const int WM_IME_COMPOSITION = 0x10f;
        public const int WM_IME_COMPOSITIONFULL = 0x284;
        public const int WM_IME_CONTROL = 0x283;
        public const int WM_IME_ENDCOMPOSITION = 270;
        public const int WM_IME_KEYDOWN = 0x290;
        public const int WM_IME_KEYLAST = 0x10f;
        public const int WM_IME_KEYUP = 0x291;
        public const int WM_IME_NOTIFY = 0x282;
        public const int WM_IME_SELECT = 0x285;
        public const int WM_IME_SETCONTEXT = 0x281;
        public const int WM_IME_STARTCOMPOSITION = 0x10d;
        public const int WM_INITDIALOG = 0x110;
        public const int WM_INITMENU = 0x116;
        public const int WM_INITMENUPOPUP = 0x117;
        public const int WM_INPUTLANGCHANGE = 0x51;
        public const int WM_INPUTLANGCHANGEREQUEST = 80;
        public const int WM_KEYDOWN = 0x100;
        public const int WM_KEYFIRST = 0x100;
        public const int WM_KEYLAST = 0x108;
        public const int WM_KEYUP = 0x101;
        public const int WM_KILLFOCUS = 8;
        public const int WM_LBUTTONDBLCLK = 0x203;
        public const int WM_LBUTTONDOWN = 0x201;
        public const int WM_LBUTTONUP = 0x202;
        public const int WM_MBUTTONDBLCLK = 0x209;
        public const int WM_MBUTTONDOWN = 0x207;
        public const int WM_MBUTTONUP = 520;
        public const int WM_MDIACTIVATE = 0x222;
        public const int WM_MDICASCADE = 0x227;
        public const int WM_MDICREATE = 0x220;
        public const int WM_MDIDESTROY = 0x221;
        public const int WM_MDIGETACTIVE = 0x229;
        public const int WM_MDIICONARRANGE = 0x228;
        public const int WM_MDIMAXIMIZE = 0x225;
        public const int WM_MDINEXT = 0x224;
        public const int WM_MDIREFRESHMENU = 0x234;
        public const int WM_MDIRESTORE = 0x223;
        public const int WM_MDISETMENU = 560;
        public const int WM_MDITILE = 550;
        public const int WM_MEASUREITEM = 0x2c;
        public const int WM_MENUCHAR = 0x120;
        public const int WM_MENUSELECT = 0x11f;
        public const int WM_MOUSEACTIVATE = 0x21;
        public const int WM_MOUSEFIRST = 0x200;
        public const int WM_MOUSEHOVER = 0x2a1;
        public const int WM_MOUSELAST = 0x20a;
        public const int WM_MOUSELEAVE = 0x2a3;
        public const int WM_MOUSEMOVE = 0x200;
        public const int WM_MOUSEWHEEL = 0x20a;
        public const int WM_MOVE = 3;
        public const int WM_MOVING = 0x216;
        public const int WM_NCACTIVATE = 0x86;
        public const int WM_NCCALCSIZE = 0x83;
        public const int WM_NCCREATE = 0x81;
        public const int WM_NCDESTROY = 130;
        public const int WM_NCHITTEST = 0x84;
        public const int WM_NCLBUTTONDBLCLK = 0xa3;
        public const int WM_NCLBUTTONDOWN = 0xa1;
        public const int WM_NCLBUTTONUP = 0xa2;
        public const int WM_NCMBUTTONDBLCLK = 0xa9;
        public const int WM_NCMBUTTONDOWN = 0xa7;
        public const int WM_NCMBUTTONUP = 0xa8;
        public const int WM_NCMOUSEMOVE = 160;
        public const int WM_NCPAINT = 0x85;
        public const int WM_NCRBUTTONDBLCLK = 0xa6;
        public const int WM_NCRBUTTONDOWN = 0xa4;
        public const int WM_NCRBUTTONUP = 0xa5;
        public const int WM_NCXBUTTONDBLCLK = 0xad;
        public const int WM_NCXBUTTONDOWN = 0xab;
        public const int WM_NCXBUTTONUP = 0xac;
        public const int WM_NEXTDLGCTL = 40;
        public const int WM_NEXTMENU = 0x213;
        public const int WM_NOTIFY = 0x4e;
        public const int WM_NOTIFYFORMAT = 0x55;
        public const int WM_NULL = 0;
        public const int WM_PAINT = 15;
        public const int WM_PAINTCLIPBOARD = 0x309;
        public const int WM_PAINTICON = 0x26;
        public const int WM_PALETTECHANGED = 0x311;
        public const int WM_PALETTEISCHANGING = 0x310;
        public const int WM_PARENTNOTIFY = 0x210;
        public const int WM_PASTE = 770;
        public const int WM_PENWINFIRST = 0x380;
        public const int WM_PENWINLAST = 0x38f;
        public const int WM_POWER = 0x48;
        public const int WM_POWERBROADCAST = 0x218;
        public const int WM_PRINT = 0x317;
        public const int WM_PRINTCLIENT = 0x318;
        public const int WM_QUERYDRAGICON = 0x37;
        public const int WM_QUERYENDSESSION = 0x11;
        public const int WM_QUERYNEWPALETTE = 0x30f;
        public const int WM_QUERYOPEN = 0x13;
        public const int WM_QUERYUISTATE = 0x129;
        public const int WM_QUEUESYNC = 0x23;
        public const int WM_QUIT = 0x12;
        public const int WM_RBUTTONDBLCLK = 0x206;
        public const int WM_RBUTTONDOWN = 0x204;
        public const int WM_RBUTTONUP = 0x205;
        public const int WM_REFLECT = 0x2000;
        public const int WM_RENDERALLFORMATS = 0x306;
        public const int WM_RENDERFORMAT = 0x305;
        public const int WM_SETCURSOR = 0x20;
        public const int WM_SETFOCUS = 7;
        public const int WM_SETFONT = 0x30;
        public const int WM_SETHOTKEY = 50;
        public const int WM_SETICON = 0x80;
        public const int WM_SETREDRAW = 11;
        public const int WM_SETTEXT = 12;
        public const int WM_SETTINGCHANGE = 0x1a;
        public const int WM_SHOWWINDOW = 0x18;
        public const int WM_SIZE = 5;
        public const int WM_SIZECLIPBOARD = 0x30b;
        public const int WM_SIZING = 0x214;
        public const int WM_SPOOLERSTATUS = 0x2a;
        public const int WM_STYLECHANGED = 0x7d;
        public const int WM_STYLECHANGING = 0x7c;
        public const int WM_SYSCHAR = 0x106;
        public const int WM_SYSCOLORCHANGE = 0x15;
        public const int WM_SYSCOMMAND = 0x112;
        public const int WM_SYSDEADCHAR = 0x107;
        public const int WM_SYSKEYDOWN = 260;
        public const int WM_SYSKEYUP = 0x105;
        public const int WM_TCARD = 0x52;
        public const int WM_TIMECHANGE = 30;
        public const int WM_TIMER = 0x113;
        public const int WM_UNDO = 0x304;
        public const int WM_UPDATEUISTATE = 0x128;
        public const int WM_USER = 0x400;
        public const int WM_USERCHANGED = 0x54;
        public const int WM_VKEYTOITEM = 0x2e;
        public const int WM_VSCROLL = 0x115;
        public const int WM_VSCROLLCLIPBOARD = 0x30a;
        public const int WM_WINDOWPOSCHANGED = 0x47;
        public const int WM_WINDOWPOSCHANGING = 70;
        public const int WM_WININICHANGE = 0x1a;
        public const int WM_XBUTTONDBLCLK = 0x20d;
        public const int WM_XBUTTONDOWN = 0x20b;
        public const int WM_XBUTTONUP = 0x20c;
        public const int WPF_SETMINPOSITION = 1;
        public const int WS_BORDER = 0x800000;
        public const int WS_CAPTION = 0xc00000;
        public const int WS_CHILD = 0x40000000;
        public const int WS_CLIPCHILDREN = 0x2000000;
        public const int WS_CLIPSIBLINGS = 0x4000000;
        public const int WS_DISABLED = 0x8000000;
        public const int WS_DLGFRAME = 0x400000;
        public const int WS_EX_APPWINDOW = 0x40000;
        public const int WS_EX_CLIENTEDGE = 0x200;
        public const int WS_EX_CONTEXTHELP = 0x400;
        public const int WS_EX_CONTROLPARENT = 0x10000;
        public const int WS_EX_DLGMODALFRAME = 1;
        public const int WS_EX_LAYERED = 0x80000;
        public const int WS_EX_LEFT = 0;
        public const int WS_EX_LEFTSCROLLBAR = 0x4000;
        public const int WS_EX_MDICHILD = 0x40;
        public const int WS_EX_RIGHT = 0x1000;
        public const int WS_EX_RTLREADING = 0x2000;
        public const int WS_EX_STATICEDGE = 0x20000;
        public const int WS_EX_TOOLWINDOW = 0x80;
        public const int WS_EX_TOPMOST = 8;
        public const int WS_HSCROLL = 0x100000;
        public const int WS_MAXIMIZE = 0x1000000;
        public const int WS_MAXIMIZEBOX = 0x10000;
        public const int WS_MINIMIZE = 0x20000000;
        public const int WS_MINIMIZEBOX = 0x20000;
        public const int WS_OVERLAPPED = 0;
        public const int WS_POPUP = -2147483648;
        public const int WS_SYSMENU = 0x80000;
        public const int WS_TABSTOP = 0x10000;
        public const int WS_THICKFRAME = 0x40000;
        public const int WS_VISIBLE = 0x10000000;
        public const int WS_VSCROLL = 0x200000;
        public const int WSF_VISIBLE = 1;
        public const int XBUTTON1 = 1;
        public const int XBUTTON2 = 2;

        public const int WS_SIZEBOX = WS_THICKFRAME;
        #endregion
        public const int MouseEvent_Absolute = 0x8000;
        public const int MouserEvent_Hwheel = 0x01000;
        public const int MouseEvent_Move = 0x0001;
        public const int MouseEvent_Move_noCoalesce = 0x2000;
        public const int MouseEvent_LeftDown = 0x0002;
        public const int MouseEvent_LeftUp = 0x0004;
        public const int MouseEvent_MiddleDown = 0x0020;
        public const int MouseEvent_MiddleUp = 0x0040;
        public const int MouseEvent_RightDown = 0x0008;
        public const int MouseEvent_RightUp = 0x0010;
        public const int MouseEvent_Wheel = 0x0800;
        public const int MousseEvent_XUp = 0x0100;
        public const int MousseEvent_XDown = 0x0080;
    }
}

