<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="Insert_Data.aspx.cs" Inherits="Table_Pages_Insert_Data" Title="Untitled Page" %>


<%@ Register src="~/AD_InsertControl.ascx" tagname="AD_InsertControl" tagprefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">
    .gridview
    {
    	white-space: nowrap;
    	overflow: hidden;
    }
    .control_panel
    {
    	height:90%;
    	width:100%;
    	overflow-y:auto;
    }
    .button_panel
    {
    	width:100%;
    	height:10%;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Options" Runat="Server">

    <asp:Button ID="browse_Button" runat="server" Text="Browse" 
        CssClass="header_button selected_Button" Width="125px" Height="30px" onclick="browse_Button_Click"  
            />
    <asp:Button ID="structure_Button" runat="server" Text="Structure" 
        CssClass="header_button" Width="125px" Height="30px" onclick="structure_Button_Click" 
             />
    <asp:Button ID="sql_Button" runat="server" Text="Run SQL" 
        CssClass="header_button" Width="125px" Height="30px" onclick="sql_Button_Click"
             />
    <asp:Button ID="insert_Button" runat="server" Text="Insert" 
        CssClass="header_button" Width="125px" Height="30px" onclick="insert_Button_Click"
             />
    <asp:Button ID="operation_Button" runat="server" Text="Operations" 
        CssClass="header_button" Width="125px" Height="30px" onclick="operation_Button_Click"
             />
    <asp:Button ID="clear_Button" runat="server" Text="Clear" 
        CssClass="header_button" Width="125px" Height="30px" onclick="clear_Button_Click" OnClientClick="javascript:return confirm('Are you sure ...');"
            />
    <asp:Button ID="drop_Button" runat="server" Text="Drop" 
        CssClass="header_button" Width="125px" Height="30px" onclick="drop_Button_Click" 
            />
        <br />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Content_ContentPlaceHolder" Runat="Server">
 
    <asp:Panel ID="Panel1" runat="server" CssClass="control_panel">
    </asp:Panel>
    <asp:Panel ID="Footer_Panel" runat="server" CssClass="button_panel">
        <asp:Label ID="Label1" runat="server" Text="Add Row : "></asp:Label>
        <asp:TextBox ID="row_TextBox" runat="server"></asp:TextBox>
        &nbsp;&nbsp;
        <asp:Button ID="Add_Button" runat="server" Text="ADD" 
            onclick="Add_Button_Click" CssClass="button" />
        &nbsp;
        <asp:Button ID="Save_Button" runat="server" Text="Save" Width="61px" 
        onclick="Save_Button_Click" CssClass="button"/>
        &nbsp;
        <asp:Button ID="cancel_Button" runat="server" Text="Cancel" Width="61px" 
        CssClass="button" onclick="cancel_Button_Click"/>
    </asp:Panel>
</asp:Content>
