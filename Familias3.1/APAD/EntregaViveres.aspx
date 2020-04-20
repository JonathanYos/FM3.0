<%@ Page Language="C#" MasterPageFile="~/mast.Master" AutoEventWireup="true" CodeBehind="EntregaViveres.aspx.cs" Inherits="Familias3._1.APAD.EntregaViveres" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
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
        <asp:Panel CssClass="formContTrans" runat="server">
            <table class="tblCenter">
                <tr>
                    <td>
                        <asp:Panel ID="pnlRegistro" runat="server" Visible="false">
                            <table class="tblCenter">
                                <tr>
                                    <th></th>
                                    <th></th>
                                    <th style="width: 40px"></th>
                                    <th></th>
                                    <th></th>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblFechaAutorizacion" CssClass="labelBoldForm" runat="server" Text="Fecha Autorización:"></asp:Label>
                                    </td>
                                    <td>&nbsp;&nbsp;<asp:Label ID="lblVFechaAutorizacion" CssClass="labelForm" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:CheckBox runat="server" ID="chkEntrega1" /></td>
                                    <td>
                                        <asp:Label runat="server" ID="lblEntrega1" CssClass="labelBoldForm" Text="Primera Entrega"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblrazon" CssClass="labelBoldForm" runat="server" Text="Razón:"></asp:Label>
                                    </td>
                                    <td>&nbsp;&nbsp;<asp:Label ID="lblVRazon" CssClass="labelForm" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:CheckBox runat="server" ID="chkEntrega2" /></td>
                                    <td>
                                        <asp:Label runat="server" ID="lblEntrega2" CssClass="labelBoldForm" Text="Segunda Entrega"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblFrecuencia" CssClass="labelBoldForm" runat="server" Text="Frecuencia:"></asp:Label>
                                    </td>
                                    <td>&nbsp;&nbsp;<asp:Label ID="lblVFrecuencia" CssClass="labelForm" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:CheckBox runat="server" ID="chkEntrega3" /></td>
                                    <td>
                                        <asp:Label runat="server" ID="lblEntrega3" CssClass="labelBoldForm" Text="Tercera Entrega"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblnotas" runat="server" AutoCompleteType="Disabled" CssClass="labelBoldForm" Text="Notas:"></asp:Label>
                                    </td>
                                    <td>&nbsp;&nbsp;<asp:Label ID="lblVNotas" CssClass="labelForm" runat="server" Text=""></asp:Label>

                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:CheckBox runat="server" ID="chkEntrega4" /></td>
                                    <td>
                                        <asp:Label runat="server" ID="lblEntrega4" CssClass="labelBoldForm" Text="Cuarta Entrega"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td colspan="3"></td>
                                    <td colspan="2">
                                        <asp:Button ID="btnmodificar" runat="server" Text="" CssClass="butonForm" OnClick="btnmodificar_Click" />
                                        <asp:Button ID="btnCancelar" runat="server" Text="" CssClass="butonFormSec" OnClick="btnCancelar_Click" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div style="height: 25px"></div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel runat="server" ID="pnlHistorialViveres" CssClass="scroll-Y">
                            <table class="tableContInfo tblCenter gray">
                                <tr>
                                    <th class="center">
                                        <asp:Label ID="lblHistorialViveres" runat="server">Historial Viveres</asp:Label>
                                    </th>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:GridView ID="gdvHistorialViveres" runat="server" AutoGenerateColumns="false" OnRowCommand="gvhistorial_RowCommand" OnRowDataBound="gvhistorial_RowDataBound">
                                            <Columns>
                                                <asp:BoundField DataField="AuthorizationDateTime" />
                                                <asp:BoundField DataField="Reason" />
                                                <asp:BoundField DataField="DeliveryDateTime1" HtmlEncode="false" />
                                                <asp:BoundField DataField="DeliveryDateTime2" HtmlEncode="false" />
                                                <asp:BoundField DataField="DeliveryDateTime3" HtmlEncode="false" />
                                                <asp:BoundField DataField="DeliveryDateTime4" HtmlEncode="false" />
                                                <asp:BoundField DataField="Razon" />
                                                <asp:BoundField DataField="Cantidad" />
                                                <asp:BoundField DataField="Frecuencia" />
                                                <asp:BoundField DataField="FechaAutorizacion" />
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <table style="border-style: none">
                                                            <tr>
                                                                <td style="border-style: none; text-align: left">
                                                                    <asp:Panel ID="pnlEntrega1" runat="server" Visible='<%# (Int32.Parse(Eval("Cantidad").ToString())>=1) ? true :  false%>' CssClass="pnlVIndicador" Style="display: inline-block" BackColor='<%# (!String.IsNullOrEmpty(Eval("DeliveryDateTime1").ToString())) ? colorEntregado :  colorPendiente%>' ToolTip='<%# Eval("FechaEntrega1").ToString() + " - " + Eval("DeliveredBy1").ToString()%>'></asp:Panel>
                                                                    <asp:Panel ID="pnlEntrega2" runat="server" Visible='<%# (Int32.Parse(Eval("Cantidad").ToString())>=2) ? true :  false%>' CssClass="pnlVIndicador" Style="display: inline-block" BackColor='<%# (!String.IsNullOrEmpty(Eval("DeliveryDateTime2").ToString())) ? colorEntregado :  colorPendiente%>' ToolTip='<%# Eval("FechaEntrega2").ToString() + " - " + Eval("DeliveredBy2").ToString()%>'></asp:Panel>
                                                                    <asp:Panel ID="pnlEntrega3" runat="server" Visible='<%# (Int32.Parse(Eval("Cantidad").ToString())>=3) ? true :  false%>' CssClass="pnlVIndicador" Style="display: inline-block" BackColor='<%# (!String.IsNullOrEmpty(Eval("DeliveryDateTime3").ToString())) ? colorEntregado :  colorPendiente%>' ToolTip='<%# Eval("FechaEntrega3").ToString() + " - " + Eval("DeliveredBy3").ToString()%>'></asp:Panel>
                                                                    <asp:Panel ID="pnlEntrega4" runat="server" Visible='<%# (Int32.Parse(Eval("Cantidad").ToString())>=4) ? true :  false%>' CssClass="pnlVIndicador" Style="display: inline-block" BackColor='<%# (!String.IsNullOrEmpty(Eval("DeliveryDateTime4").ToString())) ? colorEntregado :  colorPendiente%>' ToolTip='<%# Eval("FechaEntrega4").ToString() + " - " + Eval("DeliveredBy4").ToString()%>'></asp:Panel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Notas" HtmlEncode="false" />
                                                <asp:BoundField DataField="AutorizadoPor" />
                                                <asp:BoundField DataField="Usuario" />
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnActualizar" runat="server"
                                                            CommandName="cmdActualizar"
                                                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                            Text='<%#dic.actualizar%>'
                                                            CssClass="butonFormTable" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:Label runat="server" ID="lblNoTieneAF" Visible="false" CssClass="labelFormSec"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <script>
        $('.pnlVIndicador').css({
            "margin": "auto auto",
            "border": "1px solid black",
            "border-bottom-left-radius": "10px",
            "border-bottom-right-radius": "10px",
            "border-top-left-radius": "10px",
            "border-top-right-radius": "10px",
            "width": "10px",
            "height": "10px"
        });
    </script>
</asp:Content>
