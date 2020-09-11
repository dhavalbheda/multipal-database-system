<%@ Page Language="C#" MasterPageFile="~/MasterPages/Main_MasterPage.master" AutoEventWireup="true" CodeFile="CreateDatabase.aspx.cs" Inherits="CreateDatabase" Title="Untitled Page" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="Label2" runat="server" Text="Create Database"></asp:Label>
    <asp:TextBox ID="db_TextBox" runat="server" Font-Bold="False"  EnableTheming="false"
        Font-Names="Arial" Font-Size="Medium" Height="27px" Width="156px"></asp:TextBox>
    &nbsp;&nbsp;&nbsp;
    <asp:DropDownList ID="Provider_DropDownList" runat="server" Height="21px" 
         Width="99px">
        <asp:ListItem Selected="True" Value="MYSQL">MySQL</asp:ListItem>
        <asp:ListItem Value="ORACLE">Oracle</asp:ListItem>
        <asp:ListItem Value="MSSQL">Ms SQL</asp:ListItem>
    </asp:DropDownList>
    &nbsp;
    <asp:Button ID="Create_Button" runat="server" onclick="Create_Button_Click" 
        Text="Create" Width="100px" />
    <br />
    <br />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer_contentPlaceHolder" Runat="Server">
    <asp:Label ID="msg_Label" runat="server" Text="Label"></asp:Label>
</asp:Content>

