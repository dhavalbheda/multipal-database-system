<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="RunSQL_Database.aspx.cs" Inherits="RunSQL_Database" Title="Untitled Page" %>

<asp:Content ID="Menus" ContentPlaceHolderID="Options" Runat="Server" > 
    <asp:Button ID="structure_Button" runat="server" Text="Structure" onclick="structure_Button_Click" 
       CssClass="header_button" Width="125px" Height="30px" 
        CausesValidation="False"  />
    <asp:Button ID="SQL_Button" runat="server" Text="Run SQL" 
        onclick="SQL_Button_Click"  CssClass="header_button selected_Button" Width="125px" 
        Height="30px" CausesValidation="False" />
    <asp:Button ID="search_Button" runat="server" Text="Search" 
        onclick="search_Button_Click" CssClass="header_button" Width="125px" 
        Height="30px" CausesValidation="False"/>
    <asp:Button ID="Operation_Button" runat="server" Text="Operation" 
        onclick="Operation_Button_Click" CssClass="header_button " 
        Width="125px" Height="30px" CausesValidation="False"/>
    <asp:Button ID="import_Button" runat="server" Text="Import" CssClass="header_button"
        onclick="import_Button_Click" Width="125px" Height="30px" 
        CausesValidation="False" />
    <asp:Button ID="export_Button" runat="server" Text="Export" CssClass="header_button"
        onclick="export_Button_Click" Width="125px" Height="30px" 
        CausesValidation="False" />
    <asp:Button ID="drop_Button" runat="server" Text="Drop" CssClass="header_button"
        onclick="drop_Button_Click" OnClientClick="javascript:return confirm('Are you sure Drop Database ...');" Width="125px" Height="30px" 
        CausesValidation="False" />
    <br />
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="Content_ContentPlaceHolder" Runat="Server">
 <br />
    <asp:Panel ID="Panel1" runat="server" GroupingText="Run SQL Query">
        <br />
        <div style="margin-left:5px;margin-right:5px;">
            <asp:DropDownList ID="Provider_DropDownList" runat="server" Height="27px" 
                Width="100px" />
            <br />
            <br />
            <asp:TextBox ID="Query_TextBox" runat="server" Columns="70" Rows="5" 
                TextMode="MultiLine" ></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="Go_Button" runat="server" Text="Generate Query" 
                onclick="Go_Button_Click" CssClass="button" 
               />
            <br />
            <br />
        </div>
    </asp:Panel>
        <br />
    <asp:GridView ID="Records_GridView" runat="server" Visible="false">
    </asp:GridView>
        <br />
</asp:Content>