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
using System.Diagnostics;
using System.IO;

public partial class database_operation : System.Web.UI.Page
{
    string providernm, tableName, QUERY;
    public string dbname;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["provider"] != null && Request.QueryString["dbnm"] != null)
        {
            providernm = Request.QueryString["provider"].ToString();
            dbname = Request.QueryString["dbnm"].ToString();
            Page.DataBind();
            if (!IsPostBack)
            {
                SQL.UseDatabase(providernm, dbname);
            }
            if (providernm.Equals("MSSQL", StringComparison.OrdinalIgnoreCase))
            {
                structure_RadioButton.Enabled = false;
                structure_RadioButton.Checked = false;
                structure_data_RadioButton.Checked = true;
            }
        }
        else
        {
            Response.Redirect("~/Default.aspx");
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

    protected void TableCreate_Button_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/CreateTables.aspx?provider=" + providernm + "&dbnm=" + dbname + "&TableName=" + TableName_TextBox.Text + "&Filed=" + Field_TextBox.Text);
    }

    protected void rename_Button_Click(object sender, EventArgs e)
    {
        bool dbAvailable = IsDatabaseAvailable(Rename_TextBox.Text);

        if (dbAvailable)
        {
            GlobalFunction.ShowMessage(Page, "Database Already Exists");
            (this.Master as MasterPages_MasterPage).ShowMessageLabel("", Color.Red);
        }
        else
        {
            Color color;
            string msg = SQL.RenameDatabase(providernm, dbname, Rename_TextBox.Text, out color);

            (this.Master as MasterPages_MasterPage).ShowMessageLabel(msg, color);
            if (color == Color.Green)
                (this.Master as MasterPages_MasterPage).Refresh_sidebar();
        }

    }

    protected void copy_Button_Click(object sender, EventArgs e)
    {
        bool dbAvailable = IsDatabaseAvailable(copy_TextBox.Text);

        if (dbAvailable)
        {
            GlobalFunction.ShowMessage(Page, "Database Already Exists");
            (this.Master as MasterPages_MasterPage).ShowMessageLabel("", Color.Red);
        }
        else
        {
            Color color = Color.Red;
            string msg = "";

            if (structure_RadioButton.Checked)
            {
                msg = SQL.Copy_DatabaseStructure(providernm, dbname, copy_TextBox.Text, out color);
            }
            else if (structure_data_RadioButton.Checked)
            {
                msg = SQL.CopyDatabase(Page, providernm, dbname, copy_TextBox.Text, out color);
            }

            (this.Master as MasterPages_MasterPage).ShowMessageLabel(msg, color);

            if(color == Color.Green)
                (this.Master as MasterPages_MasterPage).Refresh_sidebar();
        }
    }

    private bool IsDatabaseAvailable(string dbnm)
    {
        TreeView tv = (TreeView)Master.FindControl("Database_TreeView");
        TreeNodeCollection collection = tv.Nodes;
        TreeNode ProviderNode = tv.FindNode(providernm);
        int provider_Index = collection.IndexOf(ProviderNode);

        bool dbAvailable = false;
        for (int i = 0; i < tv.Nodes[provider_Index].ChildNodes.Count; i++)
        {
            if (tv.Nodes[provider_Index].ChildNodes[i].Value.Equals(dbnm, StringComparison.OrdinalIgnoreCase))
            {
                dbAvailable = true;
                break;
            }
        }
        return dbAvailable;
    }
}