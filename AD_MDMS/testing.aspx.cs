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
using System.IO;

public partial class testing : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ADControl[] abc= new ADControl[5];
        for (int i = 0; i < 5; i++)
        {
            abc[i]= (ADControl)LoadControl("~/ADControl.ascx");
            abc[i].DatabaseProvider = GlobalFunction.Database.MYSQL;
            
            abc[i].ShowLabel = false;
            this.form1.Controls.Add(abc[i]);
            
        }


        /*string path = MapPath("~/ExportDatabase/only007.sql");
        string filename = Path.GetFileName(path);
        try
        {
            Response.Clear();
            Response.ContentType = "text/sql";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + ";");
            Response.TransmitFile(path);
            Response.Flush();
            Response.Close();
            //HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }*/

    }
    protected void Button3_Click(object sender, EventArgs e)
    {
       /* if (FileUpload1.HasFile)
        {
            Response.Write(FileUpload1.FileName);
            //Response.Write(File.ReadAllText(FileUpload1.FileName));

            string s;
            using(StreamReader reader = new StreamReader(FileUpload1.PostedFile.InputStream))
            {
                s = reader.ReadToEnd();
            }
            
        }*/
    }
}
