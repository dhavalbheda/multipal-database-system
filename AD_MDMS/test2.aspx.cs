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
using System.Diagnostics;
using System.IO;

public partial class test2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        /*ADControl[] control = new ADControl[5];
        for (int i = 0; i < control.Length; i++)
        {
            control[i] = (ADControl)LoadControl("~/ADControl.ascx");
            this.form1.Controls.Add(control[i]);
        }*/
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        var processInfo = new ProcessStartInfo();
        processInfo.FileName = "cmd.exe";
        processInfo.WorkingDirectory = ConfigurationManager.AppSettings["xampp_path"].ToString() + "/mysql/bin";
        processInfo.UseShellExecute = false;
        //processInfo.WindowStyle = ProcessWindowStyle.Hidden;

        try
        {
            Process process = new Process();
            process.StartInfo = processInfo;
            process.StartInfo.RedirectStandardInput = true;
            /*process.StartInfo.RedirectStandardError = true;
            process.StartInfo.RedirectStandardOutput = true;*/
            process.Start();

            StreamWriter wr = process.StandardInput;
            wr.WriteLine("mysqladmin -uroot create 007");
            wr.WriteLine("mysqldump -uroot --routines only | mysql -uroot 007");
            wr.WriteLine("mysqladmin -uroot drop only");
            wr.Write("y"+"\n");
           // processInfo.RedirectStandardInput = false;
            wr.Dispose();
            wr.Close();
            //Response.Write(process.StandardError.ReadToEnd());
           //process.StartInfo.RedirectStandardInput = false;
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
}
