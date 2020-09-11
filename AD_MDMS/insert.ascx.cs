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

public partial class insert : System.Web.UI.UserControl
{
    GlobalFunction gf = new GlobalFunction();
    DataTable dt;
    protected void Page_Load(object sender, EventArgs e)
    {
        dt = gf.ShowColumns("Mysql", "Khanstore", "products");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            TableRow row = new TableRow();
            TableCell cell = new TableCell();
            cell.Text = dt.Rows[i][0].ToString();
            row.Cells.Add(cell);
            t1.Rows.Add(row);
        }
    }
}
