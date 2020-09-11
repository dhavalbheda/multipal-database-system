<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="Database.aspx.cs" Inherits="Database" Title="Untitled Page" %>

<asp:Content ID="Menus" ContentPlaceHolderID="Options" Runat="Server" > 
    <asp:Button ID="Database_Button" runat="server" Text="Database" 
        onclick="Button1_Click" />
<asp:Button ID="SQL_Button" runat="server" Text="Run SQL" 
        onclick="SQL_Button_Click" />
<asp:Button ID="Import_Button" runat="server" Text="Database Import" 
        onclick="Import_Button_Click" />
<asp:Button ID="Export_Button" runat="server" Text="Database Export" 
        onclick="Export_Button_Click" />
</asp:Content>

