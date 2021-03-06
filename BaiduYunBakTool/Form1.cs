﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using static BaiduYunBakTool.API;
using static BaiduYunBakTool.win32;

namespace BaiduYunBakTool
{
    [ComVisible(true)]
    public partial class Form1 : Form
    {
        string StartTime = ConfigurationManager.AppSettings["StartTime"].ToString();
        string WinRarPath = ConfigurationManager.AppSettings["WinRarPath"].ToString();
        string IISPath = ConfigurationManager.AppSettings["IISPath"].ToString();
        string MysqlPath = ConfigurationManager.AppSettings["MysqlPath"].ToString();
        string User = ConfigurationManager.AppSettings["User"].ToString();
        string Password = ConfigurationManager.AppSettings["Password"].ToString();
        string DbName = ConfigurationManager.AppSettings["DbName"].ToString();
        string BakPath = ConfigurationManager.AppSettings["BakPath"].ToString();
        string AutoHotKeyPath = ConfigurationManager.AppSettings["AutoHotKeyPath"].ToString();

        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now.ToString("HH:mm:ss") == ConfigurationManager.AppSettings["StartTime"].ToString() + ":00")
            {
                Bak();
            }
            if (DateTime.Now.ToString("HH:mm:ss") == "22:00:00")
            {
                Application.Restart();
                Environment.Exit(0);
            }
        }
        private void Bak()
        {
            try
            {
                //this.wb.Refresh(WebBrowserRefreshOption.Completely);
                //Thread.Sleep(1000);
                BakPath = ConfigurationManager.AppSettings["BakPath"].ToString();
                if (!Directory.Exists(BakPath))
                {
                    Directory.CreateDirectory(BakPath);
                }
                BakPath = Path.Combine(BakPath, DateTime.Now.ToString("yyyyMMdd"));
                if (!Directory.Exists(BakPath))
                {
                    Directory.CreateDirectory(BakPath);
                }
                DeleteBeforeFiles();
                Thread t = new Thread(doBak);
                t.Start();
                Log("备份成功", BakPath);
            }
            catch (Exception ex)
            {
                Log("备份失败", ex.Message + ex.StackTrace);
            }

        }
        private void doBak()
        {
            GenMysqlBak();
            GenIISBak();
            GenFinalRar();
            this.BeginInvoke(new Action(() => {
                this.Show();
                Thread.Sleep(100);
                UploadBaidu();
            }));
            
        }
        private void DeleteBeforeFiles()
        {
            try
            {
                var path = ConfigurationManager.AppSettings["BakPath"].ToString();
                DirectoryInfo dir = new DirectoryInfo(path);
                FileSystemInfo[] fileInfoList = dir.GetFileSystemInfos("*.*", SearchOption.AllDirectories);
                List<DirectoryInfo> deleteDirs = new List<DirectoryInfo>();
                List<FileInfo> deleteFiles = new List<FileInfo>();
                foreach (var f in fileInfoList)
                {
                    if (DateTime.Compare(f.CreationTime.Date, DateTime.Now.Date) < 0)
                    {
                        if (f is DirectoryInfo)
                        {
                            deleteDirs.Add(f as DirectoryInfo);
                        }
                        else if(f is FileInfo)
                        {
                            deleteFiles.Add(f as FileInfo);
                        }
                    }
                }
                foreach (var f in deleteFiles)
                {
                    try
                    {
                        f.Delete();
                    }
                    catch
                    {
                        Log("删除旧文件失败", f.FullName);
                    }
                }
                foreach (var f in deleteDirs)
                {
                    try
                    {
                        f.Delete();
                    }
                    catch
                    {
                        Log("删除旧文件失败", f.FullName);
                    }
                }
            }
            catch (Exception ex)
            {
                Log("删除旧文件失败", ex.Message + ex.StackTrace);
            }
        }
        /// <summary>
        /// 此方法成功过
        /// </summary>
        private void autoBaidu2()
        {

            string bakfile = Path.Combine(ConfigurationManager.AppSettings["BakPath"].ToString(), DateTime.Now.ToString("yyyyMMdd") + ".rar");


            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = "Explorer.exe";
            info.WindowStyle = ProcessWindowStyle.Normal;
            info.Arguments = "/select,\"" + bakfile + "\"";
            Process p1 = Process.Start(info);
            p1.WaitForExit(1000 * 60 * 10);
            Thread.Sleep(1000);
            SendKeys.SendWait("^c");
            Thread.Sleep(100);
            var baidu = win32.FindWindow(null, "欢迎使用百度网盘");
            var baiduTool = win32.FindWindow(null, "百度云");
            API.SendMessage(baiduTool, API.WM_LBUTTONDBLCLK, 0, 0);
            Thread.Sleep(100);
            API.SetActiveWindow(baidu);
            Rectangle barRect = new Rectangle();
            API.GetWindowRect(baidu, ref barRect);
            int dx = barRect.X + 340;
            int dy = barRect.Y + 40;
            int dy2 = barRect.Y + 300;
            ////点击我的网盘
            //API.SendMessage(baidu, WM_LBUTTONDOWN, 0, (dy << 16) | dx);
            //API.SendMessage(baidu, WM_LBUTTONUP, 0, (dy << 16) | dx);
            //Thread.Sleep(100);
            ////点击文件列表标题
            //dx = barRect.X + 340;
            //dy = barRect.Y + 190;
            //API.SendMessage(baidu, WM_LBUTTONDOWN, 0, (dy << 16) | dx);
            //API.SendMessage(baidu, WM_LBUTTONUP, 0, (dy << 16) | dx);
            string script = "CoordMode, Mouse, Screen \r\n";
            script += "MouseClick, left, " + dx + "," + dy + " \r\n";
            script += " Sleep,100 \r\n";
            script += "MouseClick, left, " + dx + "," + dy2 + " \r\n";
            script += " Sleep,100 \r\n";
            script += "send, ^v  \r\n ";


            File.WriteAllText("auto.ahk", script);
            ProcessStartInfo info1 = new ProcessStartInfo();
            info1.FileName = AutoHotKeyPath;
            info1.WindowStyle = ProcessWindowStyle.Hidden;
            info1.Arguments = " auto.ahk ";
            Process p = Process.Start(info1);
            p.WaitForExit(1000 * 60 * 10);


        }
        /// <summary>
        /// 用sendmessage 拖拽消息 上传 
        /// </summary>
        private void autoBaidu3()
        {
            string bakfile = Path.Combine(ConfigurationManager.AppSettings["BakPath"].ToString(), DateTime.Now.ToString("yyyyMMdd") + ".rar");
            var baidu = win32.FindWindow(null, "百度云");
            API.SendMessage(baidu, API.WM_LBUTTONDBLCLK, 0, 0);
            DropFile(baidu, bakfile);

        }

        /// <summary>
        /// 此方法对百度云无效
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private bool DropFile(IntPtr hWnd, string filePath)
        {
            unsafe
            {
                bool ret = false;
                int pid;
                GetWindowThreadProcessId(hWnd, out pid);

                IntPtr hproc;
                //打开进程
                hproc = OpenProcess(PROCESS_ALL_ACCESS, false, pid);
                if (hproc == IntPtr.Zero)
                    return false;

                char[] strbuf = filePath.ToCharArray();
                int cbstrSize = filePath.Length * 2 + 2;//*2因每个字符占用两字节,+2为最后的/0所占的2字节
                int sizedropfiles = Marshal.SizeOf(typeof(DROPFILES));
                //字符串缓存
                int bufSize = sizedropfiles + cbstrSize;

                //申请缓存
                IntPtr pdbuf = Marshal.AllocCoTaskMem(bufSize);

                DROPFILES* dropfiles = (DROPFILES*)pdbuf;
                //计算字符串的地址
                IntPtr pstrbuf = new IntPtr(pdbuf.ToInt32() + sizedropfiles);
                //清零初始化
                //    ZeroMemory((IntPtr)dropfiles, bufSize);   //这个清零不知道怎么写。

                //构造本地的DROPFILES
                dropfiles->pFiles = sizedropfiles;
                dropfiles->x = 0;
                dropfiles->y = 0;
                dropfiles->fNC = 0;
                dropfiles->fWide = 1;//使用Unicode字符
                //复制填充字符串
                Marshal.Copy(strbuf, 0, pstrbuf, strbuf.Length);

                //申请远程内存
                IntPtr ptrRemote = VirtualAllocEx(hproc, IntPtr.Zero, (uint)bufSize, AllocationType.Commit, MemoryProtection.ReadWrite);
                if (ptrRemote == IntPtr.Zero)
                    goto clear;

                int writecount;
                //写入目标进程
                if (WriteProcessMemory(hproc, ptrRemote, (IntPtr)dropfiles, bufSize, out writecount))
                {
                    /*MessageBox.Show(string.Format(
                        "缓存写入对方内存:{0:X}/n应写入{1}字节,成功写入{2}字节", ptrRemote.ToInt32()
                        , bufSize, writecount));*/
                    //发送消息
                    SendMessage(hWnd, WM_DROPFILES, ptrRemote.ToInt32(), 0);
                    ret = true;
                }

                clear://收尾工作
                Marshal.FreeCoTaskMem((IntPtr)(dropfiles));//释放Com里分配的缓存

                if (ptrRemote != IntPtr.Zero)
                {
                    //释放远程内存
                    if (!VirtualFreeEx(hproc, ptrRemote, 0, FreeType.Release))
                    {
                        throw new Win32Exception();
                    }
                }
                //关闭句柄
                CloseHandle(hproc);

                return ret;
            }
        }
        private void autoBaidu()
        {

            string bakfile = Path.Combine(ConfigurationManager.AppSettings["BakPath"].ToString(), DateTime.Now.ToString("yyyyMMdd") + ".rar");


            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = "Explorer.exe";
            info.WindowStyle = ProcessWindowStyle.Normal;
            info.Arguments = "/select,\"" + bakfile + "\"";
            Process p1 = Process.Start(info);
            Thread.Sleep(100);//让程序停一会
            API.MoveWindow(p1.MainWindowHandle, 200, 100, 400, 400, true);
            p1.WaitForExit(1000 * 60 * 10);
            Thread.Sleep(100);
            GetSelectFile();
            Point point = new Point();
            win32.GetCursorPos(out point);
            int x1, y1, x2, y2;
            x1 = point.X;
            y1 = point.Y;
            var baidu = win32.FindWindow(null, "欢迎使用百度网盘");
            RECT rect = new RECT();
            win32.GetWindowRect(baidu, out rect);
            x2 = rect.X + 100;
            y2 = rect.Y + 150;
            string script = "CoordMode, Mouse, Screen \r\n";
            script += "MouseClickDrag, Left, " + x1 + "," + y1 + "," + x2 + "," + y2 + " \r\n";
            File.WriteAllText("auto.ahk", script);
            ProcessStartInfo info1 = new ProcessStartInfo();
            info1.FileName = AutoHotKeyPath;
            info1.WindowStyle = ProcessWindowStyle.Hidden;
            info1.Arguments = " auto.ahk ";
            Process p = Process.Start(info1);
            p.WaitForExit(1000 * 60 * 10);
        }
        private void GenMysqlBak()
        {
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = "cmd.exe";
            info.WindowStyle = ProcessWindowStyle.Hidden;
            info.UseShellExecute = false;
            info.RedirectStandardOutput = true;
            info.Arguments = string.Format(" /c mysqldump.exe  -u {0} -p{1} {2} > {3}", User, Password, "wecenter", Path.Combine(BakPath, DateTime.Now.ToString("yyyyMMddHHmm") + ".sql"));
            info.WorkingDirectory = MysqlPath;
            Process p = Process.Start(info);
            Log("mysql", p.StandardOutput.ReadToEnd());
            p.WaitForExit();
            Thread.Sleep(1000);

        }
        private void GenIISBak()
        {
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = Path.Combine(WinRarPath, "Rar.exe");
            info.WindowStyle = ProcessWindowStyle.Hidden;
            info.Arguments = " a -pSfx371482 " + Path.Combine(BakPath, DateTime.Now.ToString("yyyyMMddHHmm") + ".rar") + " " + IISPath;
            Process p = Process.Start(info);
            p.WaitForExit();
        }
        private void GenFinalRar()
        {
            string bakfile = Path.Combine(ConfigurationManager.AppSettings["BakPath"].ToString(), DateTime.Now.ToString("yyyyMMdd") + ".rar");
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = Path.Combine(WinRarPath, "Rar.exe");
            info.WindowStyle = ProcessWindowStyle.Hidden;
            info.Arguments = " a -pSfx371482 " + bakfile + " " + BakPath;
            Process p = Process.Start(info);
            p.WaitForExit();
        }
        private void UploadBaidu()
        {
            Thread.Sleep(1000);
            string bakfile = Path.Combine(ConfigurationManager.AppSettings["BakPath"].ToString(), DateTime.Now.ToString("yyyyMMdd") + ".rar");
            var r = this.wb.Document.InvokeScript("lzqUpload", new object[] { bakfile});
        }
        private void openBaidu()
        {
            var Shell_TrayWnd = win32.FindWindow("Shell_TrayWnd", null);
            IntPtr hNext = IntPtr.Zero;
            //第一个对话框
            var TrayNotifyWnd = win32.FindWindowEx(Shell_TrayWnd, hNext, "TrayNotifyWnd", "");
            var SysPager = win32.FindWindowEx(TrayNotifyWnd, hNext, "SysPager", "");
            var ToolbarWindow32 = win32.FindWindowEx(SysPager, hNext, null, "用户升级的通知区域");
            int Pid = 0;
            GetWindowThreadProcessId(ToolbarWindow32, out Pid);
            //打开ToolbarWindow32所在的进程，就是explorer.exe
            IntPtr hProcess = OpenProcess(PROCESS_ALL_ACCESS, false, Pid);
            //在进程explorer.exe中申请内存

            IntPtr lpButton = VirtualAllocEx(hProcess, IntPtr.Zero, 1024, AllocationType.Commit, MemoryProtection.ReadWrite);
            //执行TB_GETBUTTON
            TBBUTTONINFO info = new TBBUTTONINFO();
            var count = API.SendMessage(ToolbarWindow32, API.TB_GETBUTTON, 0, 0);
            API.SendMessage(ToolbarWindow32, API.TB_GETBUTTON, 1, ref info);

            //从进程explorer.exe中读取需要的数据
            byte[] lpBuffer = new byte[Marshal.SizeOf(info)];
            int lpNumberOfByteRead = 0;
            ReadProcessMemory(hProcess, info.lParam, out lpBuffer, Marshal.SizeOf(info), out lpNumberOfByteRead);
            //释放在进程explorer.exe中申请的内存
            VirtualFreeEx(hProcess, lpButton, Marshal.SizeOf(info), FreeType.Release);
            CloseHandle(hProcess);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bak();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void 打开主窗体ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void 退出程序ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("您确定要退出程序，退出后备份会不正常工作！", "警告", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                System.Environment.Exit(0);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.wb.Navigate("https://pan.baidu.com");
            //js调用C#用
            this.wb.ObjectForScripting = this;
            SetAutoRun();
            this.notifyIcon1.ShowBalloonTip(2000, "提示", "备份服务开始运行！", ToolTipIcon.Info);
        }
        //添加脚本
        private void InstallScript(string code, HtmlDocument frameDoc = null)
        {

            if (null == this.wb.Document || (string.IsNullOrEmpty(code)))
                return;

            HtmlElement scriptElement = null;
            HtmlElementCollection elements = null;
            if (null == scriptElement)
            {
                if (frameDoc == null)
                {
                    scriptElement = this.wb.Document.CreateElement("script");
                    elements = this.wb.Document.GetElementsByTagName("head");

                }
                else
                {
                    scriptElement = frameDoc.CreateElement("script");
                    elements = frameDoc.GetElementsByTagName("head");

                }
                if (elements.Count > 0)
                    elements[0].AppendChild(scriptElement);




            }

            //scriptElement.SetAttribute("id", id);
            scriptElement.SetAttribute("type", "text/javascript");
            scriptElement.SetAttribute("language", "javascript");
            scriptElement.SetAttribute("text", code);


        }
        public void UploadBaidu(string filename)
        {
            Log("uploadbaidu", filename);
            var openFile = win32.FindWindow(null, "选择要加载的文件");
            win32.SetForegroundWindow(openFile);
            IntPtr hNext = IntPtr.Zero;
            var ComboBoxEx32 = win32.FindWindowEx(openFile, hNext, "ComboBoxEx32", null);
            var ComboBox = win32.FindWindowEx(ComboBoxEx32, hNext, "ComboBox", null);
            var edit = win32.FindWindowEx(ComboBox, hNext, "Edit", null);
            win32.SendMessage(edit,win32.WM_SETTEXT, IntPtr.Zero, filename);
            Thread.Sleep(100);
            //第一个对话框
            var openButton = win32.FindWindowEx(openFile, hNext, "Button", "打开(&O)");
            win32.SendMessage(openButton, BM_CLICK, IntPtr.Zero, null);
            Thread.Sleep(100);
            this.Hide();

        }
        private void SetAutoRun()
        {
            try
            {
                string path = Application.ExecutablePath;
                RegistryKey rk = Registry.LocalMachine;
                RegistryKey rk2 = rk.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
                rk2.SetValue("BaiduYunBakTool", path + " /h");
                rk2.Close();
                rk.Close();

            }
            catch (Exception ex)
            {
                Log("SetAutoRun", ex.Message);
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 添加文本日志
        /// </summary>
        /// <param name="type"></param>
        /// <param name="msg"></param>
        public void Log(string type, string msg)
        {
            try
            {
                var dir = AppDomain.CurrentDomain.BaseDirectory + "\\log";
                var log = dir + "\\" + System.DateTime.Now.ToString("yyyyMMdd") + "log.txt";
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                if (File.Exists(log))
                {
                    var file = new FileInfo(log);
                    if (file.Length >= 1024 * 1024 * 10) //100M
                    {
                        file.Delete();
                    }
                }
                File.AppendAllText(log, "\r\n时间:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "操作：" + type + "\r\n内容：" + msg);
            }
            catch
            {

            }
        }
        ///
        /// 根据句柄获取类名
        ///
        ///
        ///
        private string GetFormClassName(IntPtr ptr)
        {
            StringBuilder nameBiulder = new StringBuilder(255);
            GetClassName(ptr, nameBiulder, 255);
            return nameBiulder.ToString();
        }

        ///
        /// 根据句柄获取窗口标题
        ///
        ///
        ///
        private string GetFormTitle(IntPtr ptr)
        {
            StringBuilder titleBiulder = new StringBuilder(255);
            GetWindowText(ptr, titleBiulder, 255);
            return titleBiulder.ToString();
        }

        public bool FindCallback(IntPtr hwnd, int lParam)
        {
            int pHwnd = GetParent(hwnd);
            //如果再没有父窗口并且为可视状态的窗口，则遍历
            if (IsWindowVisible(hwnd) == true)
            {
                IntPtr cabinetWClassIntPtr = hwnd;
                string cabinetWClassName = GetFormClassName(cabinetWClassIntPtr);
                //如果类名为CabinetWClass ，则为explorer窗口，可以通过spy++查看窗口类型
                if (cabinetWClassName.Equals("CabinetWClass", StringComparison.OrdinalIgnoreCase))
                {
                    //下面为一层层往下查找，直到找到资源管理器的地址窗体，通过他获取窗体地址
                    IntPtr workerWIntPtr = FindWindowEx(cabinetWClassIntPtr, IntPtr.Zero, "WorkerW", null);
                    IntPtr reBarWindow32IntPtr = FindWindowEx(workerWIntPtr, IntPtr.Zero, "ReBarWindow32", null);
                    IntPtr addressBandRootIntPtr = FindWindowEx(reBarWindow32IntPtr, IntPtr.Zero, "Address Band Root", null);
                    IntPtr msctls_progress32IntPtr = FindWindowEx(addressBandRootIntPtr, IntPtr.Zero, "msctls_progress32", null);
                    IntPtr breadcrumbParentIntPtr = FindWindowEx(msctls_progress32IntPtr, IntPtr.Zero, "Breadcrumb Parent", null);
                    IntPtr toolbarWindow32IntPtr = FindWindowEx(breadcrumbParentIntPtr, IntPtr.Zero, "ToolbarWindow32", null);


                    string title = GetFormTitle(toolbarWindow32IntPtr);
                    System.Diagnostics.Trace.WriteLine("title:" + title);
                    StringBuilder text = new StringBuilder();
                    win32.GetWindowText(cabinetWClassIntPtr, text, 255);
                    System.Diagnostics.Trace.WriteLine("text:" + text.ToString());

                    int index = title.IndexOf(':');
                    index++;
                    string path = title.Substring(index, title.Length - index);
                    Console.WriteLine(path);
                }
            }
            return true;
        }

        private void GetSelectFile()
        {
            EnumWindows(FindCallback, 0);
        }

        private void button2_Click(object sender, EventArgs e)
        {

            var bakfile = "e:\\test.rar";
            var r = this.wb.Document.InvokeScript("lzqUpload", new object[] { bakfile });
        }

        private void wb_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            System.Diagnostics.Trace.WriteLine("DocumentCompleted：" + e.Url);
            Log("loadurl", e.Url.ToString());
            //文件列表
            if (e.Url.ToString().IndexOf("https://pan.baidu.com/disk/home") > -1)
            {
                InstallJs(this.wb.Document);
            }
        }
        private void InstallJs(HtmlDocument doc)
        {
            string script = File.ReadAllText("baidu.js");
            InstallScript(script, null);
            
        }
    }
}
