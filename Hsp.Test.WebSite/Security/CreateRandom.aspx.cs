using System;
using System.IO;
using System.Web.UI;

public partial class Security_CreateRandom : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            for (var i = 0; i < 99; i++)
            {
                DropDownList1.Items.Add((i + 1).ToString());
            }

            DropDownList1.SelectedIndex = 15;
        }
    }

    /// <summary>
    ///     生成密码
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (!CheckBox1.Checked && !CheckBox2.Checked && !CheckBox3.Checked && !CheckBox4.Checked)

        {
            Response.Write("<font style=\"color: red;\">错误：请选择“所用字符”。</font>");

            return;
        }


        //自定义组合字符

        var strAll = "";

        if (CheckBox1.Checked)

            strAll += "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        if (CheckBox2.Checked)

            strAll += "abcdefghijklmnopqrstuvwxyz";

        if (CheckBox3.Checked)

            strAll += "0123456789";

        if (CheckBox4.Checked)

            strAll += "!@#$%^&*";


        //定义一个结果

        var result = "";


        //实例化Random对象

        var random = new Random();


        //使用for循环得到6为字符

        for (var i = 0; i < DropDownList1.SelectedIndex + 1; i++)

        {
            //返回一个小于当前自定义组合字符串长度的int类型的随机数

            var rd = random.Next(strAll.Length);


            //随机从指定的位置开始获取一个字符

            var oneChar = strAll.Substring(rd, 1);


            result += oneChar;
        }

        TextBox1.Text = result;

        Response.Write("<font style=\"color: blue;\">结果文字长度：" + result.Length + "</font>");


        #region 写入日志

        var logFile = "Logger.txt";
        var logPath = Server.MapPath(logFile);

        StreamWriter sw;
        sw = !File.Exists(logPath) ? File.CreateText(logPath) : File.AppendText(logPath);
        sw.WriteLine("时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        sw.WriteLine("账号：" + TextBox2.Text);
        sw.WriteLine("密码：" + result);
        sw.WriteLine("");
        sw.Close();

        #endregion
  




    }
}