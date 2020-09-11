using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Drawing;
using System.Collections.Generic;

public class BasePage : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        Response.Write("onlyd");
        Page.Theme = "noon";
    }
}
public partial class MasterPages_MasterPage : System.Web.UI.MasterPage
{
    GlobalFunction gf = new GlobalFunction();
    DataTable ProviderTable;
    List<string> vpath = new List<string>();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Treeview_Expand"] != null)
        {
            vpath = (List<string>)Session["Treeview_Expand"];
        }
        
        if (!Page.IsPostBack)
        {
           LoadProvider();
            LoadTreeNode();
        }

        ProviderTable = (DataTable)Session["Providers"];

        #region comment
        /*TreeNode lastNode = Database_TreeView.FindNode("MYSQL/jems");
        ExpandTreeView(lastNode);*/
        /*if (Session["vpath"] != null)
        {
            vpath = (List<string>)Session["vpath"];
        }*/
        #endregion
    }

    private void LoadTreeNode()
    {
        if (vpath.Count > 0)
        {
            Database_TreeView.TreeNodeExpanded -= Database_TreeView_TreeNodeExpanded;

            for (int i = 0; i < vpath.Count; i++)
            {
                TreeNode node = Database_TreeView.FindNode(vpath[i]);
                if (node != null)
                {
                    if (node.Parent != null)
                        TreeState(node);
                    else
                        node.Expand();
                }
            }

            Database_TreeView.TreeNodeExpanded += Database_TreeView_TreeNodeExpanded;
        }
    }

    private void TreeState(TreeNode node)
    {
        node.Expand();
        if (node.Parent != null)
        {
            TreeState(node.Parent);
        }
    }

    private void ExpandTreeView(TreeNode lastNode)
    {
        while (lastNode.Parent != null)
        {
            lastNode.Parent.Expand();
            ExpandTreeView(lastNode.Parent);
        }
    }

    private string GetSpace(int times)
    {
        string str = "";
        for (int i = 1; i <= times; i++)
            str += "&nbsp;";

        return str;
    }

    private TreeNode GetNode(string text, string value, string image)
    {
        TreeNode node = new TreeNode();
        node.Text = text;
        node.Value = value;
        node.ImageUrl = image;
        return node;
    }

    public void LoadProvider()
    {
        ProviderTable = (DataTable)Session["Providers"];
        for (int i = 0; i < ProviderTable.Rows.Count; i++)
        {
            Database_TreeView.Nodes.Add(GetNode(GetSpace(2) + ProviderTable.Rows[i][0].ToString(), ProviderTable.Rows[i][0].ToString(), ProviderTable.Rows[i][1].ToString()));
        }
        LoadDatabases();
    }

    private void LoadDatabases()
    {
        DataTable dt;
        for (int i = 0; i < Database_TreeView.Nodes.Count; i++)
        {
            dt = gf.ShowDatabase(Database_TreeView.Nodes[i].Value.ToString());
            Database_TreeView.Nodes[i].Text = GetSpace(2) + Database_TreeView.Nodes[i].Text + GetSpace(2) + "(" + dt.Rows.Count + ")";
            AddDatabases(dt, i);
        }
        LoadTables();
    }

    private void AddDatabases(DataTable dt, int index)
    {
        Database_TreeView.Nodes[index].ChildNodes.Clear();
        foreach (DataRow dr in dt.Rows)
        {
            TreeNode tn = new TreeNode();
            tn.Text = GetSpace(1) + dr["Database"].ToString();
            tn.Value = dr["Database"].ToString();
            tn.ImageUrl = "~/Images/Database.ico";
            Database_TreeView.Nodes[index].ChildNodes.Add(tn);
        }
    }

    private void LoadTables()
    {
        for (int provider = 0; provider < Database_TreeView.Nodes.Count; provider++)
        {
            TreeNodeCollection nodes = Database_TreeView.Nodes[provider].ChildNodes;

            for (int i = 0; i < nodes.Count; i++)
            {
                nodes[i].ChildNodes.Clear();

                string dbnm = nodes[i].Value;

                DataTable dt = gf.ShowTables(Database_TreeView.Nodes[provider].Value, dbnm);

                foreach (DataRow dr in dt.Rows)
                {
                    Database_TreeView.Nodes[provider].ChildNodes[i].ChildNodes.Add(GetNode(GetSpace(2) + dr["TableName"].ToString(), dr["TableName"].ToString(), "~/Images/table.ico"));
                }

                if (dt.Rows.Count > 0)
                {
                    nodes[i].Text = GetSpace(1) + dbnm + GetSpace(2) + "(" + dt.Rows.Count + ")";
                }
            }
        }
        LoadColumns();

    }

    private void LoadColumns()
    {
        for (int provider = 0; provider < Database_TreeView.Nodes.Count; provider++)
        {
            TreeNodeCollection DBNode = Database_TreeView.Nodes[provider].ChildNodes;

            string providerName = Database_TreeView.Nodes[provider].Value;

            for (int database = 0; database < DBNode.Count; database++)
            {
                TreeNodeCollection TableNode = DBNode[database].ChildNodes;

                string databaseName = DBNode[database].Value;

                for (int table = 0; table < TableNode.Count; table++)
                {

                    string tableName = TableNode[table].Value;

                    DataTable dt = gf.ShowColumns(providerName, databaseName, tableName);

                    foreach (DataRow row in dt.Rows)
                    {
                        TreeNode ColumnNode = GetNode(row[0].ToString(), row[0].ToString(), "~/Images/Column.ico");
                        Database_TreeView.Nodes[provider].ChildNodes[database].ChildNodes[table].ChildNodes.Add(ColumnNode);
                    }

                    if (dt.Rows.Count > 0)
                    {
                        TableNode[table].Text = GetSpace(1) + tableName + GetSpace(2) + "(" + dt.Rows.Count + ")";
                    }
                }
            }
        }

    }

    protected void Database_TreeView_SelectedNodeChanged(object sender, EventArgs e)
    {

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
        
        if (Database_TreeView.SelectedNode.Depth == 0)
        {
            Response.Redirect("~/CreateDatabase.aspx",false);
        }
        else if (Database_TreeView.SelectedNode.Depth == 1)
        {
            Response.Redirect("database_structure.aspx?provider=" + Database_TreeView.SelectedNode.Parent.Value + "&dbnm=" + Database_TreeView.SelectedNode.Value,false);
        }
        else if (Database_TreeView.SelectedNode.Depth == 2)
        {
            Response.Redirect("~/Table_Pages/table_browse.aspx?provider=" + Database_TreeView.SelectedNode.Parent.Parent.Value + "&dbnm=" + Database_TreeView.SelectedNode.Parent.Value + "&tbnm=" + Database_TreeView.SelectedNode.Value, false);
        }
        
        Database_TreeView.SelectedNode.Selected = false;
    }
    protected void Database_TreeView_TreeNodeExpanded(object sender, TreeNodeEventArgs e)
    {
        if (e.Node.Depth == 0)
        {
            Database_TreeView.TreeNodeCollapsed -= Database_TreeView_TreeNodeCollapsed;
            Database_TreeView.TreeNodeExpanded -= Database_TreeView_TreeNodeExpanded;

            Database_TreeView.CollapseAll();
            e.Node.Expand();

            Database_TreeView.TreeNodeCollapsed += Database_TreeView_TreeNodeCollapsed;
            Database_TreeView.TreeNodeExpanded += new TreeNodeEventHandler(Database_TreeView_TreeNodeExpanded);

        }
            
        vpath.Add(e.Node.ValuePath);
        Session["Treeview_Expand"] = vpath;
    }

    protected void DatabaseConfiguration_ImageButton_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("DatabaseConfiguration.aspx");
    }
    
    protected void Refresh_ImageButton_Click(object sender, ImageClickEventArgs e)
    {
        Refresh_sidebar();
    }

    public void Refresh_sidebar()
    {
        Database_TreeView.Nodes.Clear();
        LoadProvider();
        LoadTreeNode();
    }
    public void ShowMessageLabel(string text, Color color)
    {
        Message_Label.Visible = true;
        Message_Label.Text = text;
        Message_Label.ForeColor = color;
    }

    public void ShowSQLLabel(string text, Color color)
    {
        SQL_Label.Visible = true;
        SQL_Label.Text = text;
        SQL_Label.ForeColor = color;
    }

    public void RemoveMessageLabel()
    {
        Message_Label.Text = "";
        Message_Label.Visible = false;
    }

    private void saveExpandedNode(TreeNodeCollection treeNodeCollection, List<string> expanded)
    {
        foreach (TreeNode node in treeNodeCollection)
        {
            if (node.Expanded == true)
            {
                expanded.Add(node.ValuePath);
                if (node.ChildNodes.Count > 0)
                    saveExpandedNode(node.ChildNodes, expanded);
            }
        }
    }

    protected void Database_TreeView_TreeNodeCollapsed(object sender, TreeNodeEventArgs e)
    {
        if (vpath.Contains(e.Node.ValuePath))
        {
            vpath.Remove(e.Node.ValuePath);
            Session["Treeview_Expand"] = vpath;
        }
    }
}
