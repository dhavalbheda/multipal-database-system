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
using System.Drawing;

/// <summary>
/// Summary description for Table_SQL
/// </summary>
public class Table_SQL
{
	public Table_SQL()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    private static MySqlConnection GetMySQLConnection()
    {
        return (new MySqlConnection(ConfigurationManager.ConnectionStrings["mysql"].ConnectionString));
    }

    private static SqlConnection GetMsSQLConnection()
    {
        return (new SqlConnection(ConfigurationManager.ConnectionStrings["mssql"].ConnectionString));
    }

    public static void UseDatabase(string providernm, string DBName)
    {
        if (providernm.Equals("mysql", StringComparison.OrdinalIgnoreCase))
        {
            try
            {
                using (MySqlConnection con = GetMySQLConnection())
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.CommandText = "use `" + DBName + "`;";
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                string d = e.Message;

            }
        }
        else if (providernm.Equals("mssql", StringComparison.OrdinalIgnoreCase))
        {
            try
            {
                using (SqlConnection conn = GetMsSQLConnection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "use [" + DBName + "];";
                        cmd.Connection = conn;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                string d = e.Message;
            }
        }
    }

    public static DataTable showRecords(string provider,string DBName, string tablenm, out string QUERY)
    {
        QUERY = "JEMS";
        DataTable dt = new DataTable();

        if (provider.Equals("MySQL", StringComparison.OrdinalIgnoreCase))
        {
            QUERY = "SELECT * FROM `" + tablenm + "`;";
            try
            {
                using (MySqlConnection con = GetMySQLConnection())
                {
                    using (MySqlDataAdapter da = new MySqlDataAdapter(QUERY, con))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        else if (provider.Equals("MSSQL", StringComparison.OrdinalIgnoreCase))
        {
            QUERY = "SELECT * FROM " + tablenm + ";";
            try
            {
                using (SqlConnection con = GetMsSQLConnection())
                {
                    using (SqlDataAdapter da = new SqlDataAdapter("use [" + DBName + "];SELECT * FROM " + tablenm + ";", con))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        return dt;
    }

    public static string CleanTableRecords(string providernm, string DBName, string TableName, out Color color, out string QUERY)
    {
        if (providernm.Equals("mysql", StringComparison.OrdinalIgnoreCase))
        {
            try
            {
                using (MySqlConnection con = GetMySQLConnection())
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.CommandText = QUERY = "TRUNCATE `" + TableName + "`;";
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        color = Color.Green;
                        return "All Records R Deleted Successfully...";
                    }
                }
            }
            catch (Exception e)
            {
                QUERY = "TRUNCATE `" + TableName + "`;";
                color = Color.Red;
                return e.Message;
            }
        }
        else if (providernm.Equals("MSSQL", StringComparison.OrdinalIgnoreCase))
        {
            QUERY = "DELETE FROM " + TableName + ";";
            try
            {
                using (SqlConnection con = GetMsSQLConnection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "use [" + DBName + "];DELETE FROM " + TableName + ";";
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        color = Color.Green;
                        return "All Records R Deleted Successfully...";
                    }
                }
            }
            catch (Exception e)
            {
                QUERY = "Only D";
                color = Color.Red;
                return e.Message;
            }
        }
        QUERY = "Onlyd";
        color = Color.Red;
        return "";
    }

    public static DataTable showTableStructure(string provider, string dbnm, string tablenm)
    {
        DataTable dt = new DataTable();
        if (provider.Equals("MYSQL", StringComparison.OrdinalIgnoreCase))
        {
            using (MySqlConnection conn = GetMySQLConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand("DESCRIBE `" + tablenm + "`;", conn))
                {
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
        }
        else if (provider.Equals("MSSQL", StringComparison.OrdinalIgnoreCase))
        {
            using (SqlConnection conn = GetMsSQLConnection())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "use [" + dbnm + "]; select * from information_schema.columns where table_name='" + tablenm + "'";
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }

            }
        }
        return dt;
    }

    public static string DeleteRecords(string providernm, string DBName, string QUERY,out Color color)
    {
        if (providernm.Equals("mysql", StringComparison.OrdinalIgnoreCase))
        {
            try
            {
                using (MySqlConnection con = GetMySQLConnection())
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.CommandText = QUERY;
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        color = Color.Green;
                        return "Delete Record Successfully...\r\nOnlyD";
                    }
                }
            }
            catch (Exception e)
            {
                color = Color.Red;
                return e.Message;
            }
        }
        else if (providernm.Equals("MSSQL", StringComparison.OrdinalIgnoreCase))
        {
            using (SqlConnection conn = GetMsSQLConnection())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "use [" + DBName + "];" + QUERY;
                    cmd.Connection = conn;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    color = Color.Green;
                    return "Delete Record Successfully...";
                }

            }
        }
        color = Color.Azure;
        return "onlyd";
    }

    public static DataTable GetInsertStructure(string provider, string dbName, string tableName)
    {
        DataTable dt = new DataTable();
        if (provider.Equals("MYSQL", StringComparison.OrdinalIgnoreCase))
        {
            using (MySqlConnection con = GetMySQLConnection())
            {
                using (MySqlDataAdapter da = new MySqlDataAdapter("SELECT COLUMN_NAME,COLUMN_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='" + dbName + "' AND TABLE_NAME='" + tableName + "';", con))
                {
                    da.Fill(dt);
                }
            }
        }
        else if (provider.Equals("MSSQL", StringComparison.OrdinalIgnoreCase))
        {
            try
            {
                using (SqlConnection conn = GetMsSQLConnection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        conn.Open();
                        cmd.CommandText = "USE [" + dbName + "]";
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "sp_columns";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@table_name", tableName);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            if (dt.Columns[i].ColumnName.Equals("COLUMN_NAME"))
                            {
                                dt.Columns[i].ColumnName = "ColumnName";
                            }
                            else if (dt.Columns[i].ColumnName.Equals("TYPE_NAME"))
                            {
                                dt.Columns[i].ColumnName = "DataType";
                            }
                            else if (dt.Columns[i].ColumnName.Equals("LENGTH"))
                            {
                                dt.Columns[i].ColumnName = "LENGTH";
                            }
                            else if (dt.Columns[i].ColumnName.Equals("COLUMN_DEF"))
                            {
                                dt.Columns[i].ColumnName = "Default";
                            }
                            else
                            {
                                dt.Columns.Remove(dt.Columns[i].ColumnName);
                                i--;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        return dt;
    }

    public static string InsertRecords(string providernm, string DBName, string TableName, string query, out Color color)
    {
        if (providernm.Equals("mysql", StringComparison.OrdinalIgnoreCase))
        {
            try
            {
                using (MySqlConnection con = GetMySQLConnection())
                {
                    using (MySqlCommand cmd = new MySqlCommand(query,con))
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        color = Color.Green;
                        return "All Records Inserted Successfully...";
                    }
                }
            }
            catch (Exception e)
            {
                color = Color.Red;
                return e.Message;
            }
        }
        else if (providernm.Equals("MSSQL", StringComparison.OrdinalIgnoreCase))
        {
            //UseDatabase(providernm, DBName);
            try
            {
                using (SqlConnection con = GetMsSQLConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        color = Color.Green;
                        return "All Records Inserted Successfully...";
                    }
                }
            }
            catch (Exception e)
            {
                color = Color.Red;
                return e.Message;
            }
        }
        color = Color.Red;
        return "onlyd";
    }

    public static string DropTable(string providernm, string DBName, string TableName, out Color color, out string QUERY,out bool flag)
    {
        flag = false;
        if (providernm.Equals("mysql", StringComparison.OrdinalIgnoreCase))
        {
            try
            {
                using (MySqlConnection con = GetMySQLConnection())
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.CommandText = QUERY = "Drop Table `" + TableName + "`;";
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        color = Color.Green;
                        flag = true;
                        return "Delete Table Successfully...";
                    }
                }
            }
            catch (Exception e)
            {
                QUERY = "Drop Table `" + TableName + "`;";
                color = Color.Red;
                return e.Message;
            }
        }
        else if (providernm.Equals("MSSQL", StringComparison.OrdinalIgnoreCase))
        {
            QUERY = "DELETE FROM " + TableName + ";";
            try
            {
                using (SqlConnection con = GetMsSQLConnection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "use [" + DBName + "];DELETE FROM " + TableName + ";";
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        color = Color.Green;
                        flag = true;
                        return "Delete Table Successfully...";
                    }
                }
            }
            catch (Exception e)
            {
                QUERY = "Only D";
                color = Color.Red;
                return e.Message;
            }
        }
        QUERY = "Onlyd";
        color = Color.Red;
        return "";
    }

}
