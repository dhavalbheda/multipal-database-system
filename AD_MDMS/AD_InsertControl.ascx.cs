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

public partial class AD_InsertControl : System.Web.UI.UserControl
{
    string Providernm, DBName, Tablenm;
    DataTable dt;
    public bool AllowInsert;

    public AD_InsertControl()
    {
        
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public void CreateStructure(string providernm, string dbname, string tablenm)
    {
        Providernm = providernm;
        DBName = dbname;
        Tablenm = tablenm;
        dt =Table_SQL.GetInsertStructure(providernm, dbname, tablenm);
        if (providernm.Equals("MYSQL", StringComparison.OrdinalIgnoreCase))
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TableRow Row_Column = new TableRow();
                TableRow Row_DataType = new TableRow();
                TableRow Row_Value = new TableRow();
                TableCell cell_Column = new TableCell();
                TableCell cell_DataType = new TableCell();
                TableCell cell_Value = new TableCell();
                TextBox t1 = new TextBox();
                t1.ID = "t" + i;

                cell_Column.Text = dt.Rows[i][0].ToString();
                Row_Column.Cells.Add(cell_Column);
                Table_Column.Rows.Add(Row_Column);

                cell_DataType.Text = dt.Rows[i][1].ToString();
                Row_DataType.Cells.Add(cell_DataType);
                Table_DataType.Rows.Add(Row_DataType);

                cell_Value.Controls.Add(t1);
                Row_Value.Cells.Add(cell_Value);
                Table_Value.Rows.Add(Row_Value);
            }
        }
        else if (providernm.Equals("MSSQL", StringComparison.OrdinalIgnoreCase))
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TableRow Row_Column = new TableRow();
                TableRow Row_DataType = new TableRow();
                TableRow Row_Value = new TableRow();
                TableCell cell_Column = new TableCell();
                TableCell cell_DataType = new TableCell();
                TableCell cell_Value = new TableCell();
                TextBox t1 = new TextBox();
                t1.ID = "t" + i;

                cell_Column.Text = dt.Rows[i]["ColumnName"].ToString();
                Row_Column.Cells.Add(cell_Column);
                Table_Column.Rows.Add(Row_Column);

                cell_DataType.Text = dt.Rows[i]["DataType"].ToString();
                Row_DataType.Cells.Add(cell_DataType);
                Table_DataType.Rows.Add(Row_DataType);
                
                cell_Value.Controls.Add(t1);
                Row_Value.Cells.Add(cell_Value);
                Table_Value.Rows.Add(Row_Value);
                if (dt.Rows[i]["DataType"].ToString().Contains("identity"))
                {
                    t1.Enabled = false;
                }
            }
        }
    }

    public string GetInsertQuery()
    {
       
        if (Providernm.Equals("MSSQL", StringComparison.OrdinalIgnoreCase))
        {
            string Query = "Insert Into [" + Tablenm + "] Values(";
            for (int i = 0; i < Table_Value.Rows.Count; i++)
            {
                TextBox t = (TextBox)this.Table_Value.Rows[i].Cells[0].FindControl("t" + i);
                if (t.Enabled)
                {
                    Query += "'" + t.Text + "'";
                    if (i + 1 < Table_Value.Rows.Count)
                    {
                        Query += ",";
                    }
                }
            }
            Query += ");";
            return Query;
        }
        else
        {
            string Query = "Insert Into " + Tablenm + " Values(";
            for (int i = 0; i < Table_Value.Rows.Count; i++)
            {
                TextBox t = (TextBox)this.Table_Value.Rows[i].Cells[0].FindControl("t" + i);
                Query += "'" + t.Text + "'";
                if (i + 1 < Table_Value.Rows.Count)
                {
                    Query += ",";
                }
                else
                {
                    Query += ");";
                }
            }
            return Query;
        }
        
    }

    public bool isInsert
    {
        get
        {
            return this.Ignore_CheckBox.Checked;
        }
    }
    protected void Ignore_CheckBox_CheckedChanged(object sender, EventArgs e)
    {
    }
}
