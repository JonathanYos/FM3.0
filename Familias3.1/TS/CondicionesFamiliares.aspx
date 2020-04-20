<%@ Page Language="C#" Async="true" AutoEventWireup="true" MasterPageFile="~/mast.Master" CodeBehind="CondicionesFamiliares.aspx.cs" Inherits="Familias3._1.TS.RegistrarCondiciones" %>

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
                <asp:LinkButton ID="lnkVivienda" CssClass="cabecera c-activa" runat="server" OnClick="lnkVivienda_Click">Vivienda</asp:LinkButton>
                <asp:LinkButton ID="lnkGastos" CssClass="cabecera" runat="server" OnClick="lnkGastos_Click">Gastos</asp:LinkButton>
                <asp:LinkButton ID="lnkOcupaciones" CssClass="cabecera" runat="server" OnClick="lnkOcupaciones_Click">Ocupaciones</asp:LinkButton>
                <asp:LinkButton ID="lnkIngresosExtra" CssClass="cabecera" runat="server" OnClick="lnkIngresosExtra_Click">Ingresos Extra</asp:LinkButton>
                <asp:LinkButton ID="lnkPosesiones" CssClass="cabecera" runat="server" OnClick="lnkPosesiones_Click">Posesiones</asp:LinkButton>
            </div>
            <div style="height: 20px"></div>
            <asp:Panel runat="server" ID="pnlVivienda" class="pestana p-activa">
                <div>
                    <table class="mainTable tblCenter">
                        <tr>
                            <th></th>
                            <th style="width: 30px"></th>
                            <th></th>
                        </tr>
                        <tr>
                            <td colspan="3" style="text-align: center">
                                <asp:Button ID="vvnBtnNuevasCondicionesVVn" runat="server" Visible="false" OnClick="vvnBtnNuevasCondicionesVVn_Click" class="butonFormRet" />
                                <asp:Button ID="vvnBtnGuardarVvn" runat="server" OnClick="vvnBtnGuardarVvn_Click" class="butonForm" />
                                <asp:Button ID="vvnBtnEliminarVvn" runat="server" Visible="false" OnClick="vvnBtnEliminarVvn_Click" class="butonFormSec" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div style="height: 5px"></div>
                                <table class="tableContInfo blue">
                                    <tr>
                                        <td class="center" colspan="4">
                                            <asp:Label ID="vvnLblCasa" runat="server" Text="Casa:"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td class="center"></td>
                                                    <td class="center">
                                                        <asp:Label ID="vvnLblMaterialCsa" runat="server" Text="Material"></asp:Label>
                                                    </td>
                                                    <td class="center">
                                                        <asp:Label ID="vvnLblCalidadCsa" runat="server" Text="Calidad"></asp:Label>
                                                    </td>
                                                    <td class="center">
                                                        <asp:Label ID="vvnLblNotasCsa" runat="server" Text="Notas"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="left">
                                                        <asp:Label ID="vvnLblParedCsa" runat="server" Text="Pared"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="vvnDdlMatParedCsa" runat="server" class="comboBoxForm"></asp:DropDownList>
                                                        <asp:Label ID="vvnLblVMatParedCsa" runat="server" Visible="false" class="labelForm"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="vvnDdlCaldParedCsa" runat="server" class="comboBoxForm"></asp:DropDownList>
                                                        <asp:Label ID="vvnLblVCaldParedCsa" runat="server" Visible="false" class="labelForm"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="vvnTxbNotasParedCsa" runat="server" AutoCompleteType="Disabled" class="textBoxForm"></asp:TextBox>
                                                        <asp:Label ID="vvnLblVNotasParedCsa" runat="server" Visible="false" class="labelForm"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="left">
                                                        <asp:Label ID="vvnLblTechoCsa" runat="server" Text="Techo"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="vvnDdlMatTechoCsa" runat="server" class="comboBoxForm"></asp:DropDownList>
                                                        <asp:Label ID="vvnLblVMatTechoCsa" runat="server" Visible="false" class="labelForm"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="vvnDdlCaldTechoCsa" runat="server" class="comboBoxForm"></asp:DropDownList>
                                                        <asp:Label ID="vvnLblVCaldTechoCsa" runat="server" Visible="false" class="labelForm"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="vvnTxbNotasTechoCsa" AutoCompleteType="Disabled" runat="server" class="textBoxForm"></asp:TextBox>
                                                        <asp:Label ID="vvnLblVNotasTechoCsa" runat="server" Visible="false" class="labelForm"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="left">
                                                        <asp:Label ID="vvnLblPisoCsa" runat="server" Text="Piso"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="vvnDdlMatPisoCsa" runat="server" class="comboBoxForm"></asp:DropDownList>
                                                        <asp:Label ID="vvnLblVMatPisoCsa" runat="server" Visible="false" class="labelForm"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="vvnDdlCaldPisoCsa" runat="server" class="comboBoxForm"></asp:DropDownList>
                                                        <asp:Label ID="vvnLblVCaldPisoCsa" runat="server" Visible="false" class="labelForm"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="vvnTxbNotasPisoCsa" AutoCompleteType="Disabled" runat="server" class="textBoxForm"></asp:TextBox>
                                                        <asp:Label ID="vvnLblVNotasPisoCsa" runat="server" Visible="false" class="labelForm"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td></td>
                            <td>
                                <table class="tableContInfo blue">
                                    <tr class="center">
                                        <td class="center" colspan="4">
                                            <asp:Label ID="vvnLblCocina" runat="server" Text="Cocina:"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td class="center"></td>
                                                    <td class="center">
                                                        <asp:Label ID="vvnLblMaterialCcna" runat="server" Text="Material"></asp:Label>
                                                    </td>
                                                    <td class="center">
                                                        <asp:Label ID="vvnLblCalidadCcna" runat="server" Text="Calidad"></asp:Label>
                                                    </td>
                                                    <td class="center">
                                                        <asp:Label ID="vvnLblNotasCcna" runat="server" Text="Notas"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="left">
                                                        <asp:Label ID="vvnLblParedCcna" runat="server" Text="Pared"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="vvnDdlMatParedCcna" runat="server" class="comboBoxForm"></asp:DropDownList>
                                                        <asp:Label ID="vvnLblVMatParedCcna" runat="server" Visible="false" class="labelForm"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="vvnDdlCaldParedCcna" runat="server" class="comboBoxForm"></asp:DropDownList>
                                                        <asp:Label ID="vvnLblVCaldParedCcna" runat="server" Visible="false" class="labelForm"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="vvnTxbNotasParedCcna" AutoCompleteType="Disabled" runat="server" class="textBoxForm"></asp:TextBox>
                                                        <asp:Label ID="vvnLblVNotasParedCcna" runat="server" Visible="false" class="labelForm"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="left">
                                                        <asp:Label ID="vvnLblTechoCcna" runat="server" Text="Techo"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="vvnDdlMatTechoCcna" runat="server" class="comboBoxForm"></asp:DropDownList>
                                                        <asp:Label ID="vvnLblVMatTechoCcna" runat="server" Visible="false" class="labelForm"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="vvnDdlCaldTechoCcna" runat="server" class="comboBoxForm"></asp:DropDownList>
                                                        <asp:Label ID="vvnLblVCaldTechoCcna" runat="server" Visible="false" class="labelForm"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="vvnTxbNotasTechoCcna" AutoCompleteType="Disabled" runat="server" class="textBoxForm"></asp:TextBox>
                                                        <asp:Label ID="vvnLblVNotasTechoCcna" runat="server" Visible="false" class="labelForm"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="height: 15px"></td>
                        </tr>
                        <tr>
                            <td>
                                <table class="tableContInfo blue">
                                    <tr>
                                        <td class="center" colspan="4">
                                            <asp:Label ID="vvnLblTerreno" runat="server" Text="Terreno:"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="left">
                                            <asp:Label ID="vvnLblTenencia" runat="server" Text="Tenencia"> </asp:Label></td>
                                        <td colspan="3">
                                            <asp:DropDownList ID="vvnDdlTenencia" runat="server" class="comboBoxForm"></asp:DropDownList>
                                            <asp:Label ID="vvnLblVTenencia" runat="server" Visible="false" class="labelForm"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="left">
                                            <asp:Label ID="vvnLblTamaño" runat="server" Text="Tamaño Terreno"> </asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="vvnTxbTamañoX" runat="server" AutoCompleteType="Disabled" class="textBoxForm num3" onkeypress='return esDigito(event)' onkeyup='this.value=retornaSoloDigitos(this.value)'></asp:TextBox>
                                            <asp:Label ID="vvnLblVTamañoX" runat="server" Visible="false" class="labelForm"></asp:Label>
                                        </td>
                                        <td>
                                            <span class="labelForm">X </span></td>
                                        <td>
                                            <asp:TextBox ID="vvnTxbTamañoY" runat="server" AutoCompleteType="Disabled" class="textBoxForm num3" onkeypress='return esDigito(event)' onkeyup='this.value=retornaSoloDigitos(this.value)'></asp:TextBox>
                                            <asp:Label ID="vvnLblVTamañoY" runat="server" Visible="false" class="labelForm"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="left">
                                            <asp:Label ID="vvnLblJardín" runat="server" Text="Tamaño Área Verde"> </asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="vvnTxbTamañoXJardin" runat="server" AutoCompleteType="Disabled" class="textBoxForm num3" onkeypress='return esDigito(event)' onkeyup='this.value=retornaSoloDigitos(this.value)'></asp:TextBox>
                                            <asp:Label ID="vvnLblVTamañoXJardin" runat="server" Visible="false" class="labelForm"></asp:Label>
                                        </td>
                                        <td>
                                            <span class="labelForm">X </span>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="vvnTxbTamañoYJardin" runat="server" AutoCompleteType="Disabled" class="textBoxForm num3" onkeypress='return esDigito(event)' onkeyup='this.value=retornaSoloDigitos(this.value)'></asp:TextBox>
                                            <asp:Label ID="vvnLblVTamañoYJardin" runat="server" Visible="false" class="labelForm"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <asp:CheckBox ID="vvnChkTieneEsc" runat="server" />
                                            <asp:Label ID="vvnLblTieneEsc" runat="server" Text="Tiene Escritura"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td></td>
                            <td>
                                <table class="tableContInfo blue">
                                    <tr>
                                        <td class="center" colspan="4">
                                            <asp:Label ID="vvnLblOtros" runat="server" Text="Otros:"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="left">
                                            <asp:Label ID="vvnLblNoCuartos" runat="server" Text="# Cuartos"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="vvnDdlNoCuartos" runat="server" class="comboBoxForm"></asp:DropDownList>
                                            <asp:Label ID="vvnLblVNoCuartos" runat="server" Visible="false" class="labelForm"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="left">
                                            <asp:Label ID="vvnLblHigiene" runat="server" Text="Higiene"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="vvnDdlHigiene" runat="server" class="comboBoxForm"></asp:DropDownList>
                                            <asp:Label ID="vvnLblVHigiene" runat="server" Visible="false" class="labelForm"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="left">
                                            <asp:Label ID="vvnLblNotasHigiene" runat="server" Text="Notas Higiene"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="vvnTxbHigieneNotas" AutoCompleteType="Disabled" runat="server" class="textBoxForm"></asp:TextBox>
                                            <asp:Label ID="vvnLblVHigieneNotas" runat="server" Visible="false" class="labelForm"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:CheckBox ID="vvnChkSegundoNivel" runat="server" />
                                            <asp:Label ID="vvnLblSegundoNivel" runat="server" Text="Tiene Segundo Nivel"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height: 15px"></td>
                        </tr>
                        <tr>
                            <td>
                                <table class="tableContInfo tblCenter blue">
                                    <tr>
                                        <td class="center" colspan="4">
                                            <asp:Label ID="vvnLblServicios" runat="server" Text="Servicios"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="left">
                                            <asp:Label ID="vvnLblAgua" runat="server" Text="Agua"></asp:Label></td>
                                        <td>
                                            <asp:DropDownList ID="vvnDdlAgua" runat="server" class="comboBoxForm"></asp:DropDownList>
                                            <asp:Label ID="vvnLblVAgua" runat="server" Visible="false" class="labelForm"></asp:Label>
                                        </td>
                                        <td class="left">
                                            <asp:Label ID="vvnLblElectricidad" runat="server" Text="Electricidad"></asp:Label></td>
                                        <td>
                                            <asp:DropDownList ID="vvnDdlElectricidad" runat="server" class="comboBoxForm"></asp:DropDownList>
                                            <asp:Label ID="vvnLblVElectricidad" runat="server" Visible="false" class="labelForm"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="left">
                                            <asp:Label ID="vvnLblDrenaje" runat="server" Text="Drenaje"></asp:Label></td>
                                        <td>
                                            <asp:DropDownList ID="vvnDdlDrenaje" runat="server" class="comboBoxForm"></asp:DropDownList>
                                            <asp:Label ID="vvnLblVDrenaje" runat="server" Visible="false" class="labelForm"></asp:Label>
                                        </td>
                                        <td class="left">
                                            <asp:Label ID="vvnLblExcretas" runat="server" Text="Baño"></asp:Label></td>
                                        <td>
                                            <asp:DropDownList ID="vvnDdlExcretas" runat="server" class="comboBoxForm"></asp:DropDownList>
                                            <asp:Label ID="vvnLblVExcretas" runat="server" Visible="false" class="labelForm"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td></td>
                            <td>
                                <%-- <asp:Panel runat="server" ID="vvnPnlHistorial" CssClass="scroll-Y">--%>
                                <table class="tableContInfo gray">
                                    <tr>
                                        <th>
                                            <asp:Label ID="vvnLblHistorial" runat="server">Historial</asp:Label>
                                        </th>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="vvnGdvHistorial" CssClass="tableContInfo tblCenter gray" runat="server" AutoGenerateColumns="false" OnRowCommand="vvnGdvHistorial_RowCommand">
                                                <Columns>
                                                    <asp:BoundField DataField="CreationDateTime" />
                                                    <asp:BoundField DataField="StartDate" />
                                                    <asp:BoundField DataField="Active" />
                                                    <asp:BoundField DataField="FechaInicio" />
                                                    <asp:BoundField DataField="Estado" Visible="false" />
                                                    <asp:BoundField DataField="Usuario" />
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnSeleccionar" runat="server"
                                                                CommandName="cmdSeleccionar"
                                                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                                Text='<%#dic.seleccionar%>'
                                                                CssClass="butonFormTable" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:Label runat="server" ID="vvnLblNoTiene" CssClass="labelFormSec"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <%-- </asp:Panel>--%>
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
            <asp:Panel runat="server" ID="pnlGastos" Visible="false" class="pestana">
                <div>
                    <table class="tblCenter green">
                        <tr>
                            <td style="text-align: center">
                                <asp:Button ID="gstBtnGuardarGastos" runat="server" Text="Guardar / Save" OnClick="gstBtnGuardarGastos_Click" class="butonForm" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="gdvGastos" CssClass="tableCont mediumTbl tblCenter green" runat="server" AutoGenerateColumns="false">
                                    <Columns>
                                        <%--                                    <asp:TemplateField ItemStyle-CssClass="left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGasto" runat="server" CssClass="labelForm" Text ='<%#Eval("Des")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                        </EditItemTemplate>
                                    </asp:TemplateField>--%>
                                        <asp:BoundField DataField="Expense" HtmlEncode="false" />
                                        <asp:BoundField DataField="Des" ItemStyle-CssClass="left" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblVMonto" Font-Bold='<%# (Eval("Edit").ToString().Equals("0")) ? true : false%>' ItemStyle-CssClass="labelForm" Text='<%# Eval("Amount").ToString()%>' Visible='<%# (Eval("Edit").ToString().Equals("0")) ? true : false%>'></asp:Label>
                                                <%--<asp:TextBox ID="txbMonto" runat="server"
                                                Text='<%# Eval("Amount") %>' AutoCompleteType="Disabled" CssClass="" MaxLength="5" onkeypress='return esDigito(event)' onkeyup='this.value=retornaSoloDigitos(this.value)' Visible='<%# (Eval("Edit").ToString().Equals("1")) ? true : false%>' />--%>
                                                <asp:TextBox ID="txbMonto" runat="server"
                                                    Text='<%# Eval("Amount") %>' AutoCompleteType="Disabled" CssClass="" MaxLength="7" onkeydown='keyDownMonedas(this.value)' onkeyup='this.value = keyUpMonedas(this.value)' Visible='<%# (Eval("Edit").ToString().Equals("1")) ? true : false%>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                            </EditItemTemplate>
                                            <ControlStyle Width="50px"></ControlStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                    <div style="height: 10px"></div>
                    <table class="tableContInfo tblCenter blue">
                        <tr>
                            <th colspan="2">
                                <asp:Label runat="server" ID="gstLblUtilidad">Utilidad</asp:Label>
                            </th>
                        </tr>
                        <tr>
                            <td>
                                <asp:Table runat="server" ID="tblDiferenciaIngresos" Visible="false" class="tableCont tblCenter blue">
                                    <asp:TableRow>
                                        <asp:TableCell class="left">
                                            <asp:Label runat="server" ID="gstLblTotalIngresosOcupaciones"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label runat="server" ID="gstLblVTotalIngresosOcupaciones"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell class="left">
                                            <asp:Label runat="server" ID="gstLblTotalIngresosExtra"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label runat="server" ID="gstLblVTotalIngresosExtra"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell class="left">
                                            <asp:Label runat="server" ID="gstLblGastoMen"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label runat="server" ID="gstLblVGastoMen"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell class="left">
                                            <asp:Label runat="server" ID="gstLblDiferenciaIngreso"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label runat="server" ID="gstLblVDiferenciaIngreso"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </td>
                            <td>
                                <asp:Panel runat="server" Visible="false">
                                    <asp:Table runat="server" ID="tblDiferenciaAporte" Visible="false" class="tableCont tblCenter blue">
                                        <asp:TableRow>
                                            <asp:TableCell class="left">
                                                <asp:Label runat="server" ID="gstLblTotalAportesOcupaciones"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label runat="server" ID="gstLblVTotalAportesOcupaciones"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell class="left">
                                                <asp:Label runat="server" ID="gstLblGastoMen2"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label runat="server" ID="gstLblVGastosMen2"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell class="left">
                                                <asp:Label runat="server" ID="gstLblDiferenciaAporte"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label runat="server" ID="gstLblVDiferenciaAporte"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
            <asp:Panel runat="server" ID="pnlOcupaciones" Visible="false" class="pestana">
                <div>
                    <table class="tblCenter">
                        <tr>
                            <td>
                                <asp:Panel ID="ocpPnlMiembros" runat="server" CssClass="">
                                    <table class="tableContInfo blue">
                                        <tr>
                                            <th>
                                                <asp:Label ID="ocpLblMiembros" runat="server"></asp:Label>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="ocpGdvMiembros" CssClass="tableContInfo tblCenter blue" AutoGenerateColumns="false" runat="server" OnRowCommand="ocpGdvMiembros_RowCommand" OnRowDataBound="ocpGdvMiembros_RowDataBound" ShowFooter="true">
                                                    <Columns>
                                                        <asp:BoundField DataField="Inactivo" />
                                                        <asp:BoundField DataField="MemberId" />
                                                        <asp:BoundField DataField="Nombre" />
                                                        <asp:BoundField DataField="Relacion" />
                                                        <asp:BoundField DataField="RazonInactivo" />
                                                        <asp:BoundField DataField="TipoAfil" />
                                                        <asp:BoundField DataField="Ocupaciones" />
                                                        <%--<asp:BoundField DataField="IngresoMensual" />--%>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <div>
                                                                    <asp:Label ID="lblIngresoMen" runat="server" Text='<%# Eval("IngresoMensual") %>' />
                                                                </div>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <div>
                                                                    <asp:Label ID="lblFtrIngresoMen" runat="server" />
                                                                </div>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="AporteMensual" Visible="false" />
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Button ID="btnMName" runat="server"
                                                                    CommandName="cmdMName"
                                                                    CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                                    Text='<%#dic.seleccionar%>' CssClass="butonFormTable" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel runat="server" ID="ocpPnlRegistro" Visible="false">
                                    <table class="tblCenter">
                                        <tr>
                                            <td style="text-align: center">
                                                <asp:Button ID="ocpBtnRegresar" runat="server" Text="Regresar" OnClick="ocpBtnRegresar_Click" class="butonFormRet" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Panel runat="server" ID="ocpPnlGuardar">
                                                    <table>
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
                                                                <asp:Label ID="ocpLblNmbMiembro" runat="server" CssClass="labelBoldForm" Text="Nombre de Miembro:"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="ocpLblVNmbMiembro" runat="server" CssClass="labelForm" Text=""></asp:Label>
                                                            </td>
                                                            <td></td>
                                                            <td class="left">
                                                                <asp:Label ID="ocpLblOcupacion" runat="server" CssClass="labelBoldForm" Text="Ocupacion:"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ocpDdlOcupacion" runat="server" class="comboBoxBlueForm" AutoPostBack="true" OnSelectedIndexChanged="ocpDdlOcupacion_SelectedIndexChanged"></asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="ocpRevDdlOcupacion" runat="server" ControlToValidate="ocpDdlOcupacion" ErrorMessage="RequiredFieldValidator" ValidationGroup="ocpGrpGuardar" Display="none"></asp:RequiredFieldValidator>

                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ocpRevDdlOcupacion_ValidatorCalloutExtender" runat="server" BehaviorID="ocpRevDdlOcupacion_ValidatorCalloutExtender" TargetControlID="ocpRevDdlOcupacion">
                                                                </ajaxToolkit:ValidatorCalloutExtender>

                                                                <asp:Label ID="ocpLblVOcupacion" runat="server" CssClass="labelForm" Visible="false"></asp:Label>

                                                            </td>
                                                            <td></td>
                                                            <td class="left">
                                                                <asp:Label ID="ocpLblCategoria" runat="server" CssClass="labelBoldForm" Text="Categoría:"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ocpDdlCategoria" runat="server" class="comboBoxBlueForm" AutoPostBack="true" OnSelectedIndexChanged="ocpDdlCategoria_SelectedIndexChanged"></asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="ocpRevDdlCategoria" runat="server" ControlToValidate="ocpDdlCategoria" ErrorMessage="RequiredFieldValidator" ValidationGroup="ocpGrpGuardar" Display="none"></asp:RequiredFieldValidator>

                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ocpRevDdlCategoria_ValidatorCalloutExtender" runat="server" BehaviorID="ocpRevDdlCategoria_ValidatorCalloutExtender" TargetControlID="ocpRevDdlCategoria">
                                                                </ajaxToolkit:ValidatorCalloutExtender>

                                                                <asp:Label ID="ocpLblVCategoria" runat="server" CssClass="labelForm" Visible="false"></asp:Label>

                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="left">
                                                                <asp:Label ID="ocpLblFechaInicio" runat="server" CssClass="labelBoldForm" Text="Fecha Inicio:"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="ocpTxbFechaInicio" runat="server" AutoCompleteType="Disabled" class="textBoxBlueForm date noPaste" contentEditable="false"></asp:TextBox>
                                                                <ajaxToolkit:CalendarExtender ID="ocpTxbFechaInicio_CalendarExtender" runat="server" BehaviorID="ocpTxbFechaInicio_CalendarExtender" Format="dd/MM/yyyy" TargetControlID="ocpTxbFechaInicio"></ajaxToolkit:CalendarExtender>
                                                                <asp:RequiredFieldValidator ID="ocpRevTxbFechaInicio" runat="server" ControlToValidate="ocpTxbFechaInicio" ErrorMessage="RequiredFieldValidator" ValidationGroup="ocpGrpGuardar" Display="None"></asp:RequiredFieldValidator>
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ocpRevTxbFechaInicio_ValidatorCalloutExtender" runat="server" BehaviorID="ocpRevTxbFechaInicio_ValidatorCalloutExtender" TargetControlID="ocpRevTxbFechaInicio">
                                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                                <asp:Label ID="ocpLblVFechaInicio" runat="server" CssClass="labelForm" Visible="false"></asp:Label>
                                                            </td>
                                                            <td></td>
                                                            <td class="left">
                                                                <asp:Label ID="ocpLblFechaFin" runat="server" CssClass="labelBoldForm" Text="Fecha Fin:"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="ocpTxbFechaFin" runat="server" AutoCompleteType="Disabled" class="textBoxBlueForm date noPaste" contentEditable="false"></asp:TextBox>
                                                                <ajaxToolkit:CalendarExtender ID="ocpTxbFechaFin_CalendarExtender" runat="server" BehaviorID="ocpTxbFechaFin_CalendarExtender" Format="dd/MM/yyyy" TargetControlID="ocpTxbFechaFin"></ajaxToolkit:CalendarExtender>
                                                                <asp:RegularExpressionValidator ID="ocpRevTxbFechaFin" runat="server" ControlToValidate="ocpTxbFechaFin" ErrorMessage="RequiredFieldValidator" ValidationExpression="^(([0-2][0-9]|(3)[0-1])(\/)(((0)[0-9])|((1)[0-2]))(\/)\d{4})?$" ValidationGroup="ocpGrpGuardar" Display="None"></asp:RegularExpressionValidator>
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ocpRevTxbFechaFin_ValidatorCalloutExtender" runat="server" BehaviorID="ocpRevTxbFechaFin_ValidatorCalloutExtender" TargetControlID="ocpRevTxbFechaFin">
                                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                                <asp:Label ID="ocpLblVFechaFin" runat="server" CssClass="labelForm" Visible="false"></asp:Label>
                                                            </td>
                                                            <td></td>
                                                            <td class="left">
                                                                <asp:Label ID="ocpLblRazonFin" runat="server" CssClass="labelBoldForm" Text="Razón Terminación"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ocpDdlRazonFin" runat="server" class="comboBoxBlueForm"></asp:DropDownList>
                                                                <asp:Label ID="ocpLblVRazonFin" runat="server" CssClass="labelForm" Visible="false"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="left" rowspan="2">
                                                                <asp:Label ID="ocpLblIngresos" runat="server" CssClass="labelBoldForm" Text="Ingresos Mensuales (Q):"></asp:Label>
                                                            </td>
                                                            <td rowspan="2">
                                                                <asp:TextBox ID="ocpTxbIngresos" runat="server" AutoCompleteType="Disabled" class="textBoxBlueForm num2" onkeypress='return esDigito(event)' onkeyup='this.value=retornaSoloDigitos(this.value)'></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="ocpRevTxbIngresos" runat="server" ControlToValidate="ocpTxbIngresos" ErrorMessage="RequiredFieldValidator" ValidationGroup="ocpGrpGuardar" Display="none"></asp:RequiredFieldValidator>
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ocpRevTxbIngresos_ValidatorCalloutExtender" runat="server" BehaviorID="ocpRevTxbIngresos_ValidatorCalloutExtender" TargetControlID="ocpRevTxbIngresos">
                                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                                <asp:Label ID="ocpLblVIngresos" runat="server" CssClass="labelForm" Visible="false"></asp:Label>
                                                                <asp:HiddenField ID="ocpHdnIngresos" runat="server" />
                                                            </td>
                                                            <td></td>
                                                            <td colspan="2">
                                                                <asp:CheckBox ID="ocpChkTieneConstancia" runat="server" />
                                                                <asp:Label ID="ocpLblTieneConstancia" runat="server" CssClass="labelBoldForm" Text="Tiene Constancia Laboral"></asp:Label>
                                                            </td>
                                                            <td></td>
                                                            <td colspan="2">
                                                                <asp:CheckBox ID="ocpChkTieneIGGS" runat="server" />
                                                                <asp:Label ID="ocpLblTieneIGGS" runat="server" CssClass="labelBoldForm" Text="Tiene Afiliación con el IGGS"></asp:Label>
                                                            </td>
                                                            <%--<td class="left">
                                                            <asp:Label ID="ocpLblAporte" runat="server" Text="Aporte Mensuales (Q):"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="ocpTxbAporte" runat="server" AutoCompleteType="Disabled" class="textBoxForm num2" onkeypress='return esDigito(event)' onkeyup='this.value=retornaSoloDigitos(this.value)'></asp:TextBox>
                                                            <asp:Label ID="ocpLblVAporte" runat="server" CssClass="labelForm" Visible="false"></asp:Label>
                                                        </td>--%>
                                                        </tr>
                                                        <tr>
                                                            <td></td>
                                                            <td class="left">
                                                                <asp:Label ID="ocpLblIngresoSemanal" runat="server" CssClass="labelBoldForm" Text="Ingreso Semanal (Q):" Visible="false"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="ocpTxbIngresoSemanal" runat="server" AutoCompleteType="Disabled" Visible="false" class="textBoxBlueForm num2" onkeypress='return esDigito(event)' onkeyup='this.value=retornaSoloDigitos(this.value); calcularIngresoMensual()'></asp:TextBox>
                                                                <asp:Label ID="ocpLblVIngresoSemanal" runat="server" CssClass="labelForm" Visible="false"></asp:Label>
                                                            </td>
                                                            <td></td>
                                                            <td class="left">
                                                                <asp:Label ID="ocpLblNumSemanas" runat="server" CssClass="labelBoldForm" Text="No. Semanas al Mes:" Visible="false"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="ocpTxbNumSemanas" runat="server" AutoCompleteType="Disabled" Visible="false" class="textBoxBlueForm num3" onkeypress='return esDigito(event)' onkeyup='this.value=retornaSoloDigitos(this.value); calcularIngresoMensual()'></asp:TextBox>
                                                                <asp:Label ID="ocpLblVNumSemanas" runat="server" CssClass="labelForm" Visible="false"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="left">
                                                                <asp:Label ID="ocpLblLugarTrb" runat="server" CssClass="labelBoldForm" Text="Lugar de Trabajo:"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="ocpTxbLugarTrb" runat="server" AutoCompleteType="Disabled" class="textBoxBlueForm"></asp:TextBox>
                                                                <asp:Label ID="ocpLblVLugarTrb" runat="server" CssClass="labelForm" Visible="false"></asp:Label>
                                                            </td>
                                                            <td></td>
                                                            <td class="left">
                                                                <asp:Label ID="ocpLblJornada" runat="server" CssClass="labelBoldForm" Text="Jornada:"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ocpDdlJornada" runat="server" class="comboBoxBlueForm"></asp:DropDownList>
                                                                <asp:Label ID="ocpLblVJornada" runat="server" CssClass="labelForm" Visible="false"></asp:Label>
                                                            </td>
                                                            <td></td>
                                                            <td class="left">
                                                                <asp:Label ID="ocpLblHorasSem" runat="server" CssClass="labelBoldForm" Text="Horas Semanales"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="ocpTxbHorasSem" runat="server" AutoCompleteType="Disabled" class="textBoxBlueForm num3" MaxLength="2" onkeypress='return esDigito(event)' onkeyup='this.value=retornaSoloDigitos(this.value)'></asp:TextBox>
                                                                <asp:Label ID="ocpLblVHorasSem" runat="server" CssClass="labelForm" Visible="false"></asp:Label>
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td style="text-align: center" colspan="8">
                                                                <asp:Button ID="ocpBtnGuardarOcupacion" runat="server" Text="Guardar" OnClick="ocpBtnGuardarOcupacion_Click" class="butonForm" ValidationGroup="ocpGrpGuardar" />
                                                                <asp:Button ID="ocpBtnEliminarOcupacion" runat="server" Text="Eliminar" Visible="false" class="butonFormSec" OnClick="ocpBtnEliminarOcupacion_Click" />
                                                                <asp:Button ID="ocpBtnInsertarOcupacion" runat="server" Text="Nueva Ocupación" Visible="false" OnClick="ocpBtnInsertarOcupacion_Click" class="butonFormRet" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <div style="height: 25px"></div>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:Panel runat="server" ID="ocpPnlOcupaciones" CssClass="scroll-Y">
                                        <table class="tableContInfo tblCenter gray">
                                            <tr>
                                                <th>
                                                    <asp:Label ID="ocpLblOcupacionesMiembro" runat="server"></asp:Label>
                                                </th>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:GridView ID="ocpGdvOcupaciones" CssClass="tableContInfo tblCenter pink" runat="server" AutoGenerateColumns="false" OnRowCommand="ocpGdvOcupaciones_RowCommand" OnRowDataBound="ocpGdvOcupaciones_RowDataBound" ShowFooter="true">
                                                        <Columns>
                                                            <asp:BoundField DataField="CreationDateTime" />
                                                            <asp:BoundField DataField="Code" />
                                                            <asp:BoundField DataField="StartDate" />
                                                            <asp:BoundField DataField="Des" />
                                                            <asp:BoundField DataField="StartDateU" />
                                                            <asp:BoundField DataField="EndDate" />
                                                            <%--<asp:BoundField DataField="MonthlyIncome" />--%>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <div>
                                                                        <asp:Label ID="lblIngresoMen" runat="server" Text='<%# Eval("MonthlyIncome") %>' />
                                                                    </div>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <div>
                                                                        <asp:Label ID="lblFtrIngresoMen" runat="server" />
                                                                    </div>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="WeeklyIncome" />
                                                            <asp:BoundField DataField="MonthlyContribution" Visible="false" />
                                                            <asp:BoundField DataField="WorkPlace" />
                                                            <asp:BoundField DataField="UserId" />
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:Button ID="ocpBtnSeleccionar" runat="server"
                                                                        CommandName="cmdSeleccionar"
                                                                        CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                                        Text='<%#dic.seleccionar%>' CssClass="butonFormTable" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    <asp:Label runat="server" ID="ocpLblNoTiene" CssClass="labelFormSec"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
            <asp:Panel runat="server" ID="pnlIngresosExtra" Visible="false" class="pestana">
                <div>
                    <div>
                        <table class="tblCenter">
                            <tr>
                                <td>
                                    <asp:Panel runat="server" ID="extPnlRegistro">
                                        <table class="tblCenter">
                                            <tr>
                                                <td>
                                                    <asp:Panel runat="server" ID="extPnlGuardar">
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
                                                                    <asp:Label ID="extLblTipo" runat="server" CssClass="labelBoldForm" Text="Tipo:"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="extDdlTipo" runat="server" class="comboBoxBlueForm"></asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="extRevDdlTipo" runat="server" ControlToValidate="extDdlTipo" ErrorMessage="RequiredFieldValidator" ValidationGroup="extGrpGuardar" Display="none"></asp:RequiredFieldValidator>

                                                                    <ajaxToolkit:ValidatorCalloutExtender ID="extRevDdlTipo_ValidatorCalloutExtender" runat="server" BehaviorID="extRevDdlTipo_ValidatorCalloutExtender" TargetControlID="extRevDdlTipo">
                                                                    </ajaxToolkit:ValidatorCalloutExtender>

                                                                    <asp:Label ID="extLblVTipo" runat="server" CssClass="labelForm" Visible="false"></asp:Label>

                                                                </td>
                                                                <td></td>
                                                                <td class="left">
                                                                    <asp:Label ID="extLblIngresos" runat="server" CssClass="labelBoldForm" Text="Ingresos Mensuales (Q):"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="extTxbIngresos" runat="server" AutoCompleteType="Disabled" class="textBoxBlueForm num2" onkeypress='return esDigito(event)' onkeyup='this.value=retornaSoloDigitos(this.value)'></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="extRevTxbIngresos" runat="server" ControlToValidate="extTxbIngresos" ErrorMessage="RequiredFieldValidator" ValidationGroup="extGrpGuardar" Display="none"></asp:RequiredFieldValidator>
                                                                    <ajaxToolkit:ValidatorCalloutExtender ID="extRevTxbIngresos_ValidatorCalloutExtender" runat="server" BehaviorID="extRevTxbIngresos_ValidatorCalloutExtender" TargetControlID="extRevTxbIngresos">
                                                                    </ajaxToolkit:ValidatorCalloutExtender>
                                                                    <asp:Label ID="extLblVIngresos" runat="server" CssClass="labelForm" Visible="false"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="left">
                                                                    <asp:Label ID="extLblFechaInicio" runat="server" CssClass="labelBoldForm" Text="Fecha Inicio:"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="extTxbFechaInicio" runat="server" AutoCompleteType="Disabled" class="textBoxBlueForm date noPaste" contentEditable="false"></asp:TextBox>
                                                                    <ajaxToolkit:CalendarExtender ID="extTxbFechaInicio_CalendarExtender" runat="server" BehaviorID="extTxbFechaInicio_CalendarExtender" Format="dd/MM/yyyy" TargetControlID="extTxbFechaInicio"></ajaxToolkit:CalendarExtender>
                                                                    <asp:RequiredFieldValidator ID="extRevTxbFechaInicio" runat="server" ControlToValidate="extTxbFechaInicio" ErrorMessage="RequiredFieldValidator" ValidationGroup="extGrpGuardar" Display="None"></asp:RequiredFieldValidator>
                                                                    <ajaxToolkit:ValidatorCalloutExtender ID="extRevTxbFechaInicio_ValidatorCalloutExtender" runat="server" BehaviorID="extRevTxbFechaInicio_ValidatorCalloutExtender" TargetControlID="extRevTxbFechaInicio">
                                                                    </ajaxToolkit:ValidatorCalloutExtender>
                                                                    <asp:Label ID="extLblVFechaInicio" runat="server" CssClass="labelForm" Visible="false"></asp:Label>
                                                                </td>
                                                                <td></td>
                                                                <td class="left">
                                                                    <asp:Label ID="extLblFechaFin" runat="server" CssClass="labelBoldForm" Text="Fecha Fin:"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="extTxbFechaFin" runat="server" AutoCompleteType="Disabled" class="textBoxBlueForm date noPaste" contentEditable="false"></asp:TextBox>
                                                                    <ajaxToolkit:CalendarExtender ID="extTxbFechaFin_CalendarExtender" runat="server" BehaviorID="extTxbFechaFin_CalendarExtender" Format="dd/MM/yyyy" TargetControlID="extTxbFechaFin"></ajaxToolkit:CalendarExtender>
                                                                    <asp:RegularExpressionValidator ID="extRevTxbFechaFin" runat="server" ControlToValidate="extTxbFechaFin" ErrorMessage="RequiredFieldValidator" ValidationExpression="^(([0-2][0-9]|(3)[0-1])(\/)(((0)[0-9])|((1)[0-2]))(\/)\d{4})?$" ValidationGroup="extGrpGuardar" Display="None"></asp:RegularExpressionValidator>
                                                                    <ajaxToolkit:ValidatorCalloutExtender ID="extRevTxbFechaFin_ValidatorCalloutExtender" runat="server" BehaviorID="extRevTxbFechaFin_ValidatorCalloutExtender" TargetControlID="extRevTxbFechaFin">
                                                                    </ajaxToolkit:ValidatorCalloutExtender>
                                                                    <asp:Label ID="extLblVFechaFin" runat="server" CssClass="labelForm" Visible="false"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <%--<tr>
                                                            <td class="left">
                                                                <asp:Label ID="extLblRazonFin" runat="server" Text="Razón Terminación"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="extDdlRazonFin" runat="server" class="comboBoxForm"></asp:DropDownList>
                                                                <asp:Label ID="extLblVRazonFin" runat="server" CssClass="labelForm" Visible="false"></asp:Label>
                                                            </td>
                                                        </tr>--%>
                                                            <tr>
                                                                <td class="left">
                                                                    <asp:Label ID="extLblNotas" runat="server" CssClass="labelBoldForm" Text="Notas:"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="extTxbNotas" runat="server" AutoCompleteType="Disabled" class="textBoxBlueForm"></asp:TextBox>
                                                                    <asp:Label ID="extLblVNotas" runat="server" CssClass="labelForm" Visible="false"></asp:Label>
                                                                </td>
                                                                <td></td>
                                                                <td style="text-align: center" colspan="2">
                                                                    <asp:Button ID="extBtnGuardarIngreso" runat="server" Text="Guardar" class="butonForm" ValidationGroup="extGrpGuardar" OnClick="extBtnGuardarIngreso_Click" />
                                                                    <asp:Button ID="extBtnEliminarIngreso" runat="server" Text="Eliminar" Visible="false" class="butonFormSec" OnClick="extBtnEliminarIngreso_Click" />
                                                                    <asp:Button ID="extBtnInsertarIngreso" runat="server" Text="Nuevo Ingreso" Visible="false" class="butonFormRet" OnClick="extBtnInsertarIngreso_Click" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <div style="height: 25px"></div>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:Panel runat="server" ID="extPnlIngresos" CssClass="scroll-Y">
                                            <table class="tableContInfo tblCenter gray">
                                                <tr>
                                                    <th>
                                                        <asp:Label ID="extLblHistorialIngresos" runat="server">Historial</asp:Label>
                                                    </th>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:GridView ID="extGdvIngresos" ShowFooter="true" CssClass="tableContInfo tblCenter pink" runat="server" AutoGenerateColumns="false" OnRowCommand="extGdvIngresos_RowCommand" OnRowDataBound="extGdvIngresos_RowDataBound">
                                                            <Columns>
                                                                <asp:BoundField DataField="CreationDateTime" />
                                                                <asp:BoundField DataField="Code" />
                                                                <asp:BoundField DataField="StartDate" />
                                                                <asp:BoundField DataField="EndDate" HtmlEncode="false" />
                                                                <asp:BoundField DataField="MonthlyIncome" HtmlEncode="false" />
                                                                <asp:BoundField DataField="Notes" HtmlEncode="false" />
                                                                <asp:BoundField DataField="Des" />
                                                                <asp:BoundField DataField="StartDateU" />
                                                                <asp:BoundField DataField="EndDateU" />
                                                                <%--<asp:BoundField DataField="MonthlyIncome" />--%>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <div>
                                                                            <asp:Label ID="lblIngreso" runat="server" Text='<%# Eval("MonthlyIncome") %>' />
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <div>
                                                                            <asp:Label ID="lblFtrIngreso" runat="server" />
                                                                        </div>
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="Notes" />
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
                                                            <%--                                                       <FooterStyle BackColor="#323233" ForeColor="Black" HorizontalAlign="Left" />--%>
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
            <%--<a href="#" class="cabecera" onclick="mostrarPestana(3);">Calisto</a>--%>
            <asp:Panel runat="server" ID="pnlPosesiones" Visible="false" class="pestana">
                <div>
                    <table class="tblCenter">
                        <tr>
                            <td style="text-align: center">
                                <asp:Button ID="pssBtnGuardar" runat="server" Text="Guardar" OnClick="pssBtnGuardar_Click" class="butonForm" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div style="display: flex">
                                    <asp:GridView runat="server" CssClass="tableCont miniTbl yellow" ID="pssGdvPosesiones1" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:BoundField DataField="Possession" />
                                            <asp:BoundField DataField="Des" ItemStyle-CssClass="left" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblVCantidad" ItemStyle-CssClass="labelForm" Text='<%# Eval("Quantity").ToString()%>' Visible="false"></asp:Label>
                                                    <asp:TextBox ID="txbCantidad" runat="server"
                                                        Text='<%# Eval("Quantity") %>' CssClass="" onkeypress='return esDigito(event)' onkeyup='this.value=retornaSoloDigitos(this.value)' MaxLength="2" />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                </EditItemTemplate>
                                                <ControlStyle Width="30px"></ControlStyle>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <div style="width: 10px"></div>
                                    <asp:GridView runat="server" CssClass="tableCont miniTbl yellow" ID="pssGdvPosesiones2" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:BoundField DataField="Possession" />
                                            <asp:BoundField DataField="Des" ItemStyle-CssClass="left" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblVCantidad" ItemStyle-CssClass="labelForm" Text='<%# Eval("Quantity").ToString()%>' Visible="false"></asp:Label>
                                                    <asp:TextBox ID="txbCantidad" runat="server"
                                                        Text='<%# Eval("Quantity") %>' CssClass="" onkeypress='return esDigito(event)' onkeyup='this.value=retornaSoloDigitos(this.value)' MaxLength="2" />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                </EditItemTemplate>
                                                <ControlStyle Width="30px"></ControlStyle>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <div style="width: 10px"></div>
                                    <asp:GridView runat="server" CssClass="tableCont miniTbl yellow" ID="pssGdvPosesiones3" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:BoundField DataField="Possession" />
                                            <asp:BoundField DataField="Des" ItemStyle-CssClass="left" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblVCantidad" ItemStyle-CssClass="labelForm" Text='<%# Eval("Quantity").ToString()%>' Visible="false"></asp:Label>
                                                    <asp:TextBox ID="txbCantidad" runat="server"
                                                        Text='<%# Eval("Quantity") %>' CssClass="" onkeypress='return esDigito(event)' onkeyup='this.value=retornaSoloDigitos(this.value)' MaxLength="2" />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                </EditItemTemplate>
                                                <ControlStyle Width="30px"></ControlStyle>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <div style="width: 10px"></div>
                                    <asp:GridView runat="server" CssClass="tableCont miniTbl yellow" ID="pssGdvPosesiones4" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:BoundField DataField="Possession" />
                                            <asp:BoundField DataField="Des" ItemStyle-CssClass="left" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblVCantidad" ItemStyle-CssClass="labelForm" Text='<%# Eval("Quantity").ToString()%>' Visible="false"></asp:Label>
                                                    <asp:TextBox ID="txbCantidad" runat="server"
                                                        Text='<%# Eval("Quantity") %>' CssClass="" onkeypress='return esDigito(event)' onkeyup='this.value=retornaSoloDigitos(this.value)' MaxLength="2" />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                </EditItemTemplate>
                                                <ControlStyle Width="30px"></ControlStyle>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <div style="width: 10px"></div>
                                    <asp:GridView runat="server" CssClass="tableCont miniTbl yellow" ID="pssGdvPosesiones5" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:BoundField DataField="Possession" />
                                            <asp:BoundField DataField="Des" ItemStyle-CssClass="left" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblVCantidad" ItemStyle-CssClass="labelForm" Text='<%# Eval("Quantity").ToString()%>' Visible="false"></asp:Label>
                                                    <asp:TextBox ID="txbCantidad" runat="server"
                                                        Text='<%# Eval("Quantity") %>' CssClass="" onkeypress='return esDigito(event)' onkeyup='this.value=retornaSoloDigitos(this.value)' MaxLength="2" />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                </EditItemTemplate>
                                                <ControlStyle Width="30px"></ControlStyle>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <div style="width: 10px"></div>
                                    <asp:GridView runat="server" CssClass="tableCont miniTbl yellow" ID="pssGdvPosesiones6" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:BoundField DataField="Possession" />
                                            <asp:BoundField DataField="Des" ItemStyle-CssClass="left" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblVCantidad" ItemStyle-CssClass="labelForm" Text='<%# Eval("Quantity").ToString()%>' Visible="false"></asp:Label>
                                                    <asp:TextBox ID="txbCantidad" runat="server"
                                                        Text='<%# Eval("Quantity") %>' CssClass="" onkeypress='return esDigito(event)' onkeyup='this.value=retornaSoloDigitos(this.value)' MaxLength="2" />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                </EditItemTemplate>
                                                <ControlStyle Width="30px"></ControlStyle>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <div style="width: 10px"></div>
                                    <asp:GridView runat="server" CssClass="tableCont miniTbl yellow" ID="pssGdvPosesiones7" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:BoundField DataField="Possession" />
                                            <asp:BoundField DataField="Des" ItemStyle-CssClass="left" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblVCantidad" ItemStyle-CssClass="labelForm" Text='<%# Eval("Quantity").ToString()%>' Visible="false"></asp:Label>
                                                    <asp:TextBox ID="txbCantidad" runat="server"
                                                        Text='<%# Eval("Quantity") %>' CssClass="" onkeypress='return esDigito(event)' onkeyup='this.value=retornaSoloDigitos(this.value)' MaxLength="2" />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                </EditItemTemplate>
                                                <ControlStyle Width="30px"></ControlStyle>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
        </div>
    </div>
    <script>
        function calcularIngresoMensual() {
            var txbIngresoSemanal = document.getElementById('<%=ocpTxbIngresoSemanal.ClientID %>');
            var txbNumSemanas = document.getElementById('<%=ocpTxbNumSemanas.ClientID%>');
            var lblIngresoMensual = document.getElementById('<%=ocpLblVIngresos.ClientID%>');
            var hdnIngresoMensual = document.getElementById('<%=ocpHdnIngresos.ClientID%>');
            var ingresoMensual = txbIngresoSemanal.value * txbNumSemanas.value;
            hdnIngresoMensual.value = ingresoMensual;
            lblIngresoMensual.innerText = String.fromCharCode(160) + ingresoMensual;
            txbIngresoMensual.value = ingresoMensual;

        }
    </script>
</asp:Content>
