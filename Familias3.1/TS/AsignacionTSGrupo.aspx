<%@ Page Language="C#" Async="true" AutoEventWireup="true" MasterPageFile="~/mast.Master" CodeBehind="AsignacionTSGrupo.aspx.cs" Inherits="Familias3._1.TS.AsignarTSGrupo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ MasterType VirtualPath="~/mast.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel runat="server">
        <asp:Panel runat="server" ID="pnlBuscar" CssClass="formContGlobal">
            <div class="formContTrans">
                <table class="tblCenter">
                    <tr>
                        <td>
                            <asp:Label ID="lblTS" runat="server" Text="Nuevo Trabajador Social" CssClass="labelBoldForm"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlTS" runat="server" CssClass="comboBoxBlueForm"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblArea" runat="server" Text="Área" CssClass="labelBoldForm"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlArea" runat="server" CssClass="comboBoxBlueForm"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="butonForm" OnClick="btnBuscar_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlAsignar" runat="server" Visible="False" CssClass="formContGlobalReport">
            <table class="tblCenter">
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblNuevoTS" Text="Nuevo Trabajador Social" CssClass="labelBoldForm"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlNuevoTS" runat="server" CssClass="comboBoxBlueForm"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="revDdlNuevoTS" runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="ddlNuevoTS" ValidationGroup="grpAsignar" Display="None"></asp:RequiredFieldValidator>
                        <ajaxToolkit:ValidatorCalloutExtender ID="revDdlNuevoTS_ValidatorCalloutExtender" runat="server" BehaviorID="revDdlNuevoTS_ValidatorCalloutExtender" TargetControlID="revDdlNuevoTS">
                        </ajaxToolkit:ValidatorCalloutExtender>
                    </td>
                    <td>
                        <asp:Button ID="btnAsignar" runat="server" Text="Asignar" ValidationGroup="grpAsignar" CssClass="butonForm" OnClick="btnAsignar_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnNuevaBusqueda" runat="server" Text="Nueva Búsqueda" CssClass="butonFormRet" OnClick="btnNuevaBusqueda_Click" />
                    </td>
                </tr>
            </table>
            <table class="tblCenter">
                <tr>
                    <td>
                        <asp:Panel runat="server" ID="pnlFamilias" CssClass="scroll-Y" Style="height: 50vh">
                            <asp:GridView runat="server" ID="gdvFamilias" CssClass="tableContInfo" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:BoundField DataField="FamilyId" />
                                    <asp:BoundField DataField="Familia" />
                                    <asp:BoundField DataField="TS" />
                                    <asp:BoundField DataField="FechaInicio" />
                                    <asp:BoundField DataField="JefeCasa" />
                                    <asp:BoundField DataField="Direccion" />
                                    <asp:BoundField DataField="Area" />
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkHeader" runat="server" Checked="true" onclick="javascript:CopyCheckStateByColumn(this,this.offsetParent.offsetParent.id);" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkModificar" runat="server" Checked="true" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </asp:Panel>
    <script>
        function CopyCheckStateByColumn(HeaderCheckBox, gridViewName) {
            var columnIndex = HeaderCheckBox.parentElement.cellIndex;
            var newState = HeaderCheckBox.checked;
            ChangeChecksByColumn(gridViewName, newState, columnIndex);
        }
        function ChangeChecksByColumn(gridViewName, newState, columnIndex) {
            var tabla = document.getElementById(gridViewName);
            var nFilas = tabla.rows.length;
            var nColumnas = 5;
            var nCeldas = nColumnas * nFilas;
            for (var i = 1; i < nFilas; i++) {
                if ((tabla.rows[i].cells[columnIndex].firstElementChild.type == "checkbox") &&
                    (tabla.rows[i].cells[columnIndex].firstElementChild.checked != newState)) {
                    tabla.rows[i].cells[columnIndex].firstElementChild.click();
                }
            }
        }
    </script>
</asp:Content>
