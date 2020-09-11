<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AD_InsertControl.ascx.cs" Inherits="AD_InsertControl" %>


<br />

<style type="text/css">
    .style1
    {
        width: 106px;
    }
    .style2
    {
        width: 137px;
    }
    .table
    {
    	border:1px solid black;
    	margin-right:0px;
    	margin-bottom:0px;
    	margin-top:0px;
    	display:block;
    	
        
    }
    .inner
    {
    	width:100%;
    	text-align:center;
    	font-size:larger;
    	vertical-align:middle;
    }
    
    .style3
    {
        width: 121px;
    }
</style>
<p>
    <asp:CheckBox ID="Ignore_CheckBox" runat="server" Checked="True" 
        oncheckedchanged="Ignore_CheckBox_CheckedChanged" Text="Ignore" />
</p>
<table style="width:auto;" border=1>
    <tr>
        <td class="style1">
            <asp:Table ID="Table_Column" GridLines="Horizontal" runat="server" CssClass="inner" CellPadding="5" CellSpacing="5">
            </asp:Table>
        </td>
        <td class="style2">
            <asp:Table ID="Table_DataType" GridLines="Horizontal" runat="server" CssClass="inner" CellPadding="5" CellSpacing="5">
            </asp:Table>
        </td>
        <td class="style3">
            <asp:Table ID="Table_Value" GridLines="Horizontal" runat="server" CssClass="inner" CellPadding="5" CellSpacing="5">
            </asp:Table>
        </td>
    </tr>
</table>
<br />

