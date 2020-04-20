<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/mast.Master" CodeBehind="CambiarPreferencias.aspx.cs" Inherits="Familias3._1.MISC.CambiarPreferencias" %>
<%@ MasterType virtualPath="~/mast.Master"%> 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="formCont searchFrm">
    <table>
        <tr>
            <td>
                <asp:Label ID="lblSitio" class="labelBoldForm" runat="server" ></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlSitio" class="comboBoxBlueForm" runat="server"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="text-align:right">
                <asp:CheckBox ID="chkIdioma" runat="server" Checked="false"/>
            </td>
            <td>
                <asp:Label ID="lblIdioma" class="labelBoldForm" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td></td>
            <td>
                 <asp:Button ID="btnCambiarPref" class="butonForm" runat="server" Text="Button" OnClick="btnCambiarPref_Click" />
            </td>
        </tr>
    </table>
    </div>
</asp:Content>