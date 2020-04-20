<%@ Page Language="C#" Async="true" AutoEventWireup="true" MasterPageFile="~/mast.Master" CodeBehind="AgregarSolicitudAfiliacion.aspx.cs" Inherits="Familias3._1.AFIL.AgregarSolicitudAfiliacion" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ MasterType VirtualPath="~/mast.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <asp:Panel runat="server" CssClass="formCont" ID="pnlRegistro">
            <table class="tblCenter">
                <tr>
                    <th></th>
                    <th></th>
                    <th style="width: 60px"></th>
                    <th></th>
                    <th></th>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblEstadoSolAfil" CssClass="labelBoldForm">*Estado de Solicitud:</asp:Label></td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlEstadoSolAfil" CssClass="comboBoxBlueForm"></asp:DropDownList></td>
                    <td></td>
                    <td>
                        <asp:Label runat="server" ID="lblTipoAfil" CssClass="labelBoldForm">*Tipo de Afiliacion:</asp:Label></td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlTipoAfil" CssClass="comboBoxBlueForm"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblClasif" CssClass="labelBoldForm">Clasificación:</asp:Label></td>
                    <td>
                        <asp:TextBox runat="server" ID="txbClasif" CssClass="textBoxBlueForm"></asp:TextBox></td>
                    <td></td>
                    <td>
                        <asp:Label runat="server" ID="lblAtendidaPor" CssClass="labelBoldForm">Atendida Por:</asp:Label></td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlAtendidaPor" CssClass="comboBoxBlueForm"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblComentarios" CssClass="labelBoldForm">Comentarios:</asp:Label></td>
                    <td colspan="3">
                        <asp:TextBox runat="server" ID="txbComentarios" CssClass="textBoxBlueForm" TextMode="MultiLine"></asp:TextBox></td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblFechaVisita" CssClass="labelBoldForm">Fecha Visita:</asp:Label></td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <asp:TextBox runat="server" ID="txbDiaVisita" CssClass="textBoxBlueForm num3" MaxLength="2" onkeypress='return esDigito(event)'></asp:TextBox>
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlMesVisita" CssClass="comboBoxBlueForm"></asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txbAñoVisita" CssClass="textBoxBlueForm num3" MaxLength="4" onkeypress='return esDigito(event)'></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td></td>
                    <td>
                        <asp:Label runat="server" ID="lblFechaReunion" CssClass="labelBoldForm">Fecha Visita:</asp:Label></td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <asp:TextBox runat="server" ID="txbDiaReunion" CssClass="textBoxBlueForm num3" MaxLength="2" onkeypress='return esDigito(event)'></asp:TextBox>
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlMesReunion" CssClass="comboBoxBlueForm"></asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txbAñoReunion" CssClass="textBoxBlueForm num3" MaxLength="4" onkeypress='return esDigito(event)'></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                 <tr>
                    <td colspan="5">
                        <div style="height:20px"></div>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                    <td style="width: 60px"></td>
                    <td>
                        <asp:Button runat="server" ID="btnGuardar" Text = "Guardar" CssClass="butonForm"/>
                    </td>
                    <td></td>
                </tr>
            </table>
        </asp:Panel>
        <div style="width: 20px"></div>
        <asp:Panel runat="server" ID="pnlHistorial">
            <table class="tableContInfo  tblCenter">
                <tr>
                    <th>Historial de Solicitudes</th>
                </tr>
                <tr>
                    <td>
                        <table class="tableContInfo">
                            <tr>
                                <th>Fecha de Solicitud</th>
                                <th>Estado</th>
                                <th>Fecha de Estado</th>
                                <th>Fecha Visita</th>
                                <th>Usuario</th>
                                <th>Acción</th>
                            </tr>
                            <tr>
                                <td>10 ene 2019</td>
                                <td>Rechazada</td>
                                <td>11 ene 2019</td>
                                <td>11 ene 2019</td>
                                <td>CarmenG</td>
                                <td>
                                    <input value="Seleccionar" style="width: auto; text-align: center" class="butonFormTable" /></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
</asp:Content>
