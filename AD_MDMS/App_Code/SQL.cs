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
using System.IO;
using System.Diagnostics;

/// <summary>
/// Summary description for onlyD
/// </summary>
public class SQL
{
    public SQL()
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
                string d=  e.Message;

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

    public static string CreateDatabases(string selectedDB, string DBName,out Color color)
    {
        if (selectedDB.Equals("MYSQL", StringComparison.OrdinalIgnoreCase))
        {
            using (MySqlConnection conn = GetMySQLConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand("CREATE DATABASE `" + DBName + "` ;", conn))
                {
                    try
                    {
                        conn.Open();
                        int affected = cmd.ExecuteNonQuery();
                        color = Color.Green;
                        return "Database Created Successfully";
                    }
                    catch (Exception ex)
                    {
                        color = Color.Red;
                        return ex.Message;
                    }
                }
            }
        }
        else if (selectedDB.Equals("MsSQL", StringComparison.OrdinalIgnoreCase))
        {
            using (SqlConnection conn = GetMsSQLConnection())
            {
                using (SqlCommand cmd = new SqlCommand("CREATE DATABASE \"" + DBName + "\" ;", conn))
                {
                    try
                    {
                        conn.Open();
                        int affected = cmd.ExecuteNonQuery();
                        color = Color.Green;
                        return "Database Created Successfully";
                    }
                    catch (Exception ex)
                    {
                        color = Color.Red;
                        return ex.Message;
                    }
                }
            }
        }


