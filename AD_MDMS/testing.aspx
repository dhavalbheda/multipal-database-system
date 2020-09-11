<%@ Page Language="C#" AutoEventWireup="true" CodeFile="testing.aspx.cs" Inherits="testing" %>

<%@ Register src="ADControl.ascx" tagname="ADControl" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <br />
    <br />
    <br />
    <asp:Button ID="Button1" runat="server" Text="With Validations" />
        <asp:Button ID="Button2" runat="server" Text="without Validations" 
            CausesValidation="False" />
            
            <asp:FileUpload ID="FileUpload1" runat="server" />
        <asp:Button ID="Button3" runat="server" CausesValidation="False" 
            onclick="Button3_Click" Text="Button" />
    </form>
</body>
</html>
