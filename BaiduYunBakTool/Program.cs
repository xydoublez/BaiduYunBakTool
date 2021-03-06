﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BaiduYunBakTool
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            bool bNotRun = true;
            System.Threading.Mutex mux = new System.Threading.Mutex(true, "BaiduYunBakTool", out bNotRun);
            if (!bNotRun)
            {
                MessageBox.Show("程序已经运行!请从托盘处打开程序！");
                Environment.Exit(0);
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
