using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hsp.Test.Common;

namespace WebApplicationTest
{
    public partial class UtcTimeTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnResult_Click(object sender, EventArgs e)
        {
            var strUtcTime = txtUTCTime.Text;
            if(string.IsNullOrEmpty(strUtcTime)) return;

            long lUtcTime = 0L;
            long.TryParse(strUtcTime, out lUtcTime);

            if (lUtcTime == 0) return;
            
            var datetime = Utility.ConvertTimeStampToDateTime(lUtcTime);
            txtDate.Text = datetime.ToString("yyyy-MM-dd");
            txtDateTime.Text = datetime.ToString("yyyy-MM-dd HH:mm:ss");

        }
    }
}