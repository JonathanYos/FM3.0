<%@ Page Language="C#" Async="true" AutoEventWireup="true" MasterPageFile="~/mast.Master" CodeBehind="AgregarSolicitudDesafiliacion.aspx.cs" Inherits="Familias3._1.AFIL.AgregarSolicitudDesafiliacion" %>

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
                        <asp:Label runat="server" ID="lblTipoProceso" CssClass="labelBoldForm">*Tipo de Solicitud:</asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlTipoProceso" CssClass="comboBoxBlueForm"></asp:DropDownList>
                    </td>
                    <td></td>
                    <td>
                        <asp:Label runat="server" ID="lblEstado" CssClass="labelBoldForm">*Estado de Solicitud:</asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlEstado" CssClass="comboBoxBlueForm"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblPlanGrad" CssClass="labelBoldForm">*Plan de Graduación:</asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlPlanGrad" CssClass="comboBoxBlueForm"></asp:DropDownList>
                    </td>
                    <td></td>
                    <td>
                        <asp:Label runat="server" ID="lblRazon" CssClass="labelBoldForm">*Razón:</asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlRazon" CssClass="comboBoxBlueForm"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblNota" CssClass="labelBoldForm">Nota:</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txbNota" CssClass="textBoxBlueForm"></asp:TextBox>
                    </td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td>
                        <asp:Button runat="server" ID="btnGuardar" Text="Guardar" CssClass="butonForm" />
                    </td>
                    <td></td>
                </tr>
                <%-- <table style="display: flex">
                            <tr>
                                <td>
                                    <asp:CheckBox ID="chkAfilOtroProy" runat="server" Checked="true" />
                                </td>
                                <td>
                                    <asp:Label ID="lblAfilOtroProy" runat="server" class="labelBoldForm" Text="Afiliado con Otro Proyecto"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="chkEstablecimientoNo" runat="server" />
                                </td>
                                <td>
                                    <asp:Label ID="lblEstablecimientoNo" runat="server" class="labelBoldForm" Text="Establecimiento no Favorito"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="chkFallecio" runat="server" />
                                </td>
                                <td>
                                    <asp:Label ID="lblFallecio" runat="server" class="labelBoldForm" Text="Falleció"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="Fuera de Área" runat="server" />
                                </td>
                                <td>
                                    <asp:Label ID="Label1" runat="server" class="labelBoldForm" Text="Falleció"></asp:Label>
                                </td>
                            </tr>
                        </table>--%>
            </table>
        </asp:Panel>
        <div style="width: 10px"></div>
        <asp:Panel runat="server" ID="pnlHistorial">
            <table class="tableContInfo  tblCenter">
                <tr>
                    <th>Historial de Solicitudes</th>
                </tr>
                <tr>
                    <td>
                        <table class="tableContInfo">
                            <tr>
                                <th>Tipo de Proceso</th>
                                <th>Fecha de Solicitud</th>
                                <th>Estado</th>
                                <th>Fecha de Estado</th>
                                <th>Plan de Graduación</th>
                                <th>Razón</th>
                                <th>Nota</th>
                                <th>Usuario</th>
                                <th>Acción</th>
                            </tr>
                            <tr>
                                <td>Desafiliación</td>
                                <td>10 ene 2019</td>
                                <td>Rechazada</td>
                                <td>11 ene 2019</td>
                                <td>Fase II</td>
                                <td>Repitencia</td>
                                <td>Nota Libre</td>
                                <td>CarmenG</td>
                                <td>
                                    <input value="Seleccionar" style="width: auto; text-align: center" class="butonFormTable" /></td>
                            </tr>
                             <tr>
                                <td>Desafiliación</td>
                                <td>10 ene 2019</td>
                                <td>Rechazada</td>
                                <td>11 ene 2019</td>
                                <td>Fase II</td>
                                <td>Repitencia</td>
                                <td>Nota Libre</td>
                                <td>CarmenG</td>
                                <td>
                                    <input value="Seleccionar" style="width: auto; text-align: center" class="butonFormTable" /></td>
                            </tr>
                             <tr>
                                <td>Desafiliación</td>
                                <td>10 ene 2019</td>
                                <td>Rechazada</td>
                                <td>11 ene 2019</td>
                                <td>Fase II</td>
                                <td>Repitencia</td>
                                <td>Nota Libre</td>
                                <td>CarmenG</td>
                                <td>
                                    <input value="Seleccionar" style="width: auto; text-align: center" class="butonFormTable" /></td>
                            </tr>
                             <tr>
                                <td>Desafiliación</td>
                                <td>10 ene 2019</td>
                                <td>Rechazada</td>
                                <td>11 ene 2019</td>
                                <td>Fase II</td>
                                <td>Repitencia</td>
                                <td>Nota Libre</td>
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
