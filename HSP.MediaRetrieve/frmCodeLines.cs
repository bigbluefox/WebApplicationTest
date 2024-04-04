using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using HSP.MediaRetrieve.Business;

namespace HSP.MediaRetrieve
{
    public partial class frmCodeLines : Form
    {
        /// <summary>
        /// 代码行数
        /// </summary>
        internal static Int64 CodeLines { get; set; }

        /// <summary>
        /// 子项代码行数
        /// </summary>
        internal static Int64 SubLines { get; set; }

        /// <summary>
        ///  代码文件数
        /// </summary>
        internal static Int64 CodeFiles { get; set; }

        /// <summary>
        /// 代码路径级数
        /// </summary>
        internal int CodeLevel { get; set; }

        /// <summary>
        /// 默认代码文件类型
        /// </summary>
        internal string DefaultCodesFiles { get; set; }

        /// <summary>
        /// 项目查找结果
        /// </summary>
        internal List<ProjectEntity> List = new List<ProjectEntity>();


        // 定义事件使用的委托
        public delegate void ValueChangedEventHandler(object sender, ValueEventArgs e);

        // 进度发生变化之后的回调方法
        private void workder_ValueChanged(object sender, ValueEventArgs e)
        {
            //this.progressBar1.Value = e.Value;
            System.Windows.Forms.MethodInvoker invoker = () => this.progressBar1.Value = e.Value;

            if (this.progressBar1.InvokeRequired)
            {
                this.progressBar1.Invoke(invoker);
            }
            else
            {
                invoker();
            }
        }



