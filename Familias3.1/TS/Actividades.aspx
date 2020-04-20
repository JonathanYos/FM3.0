<%@ Page Language="C#" Async="true" AutoEventWireup="true" MasterPageFile="~/mast.Master" CodeBehind="Actividades.aspx.cs" Inherits="Familias3._1.TS.Actividades" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ MasterType VirtualPath="~/mast.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="formContGlobal">
        <table class="tblCenter">
            <tr>
                <td><asp:Label ID="lblTSU" runat="server" Text="" class="labelBoldForm"></asp:Label>&nbsp;
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
        <asp:Panel runat="server" CssClass="formContTrans">
            <table class="tblCenter">
                <tr>
                    <td>
                        <asp:Panel runat="server" ID="pnlIngresarActividad">
                            <table class="tblCenter">
                                <tr>
                                    <td class="left">
                                        <asp:Label runat="server" ID="lblActividad" CssClass="labelBoldForm">Actividad:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="ddlActividad" CssClass="comboBoxBlueForm"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="revDdlActividad" runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="ddlActividad" ValidationGroup="grpGuardar" Display="None"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="revDdlActividad_ValidatorCalloutExtender" runat="server" BehaviorID="revDdlActividad_ValidatorCalloutExtender" TargetControlID="revDdlActividad">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="left">
                                        <asp:Label runat="server" ID="lblFechaActividad"  Visible ="false" CssClass="labelBoldForm">Fecha Aviso:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txbFechaActividad"  Visible ="false" CssClass="textBoxBlueForm"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="txbFechaActividad_CalendarExtender" runat="server" BehaviorID="txbFechaActividad_CalendarExtender" Format="dd/MM/yyyy" TargetControlID="txbFechaActividad"></ajaxToolkit:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="left">
                                        <asp:Label runat="server" ID="lblNotas" Text="Notas" CssClass="labelBoldForm"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txbNotas" AutoCompleteType="Disabled" runat="server" CssClass="textBoxBlueForm"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Button runat="server" ID="btnGuardar" CssClass="butonForm" Text="Ingresar Actividad" ValidationGroup="grpGuardar" OnClick="btnGuardar_Click" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="pnlActualizarActividad" Visible="false">
                            <table class="tblCenter">
                                <tr>
                                    <td class="left">
                                        <asp:Label runat="server" ID="lblActividadAct" CssClass="labelBoldForm">Aviso:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblVActividadAct" CssClass="labelForm"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="left">
                                        <asp:Label runat="server" ID="lblFechaActividadAct" Visible ="false" CssClass="labelBoldForm">Fecha Aviso:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblVFechaActividadAct"  Visible ="false" CssClass="labelForm">Fecha Aviso:</asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="left">
                                        <asp:Label runat="server" ID="lblActNotas" Text="Notas" CssClass="labelBoldForm"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" AutoCompleteType="Disabled" ID="txbActNotas" CssClass="textBoxBlueForm"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Button runat="server" ID="btnGuardarAct" CssClass="butonForm" Text="Actualizar Aviso" OnClick="btnGuardarAct_Click" />
                                        <asp:Button runat="server" ID="btnNuevaActividad" CssClass="butonFormRet" Text="Nuevo Aviso" OnClick="btnNuevaActividad_Click" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div style = "height:25px"></div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel runat="server" ID="pnlActividadesFamliares" CssClass="scroll-Y">
                            <table class="tableContInfo tblCenter gray">
                                <tr>
                                    <th class="center">
                                        <asp:Label runat="server" ID="lblActividadesFamiliares">Actividades Familiares</asp:Label>
                                    </th>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:GridView ID="gdvActividadesFamiliares" runat="server" AutoGenerateColumns="False" OnRowCommand="gdvActividadesFamiliares_RowCommand">
                                            <Columns>
                                                <asp:BoundField DataField="Type" />
                                                <asp:BoundField DataField="ActivityDateTime" />
                                                <asp:BoundField DataField="CreationDateTime" />
                                                <asp:BoundField DataField="Des" />
                                                <asp:BoundField DataField="ActivityDateU" />
                                                <asp:BoundField DataField="UserId" />
                                                <asp:BoundField DataField="Notes" HtmlEncode ="False" />
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnActualizar" runat="server"
                                                            CommandName="cmdActualizar"
                                                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                            Text='<%#dic.actualizar%>' 
                                                            CssClass="butonFormTable"/>
                                                        <asp:Button ID="btnEliminar" runat="server"
                                                            CommandName="cmdEliminar"
                                                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                            Text='<%#dic.eliminar%>' 
                                                            CssClass="butonFormSecTable"/>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:Label runat="server" ID="lblNoTieneAct" Visible="false" CssClass="labelFormSec"></asp:Label>
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
