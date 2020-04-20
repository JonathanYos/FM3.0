<%@ Page Title="" Language="C#" MasterPageFile="~/mast.Master" AutoEventWireup="true" CodeBehind="RegistroRegaloAPAD.aspx.cs" Inherits="Familias3._1.Apadrinamiento.RegistroRegaloAPAD" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .i50{
            margin-left:50px;
        }
    </style>
<table class="tableCont" style="margin: 0 auto;" runat="server" id="tbfiltros">
        <tr>
            <th>
                <asp:Label ID="lblsitio" runat="server"></asp:Label>
            </th>
            <td>
                <asp:DropDownList ID="ddlsitio" runat="server" CssClass="comboBoxForm">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <th>
                <asp:Label ID="lblmiembro" runat="server"></asp:Label>
            </th>
            <td>
                <asp:TextBox ID="txtmiembro" runat="server" CssClass="textBoxForm" onkeypress="return esDigito(event)"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnbuscar" runat="server" CssClass="butonForm" OnClick="btnbuscar_Click" />
            </td>
        </tr>
    </table>
    <div class="contenedor">
        <table class="tableCont hijo dttamano pink" runat="server" id="tbregistro">
            <tr>
                <th>
                    <i style="color:red; font-size:14px; font-weight:bold;">*</i><asp:Label ID="lblcategoria" runat="server" Text=""></asp:Label>
                </th>
                <td>
                    <asp:DropDownList ID="ddlcategoria" runat="server" CssClass="comboBoxForm largo">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>
                    <i style="color:red; font-size:14px; font-weight:bold;">*</i><asp:Label ID="lbltipo" runat="server" Text=""></asp:Label>
                </th>
                <td style="width: 122px">
                    <asp:DropDownList ID="ddltipo" CssClass="comboBoxForm largo" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>
                    <asp:Label ID="lblnotas" runat="server" Text=""></asp:Label>
                </th>
                <td>
                    <asp:TextBox ID="txtnotas" runat="server" CssClass="textBoxForm largo" MaxLength="40"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                    <asp:Label ID="lblfechaentrega" runat="server" Text=""></asp:Label>
                </th>
                <td>
                    <asp:CheckBox ID="chkentrega" runat="server" />
                    <asp:Label ID="lblVseleccion" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lblVnotas2" runat="server" Visible="False"></asp:Label>
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
        <asp:GridView ID="gvhistorial" runat="server" CssClass="tableCont gray hijo i50" AutoGenerateColumns="false" OnRowCommand="gvhistorial_RowCommand" OnRowDataBound="gvhistorial_RowDataBound">
            <Columns>
                <asp:BoundField DataField="Selección"/>
                <asp:BoundField DataField="Categoria"/>
                <asp:BoundField DataField="Tipo"/>
                <asp:BoundField DataField="Notas"/>
                <asp:BoundField DataField="Entrega"/>
                <asp:ButtonField ButtonType="button" HeaderText="Accion/Action" Text="Seleccionar/Select" ControlStyle-CssClass="butonForm" />
            </Columns>
        </asp:GridView>
    </div>
    <script>
        $(document).ready(function () {
            var tamanoregistro = $('.dttamano').width();
            var tamanohistorial = $('#ContentPlaceHolder1_gvhistorial').width();
            var tamanototal = tamanohistorial + 70 + tamanoregistro;
            $('.contenedor').css('width', tamanototal + 'px');

        });
    </script>
</asp:Content>
