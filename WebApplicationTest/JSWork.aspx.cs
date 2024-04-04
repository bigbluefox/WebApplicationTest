using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplicationTest
{
    public partial class JSWork : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var i = Math.Ceiling(50.0/10.0);


            Label1.Text = i.ToString();
        }
    }
}