<%@ Page Title="" Language="C#" MasterPageFile="~/mast.Master" AutoEventWireup="true" CodeBehind="HistorialEducati.aspx.cs" Inherits="Familias3._1.EDUC.HistorialEducati" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .informacionyavisos {
            grid-column: span 3;
            display: flex;
            justify-content: center;
        }

        .avisos {
            margin-left: 10px;
            /* display:flex;
            justify-content:center;*/
        }

        .disno {
            display: none;
        }

        .tam200 {
            width: 200px;
        }
    </style>
    <asp:Panel ID="pnltodo" runat="server" CssClass="formContGlobal">
        <table runat="server" id="tbContendor" style="width: 100%;">
            <tr>
                <td>
                    <div style="width: 100%; float: right;">
                        <asp:Button ID="btnregresar" class="butonFormRet" runat="server" Text="Button" OnClick="btnregresar_Click" />
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvhistorial" CssClass="tableCont" runat="server" AutoGenerateColumns="false" OnRowCommand="gvhistorial_RowCommand" OnRowDataBound="gvhistorial_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="Crea" ItemStyle-CssClass="disno" HeaderStyle-CssClass="disno" />
                            <asp:BoundField DataField="Año" />
                            <asp:BoundField DataField="Grado" />
                            <asp:BoundField DataField="Estado" />
                            <asp:BoundField DataField="ExcepcionEstado" />
                            <asp:BoundField DataField="Escuela" />
                            <asp:BoundField DataField="Carrera" />
                            <asp:BoundField DataField="Notas" />
                            <asp:BoundField DataField="Usuario" />
                            <asp:TemplateField HeaderText="Accion/Action" Visible="True">
                                <ItemTemplate>
                                    <asp:Button ID="btnver" CommandName="ver" CssClass="butonForm floatl" Text="Referir/Refer" runat="server" />
                                    <asp:Button ID="btneliminar" runat="server" CommandName="del" CssClass="butonFormSec" Text="Historial/History" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td style="display: flex; justify-content: center;">
                    <div class="informacion df">
                        <table runat="server" id="tbinfo">
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
                        <asp:GridView runat="server" ID="gdvAvisos" CssClass="tb-avisos tableCont resumen red" AutoGenerateColumns="false" OnRowDataBound="gdvAvisos_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Accion/Action" Visible="True">
                                    <ItemTemplate>
                                        <asp:Button ID="btnelimin" CssClass="butonForm floatl" Text="Referir/Refer" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div style="display: flex; justify-content: center;">
                        <div style="margin-right: 10px;">
                            <asp:GridView ID="gvutiles" CssClass="tableCont" runat="server" OnRowCommand="gvutiles_RowCommand">
                                <Columns>
                                    <asp:ButtonField ButtonType="Button" CommandName="borrar" ControlStyle-CssClass="butonForm" Text="Borrar/Delete" />
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div>
                            <table runat="server" id="tbcheques">
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkrembolsos" CssClass="labelForm" runat="server" AutoPostBack="True" OnCheckedChanged="chkrembolsos_CheckedChanged" /></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkactividades" CssClass="labelForm" runat="server" AutoPostBack="True" OnCheckedChanged="chkactividades_CheckedChanged" /></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkobservaciones" CssClass="labelForm" runat="server" AutoPostBack="True" OnCheckedChanged="chkobservaciones_CheckedChanged" /></td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
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
