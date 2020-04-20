<%@ Page Language="C#" Async="true" AutoEventWireup="true" MasterPageFile="~/mast.Master" CodeBehind="ClasificacionAnoSiguiente.aspx.cs" Inherits="Familias3._1.TS.AsignarClasificacion" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ MasterType VirtualPath="~/mast.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <asp:Panel runat="server" ID="pnlFamilias" CssClass="formContGlobalReport">
            <table class="tblCenter">
                <tr>
                    <td>
                        <table class="tblCenter">
                            <tr>
                                <td>
                                    <asp:Label ID="lblTS" runat="server" Text="Trabajador Social:" CssClass="labelBoldForm"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlTS" runat="server" CssClass="comboBoxBlueForm"></asp:DropDownList>
                                </td>
                                <td style="width: 20px"></td>
                                <td>
                                    <asp:Label ID="lblRegion" runat="server" Text="Región:" CssClass="labelBoldForm"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlRegion" runat="server" CssClass="comboBoxBlueForm"></asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="butonForm" OnClick="btnBuscar_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table class="tblCenter">
                <tr>
                    <td>
                        <asp:Panel runat="server" ID="pnlGdvFamilias" CssClass="scroll-Y" Style="height: 75vh">
                            <asp:GridView runat="server" ID="gdvFamilias" CssClass="tableContInfo" AutoGenerateColumns="false" AllowCustomPaging="False" OnRowCommand="gdvFamilias_RowCommand" OnRowDataBound="gdvFamilias_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="FamilyId" />
                                    <asp:BoundField DataField="Familia" />
                                    <asp:BoundField DataField="ClasificacionActual" />
                                    <asp:BoundField DataField="ClasificacionSiguiente" />
                                    <asp:BoundField DataField="TS" />
                                    <asp:BoundField DataField="Area" />
                                    <asp:BoundField DataField="Direccion" />
                                    <asp:BoundField DataField="JefeCasa" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="btnActualizar" runat="server"
                                                CommandName="cmdActualizar"
                                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                Text='<%#dic.actualizar%>'
                                                Visible='<%# (Eval("Accion").ToString()=="A") ? true : false%>'
                                                CssClass="butonFormSecTable" />
                                            <asp:Button ID="btnIngresar" runat="server"
                                                CommandName="cmdIngresar"
                                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                Text='<%#dic.ingresar%>'
                                                Visible='<%# (Eval("Accion").ToString()=="I") ? true : false%>'
                                                CssClass="butonFormTable" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlAsignarClasif" Visible="false" CssClass="formContGlobal">
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
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="butonForm" OnClick="btnGuardar_Click" />
                        <%--</td>
                                <td>--%>
                        <asp:Button ID="btnNuevaSeleccion" runat="server" Text="Nueva Selección" CssClass="butonFormRet" OnClick="btnNuevaSeleccion_Click" />
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
            <div class="formContTrans">
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
            </div>
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
