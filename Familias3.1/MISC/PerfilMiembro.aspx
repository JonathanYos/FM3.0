<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/mast.Master" CodeBehind="PerfilMiembro.aspx.cs" Inherits="Familias3._1.MISC.PerfilMiembro" %>

<%@ MasterType VirtualPath="~/mast.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .d {
            display: block;
        }

        .todo {
            width: 100%;
        }

        .espacio {
            margin-left: 10px;
            margin-top: 5px;
            display: inline;
            float: left;
        }

        .mg {
            margin-top: 6px;
            margin-left: 10px;
        }
    </style>
    <div class="formContGlobal">
        <table class="mainTable todo">
            <tr>
                <th></th>
                <th></th>
                <th></th>
                <th style="width: 10px"></th>

            </tr>
            <tr>
                <td class="ppru">
                    <table id="Table1" class="tableContInfo genTbl blue espacio" runat="server">
                        <tr>
                            <th colspan="6">
                                <asp:Label runat="server" ID="lblGeneralInfo"></asp:Label>
                            </th>
                        </tr>
                        <%--
                    <tr>
                        <th></th>
                        <th></th>
                        <th style="width:15px"></th>
                        <th></th>
                        <th></th>
                        <th style="width:15px"></th>
                        <th></th>
                        <th></th>
                    </tr>--%>
                        <tr>
                            <td class="left">
                                <asp:Label runat="server" ID="lblNombres"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblVNombres"></asp:Label>
                            </td>
                            <td class="left">
                                <asp:Label runat="server" ID="lblApellidos"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblVApellidos"></asp:Label>
                            </td>
                            <td class="left">
                                <asp:Label runat="server" ID="lblNUsual"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblVNUsual"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="left">
                                <asp:Label runat="server" ID="lblBirth"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblVBirth"></asp:Label>
                            </td>
                            <td class="left">
                                <asp:Label runat="server" ID="lblPhone"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblVPhone"></asp:Label>
                            </td>
                            <td class="left">
                                <asp:Label runat="server" ID="lblDPI"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblVDPI"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="left">
                                <asp:Label runat="server" ID="lblOtraAFil"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblVOtraAfil"></asp:Label>
                            </td>
                            <td class="left">
                                <asp:Label runat="server" ID="lblLee"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblVLee"></asp:Label>
                            </td>
                            <td class="left">
                                <asp:Label runat="server" ID="lblGenero"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblVGenero"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                <td rowspan="8" style="vertical-align: top;">
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
            <tr>
                <td style="height: 15px" class="ppru"></td>
            </tr>
            <tr>
                <td class="ppru">
                    <asp:Table runat="server" ID="tblEduc" class="tableContInfo secTbl yellow espacio">
                        <asp:TableRow>
                            <asp:TableHeaderCell ColumnSpan="4">
                                <asp:Label runat="server" ID="lblEducacion"></asp:Label>
                            </asp:TableHeaderCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell CssClass="left">
                                <asp:Label runat="server" ID="lblGrado"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Label runat="server" ID="lblVGrado"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell CssClass="left">
                                <asp:Label runat="server" ID="lblSemaforo"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <%--<asp:Label runat=BRa"server" ID="lblVSemaforo" Width="100px"></asp:Label>--%>
                                <asp:Panel runat="server" class="pnlVSemaforo" ForeColor="White" ID="pnlVSemaforo">
                                    <asp:Label runat="server" ID="lblVSemaforo" Style="top: -5px" Font-Size="10px"></asp:Label>
                                </asp:Panel>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </td>
            </tr>
            <tr>
                <td style="height: 15px" class="ppru"></td>
            </tr>
            <tr>
                <td class="d" class="ppru">
                    <asp:Table runat="server" ID="tblBenef" class="tableContInfo secTbl espacio orange">
                        <asp:TableRow>
                            <asp:TableHeaderCell runat="server" class="tblhead yellow">
                                <asp:Label ID="lblBenef" runat="server"></asp:Label>
                            </asp:TableHeaderCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="lblNoDerechos" runat="server" CssClass="labelFormSec" Text="No tiene derechos"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell runat="server" ID="cellSalud">
                                <asp:Label ID="lblSalud" runat="server"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell runat="server" ID="cellVvnd">
                                <asp:Label ID="lblVvnd" runat="server"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell runat="server" ID="cellEduc">
                                <asp:Label ID="lblEduc" runat="server"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>

                    <table runat="server" id="tblAfiliacion" class="tableContInfo secTbl espacio  pink">
                        <tr>
                            <th colspan="2" style="height: 22px">
                                <asp:Label runat="server" ID="lblAfil"></asp:Label>
                            </th>
                        </tr>
                        <tr>
                            <td class="left">
                                <asp:Label runat="server" ID="lblTAfil"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblVTAfil"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="left">
                                <asp:Label runat="server" ID="lblAfilStatus"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblVAfilStatus"></asp:Label>
                            </td>
                        </tr>
                    </table>

                    <asp:Panel runat="server" ID="pnlPadrinos">
                        <table class="tableContInfo mediumTbl green espacio">
                            <tr>
                                <th>
                                    <asp:Label ID="lblPadrinos" runat="server"></asp:Label>
                                </th>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="gdvPadrinos" CssClass="tableCont mediumTbl green" runat="server" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:BoundField DataField="SponsorId" Visible="false" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnSName" runat="server"
                                                        CommandName="cmdSName" Enabled="false"
                                                        CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                        Text='<%# Eval("SponsorId") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="SponsorNames" />
                                        </Columns>
                                    </asp:GridView>
                                    <asp:Label runat="server" ID="lblNoPadrinos" CssClass="labelFormSec" Visible="false"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td style="height: 15px" class="ppru"></td>
            </tr>
            <tr>
                <td class="ppru">
                    <asp:Panel runat="server" ID="pnlPadres">
                        <table class="tableContInfo mediumTbl  purple espacio">
                            <tr>
                                <th>
                                    <asp:Label runat="server" ID="lblPadres"></asp:Label>
                                </th>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="gdvPadres" CssClass="tableCont mediumTbl purple" runat="server" AutoGenerateColumns="false" OnRowCommand="gdvPadres_RowCommand">
                                        <Columns>
                                            <asp:BoundField DataField="MemberId" Visible="false" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnPName" runat="server"
                                                        CommandName="cmdPName"
                                                        CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                        Text='<%# Eval("MemberId") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="MemberNames" />
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
    <script>
        $(document).ready(function () {
            $('.secTbl Table').eq(1).css({
                "width": "1000px"
            });
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
            //$('.tblAvisos').css({
            //    "min-width": "250px",
            //    "margin-left":"10px"
            //})
            //$('table').css({
            //    "border" : "solid black 1px"
            //});
            //$('td').css({
            //    "border": "solid black 1px"
            //});
        });
    </script>
</asp:Content>
