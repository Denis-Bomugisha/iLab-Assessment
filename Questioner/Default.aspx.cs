using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Questioner
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void AssessBtn_Click(object sender, EventArgs e)
        {
            Server.Transfer("start.aspx");
        }

        protected void SB_Btn_Click(object sender, EventArgs e)
        {
            Response.Redirect("http://ilabs.mak.ac.ug/files/DSSSLabManual.pdf");
        }

        protected void DwnLab_Click(object sender, EventArgs e)
        {
           Response.Redirect("http://ilabs.mak.ac.ug/files/DSSSiLab.rar");
        }

        
        

        
    }
}