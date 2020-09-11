<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="database_export.aspx.cs" Inherits="database_export" Title="Untitled Page" %>

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
        <asp:Button ID="import_Button" runat="server" Text="Import" CssClass="header_button"
            onclick="import_Button_Click" Width="125px" Height="30px" />
        <asp:Button ID="export_Button" runat="server" Text="Export" CssClass="header_button selected_Button"
            onclick="export_Button_Click" Width="125px" Height="30px" />
        <asp:Button ID="drop_Button" runat="server" Text="Drop" CssClass="header_button"
            onclick="drop_Button_Click" OnClientClick="javascript:return confirm('Are you sure Drop Database ...');" Width="125px" Height="30px" />
        <br />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Content_ContentPlaceHolder" Runat="Server">
    <br />
    <br />
    <asp:Label ID="Label2" runat="server" Text="Export File Name : "></asp:Label>
    <asp:TextBox ID="file_TextBox" runat="server"></asp:TextBox>
    <br />
    <br />
    <asp:Button ID="export_file_Button" runat="server" 
        onclick="export_file_Button_Click1" Text="Export Database" />
    <br />
    <br />
</asp:Content>

