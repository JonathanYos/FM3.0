<%@ Page Language="C#" Async="true" AutoEventWireup="true" MasterPageFile="~/mast.Master" CodeBehind="Avisos.aspx.cs" Inherits="Familias3._1.TS.RegistrarAvisos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ MasterType VirtualPath="~/mast.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="formContGlobal">
        <div>
            <table class="tblCenter">
                <tr>
                    <td>
                        <asp:Label ID="lblTS" runat="server" Text="" class="labelBoldForm"></asp:Label>&nbsp;
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
        <div class="contenedor">
            <div style="text-align: center">
                <asp:LinkButton ID="lnkOpciones" CssClass="cabecera c-activa" runat="server" OnClick="lnkOpciones_Click">Predeterminados</asp:LinkButton>
                <asp:LinkButton ID="lnkNotaLibre" CssClass="cabecera" runat="server" OnClick="lnkNotaLibre_Click">Nota Libre</asp:LinkButton>
            </div>
            <asp:Panel runat="server" ID="pnlOpciones" CssClass="pestana p-activa">
                <asp:Panel runat="server" CssClass="formContTrans">
                    <div>
                        <table class="tblCenter">
                            <tr>
                                <td>
                                    <asp:Panel runat="server" ID="pnlIngresarAviso">
                                        <table class="tblCenter">
                                            <tr>
                                                <td class="left">
                                                    <asp:Label runat="server" ID="lblAviso" CssClass="labelBoldForm">Aviso:</asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList runat="server" ID="ddlAviso" CssClass="comboBoxBlueForm"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="revDdlAviso" runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="ddlAviso" ValidationGroup="grpGuardar" Display="None"></asp:RequiredFieldValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="revDdlAviso_ValidatorCalloutExtender" runat="server" BehaviorID="revDdlAviso_ValidatorCalloutExtender" TargetControlID="revDdlAviso">
                                                    </ajaxToolkit:ValidatorCalloutExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="left">
                                                    <asp:Label runat="server" ID="lblFechaAviso" Visible="false" CssClass="labelBoldForm">Fecha Aviso:</asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txbFechaAviso" AutoCompleteType="Disabled" Visible="false" CssClass="textBoxBlueForm"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="txbFechaAviso_CalendarExtender" runat="server" BehaviorID="txbFechaAviso_CalendarExtender" Format="dd/MM/yyyy" TargetControlID="txbFechaAviso"></ajaxToolkit:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td>
                                                    <asp:Button runat="server" ID="btnGuardar" CssClass="butonForm" Text="Ingresar Aviso" ValidationGroup="grpGuardar" OnClick="btnGuardar_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <asp:Panel runat="server" ID="pnlActualizarAviso" Visible="false">
                                        <table class="tblCenter">
                                            <tr>
                                                <td class="left">
                                                    <asp:Label runat="server" ID="lblAvisoAct" CssClass="labelBoldForm">Aviso:</asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblVAvisoAct" CssClass="labelForm"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="left">
                                                    <asp:Label runat="server" ID="lblFechaAvisoAct" Visible="false" CssClass="labelBoldForm">Fecha Aviso:</asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblVFechaAvisoAct" Visible="false" CssClass="labelForm">Fecha Aviso:</asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td>
                                                    <asp:CheckBox runat="server" ID="chkInactivo" Checked="false" />
                                                    <asp:Label runat="server" ID="lblInactivo" Text="Inactivo" CssClass="labelBoldForm"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td>
                                                    <asp:Button runat="server" ID="btnGuardarAct" CssClass="butonForm" Text="Actualizar Aviso" OnClick="btnGuardarAct_Click" />
                                                    <asp:Button runat="server" ID="btnNuevoAviso" CssClass="butonFormRet" Text="Nuevo Aviso" OnClick="btnNuevoAviso_Click" />
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
                                    <asp:Panel runat="server" ID="pnlAvisosFamliares">
                                        <table class="tableContInfo tblCenter gray">
                                            <tr>
                                                <th class="center">
                                                    <asp:Label runat="server" ID="lblAvisosFamiliares">Avisos Familiares</asp:Label>
                                                </th>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:GridView ID="gdvAvisosFamiliares" CssClass="tableCont" runat="server" AutoGenerateColumns="False" OnRowCommand="gdvAvisosFamiliares_RowCommand">
                                                        <Columns>
                                                            <asp:BoundField DataField="Warning" HeaderText='<>' />
                                                            <asp:BoundField DataField="WarningDate" />
                                                            <asp:BoundField DataField="FunctionalArea" />
                                                            <asp:BoundField DataField="CreationDateTime" />
                                                            <asp:BoundField DataField="Inactive" />
                                                            <asp:BoundField DataField="Des" />
                                                            <asp:BoundField DataField="WarningDateU" />
                                                            <asp:BoundField DataField="UserId" />
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblInactive" runat="server" Text='<%# (Eval("Inactive").ToString()=="True") ? dic.inactivo : dic.activo%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
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
                                                    <asp:Label runat="server" ID="lblNoTieneAF" Visible="false" CssClass="labelFormSec"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
            </asp:Panel>
            <asp:Panel runat="server" ID="pnlNotaLibre" Visible="false" CssClass="pestana">
                <asp:Panel runat="server" CssClass="formContTrans">
                    <div>
                        <table class="tblCenter">
                            <tr>
                                <td class="left">
                                    <asp:Label runat="server" ID="lblNota" CssClass="labelBoldForm">Nota:</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txbNota" Width="400px" MaxLenght="100" CssClass="textBoxBlueForm"></asp:TextBox>
                                    <asp:Label runat="server" ID="lblVNota" Visible="false" CssClass="labelForm"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Button runat="server" ID="btnGuardarNota" CssClass="butonForm" Text="Guardar" OnClick="btnGuardarNota_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
