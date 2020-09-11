<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="CreateDatabase.aspx.cs" Inherits="CreateDatabase" Title="Untitled Page" %>
<asp:Content ContentPlaceHolderID="head" ID="header" runat="server" >

</asp:Content>
<asp:Content ID="Menus" ContentPlaceHolderID="Options" Runat="Server"> 
    <asp:Button ID="Database_Button" runat="server" Text="Database" 
        onclick="Database_Click" CssClass="header_button selected_Button" 
        Width="130px" Height="30px" />
<asp:Button ID="SQL_Button" runat="server" Text="Run SQL" 
        onclick="SQL_Button_Click" CssClass="header_button" Width="130px" 
        Height="30px" />
<asp:Button ID="Import_Button" runat="server" Text="Database Import" 
        onclick="Import_Button_Click" CssClass="header_button" Width="130px" 
        Height="30px" />
<asp:Button ID="Export_Button" runat="server" Text="Database Export" 
        onclick="Export_Button_Click" CssClass="header_button" Width="124px" 
        Height="30px" />
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="Content_ContentPlaceHolder" runat="server">
    
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View1" runat="server">
            <br />
            <asp:Label ID="CreateDB_Label" runat="server" Text="Create Databases : "></asp:Label>
            <asp:TextBox ID="DatabaseName_TextBox" runat="server" Width="139px"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;
            <asp:DropDownList ID="Provider_DropDownList" runat="server" Width="100px" CssClass="combobox">
            </asp:DropDownList>
            &nbsp;&nbsp;
            <asp:Button ID="CreateDatabase_Button" runat="server" Height="26px" 
                onclick="CreateDatabase_Button_Click" Text="Create" Width="88px" CssClass="button"/>
            <br />
            <br />
            <asp:Label ID="Label2" runat="server" Text="Select Database Provider : "></asp:Label>
            <asp:DropDownList ID="Provider_DropDownList2" runat="server" Width="100px" 
                AutoPostBack="True" 
                onselectedindexchanged="Provider_DropDownList2_SelectedIndexChanged" CssClass="combobox">
            </asp:DropDownList>
            &nbsp;&nbsp;
            <br />
            <br />
            <asp:GridView ID="Database_GridView" runat="server" BackColor="#CCCCCC" 
                BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" 
                CellSpacing="2" ForeColor="Black" Width="300px">
                <RowStyle BackColor="White" HorizontalAlign="Center" />
                <Columns>
                    <asp:TemplateField HeaderText="Select">
                        <ItemTemplate>
                            <asp:CheckBox ID="delete_CheckBox" runat="server" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#CCCCCC" />
                <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
            <asp:Button ID="Delete_Button" runat="server" onclick="Delete_Button_Click" 
                Text="Drop" Visible="False" CssClass="button" Width="64px" />
        </asp:View>
        <br />
        <asp:View ID="View2" runat="server">
            <br />
             <asp:Panel ID="Panel1" runat="server" GroupingText="Run SQL Query">
        <br />
            <div style="margin-left:5px;margin-right:5px;">
            
                <br />
                <asp:TextBox ID="Query_TextBox" runat="server" Columns="70" Rows="5" 
                    TextMode="MultiLine"></asp:TextBox>
                <br />
                <br />
                <asp:Label ID="Label1" runat="server" Text="Do You Really Want To Delete above Databases ?"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="yes_Button" runat="server" Text="Yes" Width="50" OnClick="yes_Button_Click" />
                <asp:Button ID="no_Button" runat="server" Text="No" Width="50" OnClick="no_Button_Click" />
            <br />
            <br />
        </div>
    </asp:Panel> 
        </asp:View>
    </asp:MultiView>            
</asp:Content>

