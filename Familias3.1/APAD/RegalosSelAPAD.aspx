<%@ Page Title="" Language="C#" MasterPageFile="~/mast.Master" AutoEventWireup="true" CodeBehind="RegalosSelAPAD.aspx.cs" Inherits="Familias3._1.Apadrinamiento.RegalosSelAPAD" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        elemento {
        }

        div.ajax__calendar_container {
            width: 185px;
        }
    </style>
    <div class="formContGlobal">
    <h1 style="text-align: center;">Seleccion de Regalos</h1>
    <table class="tableCont otro" id="rango2" runat="server">
        <tr>
            <td style="height: 26px">
                <i style="color:red; font-size:14px; font-weight:bold;">*</i><asp:Label ID="lbldefecha" runat="server" Text=""></asp:Label></td>
            <td style="width: 122px; height: 26px;">
                <asp:TextBox ID="txtfechade" runat="server" CssClass="textBoxForm" TextMode="DateTime" contentEditable="false"></asp:TextBox>
                <asp:RequiredFieldValidator ID="revtxtfecha" runat="server" ControlToValidate="txtfechade" ValidationGroup="grpGuardar" Display="None"></asp:RequiredFieldValidator>
                <ajaxToolkit:ValidatorCalloutExtender ID="revtxtfecha_ValidatorCalloutExtender" runat="server" BehaviorID="revtxtfecha_ValidatorCalloutExtender" TargetControlID="revtxtfecha">
                </ajaxToolkit:ValidatorCalloutExtender>
                <ajaxToolkit:CalendarExtender ID="txtfechade_CalendarExtender" runat="server" BehaviorID="txtfechade_CalendarExtender" Format="yyyy-MM-dd" TargetControlID="txtfechade"></ajaxToolkit:CalendarExtender>

            </td>

            <td>
                <i style="color:red; font-size:14px; font-weight:bold;">*</i><asp:Label ID="lblafecha" runat="server" Text=""></asp:Label></td>
            <td style="width: 122px">
                <asp:TextBox ID="txtfechaa" runat="server" CssClass="textBoxForm" contentEditable="false" TextMode="DateTime"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="txtfechaa_CalendarExtender" Format="yyyy-MM-dd" runat="server" BehaviorID="txtfechaa_CalendarExtender" TargetControlID="txtfechaa"></ajaxToolkit:CalendarExtender>
                <asp:RequiredFieldValidator ID="revtxtfechaa" runat="server" ControlToValidate="txtfechaa" ValidationGroup="grpGuardar" Display="None"></asp:RequiredFieldValidator>
                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" BehaviorID="revtxtfechaa_ValidatorCalloutExtender" TargetControlID="revtxtfechaa">
                </ajaxToolkit:ValidatorCalloutExtender>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblcat" runat="server" Text=""></asp:Label></td>
            <td>
                <asp:DropDownList ID="ddlcat" runat="server" CssClass="combobox2 comboBoxForm"></asp:DropDownList></td>
            <td>
                <asp:Label ID="lbltipo" runat="server" Text=""></asp:Label></td>
            <td>
                <asp:DropDownList ID="ddltipo" runat="server" CssClass="combobox2 comboBoxForm"></asp:DropDownList></td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Button ID="btnaceptarf" runat="server" CssClass="butonForm" ValidationGroup="grpGuardar" Text="" OnClick="btnaceptarf_Click" /><asp:Button ID="btncancelarf" runat="server" CssClass="butonFormSec" Text="" />
            </td>
        </tr>
    </table>

    <table runat="server" id="historial" class="todo">
        <tr>
            <td>
                <div class="borde mdf">
                    <div class="titulo2">
                        <asp:Label ID="lblVconteo" runat="server" Text=""></asp:Label>
                    </div>
                    <asp:Button ID="btnOtraB" runat="server" Text="" CssClass="butonFormSec btnbusqueda" OnClick="btnOtraB_Click" />
                    <div class="selhistorial">
                        <asp:GridView ID="gvhistorial" CssClass="tableCont gray otro" runat="server" OnRowCommand="gvhistorial_RowCommand" OnRowDataBound="gvhistorial_RowDataBound" AutoGenerateColumns="false">
                            <Columns>
                                <asp:BoundField DataField="Sitio" HeaderText="Genero" />
                                <asp:BoundField DataField="Miembro" HeaderText="Genero" />
                                <asp:BoundField DataField="Nombre" HeaderText="Genero" />
                                <asp:BoundField DataField="Selección" HeaderText="Genero" />
                                <asp:BoundField DataField="Categoria" HeaderText="Genero" />
                                <asp:BoundField DataField="Tipo" HeaderText="Genero" />
                                <asp:BoundField DataField="Notas" HeaderText="Genero" />
                                <asp:BoundField DataField="Edad" HeaderText="Genero" />
                                <asp:BoundField DataField="Cumpleaños" HeaderText="Genero" />
                                <asp:ButtonField ButtonType="Button" HeaderText="Action/Accion" Text="Entregado/Delivery" ControlStyle-CssClass="butonForm" />
                            </Columns>
                        </asp:GridView>
                    </div>
            </td>
        </tr>
    </table>
    </div>
    <script>
        $(document).ready(function () {
            var alto = $('#pnlMenu').height();
            $('.mdf').css('height', (alto - 100) + 'px');
        });
    </script>
</asp:Content>
