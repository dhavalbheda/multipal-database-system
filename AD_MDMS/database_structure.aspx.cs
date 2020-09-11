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
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;

public partial class Database_structure : System.Web.UI.Page
{
    string providernm, dbname, tableName,QUERY;
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["Message_Label"] != null) && (Session["SQL_Label"] != null) )
        {
            Color setColor = Color.Green;
            (this.Master as MasterPages_MasterPage).ShowMessageLabel(Session["Message_Label"].ToString(), setColor);
            (this.Master as MasterPages_MasterPage).ShowSQLLabel(Session["SQL_Label"].ToString(), setColor);
            Session["Message_Label"] = null;
            Session["SQL_Label"] = null;
        }
        if (Request.QueryString["provider"] != null && Request.QueryString["dbnm"] != null)
        {
            providernm = Request.QueryString["provider"].ToString();
            dbname = Request.QueryString["dbnm"].ToString();
            if (!IsPostBack)
            {
                SQL.UseDatabase(providernm, dbname);
                LoadTableDataGridView();
            }
        }
        else
        {
            Response.Redirect("~/Default.aspx");
        }
    }

    private void LoadTableDataGridView()
    {
        DataTable dt = SQL.ShowTables(providernm, dbname);
        TablesGridView.DataSource = dt;
        TablesGridView.DataBind();
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
        Response.Redirect("database_operation.aspx?provider=" + providernm + "&dbnm=" + dbname);
    }

    protected void import_Button_Click(object sender, EventArgs e)
    {
        Response.Redirect("database_import.aspx?provider=" + providernm + "&dbnm=" + dbname);
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
   
    protected void TablesGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        tableName = TablesGridView.Rows[index].Cells[1].Text;
        
        if (e.CommandName == "Brows_Button")
        {           
                        
        }
        else if (e.CommandName == "Structure_Button")
        {
            
        }
        else if (e.CommandName == "Insert_Button")
        {
            
        }
        else if (e.CommandName == "Clean_Button")
        {
            Color setColor;
            string msg = SQL.CleanTableRecords(providernm, dbname, tableName, out setColor,out QUERY);
            (this.Master as MasterPages_MasterPage).ShowMessageLabel(msg, setColor);
            (this.Master as MasterPages_MasterPage).ShowSQLLabel(QUERY, setColor);
            LoadTableDataGridView();
        }
        else if (e.CommandName == "Drop_Button")
        {
            Color setColor;
            string msg = SQL.DeleteTable(providernm, dbname, tableName, out setColor, out QUERY);
            (this.Master as MasterPages_MasterPage).ShowMessageLabel(msg, setColor);
            (this.Master as MasterPages_MasterPage).ShowSQLLabel(QUERY, setColor);
            LoadTableDataGridView();
            (this.Master as MasterPages_MasterPage).Refresh_sidebar();
        }
    }

    protected void TableCreate_Button_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/CreateTables.aspx?provider=" +providernm + "&dbnm=" + dbname+"&TableName=" + TableName_TextBox.Text + "&Filed=" + Field_TextBox.Text);
        (this.Master as MasterPages_MasterPage).Refresh_sidebar();
    }
}
