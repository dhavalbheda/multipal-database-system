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
using MySql.Data.MySqlClient;
using System.IO;

public partial class database_import : System.Web.UI.Page
{
    string providernm, dbname, QUERY;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["provider"] != null && Request.QueryString["dbnm"] != null)
        {
            providernm = Request.QueryString["provider"].ToString();
            dbname = Request.QueryString["dbnm"].ToString();
        }
        else
        {
            Response.Redirect("~/Default.aspx");
        }

    }

    protected void structure_Button_Click(object sender, EventArgs e)
    {
        Response.Redirect("database_structure.aspx");
    }

    protected void SQL_Button_Click(object sender, EventArgs e)
    {
        Response.Redirect("RunSQL_Database.aspx?provider=" + providernm + "&dbnm=" + dbname);
    }

    protected void search_Button_Click(object sender, EventArgs e)
    {
        Response.Redirect("database_search.aspx");
    }

    protected void Operation_Button_Click(object sender, EventArgs e)
    {
        Response.Redirect("operation.aspx");
    }

    protected void import_Button_Click(object sender, EventArgs e)
    {
        Response.Redirect("import.aspx");
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

    protected void importDB_Button_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            string msg = "";
            Color color = Color.Black;
            string query = "";
            string importPath = "";

            if (providernm.Equals("MYSQL", StringComparison.OrdinalIgnoreCase))
            {
                using (StreamReader reader = new StreamReader(FileUpload1.PostedFile.InputStream))
                {
                    query = reader.ReadToEnd();
                }
                msg = SQL.ImportMYSQLDatabase(providernm, query, out color);
            }
            else if (providernm.Equals("MSSQL", StringComparison.OrdinalIgnoreCase))
            {
                string destPath = MapPath("~/ImportDatabase/");
                FileUpload1.SaveAs(destPath + FileUpload1.FileName);


                importPath = destPath + FileUpload1.FileName;

                msg = SQL.ImportMSSQLDatabase(providernm, dbname, importPath, out color);
            }

            (this.Master as MasterPages_MasterPage).ShowMessageLabel(msg, color);

            if (File.Exists(importPath))
            {
                File.Delete(importPath);
            }
        }
    }
}
