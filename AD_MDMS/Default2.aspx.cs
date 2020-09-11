using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Default2 : System.Web.UI.Page
{
    AD_InsertControl [] ad = new AD_InsertControl[5];
    protected void Page_Load(object sender, EventArgs e)
    {
        for (int i = 0; i < 2; i++)
        {
            ad[i] = (AD_InsertControl)LoadControl("~/AD_InsertControl.ascx");
            ad[i].CreateStructure("mysql", "khanstore", "cart");
            this.form1.Controls.Add(ad[i]);
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Label1.Text = ad[1].GetInsertQuery();
    }
}
