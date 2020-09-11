<%@ Page EnableTheming="false" Theme="Whity" Language="C#" Trace="true" AutoEventWireup="true" CodeFile="DatabaseConfiguration.aspx.cs" Inherits="DatabaseConfiguration" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Database Configuration</title>
    <link rel="Shortcut icon" href="Images/small_logo.png" />
    <style type="text/css">
    .ConfigurationPage_Buttons
    {
	    background-color:Transparent;
	    border-style:solid;
	    color:White;
	    margin-bottom:5px;
	    margin-top:5px;
	    margin-left:5px;
	    height:50px;
	    width:200px;
	    cursor:pointer;
	    font-size:medium;
	    border-radius:10px;
    }
    .ConfigurationTab
    {
    	border-right:1px solid blue;
    }
    .Configuration_Form
    {
    	font-family:Times New Roman;
    	font-size : large;
    }
    .FormButton
    {
    	height:40px;
    	width:100%;
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table style="width:100%;height:100%;" bgcolor="#A0B9C0">
        <tr style="height:20%" bgcolor="Black">
            <td style="width:15%;" align="center" bgcolor="#1E2125" valign="middle">
                <asp:ImageButton ID="Logo_ImageButton" runat="server" Height="80px" 
                    ImageUrl="~/Images/Transparent_logo.png" Width="80px" 
                    onclick="Logo_ImageButton_Click" />
            </td>
            <td align="center" bgcolor="#1E2125" valign="middle">
                <asp:Label ID="SiteName_Label" runat="server" Font-Bold="False" Font-Italic="True" 
                    Font-Names="Century Gothic" Font-Size="X-Large" ForeColor="White" 
                    Text="A.D. - Database Management System "></asp:Label>
            </td>
        </tr>
        <tr style="height:8%;">
            <td colspan="2" bgcolor="#666666" align="justify" valign="bottom">
                <asp:Button ID="configuration_button" runat="server" Text="Database Configuration"
                    CssClass="ConfigurationPage_Buttons" onclick="configuration_button_Click" />
                <asp:Button ID="theme_Button" runat="server" Text="Theme" CssClass="ConfigurationPage_Buttons"
                    onclick="theme_Button_Click"/>
                <asp:Button ID="Home_Button" runat="server" Text="Go To Home" CssClass="ConfigurationPage_Buttons"
                    onclick="Home_Button_Click"  />
            </td>
        </tr>
        <tr>
            <td colspan="2" align="justify" valign="top">
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View1" runat="server">
                        <table style="width:100%;height:100%;">
                            <tr>
                                <td width="15%" class="ConfigurationTab" align="center" valign="middle">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:RadioButtonList ID="DatabaseProvider_RadioButtonList" runat="server" AutoPostBack="True" 
                                                CellSpacing="20" Font-Bold="True" Font-Italic="True" Font-Names="Helvetica" 
                                                Font-Size="Medium" Font-Underline="False" 
                                                onselectedindexchanged="RadioButtonList1_SelectedIndexChanged">
                                                <asp:ListItem Value="MYSQL"> My SQL</asp:ListItem>
                                                <asp:ListItem Value="ORACLE"> Oracle</asp:ListItem>
                                                <asp:ListItem Value="MSSQL"> MS SQL</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td align="center" valign="middle">
                                    <table cellspacing="20">
                                        <tr>
                                            <td align="right" class="style3">
                                                <asp:Label ID="server_Label" runat="server" CssClass="Configuration_Form" 
                                                    Text="Server Name : "></asp:Label>
                                            </td>
                                            <td class="style1">
                                                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="Server_TextBox" runat="server" CssClass="Configuration_Form"></asp:TextBox>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td class="style2">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="style3">
                                                <asp:Label ID="UserName_Label" runat="server" CssClass="Configuration_Form" 
                                                    Text="User Name : "></asp:Label>
                                            </td>
                                            <td class="style1">
                                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="Unm_TextBox" runat="server" CssClass="Configuration_Form"></asp:TextBox>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td class="style2">
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                    <ContentTemplate>
                                                        <asp:CheckBox ID="Unm_CheckBox" runat="server" AutoPostBack="True" 
                                                            oncheckedchanged="Unm_CheckBox_CheckedChanged" Text="   No User Name" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="style3">
                                                <asp:Label ID="Password_Label" runat="server" CssClass="Configuration_Form" 
                                                    Text="Password : "></asp:Label>
                                            </td>
                                            <td class="style1">
                                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="Pwd_TextBox" runat="server" CssClass="Configuration_Form" 
                                                            TextMode="Password"></asp:TextBox>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td class="style2">
                                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                    <ContentTemplate>
                                                        <asp:CheckBox ID="Pwd_CheckBox" runat="server" AutoPostBack="True" 
                                                            oncheckedchanged="Pwd_CheckBox_CheckedChanged" Text="   No Password" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr align="center">
                                            <td class="style3">
                                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                    <ContentTemplate>
                                                        <asp:Button ID="Clear_Button" runat="server" CssClass="FormButton" 
                                                            onclick="Clear_Button_Click" Text="Clear" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td class="style1">
                                                <asp:Button ID="TestConnection_Button" runat="server" CssClass="FormButton" 
                                                    onclick="TestConnection_Button_Click" Text="Test Connection" />
                                            </td>
                                            <td class="style2">
                                                <asp:Button ID="Save_Button" runat="server" CssClass="FormButton" 
                                                    onclick="Save_Button_Click" Text="Save" />
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:Panel ID="Panel1" runat="server">
                                        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                            <ContentTemplate>
                                                <asp:Label ID="Status_Label" runat="server" Font-Size="X-Large"></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                    <asp:View ID="View2" runat="server">
                        Theme Part
                    </asp:View>
                </asp:MultiView>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">