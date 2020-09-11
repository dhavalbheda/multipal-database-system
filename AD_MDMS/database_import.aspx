<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="database_import.aspx.cs" Inherits="database_import" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Options" Runat="Server">
    <asp:Button ID="structure_Button" runat="server" Text="Structure" onclick="structure_Button_Click" 
           CssClass="header_button" Width="125px" Height="30px"  />
    <asp:Button ID="SQL_Button" runat="server" Text="Run SQL" 
            onclick="SQL_Button_Click"  CssClass="header_button" Width="125px" Height="30px" 
             />
    <asp:Button ID="search_Button" runat="server" Text="Search" 
            onclick="search_Button_Click" CssClass="header_button" Width="125px" Height="30px"
             />
    <asp:Button ID="Operation_Button" runat="server" Text="Operation" 
            onclick="Operation_Button_Click" CssClass="header_button" Width="125px" Height="30px"
             />
        <asp:Button ID="import_Button" runat="server" Text="Import" CssClass="header_button selected_Button"
            onclick="import_Button_Click" Width="125px" Height="30px" />
        <asp:Button ID="export_Button" runat="server" Text="Export" CssClass="header_button"
            onclick="export_Button_Click" Width="125px" Height="30px" />
        <asp:Button ID="drop_Button" runat="server" Text="Drop" CssClass="header_button"
            onclick="drop_Button_Click" OnClientClick="javascript:return confirm('Are you sure Drop Database ...');" Width="125px" Height="30px" />
        <br />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Content_ContentPlaceHolder" Runat="Server">
    <br />
    <asp:Label ID="label1" runat="server" Text="Select File To Import : "></asp:Label>
    <asp:FileUpload ID="FileUpload1" runat="server" />
&nbsp;<br />
    <br />
    <asp:Button ID="importDB_Button" runat="server" onclick="importDB_Button_Click" 
        Text="Import Database" />
    <br />
    <br />
</asp:Content>


