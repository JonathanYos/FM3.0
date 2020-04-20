<%@ Page Language="C#" MasterPageFile="~/mast.Master" AutoEventWireup="true" CodeBehind="Viveres.aspx.cs" Inherits="Familias3._1.TS.Viveres" %>

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
                        <asp:Panel ID="pnlRegistro" runat="server">
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
                                        <asp:Label ID="lblrazon" CssClass="labelBoldForm" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlrazon" runat="server" CssClass="comboBoxBlueForm">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="revDdlrazon" runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="ddlrazon" ValidationGroup="grpIngresar" Display="None"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="revDdlrazon_ValidatorCalloutExtender" runat="server" BehaviorID="revDdlrazon_ValidatorCalloutExtender" TargetControlID="revDdlrazon">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                        <asp:Label ID="lblVRazon" CssClass="labelForm" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:Label ID="lblCantidad" CssClass="labelBoldForm" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlCantidad" runat="server" CssClass="comboBoxBlueForm">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="revDdlCantidad" runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="ddlCantidad" ValidationGroup="grpIngresar" Display="None"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="revDdlCantidad_ValidatorCalloutExtender" runat="server" BehaviorID="revDdlCantidad_ValidatorCalloutExtender" TargetControlID="revDdlCantidad">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                        <asp:Label ID="lblVCantidad" CssClass="labelForm" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblnotas" runat="server" AutoCompleteType="Disabled" CssClass="labelBoldForm" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtnotas" runat="server" CssClass="textBoxBlueForm" MaxLength="40"></asp:TextBox>

                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:Label ID="lblFrecuencia" CssClass="labelBoldForm" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlFrecuencia" runat="server" CssClass="comboBoxBlueForm">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="revDdlFrecuencia" runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="ddlFrecuencia" ValidationGroup="grpIngresar" Display="None"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="revDdlFrecuencia_ValidatorCalloutExtender" runat="server" BehaviorID="revDdlFrecuencia_ValidatorCalloutExtender" TargetControlID="revDdlFrecuencia">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                        <asp:Label ID="lblVFrecuencia" CssClass="labelForm" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td colspan="2">
                                        <asp:Button ID="btnaceptar" runat="server" Text="" CssClass="butonForm" ValidationGroup="grpIngresar" OnClick="btnaceptar_Click" />
                                        <asp:Button ID="btnmodificar" runat="server" Text="" CssClass="butonForm" OnClick="btnmodificar_Click" />
                                        <asp:Button ID="btncancelar" runat="server" Text="" CssClass="butonFormRet" Visible="false" OnClick="btncancelar_Click" />
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
                                                        <asp:Button ID="btnEliminar" runat="server"
                                                            CommandName="cmdEliminar"
                                                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                            Text='<%#dic.eliminar%>'
                                                            CssClass="butonFormSecTable" />
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
