using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

/// <summary>
/// Summary description for NewGlobalFunction
/// </summary>
public class GlobalFunction
{
    public GlobalFunction()
    {

    }

    #region variables
    public Database SelectedProvider { get; set; }
    public enum Database
    {
        MYSQL,
        ORACLE,
        MSSQL
    }
    #endregion

    public bool CheckConfiguration()
    {
        if (ConfigurationManager.ConnectionStrings.Count == 2)
        {
            return false;
        }
        else
        {
            return true;            
        }
    }

    public DataTable GetDBProvider()
    {
        DataTable dt = new DataTable("DataBaseProviders");
        dt.Columns.Add(new DataColumn("DBProviders"));
        dt.Columns.Add(new DataColumn("Icons"));

        string[] providers = Enum.GetNames(typeof(Database));

        foreach (string provider in providers)
        {
            if (ConfigurationManager.ConnectionStrings[provider.ToLower()] != null)
            {
                dt.Rows.Add(provider.ToUpper(), "~/Images/" + provider.ToLower() + ".png");
            }
        }

        return dt;
    }

    public DataTable ShowDatabase(string selectedDB)
    {
        DataTable dt = new DataTable("Table");

        try
        {
            if (selectedDB.Equals("MySQL", StringComparison.OrdinalIgnoreCase))
            {
                dt = new DataTable("MySQL");

                using (MySqlConnection conn = GetMySQLConnection())
                {
                    using (MySqlCommand cmd = new MySqlCommand("show databases;", conn))
                    {
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        da.Fill(dt);
                        dt.Columns[0].ColumnName = "Database";
                    }
                }
            }
            else if (selectedDB.Equals("MSSQL", StringComparison.OrdinalIgnoreCase))
            {
                dt = new DataTable("MSSQL");
                using (SqlConnection conn = GetMsSQLConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("select name from sys.databases;", conn))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                        dt.Columns[0].ColumnName = "Database";
                    }
                }
            }
            else if (selectedDB.Equals("ORACLE", StringComparison.OrdinalIgnoreCase))
            {

            }
        }
        catch (Exception e)
        {
        }
        return dt;
    }

    public DataTable ShowTables(string selectedDB, string DatabaseName)
    {
        DataTable dt = new DataTable(DatabaseName);
        if (selectedDB.Equals("MYSQL", StringComparison.OrdinalIgnoreCase))
        {
            using (MySqlConnection conn = GetMySQLConnection())
            {
                string query = "select table_name as TableName from information_schema.tables where table_schema='" + DatabaseName + "';";
                using (MySqlDataAdapter da = new MySqlDataAdapter(query, conn))
                {
                    da.Fill(dt);
                    dt.Columns[0].ColumnName = "TableName";
                }
            }
        }
        else if (selectedDB.Equals("MSSQL", StringComparison.OrdinalIgnoreCase))
        {
            using (SqlConnection conn = GetMsSQLConnection())
            {
                using (SqlDataAdapter da = new SqlDataAdapter("use \"" + DatabaseName + "\";select Table_name from information_schema.tables", conn))
                {
                    da.Fill(dt);
                    dt.Columns[0].ColumnName = "TableName";
                }
            }
        }

        return dt;
    }

    public DataTable ShowColumns(string selectedDB, string DatabaseName, string TableName)
    {
        DataTable dt = new DataTable(TableName);
        if (selectedDB.Equals("MYSQL", StringComparison.OrdinalIgnoreCase))
        {
            using (MySqlConnection conn = GetMySQLConnection())
            {
                string query = "select column_name as ColumnName from information_schema.columns where table_name='" + TableName + "' and table_schema='" + DatabaseName + "'";
                using (MySqlDataAdapter da = new MySqlDataAdapter(query, conn))
                {
                    dt = new DataTable(TableName);
                    da.Fill(dt);
                }
            }
        }
        else if (selectedDB.Equals("MSSQL", StringComparison.OrdinalIgnoreCase))
        {
            using (SqlConnection conn = GetMsSQLConnection())
            {
                string query = "use \"" + DatabaseName + "\"; select Column_Name from INFORMATION_SCHEMA.columns where TABLE_NAME = '" + TableName + "';";
                using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                {
                    dt = new DataTable(TableName);
                    da.Fill(dt);
                    dt.Columns[0].ColumnName = "ColumnName";
                }
            }
        }
        return dt;
    }

    public static void ShowMessage(Page page,string msg)
    {
        ScriptManager.RegisterClientScriptBlock(page, typeof(Page), "ClientScript", "alert('"+msg+"')", true);
    }

    public MySqlConnection GetMySQLConnection()
    {
        return (new MySqlConnection(ConfigurationManager.ConnectionStrings["mysql"].ConnectionString));
    }

    public SqlConnection GetMsSQLConnection()
    {
        return (new SqlConnection(ConfigurationManager.ConnectionStrings["mssql"].ConnectionString));
    }

}
