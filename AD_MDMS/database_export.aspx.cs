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
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Web.Configuration;

public partial class database_export : System.Web.UI.Page
{
    string providernm, dbname, tableName, QUERY;
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

        Label2.Text = "Export Database " + dbname + " as : ";
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
    protected void export_file_Button_Click1(object sender, EventArgs e)
    {
        string ExportPath = "";
        string FinalFileExt = "";

        if (file_TextBox.Text == "")
        {
            (this.Master as MasterPages_MasterPage).ShowMessageLabel("File Name Required", Color.Red);
        }
        else
        {
            /************This Code is For Export Database to our Export Database folder*************/
            if (providernm.Equals("MYSQL", StringComparison.OrdinalIgnoreCase))
            {
                string user = ConfigurationManager.AppSettings["mysql_user"].ToString();
                string password = ConfigurationManager.AppSettings["mysql_pwd"].ToString();

                FinalFileExt = ".sql";

                string destPath = MapPath("~/ExportDatabase") + "\\" + file_TextBox.Text + ".sql";

                var processInfo = new ProcessStartInfo();
                processInfo.WorkingDirectory = ConfigurationManager.AppSettings["xampp_path"].ToString() + "/mysql/bin";
                processInfo.FileName = "cmd.exe";

                //Concating Command will be written inside cmd
                string arguments = "/c mysqldump --add-drop-table -u " + user;
                if (!string.IsNullOrEmpty(password))
                    arguments += " -p " + password;
                arguments += " " + dbname + " > \"" + destPath + "\"";

                processInfo.Arguments = arguments;
                processInfo.WindowStyle = ProcessWindowStyle.Hidden;

                Process p = new Process();
                p.StartInfo = processInfo;
                p.Start();
                p.WaitForExit();
                p.Close();
            }
            else if (providernm.Equals("MSSQL", StringComparison.OrdinalIgnoreCase))
            {
                FinalFileExt = ".bak";

                string destPath = MapPath("~/ExportDatabase" + "\\" + file_TextBox.Text + ".bak");

            }
            ExportPath = MapPath("~/ExportDatabase/" + file_TextBox.Text + FinalFileExt);

            Color color;
            /************The Below Function is To Send the .sql file to client*********************/
            string msg = SQL.ExportDatabase(providernm, dbname, ExportPath, out color, Response);

            (this.Master as MasterPages_MasterPage).ShowMessageLabel(msg, color);
        }
    }
}
