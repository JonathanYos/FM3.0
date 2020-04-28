<%@ Page Title="" Language="C#" MasterPageFile="~/mast.Master" AutoEventWireup="true" CodeBehind="ResumenBecas.aspx.cs" Inherits="Familias3._1.EDUC.ResumenBecas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        * {
            margin: 0;
            padding: 0;
        }

        .contenedortotal {
            margin-top: 10px;
            display: grid;
            height: 200px;
            grid-template-columns: 1fr 1fr 1fr;
            grid-template-rows: 250px 600px;
            grid-gap: 10px;
        }

        .gr {
            background: #fff;
        }

        .informacionyavisos {
            grid-column: span 3;
            display: flex;
            justify-content: center;
        }

        .df {
            background: rgba(0,0,0,.2);
        }

        .tam200 {
            width: 200px;
        }

        .izq {
            text-align: left;
        }

        .avisos {
            margin-left: 10px;
            /* display:flex;
            justify-content:center;*/
        }

        .mg10 {
            margin-left: 10px;
        }

        .w100 {
            width: 100%;
        }
    </style>
    <asp:Panel ID="pnltodo" runat="server" class="contenedortotal">


        <div class="informacionyavisos gr">
            <div class="informacion df">
                <table>
                    <tr>
                        <td rowspan="8" class="tdimage">
                            <img class="mg10 imgapadfoto" id="imgApadFoto" runat="server" src="#" />
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblnombre" CssClass="labelBoldForm mg10" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblanioesc" CssClass="labelBoldForm mg10" runat="server" Text="Label"></asp:Label></td>
                        <td>
                            <asp:Label ID="lblVanio" runat="server" CssClass="labelBlackForm"> </asp:Label></td>
                        <td>
                            <asp:Label ID="lblsemaforo" CssClass="labelBoldForm mg10" runat="server" Text="Label"></asp:Label>
                            <asp:Panel runat="server" class="pnlVSemaforo" ForeColor="White" ID="pnlVSemaforo">
                                <asp:Label runat="server" ID="lblVSemaforo" Style="top: -5px" Font-Size="10px"></asp:Label>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td class="izq">
                            <asp:Label ID="lblgrado" CssClass="labelForm mg10" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlgrado" CssClass="comboBoxBlueForm tam200" runat="server"></asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="lbldesafiliacion" CssClass="labelForm mg10" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="izq">
                            <asp:Label ID="lblescuela" CssClass="labelForm mg10" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlescuela" CssClass="comboBoxBlueForm tam200" runat="server"></asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="lblestadoafil" CssClass="labelForm mg10" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="izq">
                            <asp:Label ID="lblestado" CssClass="labelForm mg10" runat="server" Text="Label"></asp:Label></td>
                        <td>
                            <asp:DropDownList ID="ddlestado" CssClass="comboBoxBlueForm tam200" runat="server"></asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="lbltipoafil" CssClass="labelForm mg10" runat="server" Text="Label"></asp:Label>

                        </td>
                    </tr>
                    <tr>
                        <td class="izq">
                            <asp:Label ID="lblexcestado" CssClass="labelForm mg10" runat="server" Text="Label"></asp:Label></td>
                        <td>
                            <asp:DropDownList ID="ddlexestado" CssClass="comboBoxBlueForm tam200" runat="server"></asp:DropDownList>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="izq">
                            <asp:Label ID="lblcarrera" CssClass="labelForm mg10" runat="server" Text="Label"></asp:Label></td>
                        <td>
                            <asp:DropDownList ID="ddlcarrera" CssClass="comboBoxBlueForm tam200" runat="server"></asp:DropDownList>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblporcentaje" CssClass="labelForm mg10" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblVporcentaje" CssClass="labelForm" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblclasificacion" CssClass="labelForm mg10" runat="server" Text="Label"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="CreationDateL" runat="server" Visible="false"></asp:Label></td>
                        <td></td>
                        <td></td>
                        <td>
                            <asp:CheckBox ID="chktienecertificado" CssClass="labelForm mg10" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center;" colspan="4">
                            <asp:Button ID="btnactualizarinfo" CssClass="butonForm" runat="server" Text="Button" OnClick="btnactualizarinfo_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="avisos df">
                <asp:GridView runat="server" ID="gdvAvisos" CssClass="tb-avisos tableCont resumen red" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="Aviso" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div class="observaciones gr">
            <table style="width: 100%;">
                <tr>
                    <td>
                        <asp:Label ID="lbltituloobs" runat="server" Text="Label"></asp:Label></td>
                </tr>
                <tr>
                    <td>
                        <div style="width: 100%; height: 400px; overflow-x: auto; display: flex; justify-content: center;">
                            <asp:GridView ID="gvhistoriaobs" CssClass="tableCont" runat="server">
                                <Columns>
                                    <asp:BoundField DataField="Categoria" />
                                </Columns>
                            </asp:GridView>            
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table style="width: 100%;">
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="lblnuevaobs" CssClass="labelForm" runat="server" Text="Label"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:DropDownList ID="ddltipoobs" CssClass="comboBoxForm w100" runat="server"></asp:DropDownList></td>
                                <td style="width: 60px;">
                                    <asp:Button ID="btnguardarobs" runat="server" CssClass="butonForm" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:TextBox ID="txtnotasobs" CssClass="textBoxForm w100" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <div class="rembolsos gr">
            <asp:Button ID="btntoposrem" CssClass="butonForm" runat="server" Text="Button" OnClick="btntoposrem_Click" />
            <asp:Button ID="btndetallesrem" CssClass="butonFormSec" runat="server" Text="Button" OnClick="btndetallesrem_Click" />
            <asp:Button ID="btnautorizarem" CssClass="butonFormRet" runat="server" Text="Button" />
            <div style="width: 100%; height: 400px; overflow-x: auto; display: flex; justify-content: center;">
                <asp:GridView ID="gvrem" CssClass="tableCont" runat="server"></asp:GridView>
            </div>

        </div>
        <div class="calificaciones gr">
            <table style="width: 100%;">
                <tr>
                    <td>
                        <asp:DropDownList ID="ddlcategoriascal" CssClass="comboBoxForm w100" runat="server"></asp:DropDownList>
                    </td>
                    <td style="width: 200px;">
                        <asp:Button ID="btnvercali" CssClass="butonForm" runat="server" Text="Button" OnClick="btnvercali_Click" />
                    </td>
                </tr>
            </table>
            <div style="width: 100%; height: 400px; overflow-x: auto; display: flex; justify-content: center;">
                <asp:GridView ID="gvhistotirialcal" CssClass="tableCont" runat="server"></asp:GridView>
            </div>
            <table style="width: 100%;">
                <tr>
                    <td>
                        <asp:DropDownList ID="ddltipocal" CssClass="comboBoxForm w100" runat="server"></asp:DropDownList>
                    </td>
                    <td style="width: 200px;">
                        <asp:Button ID="btnguardarcal" CssClass="butonForm" runat="server" Text="Button" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:TextBox ID="txtnotascal" CssClass="textBoxForm w100" runat="server"></asp:TextBox></td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <script>
        $(document).ready(function () {
            $('.pnlVSemaforo').css({
                "float": "left",
                "border": "1px solid gray",
                "margin-top": "3px",
                "margin-left": "5px",
                "border-bottom-left-radius": "10px",
                "border-bottom-right-radius": "10px",
                "border-top-left-radius": "10px",
                "border-top-right-radius": "10px",
                "width": "50px",
                "height": "10px"
            });
            $('#ContentPlaceHolder1_lblsemaforo').css({
                "float": "left"
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
