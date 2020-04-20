<%@ Page Language="C#" Async="true" AutoEventWireup="true" MasterPageFile="~/mast.Master" CodeBehind="Resumen.aspx.cs" Inherits="Familias3._1.TS.Resumen" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="formContGlobalReport">
        <div>
            <table class="tblCenter">
                <tr>
                    <td>
                        <asp:Label ID="lblTSU" runat="server" Text="" class="labelBoldForm"></asp:Label>&nbsp;
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
        <div>
            <asp:Panel runat="server" ID="pnlContenedor" CssClass="contenedor">
                <div style="text-align: center">
                    <table>
                        <tr>
                            <td>
                                <asp:LinkButton ID="lnkInfoGeneral" CssClass="cabecera c-activa" runat="server" OnClick="lnkInfoGeneral_Click">Información General</asp:LinkButton>
                                <asp:LinkButton ID="lnkInfoBecas" CssClass="cabecera " runat="server" OnClick="lnkInfoBecas_Click">Becas</asp:LinkButton>
                                <asp:LinkButton ID="lnkInfoProgramas" CssClass="cabecera " runat="server" OnClick="lnkInfoProgramas_Click">Programas</asp:LinkButton>
                                <asp:LinkButton ID="lnkInfoApadAfil" CssClass="cabecera " runat="server" OnClick="lnkInfoApadAfil_Click">Apadrinamiento</asp:LinkButton>
                                <asp:LinkButton ID="lnkInfoOtros" CssClass="cabecera " runat="server" OnClick="lnkInfoOtros_Click">Otros</asp:LinkButton>
                            </td>
                            <td>&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblCantMeses" CssClass="labelBoldForm"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txbCantMeses" AutoCompleteType="Disabled" CssClass="textBoxBlueForm num3" MaxLength="2" onkeypress='return esDigito(event)' onkeyup='this.value=retornaSoloDigitos(this.value)' Text="4"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="revtxbCantMeses" runat="server" ControlToValidate="txbCantMeses" ErrorMessage="RequiredFieldValidator" ValidationGroup="grpBuscar" Display="none"></asp:RequiredFieldValidator>
                                <ajaxToolkit:ValidatorCalloutExtender ID="revTxbCantMeses_ValidatorCalloutExtender" runat="server" BehaviorID="revTxbCantMeses_ValidatorCalloutExtender" TargetControlID="revTxbCantMeses"></ajaxToolkit:ValidatorCalloutExtender>
                            </td>
                            <td>
                                <asp:Button runat="server" ID="btnBuscar" CssClass="butonForm" ValidationGroup="grpBuscar" OnClick="btnBuscar_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <style>
                    .tabla {
                        display: inline-block;
                        float: left;
                        margin-left: 10px;
                        margin-top: 10px;
                    }

                    .tamano2 {
                        width: 100%;
                    }

                    .tabla3 {
                        float: left;
                        margin-top: 10px;
                        margin-left: 10px;
                    }

                    .tamanocom {
                        width: 100%;
                    }
                </style>
                <asp:Panel runat="server" ID="pnlInfoGeneral" CssClass="text-lexft">
                    <table class="tamanocom">
                        <tr>
                            <td colspan="2">
                                <asp:Panel runat="server" ID="pnlMiembros" CssClass="tabla tamano1 scroll-X special">
                                    <table class="tableContInfo blue">
                                        <tr>
                                            <th>
                                                <asp:Label ID="lblInfoGeneral" runat="server">Información General</asp:Label>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="gdvMiembros" runat="server" CssClass="tableContInfo aliacion blue" AutoGenerateColumns="false" OnRowCommand="gdvMiembros_RowCommand">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnMName" runat="server"
                                                                    CommandName="cmdMName"
                                                                    CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                                    Text='<%# Eval("MemberId") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Nombre" />
                                                        <asp:BoundField DataField="Nacimiento" />
                                                        <asp:BoundField DataField="Relacion" />
                                                        <asp:BoundField DataField="RazonInactivo" />
                                                        <asp:BoundField DataField="Celular" />
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel runat="server" ID="pnlTS" CssClass="block scroll-X tabla3">
                                    <table class="tableContInfo tblCenter gray">
                                        <tr>
                                            <th>
                                                <asp:Label ID="lblTS" runat="server">Trabajo Social</asp:Label>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="gdvTS" runat="server" CssClass="tableContInfo gray" AutoGenerateColumns="false">
                                                    <Columns>
                                                        <asp:BoundField DataField="Actividad" />
                                                        <asp:BoundField DataField="Fecha" />
                                                    </Columns>
                                                </asp:GridView>
                                                <asp:Label ID="lblTSNoTiene" runat="server" Visible="false" CssClass="labelFormSec"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
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
                    </table>
                </asp:Panel>
                <asp:Panel runat="server" ID="pnlInfoBecas" Visible="false" CssClass="text-lexft">
                    <table class="tamanocom">
                        <tr>
                            <td>
                                <asp:Panel runat="server" ID="pnlNotas" CssClass="block scroll-X tabla3">
                                    <table class="tableContInfo tblCenter yellow">
                                        <tr>
                                            <th>
                                                <asp:Label ID="lblNotas" runat="server">Notas</asp:Label>
                                                <asp:Button ID="btnMostrarTodasNotas" class="butonFormSec btnTable" runat="server" Text="Todas las Notas" OnClick="btnMostrarTodasNotas_Click" />
                                            </th>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="gdvNotas" runat="server" CssClass="tableContInfo yellow" AutoGenerateColumns="false" OnRowDataBound="gdvNotas_RowDataBound">
                                                    <Columns>
                                                        <asp:BoundField DataField="miembroId" />
                                                        <asp:BoundField DataField="nombre" />
                                                        <asp:BoundField DataField="fase" />
                                                        <asp:BoundField DataField="unidad" />
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Panel ID="pnlSemaforo" runat="server" CssClass="pnlVSemaforo" BackColor='<%# (Eval("semaforo").ToString()=="Rojo") ? colorMalo :  (Eval("semaforo").ToString()=="Amarillo") ? colorRegular : colorBueno%>'></asp:Panel>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="ganoTodas" />
                                                        <asp:BoundField DataField="numPerdidas" />
                                                        <asp:BoundField DataField="fuente" />
                                                    </Columns>
                                                </asp:GridView>
                                                <asp:Label ID="lblNotasNoTiene" runat="server" Visible="false" CssClass="labelFormSec"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel runat="server" ID="pnlEducGeneral" CssClass="tabla tamano1 scroll-X special">
                                    <table class="tableContInfo orange">
                                        <tr>
                                            <th>
                                                <asp:Label ID="lblInfoEducGeneral" runat="server">Información General Educátiva</asp:Label>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="gdvEducGeneral" runat="server" CssClass="tableCont aliacion orange" AutoGenerateColumns="false">
                                                    <Columns>
                                                        <asp:BoundField DataField="Nombre" />
                                                        <asp:BoundField DataField="Afiliación" />
                                                        <asp:BoundField DataField="Fase" />
                                                        <asp:BoundField DataField="Educacion" />
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel runat="server" ID="pnlEduc" CssClass="block scroll-X tabla3">
                                    <table class="tableContInfo tblCenter yellow">
                                        <tr>
                                            <th>
                                                <asp:Label ID="lblEduc" runat="server">Becas</asp:Label>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="gdvEduc" runat="server" CssClass="tableContInfo yellow" AutoGenerateColumns="false">
                                                    <Columns>
                                                        <asp:BoundField DataField="Actividad" />
                                                        <asp:BoundField DataField="Fecha" />
                                                    </Columns>
                                                </asp:GridView>
                                                <asp:Label ID="lblEducNoTiene" runat="server" Visible="false" CssClass="labelFormSec"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel runat="server" ID="pnlInfoProgramas" Visible="false" CssClass="text-lexft">
                    <table>
                        <tr>
                            <td colspan="2">
                                <asp:Panel runat="server" ID="pnlPsic" CssClass="block scroll-X  tabla3">
                                    <table class="tableContInfo tblCenter purple">
                                        <tr>
                                            <th>
                                                <asp:Label ID="lblPsic" runat="server">Apoyo Educativos</asp:Label>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="gdvPsic" runat="server" CssClass="tableContInfo purple" AutoGenerateColumns="false">
                                                    <Columns>
                                                        <asp:BoundField DataField="Actividad" />
                                                        <asp:BoundField DataField="Fecha" />
                                                    </Columns>
                                                </asp:GridView>
                                                <asp:Label ID="lblPsicNoTiene" runat="server" Visible="false" CssClass="labelFormSec"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel runat="server" ID="pnlApoyoJov" CssClass=" scroll-X  tabla3">
                                    <table class="tableContInfo tblCenter pink">
                                        <tr>
                                            <th>
                                                <asp:Label ID="lblApoJov" runat="server">Apoyo a Jóvenes</asp:Label>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="gdvApoyoJov" runat="server" CssClass="tableContInfo pink" AutoGenerateColumns="false">
                                                    <Columns>
                                                        <asp:BoundField DataField="Actividad" />
                                                        <asp:BoundField DataField="Nombre" />
                                                        <asp:BoundField DataField="Ene" />
                                                        <asp:BoundField DataField="Feb" />
                                                        <asp:BoundField DataField="Mar" />
                                                        <asp:BoundField DataField="Abr" />
                                                        <asp:BoundField DataField="May" />
                                                        <asp:BoundField DataField="Jun" />
                                                        <asp:BoundField DataField="Jul" />
                                                        <asp:BoundField DataField="Ago" />
                                                        <asp:BoundField DataField="Sep" />
                                                        <asp:BoundField DataField="Oct" />
                                                        <asp:BoundField DataField="Nov" />
                                                        <asp:BoundField DataField="Dic" />
                                                        <asp:BoundField DataField="Total" />
                                                    </Columns>
                                                </asp:GridView>
                                                <asp:Label ID="lblApoyoJovNoTiene" runat="server" Visible="false" CssClass="labelFormSec"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel runat="server" ID="pnlInfoApadAfil" Visible="false" CssClass="text-lexft">
                    <table class="tamanocom">
                        <tr>
                            <td colspan="2">
                                <asp:Panel runat="server" ID="pnlApad" CssClass="block scroll-X tabla3">
                                    <table class="tableContInfo tblCenter pink">
                                        <tr>
                                            <th>
                                                <asp:Label ID="lblApad" runat="server">Apadrinamiento</asp:Label>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="gdvApad" runat="server" CssClass="tableContInfo pink" AutoGenerateColumns="false">
                                                    <Columns>
                                                        <asp:BoundField DataField="Actividad" />
                                                        <asp:BoundField DataField="Fecha" />
                                                    </Columns>
                                                </asp:GridView>
                                                <asp:Label ID="lblApadNoTiene" runat="server" Visible="false" CssClass="labelFormSec"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel runat="server" ID="pnlNADFAS" CssClass="tabla tamano1">
                                    <table class="tableContInfo tblCenter">
                                        <tr>
                                            <th class="center">
                                                <asp:Label runat="server" ID="lblNADFASs">Historial de NADFAS</asp:Label>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="gdvNADFAS" runat="server" CssClass="tableContInfo" AutoGenerateColumns="False">
                                                    <Columns>
                                                        <asp:BoundField DataField="Nombre" />
                                                        <asp:BoundField DataField="FechaInicio" />
                                                    </Columns>
                                                </asp:GridView>
                                                <asp:Label runat="server" ID="lblNADFASNoTiene" CssClass="labelFormSec"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel runat="server" ID="pnlInfoOtros" Visible="false" CssClass="text-lexft">
                    <table class="tamanocom">
                        <tr>
                            <td>
                                <asp:Panel runat="server" ID="Salud" CssClass="block scroll-X  tabla3">
                                    <table class="tableContInfo tblCenter sky">
                                        <tr>
                                            <th>
                                                <asp:Label ID="lblSalud" runat="server">Salud</asp:Label>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="gdvSalud" runat="server" CssClass="tableContInfo sky" AutoGenerateColumns="false">
                                                    <Columns>
                                                        <asp:BoundField DataField="Actividad" />
                                                        <asp:BoundField DataField="Fecha" />
                                                    </Columns>
                                                </asp:GridView>
                                                <asp:Label ID="lblSaludNoTiene" runat="server" Visible="false" CssClass="labelFormSec"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel runat="server" ID="pnlCaja" CssClass="block scroll-X tabla3">
                                    <table class="tableContInfo tblCenter sky">
                                        <tr>
                                            <th colspan="2">
                                                <asp:Label ID="lblCaja" runat="server">Caja</asp:Label>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td class="left">
                                                <asp:Label ID="lblCuentaActiva" runat="server" Text="Cuenta Activa"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblVCuentaActiva" Text="No" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="left">
                                                <asp:Label ID="lblActualizacion" runat="server" Text="Actualización"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblVActualizacion" Text="-" runat="server"></asp:Label>
                                            </td>
                                            <%--<asp:GridView ID="gdvCaja" runat="server" CssClass="tableContInfo sky" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:BoundField DataField="Cta_Activa" />
                                                <asp:BoundField DataField="Actualización" />
                                            </Columns>
                                        </asp:GridView>--%>
                                            <%--<asp:Label ID="lblCajaNoTiene" runat="server" Visible="false" CssClass="labelFormSec"></asp:Label>--%>
                                            <%--</td>--%>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="pnlDiferencias" runat="server" CssClass="block scroll-X tabla3">
                                    <table class="tableContInfo tblCenter blue">
                                        <tr>
                                            <th colspan="2">
                                                <asp:Label runat="server" ID="lblUtilidad">Utilidad</asp:Label>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Table runat="server" ID="tblDiferenciaIngresos" Visible="false" class="tableContInfo tblCenter blue">
                                                    <asp:TableRow>
                                                        <asp:TableCell class="left">
                                                            <asp:Label runat="server" ID="gstLblTotalIngresosOcupaciones"></asp:Label>
                                                        </asp:TableCell>
                                                        <asp:TableCell>
                                                            <asp:Label runat="server" ID="gstLblVTotalIngresosOcupaciones"></asp:Label>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                    <asp:TableRow>
                                                        <asp:TableCell class="left">
                                                            <asp:Label runat="server" ID="gstLblTotalIngresosExtra"></asp:Label>
                                                        </asp:TableCell>
                                                        <asp:TableCell>
                                                            <asp:Label runat="server" ID="gstLblVTotalIngresosExtra"></asp:Label>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                    <asp:TableRow>
                                                        <asp:TableCell class="left">
                                                            <asp:Label runat="server" ID="gstLblGastoMen"></asp:Label>
                                                        </asp:TableCell>
                                                        <asp:TableCell>
                                                            <asp:Label runat="server" ID="gstLblVGastoMen"></asp:Label>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                    <asp:TableRow>
                                                        <asp:TableCell class="left">
                                                            <asp:Label runat="server" ID="gstLblDiferenciaIngreso"></asp:Label>
                                                        </asp:TableCell>
                                                        <asp:TableCell>
                                                            <asp:Label runat="server" ID="gstLblVDiferenciaIngreso"></asp:Label>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                </asp:Table>
                                            </td>
                                            <td>
                                                <asp:Panel runat="server" Visible="false">
                                                    <asp:Table runat="server" ID="tblDiferenciaAporte" Visible="false" class="tableCont tblCenter blue">
                                                        <asp:TableRow>
                                                            <asp:TableCell class="left">
                                                                <asp:Label runat="server" ID="gstLblTotalAportesOcupaciones"></asp:Label>
                                                            </asp:TableCell>
                                                            <asp:TableCell>
                                                                <asp:Label runat="server" ID="gstLblVTotalAportesOcupaciones"></asp:Label>
                                                            </asp:TableCell>
                                                        </asp:TableRow>
                                                        <asp:TableRow>
                                                            <asp:TableCell class="left">
                                                                <asp:Label runat="server" ID="gstLblGastoMen2"></asp:Label>
                                                            </asp:TableCell>
                                                            <asp:TableCell>
                                                                <asp:Label runat="server" ID="gstLblVGastosMen2"></asp:Label>
                                                            </asp:TableCell>
                                                        </asp:TableRow>
                                                        <asp:TableRow>
                                                            <asp:TableCell class="left">
                                                                <asp:Label runat="server" ID="gstLblDiferenciaAporte"></asp:Label>
                                                            </asp:TableCell>
                                                            <asp:TableCell>
                                                                <asp:Label runat="server" ID="gstLblVDiferenciaAporte"></asp:Label>
                                                            </asp:TableCell>
                                                        </asp:TableRow>
                                                    </asp:Table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Panel runat="server" ID="pnlVvnd" CssClass="tabla tamano1 scroll-X special">
                                    <table class="tableContInfo tblCenter green">
                                        <tr>
                                            <th>
                                                <asp:Label ID="lblVvd" runat="server">Viviendas</asp:Label>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="gdvVvd" runat="server" CssClass="tableContInfo green" AutoGenerateColumns="false">
                                                    <Columns>
                                                        <asp:BoundField DataField="Año" />
                                                        <asp:BoundField DataField="Tipo" />
                                                        <asp:BoundField DataField="Aplica" />
                                                        <asp:BoundField DataField="Diagnostico" />
                                                        <asp:BoundField DataField="Notas" />
                                                        <asp:BoundField DataField="Solicitud" />
                                                        <asp:BoundField DataField="Estado" />
                                                        <asp:BoundField DataField="Hrs Requeridas" />
                                                        <asp:BoundField DataField="Exoneracion" />
                                                        <asp:BoundField DataField="Hrs Trabajadas" />
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
                </asp:Panel>
            </asp:Panel>
        </div>
    </div>
    <script>
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
        $(document).ready(function ($) {
            var div_ancho = $("#ContentPlaceHolder1_pnlNotas").width();
            var div_alto = $("#ContentPlaceHolder1_pnlPsic").width();
            var resultado;
            if (div_alto > div_ancho) {
                resultado = div_alto - div_ancho + 10;
                $("#ContentPlaceHolder1_pnlAvisos").css("margin-left", resultado);
            } else {
                resultado = div_ancho - div_alto + 10;
                $("#ContentPlaceHolder1_Salud").css("margin-left", resultado);
            }
            var div = $("#ContentPlaceHolder1_pnlTS").width();
            var div2 = $("#ContentPlaceHolder1_pnlApad").width();
            if (div > div2) {
                resultado = div - div2 + 10;
                $("#ContentPlaceHolder1_pnlCaja").css("margin-left", resultado);
            } else {
                resultado = div2 - div;
                $("#ContentPlaceHolder1_pnlEduc").css("margin-left", resultado);

            }
        });
    </script>
</asp:Content>

