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

public partial class Database : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(bool)Session["IsConfigured"])
        {
            Response.Redirect("~/DatabaseConfiguration.aspx");
        }
        Response.Redirect("CreateDatabase.aspx");
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("CreateDatabase.aspx");
    }
    protected void SQL_Button_Click(object sender, EventArgs e)
    {
        Response.Redirect("SQL.aspx");
    }

    protected void Import_Button_Click(object sender, EventArgs e)
    {
        Response.Redirect("DatabaseImport.aspx");
    }
    protected void Export_Button_Click(object sender, EventArgs e)
    {
        Response.Redirect("database_export.aspx");
    }
}
