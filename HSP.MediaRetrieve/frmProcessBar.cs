using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HSP.MediaRetrieve
{
    public partial class frmProcessBar : Form
    {
        public frmProcessBar()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Increase process bar
        /// </summary>
        /// <param name="nValue">the value increased</param>
        /// <returns></returns>
        public bool Increase(int nValue)
        {
            if (nValue > 0)
            {
                if (progressBar1.Value + nValue < progressBar1.Maximum)
                {
                    progressBar1.Value += nValue;
                    return true;
                }
                else
                {
                    progressBar1.Value = progressBar1.Maximum;
                    this.Close();
                    return false;
                }
            }
            return false;
        }
    }
}
