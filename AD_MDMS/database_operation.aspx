<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="database_operation.aspx.cs" Inherits="database_operation" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Menus" ContentPlaceHolderID="Options" Runat="Server" > 
    <asp:Button ID="structure_Button" runat="server" Text="Structure" onclick="structure_Button_Click" 
       CssClass="header_button" Width="125px" Height="30px" 
        CausesValidation="False"  />
    <asp:Button ID="SQL_Button" runat="server" Text="Run SQL" 
        onclick="SQL_Button_Click"  CssClass="header_button" Width="125px" 
        Height="30px" CausesValidation="False" />
    <asp:Button ID="search_Button" runat="server" Text="Search" 
        onclick="search_Button_Click" CssClass="header_button" Width="125px" 
        Height="30px" CausesValidation="False"/>
    <asp:Button ID="Operation_Button" runat="server" Text="Operation" 
        onclick="Operation_Button_Click" CssClass="header_button selected_Button" 
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
<asp:Content ID="Content3" ContentPlaceHolderID="Content_ContentPlaceHolder" Runat="Server">
    <br />
    <br />
    <asp:Panel ID="CreateTable_Panel" runat="server" 
        GroupingText='<%# "Create Table on \"" +dbname+"\" : "%>' BackColor="#D7EBFB" 
        BorderWidth="0px">
        &nbsp;&nbsp;
        <asp:Label ID="CreateTable_Label" runat="server" Text="Create Table :"></asp:Label>
        &nbsp;&nbsp;&nbsp;<asp:TextBox ID="TableName_TextBox" runat="server" 
            Width="139px" ValidationGroup="createTable" ></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
            ControlToValidate="TableName_TextBox" Display="Dynamic" 
            ErrorMessage="Table Name Required" ValidationGroup="createTable" SetFocusOnError="True">*</asp:RequiredFieldValidator>
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Fields_Label" runat="server" Text="No Of Fields  :"></asp:Label>
        &nbsp;
        <asp:TextBox ID="Field_TextBox" runat="server" Width="43px" 
            ValidationGroup="createTable"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
            ControlToValidate="Field_TextBox" Display="Dynamic" 
            ErrorMessage="No Of Fields Require" ValidationGroup="createTable" SetFocusOnError="True">*</asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
            ControlToValidate="Field_TextBox" Display="Dynamic" 
            ErrorMessage="Must Be Number" ValidationExpression="^[0-9]*$" 
            ValidationGroup="createTable" SetFocusOnError="True">*</asp:RegularExpressionValidator>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="TableCreate_Button" runat="server" Text="Create" 
            CssClass="button" onclick="TableCreate_Button_Click" 
            ValidationGroup="createTable"/>
           <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
            DisplayMode="List" ShowMessageBox="True" ShowSummary="False" 
            ValidationGroup="createTable" />
           <br /><br />
    </asp:Panel>
    <br />
    
    <asp:Panel ID="Panel1" runat="server" GroupingText='Rename Database To : ' 
        BackColor="#D7EBFB">
        &nbsp;&nbsp;
        <asp:TextBox ID="Rename_TextBox" runat="server" ValidationGroup="rename" 
            Width="202px"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
            ControlToValidate="Rename_TextBox" ErrorMessage="Database Name Required" 
            ValidationGroup="rename" SetFocusOnError="True">*</asp:RequiredFieldValidator>
        &nbsp;&nbsp;
        <asp:Button ID="rename_Button" runat="server" CssClass="button" 
            Text="Rename Database" ValidationGroup="rename" 
            onclick="rename_Button_Click" />
        <asp:ValidationSummary ID="ValidationSummary2" runat="server" 
            ShowMessageBox="True" ShowSummary="False" ValidationGroup="rename" 
            DisplayMode="List" />
        <br /><br />
    </asp:Panel>
    <br />
    
    <asp:Panel ID="Panel2" runat="server" GroupingText='Copy Database To : ' BackColor="#D7EBFB">
    &nbsp;&nbsp;
        <asp:TextBox ID="copy_TextBox" runat="server"  Width="202px" ValidationGroup="copy"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Database Name Required" 
            ValidationGroup="copy" ControlToValidate="copy_TextBox" SetFocusOnError="True">*</asp:RequiredFieldValidator>
        &nbsp;&nbsp;
        <asp:Button ID="copy_Button" runat="server" CssClass="button" 
            Text="Copy Database" ValidationGroup="copy" />
        <br />
        &nbsp;&nbsp;
        <asp:RadioButton ID="structure_RadioButton" runat="server" Text="  Structure Only" GroupName="copy1" Checked="true" />
        <br />&nbsp;&nbsp;
        <asp:RadioButton ID="structure_data_RadioButton" runat="server" Text="  Structure and Data" GroupName="copy1" />
        <asp:ValidationSummary ID="ValidationSummary3" runat="server" 
            ShowMessageBox="True" ShowSummary="False" ValidationGroup="copy" DisplayMode="List" />
            <br />
    </asp:Panel>
</asp:Content>

