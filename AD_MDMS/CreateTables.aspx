<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="CreateTables.aspx.cs" Inherits="CreateTables" Title="Untitled Page" %>

<%@ Register src="ADControl.ascx" tagname="ADControl" tagprefix="uc1" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content_ContentPlaceHolder" Runat="Server">

    <asp:Label ID="TableName_Lable" runat="server" Text="Label">
    </asp:Label>

    <br />
    <br />

    <asp:ScriptManager ID="ScriptManager2" runat="server">
    </asp:ScriptManager>

    <asp:Panel ID="ContentPanel" runat="server">
    </asp:Panel>
    <br />
    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Button" />

</asp:Content>

