<%@ Page Language="C#" Async="true" AutoEventWireup="true" MasterPageFile="~/mast.Master" CodeBehind="ReportesYEstadisticas.aspx.cs" Inherits="Familias3._1.TS.ReporteActividadesFamiliares" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel runat="server" ID="pnlContenedor" CssClass="contenedor formContGlobal">
        <div style="text-align: center">
            <asp:LinkButton ID="lnkReporte" CssClass="cabecera c-activa" runat="server" OnClick="lnkReporte_Click">Informe de Visitas</asp:LinkButton>
            <asp:LinkButton ID="lnkEstadisticas" CssClass="cabecera" runat="server" OnClick="lnkEstadisticas_Click">Estádisticas de Visitas</asp:LinkButton>
            <asp:LinkButton ID="lnkFamiliasTS" CssClass="cabecera" runat="server" OnClick="lnkFamiliasTS_Click">Familias por Área y Trabajador Social</asp:LinkButton>
        </div>
        <asp:Panel runat="server" ID="pnlReporte">
            <div>
                <asp:Panel ID="pnlBusquedaFamilia" runat="server" CssClass="formContTrans">
                    <table style="margin: 5px auto;" class="tblCenter">
                        <tr>
                            <td>
                                <asp:Label runat="server" ID="lblArea" CssClass="labelBoldForm">Area</asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlArea" CssClass="comboBoxBlueForm"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label runat="server" ID="lblTS" CssClass="labelBoldForm">Trabajador Social</asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlTS" CssClass="comboBoxBlueForm"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label runat="server" ID="lblTipoVisita" CssClass="labelBoldForm">Tipo de Visita</asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlTipoVisita" CssClass="comboBoxBlueForm"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label runat="server" ID="lblObjetivo" CssClass="labelBoldForm">Objetivo</asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlObjetivo" CssClass="comboBoxBlueForm"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblFecha" class="labelBoldForm" runat="server" Text="Fecha Inicio"></asp:Label>
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txbDiaFecha" runat="server" AutoCompleteType="Disabled" CssClass="textBoxBlueForm num3" MaxLength="2" onkeypress='return esDigito(event)' onkeyup='this.value=retornaSoloDigitos(this.value)'></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlMesFecha" runat="server" CssClass="comboBoxBlueForm">
                                                <asp:ListItem Text="January" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="February" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="March" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="April" Value="4"></asp:ListItem>
                                                <asp:ListItem Text="May" Value="5"></asp:ListItem>
                                                <asp:ListItem Text="Juny" Value="6"></asp:ListItem>
                                                <asp:ListItem Text="July" Value="7"></asp:ListItem>
                                                <asp:ListItem Text="August" Value="8"></asp:ListItem>
                                                <asp:ListItem Text="September" Value="9"></asp:ListItem>
                                                <asp:ListItem Text="October" Value="10"></asp:ListItem>
                                                <asp:ListItem Text="November" Value="11"></asp:ListItem>
                                                <asp:ListItem Text="December" Value="12"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txbAñoFecha" AutoCompleteType="Disabled" class="textBoxBlueForm num3" runat="server" MaxLength="4" onkeypress='return esDigito(event)' onkeyup='this.value=retornaSoloDigitos(this.value)'></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Button runat="server" ID="btnBuscar" Visible="false" CssClass="butonForm" OnClick="btnBuscar_Click" Text="Buscar Todas" />
                                        </td>
                                        <td>
                                            <asp:Button runat="server" ID="btnRealizado" CssClass="butonForm" OnClick="btnBuscarRealizado_Click" Text="Buscar las Visitadas" />
                                        </td>
                                        <td>
                                            <asp:Button runat="server" ID="btnNecesita" CssClass="butonForm" OnClick="btnBuscarNecesita_Click" Text="Buscar las que Necesitan" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel runat="server" ID="pnlMostrar" Visible="false">
                    <div style="text-align: center; padding: 10px">
                        <asp:Button ID="btnNuevaBusqueda" runat="server" Text="Nueva Búsqueda" CssClass="butonFormRet" OnClick="btnNuevaBusqueda_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label runat="server" ID="lblTotal" CssClass="labelFormBig">Total:</asp:Label>
                    </div>
                    <div class="scroll-Y" style="height: 75vh">
                        <asp:GridView runat="server" ID="gdvFamilias" AutoGenerateColumns="false" CssClass="tableContInfo tblCenter" OnRowCommand="gdvFamilias_OnRowCommand">
                            <Columns>
                                <asp:BoundField DataField="FamilyId" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkFamilyId" runat="server"
                                            CommandName="cmdFamilyId"
                                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                            Text='<%# S + Eval("FamilyId") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Direccion" HeaderText="Dirección" />
                                <asp:BoundField DataField="Area" HeaderText="Area" />
                                <asp:BoundField DataField="JefeCasa" HeaderText="Jefe Casa" />
                                <asp:BoundField DataField="ApadrinadosFase2" HeaderText="No. Apad. Fase II" />
                                <asp:BoundField DataField="fechaUltimaVisita" HeaderText="Fecha Última Visita" />
                                <asp:BoundField DataField="TipoVisita" HeaderText="Tipo Visita" />
                                <asp:BoundField DataField="TS" HeaderText="Trabajador Social" />
                                <asp:BoundField DataField="Enfoques" HeaderText="Enfoques" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </asp:Panel>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlEstadisticas" Visible="false">
            <div>
                <div style="height: 5px"></div>
                <table class="tblCenter">
                    <tr>
                        <td>
                            <asp:Label ID="lblFechaInicio" class="labelBoldForm" runat="server" Text="Fecha Inicio"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txbDiaFechaInicio" runat="server" AutoCompleteType="Disabled" CssClass="textBoxBlueForm num3" MaxLength="2" onkeypress='return esDigito(event)' onkeyup='this.value=retornaSoloDigitos(this.value)'></asp:TextBox>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlMesFechaInicio" runat="server" CssClass="comboBoxBlueForm">
                                <asp:ListItem Text="January" Value="1"></asp:ListItem>
                                <asp:ListItem Text="February" Value="2"></asp:ListItem>
                                <asp:ListItem Text="March" Value="3"></asp:ListItem>
                                <asp:ListItem Text="April" Value="4"></asp:ListItem>
                                <asp:ListItem Text="May" Value="5"></asp:ListItem>
                                <asp:ListItem Text="Juny" Value="6"></asp:ListItem>
                                <asp:ListItem Text="July" Value="7"></asp:ListItem>
                                <asp:ListItem Text="August" Value="8"></asp:ListItem>
                                <asp:ListItem Text="September" Value="9"></asp:ListItem>
                                <asp:ListItem Text="October" Value="10"></asp:ListItem>
                                <asp:ListItem Text="November" Value="11"></asp:ListItem>
                                <asp:ListItem Text="December" Value="12"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txbAñoFechaInicio" AutoCompleteType="Disabled" class="textBoxBlueForm num3" runat="server" MaxLength="4" onkeypress='return esDigito(event)' onkeyup='this.value=retornaSoloDigitos(this.value)'></asp:TextBox>
                        </td>
                        <td style="width: 20px"></td>
                        <td>
                            <asp:Label ID="lblFechaFinal" class="labelBoldForm" runat="server" Text="Fecha Fin"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txbDiaFechaFinal" AutoCompleteType="Disabled" class="textBoxBlueForm num3" runat="server" MaxLength="2" onkeypress='return esDigito(event)' onkeyup='this.value=retornaSoloDigitos(this.value)'></asp:TextBox>
                        </td>
                        <td>
                            <!--<asp:TextBox ID="txtMesFechaFinal" runat="server" TextMode="Number"  onkeyup="this.value=numbers(this.value)" MaxLength="2"></asp:TextBox>-->
                            <asp:DropDownList ID="ddlMesFechaFinal" runat="server" class="comboBoxBlueForm">
                                <asp:ListItem Text="January" Value="1"></asp:ListItem>
                                <asp:ListItem Text="February" Value="2"></asp:ListItem>
                                <asp:ListItem Text="March" Value="3"></asp:ListItem>
                                <asp:ListItem Text="April" Value="4"></asp:ListItem>
                                <asp:ListItem Text="May" Value="5"></asp:ListItem>
                                <asp:ListItem Text="Juny" Value="6"></asp:ListItem>
                                <asp:ListItem Text="July" Value="7"></asp:ListItem>
                                <asp:ListItem Text="August" Value="8"></asp:ListItem>
                                <asp:ListItem Text="September" Value="9"></asp:ListItem>
                                <asp:ListItem Text="October" Value="10"></asp:ListItem>
                                <asp:ListItem Text="November" Value="11"></asp:ListItem>
                                <asp:ListItem Text="December" Value="12"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txbAñoFechaFinal" AutoCompleteType="Disabled" class="textBoxBlueForm num3" runat="server" MaxLength="4" onkeypress='return esDigito(event)' onkeyup='this.value=retornaSoloDigitos(this.value)'></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btnGenerar" runat="server" CssClass="butonForm" Text="Generar" OnClick="btnGenerar_Click" />
                        </td>
                    </tr>
                </table>
                <div style="height: 20px"></div>
                <asp:Table runat="server" ID="tblEstadisticas" CssClass="tableContInfo tblCenter"></asp:Table>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlFamiliasTS" Visible="false">
            <div>
                <div style="height: 5px"></div>
                <asp:Table ID="tblFamilias" runat="server" CssClass="tableCont tblCenter">
                    <asp:TableRow>
                    </asp:TableRow>
                </asp:Table>
            </div>
        </asp:Panel>
    </asp:Panel>
</asp:Content>
