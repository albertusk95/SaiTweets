using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TestASPNETv0._0
{
    public partial class formstart : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ExtractTweet_Click(object sender, EventArgs e)
        {
            Page.Server.Transfer("formtarget.aspx");
        }

        protected void Analisis_Click(object sender, EventArgs e)
        {
            Page.Server.Transfer("formpreanalisis.aspx");
        }
        
    }
}