        /// <summary>
        /// 代码数量统计构造函数
        /// </summary>
        public frmCodeLines()
        {
            InitializeComponent();

            DefaultCodesFiles = ConfigurationManager.AppSettings["DefaultCodesFiles"] ?? "";
            this.lblFileType.Text += DefaultCodesFiles;

            if (!string.IsNullOrEmpty(txtDirectory.Text)) return;

            txtDirectory.Text = @"D:\HYQC\Codes\北自所海淀E回收\代码\Manage\Riamb.eRecycling.Project";
            var txtCodePath = txtDirectory.Text;
            txtCodePath = txtCodePath.EndsWith(@"\") ? txtCodePath : txtCodePath + @"\";
            ShowRootDirView(txtCodePath);
        }

        /// <summary>
        /// 项目目录选择及代码行数统计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnProjectDir_Click(object sender, EventArgs e)
        {
            var folder = new FolderBrowserDialog();
            if (folder.ShowDialog() != DialogResult.OK) return;

            txtDirectory.Text = folder.SelectedPath;

            var txtCodePath = txtDirectory.Text;
            txtCodePath = txtCodePath.EndsWith(@"\") ? txtCodePath : txtCodePath + @"\";
            ShowRootDirView(txtCodePath);
        }

        /// <summary>
        /// 项目初级目录试图列表处理
        /// </summary>
        /// <param name="txtCodePath">项目目录</param>
        private void ShowRootDirView(string txtCodePath)
        {
            DirectoryInfo[] directories = new DirectoryInfo(txtCodePath).GetDirectories();
            if (directories.Length <= 0) return;

            var i = 1;
            foreach (DirectoryInfo info in directories)
            {
                this.lvwCodesCount.BeginUpdate();
                var item = new ListViewItem();
                item.SubItems.Add(i.ToString(CultureInfo.InvariantCulture));
                item.SubItems.Add(info.Name);
                item.SubItems.Add(info.FullName);
                item.SubItems.Add("0");
                this.lvwCodesCount.Items.Add(item);
                this.lvwCodesCount.EndUpdate();
                i++;
            }
        }

        // 结束异步操作
        private void AsyncCallback(IAsyncResult ar)
        {
            // 标准的处理步骤
            Action handler = ar.AsyncState as Action;
            if (handler != null) handler.EndInvoke(ar);

            // 重新启用按钮
            System.Windows.Forms.MethodInvoker invoker = () => this.btnStart.Enabled = true;

            if (this.InvokeRequired)
            {
                this.Invoke(invoker);
            }
            else
            {
                invoker();
            }

            //MessageBox.Show("工作完成！");
            //this.button1.Enabled = true;
            //this.timer1.Enabled = false;
        }

        public delegate void MethodInvoker();

        [BrowsableAttribute(false)]
        public new bool InvokeRequired { get; set; }

        private frmProcessBar myProcessBar = null;
        private delegate bool IncreaseHandle(int nValue);
        private IncreaseHandle myIncrease = null;

        private void ShowProcessBar()
        {
            myProcessBar = new frmProcessBar();
            // Init increase event
            myIncrease = new IncreaseHandle(myProcessBar.Increase);
            myProcessBar.StartPosition = FormStartPosition.CenterScreen;
            myProcessBar.ShowDialog();
            myProcessBar = null;
        }

        private void ThreadFun()
        {
            MethodInvoker mi = new MethodInvoker(ShowProcessBar);
            this.BeginInvoke(mi);
            Thread.Sleep(1000);//Sleep a while to show window
            bool blnIncreased = false;
            object objReturn = null;
            do
            {
                Thread.Sleep(50);
                objReturn = this.Invoke(this.myIncrease, new object[] { 2 });
                blnIncreased = (bool)objReturn;
            }
            while (blnIncreased);
        }



        /// <summary>
        /// 开始统计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            // 禁用按钮
            this.btnStart.Enabled = false;
            this.timer1.Enabled = true;

            #region 进度条

            // 实例化业务对象
            //LongTimeWork workder = new LongTimeWork();
            //workder.ValueChanged += new Business.ValueChangedEventHandler(workder_ValueChanged);

            //// 使用异步方式调用长时间的方法
            //Action handler = new Action(workder.LongTimeMethod);
            //handler.BeginInvoke(
            //    new AsyncCallback(this.AsyncCallback),
            //    handler
            //    );

            // 使用浮动进度条
            //Thread thdSub = new Thread(new ThreadStart(ThreadFun));
            //thdSub.Start();

            #endregion

            var dt = DateTime.Now;
            
            CodeLines = 0;
            var i = 1;
            var rst = "";

            if (lvwCodesCount.Items.Count > 0)
            {
                List = new List<ProjectEntity>();

                foreach (ListViewItem item in lvwCodesCount.Items)
                {
                    var entity = new ProjectEntity
                    {
                        Idx = i,
                        Name = item.SubItems[2].Text,
                        Path = item.SubItems[3].Text,
                        Count = 0
                    };

                    List.Add(entity);

                    i++;
                }

                lvwCodesCount.Items.Clear();
            }

            i = 1;

            ListViewItem lvItem;
            if (List.Count > 0)
            {
                foreach (var entity in List)
                {
                    SubLines = 0;
                    Recursive(entity.Path);

                    //此处开始异步执行,并且可以给出一个回调函数(如果不需要执行什么后续操作也可以不使用回调)
                    _recursiveMethod.BeginInvoke(entity.Path, new AsyncCallback(TaskFinished), null);

                    this.lvwCodesCount.BeginUpdate();
                    lvItem = new ListViewItem();
                    lvItem.SubItems.Add(i.ToString(CultureInfo.InvariantCulture));
                    lvItem.SubItems.Add(entity.Name);
                    lvItem.SubItems.Add(entity.Path);
                    lvItem.SubItems.Add(SubLines.ToString());
                    this.lvwCodesCount.Items.Add(lvItem);
                    this.lvwCodesCount.EndUpdate();

                    rst += entity.Name + " * " + SubLines + Environment.NewLine;

                    i++;

                }
            }

            this.lvwCodesCount.BeginUpdate();
            lvItem = new ListViewItem();
            lvItem.SubItems.Add(i.ToString(CultureInfo.InvariantCulture));
            lvItem.SubItems.Add("代码总计");
            lvItem.SubItems.Add("");
            lvItem.SubItems.Add(CodeLines.ToString());
            this.lvwCodesCount.Items.Add(lvItem);
            this.lvwCodesCount.EndUpdate();

            TimeSpan span = DateTime.Now - dt;

            rst += @"开始时间：" + dt.ToString("yyyy-MM-dd HH:mm:ss") + @"，结束时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + Environment.NewLine;
            rst += @"耗时：" + span.TotalMinutes + @"，代码行数：" + CodeLines;
            txtResult.Text = rst;

            lblFileCount.Text = @"耗时：" + span.Seconds + @"，文件数量：" + CodeFiles + @"，代码行数：" + CodeLines;

            lblCodeLineCount.Text += CodeLines;
        }

        // 递归查询委托
        delegate void RecursiveMethod(string path); //申明一个委托，表明需要在子线程上执行的方法的函数签名

        RecursiveMethod _recursiveMethod = new RecursiveMethod(Recursive);//把委托和具体的方法关联起来


        //线程完成之后回调的函数
        public void TaskFinished(IAsyncResult result)
        {
            _recursiveMethod.EndInvoke(result);

            txtResult.Text += Environment.NewLine + CodeLines.ToString();

            //double re = 0;
            //re = calcMethod.EndInvoke(result);
            //Console.WriteLine(re);
        }

        /// <summary>
        /// 递归查询代码行数
        /// </summary>
        /// <param name="codePath">代码目录</param>
        private static void Recursive(string codePath)
        {
            string[] files = Directory.GetFiles(codePath);

            foreach (string file in files)
            {
                FileInfo fi = new FileInfo(file);

                if (IsValidType(fi.Extension))
                {
                    Int64 count = CountTheLines(fi.FullName);
                    SubLines += count;
                    CodeLines += count;
                    CodeFiles += 1;
                }
            }

            DirectoryInfo[] directories = new DirectoryInfo(codePath).GetDirectories();
            if (directories.Length > 0)
            {
                foreach (DirectoryInfo info in directories)
                {
                    Recursive(info.FullName);
                }
            }

        }

        /// <summary>
        /// 是否有效类型
        /// </summary>
        /// <param name="filetype">文件类型</param>
        /// <returns></returns>
        public static bool IsValidType( string filetype)
        {
            string validType = "*.aspx;|*.cs;|*.Master;|*.ashx;";
            filetype = filetype.Trim('.');
            return validType.IndexOf("." + filetype + ";", StringComparison.Ordinal) > -1;
        }

        /// <summary>
        /// 计算代码行数
        /// </summary>
        /// <param name="file">文件目录</param>
        /// <returns></returns>
        private static Int64 CountTheLines(string file)
        {
            Int64 count = 0;
            try
            {
                FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs);
                string str = sr.ReadLine();
                while (str != null)
                {
                    str = str.Trim();
                    try
                    {
                        if (str == "" || str.Length == 1 || str.Substring(0, 2) == "//" 
                            || str.Substring(0, 5) == "using" 
                            || str.Substring(0, 9) == "namespace"
                            || str.Substring(0, 16) == "public interface"
                            || str.Substring(0, 12) == "public class"
                            || str.Substring(0, 14) == "[Serializable]"
                            || str.Substring(0, 12) == "[DataMember]"
                            || str.Substring(0, 10) == "[assembly:"


                            )
                        {
                            count--;
                        }
                            
                    }
                    catch (Exception ex)
                    {
                    }
                    count++;
                    str = sr.ReadLine();
                }
                sr.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return count;
        }

        /// <summary>
        /// 试图列表行选中事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvwCodesCount_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 右键菜单行删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvwCodesCount_RowDelete(object sender, EventArgs e)
        {
            if(lvwCodesCount.SelectedItems.Count == 0) return;

            //var lvwRow = lvwCodesCount.SelectedItems[0];

            //移除文件  
            foreach (ListViewItem item in this.lvwCodesCount.SelectedItems)
            {
                this.lvwCodesCount.Items.Remove(item);
            }  

            //lblFileType.Text = @"删除行" +lvwRow.SubItems[2].Text;
        }

        /// <summary>
        /// 清空列表视图数据点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearList_Click(object sender, EventArgs e)
        {
            if (lvwCodesCount.Items.Count > 0)
            {
                List = new List<ProjectEntity>();
                var i = 1;

                foreach (ListViewItem item in lvwCodesCount.Items)
                {
                    var entity = new ProjectEntity
                    {
                        Idx = i,
                        Name = item.SubItems[2].Text,
                        Path = item.SubItems[3].Text,
                        Count = 0
                    };

                    List.Add(entity);

                    i++;
                }
            }
            lvwCodesCount.Items.Clear();
        }

        /// <summary>
        /// 列表视图刷新点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReList_Click(object sender, EventArgs e)
        {
            if (lvwCodesCount.Items.Count > 0)
            {
                List = new List<ProjectEntity>();
                var i = 1;

                foreach (ListViewItem item in lvwCodesCount.Items)
                {
                    var entity = new ProjectEntity
                    {
                        Idx = i,
                        Name = item.SubItems[2].Text,
                        Path = item.SubItems[3].Text,
                        Count = 0
                    };

                    List.Add(entity);

                    i++;
                }
            }

            if (List.Count > 0)
            {
                lvwCodesCount.Items.Clear();
                var i = 1;
                foreach (var entity in List)
                {
                    this.lvwCodesCount.BeginUpdate();
                    var item = new ListViewItem();
                    item.SubItems.Add(i.ToString(CultureInfo.InvariantCulture));
                    item.SubItems.Add(entity.Name);
                    item.SubItems.Add(entity.Path);
                    item.SubItems.Add(entity.Count.ToString());
                    this.lvwCodesCount.Items.Add(item);
                    this.lvwCodesCount.EndUpdate();
                    i++;
                }
            }
        }

    }

    /// <summary>
    /// 项目实体
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ProjectEntity
    {
        /// <summary>
        /// 序号
        /// </summary>
        public int Idx;

        /// <summary>
        /// 项目名称
        /// </summary>
        public string Name;

        /// <summary>
        /// 项目路径
        /// </summary>
        public string Path;

        /// <summary>
        /// 代码行数
        /// </summary>
        public int Count;
    }

}

//Processist.StandardServer.Bll * 612
//Processist.StandardServer.Common * 786
//Processist.StandardServer.Dal * 1979
//Processist.StandardServer.Model * 503
//Processist.StandardServer.Web * 21038
//耗时：51，代码行数：24918

//Processist.StandardServer.Bll * 612
//Processist.StandardServer.Common * 786
//Processist.StandardServer.Dal * 1979
//Processist.StandardServer.Model * 503
//Processist.StandardServer.Web * 21038
//开始时间：2018-05-03 10:16:40，结束时间：2018-05-03 10:25:51
//耗时：10，代码行数：24918