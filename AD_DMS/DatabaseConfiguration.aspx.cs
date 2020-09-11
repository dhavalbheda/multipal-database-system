using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Drawing;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Web.Configuration;


public partial class DatabaseConfiguration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MultiView1.ActiveViewIndex = 0;
            DatabaseProvider_RadioButtonList.SelectedIndex = 0;
        }
    }
    protected void configuration_button_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 0;
    }
    protected void theme_Button_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
    }
    protected void Home_Button_Click(object sender, EventArgs e)
    {
        GoToHome();
    }

    protected void Logo_ImageButton_Click(object sender, ImageClickEventArgs e)
    {
        GoToHome();
    }

    private void GoToHome()
    {
        if (WebConfigurationManager.ConnectionStrings.Count == 2)
        {
            SetStatusLabel("Please Configure Your Database First!", Color.Crimson, true);
        }
        else
        {
            Response.Redirect("~/Default.aspx");
        }
    }

    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Clear_Button_Click(sender, e);
        Status_Label.Text = "";
        Status_Label.Visible = false;
    }
    protected void Unm_CheckBox_CheckedChanged(object sender, EventArgs e)
    {
        if (Unm_CheckBox.Checked)
        {
            Unm_TextBox.Enabled = false;
            Pwd_CheckBox.Checked = true;
            Pwd_CheckBox_CheckedChanged(sender, e);
            Pwd_CheckBox.Enabled = false;
            Unm_TextBox.Text = "";
        }
        else
        {
            Unm_TextBox.Enabled = true;
            Pwd_CheckBox.Enabled = true;
        }
    }
    protected void Pwd_CheckBox_CheckedChanged(object sender, EventArgs e)
    {
        if (Pwd_CheckBox.Checked)
        {
            Pwd_TextBox.Enabled = false;
            Pwd_TextBox.Text = "";
        }
        else
            Pwd_TextBox.Enabled = true;
    }
    protected void Clear_Button_Click(object sender, EventArgs e)
    {
        Server_TextBox.Text = Unm_TextBox.Text = Pwd_TextBox.Text = "";
    }
    protected void TestConnection_Button_Click(object sender, EventArgs e)
    {
        string message = "";
        if (IsValidDetails())
        {
            if (TestConnection(DatabaseProvider_RadioButtonList.SelectedValue, out message))
                SetStatusLabel(message, Color.Green, true);
            else
                SetStatusLabel(message, Color.Red, true);
        }
    }

    private void SetStatusLabel(string message, Color color, bool p)
    {
        Status_Label.Text = message;
        Status_Label.ForeColor = color;
        Status_Label.Visible = p;
    }

    private bool TestConnection(string dbprovider,out string message)
    {
        if (dbprovider.Equals("MYSQL"))
        {
            try
            {
                string ConStr = "Server='"+Server_TextBox.Text+"';User Id='"+Unm_TextBox.Text+"';Password='"+Pwd_TextBox.Text+"';";
                using (MySqlConnection conn = new MySqlConnection(ConStr))
                {
                    conn.Open(); 
                }
                message = "Connected Successfully";
                return true;
            }
            catch (Exception e)
            {
                message = e.Message;
                return false;
            }
        }
        else if (dbprovider.Equals("MSSQL"))
        {
            try
            {
                string ConStr = "Server='" + Server_TextBox.Text + "';User Id='" + Unm_TextBox.Text + "';Password='" + Pwd_TextBox.Text + "';Integrated Security=True;";
                using (SqlConnection conn = new SqlConnection(ConStr))
                {
                    conn.Open();
                }
                message = "Connected Successfully";
                return true;
            }
            catch (Exception e)
            {
                message = e.Message;
                return false;
            }
        }
        else
        {
            message = "other";
            return true;
        }
    }

    private bool IsValidDetails()
    {
        bool isvalid = true;
        string error_message="All Informations are Required. :(";
        if (String.IsNullOrEmpty(Server_TextBox.Text))
        {
            isvalid = false;
            error_message = "Server Name is Required.";
        }
        else if (String.IsNullOrEmpty(Unm_TextBox.Text) && !Unm_CheckBox.Checked)
        {
            isvalid = false;
            error_message = "User Name is Required.";
        }
        else if (String.IsNullOrEmpty(Pwd_TextBox.Text) && !Pwd_CheckBox.Checked)
        {
            isvalid = false;
            error_message = "Password is Required.";
        }

        if (!isvalid)
        {
            SetStatusLabel(error_message, Color.Red, true);
        }
        else
            Status_Label.Visible = false;

        return isvalid;
    }

    protected void Save_Button_Click(object sender, EventArgs e)
    {
        string message = "";
        if (IsValidDetails())
        {
            if (TestConnection(DatabaseProvider_RadioButtonList.SelectedValue, out message))
            {
                string dbprovider = DatabaseProvider_RadioButtonList.SelectedValue;
                string server = Server_TextBox.Text;
                string unm = Unm_TextBox.Text;
                string pwd = Pwd_TextBox.Text;

                SaveToWebConfig(dbprovider, server, unm, pwd);
                SetStatusLabel("Connected Successfully.<br>Configuration Saved Successfully.", Color.Green, true);
            }
            else
                SetStatusLabel(message, Color.Red, true);
        }
    }

    private void SaveToWebConfig(string dbprovider, string server, string unm, string pwd)
    {
        Configuration webConfig = WebConfigurationManager.OpenWebConfiguration("~");
        string constr = "";
        if (dbprovider.Equals("MYSQL"))
        {
            constr = "Server='" + server + "';User Id='" + unm + "';Password='" + pwd + "';";
        }
        else if (dbprovider.Equals("MSSQL"))
        {
            constr = "Server='" + server + "';Integrated Security=True;";
        }
        else
        {
        }

        if (webConfig.ConnectionStrings.ConnectionStrings[dbprovider] == null)
        {
            ConnectionStringSettings con_setting = new ConnectionStringSettings();
            con_setting.Name = dbprovider.ToLower();
            webConfig.ConnectionStrings.ConnectionStrings.Add(con_setting);
        }

        webConfig.ConnectionStrings.ConnectionStrings[dbprovider.ToLower()].ConnectionString = constr;
        webConfig.Save(ConfigurationSaveMode.Modified);
        ConfigurationManager.RefreshSection("connectionStrings");
    }
    
}
