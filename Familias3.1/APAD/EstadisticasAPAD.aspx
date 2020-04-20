<%@ Page Title="" Language="C#" MasterPageFile="~/mast.Master" AutoEventWireup="true" CodeBehind="EstadisticasAPAD.aspx.cs" Inherits="Familias3._1.Apadrinamiento.ReporteCartasAPAD" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="formContGlobal">
        <style>
            .tableCont {
                margin-bottom: 10px;
            }

            .auto-style1 {
                height: 19px;
            }
        </style>
        <asp:Panel ID="pnldatos" runat="server">
            <table class="tableCont cntrar2" id="ingreso" runat="server">
                <tr>
                    <td style="height: 27px">
                        <asp:Label ID="lbldefecha" runat="server" Text=""></asp:Label>
                    </td>
                    <td style="height: 27px">
                        <asp:TextBox ID="txtdefecha" runat="server" contentEditable="false"></asp:TextBox>

                        <ajaxToolkit:CalendarExtender ID="txtdefecha_CalendarExtender" runat="server" BehaviorID="txtdefecha_CalendarExtender" TargetControlID="txtdefecha" />

                    </td>

                    <td>
                        <asp:Label ID="lblafecha" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txbafecha" runat="server" contentEditable="false"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="txbafecha_CalendarExtender" runat="server" BehaviorID="txbafecha_CalendarExtender" TargetControlID="txbafecha" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblCategoria" runat="server" Text=""></asp:Label></td>
                    <td colspan="2">
                        <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="comboBoxForm combobox2"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Button ID="btnbuscar" runat="server" Text="" CssClass="butonForm" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlinfo" runat="server">
            <table>
                <tr>
                    <td>
                        <table class="tableCont">
                            <tr>
                                <th>
                                    <asp:Label ID="lblfamiliares" runat="server"></asp:Label>
                                </th>
                                <td>
                                    <asp:Label ID="lblVfamiliares" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <asp:Label ID="lblindividuales" runat="server"></asp:Label>
                                </th>
                                <td>
                                    <asp:Label ID="lblVindividuales" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <asp:Label ID="lblfamiliasAfil" runat="server"></asp:Label>
                                </th>
                                <td>
                                    <asp:Label ID="lblVfamiliasAfil" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table class="tableCont">
                            <tr>
                                <th colspan="2">Miembros Afiliados y Niveles de Apadrinamiento</th>
                            </tr>
                            <tr>
                                <th>Nivel de Apadrinamiento</th>
                                <th>Cantidad</th>
                            </tr>
                            <tr>
                                <td>Completo</td>
                                <td>1212</td>
                            </tr>
                            <tr>
                                <td>Ninguno</td>
                                <td>140</td>
                            </tr>
                            <tr>
                                <td>Parcial</td>
                                <td>166</td>
                            </tr>
                            <tr>
                                <td>Tipo de Padrino Viejo</td>
                                <td>0</td>
                            </tr>
                            <tr>
                                <td>Total</td>
                                <td>1518</td>
                            </tr>

                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table class="tableCont">
                            <tr>
                                <th>Miembros Afiliados con Niveles Invalidos de Apadrinamiento
                                </th>
                            </tr>
                            <tr>
                                <th>Número de Miembro
                                </th>
                            </tr>
                            <tr>
                                <td>&nbsp;&nbsp;&nbsp; &nbsp;</td>
                            </tr>
                            <tr>
                                <td>TOTAL: 0
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table class="tableCont">
                            <tr>
                                <th>
                                    <asp:Label ID="lblfamiliaafil" runat="server"></asp:Label>
                                </th>
                                <td>
                                    <asp:Label ID="lblVfamiliaafil" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <asp:Label ID="lblmiembroafil" runat="server"></asp:Label></th>
                                <td>
                                    <asp:Label ID="lblVmiembroafil" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td colspan="2">&nbsp;&nbsp;&nbsp; &nbsp;</td>
                            </tr>
                            <tr>
                                <th>
                                    <asp:Label ID="lblfamiliadesa" runat="server"></asp:Label></th>
                                <td>
                                    <asp:Label ID="lblVfamiliadesa" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <th>
                                    <asp:Label ID="lblmiembrodesa" runat="server"></asp:Label></th>
                                <td>
                                    <asp:Label ID="lblVmiembrodesa" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td colspan="2">&nbsp;&nbsp;&nbsp; &nbsp;</td>
                            </tr>
                            <tr>
                                <th>
                                    <asp:Label ID="lblfamiliagrad" runat="server"></asp:Label></th>
                                <td>
                                    <asp:Label ID="lblVfamiliagrad" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <th>
                                    <asp:Label ID="lblmiembrosgrad" runat="server"></asp:Label></th>
                                <td>
                                    <asp:Label ID="lblVmiembrosgrad" runat="server"></asp:Label></td>
                            </tr>

                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table class="tableCont">
                            <tr>
                                <th colspan="5">
                                    <asp:Label ID="lbltitcartaescrita" runat="server"></asp:Label></th>
                            </tr>
                            <tr>
                                <th>Cartas Escritas</th>
                                <th>Total de Afiliados</th>
                                <th>Porcentaje de Total de Afiliados</th>
                                <th>Total de Cartas</th>
                                <th>Porcentaje Total de Cartas</th>
                            </tr>
                            <tr>
                                <td>1</td>
                                <td>8</td>
                                <td>42</td>
                                <td>8</td>
                                <td>26</td>
                            </tr>
                            <tr>
                                <td>2</td>
                                <td>11</td>
                                <td>57</td>
                                <td>22</td>
                                <td>73</td>
                            </tr>
                            <tr>
                                <td>Total</td>
                                <td>19</td>
                                <td>99</td>
                                <td>30</td>
                                <td>99</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table class="tableCont">
                            <tr>
                                <th>
                                    <asp:Label ID="lblcartassn" runat="server"></asp:Label></th>
                                <td>
                                    <asp:Label ID="lblVcartassn" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
                            </tr>
                            <tr>
                                <th>
                                    <asp:Label ID="lblfotostom" runat="server"></asp:Label></th>
                                <td>
                                    <asp:Label ID="lblVfotostom" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
                            </tr>
                            <tr>
                                <th colspan="2">
                                    <asp:Label ID="lbltitregalosentre" runat="server"></asp:Label></th>
                            </tr>
                            <tr>
                                <td>Regalo de Cumpleaños</td>
                                <td>118</td>
                            </tr>
                            <tr>
                                <td>Regalo de Padrinos</td>
                                <td>20</td>
                            </tr>
                            <tr>
                                <td>Total:</td>
                                <td>138</td>
                            </tr>
                            <tr>
                                <td colspan="2"></td>
                            </tr>
                            <tr>
                                <th>
                                    <asp:Label ID="lblregalopadrino" runat="server"></asp:Label></th>
                                <td>
                                    <asp:Label ID="lblVregalopadrino" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
                            </tr>
                            <tr>
                                <th colspan="2">
                                    <asp:Label ID="lbltitviveres" runat="server"></asp:Label></th>
                            </tr>
                            <tr>
                                <td>Accidente</td>
                                <td>1</td>
                            </tr>
                            <tr>
                                <td>Desempleo</td>
                                <td>2</td>
                            </tr>
                            <tr>
                                <td>Enfermedad</td>
                                <td>32</td>
                            </tr>
                            <tr>
                                <td>Otra</td>
                                <td>2</td>
                            </tr>
                            <tr>
                                <td>Total:</td>
                                <td>37</td>
                            </tr>
                            <tr>
                                <td colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
                            </tr>
                            <tr>
                                <th>
                                    <asp:Label ID="lblcartaspad" runat="server"></asp:Label></th>
                                <td>
                                    <asp:Label ID="lblVcartaspad" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <th>
                                    <asp:Label ID="lblrecicartapad" runat="server"></asp:Label></th>
                                <td>
                                    <asp:Label ID="lblVrecicartapad" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td colspan="2" style="height: 17px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
                            </tr>
                            <tr>
                                <th>
                                    <asp:Label ID="lblvisitapad" runat="server"></asp:Label></th>
                                <td>
                                    <asp:Label ID="lblVvisitapad" runat="server"></asp:Label></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table class="tableCont">
                            <tr>
                                <th colspan="6">
                                    <asp:Label ID="lblprocafilmemb" runat="server"></asp:Label></th>
                            </tr>
                            <tr>
                                <th rowspan="2">Tipo de Afiiacion</th>
                                <th colspan="5">Cartas Escritas por Afiliados</th>
                            </tr>
                            <tr>
                                <th>Aprobada</th>
                                <th>Nesecita Visita</th>
                                <th>Pendiente</th>
                                <th>Rechazada</th>
                                <th>Total</th>
                            </tr>
                            <tr>
                                <td>Fase I</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>0</td>
                            </tr>
                            <tr>
                                <td>Fase II</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>0</td>
                            </tr>
                            <tr>
                                <td>Fase III</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>0</td>
                            </tr>
                            <tr>
                                <td>Total</td>
                                <td>0</td>
                                <td>0</td>
                                <td>0</td>
                                <td>0</td>
                                <td>0</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table class="tableCont">
                            <tr>
                                <th colspan="5">
                                    <asp:Label ID="lblprocdesamemb" runat="server"></asp:Label></th>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="GridView1" CssClass="tableCont" runat="server"></asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table class="tableCont">
                            <tr>
                                <th>
                                    <asp:Label ID="lblprocgradmemb" runat="server"></asp:Label></th>
                            </tr>
                            <tr>

                                <td>
                                    <asp:GridView ID="gvgrad" CssClass="tableCont" AutoGenerateColumns="false" runat="server" OnRowDataBound="gvgrad_RowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="Plan Graduación" />
                                            <asp:BoundField DataField="Abandonada" />
                                            <asp:BoundField DataField="Completa" />
                                            <asp:BoundField DataField="Pendiente" />
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table class="tableCont">
                            <tr>
                                <th>
                                    <asp:Label ID="lblvisitaafil" runat="server"></asp:Label></th>
                                <td>
                                    <asp:Label ID="lblVvisitaafil" runat="server"></asp:Label></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
</asp:Content>
