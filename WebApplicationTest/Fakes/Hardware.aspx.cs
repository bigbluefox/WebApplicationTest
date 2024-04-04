using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplicationTest.Fakes
{
    public partial class Hardware : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 硬件信息按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnHardware_Click(object sender, EventArgs e)
        {
            GetInfo();
        }

        private void GetInfo()
        {
            string cpuInfo = "";//cpu序列号 
            ManagementClass cimobject = new ManagementClass("Win32_Processor");
            ManagementObjectCollection moc = cimobject.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                cpuInfo = mo.Properties["ProcessorId"].Value.ToString();
                Response.Write("cpu序列号：" + cpuInfo.ToString() + "<br />");
            }

            //获取硬盘ID 
            String HDid;
            ManagementClass cimobject1 = new ManagementClass("Win32_DiskDrive");
            ManagementObjectCollection moc1 = cimobject1.GetInstances();
            foreach (ManagementObject mo in moc1)
            {
                HDid = (string)mo.Properties["Model"].Value;
                Response.Write("硬盘序列号：" + HDid.ToString() + "<br />");
            }
            //获取网卡硬件地址 
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc2 = mc.GetInstances();
            foreach (ManagementObject mo in moc2)
            {
                if ((bool)mo["IPEnabled"] == true)
                    Response.Write("MAC address\t{0}" + mo["MacAddress"].ToString() + "<br />");
                mo.Dispose();
            }

            Response.Write("<br />");

            var stringMAC = String.Empty;
            var stringIP = String.Empty;

            foreach (ManagementObject mo in moc2)
            {
                if ((bool) mo["IPEnabled"] != true) continue;
                stringMAC += "<br />" + mo["MACAddress"].ToString();

                string[] IPAddresses = (string[])mo["IPAddress"];
                if (IPAddresses.Length > 0)
                    stringIP += "<br />" + IPAddresses[0];
            }

            lblHardware.Text = stringMAC.ToString();
            lblHardware.Text += stringIP.ToString();

        }
    }
}