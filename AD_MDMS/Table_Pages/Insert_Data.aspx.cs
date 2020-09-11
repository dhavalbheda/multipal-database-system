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

public partial class Table_Pages_Insert_Data : System.Web.UI.Page
{
    string providernm, dbname, tableName, QUERY;
    AD_InsertControl[] ad = new AD_InsertControl[100];

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["provider"] != null && Request.QueryString["dbnm"] != null && Request.QueryString["tbnm"] != null)
        {
            providernm = Request.QueryString["provider"].ToString();
            dbname = Request.QueryString["dbnm"].ToString();
            tableName = Request.QueryString["tbnm"].ToString();
            Table_SQL.UseDatabase(providernm, dbname);
            int total=0;
            if (!IsPostBack)
            {
                ViewState["total"] = total = 2;
            }
            else
            {
                total = int.Parse(ViewState["total"].ToString());
            }
            CreateInsertStructure(total);
        }
        else
        {
            Response.Redirect("~/Default.aspx");
        }
    }

    private void CreateInsertStructure(int total)
    {
        for (int i = 0; i < total; i++)
        {
            ad[i] = (AD_InsertControl)LoadControl("~/AD_InsertControl.ascx");
            ad[i].CreateStructure(providernm, dbname, tableName);
            Panel1.Controls.Add(ad[i]);
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

    protected void sql_Button_Click(object sender, EventArgs e)
    {

    }

    protected void insert_Button_Click(object sender, EventArgs e)
    {

    }
    
    protected void operation_Button_Click(object sender, EventArgs e)
    {

    }

    protected void clear_Button_Click(object sender, EventArgs e)
    {
        Color setColor;
        string msg = Table_SQL.CleanTableRecords(providernm, dbname, tableName, out setColor, out QUERY);
        (this.Master as MasterPages_MasterPage).ShowMessageLabel(msg, setColor);
        (this.Master as MasterPages_MasterPage).ShowSQLLabel(QUERY, setColor);
    }

    protected void drop_Button_Click(object sender, EventArgs e)
    {
        Color setColor;
        bool flag = false;
        string msg = Table_SQL.DropTable(providernm, dbname, tableName, out setColor, out QUERY, out flag);
        if (flag)
        {
            Session["SQL_Label"] = QUERY;
            Session["Message_Label"] = msg;
            Response.Redirect("~/database_structure.aspx?provider=" + providernm + "&dbnm=" + dbname);
        }
        else
        {
            (this.Master as MasterPages_MasterPage).ShowMessageLabel(msg, setColor);
            (this.Master as MasterPages_MasterPage).ShowSQLLabel(QUERY, setColor);
        }     
    }

    protected void cancel_Button_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Table_Pages/table_browse.aspx?provider=" + providernm + "&dbnm=" + dbname + "&tbnm=" + tableName);
    }

    protected void Add_Button_Click(object sender, EventArgs e)
    {
        int row = int.Parse(row_TextBox.Text);
        ViewState["total"] = row + int.Parse(ViewState["total"].ToString());
        CreateInsertStructure(row);
    }

    protected void Save_Button_Click(object sender, EventArgs e)
    {
        bool isInsert = false;
        string InsertQuery = "";
        if (providernm.Equals("MSSQL", StringComparison.OrdinalIgnoreCase))
        {
             InsertQuery = "USE ["+dbname+"]";
        }
        else
        {
             InsertQuery = "";
        }
        int total = int.Parse(ViewState["total"].ToString());
        for (int i = 0; i < total; i++)
        {
            if (!ad[i].isInsert)
            {
                InsertQuery += ad[i].GetInsertQuery();
                isInsert = true;
            }
        }
        if (isInsert)
        {
            Color setColor;
            string msg = Table_SQL.InsertRecords(providernm, dbname, tableName, InsertQuery, out setColor);
            Session["SQL_Label"] = InsertQuery;
            Session["Message_Label"] = msg;
            Response.Redirect("~/Table_Pages/table_browse.aspx?provider=" + providernm + "&dbnm=" + dbname + "&tbnm=" + tableName);
        }
        else
        {
            (this.Master as MasterPages_MasterPage).ShowMessageLabel("Please Insert Any Record...", Color.Red);
        }
    }
}
