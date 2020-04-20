<%@ Page Language="C#" Async="true" AutoEventWireup="true" MasterPageFile="~/mast.Master" CodeBehind="AsignacionTS.aspx.cs" Inherits="Familias3._1.TS.AsignarTS" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ MasterType VirtualPath="~/mast.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="formContGlobal">
        <div>
            <table class="tblCenter">
                <tr>
                    <td>
                        <asp:Label ID="lblTSU" runat="server" Text="" class="labelBoldForm"></asp:Label>&nbsp;
                    </td>
                    <td style="text-align: center">
                        <asp:Label ID="lblVTS" runat="server" class="labelForm"></asp:Label>
                    </td>
                    <td></td>
                    <td>&nbsp;&nbsp;<asp:Label ID="lblClasif" runat="server" Text="" class="labelBoldForm">Clasificaciòn:</asp:Label>&nbsp;
                    </td>
                    <td style="text-align: center">
                        <asp:Label ID="lblVClasif" runat="server" class="labelForm"></asp:Label>
                    </td>
                    <td></td>
                    <td>&nbsp;&nbsp;<asp:Label ID="lblTelef" runat="server" Text="" class="labelBoldForm"></asp:Label>&nbsp;
                    </td>
                    <td style="text-align: center">
                        <asp:Label ID="lblVTelef" runat="server" class="labelForm"></asp:Label>
                    </td>
                    <td></td>
                    <td>&nbsp;&nbsp;<asp:Label ID="lblDirec" runat="server" Text="" class="labelBoldForm"></asp:Label>&nbsp;
                    </td>
                    <td style="text-align: center">
                        <asp:Label ID="lblVDirec" runat="server" class="labelForm"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>

        <asp:Panel runat="server" CssClass="formContTrans">
            <table class="tblCenter">
                <tr>
                    <td>
                        <asp:Panel runat="server" ID="pnlIngresarAsignacion">
                            <table class="tblCenter">
                                <tr>
                                    <td class="left">
                                        <asp:Label runat="server" ID="lblTS" CssClass="labelBoldForm">Trabajador Social:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="ddlTS" CssClass="comboBoxBlueForm"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="revDdlTS" runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="ddlTS" ValidationGroup="grpGuardar" Display="None"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="revDdlTS_ValidatorCalloutExtender" runat="server" BehaviorID="revDdlTS_ValidatorCalloutExtender" TargetControlID="revDdlTS">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="left">
                                        <asp:Label runat="server" ID="lblFechaInicio" CssClass="labelBoldForm">Fecha Inicio:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txbFechaInicio" CssClass="textBoxBlueForm num"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="txbFechaInicio_CalendarExtender" runat="server" BehaviorID="txbFechaInicio_CalendarExtender" Format="dd/MM/yyyy" TargetControlID="txbFechaInicio"></ajaxToolkit:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Button runat="server" ID="btnGuardar" CssClass="butonForm" Text="Guardar" ValidationGroup="grpGuardar" OnClick="btnGuardar_Click" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="pnlActualizarAsignacion" Visible="False">
                            <table class="tblCenter">
                                <tr>
                                    <td class="left">
                                        <asp:Label runat="server" ID="lblTSAct" CssClass="labelBoldForm">Trabajador Social:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblVTSAct" CssClass="labelForm">Trabajador Social:</asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="left">
                                        <asp:Label runat="server" ID="lblFechaInicioAct" CssClass="labelBoldForm">Fecha Inicio:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblVFechaInicioAct" CssClass="labelForm"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="left">
                                        <asp:Label runat="server" ID="lblFechaFin" CssClass="labelBoldForm">Fecha Fin:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txbFechaFin" CssClass="textBoxBlueForm num"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="txbFechaFin_CalendarExtender" runat="server" BehaviorID="txbFechaFin_CalendarExtender" Format="dd/MM/yyyy" TargetControlID="txbFechaFin"></ajaxToolkit:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Button runat="server" ID="btnGuardarAct" CssClass="butonForm" Text="Guardar" OnClick="btnGuardarAct_Click" />
                                        <asp:Button runat="server" ID="btnAsignarNuevoTS" CssClass="butonForm" Text="Asignar Nuevo Trabajador Social" OnClick="btnAsignarNuevoTS_Click" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div style="height: 25px"></div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel runat="server" ID="pnlAsignacionesTS" CssClass="scroll-Y">
                            <table class="tableContInfo tblCenter gray">
                                <tr>
                                    <th class="center">
                                        <asp:Label runat="server" ID="lblAsignacionesTS">Historial de Asignaciones</asp:Label>
                                    </th>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:GridView ID="gdvAsignacionesTS" CssClass="tableContInfo" runat="server" AutoGenerateColumns="False" OnRowCommand="gdvAsignacionesTS_RowCommand">
                                            <Columns>
                                                <asp:BoundField DataField="CreationDateTime" />
                                                <asp:BoundField DataField="EmployeeId" />
                                                <asp:BoundField DataField="StartDate" />
                                                <asp:BoundField DataField="EndDate" />
                                                <asp:BoundField DataField="EmployeeIdU" />
                                                <asp:BoundField DataField="StartDateU" />
                                                <asp:BoundField DataField="EndDateU" />
                                                <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnActualizar" runat="server"
                                                            CommandName="cmdActualizar"
                                                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                            Text='<%#dic.actualizar%>' />
                                                        <asp:Button ID="btnEliminar" runat="server"
                                                            CommandName="cmdEliminar"
                                                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                            Text='<%#dic.eliminar%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:Label runat="server" ID="lblNoTieneATS" Visible="false" CssClass="labelFormSec"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
</asp:Content>
