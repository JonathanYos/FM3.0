<%@ Page Language="C#" Async="true" AutoEventWireup="true" MasterPageFile="~/mast.Master" CodeBehind="AnoEscolar.aspx.cs" Inherits="Familias3._1.TS.AñoEscolar" %>

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
        <asp:Panel runat="server" CssClass="formContTrans">
            <asp:Panel ID="pnlMiembros" runat="server">
                <table class="tableContInfo tblCenter">
                    <tr>
                        <td class="center">
                            <asp:Label ID="lblMiembros" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gdvMiembros" CssClass="tableContInfo tblCenter" AutoGenerateColumns="false" runat="server" OnRowCommand="gdvMiembros_RowCommand">
                                <Columns>
                                    <asp:BoundField DataField="Miembro" />
                                    <asp:BoundField DataField="Nombre" />
                                    <asp:BoundField DataField="Edad" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="btnMName" runat="server"
                                                CommandName="cmdMName"
                                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                Text='<%# dic.seleccionar %>'
                                                CssClass="butonFormTable" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Age" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlRegistroAñoEscolar" runat="server" Visible="false">
                <table class="tblCenter">
                    <tr>
                        <td style="text-align: center">
                            <asp:Button runat="server" ID="btnRegresar" CssClass="butonFormRet" Text="Regresar" OnClick="btnRegresar_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel runat="server" ID="pnlIngresarAñoEscolar">
                                <table class="tblCenter">
                                    <tr>
                                        <td class="left">
                                            <asp:Label runat="server" ID="lblMiembro" CssClass="labelBoldForm">Miembro:</asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblVMiembro" CssClass="labelForm"></asp:Label>
                                            <%--<asp:DropDownList runat="server" ID="ddlMiembro" CssClass="comboBoxBlueForm"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="revDdlMiembro" runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="ddlMiembro" ValidationGroup="grpGuardar" Display="None"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="revDdlMiembro_ValidatorCalloutExtender" runat="server" BehaviorID="revDdlMiembro_ValidatorCalloutExtender" TargetControlID="revDdlMiembro">
                                        </ajaxToolkit:ValidatorCalloutExtender>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="left">
                                            <asp:Label runat="server" ID="lblAño" CssClass="labelBoldForm">Año:</asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txbAño" AutoCompleteType="Disabled" CssClass="textBoxBlueForm num3" onkeypress='return esDigito(event)' onkeyup='this.value=retornaSoloDigitos(this.value)'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="revTxbAño" runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="txbAño" ValidationGroup="grpGuardar" Display="None"></asp:RequiredFieldValidator>
                                            <ajaxToolkit:ValidatorCalloutExtender ID="revTxbAño_ValidatorCalloutExtender" runat="server" BehaviorID="revTxbAño_ValidatorCalloutExtender" TargetControlID="revTxbAño">
                                            </ajaxToolkit:ValidatorCalloutExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="left">
                                            <asp:Label runat="server" ID="lblEstadoEduc" CssClass="labelBoldForm">Estado Educativo:</asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="ddlEstadoEduc" CssClass="comboBoxBlueForm"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="revDdlEstadoEduc" runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="ddlEstadoEduc" ValidationGroup="grpGuardar" Display="None"></asp:RequiredFieldValidator>
                                            <ajaxToolkit:ValidatorCalloutExtender ID="revDdlEstadoEduc_ValidatorCalloutExtender" runat="server" BehaviorID="revDdlEstadoEduc_ValidatorCalloutExtender" TargetControlID="revDdlEstadoEduc">
                                            </ajaxToolkit:ValidatorCalloutExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="left">
                                            <asp:Label runat="server" ID="lblProximoGrado" CssClass="labelBoldForm">Próximo Grado:</asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="ddlProximoGrado" CssClass="comboBoxBlueForm"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="revDdlProximoGrado" runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="ddlProximoGrado" ValidationGroup="grpGuardar" Display="None"></asp:RequiredFieldValidator>
                                            <ajaxToolkit:ValidatorCalloutExtender ID="revDdlProximoGrado_ValidatorCalloutExtender" runat="server" BehaviorID="revDdlProximoGrado_ValidatorCalloutExtender" TargetControlID="revDdlProximoGrado">
                                            </ajaxToolkit:ValidatorCalloutExtender>
                                        </td>
                                    </tr>
                                    <%--                                <tr>
                                    <td class="left">
                                        <asp:Label runat="server" ID="lblCentroEduc" CssClass="labelForm">Centro Educativo:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="ddlCentroEduc" CssClass="comboBoxBlueForm"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="revDdlCentroEduc" runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="ddlCentroEduc" ValidationGroup="grpGuardar" Display="None"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="revDdlCentroEduc_ValidatorCalloutExtender" runat="server" BehaviorID="revDdlCentroEduc_ValidatorCalloutExtender" TargetControlID="revDdlCentroEduc">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                    </td>
                                </tr>--%>
                                    <tr>
                                        <td class="left">
                                            <asp:Label runat="server" ID="lblCarreraEduc" CssClass="labelBoldForm">Carrera Educativa:</asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="ddlCarreraEduc" CssClass="comboBoxBlueForm"></asp:DropDownList>
                                            <%--                                        <asp:RequiredFieldValidator ID="revDdlCarreraEduc" runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="ddlCarreraEduc" ValidationGroup="grpGuardar" Display="None"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="revDdlCarreraEduc_ValidatorCalloutExtender" runat="server" BehaviorID="revDdlCarreraEduc_ValidatorCalloutExtender" TargetControlID="revDdlCarreraEduc">
                                        </ajaxToolkit:ValidatorCalloutExtender>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="left">
                                            <asp:Label runat="server" ID="lblSeccion" CssClass="labelBoldForm">Seccion:</asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" AutoCompleteType="Disabled" ID="txbSeccion" MaxLenght="1" CssClass="textBoxBlueForm num3"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="left">
                                            <asp:Label runat="server" ID="lblNotas" CssClass="labelBoldForm">Notas:</asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" AutoCompleteType="Disabled" ID="txbNotas" CssClass="textBoxBlueForm"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button runat="server" ID="btnIngresar" ValidationGroup="grpGuardar" CssClass="butonForm" Text="Ingresar" OnClick="btnIngresar_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel runat="server" ID="pnlActualizarAñoEscolar" Visible="false">
                                <table class="tblCenter">
                                    <tr>
                                        <td class="left">
                                            <asp:Label runat="server" ID="lblActMiembro" CssClass="labelBoldForm">Miembro:</asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblVActMiembro" CssClass="labelForm"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="left">
                                            <asp:Label runat="server" ID="lblActAño" CssClass="labelBoldForm">Año:</asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblVActAño" CssClass="labelForm"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="left">
                                            <asp:Label runat="server" ID="lblActEstadoEduc" CssClass="labelBoldForm">Estado Educativo:</asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="ddlActEstadoEduc" CssClass="comboBoxBlueForm"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="revDdlActEstadoEduc" runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="ddlActEstadoEduc" ValidationGroup="grpGuardar" Display="None"></asp:RequiredFieldValidator>
                                            <ajaxToolkit:ValidatorCalloutExtender ID="revDdlActEstadoEduc_ValidatorCalloutExtender" runat="server" BehaviorID="revDdlActEstadoEduc_ValidatorCalloutExtender" TargetControlID="revDdlActEstadoEduc">
                                            </ajaxToolkit:ValidatorCalloutExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="left">
                                            <asp:Label runat="server" ID="lblActProximoGrado" CssClass="labelBoldForm">Próximo Grado:</asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="ddlActProximoGrado" CssClass="comboBoxBlueForm"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="revDdlActProximoGrado" runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="ddlActProximoGrado" ValidationGroup="grpActualizar" Display="None"></asp:RequiredFieldValidator>
                                            <ajaxToolkit:ValidatorCalloutExtender ID="revDdlActProximoGrado_ValidatorCalloutExtender" runat="server" BehaviorID="revDdlActProximoGrado_ValidatorCalloutExtender" TargetControlID="revDdlActProximoGrado">
                                            </ajaxToolkit:ValidatorCalloutExtender>
                                        </td>
                                    </tr>
                                    <%--                                <tr>
                                    <td class="left">
                                        <asp:Label runat="server" ID="lblActCentroEduc" CssClass="labelForm">Centro Educativo:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="ddlActCentroEduc" CssClass="comboBoxBlueForm"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="revDdlActCentroEduc" runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="ddlActCentroEduc" ValidationGroup="grpActualizar" Display="None"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="revDdlActCentroEduc_ValidatorCalloutExtender" runat="server" BehaviorID="revDdlActCentroEduc_ValidatorCalloutExtender" TargetControlID="revDdlActCentroEduc">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                    </td>
                                </tr>--%>
                                    <tr>
                                        <td class="left">
                                            <asp:Label runat="server" ID="lblActCarreraEduc" CssClass="labelBoldForm">Carrera Educativa:</asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="ddlActCarreraEduc" CssClass="comboBoxBlueForm"></asp:DropDownList>
                                            <%--<asp:RequiredFieldValidator ID="revDdlActCarreraEduc" runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="ddlActCarreraEduc" ValidationGroup="grpGuardar" Display="None"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="revDdlActCarreraEduc_ValidatorCalloutExtender" runat="server" BehaviorID="revDdlActCarreraEduc_ValidatorCalloutExtender" TargetControlID="revDdlActCarreraEduc">
                                        </ajaxToolkit:ValidatorCalloutExtender>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="left">
                                            <asp:Label runat="server" ID="lblActSeccion" CssClass="labelBoldForm">Seccion:</asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" AutoCompleteType="Disabled" ID="txbActSeccion" MaxLenght="1" CssClass="textBoxBlueForm num3"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="left">
                                            <asp:Label runat="server" ID="lblActNotas" CssClass="labelBoldForm">Notas:</asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" AutoCompleteType="Disabled" ID="txbActNotas" CssClass="textBoxBlueForm"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button runat="server" ID="btnActualizar" CssClass="butonForm" ValidationGroup="grpActualizar" Text="Ingresar" OnClick="btnActualizar_Click" />
                                            <asp:Button runat="server" ID="btnNuevoAñoEscolar" CssClass="butonFormRet" Text="Nuevo NADFAS" OnClick="btnNuevoAñoEscolar_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div style="height: 15px"></div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel runat="server" ID="pnlAñoEscolar">
                                <table class="tableContInfo tblCenter gray">
                                    <tr>
                                        <th class="center">
                                            <asp:Label runat="server" ID="lblAñosEscolar">Historial de Años Escolares</asp:Label>
                                        </th>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="gdvAñosEscolar" CssClass="tableContInfo" runat="server" AutoGenerateColumns="False" OnRowCommand="gdvAñosEscolar_RowCommand">
                                                <Columns>
                                                    <asp:BoundField DataField="MemberId" />
                                                    <asp:BoundField DataField="CreationDateTime" />
                                                    <asp:BoundField DataField="SchoolYear" />
                                                    <asp:BoundField DataField="Año" />
                                                    <asp:BoundField DataField="Grado" />
                                                    <asp:BoundField DataField="Carrera" />
                                                    <asp:BoundField DataField="Estado" />
                                                    <asp:BoundField DataField="Seccion" HtmlEncode="false" />
                                                    <asp:BoundField DataField="Notas" HtmlEncode="false" />
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
                                            <asp:Label runat="server" ID="lblNoTiene" Visible="false" CssClass="labelFormSec"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:Panel>
    </div>
</asp:Content>