        color = Color.Red;
        return "";
    }

    public static string CleanTableRecords(string providernm, string DBName, string TableName, out Color color,out string QUERY)
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

    public static string DeleteTable(string providernm, string DBName, string TableName, out Color color,out string QUERY)
    {
        if (providernm.Equals("mysql", StringComparison.OrdinalIgnoreCase))
        {
            try
            {
                using (MySqlConnection con = GetMySQLConnection())
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.CommandText = QUERY = "DROP TABLE `" + TableName + "`;";
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        color = Color.Green;
                        return "Table Deleted Successfully...";
                    }
                }
            }
            catch (Exception e)
            {
                QUERY = "DROP TABLE `" + TableName + "`;";
                color = Color.Red;
                return e.Message;
            }
        }
        else if (providernm.Equals("MSSQL", StringComparison.OrdinalIgnoreCase))
        {
            QUERY = "DROP TABLE " + TableName + ";";
            try 
            {
                using (SqlConnection con = GetMsSQLConnection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "use [" + DBName + "];DROP TABLE " + TableName + ";";
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        color = Color.Green;
                        return "Table Deleted Successfully...";
                    }
                }
            }
            catch (Exception e)
            {
                color = Color.Red;
                return e.Message;
            }
        }
        QUERY = "Only D";
        color = Color.Red;
        return "";
    }

    public static string RunSQL(string selectedDB, string query, out Color color, out DataTable dt)
    {
        dt = new DataTable();
        if (selectedDB.Equals("MYSQL", StringComparison.OrdinalIgnoreCase))
        {
            using (MySqlConnection conn = GetMySQLConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    try
                    {
                        conn.Open();
                        int affected = cmd.ExecuteNonQuery();

                        String[] qrys = query.Split(';');
                        int idx = qrys.Length;
                        if (string.IsNullOrEmpty(qrys[qrys.Length - 1]))
                        {
                            idx--;
                        }
                        if (qrys[idx - 1].Contains("select") || qrys[idx - 1].Contains("show"))
                        {
                            MySqlDataAdapter da = new MySqlDataAdapter(qrys[idx - 1], conn);
                            da.Fill(dt);
                        }

                        color = Color.Green;
                        return "Query Executed Successfully";
                    }
                    catch (Exception ex)
                    {
                        color = Color.Red;
                        return ex.Message;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }
        else if (selectedDB.Equals("MSSQL", StringComparison.OrdinalIgnoreCase))
        {
            using (SqlConnection conn = GetMsSQLConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    try
                    {
                        conn.Open();
                        int affected = cmd.ExecuteNonQuery();

                        string[] qrys = query.Split(';');
                        int idx = qrys.Length;
                        if (string.IsNullOrEmpty(qrys[qrys.Length - 1]))
                        {
                            idx--;
                        }
                        if (qrys[idx - 1].Contains("select"))
                        {
                            SqlDataAdapter da = new SqlDataAdapter(qrys[idx - 1], conn);
                            da.Fill(dt);
                        }

                        color = Color.Green;
                        return "Query Executed Successfully";
                    }
                    catch (Exception ex)
                    {
                        color = Color.Red;
                        return ex.Message;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }

        color = Color.Red;
        return "";
    }

    public static string DeleteDatabases(string selectedDB, string DBName, out Color color,out string QUERY)
    {
        if (selectedDB.Equals("MYSQL", StringComparison.OrdinalIgnoreCase))
        {
            using (MySqlConnection conn = GetMySQLConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    try
                    {
                        cmd.CommandText = QUERY = "Drop Database `" + DBName+"`;";
                        cmd.Connection = conn;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        color = Color.Green;
                        return "Database Deleted Successfully";
                    }
                    catch (Exception ex)
                    {
                        QUERY = "Drop Database `" + DBName + "`;";
                        color = Color.Red;
                        return ex.Message;
                    }
                }
            }
        }
        else if (selectedDB.Equals("MsSQL", StringComparison.OrdinalIgnoreCase))
        {
            using (SqlConnection conn = GetMsSQLConnection())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = QUERY = "Drop Database "+DBName;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        color = Color.Green;
                        return "Database Deleted Successfully";
                    }
                    catch (Exception ex)
                    {
                        QUERY = "Drop Database " + DBName;
                        color = Color.Red;
                        return ex.Message;
                    }
                }
            }
        }
        QUERY = "Only D";
        color = Color.Red;
        return "";
    }

    public static DataTable ShowTables(string selectedDB, string DatabaseName)
    {
        DataTable dt = new DataTable(DatabaseName);
        if (selectedDB.Equals("MYSQL", StringComparison.OrdinalIgnoreCase))
        {
            using (MySqlConnection conn = GetMySQLConnection())
            {
                string query = "select table_name as TableName,table_rows as Records from information_schema.tables where table_schema='" + DatabaseName + "';";
                using (MySqlDataAdapter da = new MySqlDataAdapter(query, conn))
                {
                    da.Fill(dt);
                    dt.Columns[0].ColumnName = "TableName";
                }
            }
        }
        else if (selectedDB.Equals("MSSQL", StringComparison.OrdinalIgnoreCase))
        {
            using (SqlConnection con = GetMsSQLConnection())
            {
                using (SqlDataAdapter da = new SqlDataAdapter("use ["+DatabaseName+"];select table_name from information_schema.tables;", con))
                {
                    da.Fill(dt);
                    dt.Columns[0].ColumnName = "TableName";
                }
                if (dt.Rows.Count > 0)
                {
                    con.Open();
                    dt.Columns.Add("Records");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.CommandText = "use ["+DatabaseName+"];select count(*) from [" + dt.Rows[i][0].ToString() + "]";
                            cmd.Connection = con;
                            dt.Rows[i][1]=cmd.ExecuteScalar().ToString();
                        }
                    }
                }
            }
        }
        return dt;
    }

    public static string DeleteDatabase_batch(string Provider, string Query, out Color color)
    {
        if (Provider.Equals("MYSQL", StringComparison.OrdinalIgnoreCase))
        {
            using (MySqlConnection conn = GetMySQLConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    try
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = Query;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        color = Color.Green;
                        return "Database Deleted Successfully";
                    }
                    catch (Exception ex)
                    {
                        color = Color.Red;
                        return ex.Message;
                    }
                }
            }
        }
        else if (Provider.Equals("MsSQL", StringComparison.OrdinalIgnoreCase))
        {
            using (SqlConnection conn = GetMsSQLConnection())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = Query;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        color = Color.Green;
                        return "Database Deleted Successfully";
                    }
                    catch (Exception ex)
                    {
                        color = Color.Red;
                        return ex.Message;
                    }
                }
            }
        }
        color = Color.Red;
        return "abc";
    }

    public static string ImportMYSQLDatabase(string provider, string query, out Color color)
    {
        using (MySqlConnection conn = GetMySQLConnection())
        {
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();

                    color = Color.Green;
                    return "Database Imported Successfully";
                }
                catch (Exception ex)
                {
                    color = Color.Red;
                    return ex.Message;
                }
            }
        }
    }

    public static string ImportMSSQLDatabase(string provider, string dbnm, string importPath, out Color color)
    {
        using (SqlConnection conn = GetMsSQLConnection())
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                try
                {
                    cmd.Connection = conn;

                    cmd.CommandText = "RESTORE FILELISTONLY FROM DISK='" + importPath + "'";
                    conn.Open();

                    SqlDataReader dr = cmd.ExecuteReader();
                    string[] logicalName = new string[2];

                    if (dr.HasRows)
                    {
                        int i = 0;
                        while (dr.Read())
                        {
                            logicalName[i] = dr["LogicalName"].ToString();
                            i++;
                        }
                    }
                    conn.Close();

                    cmd.CommandText = "use [" + dbnm + "];" + "select physical_name from sys.database_files;";
                    conn.Open();
                    string physicalpath = cmd.ExecuteScalar().ToString();
                    conn.Close();

                    string pathToMove = Path.GetDirectoryName(physicalpath);

                    string command = "RESTORE DATABASE [" + dbnm + "] FROM DISK='" + importPath + "' WITH REPLACE,";
                    command += "move '" + logicalName[0] + "' to '" + pathToMove + "\\" + dbnm + ".mdf',";
                    command += "move '" + logicalName[1] + "' to '" + pathToMove + "\\" + dbnm + "_log.LDF'";

                    conn.Open();
                    cmd.CommandText = command;
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    conn.Open();
                    string cmd2 = "alter database [" + dbnm + "] modify file(name=" + logicalName[0] + ",NEWNAME=" + dbnm + ");";
                    cmd2 += "alter database [" + dbnm + "] modify file(name=" + logicalName[1] + ",newname=" + dbnm + "_log);";
                    cmd.CommandText = cmd2;

                    cmd.ExecuteNonQuery();
                    conn.Close();


                    color = Color.Green;
                    return "Database Imported Successfully";
                }
                catch (Exception ex)
                {
                    color = Color.Red;
                    return ex.Message;
                }
            }
        }
    }

    public static string ExportDatabase(string provider, string database, string FilePathToExport, out Color color, HttpResponse Response)
    {
        if (provider.Equals("MSSQL", StringComparison.OrdinalIgnoreCase))
        {
            using (SqlConnection conn = GetMsSQLConnection())
            {
                using (SqlCommand cmd = new SqlCommand("BACKUP DATABASE [" + database + "] TO DISK='" + FilePathToExport + "'", conn))
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        string msg = "";
        string filename = Path.GetFileName(FilePathToExport);
        color = Color.Black;

        try
        {
            Response.Clear();
            Response.ContentType = "Application/octet-stream";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + ";");
            Response.TransmitFile(FilePathToExport);
            Response.Flush();
            Response.Close();
        }
        catch (Exception ex)
        {
            msg = ex.Message;
            color = Color.Red;
        }

        if (File.Exists(FilePathToExport))
        {
            File.Delete(FilePathToExport);
        }

        return msg;
    }

    public static string CreateTable(string selectedDB, string query, out bool flag)
    {
        flag = false;
        if (selectedDB.Equals("MYSQL", StringComparison.OrdinalIgnoreCase))
        {
            using (MySqlConnection conn = GetMySQLConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    try
                    {
                        conn.Open();
                        int affected = cmd.ExecuteNonQuery();
                        flag = true;
                        return "Query Executed Successfully ";
                    }
                    catch (Exception ex)
                    {
                        return ex.Message + "\n" + query;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }
        else if (selectedDB.Equals("MSSQL", StringComparison.OrdinalIgnoreCase))
        {
            using (SqlConnection conn = GetMsSQLConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    try
                    {
                        conn.Open();
                        int affected = cmd.ExecuteNonQuery();
                        flag = true;
                        return "Query Executed Successfully ";
                    }
                    catch (Exception ex)
                    {
                        return ex.Message + "\n" + query;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }
        return "j";
    }

    public static string RenameDatabase(string provider, string oldDBnm, string newDBnm, out Color color)
    {
        if (provider.Equals("MYSQL", StringComparison.OrdinalIgnoreCase))
        {
            var processInfo = new ProcessStartInfo();
            processInfo.FileName = "cmd.exe";
            processInfo.WorkingDirectory = ConfigurationManager.AppSettings["xampp_path"].ToString() + "/mysql/bin";
            processInfo.UseShellExecute = false;
            processInfo.WindowStyle = ProcessWindowStyle.Hidden;

            string user = ConfigurationManager.AppSettings["mysql_user"].ToString();
            string password = ConfigurationManager.AppSettings["mysql_pwd"].ToString();

            try
            {
                Process process = new Process();
                process.StartInfo = processInfo;
                process.StartInfo.RedirectStandardInput = true;
                process.Start();

                StreamWriter wr = process.StandardInput;

                #region command1
                string cmd1 = "mysqladmin -u" + AddUserPassword() + " create \"" + newDBnm + "\"";
                #endregion

                #region Command2
                string cmd2 = "mysqldump -u" + AddUserPassword() + " --routines " + oldDBnm + " | mysql -u" + AddUserPassword() + " " + newDBnm;
                #endregion

                #region Command3
                string cmd3 = "mysqladmin -u" + AddUserPassword() + " drop " + oldDBnm;
                #endregion

                wr.WriteLine(cmd1);
                wr.WriteLine(cmd2);
                wr.WriteLine(cmd3);
                wr.Write("y" + "\n");
                wr.Dispose();
                wr.Close();

                process.WaitForExit();
                process.Close();

                color = Color.Green;
                return "Database Renamed Successfully.";
            }
            catch (Exception ex)
            {
                color = Color.Red;
                return ex.Message;
            }

        }
        else if (provider.Equals("MSSQL", StringComparison.OrdinalIgnoreCase))
        {
            using (SqlConnection conn = GetMsSQLConnection())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        cmd.CommandText = "ALTER DATABASE [" + oldDBnm + "] MODIFY NAME=[" + newDBnm + "]";
                        cmd.Connection = conn;

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();


                        conn.Open();
                        string cmd2 = "alter database [" + newDBnm + "] modify file(name=" + oldDBnm + ",NEWNAME=" + newDBnm + ");";
                        cmd2 += "alter database [" + newDBnm + "] modify file(name=" + oldDBnm + "_log,newname=" + newDBnm + "_log);";
                        cmd.CommandText = cmd2;

                        cmd.ExecuteNonQuery();
                        conn.Close();

                        color = Color.Green;
                        return "Database Renamed Successfully.";
                    }
                    catch (Exception ex)
                    {
                        color = Color.Red;
                        return ex.Message;
                    }
                }
            }
        }
        else
        {
            color = Color.Red;
            return "";

        }
    }

    public static string CopyDatabase(Page page, string provider, string oldDBnm, string newDBnm, out Color color)
    {
        Color color1;
        CreateDatabases(provider, newDBnm, out color1);

        #region MYSQL Copy Database
        if (provider.Equals("MYSQL", StringComparison.OrdinalIgnoreCase))
        {
            var processInfo = new ProcessStartInfo();
            processInfo.FileName = "cmd.exe";
            processInfo.WorkingDirectory = ConfigurationManager.AppSettings["xampp_path"].ToString() + "/mysql/bin";
            processInfo.UseShellExecute = false;
            processInfo.WindowStyle = ProcessWindowStyle.Hidden;

            try
            {
                Process process = new Process();
                process.StartInfo = processInfo;
                process.StartInfo.RedirectStandardInput = true;
                process.Start();

                StreamWriter wr = process.StandardInput;

                string cmd1 = "mysqladmin -u" + AddUserPassword() + " create " + newDBnm;
                string cmd2 = "mysqldump -u" + AddUserPassword() + " --routines " + oldDBnm + " | mysql -u" + AddUserPassword() + " " + newDBnm;

                wr.WriteLine(cmd1);
                wr.WriteLine(cmd2);

                wr.Dispose();
                wr.Close();

                process.WaitForExit();
                process.Close();

                color = Color.Green;
                return "Database Copied Successfully.";
            }
            catch (Exception ex)
            {
                color = Color.Red;
                return ex.Message;
            }
        }
        #endregion

        else if (provider.Equals("MSSQL", StringComparison.OrdinalIgnoreCase))
        {
            bool isCopied = false;
            using (SqlConnection conn = GetMsSQLConnection())
            {
                string exportPath = page.MapPath("~/ExportDatabase/") + oldDBnm + ".bak";

                using (SqlCommand cmd = new SqlCommand("BACKUP DATABASE [" + oldDBnm + "] TO DISK='" + exportPath + "'", conn))
                {
                    try
                    {
                        ///Here the backup of all database is taken in .bak file
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();

                        //the .bak file is restore
                        cmd.CommandText = "RESTORE FILELISTONLY FROM DISK='" + exportPath + "'";
                        conn.Open();

                        SqlDataReader dr = cmd.ExecuteReader();
                        string[] logicalName = new string[2];

                        if (dr.HasRows)
                        {
                            int i = 0;
                            while (dr.Read())
                            {
                                logicalName[i] = dr["LogicalName"].ToString();
                                i++;
                            }
                        }
                        conn.Close();

                        cmd.CommandText = "use [" + oldDBnm + "];" + "select physical_name from sys.database_files;";
                        conn.Open();
                        string physicalpath = cmd.ExecuteScalar().ToString();
                        conn.Close();

                        string pathToMove = Path.GetDirectoryName(physicalpath);

                        string command = "RESTORE DATABASE [" + newDBnm + "] FROM DISK='" + exportPath + "' WITH REPLACE,";
                        command += "move '" + logicalName[0] + "' to '" + pathToMove + "\\" + newDBnm + ".mdf',";
                        command += "move '" + logicalName[1] + "' to '" + pathToMove + "\\" + newDBnm + "_log.LDF'";

                        conn.Open();
                        cmd.CommandText = command;
                        cmd.ExecuteNonQuery();
                        conn.Close();

                        conn.Open();
                        string cmd2 = "alter database [" + newDBnm + "] modify file(name=" + logicalName[0] + ",NEWNAME=" + newDBnm + ");";
                        cmd2 += "alter database [" + newDBnm + "] modify file(name=" + logicalName[1] + ",newname=" + newDBnm + "_log);";
                        cmd.CommandText = cmd2;

                        cmd.ExecuteNonQuery();
                        conn.Close();

                        isCopied = true;
                    }
                    catch (Exception ex)
                    {
                        color = Color.Red;
                        return ex.Message;
                    }
                    finally
                    {
                        if (File.Exists(exportPath))
                            File.Delete(exportPath);
                    }
                }
            }
            if (isCopied)
            {
                color = Color.Green;
                return "Database Copied Successfully";
            }
        }

        color = Color.Red;
        return "";
    }

    public static string Copy_DatabaseStructure(string provider, string oldDBnm, string newDBnm, out Color color)
    {
        Color color1;
        CreateDatabases(provider, newDBnm, out color1);

        if (provider.Equals("MYSQL", StringComparison.OrdinalIgnoreCase))
        {
            var processInfo = new ProcessStartInfo();
            processInfo.FileName = "cmd.exe";
            processInfo.WorkingDirectory = ConfigurationManager.AppSettings["xampp_path"].ToString() + "/mysql/bin";
            processInfo.WindowStyle = ProcessWindowStyle.Hidden;


            string command = "/c mysqldump -u" + AddUserPassword() + " -d " + oldDBnm + " | mysql -u " + AddUserPassword() + " -D " + newDBnm;

            processInfo.Arguments = command;

            try
            {
                Process process = new Process();
                process.StartInfo = processInfo;
                process.Start();
                process.Close();

                color = Color.Green;
                return "Database Copied Successfully.";
            }
            catch (Exception ex)
            {
                color = Color.Red;
                return ex.Message;
            }
        }
        color = Color.Red;
        return "";
    }

    public static string AddUserPassword()
    {
        string user = ConfigurationManager.AppSettings["mysql_user"].ToString();
        string password = ConfigurationManager.AppSettings["mysql_pwd"].ToString();

        string s = user;
        if (!string.IsNullOrEmpty(password))
            s += " -p" + password;
        return s;
    }

}
