<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="table_structure.aspx.cs" Inherits="Tables_pages_table_structure" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Options" Runat="Server">

    <asp:Button ID="browse_Button" runat="server" Text="Browse" 
        CssClass="header_button" Width="125px" Height="30px" onclick="browse_Button_Click"  
            />
    <asp:Button ID="structure_Button" runat="server" Text="Structure" CssClass="header_button selected_Button" Width="125px" Height="30px" 
             />
    <asp:Button ID="sql_Button" runat="server" Text="Run SQL" CssClass="header_button" Width="125px" Height="30px"
             />
    <asp:Button ID="insert_Button" runat="server" Text="Insert" 
        CssClass="header_button" Width="125px" Height="30px" onclick="insert_Button_Click"
             />
    <asp:Button ID="operation_Button" runat="server" Text="Operations" CssClass="header_button" Width="125px" Height="30px"
             />
    <asp:Button ID="clear_Button" runat="server" Text="Clear" CssClass="header_button" Width="125px" Height="30px" 
            />
    <asp:Button ID="drop_Button" runat="server" Text="Drop" CssClass="header_button" Width="125px" Height="30px" 
            />
        <br />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Content_ContentPlaceHolder" Runat="Server">
    <br />
    <br />
    <asp:Label ID="table_Label" runat="server" Text=""></asp:Label>
    <br />
    <br />
    <asp:GridView ID="table_structure_GridView" runat="server" BackColor="#CCCCCC" 
        BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" 
        CellSpacing="2" ForeColor="Black">
        <RowStyle BackColor="White" HorizontalAlign="Center" />
        <FooterStyle BackColor="#CCCCCC" />
        <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
    </asp:GridView>
</asp:Content>

