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

public partial class CreateDatabase : BasePage
{
    GlobalFunction gf = new GlobalFunction();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadDropDownList();
            LoadDataGridView();
            MultiView1.ActiveViewIndex = 0;
        }
        Session["LoadSidebar"] = false ;
    }

    private void LoadDropDownList()
    {
        DataTable dt = (DataTable)Session["Providers"];
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            Provider_DropDownList.Items.Add(dt.Rows[i][0].ToString());
            Provider_DropDownList2.Items.Add(dt.Rows[i][0].ToString());
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
        Response.Redirect("database_export.aspx");
    }

    protected void CreateDatabase_Button_Click(object sender, EventArgs e)
    {
        Color setColor;
        string msg = SQL.CreateDatabases(Provider_DropDownList.SelectedItem.Text, DatabaseName_TextBox.Text, out setColor);
        (this.Master as MasterPages_MasterPage).ShowMessageLabel(msg,setColor);
        (this.Master as MasterPages_MasterPage).ShowSQLLabel("Create Database " + DatabaseName_TextBox.Text + ";", setColor);
        LoadDataGridView();
        DatabaseName_TextBox.Text = "";
        (this.Master as MasterPages_MasterPage).Refresh_sidebar();
    }

    private void LoadDataGridView()
    {
        DataTable dt = gf.ShowDatabase(Provider_DropDownList2.SelectedItem.Text);
        Database_GridView.DataSource = dt;
        Database_GridView.DataBind();
        Database_GridView.Visible = true;
        Delete_Button.Visible = true;
    }

    protected void Delete_Button_Click(object sender, EventArgs e)
    {
        string query="";
        for (int rows = 0; rows < Database_GridView.Rows.Count; rows++)
        {
            CheckBox c1 = (CheckBox)Database_GridView.Rows[rows].FindControl("delete_CheckBox");
            if (c1.Checked)
            {
                if (Provider_DropDownList2.SelectedItem.Text.Equals("mysql", StringComparison.OrdinalIgnoreCase))
                {
                    query += "DROP DATABASE `" + Database_GridView.Rows[rows].Cells[1].Text + "`;\r\n";
                }
                else if(Provider_DropDownList2.SelectedItem.Text.Equals("mssql",StringComparison.OrdinalIgnoreCase))
                {
                    query += "DROP DATABASE \"" + Database_GridView.Rows[rows].Cells[1].Text + "\";\r\n";
                }
            }
        }

        if (!string.IsNullOrEmpty(query))
        {
            MultiView1.ActiveViewIndex = 1;
            Query_TextBox.Text = query;
        }
        else
        {
            (this.Master as MasterPages_MasterPage).ShowMessageLabel("Please Select Database !...", Color.Red);
        }
    }

    protected void yes_Button_Click(object sender, EventArgs e)
    {
        Color setColor;
        string msg = SQL.DeleteDatabase_batch(Provider_DropDownList2.SelectedItem.Text, Query_TextBox.Text, out setColor);
        (this.Master as MasterPages_MasterPage).ShowMessageLabel(msg, setColor);
        (this.Master as MasterPages_MasterPage).ShowSQLLabel(msg, setColor);
        LoadDataGridView();
        MultiView1.ActiveViewIndex = 0;
        (this.Master as MasterPages_MasterPage).Refresh_sidebar();

    }

    protected void no_Button_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 0;
    }

    protected void Provider_DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadDataGridView();
    }
}
