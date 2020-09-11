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
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection.Emit;
using System.Reflection;

public partial class ADControl : System.Web.UI.UserControl
{
    GlobalFunction gf = new GlobalFunction();
    private GlobalFunction.Database _DatabaseProvider;

    public ADControl()
    {
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string[] datatype = new string[1];
            if (this._DatabaseProvider == GlobalFunction.Database.MYSQL)
            {
                datatype = Enum.GetNames(typeof(DataTypes.MYSQLDataType));
            }
            else if (this._DatabaseProvider == GlobalFunction.Database.MSSQL)
            {
                size_RequiredFieldValidator.Enabled = false;
                this.Type_DropDownList.AutoPostBack = false;

                datatype = Enum.GetNames(typeof(DataTypes.MSSQLDataType));
            }
            else
            {
                //Code for Oracle DataType load in Drop Down List
            }

            foreach (string s in datatype)
            {
                this.Type_DropDownList.Items.Add(new ListItem(s));
            }
        }
    }

    public GlobalFunction.Database DatabaseProvider
    {
        get { return _DatabaseProvider; }
        set { this._DatabaseProvider = value; }
    }

    public string FieldName 
    {
        get
        {
            return this.FieldName_TextBox.Text;
        }
        set
        {
            this.FieldName_TextBox.Text = value;
        }
    }
    
    public string FieldSize
    {
        get
        {
            return this.size_TextBox.Text;
        }
        set
        {
            this.size_TextBox.Text = value.ToString();
        }
    }

    public bool ShowLabel
    {
        get
        {
            return this.FieldName_Label.Visible;
        }
        set
        {
            this.FieldName_Label.Visible = value;
            this.Size_Label.Visible = value;
            this.Type_Label.Visible = value;
            this.Default_Label.Visible = value;
            this.null_Label.Visible = value;
            this.AI_Label.Visible = value;
        }
    }

    public bool AllowNull
    {
        get { return null_CheckBox.Checked; }
        set { null_CheckBox.Checked = value;}
    }

    public bool AutoIncrement
    {
        get { return ai_CheckBox.Checked; }
        set { ai_CheckBox.Checked = value; }
    }

    public bool AllowDefaultValue
    {
        get { return Default_CheckBox.Checked; }
        set 
        { 
            Default_CheckBox.Checked = value;
            if (Default_CheckBox.Checked)
            {
                Default_TextBox.Visible = true;
                default_RequiredFieldValidator.Enabled = true;
            }
            else
            {
                Default_TextBox.Visible = false;
                default_RequiredFieldValidator.Enabled = false;
                Default_TextBox.Text = "";
            }
        }
    }

    public string DefaultValue
    {
        get { return Default_TextBox.Text; }
        set { Default_TextBox.Text = value; }
    }

    public DropDownList GetDataTypeList
    {
        get { return this.Type_DropDownList; }
    }

    [CssClassProperty]
    public string CssClass 
    {
        get
        {
            return (string)(ViewState["CssClass"] ?? "");
        }
        set
        {
            ViewState["CssClass"] = value;
        }
    }

    public string DataType
    {
        get 
        {
            return Type_DropDownList.SelectedItem.Text;
        }
    }


    protected void Default_CheckBox_CheckedChanged(object sender, EventArgs e)
    {
        if (Default_CheckBox.Checked)
        {
            Default_TextBox.Visible = true;
            default_RequiredFieldValidator.Enabled = true;
        }
        else
        {
            Default_TextBox.Visible = false;
            default_RequiredFieldValidator.Enabled = false;
            Default_TextBox.Text = "";
        }
    }


    protected void Type_DropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this._DatabaseProvider == GlobalFunction.Database.MYSQL && this.Type_DropDownList.SelectedValue.Equals("0"))
        {
            this.size_RequiredFieldValidator.Enabled = true;
        }
        else
        {
            this.size_RequiredFieldValidator.Enabled = false;
        }
    }
}
