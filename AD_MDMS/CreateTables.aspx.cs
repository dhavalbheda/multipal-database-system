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

public partial class CreateTables : System.Web.UI.Page
{
    string tableName,providernm,dbname;
    int Filed;
    ADControl[] AD = new ADControl[5];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["TableName"] != null && Request.QueryString["Filed"] != null)
        {
            providernm = Server.UrlDecode(Request.QueryString["provider"].ToString());
            dbname = Request.QueryString["dbnm"].ToString();
            if (!IsPostBack)
            {
                SQL.UseDatabase(providernm, dbname);
            }
            tableName = Request.QueryString["TableName"].ToString();
            TableName_Lable.Text = "Table Name : " + tableName;
            Filed = int.Parse(Request.QueryString["Filed"]);
            CreateStructure();
        }
        else
        {
            Response.Redirect("~/Default.aspx");
        }
    }

    private void CreateStructure()
    {
        
        for (int i = 0; i < Filed; i++)
        {
            AD[i] = (ADControl)LoadControl("~/ADControl.ascx");
            if (providernm.Equals("MYSQL", StringComparison.OrdinalIgnoreCase))
            {
                AD[i].DatabaseProvider = GlobalFunction.Database.MYSQL;
            }
            else if (providernm.Equals("MSSQL", StringComparison.OrdinalIgnoreCase))
            {
                AD[i].DatabaseProvider = GlobalFunction.Database.MSSQL;
            }
            
            AD[i].ShowLabel = true;
            ContentPanel.Controls.Add(AD[i]);
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (providernm.Equals("MYSQL", StringComparison.OrdinalIgnoreCase))
        {
            string Query, filedName, datatype,primary_Column="";
            bool auto_increment = false;
            Query = "CREATE TABLE `" + tableName + "` (";

            for (int i = 0; i < Filed; i++)
            {
                filedName = AD[i].FieldName.ToString();
                datatype = AD[i].DataType;
                Query += filedName + " " + datatype;
                if (!(AD[i].FieldSize.ToString().Equals("")))
                {
                    Query += " (" + AD[i].FieldSize.ToString() + ")";
                }
                if (AD[i].AllowDefaultValue)
                {
                    Query += " DEFAULT '" + AD[i].DefaultValue.ToString() + "'";
                }
                if (AD[i].AllowNull)
                {
                    Query += " NULL";
                }
                if (!(AD[i].AllowNull))
                {
                    Query += " NOT NULL";
                }
                if (AD[i].AutoIncrement)
                {
                    auto_increment = true;
                    Query += " AUTO_INCREMENT";
                    if (primary_Column.Equals(""))
                    {
                        primary_Column = filedName;
                    }
                }
                if (!(i + 1 == Filed))
                {
                    Query += ",";
                }
            }
            if (auto_increment)
            {
                Query += ", PRIMARY KEY(" + primary_Column + ")";
            }
            Query += ");";
            bool flag = false;
            string msg = SQL.CreateTable("MySQL", Query, out flag);
            if (flag)
            {
                Session["SQL_Label"] = Query;
                Session["Message_Label"] = msg;
                Response.Redirect("database_structure.aspx?provider=" + providernm + "&dbnm=" + dbname);
            }
            else
            {
                (this.Master as MasterPages_MasterPage).ShowMessageLabel(msg.ToString(), Color.Red);
                (this.Master as MasterPages_MasterPage).ShowSQLLabel(Query.ToString(), Color.Red);
            }
        }
        else if (providernm.Equals("MSSQL", StringComparison.OrdinalIgnoreCase))
        {
            string Query, filedName, datatype;
            Query = "CREATE TABLE " + tableName + " (";

            for (int i = 0; i < Filed; i++)
            {
                filedName = AD[i].FieldName.ToString();
                datatype = AD[i].DataType;
                Query += filedName + " " + datatype;
                if (!(AD[i].FieldSize.ToString().Equals("")))
                {
                    Query += " (" + AD[i].FieldSize.ToString() + ")";
                }
                if (AD[i].AllowDefaultValue)
                {
                    Query += " DEFAULT '" + AD[i].DefaultValue.ToString() + "'";
                }
                if (AD[i].AllowNull)
                {
                    Query += " NULL";
                }
                if (!(AD[i].AllowNull))
                {
                    Query += " NOT NULL";
                }
                if (AD[i].AutoIncrement)
                {
                    Query += " IDENTITY(1,1) PRIMARY KEY";
                }
                if (!(i + 1 == Filed))
                {
                    Query += ",";
                }
            }
            
            Query += ");";
            bool flag = false;
            string msg = SQL.CreateTable("MSSQL", Query, out flag);
            if (flag)
            {
                Session["SQL_Label"] = Query;
                Session["Message_Label"] = msg;
                Response.Redirect("database_structure.aspx?provider=" + providernm + "&dbnm=" + dbname);
            }
            else
            {
                (this.Master as MasterPages_MasterPage).ShowMessageLabel(msg.ToString(), Color.Red);
                (this.Master as MasterPages_MasterPage).ShowSQLLabel(Query.ToString(), Color.Red);
            }
        }
    }
}
