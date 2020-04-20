<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/mast.Master" CodeBehind="BusquedaMiembrosInfoEduc.aspx.cs" Inherits="Familias3._1.MISC.BusquedaMiembroInfoEduc" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<%@ MasterType VirtualPath="~/mast.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <asp:Panel ID="pnlBusquedaMiembrosInfoEduc" runat="server" CssClass="formContGlobal">
            <div class="formContTrans">
                <table class="tblCenter">
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td class="left">
                                        <asp:Label ID="lblAño" runat="server" class="labelBoldForm" Text="Label"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txbAño" runat="server" class="textBoxBlueForm num3" MaxLength="4" onkeypress='return esDigito(event)' onkeyup='this.value=retornaSoloDigitos(this.value)'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="refTxbAño" runat="server" ErrorMessage="RequiredFieldValidator" Display="None" ValidationGroup="grpBuscar" ControlToValidate="txbAño"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="refTxbAño_ValidatorCalloutExtender" runat="server" BehaviorID="refTxbAño_ValidatorCalloutExtender" TargetControlID="refTxbAño">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="left">
                                        <asp:Label ID="lblEstadoEduc" runat="server" class="labelBoldForm" Text="Label"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlEstadoEduc" class="comboBoxBlueForm" runat="server"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="left">
                                        <asp:Label ID="lblExcEstadoEduc" runat="server" class="labelBoldForm" Text="Label"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlExcEstdoEduc" class="comboBoxBlueForm" runat="server"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="left">
                                        <asp:Label ID="lblGrado" runat="server" class="labelBoldForm" Text="Label"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlGrado" class="comboBoxBlueForm" runat="server"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="left">
                                        <asp:Label ID="lblNivelEduc" runat="server" class="labelBoldForm" Text="Label"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlNivelEduc" class="comboBoxBlueForm" runat="server"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="left">
                                        <asp:Label ID="lblCarrera" runat="server" class="labelBoldForm" Text="Label"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlCarrera" class="comboBoxBlueForm" runat="server"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="left">
                                        <asp:Label ID="lblCentroEduc" runat="server" class="labelBoldForm" Text="Label"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlCentroEduc" class="comboBoxBlueForm" runat="server"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="left">
                                        <asp:Label ID="lblTipoEscuela" runat="server" class="labelBoldForm" Text="Label"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlTipoEscuela" class="comboBoxBlueForm" runat="server"></asp:DropDownList>
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
                                    <td class="left">
                                        <asp:Label ID="lblMaestro" runat="server" Visible="false" class="labelBoldForm" Text="Label"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlMaestro" class="comboBoxBlueForm" Visible="false" runat="server"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="left">
                                        <asp:Label ID="lblPueblo" runat="server" class="labelBoldForm" Text="Label"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlPueblo" class="comboBoxBlueForm" runat="server"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" ValidationGroup="grpBuscar" CssClass="butonForm" OnClick="btnBuscar_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 50px"></td>
                        <td>
                            <table style="display: flex">
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkApad" runat="server" Checked="true" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblApad" runat="server" class="labelBoldForm" Text="Label"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkDesaf" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblDesaf" runat="server" class="labelBoldForm" Text="Label"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkGrad" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblGrad" runat="server" class="labelBoldForm" Text="Label"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="position: absolute; left: 5px; bottom: 15px">
                <asp:Label runat="server" ID="lblBuscar" CssClass="labelFormBig" ForeColor="White"></asp:Label>
                <asp:HyperLink ID="lnkBuscarPorNumero" CssClass="labelFormBig" ForeColor="White" runat="server" href="../MISC/Buscar.aspx">Buscar por Número</asp:HyperLink>
                <span class="labelFormBig" style="color:white">,&nbsp;</span>
                <asp:HyperLink ID="lnkBuscarMiembrosOtraInfo" CssClass="labelFormBig" ForeColor="White" runat="server" href="../MISC/BusquedaMiembrosOtraInfo.aspx">Buscar Miembro por Otra Info.</asp:HyperLink>
                <asp:Label runat="server" ID="lblO" class="labelFormBig" ForeColor="White">,&nbsp;</asp:Label>
                <asp:HyperLink ID="lnkBuscarFamilias" CssClass="labelFormBig" ForeColor="White" runat="server" href="../MISC/BusquedaFamilias.aspx">Buscar Familia</asp:HyperLink>
                <span class="labelFormBig" style="color:white">.</span>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlMostrar" runat="server" Visible=" false" CssClass="formContGlobalReport">
            <div style="text-align: center; padding: 10px">
                <asp:Button ID="btnNuevaBusqueda" runat="server" Text="Nueva Búsqueda" CssClass="butonFormRet" OnClick="btnNuevaBusqueda_Click" />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label runat="server" ID="lblTotal" CssClass="labelFormBig">Total:</asp:Label>
            </div>
            <div class="scroll-Y" style="height: 77vh">
                <asp:GridView ID="gdvMiembrosInfoEduc" class="tableCont tblCenter" runat="server" AutoGenerateColumns="false" OnRowCommand="gdvMiembrosInfoEduc_OnRowCommand">
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
</asp:Content>
