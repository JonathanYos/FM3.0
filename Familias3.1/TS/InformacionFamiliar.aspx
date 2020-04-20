<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/mast.Master" CodeBehind="InformacionFamiliar.aspx.cs" Inherits="Familias3._1.TS.InformacionFamiliar" %>

<%@ MasterType VirtualPath="~/mast.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
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
                <asp:LinkButton ID="lnkIngresarMiembro" CssClass="cabecera c-activa" runat="server" OnClick="lnkIngresarMiembro_Click">Ingresar Miembro</asp:LinkButton>
                <asp:LinkButton ID="lnkModificarMiembro" CssClass="cabecera" runat="server" OnClick="lnkModificarMiembro_Click">Modificar Miembro</asp:LinkButton>
                <asp:LinkButton ID="lnkModificarFamilia" CssClass="cabecera" runat="server" OnClick="lnkModificarFamilia_Click">Modificar Familia</asp:LinkButton>
                <asp:LinkButton ID="lnkReasignarFamiliaMiembro" CssClass="cabecera" runat="server" OnClick="lnkReasignarFamiliaMiembro_Click">Reasignar Familia Miembro</asp:LinkButton>
                <asp:LinkButton ID="lnkTelefonos" CssClass="cabecera" runat="server" OnClick="lnkTelefonos_Click">Teléfonos</asp:LinkButton>
            </div>
            <div style="height: 20px"></div>
            <asp:Panel runat="server" ID="pnlIngresarMiembro" class="pestana p-activa">
                <div class="">
                    <table class="tblCenter">
                        <tr>
                            <td>
                                <asp:Panel ID="ingMpnlFormIngreso" runat="server">
                                    <table class="tblCenter">
                                        <tr>
                                            <th></th>
                                            <th></th>
                                            <th style="width: 40px"></th>
                                            <th></th>
                                            <th></th>
                                            <th style="width: 40px"></th>
                                            <th></th>
                                            <th></th>
                                        </tr>
                                        <tr>
                                            <td class="left">
                                                <asp:Label runat="server" ID="ingMlblNombres" CssClass="labelBoldForm">Nombres:</asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="ingMtxbNombres" AutoCompleteType="Disabled" CssClass="textBoxBlueForm" onkeypress='return esLetra(event)' onkeyup='this.value=retornaSoloLetras(this.value)'></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="ingMRevTxbNombres" runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="ingMTxbNombres" ValidationGroup="ingMGrpAceptar" Display="None"></asp:RequiredFieldValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="ingMRevTxbNombres_ValidatorCalloutExtender" runat="server" BehaviorID="ingMRevTxbNombres_ValidatorCalloutExtender" TargetControlID="ingMRevTxbNombres">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>
                                            <td></td>
                                            <td class="left">
                                                <asp:Label runat="server" ID="ingMlblApellidos" CssClass="labelBoldForm">Apellidos:</asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="ingMtxbApellidos" AutoCompleteType="Disabled" CssClass="textBoxBlueForm" onkeypress='return esLetra(event)' onkeyup='this.value=retornaSoloLetras(this.value)'></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="ingMRevTxbApellidos" runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="ingMTxbApellidos" ValidationGroup="ingMGrpAceptar" Display="None"></asp:RequiredFieldValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="ingMRevTxbApellidos_ValidatorCalloutExtender" runat="server" BehaviorID="ingMRevTxbApellidos_ValidatorCalloutExtender" TargetControlID="ingMRevTxbApellidos">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>
                                            <td></td>
                                            <td>
                                                <asp:Label runat="server" ID="ingMlblNombreUsual" CssClass="labelBoldForm">Nombre Usual:</asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="ingMtxbNombreUsual" AutoCompleteType="Disabled" CssClass="textBoxBlueForm num" onkeypress='return esLetra(event)' onkeyup='this.value=retornaSoloLetras(this.value)'></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="left">
                                                <asp:Label runat="server" ID="ingMlblNacimiento" CssClass="labelBoldForm">Nacimiento:</asp:Label>
                                            </td>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="ingMtxbDiaNacimiento" AutoCompleteType="Disabled" CssClass="textBoxBlueForm num3" ValidationGroup="grpAceptar" MaxLength="2" onkeypress='return esDigito(event)' onkeyup='this.value=retornaSoloDigitos(this.value)'></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="ingMRevtxbDiaNacimiento" runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="ingMtxbDiaNacimiento" ValidationGroup="ingMGrpAceptar" Display="None" ClientValidationFunction="validate"></asp:RequiredFieldValidator>
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ingMRevtxbDiaNacimiento_ValidatorCalloutExtender" runat="server" BehaviorID="ingMRevtxbDiaNacimiento_ValidatorCalloutExtender" TargetControlID="ingMRevtxbDiaNacimiento">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList runat="server" ID="ingMddlMesNacimiento" CssClass="comboBoxBlueForm" ValidationGroup="grpAceptar"></asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="ingMRevddlMesNacimiento" runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="ingMddlMesNacimiento" ValidationGroup="ingMGrpAceptar" Display="None" ClientValidationFunction="validate"></asp:RequiredFieldValidator>
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ingMRevddlMesNacimiento_ValidatorCalloutExtender" runat="server" BehaviorID="ingMRevddlMesNacimiento_ValidatorCalloutExtender" TargetControlID="ingMRevddlMesNacimiento">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="ingMtxbAñoNacimiento" AutoCompleteType="Disabled" CssClass="textBoxBlueForm num3" ValidationGroup="grpAceptar" MaxLength="4" onkeypress='return esDigito(event)' onkeyup='this.value=retornaSoloDigitos(this.value)'></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="ingMRevtxbAñoNacimiento" runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="ingMtxbAñoNacimiento" ValidationGroup="ingMGrpAceptar" Display="None" ClientValidationFunction="validate"></asp:RequiredFieldValidator>
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ingMRevtxbAñoNacimiento_ValidatorCalloutExtender" runat="server" BehaviorID="ingMRevtxbAñoNacimiento_ValidatorCalloutExtender" TargetControlID="ingMRevtxbAñoNacimiento">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td></td>
                                            <td class="left">
                                                <asp:Label runat="server" ID="ingMlblGenero" CssClass="labelBoldForm">Género:</asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList runat="server" ID="ingMddlGenero" CssClass="comboBoxBlueForm"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="ingMRevddlGenero" runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="ingMddlGenero" ValidationGroup="ingMGrpAceptar" Display="None"></asp:RequiredFieldValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="ingMRevddlGenero_ValidatorCalloutExtender" runat="server" BehaviorID="ingMRevddlGenero_ValidatorCalloutExtender" TargetControlID="ingMRevddlGenero">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>
                                            <td></td>
                                            <td>
                                                <asp:Label runat="server" ID="ingMlblPuedeLeer" CssClass="labelBoldForm">Puede Leer:</asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList runat="server" ID="ingMddlPuedeLeer" CssClass="comboBoxBlueForm"></asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label runat="server" ID="ingMlblCUI" CssClass="labelBoldForm">CUI:</asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="ingMtxbCUI" AutoCompleteType="Disabled" CssClass="textBoxBlueForm"></asp:TextBox>
                                            </td>
                                            <td></td>
                                            <td>
                                                <asp:Label runat="server" ID="ingMlblNumeroCelular" CssClass="labelBoldForm">Número de Celular:</asp:Label>
                                            </td>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="ingMtxbNumeroCelular1" AutoCompleteType="Disabled" CssClass="textBoxBlueForm  num3" MaxLength="4" onkeypress='return esDigito(event)' onkeyup='this.value=retornaSoloDigitos(this.value)'></asp:TextBox>
                                                        </td>
                                                        <td>-</td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="ingMtxbNumeroCelular2" AutoCompleteType="Disabled" CssClass="textBoxBlueForm num3" MaxLength="4" onkeypress='return esDigito(event)' onkeyup='this.value=retornaSoloDigitos(this.value)'></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td></td>
                                            <td>
                                                <asp:Label runat="server" ID="ingMlblOtraAfil" CssClass="labelBoldForm">Otra Afiliación:</asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList runat="server" ID="ingMddlOtraAfil" CssClass="comboBoxBlueForm"></asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="left">
                                                <asp:Label runat="server" ID="ingMlblNumeroMadre" CssClass="labelBoldForm">Número Madre:</asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="ingMtxbNumeroMadre" AutoCompleteType="Disabled" CssClass="textBoxBlueForm num2" MaxLength="6" onkeypress='return esDigito(event)' onkeyup='this.value=retornaSoloDigitos(this.value)'></asp:TextBox>
                                            </td>
                                            <td></td>
                                            <td class="left">
                                                <asp:Label runat="server" ID="ingMlblNombreMadre" CssClass="labelBoldForm">Nombre Madre:</asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label runat="server" ID="ingMlblVNombreMadre" CssClass="labelForm"></asp:Label>
                                            </td>
                                            <td colspan="3"></td>
                                        </tr>
                                        <tr>
                                            <td class="left">
                                                <asp:Label runat="server" ID="ingMlblNumeroPadre" CssClass="labelBoldForm">Número Padre:</asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="ingMtxbNumeroPadre" AutoCompleteType="Disabled" CssClass="textBoxBlueForm num2" MaxLength="6" onkeypress='return esDigito(event)' onkeyup='this.value=retornaSoloDigitos(this.value)'></asp:TextBox>
                                            </td>
                                            <td></td>
                                            <td class="left">
                                                <asp:Label runat="server" ID="ingMlblNombrePadre" CssClass="labelBoldForm">Nombre Padre:</asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label runat="server" ID="ingMlblVNombrePadre" CssClass="labelForm"></asp:Label>
                                            </td>
                                            <td colspan="3"></td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:CheckBox runat="server" ID="ingMchkHayCUI" Visible="false" />&nbsp;<asp:Label runat="server" ID="ingMlblHayCUI" Visible="false" CssClass="labelBoldForm">Tenemos CUI</asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label runat="server" ID="ingMlblUltimoGrado" Visible="false" CssClass="labelBoldForm">Último Grado:</asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList runat="server" ID="ingMddlUltimoGrado" Visible="false" CssClass="comboBoxBlueForm"></asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td colspan="2" style="text-align: center">
                                                <asp:Button runat="server" ID="ingMbtnAceptar" CssClass="butonForm" Text="Aceptar" OnClick="ingMbtnAceptar_Click" ValidationGroup="ingMGrpAceptar"></asp:Button>
                                                <asp:Button runat="server" ID="ingMbtnGuardarConAjenas" CssClass="butonForm" Text="Guardar" Visible="false" OnClick="ingMbtnGuardarConAjenas_Click"></asp:Button>
                                                <asp:Button runat="server" ID="ingMbtnReestablecerConAjenas" CssClass="butonFormRet" Text="Corregir Relaciones" Visible="false" OnClick="ingMbtnReestablecerConAjenas_Click"></asp:Button>
                                                <asp:Button runat="server" ID="ingMbtnGuardar" CssClass="butonForm" Text="Guardar" Visible="false" OnClick="ingMbtnGuardar_Click"></asp:Button>
                                                <asp:Button runat="server" ID="ingMbtnReestablecer" CssClass="butonFormSec" Text="Reestablecer Cambios" Visible="False" OnClick="ingMbtnReestablecer_Click"></asp:Button>
                                                <asp:Button runat="server" ID="ingMbtnReestablecerRel" Text="Reestablecer Relaciones" Visible="false" CssClass="butonFormRet" OnClick="ingMbtnReestablecerRel_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="ingMpnlRelacionesAjenas" runat="server" Visible="false">
                                    <table class="tableContInfo tblCenter orange">
                                        <tr>
                                            <th>
                                                <asp:Label ID="ingMlblRelacionesajenas" runat="server">Relaciones Ajenas</asp:Label>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="ingMgdvRelacionesAjenas" CssClass="tableContInfo tblCenter orange" AutoGenerateColumns="false" runat="server" OnRowDataBound="ingMgdvRelacionesAjenas_RowDataBound">
                                                    <Columns>
                                                        <asp:BoundField DataField="Miembro" />
                                                        <asp:BoundField DataField="Familia" />
                                                        <asp:BoundField DataField="Nombre" />
                                                        <asp:BoundField DataField="RelacionDesc" />
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlRazonInactivo" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Relacion" />
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="ingMpnlMiembros" runat="server">
                                    <table class="tableContInfo tblCenter">
                                        <tr>
                                            <th>
                                                <asp:Label ID="ingMlblRelaciones" runat="server"></asp:Label>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="ingMgdvMiembros" CssClass="tableContInfo tblCenter" AutoGenerateColumns="false" runat="server" OnRowDataBound="ingMgdvMiembros_RowDataBound">
                                                    <Columns>
                                                        <asp:BoundField DataField="Relacion" />
                                                        <asp:BoundField DataField="RazonInactivo" HtmlEncode="false" />
                                                        <asp:BoundField DataField="Genero" />
                                                        <asp:BoundField DataField="fechaCreacion" />
                                                        <asp:BoundField DataField="fechaActivo" />
                                                        <asp:BoundField DataField="yaTieneRelacion" />
                                                        <asp:BoundField DataField="MemberId" />
                                                        <asp:BoundField DataField="Nombre" />
                                                        <asp:BoundField DataField="Edad" />
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlRelacionNueva" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlRazonInactivo" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="RelacionDesc" />
                                                        <asp:BoundField DataField="RazonInactivoDesc" />
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
            <asp:Panel runat="server" ID="pnlModificarMiembro" Visible="false" class="pestana">
                <div>
                    <table class="tblCenter">
                        <tr>
                            <td style="display: flex">
                                <asp:Panel ID="mdfMpnlMiembros" runat="server">
                                    <table class="tableContInfo">
                                        <tr>
                                            <th>
                                                <asp:Label ID="mdfMlblMiembros" runat="server"></asp:Label>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="mdfMgdvMiembros" CssClass="tableContInfo tblCenter" AutoGenerateColumns="false" runat="server" OnRowCommand="mdfMgdvMiembros_RowCommand">
                                                    <Columns>
                                                        <asp:BoundField DataField="MemberId" />
                                                        <asp:BoundField DataField="Nombre" />
                                                        <asp:BoundField DataField="Relacion" />
                                                        <asp:BoundField DataField="RazonInactivo" />
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Button ID="btnMName" runat="server"
                                                                    CommandName="cmdMName"
                                                                    CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                                    Text='<%#dic.seleccionar%>'
                                                                    CssClass="butonFormTable" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="mdfMpnlFActualizarMiembro" Visible="false" runat="server">
                                    <table class="tblCenter">
                                        <tr>
                                            <td class="left">
                                                <asp:Label runat="server" ID="mdfMlblNombres" CssClass="labelBoldForm">Nombres:</asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="mdfMtxbNombres" AutoCompleteType="Disabled" CssClass="textBoxBlueForm" onkeypress='return esLetra(event)' onkeyup='this.value=retornaSoloLetras(this.value)'></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="mdfMRevTxbNombres" runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="mdfMTxbNombres" ValidationGroup="mdfMGrpGuardar" Display="None"></asp:RequiredFieldValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="mdfMRevTxbNombres_ValidatorCalloutExtender" runat="server" BehaviorID="mdfMRevTxbNombres_ValidatorCalloutExtender" TargetControlID="mdfMRevTxbNombres">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>
                                            <td></td>
                                            <td class="left">
                                                <asp:Label runat="server" ID="mdfMlblApellidos" CssClass="labelBoldForm">Apellidos:</asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="mdfMtxbApellidos" AutoCompleteType="Disabled" CssClass="textBoxBlueForm" onkeypress='return esLetra(event)' onkeyup='this.value=retornaSoloLetras(this.value)'></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="mdfMRevTxbApellidos" runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="mdfMTxbApellidos" ValidationGroup="mdfMGrpGuardar" Display="None"></asp:RequiredFieldValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="mdfMRevTxbApellidos_ValidatorCalloutExtender" runat="server" BehaviorID="mdfMRevTxbApellidos_ValidatorCalloutExtender" TargetControlID="mdfMRevTxbApellidos">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="left">
                                                <asp:Label runat="server" ID="mdfMlblNacimiento" CssClass="labelBoldForm">Nacimiento:</asp:Label>
                                            </td>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="mdfMtxbDiaNacimiento" AutoCompleteType="Disabled" CssClass="textBoxBlueForm num3" MaxLength="2" onkeypress='return esDigito(event)' onkeyup='this.value=retornaSoloDigitos(this.value)'></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="mdfMRevtxbDiaNacimiento" runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="mdfMtxbDiaNacimiento" ValidationGroup="mdfMGrpGuardar" Display="None" ClientValidationFunction="validate"></asp:RequiredFieldValidator>
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="mdfMRevtxbDiaNacimiento_ValidatorCalloutExtender" runat="server" BehaviorID="mdfMRevtxbDiaNacimiento_ValidatorCalloutExtender" TargetControlID="mdfMRevtxbDiaNacimiento">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList runat="server" ID="mdfMddlMesNacimiento" CssClass="comboBoxBlueForm"></asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="mdfMRevddlMesNacimiento" runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="mdfMddlMesNacimiento" ValidationGroup="mdfMGrpGuardar" Display="None" ClientValidationFunction="validate"></asp:RequiredFieldValidator>
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="mdfMRevddlMesNacimiento_ValidatorCalloutExtender" runat="server" BehaviorID="mdfMRevddlMesNacimiento_ValidatorCalloutExtender" TargetControlID="mdfMRevddlMesNacimiento">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="mdfMtxbAñoNacimiento" AutoCompleteType="Disabled" CssClass="textBoxBlueForm num3" MaxLength="4" onkeypress='return esDigito(event)' onkeyup='this.value=retornaSoloDigitos(this.value)'></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="mdfMRevtxbAñoNacimiento" runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="mdfMtxbAñoNacimiento" ValidationGroup="mdfMGrpGuardar" Display="None" ClientValidationFunction="validate"></asp:RequiredFieldValidator>
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="mdfMRevtxbAñoNacimiento_ValidatorCalloutExtender" runat="server" BehaviorID="mdfMRevtxbAñoNacimiento_ValidatorCalloutExtender" TargetControlID="mdfMRevtxbAñoNacimiento">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td></td>
                                            <td class="left">
                                                <asp:Label runat="server" ID="mdfMlblNombreUsual" CssClass="labelBoldForm">Nombre Usual:</asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="mdfMtxbNombreUsual" AutoCompleteType="Disabled" CssClass="textBoxBlueForm num" onkeypress='return esLetra(event)' onkeyup='this.value=retornaSoloLetras(this.value)'></asp:TextBox>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td class="left">
                                                <asp:Label runat="server" ID="mdfMlblVivoOMuerto" CssClass="labelBoldForm">Vivo/Muerto:</asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label runat="server" ID="mdfMlblVVivoOMuerto" CssClass="labelForm"></asp:Label>
                                            </td>
                                            <td></td>
                                            <td class="left">
                                                <asp:Label runat="server" ID="mdfMlblFallecimiento" CssClass="labelBoldForm">Fallecimiento:</asp:Label>
                                            </td>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="mdfMtxbDiaFallecimiento" AutoCompleteType="Disabled" CssClass="textBoxBlueForm num3" MaxLength="2" onkeypress='return esDigito(event)' onkeyup='this.value=retornaSoloDigitos(this.value)'></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList runat="server" ID="mdfMddlMesFallecimiento" CssClass="comboBoxBlueForm"></asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="mdfMtxbAñoFallecimiento" AutoCompleteType="Disabled" CssClass="textBoxBlueForm num3" MaxLength="4" onkeypress='return esDigito(event)' onkeyup='this.value=retornaSoloDigitos(this.value)'></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="left">
                                                <asp:Label runat="server" ID="mdfMlblCUI" CssClass="labelBoldForm">CUI:</asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="mdfMtxbCUI" AutoCompleteType="Disabled" CssClass="textBoxBlueForm"></asp:TextBox>
                                            </td>
                                            <td></td>
                                            <td colspan="2">
                                                <asp:CheckBox runat="server" ID="mdfMchkHayCUI" Visible="false" /><asp:Label runat="server" ID="Label2" Visible="false" CssClass="labelForm"> &nbsp;Tenemos CUI</asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="left">
                                                <asp:Label runat="server" ID="mdfMlblUltimoGrado" Visible="false" CssClass="labelBoldForm">Último Grado:</asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList runat="server" ID="mdfMddlUltimoGrado" Visible="false" CssClass="comboBoxBlueForm"></asp:DropDownList>
                                            </td>
                                            <td></td>
                                            <td class="left">
                                                <asp:Label runat="server" ID="mdfMlblAñoUltimoGrado" Visible="false" CssClass="labelBoldForm">Año del Último Grado:</asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="mdfMtxbAñoUltimoGrado" AutoCompleteType="Disabled" Visible="false" CssClass="textBoxBlueForm num3" MaxLength="4" onkeypress='return esDigito(event)' onkeyup='this.value=retornaSoloDigitos(this.value)'></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="left">
                                                <asp:Label runat="server" ID="mdfMlblEstadoUltimoGrado" Visible="false" CssClass="labelBoldForm">Estado del Último Grado:</asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList runat="server" ID="mdfMddlEstadoUltimoGrado" Visible="false" CssClass="comboBoxBlueForm"></asp:DropDownList>
                                            </td>
                                            <td></td>
                                            <td class="left">
                                                <asp:Label runat="server" ID="mdfMlblPuedeLeer" CssClass="labelBoldForm">Puede Leer:</asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList runat="server" ID="mdfMddlPuedeLeer" CssClass="comboBoxBlueForm"></asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="left">
                                                <asp:Label runat="server" ID="mdfMlblNumeroCelular" CssClass="labelBoldForm">Número de Celular:</asp:Label>
                                            </td>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="mdfMtxbNumeroCelular1" AutoCompleteType="Disabled" CssClass="textBoxBlueForm  num3" MaxLength="4" onkeypress='return esDigito(event)' onkeyup='this.value=retornaSoloDigitos(this.value)'></asp:TextBox>
                                                        </td>
                                                        <td>-</td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="mdfMtxbNumeroCelular2" AutoCompleteType="Disabled" CssClass="textBoxBlueForm num3" MaxLength="4" onkeypress='return esDigito(event)' onkeyup='this.value=retornaSoloDigitos(this.value)'></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td></td>
                                            <td class="left">
                                                <asp:Label runat="server" ID="mdfMlblOtraAfil" CssClass="labelBoldForm">Otra Afiliación:</asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList runat="server" ID="mdfMddlOtraAfil" CssClass="comboBoxBlueForm"></asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="left">
                                                <asp:Label runat="server" ID="mdfMlblNumeroMadre" CssClass="labelBoldForm">Número Madre:</asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="mdfMtxbNumeroMadre" AutoCompleteType="Disabled" CssClass="textBoxBlueForm num2" MaxLength="6" onkeypress='return esDigito(event)' onkeyup='this.value=retornaSoloDigitos(this.value)'></asp:TextBox>
                                            </td>
                                            <td></td>
                                            <td class="left">
                                                <asp:Label runat="server" ID="mdfMlblNombreMadre" CssClass="labelBoldForm">Nombre Madre:</asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label runat="server" ID="mdfMlblVNombreMadre" CssClass="labelForm"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="left">
                                                <asp:Label runat="server" ID="mdfMlblNumeroPadre" CssClass="labelBoldForm">Número Padre:</asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="mdfMtxbNumeroPadre" AutoCompleteType="Disabled" CssClass="textBoxBlueForm num2" MaxLength="6" onkeypress='return esDigito(event)' onkeyup='this.value=retornaSoloDigitos(this.value)'></asp:TextBox>
                                            </td>
                                            <td></td>
                                            <td class="left">
                                                <asp:Label runat="server" ID="mdfMlblNombrePadre" CssClass="labelBoldForm">Nombre Padre:</asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label runat="server" ID="mdfMlblVNombrePadre" CssClass="labelForm"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="5" style="text-align: center">
                                                <asp:Button ID="mdfMbtnRegresar" runat="server" Text="Regresar" CssClass="butonFormRet" OnClick="mdfMbtnRegresar_Click" />
                                                <asp:Button runat="server" ID="mdfMbtnGuardar" CssClass="butonForm" Text="Guardar" ValidationGroup="mdfMGrpGuardar" OnClick="mdfMbtnGuardar_Click"></asp:Button>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
            <asp:Panel runat="server" ID="pnlModificarFamilia" Visible="false" class="pestana">
                <div class="">
                    <table class="tblCenter">
                        <tr>
                            <td style="display: flex">
                                <asp:Panel ID="mdfFpnlFormIngreso" runat="server">
                                    <table class="tblCenter">
                                        <tr>
                                            <td>
                                                <table class="tblCenter">
                                                    <tr>
                                                        <th></th>
                                                        <th></th>
                                                        <th style="width: 40px"></th>
                                                        <th></th>
                                                        <th></th>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label runat="server" ID="mdfFlblUltimaAct" CssClass="labelBoldForm" Visible="false">Última Actualización:</asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="mdfFtxbFechaUltimaAct" AutoCompleteType="Disabled" CssClass="textBoxBlueForm num2" Visible="false"></asp:TextBox>
                                                            <ajaxToolkit:CalendarExtender ID="mdfFtxbFechaUltimaAct_CalendarExtender" runat="server" BehaviorID="mdfFtxbFechaUltimaAct_CalendarExtender" Format="dd/MM/yyyy" TargetControlID="mdfFtxbFechaUltimaAct" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label runat="server" ID="mdfFlblDireccion" CssClass="labelBoldForm">Dirección:</asp:Label>
                                                        </td>
                                                        <td colspan="5">
                                                            <asp:TextBox runat="server" ID="mdfFtxbDireccion" AutoCompleteType="Disabled" CssClass="textBoxBlueForm" Width="400px"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="mdfFRevtxbDireccion" runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="mdfFtxbDireccion" ValidationGroup="mdfFGrpGuardar" Display="None"></asp:RequiredFieldValidator>
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="mdfFRevtxbDireccion_ValidatorCalloutExtender" runat="server" BehaviorID="mdfFRevtxbDireccion_ValidatorCalloutExtender" TargetControlID="mdfFRevtxbDireccion">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label runat="server" ID="mdfFlblArea" CssClass="labelBoldForm">Área:</asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList runat="server" ID="mdfFddlArea" CssClass="comboBoxBlueForm" OnSelectedIndexChanged="mdfFddlArea_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="mdfFRevddlArea" runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="mdfFddlArea" ValidationGroup="mdfFGrpGuardar" Display="None"></asp:RequiredFieldValidator>
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="mdfFRevddlArea_ValidatorCalloutExtender" runat="server" BehaviorID="mdfFRevddlArea_ValidatorCalloutExtender" TargetControlID="mdfFRevddlArea">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                        </td>
                                                        <td></td>
                                                        <td>
                                                            <asp:Label runat="server" ID="mdfFlblPueblo" CssClass="labelBoldForm">Pueblo:</asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label runat="server" ID="mdfFlblVPueblo" CssClass="labelForm"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label runat="server" ID="mdfFlblMunicipio" CssClass="labelBoldForm">Municipio:</asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList runat="server" ID="mdfFddlMunicipio" CssClass="comboBoxBlueForm"></asp:DropDownList>
                                                        </td>
                                                        <td></td>
                                                        <td>
                                                            <asp:Label runat="server" ID="mdfFlblTiempo" CssClass="labelBoldForm">Tiempo de Vivir en el Lugar:</asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="mdfFtxbTiempo" AutoCompleteType="Disabled" CssClass="textBoxBlueForm"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label runat="server" ID="mdfFlblNumeroCelular" CssClass="labelBoldForm">Número de Teléfono:</asp:Label>
                                                        </td>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="mdfFtxbNumeroCelular1" AutoCompleteType="Disabled" CssClass="textBoxBlueForm  num3" MaxLength="4" onkeypress='return esDigito(event)' onkeyup='this.value=retornaSoloDigitos(this.value)'></asp:TextBox>
                                                                    </td>
                                                                    <td>-</td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="mdfFtxbNumeroCelular2" AutoCompleteType="Disabled" CssClass="textBoxBlueForm num3" MaxLength="4" onkeypress='return esDigito(event)' onkeyup='this.value=retornaSoloDigitos(this.value)'></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td></td>
                                                        <td>
                                                            <asp:Label runat="server" ID="mdfFlblEtnia" CssClass="labelBoldForm">Etnia:</asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList runat="server" ID="mdfFddlEtnia" CssClass="comboBoxBlueForm"></asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 15px"></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="5" style="text-align: center">
                                                            <asp:Button ID="mdfFbtnGuardar" runat="server" Text="Guardar" CssClass="butonForm" ValidationGroup="mdfFGrpGuardar" OnClick="mdfFbtnGuardar_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
            <asp:Panel ID="rsgPnlReasignarFamiliaAMiembro" Visible="false" runat="server">
                <div>
                    <table class="tblCenter">
                        <tr style="text-align: center">
                            <td>
                                <asp:Button runat="server" ID="mdfFbtnGuardarRelaciones" CssClass="butonForm" Text="Guardar" ValidationGroup="mdfFGuardarRelaciones" OnClick="mdfFbtnGuardarRelaciones_Click"></asp:Button>
                                <asp:Button runat="server" ID="rsgBtnCancelarExportar" CssClass="butonFormSec" Visible="false" Text="Cancelar Asignación de Miembro de Otra Familia" OnClick="rsgBtnCancelarExportar_Click"></asp:Button>
                                <asp:Button runat="server" ID="mdfFbtnGuardarConAjenas" CssClass="butonForm" Text="Guardar" Visible="false" OnClick="mdfFbtnGuardarConAjenas_Click"></asp:Button>
                                <asp:Button runat="server" ID="mdfFbtnReestablecerConAjenas" CssClass="butonFormRet" Text="Corregir Relaciones" Visible="false" OnClick="mdfFbtnReestablecerConAjenas_Click"></asp:Button>
                                <asp:Button ID="rsgBtnNuevo" runat="server" Text="Asignar Miembro de Otra Familia" CssClass="butonFormSec" OnClick="rsgBtnNuevo_Click" />
                                <asp:Button ID="rsgBtnReestablecer" runat="server" Text="Reestablecer Relaciones" CssClass="butonFormRet" OnClick="rsgBtnReestablecer_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center">
                                <table class="tblCenter">
                                    <tr>
                                        <td>
                                            <asp:Label ID="rsgLblMiembroNuevo" runat="server" Visible="false" CssClass="labelBoldForm" Text="Miembro Nuevo"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="rsgTxbMiembroNuevo" runat="server" Visible="false" AutoCompleteType="Disabled" CssClass="textBoxBlueForm num2" MaxLength="6" onkeypress='return esDigito(event)' onkeyup='this.value=retornaSoloDigitos(this.value)'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rsgRevTxbMiembroNuevo" runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="rsgTxbMiembroNuevo" ValidationGroup="rsgBuscar" Display="None"></asp:RequiredFieldValidator>
                                            <ajaxToolkit:ValidatorCalloutExtender ID="rsgRevTxbMiembroNuevo_ValidatorCalloutExtender" runat="server" BehaviorID="rsgRevTxbMiembroNuevo_ValidatorCalloutExtender" TargetControlID="rsgRevTxbMiembroNuevo">
                                            </ajaxToolkit:ValidatorCalloutExtender>
                                        </td>
                                        <td>
                                            <asp:Button ID="rsgBtnBuscar" runat="server" Text="Buscar Miembro" Visible="false" CssClass="butonForm" ValidationGroup="rsgBuscar" OnClick="rsgBtnBuscar_Click" />
                                        </td>
                                        <td>
                                            <asp:Button ID="rsgBtnCancelar" runat="server" Text="Cancelar" Visible="false" CssClass="butonFormSec" OnClick="rsgBtnCancelar_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="mdfFpnlRelacionesAjenas" runat="server" Visible="false">
                                    <table class="tableContInfo tblCenter orange">
                                        <tr>
                                            <th>
                                                <asp:Label ID="mdfFlblRelacionesajenas" runat="server">Relaciones Ajenas</asp:Label>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="mdfFgdvRelacionesAjenas" CssClass="tableContInfo tblCenter" AutoGenerateColumns="false" runat="server" OnRowDataBound="ingMgdvRelacionesAjenas_RowDataBound">
                                                    <Columns>
                                                        <asp:BoundField DataField="Miembro" />
                                                        <asp:BoundField DataField="Familia" />
                                                        <asp:BoundField DataField="Nombre" />
                                                        <asp:BoundField DataField="RelacionDesc" />
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlRazonInactivo" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Relacion" />
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="rsgPnlExportarMiembro" runat="server" Visible="false">
                                    <table class="tableCont">
                                        <tr>
                                            <th>
                                                <asp:Label runat="server" ID="rsgLblInactivar">Inactivar Relación de Miembro</asp:Label>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table id="rsgTbl">
                                                    <tr>
                                                        <th>
                                                            <asp:Label runat="server" ID="rsgLblMiembro">Inactivar Relación de Miembro</asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" ID="rsgLblFamilia">Inactivar Relación de Miembro</asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" ID="rsgLblNombre">Inactivar Relación de Miembro</asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" ID="rsgLblRelacion">Inactivar Relación de Miembro</asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" ID="rsgLblRazonInactivo">Inactivar Relación de Miembro</asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" ID="rsgLblNuevaRelacion">Inactivar Relación de Miembro</asp:Label></th>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="rsgLblVMiembro" runat="server" Text="Nombre"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="rsgLblVFamilia" runat="server" Text="Familia"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="rsgLblVNombre" runat="server"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="rsgLblVRelacion" runat="server"></asp:Label></td>
                                                        <td>
                                                            <asp:DropDownList ID="rsgDdlRazonInactivo" runat="server"></asp:DropDownList></td>
                                                        <asp:RequiredFieldValidator ID="rsgRevddlRazonInactivo" runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="rsgDdlRazonInactivo" ValidationGroup="mdfFGuardarRelaciones" Display="None"></asp:RequiredFieldValidator>
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="rsgRevddlRazonInactivo_ValidatorCalloutExtender" runat="server" BehaviorID="rsgRevddlRazonInactivo_ValidatorCalloutExtender" TargetControlID="rsgRevddlRazonInactivo">
                                                        </ajaxToolkit:ValidatorCalloutExtender>
                                                        <td>
                                                            <asp:DropDownList ID="rsgDdlNuevaRelacion" runat="server"></asp:DropDownList></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="mdfFpnlMiembros" runat="server">
                                    <table class="tableContInfo tblCenter">
                                        <tr>
                                            <th>
                                                <asp:Label ID="mdfFlblRelaciones" runat="server"></asp:Label>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="mdfFgdvMiembros" CssClass="tableContInfo tblCenter" AutoGenerateColumns="false" runat="server" OnRowDataBound="mdfFgdvMiembros_RowDataBound">
                                                    <Columns>
                                                        <asp:BoundField DataField="Relacion" />
                                                        <asp:BoundField DataField="RazonInactivo" HtmlEncode="false" />
                                                        <asp:BoundField DataField="Genero" />
                                                        <asp:BoundField DataField="fechaCreacion" />
                                                        <asp:BoundField DataField="fechaActivo" />
                                                        <asp:BoundField DataField="MemberId" />
                                                        <asp:BoundField DataField="Nombre" />
                                                        <asp:BoundField DataField="Edad" />
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlRelacionNueva" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlRazonInactivo" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlTelefonos" Visible="false" runat="server">
                <div>
                    <div>
                        <table class="tblCenter">
                            <tr>
                                <td>
                                    <asp:Panel runat="server" ID="tlfPnlRegistro">
                                        <table class="tblCenter">
                                            <tr>
                                                <td>
                                                    <asp:Panel runat="server" ID="tlfPnlGuardar">
                                                        <table>
                                                            <tr>
                                                                <th></th>
                                                                <th></th>
                                                                <th style="width: 40px"></th>
                                                                <th></th>
                                                                <th></th>
                                                            </tr>
                                                            <tr>
                                                                <td class="left">
                                                                    <asp:Label runat="server" ID="tlfFlblNumeroCelular" CssClass="labelBoldForm">Número de Teléfono:</asp:Label>
                                                                </td>
                                                                <td>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:TextBox runat="server" ID="tlfFtxbNumeroCelular1" AutoCompleteType="Disabled" CssClass="textBoxBlueForm  num3" MaxLength="4" onkeypress='return esDigito(event)' onkeyup='this.value=retornaSoloDigitos(this.value)'></asp:TextBox>
                                                                            </td>
                                                                            <td>-</td>
                                                                            <td>
                                                                                <asp:TextBox runat="server" ID="tlfFtxbNumeroCelular2" AutoCompleteType="Disabled" CssClass="textBoxBlueForm num3" MaxLength="4" onkeypress='return esDigito(event)' onkeyup='this.value=retornaSoloDigitos(this.value)'></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td></td>
                                                                <td class="left">
                                                                    <asp:Label ID="tlfLblPertenece" runat="server" CssClass="labelBoldForm" Text="Pertenece a:"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="tlfDdlPertenece" runat="server" class="comboBoxBlueForm"></asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="tlfRevDdlPertenece" runat="server" ControlToValidate="tlfDdlPertenece" ErrorMessage="RequiredFieldValidator" ValidationGroup="tlfGrpGuardar" Display="none"></asp:RequiredFieldValidator>

                                                                    <ajaxToolkit:ValidatorCalloutExtender ID="tlfRevDdlPertenece_ValidatorCalloutExtender" runat="server" BehaviorID="tlfRevDdlPertenece_ValidatorCalloutExtender" TargetControlID="tlfRevDdlPertenece">
                                                                    </ajaxToolkit:ValidatorCalloutExtender>

                                                                    <asp:Label ID="tlfLblVPertenece" runat="server" CssClass="labelForm" Visible="false"></asp:Label>

                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="left">
                                                                    <asp:Label ID="tlfLblCompañia" runat="server" CssClass="labelBoldForm" Text="Compañia:"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="tlfDdlCompañia" runat="server" class="comboBoxBlueForm">
                                                                        <asp:ListItem>
                                                                        </asp:ListItem>
                                                                        <asp:ListItem>
                                                                       Claro
                                                                        </asp:ListItem>
                                                                        <asp:ListItem>
                                                                       Movistar
                                                                        </asp:ListItem>
                                                                        <asp:ListItem>
                                                                       Tigo
                                                                        </asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td></td>
                                                                <td class="left" colspan="2">
                                                                    <asp:CheckBox runat="server" ID="tlfChkEsWhatsapp" Checked="false" />
                                                                    <asp:Label ID="tlfLblEsWhatsapp" runat="server" CssClass="labelBoldForm" Text="Es de Whatsapp:"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: center" colspan="5">
                                                                    <asp:Button ID="tlfBtnGuardarIngreso" runat="server" Text="Guardar" class="butonForm" ValidationGroup="tlfGrpGuardar" OnClick="tlfBtnGuardar_Click" />
                                                                    <asp:Button ID="tlfBtnEliminarIngreso" runat="server" Text="Eliminar" Visible="false" class="butonFormSec" OnClick="tlfBtnEliminar_Click" />
                                                                    <asp:Button ID="tlfBtnInsertarIngreso" runat="server" Text="Nuevo Ingreso" Visible="false" class="butonFormRet" OnClick="tlfBtnInsertar_Click" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <div style="height: 25px"></div>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:Panel runat="server" ID="tlfPnlTelefonos" CssClass="scroll-Y">
                                            <table class="tableContInfo tblCenter gray">
                                                <tr>
                                                    <th>
                                                        <asp:Label ID="tlfLblHistorialTelefonos" runat="server">Historial</asp:Label>
                                                    </th>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:GridView ID="tlfGdvTelefonos" ShowFooter="true" CssClass="tableContInfo tblCenter gray" runat="server" AutoGenerateColumns="false" OnRowCommand="tlfGdvTelefonos_RowCommand" OnRowDataBound="tlfGdvTelefonos_RowDataBound">
                                                            <Columns>
                                                                <asp:BoundField DataField="CreationDateTime" />
                                                                <asp:BoundField DataField="Pertenece" HtmlEncode="false" />
                                                                <asp:BoundField DataField="Numero" HtmlEncode="false" />
                                                                <asp:BoundField DataField="StartDateU" />
                                                                <asp:BoundField DataField="Compañia" />
                                                                <asp:BoundField DataField="EsWhatsapp" />
                                                                <asp:BoundField DataField="Activo" />
                                                                <asp:BoundField DataField="UserId" />
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:Button ID="extBtnSeleccionar" runat="server"
                                                                            CommandName="cmdSeleccionar"
                                                                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                                            Text='<%#dic.seleccionar%>' CssClass="butonFormTable" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                        <asp:Label runat="server" ID="extLblNoTiene" CssClass="labelFormSec"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>
    <script type="text/javascript">

    </script>
</asp:Content>
