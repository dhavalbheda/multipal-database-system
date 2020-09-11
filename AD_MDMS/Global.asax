<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        GlobalFunction gf = new GlobalFunction();
        // Code that runs on application startup
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e) 
    {
        GlobalFunction gf = new GlobalFunction();
        if (gf.CheckConfiguration())
        {
            Session["IsConfigured"]=true;
            Session["Providers"] = gf.GetDBProvider();
            Session["LoadSidebar"] = true;
            HttpContext.Current.Response.Redirect("~/Default.aspx");
        }
        else
        {
            Session["IsConfigured"] = false;
            HttpContext.Current.Response.Redirect("~/DatabaseConfiguration.aspx");
        }
    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.
        Session["LoadDB"] = false;
        
        string importPath = Server.MapPath("~/ImportDatabase");
        foreach (string file in System.IO.Directory.GetFiles(importPath))
        {
            System.IO.File.Delete(file);
        }
        string exportPath = Server.MapPath("~/ExportDatabase");
        foreach (string file in System.IO.Directory.GetFiles(exportPath))
        {
            System.IO.File.Delete(file);
        }
    }
       
</script>
