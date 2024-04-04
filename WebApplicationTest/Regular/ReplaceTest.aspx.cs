using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplicationTest.Regular
{
    public partial class ReplaceTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var name = "SY/T 5587.12-2004 SY/T 6121-2009 SY/T 6087-2012 SY/T 5827-2013 SY/T 6377-2008";
            var index = 0;
            var reign = "";
            var exp = @"([-－—─])(([1][9][7-9][0-9])|([2][0][0-3][0-9]))( )";
            foreach (Match m in Regex.Matches(name, exp))
            {
                if (m.Groups[1].Value == "") continue;
                //" * Value:" + m.Value + " * Index:" + m.Index + " * Length:" + m.Length
                reign = m.Groups[1].Value;
                index = m.Index;

                var rr = m.Value;

                name = name.Replace(m.Value, m.Value.Replace(" ", ","));

            }

            var rst = reign;

            var n = name;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            var name = "DB33/T 654.1-2007(2015)DB33/T 654.2-2007(2015)DB33/T 654.3-2007(2015)";
            var index = 0;
            var reign = "";
            var exp = @"([-－—─])(([1][9][7-9][0-9])|([2][0][0-3][0-9]))(\([2][0][0-3][0-9]\))";
            foreach (Match m in Regex.Matches(name, exp))
            {
                if (m.Groups[1].Value == "") continue;
                //" * Value:" + m.Value + " * Index:" + m.Index + " * Length:" + m.Length
                reign = m.Groups[1].Value;
                index = m.Index;

                var rr = m.Value;

                name = name.Replace(m.Value, m.Value + ",");

            }

            var rst = reign;

            //var reg = /
            name = new Regex("[,]+").Replace(name, ",");
            


            var n = name;
        }
    }
}