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
using System.Drawing;

public partial class RunSQL : System.Web.UI.Page
{
    GlobalFunction gf = new GlobalFunction();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadDropDownList();
        }
    }

    private void LoadDropDownList()
    {
        DataTable dt = gf.GetDBProvider();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            Provider_DropDownList.Items.Add(dt.Rows[i][0].ToString());
        }
    }

    protected void Database_Click(object sender, EventArgs e)
    {
        Response.Redirect("CreateDatabase.aspx");
    }

    protected void SQL_Button_Click(object sender, EventArgs e)
    {
        Response.Redirect("RunSQL.aspx");
    }

    protected void Import_Button_Click(object sender, EventArgs e)
    {
        Response.Redirect("DatabaseImport.aspx");
    }

    protected void Export_Button_Click(object sender, EventArgs e)
    {
        Response.Redirect("DatabaseExport.aspx");
    }
    protected void Go_Button_Click(object sender, EventArgs e)
    {
        if (Records_GridView.Visible)
            Records_GridView.Visible = false;
        Color color;
        string message;
        DataTable dt = new DataTable();
        bool hasRecords = false;

        if (!string.IsNullOrEmpty(Query_TextBox.Text))
        {
            message = SQL.RunSQL(Provider_DropDownList.Text, Query_TextBox.Text.Trim(), out color,out dt);
            if (dt.Rows.Count > 0)
                hasRecords = true;
        }
        else
        {
            message = "Please Enter the Query. :(";
            color = Color.Red;
        }
        if (hasRecords)
        {
            Records_GridView.Visible = true;
            Records_GridView.DataSource = dt;
            Records_GridView.DataBind();
        }
        (this.Master as MasterPages_MasterPage).ShowMessageLabel(message, Color.Black);
        (this.Master as MasterPages_MasterPage).Refresh_sidebar();
    }
}
