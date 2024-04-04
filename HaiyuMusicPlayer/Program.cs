using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HaiyuMusicPlayer
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Process instance = RunningInstance();

            if (instance != null)
            {
                HandleRunningInstance();
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FormMusic());
            }
        }

        public static Process RunningInstance()
        {

            Process current = Process.GetCurrentProcess();// 当前新启动的线程

            Process[] processes = Process.GetProcessesByName(current.ProcessName);

            // 遍历与当前进程名称相同的进程列表
            foreach (Process process in processes)
            {
                // process,原来旧的线程与当前新启动的线程ID不一样
                if (process.Id == current.Id) continue;

                if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == current.MainModule.FileName)
                {
                    return process;// 返回原来旧线程的窗体
                }
            }
            return null;
        }

        private static void HandleRunningInstance()
        {
            MessageBox.Show(@"该程序已经在运行！", @"提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
