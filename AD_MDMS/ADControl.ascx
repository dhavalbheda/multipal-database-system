<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ADControl.ascx.cs" Inherits="ADControl" %>

<div style="float:left">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>
<table style="width: auto;">
    <tr>
        <td align="right">
            <asp:Label ID="FieldName_Label" runat="server" Text="Field Name : "></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="FieldName_TextBox" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="name_RequiredFieldValidator" runat="server" 
                ControlToValidate="FieldName_TextBox" ErrorMessage="Field Name Required">*</asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="Type_Label" runat="server" Text="Field Type : "></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="Type_DropDownList" runat="server" Width="65%" 
                onselectedindexchanged="Type_DropDownList_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="Size_Label" runat="server" Text="Size / Length : "></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="size_TextBox" runat="server"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                ControlToValidate="size_TextBox" ErrorMessage="Size should be in Numeric Value" 
                SetFocusOnError="True" ValidationExpression="^[0-9]*$" Display="Dynamic">Must Be Number</asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="size_RequiredFieldValidator" runat="server" 
                ControlToValidate="size_TextBox" Display="Dynamic" Enabled="False" 
                ErrorMessage="Size Required">*</asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="Default_Label" runat="server" Text="Default Value : "></asp:Label>
        </td>
        <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="Default_TextBox" runat="server" Visible="False"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="default_RequiredFieldValidator" runat="server" 
                                ControlToValidate="Default_TextBox" Display="Dynamic" Enabled="False" 
                                ErrorMessage="Default Value Required" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                            <asp:CheckBox ID="Default_CheckBox" runat="server" AutoPostBack="True" 
                                oncheckedchanged="Default_CheckBox_CheckedChanged" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="null_Label" runat="server" Text="Allow Null : "></asp:Label>
        </td>
        <td>
            <asp:CheckBox ID="null_CheckBox" runat="server" />
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="AI_Label" runat="server" Text="Auto Increment : "></asp:Label>
        </td>
        <td>
            <asp:CheckBox ID="ai_CheckBox" runat="server" />
        </td>
    </tr>
</table>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" 
    DisplayMode="List" ShowMessageBox="True" ShowSummary="False" />
    <br />
    
</div>
