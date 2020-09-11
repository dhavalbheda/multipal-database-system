using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.IO;

public partial class MasterPages_Main_MasterPage : System.Web.UI.MasterPage
{
    GlobalFunction gf = new GlobalFunction();
    bool isMySQLloadtb = false;
    bool isMsSQLloadtb = false;
    bool isMySQLloadColumn = false;
    bool isMsSQLloadColumn = false;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack && bool.Parse(Session["LoadDB"].ToString()))
        {
            LoadDatabase();
            //Database_TreeView.CollapseAll();
        }
        if (ViewState["isMySQLloadtb"] == null)
        {
            ViewState["isMySQLloadtb"] = false;
        }
        if (ViewState["isMsSQLloadtb"] == null)
        {
            ViewState["isMsSQLloadtb"] = false;
        }
        if (ViewState["isMySQLloadColumn"] == null)
        {
            ViewState["isMySQLloadColumn"] = false;
        }
        if (ViewState["isMsSQLloadColumn"] == null)
        {
            ViewState["isMsSQLloadColumn"] = false;
        }

    }

    public void LoadDatabase()
    {
        try
        {

            Database_TreeView.Nodes.Add(GetNode(GetSpace(2) + "MySQL", "MySQL", "~/Images/mysql_32.png"));
            Database_TreeView.Nodes.Add(GetNode(GetSpace(2) + "Oracle", "Oracle", "~/Images/oracle_32.ico"));
            Database_TreeView.Nodes.Add(GetNode(GetSpace(2) + "MS SQL", "MS SQL", "~/Images/sql.png"));

            DataTable dt = gf.ShowDatabase(GlobalFunction.Database.MYSQL);
            Database_TreeView.Nodes[0].Text = GetSpace(2) + "MySQL" + GetSpace(2) + "(" + dt.Rows.Count + ")";
            AddDatabases(dt, 0);

            dt = gf.ShowDatabase(GlobalFunction.Database.MSSQL);

            Database_TreeView.Nodes[2].Text = GetSpace(2) + "MS SQL" + GetSpace(2) + "(" + dt.Rows.Count + ")";
            AddDatabases(dt, 2);
        }
        catch (Exception ex)
        {
        }
    }

    public void LoadDatabase(int idx)
    {
        try
        {
            if (idx == 0)
            {
                if (Database_TreeView.Nodes[0].Expanded.Value)
                {
                    Database_TreeView.Nodes[0].ChildNodes.Clear();

                    DataTable dt = gf.ShowDatabase(GlobalFunction.Database.MYSQL);
                    Database_TreeView.Nodes[0].Text = GetSpace(2) + "MySQL" + GetSpace(2) + "(" + dt.Rows.Count + ")";
                    AddDatabases(dt, 0);

                    ViewState["isMySQLloadtb"] = false;
                    Database_TreeView.Nodes[0].Collapse();
                    Database_TreeView.Nodes[0].Expand();
                    Session["LoadDB"] = true;
                }
            }
        }
        catch (Exception ex) { }

    }

    private void AddDatabases(DataTable dt,int index)
    {
        foreach (DataRow dr in dt.Rows)
        {
            TreeNode tn = new TreeNode();
            tn.Text = GetSpace(1) + dr["Database"].ToString();

            tn.Value = dr["Database"].ToString();
            tn.ImageUrl = "~/Images/Database.ico";

            Database_TreeView.Nodes[index].ChildNodes.Add(tn);
            
        }
    }

    private TreeNode GetNode(string text,string value,string image)
    {
        TreeNode node = new TreeNode();
        node.Text = text;
        node.Value = value;
        node.ImageUrl = image;
        return node;
    }
    private string GetSpace(int times)
    {
        string str = "";
        for (int i = 1; i <= times; i++)
            str += "&nbsp;";

        return str;
    }

    protected void Refresh_ImageButton_Click(object sender, ImageClickEventArgs e)
    {
        LoadDatabase(0);
    }

    protected void Home_ImageButton_Click(object sender, ImageClickEventArgs e)
    {
        LoadDatabase(0);
    }

    //Collaps and Expand the selected node   &&    generate node changed event
    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {
        string nm = Database_TreeView.SelectedNode.Value;
        if (Database_TreeView.SelectedNode.ChildNodes.Count > 0)
        {
            if (Database_TreeView.SelectedNode.Depth == 0)
            {
                if (Database_TreeView.SelectedNode.Expanded.HasValue)
                {
                    if (!Database_TreeView.SelectedNode.Expanded.Value)
                        Database_TreeView.CollapseAll();
                }
            }
            if (Database_TreeView.SelectedNode.Expanded.HasValue)
            {
                if (Database_TreeView.SelectedNode.Expanded.Value)
                {
                    Database_TreeView.SelectedNode.Collapse();
                }
                else
                {
                    Database_TreeView.SelectedNode.Expand();
                }
            }
            else
            {
                Database_TreeView.SelectedNode.Expand();
            }
        }
        
        Database_TreeView.SelectedNode.Selected = false;
    }

    //Node Expanded event - here all the tables of every databases will be loaded
    
    protected void TreeView1_TreeNodeExpanded(object sender, TreeNodeEventArgs e)
    {
        if (e.Node.Depth == 0)
        {
            if (e.Node.Text.Equals(Database_TreeView.Nodes[0].Text))
            {
                isMySQLloadtb = bool.Parse(ViewState["isMySQLloadtb"].ToString());
                if (!isMySQLloadtb)
                {
                    Load_Tables(e, GlobalFunction.Database.MYSQL);
                    ViewState["isMySQLloadtb"] = true;
                }
            }
            else if (e.Node.Text.Equals(Database_TreeView.Nodes[2].Text))
            {
                isMsSQLloadtb = bool.Parse(ViewState["isMsSQLloadtb"].ToString());
                if (!isMsSQLloadtb)
                {
                    Load_Tables(e, GlobalFunction.Database.MSSQL);
                    ViewState["isMsSQLloadtb"] = true;
                }
            }

        }
        else if (e.Node.Depth == 1)
        {
            if (e.Node.Parent.Value.Equals("MySql", StringComparison.OrdinalIgnoreCase))
            {
                isMySQLloadColumn = bool.Parse(ViewState["isMySQLloadColumn"].ToString());
                if (!isMySQLloadColumn)
                {
                    Load_Columns(e, GlobalFunction.Database.MYSQL);
                    ViewState["isMySQLloadColumn"] = true;
                }
            }
            else if (e.Node.Parent.Value.Equals("MS SQL", StringComparison.OrdinalIgnoreCase))
            {
                isMsSQLloadColumn = bool.Parse(ViewState["isMsSQLloadColumn"].ToString());
                if (!isMsSQLloadColumn)
                {
                    Load_Columns(e, GlobalFunction.Database.MSSQL);
                    ViewState["isMsSQLloadColumn"] = true;
                }
            }
        }
    }

    private void Load_Tables(TreeNodeEventArgs e,GlobalFunction.Database database)
    {
        TreeNodeCollection nodes = e.Node.ChildNodes;

        for (int i = 0; i < nodes.Count; i++)
        {
            string dbnm = nodes[i].Value;

            DataTable dt = gf.ShowTables(database, dbnm);

            foreach (DataRow dr in dt.Rows)
            {
                TreeNode node1 = new TreeNode();
                node1.Text = GetSpace(2) + dr["TableName"].ToString();

                node1.Value = dr["TableName"].ToString();

                node1.ImageUrl = "~/Images/table.ico";

                e.Node.ChildNodes[i].ChildNodes.Add(node1);
            }

            if (dt.Rows.Count > 0)
            {
                nodes[i].Text = GetSpace(1) + dbnm + GetSpace(2) + "(" + dt.Rows.Count + ")";
            }
        }
    }

    private void Load_Columns(TreeNodeEventArgs e,GlobalFunction.Database database)
    {
        string dbnm = e.Node.Value;

        TreeNodeCollection tables_nodes = e.Node.ChildNodes;
        for (int i = 0; i < tables_nodes.Count; i++)
        {
            DataTable dt = gf.ShowColumns(database,dbnm,tables_nodes[i].Value);

            foreach (DataRow dr in dt.Rows)
            {
                TreeNode column_node = new TreeNode();
                column_node.Text = GetSpace(1) + dr["ColumnName"].ToString();
                column_node.Value = dr["ColumnName"].ToString();
                column_node.ImageUrl = "~/Images/Column.ico";
                e.Node.ChildNodes[i].ChildNodes.Add(column_node);
            }
            tables_nodes[i].Text = GetSpace(2) + tables_nodes[i].Value + GetSpace(2) + "(" + dt.Rows.Count + ")";
        }
    }

    protected void Database_Button_Click(object sender, EventArgs e)
    {
        Response.Redirect("CreateDatabase.aspx");
    }
}
