<%@ Page Language="C#" Async="true" AutoEventWireup="true" MasterPageFile="~/mast.Master" CodeBehind="Clasificacion.aspx.cs" Inherits="Familias3._1.TS.HistorialClasificacion" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ MasterType VirtualPath="~/mast.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="formContGlobal">
        <table class="tblCenter">
            <tr>
                <td>
                    <asp:Label ID="lblDirec" runat="server" Text="" class="labelBoldForm"></asp:Label>&nbsp;
                </td>
                <td style="text-align: center">
                    <asp:Label ID="lblVDirec" runat="server" class="labelForm"></asp:Label>
                </td>
                <td></td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblTelef" runat="server" Text="" class="labelBoldForm"></asp:Label>&nbsp;
                </td>
                <td style="text-align: center">
                    <asp:Label ID="lblVTelef" runat="server" class="labelForm"></asp:Label>
                </td>
                <td></td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblTSU" runat="server" Text="" class="labelBoldForm"></asp:Label>&nbsp;
                </td>
                <td style="text-align: center">
                    <asp:Label ID="lblVTS" runat="server" class="labelForm"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblFechaClasActual" runat="server" Text="Fecha Clasificación Actual:" CssClass="labelBoldForm"></asp:Label>
                </td>
                <td style="text-align: center">
                    <asp:Label ID="lblVFechaClasActual" runat="server" Text="" CssClass="labelForm"></asp:Label>
                </td>
                <td></td>
                <td>&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="lblClasActual" runat="server" Text="Clasificación Actual:" CssClass="labelBoldForm"></asp:Label>
                </td>
                <td style="text-align: center">
                    <asp:Label ID="lblVClasActual" runat="server" Text="" CssClass="labelForm"></asp:Label>
                </td>
                <td></td>
                <td>&nbsp;&nbsp;&nbsp;
                        <%--<table>
                            <tr>
                                <td>--%>
                    <asp:Button ID="btnNuevaSeleccion" runat="server" Text="Nueva Selección" CssClass="butonFormRet" OnClick="btnNuevaSeleccion_Click" />
                    <asp:Button ID="btnCambiarClas" runat="server" Text="Cambiar Clasificación" CssClass="butonForm" OnClick="btnCambiarClas_Click" />
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="butonForm" OnClick="btnGuardar_Click" />
                    <%--</td>
                                <td>--%>
                    <%--</td>
                            </tr>
                        </table>--%>
                </td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblAñoNuevaClas" runat="server" Text="Año Nueva Clasificación:" CssClass="labelBoldForm"></asp:Label>
                </td>
                <td style="text-align: center">
                    <asp:Label ID="lblVAñoNuevaClas" runat="server" Text="" CssClass="labelForm"></asp:Label>
                </td>
                <td></td>
                <td>&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="lblNuevaClas" runat="server" Text="Nueva Clasificación:" CssClass="labelBoldForm"></asp:Label>
                </td>
                <td style="text-align: center">
                    <asp:Label ID="lblVNuevaClas" runat="server" Style="font-size: 12px" Text="" CssClass="labelForm"></asp:Label>
                </td>
                <td></td>
                <td></td>
                <td></td>
            </tr>
        </table>
        <asp:Panel runat="server" ID="pnlHistorial" Visible="true" class="formContTrans" style="text-align:center">
                <table class="tblCenter">
                    <tr>
                        <td>
                            <table class="tableContInfo tblCenter gray">
                                <tr>
                                    <th>
                                        <asp:Label ID="lblHistorial" runat="server">Historial</asp:Label>
                                    </th>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:GridView runat="server" ID="gdvHistorialClasif" AutoGenerateColumns="false" CssClass="tableContInfo" OnRowCommand="gdvHistorialClasif_RowCommand">
                                            <Columns>
                                                <asp:BoundField DataField="CreationDateTime" />
                                                <asp:BoundField DataField="YearClassification" />
                                                <asp:BoundField DataField="Classification" />
                                                <asp:BoundField DataField="Inactive" />
                                                <asp:BoundField DataField="Año" />
                                                <asp:BoundField DataField="Clasif" />
                                                <asp:BoundField DataField="FechaRegistro" />
                                                <asp:BoundField DataField="Activa" />
                                                <asp:BoundField DataField="Usuario" />
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnSeleccionar" runat="server"
                                                            CommandName="cmdSeleccionar"
                                                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                            Text='<%#dic.seleccionar%>' CssClass="butonFormTable" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlAsignarClasif" Visible="false" CssClass="formContTrans" style="text-align:center">
            <table class="tblCenter">
                <tr>
                    <td>
                        <asp:GridView runat="server" ID="gdvCondiciones" AutoGenerateColumns="false" CssClass="tableContInfo" OnRowCommand="gdvCondiciones_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="Code" />
                                <%--<asp:BoundField DataField="DescSpanish" />
                                <asp:BoundField DataField="Comments" />--%>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <div style="text-align: left">
                                            <b>
                                                <asp:Label runat="server" ID="lblCondicion" Text='<%# Eval("DescSpanish").ToString() + ": "%>'></asp:Label></b><br />
                                            <asp:Label runat="server" ID="lblComentario" Text='<%# Eval("Comments").ToString()%>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblAplica" Text='<%# (Eval("Points").ToString()=="1") ? dic.Si : dic.No %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkAplica" runat="server"
                                            Checked='<%#Convert.ToBoolean(Eval("Points")) %>'
                                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                            CommandName="cmdSeleccionar" onclick="javascript:changeLabelText(this.offsetParent.offsetParent.id);" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <script>
        (function () { changeLabelText("ContentPlaceHolder1_gdvCondiciones") })();
        function changeLabelText(gridViewName) {
            var contadorCondiciones = 0;
            var tabla = document.getElementById(gridViewName);
            var nFilas = tabla.rows.length;
            var ch;
            for (var i = 1; i < nFilas; i++) {
                ch = tabla.rows[i].cells[1].firstElementChild.firstElementChild;
                if (ch.checked) {
                    contadorCondiciones++;
                }
            }
            var clasificacion;
            if (contadorCondiciones > 0) {
                if ((contadorCondiciones == 1) || (contadorCondiciones == 2)) {
                    clasificacion = "C";
                }
                else if ((contadorCondiciones == 3) || (contadorCondiciones == 4)) {
                    clasificacion = "B";
                }
                else if (contadorCondiciones >= 5) {
                    clasificacion = "A";
                }
            }
            else {
                clasificacion = "";
            }
            var label = document.getElementById("ContentPlaceHolder1_lblVNuevaClas");
            label.innerHTML = "<b>" + clasificacion + "</b>";
            var label = document.getElementById("lblVNuevaClas");
            label.innerHTML = "<b>" + clasificacion + "</b>";
        }
    </script>
</asp:Content>
