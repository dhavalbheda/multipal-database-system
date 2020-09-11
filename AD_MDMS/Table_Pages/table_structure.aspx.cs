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

public partial class Tables_pages_table_structure : System.Web.UI.Page
{
    string providernm, dbname, tableName, QUERY;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["provider"] != null && Request.QueryString["dbnm"] != null && Request.QueryString["tbnm"] != null)
        {
            providernm = Request.QueryString["provider"].ToString();
            dbname = Request.QueryString["dbnm"].ToString();
            tableName = Request.QueryString["tbnm"].ToString();
            if (!IsPostBack)
            {
                Table_SQL.UseDatabase(providernm, dbname);
                LoadDataGridView();
            }
        }
        else
        {
            Response.Redirect("~/Default.aspx");
        }
    }

    private void LoadDataGridView()
    {
        DataTable dt = Table_SQL.showTableStructure(providernm, dbname, tableName);
        if (dt.Rows.Count > 0)
        {
            table_structure_GridView.DataSource = dt;
            table_structure_GridView.DataBind();
        }
        else
        {
            (this.Master as MasterPages_MasterPage).ShowMessageLabel("No Columns Available.", Color.Crimson);
        }
    }
    protected void browse_Button_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Table_Pages/table_browse.aspx?provider=" + providernm + "&dbnm=" + dbname + "&tbnm=" + tableName);
    }

    protected void structure_Button_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Table_Pages/table_structure.aspx?provider=" + providernm + "&dbnm=" + dbname + "&tbnm=" + tableName);
    }
    protected void insert_Button_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Table_Pages/insert_Data.aspx?provider=" + providernm + "&dbnm=" + dbname + "&tbnm=" + tableName);
    }
}
