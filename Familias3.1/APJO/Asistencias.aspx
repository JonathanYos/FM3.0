﻿<%@ Page Language="C#" MasterPageFile="~/mast.Master" AutoEventWireup="true" CodeBehind="Asistencias.aspx.cs" Inherits="Familias3._1.APJO.Asistencias" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ MasterType VirtualPath="~/mast.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblPrograma" runat="server" Text="" class="labelBoldForm">Programa:</asp:Label>&nbsp;
                </td>
                <td style="text-align: center" colspan="2">
                    <asp:DropDownList ID="ddlPrograma" runat="server" class="comboBoxBlueForm" AutoPostBack="true" OnSelectedIndexChanged="ddlPrograma_SelectedIndexChanged"></asp:DropDownList>
                </td>
                <td></td>
                <td>&nbsp;&nbsp;<asp:Label ID="lblSubPrograma" runat="server" Text="" class="labelBoldForm">Subprograma:</asp:Label>&nbsp;
                </td>
                <td style="text-align: center">
                    <asp:DropDownList ID="ddlSubPrograma" runat="server" class="comboBoxBlueForm" AutoPostBack="true" OnSelectedIndexChanged="ddlSubPrograma_SelectedIndexChanged"></asp:DropDownList>
                </td>
                <td></td>
                <td>&nbsp;&nbsp;<asp:Label ID="lblActividad" runat="server" Text="" class="labelBoldForm">Actividad:</asp:Label>&nbsp;
                </td>
                <td style="text-align: center" colspan="6">
                    <asp:DropDownList ID="ddlActividad" runat="server" class="comboBoxBlueForm" AutoPostBack="true" OnSelectedIndexChanged="ddlActividad_SelectedIndexChanged"></asp:DropDownList>
                </td>
                <td colspan="3">
                    &nbsp;&nbsp;
                    <asp:CheckBox runat="server" ID="chkFiltroActividades" AutoPostBack="true" OnCheckedChanged="chkFiltroActividades_CheckedChanged"/>
                    <asp:Label runat="server" ID="lblMostrarTodasActividades" class="labelBoldForm" Text="Todas las Actividades"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblFecha" runat="server" Text="" class="labelBoldForm">Fecha:</asp:Label>&nbsp;
                </td>
                <td style="text-align: center">
                    <asp:TextBox ID="txbFecha" runat="server" AutoCompleteType="Disabled" class="textBoxBlueForm date noPaste" contentEditable="false" AutoPostBack="true" OnTextChanged="txbFecha_TextChanged"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="txbFecha_CalendarExtender" runat="server" BehaviorID="txbFecha_CalendarExtender" Format="dd/MM/yyyy" TargetControlID="txbFecha"></ajaxToolkit:CalendarExtender>
                    <asp:RequiredFieldValidator ID="revTxbFecha" runat="server" ControlToValidate="txbFecha" ErrorMessage="RequiredFieldValidator" ValidationGroup="grpGuardar" Display="None"></asp:RequiredFieldValidator>
                    <ajaxToolkit:ValidatorCalloutExtender ID="revTxbFecha_ValidatorCalloutExtender" runat="server" BehaviorID="revTxbFecha_ValidatorCalloutExtender" TargetControlID="revTxbFecha">
                    </ajaxToolkit:ValidatorCalloutExtender>
                </td>
                <td>
                    <asp:Button runat="server" ID="btnCambiarFecha" CssClass="butonFormSec" Text="Fecha y Hora Manual" OnClick="btnCambiarFecha_Click" />
                </td>
                <td></td>
                <td>&nbsp;&nbsp;<asp:Label ID="lblComentarios" runat="server" Text="Comentarios:" CssClass="labelBoldForm"></asp:Label>
                </td>
                <td style="text-align: center">
                    <asp:TextBox ID="txbComentarios" runat="server" Text="" CssClass="textBoxBlueForm"></asp:TextBox>
                </td>
                <td></td>
                <td>&nbsp;&nbsp;<asp:Label ID="lblImpresiones" runat="server" Text="Impresiones:" CssClass="labelBoldForm"></asp:Label>
                </td>
                <td style="text-align: center">
                    <asp:TextBox ID="txbImpresiones" runat="server" Text="" CssClass="textBoxBlueForm num3" onkeypress='return esDigito(event)' onkeyup='this.value=retornaSoloDigitos(this.value)'></asp:TextBox>
                </td>
                <td></td>
                <td>&nbsp;&nbsp;<asp:Label ID="lblMiembro" runat="server" Text="" class="labelBoldForm">Miembro:</asp:Label>&nbsp;
                </td>
                <td style="text-align: center">
                    <asp:TextBox ID="txbMiembro" runat="server" class="textBoxBlueForm num2" onkeypress='return esDigito(event)' onkeyup='this.value=retornaSoloDigitos(this.value)'></asp:TextBox>
                </td>
                <td></td>
                <td>&nbsp;&nbsp;<asp:Label ID="lblFamilia" runat="server" Text="" class="labelBoldForm">Familia:</asp:Label>&nbsp;
                </td>
                <td style="text-align: center">
                    <asp:TextBox ID="txbFamilia" runat="server" class="textBoxBlueForm num2" onkeypress='return esDigito(event)' onkeyup='this.value=retornaSoloDigitos(this.value)'></asp:TextBox>
                </td>
                <td></td>
                <td>
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="butonForm" OnClick="btnGuardar_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblHora" runat="server" Text="" Visible="false" CssClass="labelBoldForm">Hora Entrada:</asp:Label>&nbsp;
                </td>
                <td style="text-align: center">
                    <asp:TextBox runat="server" ID="txbHora" TextMode="Time" Visible="false" CssClass="textBoxBlueForm date noPaste" contentEditable="false"></asp:TextBox>
                </td>
                <td colspan="15px"></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblHoraSalida" runat="server" Text="" Visible="false" CssClass="labelBoldForm">Hora Salida:</asp:Label>&nbsp;
                </td>
                <td style="text-align: center">
                    <asp:TextBox runat="server" ID="txbHoraSalida" TextMode="Time" Visible="false" CssClass="textBoxBlueForm date noPaste" contentEditable="false"></asp:TextBox>
                </td>

                <td colspan="15px"></td>
            </tr>
        </table>
        <asp:Panel runat="server" ID="pnlAsistencias" Visible="false">
            <table class="tableContInfo tblCenter gray">
                <tr>
                    <td>
                        <asp:GridView ID="gdvAsistencias" CssClass="tableCont" runat="server" AutoGenerateColumns="False" OnRowCommand="gdvAsistencias_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="MemberId" />
                                <asp:BoundField DataField="AssistanceDateTime" />
                                <asp:BoundField DataField="EndDateTime" />
                                <asp:BoundField DataField="Type" />
                                <asp:BoundField DataField="Miembro" HeaderText="Miembro" />
                                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                <asp:BoundField DataField="Hora" HeaderText="Hora Entrada" />
                                <asp:BoundField DataField="HoraSalida" HeaderText="Hora Salida" HtmlEncode="false" />
                                <asp:BoundField DataField="NumeroImpresiones" HeaderText="Impresiones" />
                                <asp:BoundField DataField="Notas" HeaderText="Comentario" HtmlEncode="false" />
                                <asp:BoundField DataField="Edad" HeaderText="Edad" />
                                <%--<asp:BoundField DataField="TipoMiembro" HeaderText="Tipo de Miembro" />
                                <asp:BoundField DataField="Educacion" HeaderText="Info. Educativa" />
                                <asp:TemplateField HeaderText="Semáforo">
                                    <ItemTemplate>
                                        <asp:Panel ID="pnlSemaforo" runat="server" CssClass="pnlVSemaforo" BackColor='<%# (Eval("Semaforo").ToString()=="Rojo") ? colorMalo :  (Eval("Semaforo").ToString()=="Amarillo") ? colorRegular : colorBueno%>'></asp:Panel>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="TS" HeaderText="Trabajador Social" />--%>
                                <asp:BoundField DataField="Familia" HeaderText="Familia" />
                                <asp:TemplateField HeaderText="Acciones">
                                    <ItemTemplate>
                                        <asp:Button ID="btnActualizar" runat="server"
                                            CommandName="cmdActualizar"
                                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                            Text='<%#dic.actualizar%>'
                                            CssClass="butonFormTable" />
                                        <asp:Button ID="btnEliminar" runat="server"
                                            CommandName="cmdEliminar"
                                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                            Text='<%#dic.eliminar%>'
                                            CssClass="butonFormSecTable" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <script>
            $(document).ready(function () {
                $('.secTbl Table').eq(1).css({
                    "width": "1000px"
                });
                $('.pnlVSemaforo').css({
                    "margin": "auto auto",
                    "border": "1px solid gray",
                    "border-bottom-left-radius": "10px",
                    "border-bottom-right-radius": "10px",
                    "border-top-left-radius": "10px",
                    "border-top-right-radius": "10px",
                    "width": "50px",
                    "height": "10px"
                });
            });
        </script>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <asp:Panel runat="server" ID="pnlActualizar" CssClass="formContTransModal" Visible="false">
        <table class="tblCenter">
            <tr>
                <td>
                    <asp:Label ID="lblNombre" runat="server" Text="" class="labelBoldForm">Nombre:</asp:Label>&nbsp;
                </td>
                <td style="text-align: center">
                    <asp:Label ID="lblVNombre" runat="server" class="labelForm">Domingo Eliseo</asp:Label>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblActComentarios" runat="server" Text="Comentarios:" CssClass="labelBoldForm"></asp:Label>
                </td>
                <td style="text-align: center" colspan="4">
                    <asp:TextBox ID="txbActComentarios" runat="server" Text="" CssClass="textBoxBlueForm"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblActImpresiones" runat="server" Text="Impresiones:" CssClass="labelBoldForm"></asp:Label>
                </td>
                <td style="text-align: center" colspan="4">
                    <asp:TextBox ID="txbActImpresiones" runat="server" Text="" CssClass="textBoxBlueForm num3" onkeypress='return esDigito(event)' onkeyup='this.value=retornaSoloDigitos(this.value)'></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblActHoraSalida" runat="server" Text="" Visible="false" CssClass="labelBoldForm">Hora Salida:</asp:Label>&nbsp;
                </td>
                <td style="text-align: center">
                    <asp:TextBox runat="server" ID="txbActHoraSalida" TextMode="Time" Visible="false" CssClass="textBoxBlueForm date noPaste" contentEditable="false"></asp:TextBox>
                    <asp:CheckBox ID="chkSalida" runat="server" />
                    <asp:Label ID="lblSalida" runat="server" Text="Salida" CssClass="labelBoldForm"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="btnActualizar" runat="server" Text="Actualizar" CssClass="butonForm" OnClick="btnActualizar_Click" />
                    <asp:Button ID="btnCancelarAct" runat="server" Text="Cancelar" CssClass="butonFormSec" OnClick="btnCancelarAct_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlMiembros" class="formContTransModal" Visible="false">
        <table class="tblCenter">
            <tr>
                <td>
                    <asp:Button ID="btnCancelarMiembro" runat="server" Text="Cancelar" CssClass="butonFormSec" OnClick="btnCancelarMiembro_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <table class="tableCont tableContInfo mediumTbl tblCenter blue">
                        <tr>
                            <th class="tblhead">
                                <asp:Label ID="lblMiembros" runat="server" Text="Miembros"></asp:Label>
                            </th>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="gdvMiembros" Width="100%" CssClass="tableCont bigTbl tblCenter blue" runat="server" AutoGenerateColumns="false" OnRowCommand="gdvMiembros_RowCommand">
                                    <Columns>
                                        <asp:BoundField DataField="MemberId" />
                                        <asp:BoundField DataField="MemberId" HeaderText="Miembro" />
                                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                        <asp:BoundField DataField="Relacion" HeaderText="Relación" />
                                        <asp:BoundField DataField="AfilStatus" HeaderText="Estado de Afiliación" />
                                        <asp:BoundField DataField="TipoAfil" HeaderText="Tipo de Afiliación" />
                                        <asp:TemplateField HeaderText="Acción">
                                            <ItemTemplate>
                                                <asp:Button ID="btnRegistrar" runat="server"
                                                    CommandName="cmdRegistrar"
                                                    CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                    Text="Registrar"
                                                    CssClass="butonFormTable" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
