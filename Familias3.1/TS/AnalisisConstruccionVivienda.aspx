<%@ Page Language="C#" Async="true" AutoEventWireup="true" MasterPageFile="~/mast.Master" CodeBehind="AnalisisConstruccionVivienda.aspx.cs" Inherits="Familias3._1.TS.AnalisisConstruccionVivienda" %>

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
                <tr>
                    <td>
                        <asp:Panel runat="server" ID="pnlGdvFamilias" CssClass="scroll-Y" Style="height: 75vh">
                            <asp:GridView runat="server" ID="gdvFamilias" CssClass="tableContInfo" AutoGenerateColumns="false" AllowCustomPaging="False" OnRowCommand="gdvFamilias_RowCommand">
                                <Columns>
                                    <asp:BoundField DataField="FamilyId" />
                                    <asp:BoundField DataField="Familia" />
                                    <asp:BoundField DataField="Tiene" />
                                    <asp:BoundField DataField="Clasificacion" />
                                    <asp:BoundField DataField="TS" />
                                    <asp:BoundField DataField="Area" />
                                    <asp:BoundField DataField="Direccion" />
                                    <asp:BoundField DataField="JefeCasa" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="btnSeleccionar" runat="server"
                                                CommandName="cmdSeleccionar"
                                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                Text='<%#dic.seleccionar%>'
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
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="lblDirec" runat="server" Text="" class="labelBoldForm"></asp:Label>&nbsp;
                                </td>
                                <td style="text-align: left">&nbsp;
                                    <asp:Label ID="lblVDirec" runat="server" class="labelForm"></asp:Label>
                                </td>
                                <td></td>
                                <td>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblTelef" runat="server" Text="" class="labelBoldForm"></asp:Label>&nbsp;
                                </td>
                                <td style="text-align: left">
                                    <asp:Label ID="lblVTelef" runat="server" class="labelForm"></asp:Label>
                                </td>
                                <td></td>
                                <td>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblTSU" runat="server" Text="" class="labelBoldForm"></asp:Label>&nbsp;
                                </td>
                                <td style="text-align: left">
                                    <asp:Label ID="lblVTS" runat="server" class="labelForm"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblTamaño" runat="server" Text="Tamaño:" CssClass="labelBoldForm"></asp:Label>
                                </td>
                                <td style="text-align: left">&nbsp;
                                    <asp:Label ID="lblVTamaño" runat="server" Text="" CssClass="labelForm"></asp:Label>
                                </td>
                                <td></td>
                                <td>&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="lblTenencia" runat="server" Text="Tenencia:" CssClass="labelBoldForm"></asp:Label>
                                </td>
                                <td style="text-align: left">
                                    <asp:Label ID="lblVTenencia" runat="server" Text="" CssClass="labelForm"></asp:Label>
                                </td>
                                <td></td>
                                <td>&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lblNCuartos" runat="server" Text="Número de Cuartos:" CssClass="labelBoldForm"></asp:Label>
                                </td>
                                <td style="text-align: left">
                                    <asp:Label ID="lblVNCuartos" runat="server" Text="" CssClass="labelForm"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblPared" runat="server" Text="Paredes:" CssClass="labelBoldForm"></asp:Label>
                                </td>
                                <td style="text-align: left">&nbsp;
                                    <asp:Label ID="lblVPared" runat="server" Text="" CssClass="labelForm"></asp:Label>
                                </td>
                                <td></td>
                                <td>&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="lblPiso" runat="server" Text="Piso:" CssClass="labelBoldForm"></asp:Label>
                                </td>
                                <td style="text-align: left">
                                    <asp:Label ID="lblVPiso" runat="server" Text="" CssClass="labelForm"></asp:Label>
                                </td>
                                <td></td>
                                <td>&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="lblCocinaCon" runat="server" Text="Cocina con:" CssClass="labelBoldForm"></asp:Label>
                                </td>
                                <td style="text-align: left">
                                    <asp:Label ID="lblVCocinaCon" runat="server" Text="" CssClass="labelForm"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div style="height:40px"></div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblDiagnostico" CssClass="labelBoldForm" runat="server" Text=""></asp:Label>
                                </td>
                                <td colspan="7">&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:DropDownList ID="ddlDiagnostico" runat="server" Width="800px" CssClass="comboBoxBlueForm" AutoPostBack="true" OnSelectedIndexChanged="ddlDiagnostico_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="revDdlDiagnostico" runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="ddlDiagnostico" ValidationGroup="grpIngresar" Display="None"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="revDdlDiagnostico_ValidatorCalloutExtender" runat="server" BehaviorID="revDdlDiagnostico_ValidatorCalloutExtender" TargetControlID="revDdlDiagnostico">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblAñoAnalisis" runat="server" CssClass="labelBoldForm" Text="Año de Análisis:"></asp:Label>
                                </td>
                                <td style="text-align: left">&nbsp;
                                    
                                    <asp:Label ID="lblVAñoAnalisis" runat="server" CssClass="labelForm"></asp:Label>
                                </td>
                                <td></td>
                                <td>&nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="lblAplica" runat="server" CssClass="labelBoldForm" Text=""></asp:Label>
                                </td>
                                <td style="text-align: left">
                                    <asp:Label ID="lblVAplica" runat="server" CssClass="labelForm"></asp:Label>
                                </td>
                                <td></td>
                                <td colspan="2">&nbsp;&nbsp;&nbsp;
                                    <asp:CheckBox ID="ckbPreOPost" runat="server" AutoPostBack="true" OnCheckedChanged="ckbPreOPost_CheckedChanged"/>
                                    <asp:Label ID="lblpost" CssClass="labelBoldForm" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblComentario" runat="server" CssClass="labelBoldForm" Text=""></asp:Label>
                                </td>
                                <td colspan="7">&nbsp;
                                    <asp:Label ID="lblVComentario" runat="server" CssClass="labelForm"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblNotas" runat="server" CssClass="labelBoldForm" Text=""></asp:Label>
                                </td>
                                <td colspan="7">&nbsp;
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txbNotas" runat="server" CssClass="textBoxBlueForm"></asp:TextBox>
                                            </td>
                                            <td>&nbsp;&nbsp;&nbsp;
                                                <asp:Button ID="btnNuevaSeleccion" runat="server" Text="Nueva Selección" CssClass="butonFormRet" OnClick="btnNuevaSeleccion_Click" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="butonForm" ValidationGroup="grpIngresar" OnClick="btnGuardar_Click" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btnEliminarAnalisis" runat="server" Text="Eliminar Análisis" CssClass="butonFormSec" OnClick="btnEliminarAnalisis_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div style="height: 30px"></div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table class="tblCenter">
                            <tr>
                                <td>
                                    <asp:Panel runat="server" ID="pnlVvnd" CssClass="tabla tamano1 scroll-X">
                                        <table class="tableContInfo tblCenter gray">
                                            <tr>
                                                <th>
                                                    <asp:Label ID="lblVvd" runat="server">Historial de Análisis</asp:Label>
                                                </th>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:GridView ID="gdvVvd" runat="server" CssClass="tableContInfo gray" AutoGenerateColumns="false">
                                                        <Columns>
                                                            <asp:BoundField DataField="Año" />
                                                            <asp:BoundField DataField="Aplica" />
                                                            <asp:BoundField DataField="Tipo" />
                                                            <asp:BoundField DataField="Diagnostico" />
                                                            <asp:BoundField DataField="Comentario" />
                                                            <asp:BoundField DataField="Notas" />
                                                            <asp:BoundField DataField="Solicitud" />
                                                            <asp:BoundField DataField="Estado" />
                                                            <asp:BoundField DataField="Hrs Requeridas" />
                                                            <asp:BoundField DataField="Exoneracion" />
                                                            <asp:BoundField DataField="Hrs Trabajadas" />
                                                            <%--<asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:Button ID="btnSeleccionar" runat="server"
                                                                        CommandName="cmdSeleccionar"
                                                                        CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                                        Text='<%#dic.seleccionar%>'
                                                                        CssClass="butonFormTable" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>--%>
                                                        </Columns>
                                                    </asp:GridView>
                                                    <asp:Label ID="lblVVnNoTiene" runat="server" Visible="false" CssClass="labelFormSec"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
</asp:Content>

