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

public partial class RunSQL_Database : System.Web.UI.Page
{
    string providernm, dbname,QUERY;
    GlobalFunction gf = new GlobalFunction();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["provider"] != null && Request.QueryString["dbnm"] != null)
        {
            providernm = Request.QueryString["provider"].ToString();
            dbname = Request.QueryString["dbnm"].ToString();
            Session["dbname"] = dbname;
            if (!IsPostBack)
            {
                LoadDropDownList();
            }
            else
            {
                SelectDropdownListValue();
            }
        }
        else
        {
            Response.Redirect("~/Default.aspx");
        }
    
    }

    private void LoadDropDownList()
    {
        DataTable dt = (DataTable)Session["Providers"];
        Provider_DropDownList.Items.Clear();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            Provider_DropDownList.Items.Add(dt.Rows[i][0].ToString());
        }
        SelectDropdownListValue();
        Provider_DropDownList.Enabled = false;
    }

    private void SelectDropdownListValue()
    {
        for (int i = 0; i < Provider_DropDownList.Items.Count; i++)
        {
            if (Provider_DropDownList.Items[i].Text == providernm)
            {
                Provider_DropDownList.Items[i].Selected = true;
            }
        }
    }

    protected void structure_Button_Click(object sender, EventArgs e)
    {
        Response.Redirect("database_structure.aspx?provider=" + providernm + "&dbnm=" + dbname);
    }

    protected void SQL_Button_Click(object sender, EventArgs e)
    {
        Response.Redirect("RunSQL_Database.aspx?provider=" + providernm + "&dbnm=" + dbname);
    }

    protected void search_Button_Click(object sender, EventArgs e)
    {
        Response.Redirect("database_search.aspx?provider=" + providernm + "&dbnm=" + dbname);
    }

    protected void Operation_Button_Click(object sender, EventArgs e)
    {
        Response.Redirect("operation.aspx?provider=" + providernm + "&dbnm=" + dbname);
    }

    protected void import_Button_Click(object sender, EventArgs e)
    {
        Response.Redirect("import.aspx?provider=" + providernm + "&dbnm=" + dbname);
    }

    protected void export_Button_Click(object sender, EventArgs e)
    {
        Response.Redirect("database_export.aspx?provider=" + providernm + "&dbnm=" + dbname);
    }

    protected void drop_Button_Click(object sender, EventArgs e)
    {
        Color setColor;
        string msg = SQL.DeleteDatabases(providernm, dbname, out setColor, out QUERY);
        (this.Master as MasterPages_MasterPage).ShowMessageLabel(msg, setColor);
        (this.Master as MasterPages_MasterPage).ShowSQLLabel(QUERY, setColor);
        (this.Master as MasterPages_MasterPage).Refresh_sidebar();
        Response.Redirect("CreateDatabase.aspx");
    }

    protected void Go_Button_Click(object sender, EventArgs e)
    {
        if (Records_GridView.Visible)
        {
            Records_GridView.Visible = false;
        }
        Color color;
        dbname = Session["dbname"].ToString();
        string message,query;
        DataTable dt = new DataTable();
        bool hasRecords = false;
        if (Provider_DropDownList.Text.Equals("MYSQL"))
        {
            query = "use `" + dbname + "`;";
        }
        else
        {
            query = "use \"" + dbname + "\";";
        }
        query += Query_TextBox.Text.Trim();

        if (!string.IsNullOrEmpty(Query_TextBox.Text))
        {
            message = SQL.RunSQL(Provider_DropDownList.Text,query, out color,out dt);
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
    }
}
