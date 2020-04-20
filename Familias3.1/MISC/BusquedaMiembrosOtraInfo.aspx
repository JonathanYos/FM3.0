<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/mast.Master" CodeBehind="BusquedaMiembrosOtraInfo.aspx.cs" Inherits="Familias3._1.MISC.BusquedaMiembrosOtraInfo" %>

<%@ MasterType VirtualPath="~/mast.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <asp:Panel ID="pnlBusquedaMiembrosOtraInfo" runat="server" class="formContGlobal">
            <div class="formContTrans">
                <table class="tblCenter">
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td class="left">
                                        <asp:Label ID="lblNombres" runat="server" class="labelBoldForm" Text="Label"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txbNombres" AutoCompleteType="Disabled" class="textBoxBlueForm" runat="server" onkeypress='return esLetraBusqueda(event)' onkeyup='this.value=retornaSoloLetrasBusqueda(this.value)'></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="left">
                                        <asp:Label ID="lblApell" runat="server" class="labelBoldForm" Text="Label"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txbApell" AutoCompleteType="Disabled" class="textBoxBlueForm" runat="server" onkeypress='return esLetraBusqueda(event)' onkeyup='this.value=retornaSoloLetrasBusqueda(this.value)'></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="left">
                                        <asp:Label ID="lblNombreUsual" runat="server" class="labelBoldForm" Text="Label"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txbNombreUsual" AutoCompleteType="Disabled" class="textBoxBlueForm" runat="server" onkeypress='return esLetraBusqueda(event)' onkeyup='this.value=retornaSoloLetrasBusqueda(this.value)'></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="left">
                                        <asp:Label ID="lblNacim" runat="server" class="labelBoldForm" Text="Label"></asp:Label>
                                    </td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txbDia" AutoCompleteType="Disabled" MaxLength="2" class="textBoxBlueForm num3" runat="server"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlMes" class="comboBoxBlueForm" runat="server"></asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txbAño" AutoCompleteType="Disabled" class="textBoxBlueForm num3" MaxLength="4" runat="server" onkeypress='return esDigito(event)' onkeyup='this.value=retornaSoloDigitos(this.value)'></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="left">
                                        <asp:Label ID="lblArea" runat="server" class="labelBoldForm" Text="Label"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlArea" class="comboBoxBlueForm" runat="server"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="left">
                                        <asp:Label ID="lblTS" runat="server" class="labelBoldForm" Text="Label"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlTS" class="comboBoxBlueForm" runat="server"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="left">
                                        <asp:Label ID="lblTipoAfil" runat="server" class="labelBoldForm" Text="Label"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlTipoAfil" class="comboBoxBlueForm" runat="server"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="butonForm" OnClick="btnBuscar_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 50px"></td>
                        <td>
                            <asp:Table runat="server" ID="tblFiltrosAfil" Style="display: flex">
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:CheckBox ID="chkApad" runat="server" Checked="true" />
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="lblApad" runat="server" class="labelBoldForm" Text="Label"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:CheckBox ID="chkAfil" runat="server" Checked="true" />
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="lblAfil" runat="server" class="labelBoldForm" Text="Label"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:CheckBox ID="chkOtros" runat="server" />
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="lblOtros" runat="server" class="labelBoldForm" Text="Label"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:CheckBox ID="chkDesaf" runat="server" />
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="lblDesaf" runat="server" class="labelBoldForm" Text="Label"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:CheckBox ID="chkParienD" runat="server" />
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="lblParienD" runat="server" class="labelBoldForm" Text="Label"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:CheckBox ID="chkGrad" runat="server" />
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="lblGrad" runat="server" class="labelBoldForm" Text="Label"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:CheckBox ID="chkParienG" runat="server" />
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="lblParienG" runat="server" class="labelBoldForm" Text="Label"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell colspan="2">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="rdbIncluirInfoEduc" runat="server" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblIncluirInfoEduc" runat="server" CssClass="labelBoldForm" Text="Incluir Info Escolar"></asp:Label>
                                                </td>
                                                <td style="width: 20px"></td>
                                                <td>
                                                    <asp:Label ID="lblAñoEcolar" runat="server" CssClass="labelBoldForm" Text="Año Escolar:"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txbAñoEscolar" AutoCompleteType="Disabled" runat="server" class="textBoxBlueForm num3" MaxLength="4" onkeypress='return esDigito(event)' onkeyup='this.value=retornaSoloDigitos(this.value)'></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                            <asp:Table runat="server" ID="tblFiltrosEmp" Visible="false" Style="display: flex">
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:CheckBox ID="chkEmp" runat="server" Checked="true" />
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="lblEmp" runat="server" class="labelBoldForm" Text="Label"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:CheckBox ID="chkFamEmp" runat="server" Checked="false" />
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="lblFamEmp" runat="server" class="labelBoldForm" Text="Label"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="position: absolute; left: 5px; bottom: 15px">
                <asp:Label runat="server" ID="lblBuscar" CssClass="labelFormBig" ForeColor="White"></asp:Label>
                <asp:HyperLink ID="lnkBuscarPorNumero" CssClass="labelFormBig" ForeColor="White" runat="server" href="../MISC/Buscar.aspx">Buscar por Número</asp:HyperLink>
                <asp:Label runat="server" ID="lblComa" class="labelFormBig" ForeColor="White">,&nbsp;</asp:Label>
                <asp:HyperLink ID="lnkBuscarMiembrosInfoEduc" CssClass="labelFormBig" ForeColor="White" runat="server" href="../MISC/BusquedaMiembrosInfoEduc.aspx">Buscar Miembro Info. Educativa</asp:HyperLink>
                <asp:Label runat="server" ID="lblO" class="labelFormBig" ForeColor="White">,&nbsp;</asp:Label>
                <asp:HyperLink ID="lnkBuscarFamilias" CssClass="labelFormBig" ForeColor="White" runat="server" href="../MISC/BusquedaFamilias.aspx">Buscar Familia</asp:HyperLink>
                <span class="labelFormBig" style="color:white">.</span>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlMostrar" runat="server" Style="height: 80vh" Visible=" false" CssClass="formContGlobalReport">
            <div style="text-align: center; padding: 10px">
                <asp:Button ID="btnNuevaBusqueda" runat="server" Text="Nueva Búsqueda" CssClass="butonFormRet" OnClick="btnNuevaBusqueda_Click" />
                <asp:Button ID="btnCopiar" runat="server" Text="Copiar" Visible="false" CssClass="butonFormSec" OnClick="btnCopiar_Click" />
                <%-- <input id="btnCopiar" type="button" class="butonFormSec" onclick="CopyGridView()" />--%>
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label runat="server" ID="lblTotal" CssClass="labelFormBig">Total:</asp:Label>
            </div>
            <div id="divGdv" class="scroll-Y" style="height: 77vh">
                <asp:GridView ID="gdvMiembrosOtraInfo" class="tableCont tblCenter" runat="server" AutoGenerateColumns="false" OnRowCommand="gdvMiembrosOtraInfo_OnRowCommand">
                    <Columns>
                        <asp:BoundField DataField="MemberId" />
                        <asp:BoundField DataField="FamilyId" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkMemberId" runat="server"
                                    CommandName="cmdMemberId"
                                    CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                    Text='<%# Eval("MemberId") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkFamilyId" runat="server"
                                    CommandName="cmdFamilyId"
                                    CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                    Text='<%# S + Eval("FamilyId") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Nombres" />
                        <asp:BoundField DataField="Apellidos" />
                        <asp:BoundField DataField="NombreUsual" />
                        <asp:BoundField DataField="FechaNacimiento" />
                        <asp:BoundField DataField="Genero" />
                        <asp:BoundField DataField="AfilTipo" />
                        <asp:BoundField DataField="TipoMiembro" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Panel ID="pnlSemaforo" runat="server" CssClass="pnlVSemaforo" BackColor='<%# (Eval("Semaforo").ToString()=="Rojo") ? colorMalo :  (Eval("Semaforo").ToString()=="Amarillo") ? colorRegular : colorBueno%>'></asp:Panel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Clasificacion" />
                        <asp:BoundField DataField="Area" />
                        <asp:BoundField DataField="Pueblo" />
                        <asp:BoundField DataField="TS" />
                        <asp:BoundField DataField="Direccion" />
                        <asp:BoundField DataField="Region" />
                        <asp:BoundField DataField="AñoEstadoAfil" />
                    </Columns>
                </asp:GridView>
                <asp:GridView ID="gdvMiembrosOtraInfoEmp" class="tableCont tblCenter" runat="server" AutoGenerateColumns="false" OnRowCommand="gdvMiembrosOtraInfoEmp_OnRowCommand">
                    <Columns>
                        <asp:BoundField DataField="MemberId" />
                        <asp:BoundField DataField="FamilyId" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkMemberId" runat="server"
                                    CommandName="cmdMemberId"
                                    CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                    Text='<%# Eval("MemberId") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkFamilyId" runat="server"
                                    CommandName="cmdFamilyId"
                                    CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                    Text='<%# S + Eval("FamilyId") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Nombres" />
                        <asp:BoundField DataField="Apellidos" />
                        <asp:BoundField DataField="NombreUsual" />
                        <asp:BoundField DataField="FechaNacimiento" />
                        <asp:BoundField DataField="Genero" />
                        <asp:BoundField DataField="Direccion" />
                    </Columns>
                </asp:GridView>
                <asp:GridView ID="gdvMiembrosOtraInfoEduc" class="tableCont tblCenter" runat="server" AutoGenerateColumns="false" OnRowCommand="gdvMiembrosOtraInfoEduc_OnRowCommand">
                    <Columns>
                        <asp:BoundField DataField="MemberId" />
                        <asp:BoundField DataField="FamilyId" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server"
                                    CommandName="cmdMemberId"
                                    CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                    Text='<%# Eval("MemberId") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton2" runat="server"
                                    CommandName="cmdFamilyId"
                                    CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                    Text='<%# S + Eval("FamilyId") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Nombres" />
                        <asp:BoundField DataField="Apellidos" />
                        <asp:BoundField DataField="NombreUsual" />
                        <asp:BoundField DataField="FechaNacimiento" />
                        <asp:BoundField DataField="Genero" />
                        <asp:BoundField DataField="AfilTipo" />
                        <asp:BoundField DataField="TipoMiembro" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Panel ID="Panel1" runat="server" CssClass="pnlVSemaforo" BackColor='<%# (Eval("Semaforo").ToString()=="Rojo") ? colorMalo :  (Eval("Semaforo").ToString()=="Amarillo") ? colorRegular : colorBueno%>'></asp:Panel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Clasificacion" />
                        <asp:BoundField DataField="Area" />
                        <asp:BoundField DataField="Pueblo" />
                        <asp:BoundField DataField="TS" />
                        <asp:BoundField DataField="Direccion" />
                        <asp:BoundField DataField="Region" />
                        <asp:BoundField DataField="AñoEstadoAfil" />
                        <asp:BoundField DataField="Año" />
                        <asp:BoundField DataField="Grado" />
                        <asp:BoundField DataField="EstadoEducativo" />
                        <asp:BoundField DataField="NivelEducativo" />
                        <asp:BoundField DataField="CentroEducativo" />
                        <asp:BoundField DataField="CarreraEducativa" />
                        <asp:BoundField DataField="ExcepcionEstadoEduc" />
                    </Columns>
                </asp:GridView>
            </div>
        </asp:Panel>
    </div>
    <script>
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
    </script>
    <script type="text/javascript">
        function CopyGridView() {
            var div = document.getElementById('divGdv');
            div.contentEditable = 'true';
            var controlRange;
            if (document.body.createControlRange) {
                controlRange = document.body.createControlRange();
                controlRange.addElement(div);
                controlRange.execCommand('Copy');
            }
            div.contentEditable = 'false';
        }
    </script>
</asp:Content>
