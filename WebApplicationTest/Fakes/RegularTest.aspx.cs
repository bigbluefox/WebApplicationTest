using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplicationTest.Fakes
{
    public partial class RegularTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnTest_Click(object sender, EventArgs e)
        {
            var txt = txtContent.Text;
            var rst = Regex.Replace(txt, "\\D+", ""); // "\\D+"

            lblResult.Text = rst;
        }
    }
}