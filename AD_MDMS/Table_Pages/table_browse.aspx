<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="table_browse.aspx.cs" Inherits="table_browse" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">
    .gridview
    {
    	white-space: nowrap;
    	overflow: hidden;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Options" Runat="Server">

    <asp:Button ID="browse_Button" runat="server" Text="Browse" CssClass="header_button selected_Button" Width="125px" Height="30px"  
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
    
    <br />
&nbsp;<asp:GridView ID="Table_GridView" runat="server" BackColor="#CCCCCC" CssClass="gridview"
        BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" 
        CellSpacing="2" ForeColor="Black" Width="448px" 
        onrowcommand="Table_GridView_RowCommand">
        <RowStyle BackColor="White" HorizontalAlign="Center" />
        <Columns>
            <asp:TemplateField HeaderText="Action">
                <ItemTemplate>
                    <asp:CheckBox ID="Select_CheckBox" runat="server" />
                    &nbsp;&nbsp;
                    <asp:ImageButton ID="Edit_imageButton" runat="server" 
                        ImageUrl="~/Images/edit.png" />
                    &nbsp;&nbsp;
                    <asp:ImageButton ID="Delete_ImageButton" runat="server" 
                        ImageUrl="~/Images/drop.png"
                        CommandArgument="<%# Container.DataItemIndex %>" CommandName="Delete_Button" 
                        Width="16px" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="#CCCCCC" />
        <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
    </asp:GridView>   
    <br />
&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="Button1" runat="server" CssClass="button" 
        onclick="MultipleDeleteButton_Click" Text="Delete" Width="68px" />
&nbsp;   
</asp:Content>

