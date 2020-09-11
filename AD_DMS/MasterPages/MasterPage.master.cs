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

public partial class MasterPages_MasterPage : System.Web.UI.MasterPage
{
    NewGlobalFunction gf = new NewGlobalFunction();
    DataTable ProviderTable;

    protected void Page_Load(object sender, EventArgs e)
    {
        /*if (!Page.IsPostBack)
        {
            LoadProvider();
        }
        ProviderTable = (DataTable)ViewState["ProviderTable"];*/
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

    private void LoadProvider()
    {
        ProviderTable = gf.GetDBProvider();
        ViewState["ProviderTable"] = ProviderTable;
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

        Database_TreeView.SelectedNode.Selected = false;
        //if (Database_TreeView.SelectedNode.Depth == 0)
        //{
            /*if (Database_TreeView.SelectedNode.Expanded.Value)
            {
                Database_TreeView.SelectedNode.CollapseAll();
            }
            else
            {
                for (int i = 0; i < ProviderTable.Rows.Count; i++)
                {

                    if (!Database_TreeView.SelectedNode.Value.Equals(ProviderTable.Rows[i][0].ToString()))
                    {
                        Database_TreeView.Nodes[i].Collapse();
                    }
                    else
                    {
                        Database_TreeView.Nodes[i].Expand();
                    }
                }
            }*/
    }

    protected void Database_TreeView_TreeNodeExpanded(object sender, TreeNodeEventArgs e)
    {
        if (e.Node.Depth == 0)
        {
            for (int i = 0; i < ProviderTable.Rows.Count; i++)
            {
                if (!e.Node.Value.Equals(ProviderTable.Rows[i][0].ToString()))
                    Database_TreeView.Nodes[i].Collapse();
                else
                {
                    Database_TreeView.Nodes[i].Expand();
                }
            }
        }
    }
    protected void DatabaseConfiguration_ImageButton_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("DatabaseConfiguration.aspx");
    }
}
