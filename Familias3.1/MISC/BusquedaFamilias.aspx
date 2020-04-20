<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/mast.Master" CodeBehind="BusquedaFamilias.aspx.cs" Inherits="Familias3._1.MISC.BusquedaFamilias" %>

<%@ MasterType VirtualPath="~/mast.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <asp:Panel ID="pnlBusquedaFamilia" runat="server" CssClass="formContGlobal">
            <div class="formContTrans">
                <table class="tblCenter">
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td class="left">
                                        <asp:Label ID="lblTS" runat="server" class="labelBoldForm" Text="Label"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlTS" class="comboBoxBlueForm num" runat="server"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="left">
                                        <asp:Label ID="lblEstadoAfil" runat="server" class="labelBoldForm" Text="Label"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlEstadoAfil" class="comboBoxBlueForm" runat="server"></asp:DropDownList>
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
                                        <asp:Label ID="lblRegion" runat="server" class="labelBoldForm" Text="Label"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlRegion" class="comboBoxBlueForm" runat="server"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="left">
                                        <asp:Label ID="lblDireccion" runat="server" class="labelBoldForm" Text="Label"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txbDireccion" AutoCompleteType="Disabled" class="textBoxBlueForm" runat="server"></asp:TextBox>
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
                    </tr>
                </table>
            </div>
            <div style="position: absolute; left: 5px; bottom: 15px">
                <asp:Label runat="server" ID="lblBuscar" CssClass="labelFormBig" ForeColor="White"></asp:Label>
                <asp:HyperLink ID="lnkBuscarPorNumero" CssClass="labelFormBig" ForeColor="White" runat="server" href="../MISC/Buscar.aspx">Buscar por Número</asp:HyperLink>
                <span class="labelFormBig" style="color:white">,&nbsp;</span>
                <asp:HyperLink ID="lnkBuscarMiembrosOtraInfo" CssClass="labelFormBig" ForeColor="White" runat="server" href="../MISC/BusquedaMiembrosOtraInfo.aspx">Buscar Miembro por Otra Info.</asp:HyperLink>
                <asp:Label runat="server" ID="lblO" class="labelFormBig" ForeColor="White">,&nbsp;</asp:Label>
                <asp:HyperLink ID="lnkBuscarMiembrosInfoEduc" CssClass="labelFormBig" ForeColor="White" runat="server" href="../MISC/BusquedaMiembrosInfoEduc.aspx">Buscar Miembro Info. Educativa</asp:HyperLink>
                <span class="labelFormBig" style="color:white">.</span>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlMostrar" runat="server" Visible=" false" CssClass="formContGlobalReport">

            <div style="text-align: center; padding: 10px">
                <asp:Button ID="btnNuevaBusqueda" runat="server" Text="Nueva Búsqueda" CssClass="butonFormRet" OnClick="btnNuevaBusqueda_Click" />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label runat="server" ID="lblTotal" CssClass="labelFormBig">Total:</asp:Label>
            </div>
            <asp:Panel runat="server" ID="pnlFamilias" CssClass="scroll-Y" Style="height: 80vh">
                <asp:GridView ID="gdvFamilias" class="tableCont tblCenter" runat="server" AutoGenerateColumns="false" OnRowCommand="gdvFamilias_OnRowCommand">
                    <Columns>
                        <asp:BoundField DataField="FamilyId" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkFamilyId" runat="server"
                                    CommandName="cmdFamilyId"
                                    CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                    Text='<%# Eval("Familia") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="JefeCasa" />
                        <asp:BoundField DataField="Direccion" />
                        <asp:BoundField DataField="Area" />
                        <asp:BoundField DataField="TS" />
                        <asp:BoundField DataField="Clasificacion" />
                        <asp:BoundField DataField="EstadoAfiliacion" />
                    </Columns>
                </asp:GridView>
            </asp:Panel>
        </asp:Panel>
    </div>
</asp:Content>
