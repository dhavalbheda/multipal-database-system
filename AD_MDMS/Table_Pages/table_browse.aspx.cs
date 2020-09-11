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
using System.Web.UI.MobileControls;
using System.Collections.Generic;

public partial class table_browse : System.Web.UI.Page
{
    string providernm, dbname, tableName,QUERY;
    DataTable GridView_Table;

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["Message_Label"] != null) && (Session["SQL_Label"] != null))
        {
            Color setColor = Color.Green;
            (this.Master as MasterPages_MasterPage).ShowMessageLabel(Session["Message_Label"].ToString(), setColor);
            (this.Master as MasterPages_MasterPage).ShowSQLLabel(Session["SQL_Label"].ToString(), setColor);
            Session["Message_Label"] = null;
            Session["SQL_Label"] = null;
        }
        if(ViewState["GridView_Table"]!=null)
        {
            GridView_Table=(DataTable)ViewState["GridView_Table"];
        }
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

    public void LoadDataGridView()
    {
        GridView_Table = Table_SQL.showRecords(providernm,dbname,tableName,out QUERY);
        ViewState["GridView_Table"]=GridView_Table;
        Table_GridView.DataSource = GridView_Table;
        Table_GridView.DataBind();
        if (GridView_Table.Rows.Count > 0)
        {
            (this.Master as MasterPages_MasterPage).ShowMessageLabel("Query Executed Succesfully...", Color.Green);
            (this.Master as MasterPages_MasterPage).ShowSQLLabel(QUERY, Color.Green);
        }
        else
        {
            Response.Redirect("~/Table_Pages/table_structure.aspx?provider=" + providernm + "&dbnm=" + dbname + "&tbnm=" + tableName);
        }
    }

    public void UpdateDataGridView()
    {
        GridView_Table = Table_SQL.showRecords(providernm, dbname, tableName, out QUERY);
        ViewState["GridView_Table"] = GridView_Table;
        Table_GridView.DataSource = GridView_Table;
        Table_GridView.DataBind();
    }

    private string MsSQL_DeleteStatement(int index)
    {
        string DeleteQuery = "DELETE TOP(1) FROM " + tableName + " WHERE ";
        List<string> headers = new List<string>();
        for (int i = 0; i < GridView_Table.Columns.Count; i++)
        {
            DeleteQuery += GridView_Table.Columns[i].ColumnName + " = '" + GridView_Table.Rows[index][i].ToString() + "'";
            if (i + 1 < GridView_Table.Columns.Count)
            {
                DeleteQuery += " AND";
            }
            else
            {
                DeleteQuery += ";";
            }
        }
        return DeleteQuery;
    }

    private string Mysql_DeleteStatement(int index)
    {
        string DeleteQuery = "DELETE FROM `" + tableName + "` WHERE";
        List<string> headers = new List<string>();
        for (int i = 0; i < GridView_Table.Columns.Count; i++)
        {
            DeleteQuery += " `" + GridView_Table.Columns[i].ColumnName + "` = '" + GridView_Table.Rows[index][i].ToString() + "'";
            if (i + 1 < GridView_Table.Columns.Count)
            {
                DeleteQuery += " AND";
            }
            else
            {
                DeleteQuery += " LIMIT 1;\r\n";
            }
        }
        return DeleteQuery;
    }

    protected void Table_GridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        int index = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "Delete_Button")
        {
            if (providernm.Equals("MYSQL", StringComparison.OrdinalIgnoreCase))
            {
                string DeleteQuery = Mysql_DeleteStatement(index);
                Color setColor;
                string msg = Table_SQL.DeleteRecords(providernm, dbname, DeleteQuery, out setColor);
                (this.Master as MasterPages_MasterPage).ShowMessageLabel(msg, setColor);
                (this.Master as MasterPages_MasterPage).ShowSQLLabel(DeleteQuery, setColor);
                UpdateDataGridView();
            }
            else if (providernm.Equals("MsSQL", StringComparison.OrdinalIgnoreCase))
            {
                string DeleteQuery = MsSQL_DeleteStatement(index);
                Color setColor;
                string msg = Table_SQL.DeleteRecords(providernm, dbname, DeleteQuery, out setColor);
                (this.Master as MasterPages_MasterPage).ShowMessageLabel(msg, setColor);
                (this.Master as MasterPages_MasterPage).ShowSQLLabel(DeleteQuery, setColor);
                UpdateDataGridView();
            }
        }
    }

    protected void MultipleDeleteButton_Click(object sender, EventArgs e)
    {
        QUERY = "";
        for (int index = 0; index < Table_GridView.Rows.Count; index++)
        {
            CheckBox c1 = (CheckBox)Table_GridView.Rows[index].FindControl("Select_CheckBox");
            if (c1.Checked)
            {
                if (providernm.Equals("mysql", StringComparison.OrdinalIgnoreCase))
                {
                    QUERY += Mysql_DeleteStatement(index);
                }
                else if (providernm.Equals("mssql", StringComparison.OrdinalIgnoreCase))
                {
                    QUERY += MsSQL_DeleteStatement(index);
                }
            }
        }
        Color setColor;
        string msg = Table_SQL.DeleteRecords(providernm, dbname, QUERY, out setColor);
        (this.Master as MasterPages_MasterPage).ShowMessageLabel(msg, setColor);
        (this.Master as MasterPages_MasterPage).ShowSQLLabel(QUERY, setColor);
        UpdateDataGridView();
    }

    protected void structure_Button_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Table_Pages/table_structure.aspx?provider="+providernm+"&dbnm="+dbname+"&tbnm="+tableName);
    }

    protected void sql_Button_Click(object sender, EventArgs e)
    {

    }

    protected void insert_Button_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Table_Pages/insert_Data.aspx?provider=" + providernm + "&dbnm=" + dbname + "&tbnm=" + tableName);
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
        LoadDataGridView();
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

}
