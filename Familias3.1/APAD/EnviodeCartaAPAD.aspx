<%@ Page Title="" Language="C#" MasterPageFile="~/mast.Master" AutoEventWireup="true" CodeBehind="EnviodeCartaAPAD.aspx.cs" Inherits="Familias3._1.Apadrinamiento.EnviodeCartaAPAD" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="formContGlobal" >
        <style>
            div.ajax__calendar_container {
                width: 185px;
            }
            .heigthall{
                height:100%;
            }
        </style>
        <table class="tableCont cntrar2" id="ingreso" runat="server">
            <tr>
                <td style="height: 27px">
                    <i style="color: red; font-size: 14px; font-weight: bold;">*</i><asp:Label ID="lbldefecha" runat="server" Text=""></asp:Label>
                </td>
                <td style="height: 27px">
                    <asp:RequiredFieldValidator ID="revtxtfecha" runat="server" ControlToValidate="txtdefecha" ValidationGroup="grpGuardar" Display="None"></asp:RequiredFieldValidator>
                    <ajaxToolkit:ValidatorCalloutExtender ID="revtxtfecha_ValidatorCalloutExtender" runat="server" BehaviorID="revtxtfecha_ValidatorCalloutExtender" TargetControlID="revtxtfecha">
                    </ajaxToolkit:ValidatorCalloutExtender>
                    <asp:TextBox ID="txtdefecha" runat="server" CssClass="heigthall" contentEditable="false"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="txtdefecha_CalendarExtender" runat="server" Format="yyyy-MM-dd" BehaviorID="txtdefecha_CalendarExtender" TargetControlID="txtdefecha" />
                </td>

                <td>
                    <i style="color: red; font-size: 14px; font-weight: bold;">*</i><asp:Label ID="lblafecha" runat="server" Text=""></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txbafecha" runat="server" CssClass="heigthall" contentEditable="false"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvafecha" runat="server" ControlToValidate="txbafecha" ValidationGroup="grpGuardar" Display="None"></asp:RequiredFieldValidator>
                    <ajaxToolkit:ValidatorCalloutExtender ID="vceafecha" runat="server" BehaviorID="rfvafecha_ValidatorCalloutExtender" TargetControlID="rfvafecha">
                    </ajaxToolkit:ValidatorCalloutExtender>
                    <ajaxToolkit:CalendarExtender ID="txtafecha_CalendarExtender" runat="server" BehaviorID="txtaffdecha_CalendarExtender" Format="yyyy-MM-dd" TargetControlID="txbafecha" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblCategoria" runat="server" Text=""></asp:Label></td>
                <td colspan="2">
                    <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="comboBoxForm widthall combobox2"></asp:DropDownList></td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Button ID="btnbuscar" runat="server" Text="" CssClass="butonForm" ValidationGroup="grpGuardar" OnClick="btnbuscar_Click" />
                </td>
            </tr>
        </table>

        <table runat="server" id="historial" class="historial">
            <tr>
                <td>
                    <div class="borde mdf" style="background: #fff;">
                        <div class="titulo2">
                            <asp:Label ID="lblconteo" runat="server" Text=""></asp:Label>
                        </div>
                        <asp:Button ID="btnOtraB" runat="server" Text="" CssClass="butonFormSec btnbusqueda" OnClick="btnOtraB_Click" />
                        <asp:GridView ID="gvhistorial" CssClass="tableCont gray otro" runat="server" OnRowCommand="gvhistorial_RowCommand" OnRowDataBound="gvhistorial_RowDataBound" AutoGenerateColumns="false">
                            <Columns>
                                <asp:BoundField DataField="Project" HeaderText="Genero" />
                                <asp:BoundField DataField="MemberId" HeaderText="Genero" />
                                <asp:BoundField DataField="Nombre" HeaderText="Genero" />
                                <asp:BoundField DataField="Familia" HeaderText="Genero" />
                                <asp:BoundField DataField="Escrita" HeaderText="Genero" />
                                <asp:BoundField DataField="NoPadrino" HeaderText="Genero" />
                                <asp:BoundField DataField="NombrePadrino" HeaderText="Genero" />
                                <asp:BoundField DataField="Categoria" HeaderText="Genero" />
                                <asp:ButtonField ButtonType="Button" Text="Enviar/Send" ControlStyle-CssClass="butonForm" HeaderText="Acción/Action" />
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
