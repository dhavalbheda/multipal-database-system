<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="RunSQL.aspx.cs" Inherits="RunSQL" Title="Untitled Page" %>

<asp:Content ContentPlaceHolderID="head" ID="header" runat="server" >
    <style type="text/css">
        .header_button
        {
            background-color:Transparent;
	        border-style:solid;
	        margin-top:10px;
	        color:#183187;
	        margin-left:5px;
	        height:30px;
	        width:150px;
	        cursor:pointer;
	        font-size: 0.9em;
	        border-radius:10px;
	        font-weight:bold;
	        border-color: #090909;
	        
        }
        .selected_Button
        {
            background-color:#EEF3F6;
        }
        .excute_button
        {
        	width:130px;
        	font-weight:bolder;
        	background-color: #fcefef;
            color: black;
        }

    </style>
</asp:Content>


<asp:Content ID="Menus" ContentPlaceHolderID="Options" Runat="Server">
    <asp:Button ID="Database_Button" runat="server" Text="Database" 
        onclick="Database_Click" CssClass="header_button" />
<asp:Button ID="SQL_Button" runat="server" Text="Run SQL" 
        onclick="SQL_Button_Click" CssClass="header_button selected_Button" />
<asp:Button ID="Import_Button" runat="server" Text="Database Import" 
        onclick="Import_Button_Click" CssClass="header_button" />
<asp:Button ID="Export_Button" runat="server" Text="Database Export" 
        onclick="Export_Button_Click" CssClass="header_button" />
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="Content_ContentPlaceHolder" Runat="Server">

    <br />
    <asp:Panel ID="Panel1" runat="server" GroupingText="Run SQL Query">
        <br />
        <div style="margin-left:5px;margin-right:5px;">
            <asp:DropDownList ID="Provider_DropDownList" runat="server" Height="27px" 
                Width="100px" CssClass="combobox" >
            </asp:DropDownList>
            <br />
            <br />
            <asp:TextBox ID="Query_TextBox" runat="server" Columns="70" Rows="5" 
                TextMode="MultiLine"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="Go_Button" runat="server" Text="Generate Query" 
                onclick="Go_Button_Click" CssClass="header_button excute_button" />
            <br />
            <br />
        </div>
    </asp:Panel>
        <br />
    <asp:GridView ID="Records_GridView" runat="server" Visible="False" 
        BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" 
        CellPadding="4" CellSpacing="2" ForeColor="Black">
        <RowStyle BackColor="White" HorizontalAlign="Center" />
        <FooterStyle BackColor="#CCCCCC" />
        <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
        <br />
</asp:Content>

