<%@ Page Title="" Language="C#" MasterPageFile="~/mast.Master" AutoEventWireup="true" CodeBehind="RegistroViveresAPAD.aspx.cs" Inherits="Familias3._1.Apadrinamiento.RegistroViveresAPAD" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <div class="contenedor3"> 
       <table class="otro tableCont">
    <tr>
        <th>
            <asp:Label ID="lblrazon" runat="server" Text=""></asp:Label>
        </th>
        <td>
            <asp:DropDownList ID="ddlrazon" runat="server" CssClass="comboBoxForm largo">
            </asp:DropDownList>
            <asp:Label ID="lblfecha" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <th>
            <asp:Label ID="lblnotas" runat="server" Text=""></asp:Label>
        </th>
        <td>
            <asp:TextBox ID="txtnotas" runat="server" CssClass="textBoxForm" MaxLength="40"></asp:TextBox>
            
        </td>
    </tr>
    <tr>
        <td colspan="2">
                <asp:Button ID="btnaceptar" runat="server" Text="" CssClass="butonForm" OnClick="btnaceptar_Click" /><asp:Button ID="btncancelar" runat="server" Text="" CssClass="butonFormSec" OnClick="btncancelar_Click" />
                <asp:Button ID="btnmodificar" runat="server" Text="" CssClass="butonForm" OnClick="btnmodificar_Click" />
                <asp:Button ID="btneliminar" runat="server" Text="" CssClass="butonForm" OnClick="btneliminar_Click" />
            </td>
    </tr>
</table>
    
    <asp:GridView ID="gvhistorial" runat="server" CssClass="tableCont otro orange" OnRowCommand="gvhistorial_RowCommand" OnRowDataBound="gvhistorial_RowDataBound">
        <Columns>
            <asp:ButtonField ButtonType="Button" HeaderText="Accion/Action" Text="Seleccionar/Select" ControlStyle-CssClass="butonForm" >
<ControlStyle CssClass="butonForm"></ControlStyle>
            </asp:ButtonField>
        </Columns>
    </asp:GridView>
    </div>
</asp:Content>
