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
using MySql.Data.MySqlClient;
using System.Drawing;

public partial class CreateDatabase : System.Web.UI.Page
{
    GlobalFunction gf = new GlobalFunction();

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Create_Button_Click(object sender, EventArgs e)
    {
        if (Provider_DropDownList.SelectedValue.Equals("MYSQL"))
        {
            createMYSQLDB();
        }
    }

    private void createMYSQLDB()
    {
        try
        {
            using (MySqlConnection conn = gf.GetMySQLConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand("Create database `" + db_TextBox.Text + "`;", conn))
                {
                    conn.Open();
                    int affected = cmd.ExecuteNonQuery();
                    if (affected > 0)
                    {
                        msg_Label.Text = "Database Created Successfully";
                        msg_Label.ForeColor = Color.Green;
                        Session["LoadDB"] = false;
                        (this.Master as MasterPages_Main_MasterPage).LoadDatabase(0);
                    }
                    conn.Close();
                }
            }
        }
        catch (Exception ex)
        {
            msg_Label.Text = ex.Message;
            msg_Label.ForeColor = Color.Red;
        }
    }
}
