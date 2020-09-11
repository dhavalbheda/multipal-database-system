<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="database_structure.aspx.cs" Inherits="Database_structure" Title="Untitled Page" %>

<asp:Content ID="Menus" ContentPlaceHolderID="Options" Runat="Server" > 
    <asp:Button ID="structure_Button" runat="server" Text="Structure" onclick="structure_Button_Click" 
       CssClass="header_button selected_Button" Width="125px" Height="30px" 
        CausesValidation="False"  />
    <asp:Button ID="SQL_Button" runat="server" Text="Run SQL" 
        onclick="SQL_Button_Click"  CssClass="header_button" Width="125px" 
        Height="30px" CausesValidation="False" />
    <asp:Button ID="search_Button" runat="server" Text="Search" 
        onclick="search_Button_Click" CssClass="header_button" Width="125px" 
        Height="30px" CausesValidation="False"/>
    <asp:Button ID="Operation_Button" runat="server" Text="Operation" 
        onclick="Operation_Button_Click" CssClass="header_button" Width="125px" 
        Height="30px" CausesValidation="False"/>
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

<asp:Content ID="Content" ContentPlaceHolderID="Content_ContentPlaceHolder" runat="server">

    <asp:Panel ID="CreateTable_Panel" runat="server">
        <br />
        <br />
        <asp:Label ID="CreateTable_Label" runat="server" Text="Create Table :"></asp:Label>
                
        &nbsp;&nbsp;&nbsp;<asp:TextBox ID="TableName_TextBox" runat="server" 
            Width="139px"></asp:TextBox>
        
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
            ControlToValidate="TableName_TextBox" Display="Dynamic" 
            ErrorMessage="Table Name Required">*</asp:RequiredFieldValidator>
        
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Fields_Label" runat="server" Text="No Of Fields  :"></asp:Label>
        &nbsp;
        <asp:TextBox ID="Field_TextBox" runat="server" Width="43px"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
            ControlToValidate="Field_TextBox" Display="Dynamic" 
            ErrorMessage="No of Fields Required">*</asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
            ControlToValidate="Field_TextBox" Display="Dynamic" 
            ErrorMessage="Must Be Number" ValidationExpression="^[0-9]*$">*</asp:RegularExpressionValidator>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="TableCreate_Button" runat="server" Text="Create" 
            CssClass="button" onclick="TableCreate_Button_Click"/>
        <br />
        <br />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
            DisplayMode="List" ShowMessageBox="True" ShowSummary="False" />
        <br />
        <br />
        <asp:GridView ID="TablesGridView" runat="server" BackColor="#CCCCCC" 
                BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" 
                CellSpacing="2" ForeColor="Black" Width="459px" 
            onrowcommand="TablesGridView_RowCommand">
                <RowStyle BackColor="White" HorizontalAlign="Center" />
                
                <Columns>
                    <asp:TemplateField HeaderText="Action" ItemStyle-Width="150px">
                        <ItemTemplate>
                            <asp:ImageButton ID="ImageButton1" runat="server"  
                                ImageUrl="Images/b_browse.png" CommandName="Brows_Button" Width="16px" 
                                    CommandArgument='<%# Container.DataItemIndex %>' 
                                CausesValidation="False" />
                            <asp:ImageButton ID="ImageButton2" runat="server" 
                                ImageUrl="Images/b_browse.png" CommandName="Structure_Button" Width="16px" 
                                    CommandArgument='<%# Container.DataItemIndex %>' 
                                CausesValidation="False" />
                            <asp:ImageButton ID="ImageButton3" runat="server" 
                                ImageUrl="Images/b_browse.png" CommandName="Insert_Button" Width="16px" 
                                    CommandArgument='<%# Container.DataItemIndex %>' 
                                CausesValidation="False" />
                            <asp:ImageButton ID="ImageButton4" runat="server"
                                ImageUrl="Images/b_browse.png" CommandName="Clean_Button" Width="16px" OnClientClick="javascript:return confirm('Are you sure To Delete All Records ...');"
                                    CommandArgument='<%# Container.DataItemIndex %>' 
                                CausesValidation="False" />
                            <asp:ImageButton ID="ImageButton5" runat="server" 
                                ImageUrl="Images/b_browse.png" CommandName="Drop_Button" Width="16px" OnClientClick="javascript:return confirm('Are you sure To Delete Table ...');"
                                    CommandArgument='<%# Container.DataItemIndex %>' 
                                CausesValidation="False" />
                        </ItemTemplate>
                        <ItemStyle Width="150px" />
                    </asp:TemplateField>
                </Columns>
            <FooterStyle BackColor="#CCCCCC" />
            <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
        <br />
        <br />
        
               
    </asp:Panel>

</asp:Content>
