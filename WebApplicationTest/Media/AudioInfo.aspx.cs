using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hsp.Test.Common;
using Shell32;


namespace WebApplicationTest.Media
{
    public partial class AudioInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAudioInfo_Click(object sender, EventArgs e)
        {
            var audioPath = @"E:\Videos\射雕英雄传.mp3";
            audioPath = @"E:\Music\APE\红楼梦 - 枉凝眉.ape"; // 获取不到

            //Hsp.Test.Common.Mp3FileInfo mp3 = new Mp3FileInfo(audioPath);

            ShellClass sh = new ShellClass();
            Folder dir = sh.NameSpace(Path.GetDirectoryName(audioPath));
            FolderItem item = dir.ParseName(Path.GetFileName(audioPath));
            string str = dir.GetDetailsOf(item, 27); // 获取歌曲时长。
            lblAudioInfo.Text = "音频时长：" + str + "\n";


        }
    }
}

//然后出现了这个：
//未经处理的异常:  System.InvalidCastException: 无法将类型为“Shell32.ShellClass”的 COM 对象强制转换为接口类型“Shell32.IShellDispatch6”。
//此操作失败的原因是对 IID 为“{286E6F1B-7113-4355-9562-96B7E9D64C54}”的接口的 COM 组件调用 QueryInterface 因以下错误而失败: 
//不支持此接口 (异常来自 HRESULT:0x80004002 (E_NOINTERFACE))。
//这个是因为vs自带的Interop.Shell32.dll版本是1.0 太老了，于是在网上找到了1.2版本的，然后改成引用1.2版本。
//再将引用属性里面的 嵌入互操作类型  改为false 就行了
//dll的下载地址：http://download.csdn.net/detail/u013529927/8812075
