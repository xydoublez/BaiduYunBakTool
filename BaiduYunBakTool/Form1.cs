using Microsoft.Win32;
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
        }
        private void Bak()
        {
            if (!Directory.Exists(BakPath))
            {
                Directory.CreateDirectory(BakPath);
            }
            BakPath = BakPath + DateTime.Now.ToString("yyyyMMdd");
            if (!Directory.Exists(BakPath))
            {
                Directory.CreateDirectory(BakPath);
            }
            //GenMysqlBak();
            //GenIISBak();
            Thread.Sleep(1000);
            uploadBaidu();

        }
        private void GenMysqlBak()
        {
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = Path.Combine(MysqlPath, "mysqldump.exe");
            info.WindowStyle = ProcessWindowStyle.Hidden;
            info.Arguments = "-u" + User + " " + "-p" + Password + " " + DbName + " > " + Path.Combine(BakPath, DateTime.Now.ToString("yyyyMMddHHmm") + ".sql");
            Process p = Process.Start(info);
            p.WaitForExit(1000 * 60 * 10);

        }
        private void GenIISBak()
        {
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = Path.Combine(WinRarPath, "Rar.exe");
            info.WindowStyle = ProcessWindowStyle.Hidden;
            info.Arguments = " a " + Path.Combine(BakPath, DateTime.Now.ToString("yyyyMMddHHmm") + ".rar") + " " + IISPath;
            Process p = Process.Start(info);
            p.WaitForExit(1000 * 60 * 10);
        }
        private void uploadBaidu()
        {
            //查找baidu窗口句柄
            var Shell_TrayWnd = win32.FindWindow("Shell_TrayWnd", null);
            IntPtr hNext = IntPtr.Zero;
            //第一个对话框
            var TrayNotifyWnd = win32.FindWindowEx(Shell_TrayWnd, hNext, "TrayNotifyWnd", "");
            var SysPager = win32.FindWindowEx(TrayNotifyWnd, hNext, "SysPager", "");
            var ToolbarWindow32 = win32.FindWindowEx(SysPager, hNext, "用户升级的通知区域", "");
            TBBUTTONINFO info = new TBBUTTONINFO();
            Marshal.PtrToStructure(API.SendMessage(ToolbarWindow32, API.TB_GETBUTTON, 0, 0), info);
            Rectangle IconRect = new Rectangle();
            API.SendMessage(ToolbarWindow32, API.TB_GETRECT, info.idCommand, ref IconRect);
            Rectangle barRect = new Rectangle();
            API.GetWindowRect(ToolbarWindow32, ref barRect);
            //将像素坐标转化为绝对坐标：
            //API中MouseInput结构中的dx，dy含义是绝对坐标，是相对屏幕的而言的，屏幕左上角的坐标为（0,0），右下角的坐标为（65535,65535）。而我们在C#中获得的对象（Frame，button，flash等）的坐标都是像素坐标，是跟你当前屏幕的分辨率相关的。假如你的显示器分辨率是1024*768，那么屏幕左上角的像素坐标是（0,0），右下角坐标为（1024,768）。转换函数如下：
            int ScreenHeight = Screen.PrimaryScreen.WorkingArea.Height;
            int ScreenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int x = barRect.Left + (IconRect.Left / 2);
            int y = barRect.Top + (IconRect.Top / 2);
            int dx = x * (65335 / ScreenWidth); //x,y为像素坐标。
            int dy = y * (65335 / ScreenHeight);//ScreenWidth和ScreenHeight，其实是当前显示器的分辨率，获得方法是ScreenWidth=Screen.PrimaryScreen.WorkingArea.Width；

            MouseInput myMinput = new MouseInput();
            myMinput.dx = dx;
            myMinput.dy = dy;
            myMinput.Mousedata = 0;
            myMinput.dwFlag = MouseEvent_Absolute | MouseEvent_Move | MouseEvent_LeftDown | MouseEvent_LeftUp;
            myMinput.time = 0;
            Input[] myInput = new Input[1];
            myInput[0] = new Input();
            myInput[0].type = 0;
            myInput[0].mi = myMinput;
            UInt32 result = SendInput((uint)myInput.Length, myInput, Marshal.SizeOf(myInput[0].GetType()));

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
            SetAutoRun();
            this.notifyIcon1.ShowBalloonTip(2000, "提示", "备份服务开始运行！", ToolTipIcon.Info);
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
    }
}
