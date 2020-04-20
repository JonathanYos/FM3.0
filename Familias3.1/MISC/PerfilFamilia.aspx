<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/mast.Master" CodeBehind="PerfilFamilia.aspx.cs" Inherits="Familias3._1.MISC.PerfilFamilia" %>

<%@ MasterType VirtualPath="~/mast.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="formContGlobal">
        <table class="mainTable">
            <tr>
                <th></th>
                <th style="width: 50px;"></th>
                <th></th>
                <th style="width: 50px;"></th>
                <th></th>
            </tr>
            <tr>
                <td style="display: flex">
                    <table class="tableCont tableContInfo genTbl tblCenter blue">
                        <tr>
                            <th colspan="5">
                                <asp:Label ID="lblGeneralInfo" runat="server"></asp:Label>
                            </th>
                        </tr>
                        <tr>
                            <td class="left">
                                <asp:Label ID="lblDirec" runat="server" Text="Direccion:"></asp:Label>
                            </td>
                            <td colspan="4">
                                <asp:Label ID="lblVDirec" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="left">
                                <asp:Label ID="lblPueblo" runat="server" Text="Pueblo:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblVPueblo" runat="server"></asp:Label>
                            </td>
                            <td></td>
                            <td class="left">
                                <asp:Label ID="lblArea" runat="server" Text="Area:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblVArea" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="left">
                                <asp:Label ID="lblPhone" runat="server" Text="Phone:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblVPhone" runat="server"></asp:Label>
                            </td>
                            <td></td>
                            <td class="left">
                                <asp:Label ID="lblEtnia" runat="server" Text="Etnia:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblVEtnia" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                <td></td>
                <td style="display: flex">
                    <asp:Table runat="server" ID="tblAfil" class="tableCont tableContInfo genTbl tblCenter pink">
                        <asp:TableRow>
                            <asp:TableHeaderCell ColumnSpan="5" CssClass="tblhead">
                                <asp:Label ID="lblAfil" runat="server"></asp:Label>
                            </asp:TableHeaderCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell CssClass="left">
                                <asp:Label ID="lblTS" runat="server" Text="TS:"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Label ID="lblVTS" runat="server"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell CssClass="left">
                                <asp:Label ID="lblAfilStatus" runat="server" Text="Afil Estado:"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Label ID="lblVAfilStatus" runat="server"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell CssClass="left">
                                <asp:Label ID="lblClasif" runat="server" Text="Cladificacion Afil:"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Label ID="lblVClasif" runat="server"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </td>
                <td></td>
                <td rowspan="3" class="tdAlignY">
                    <asp:Panel runat="server" ID="pnlAvisos" CssClass="block scroll-X tabla3">
                        <asp:GridView runat="server" ID="gdvAvisos" CssClass="tableContInfo miniTbl tblCenter red" AutoGenerateColumns="false">
                            <Columns>
                                <asp:BoundField DataField="Aviso" />
                                <%--<asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblCondicion" Text='<%# Eval("Aviso").ToString()%>' CssClass="labelFormSec"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                </td>
            </tr>
            <tr style="height: 20px">
                <td></td>
                <td></td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Panel runat="server" ID="pnlActiv" class="tableCont mediumTbl">
                        <table class="tableCont tableContInfo mediumTbl tblCenter orange">
                            <tr>
                                <th class="tblhead">
                                    <asp:Label ID="lblMiembros" runat="server"></asp:Label>
                                    <asp:Button ID="btnInact" class="butonFormSec btnTable" runat="server" Text="Mostrar inactivos" OnClick="btnInact_Click" />
                                </th>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="gdvMiembrosAct" Width="100%" CssClass="tableCont bigTbl tblCenter orange" runat="server" AutoGenerateColumns="false" OnRowCommand="gdvMiembrosAct_RowCommand">
                                        <Columns>
                                            <asp:BoundField DataField="MemberId" Visible="false" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnMName" runat="server"
                                                        CommandName="cmdMName"
                                                        CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                        Text='<%# Eval("MemberId") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Nombre" />
                                            <asp:BoundField DataField="Relacion" />
                                            <asp:BoundField DataField="BirthDate" />
                                            <asp:BoundField DataField="AfilStatus" />
                                            <asp:BoundField DataField="TipoAfil" />
                                            <asp:BoundField DataField="OtraAfil" />
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pnlInac" runat="server" class="tableCont mediumTbl" Visible="false">
                        <table class="tableCont tableContInfo bigTbl tblCenter orange">
                            <tr>
                                <th>
                                    <asp:Label ID="lblMiembros2" runat="server"></asp:Label>
                                    <asp:Button ID="btnAct" class="butonFormSec btnTable purple" runat="server" Text="Mostrar solo inactivos" OnClick="btnAct_Click" />
                                </th>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="gdvMiembrosInact" CssClass="tableCont mediumTbl tblCenter orange" runat="server" AutoGenerateColumns="false" OnRowCommand="gdvMiembrosInact_RowCommand">
                                        <Columns>
                                            <asp:BoundField DataField="MemberId" Visible="false" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnMName" runat="server"
                                                        CommandName="cmdMName"
                                                        CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                        Text='<%# Eval("MemberId") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Nombre" />
                                            <asp:BoundField DataField="Relacion" />
                                            <asp:BoundField DataField="BirthDate" />
                                            <asp:BoundField DataField="AfilStatus" />
                                            <asp:BoundField DataField="TipoAfil" />
                                            <asp:BoundField DataField="InacRazon" />
                                            <asp:BoundField DataField="InacFecha" />
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
                <td></td>
                <td></td>
            </tr>
        </table>
    </div>
    <script>
        $(document).ready(function () {
            $('.btnTable').parent().css({
                "width": "10%"
            })
        });
    </script>
</asp:Content>
